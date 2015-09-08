#armeabi-v7a, x86, armeabi
ABI=x86
#x86, arm
PLATFORM=x86
TOOLCHAINS_VER=4.9
ANDROID_API=android-12

ANDK_DIR=C:/Users/Egor/Documents/Android/ndk/android-ndk-r10d
ANDK_GCC=$(ANDK_DIR)/toolchains/x86-$(TOOLCHAINS_VER)/prebuilt/windows-x86_64/bin/i686-linux-android-gcc.exe
ANDK_GPP=$(ANDK_DIR)/toolchains/x86-$(TOOLCHAINS_VER)/prebuilt/windows-x86_64/bin/i686-linux-android-g++.exe
ANDK_STRIP=$(ANDK_DIR)/toolchains/x86-$(TOOLCHAINS_VER)/prebuilt/windows-x86_64/bin/i686-linux-android-strip.exe

OUTPUT_DIR=../Bin/Android/$(ABI)
URHO3D_SRC_DIR=../Urho3D/Source
URHO3D_ANDROID_DIR=../Urho3D/Urho3D_Android

C_FLAGS = -fexceptions -fPIC --sysroot=$(ANDK_DIR)/platforms/$(ANDROID_API)/arch-$(PLATFORM) -funwind-tables -funswitch-loops -finline-limit=300 -fsigned-char -no-canonical-prefixes -fdata-sections -ffunction-sections -Wa,--noexecstack  -fstack-protector -fomit-frame-pointer -fstrict-aliasing -O3 -DNDEBUG -isystem $(ANDK_DIR)/platforms/$(ANDROID_API)/arch-$(PLATFORM)/usr/include -isystem $(ANDK_DIR)/sources/cxx-stl/gnu-libstdc++/$(TOOLCHAINS_VER)/include -isystem $(ANDK_DIR)/sources/cxx-stl/gnu-libstdc++/$(TOOLCHAINS_VER)/libs/$(ABI)/include -isystem $(ANDK_DIR)/sources/cxx-stl/gnu-libstdc++/$(TOOLCHAINS_VER)/include/backward -I$(URHO3D_ANDROID_DIR)/include -I$(URHO3D_ANDROID_DIR)/include/Urho3D/ThirdParty -I$(URHO3D_ANDROID_DIR)/include/Urho3D/ThirdParty/Bullet -I$(URHO3D_SRC_DIR)/Source/Samples 

C_DEFINES = -DANDROID -DHAVE_STDINT_H -DKNET_UNIX -DURHO3D_ANGELSCRIPT -DURHO3D_FILEWATCHER -DURHO3D_LOGGING -DURHO3D_NAVIGATION -DURHO3D_NETWORK -DURHO3D_OPENGL -DURHO3D_PHYSICS -DURHO3D_PROFILING -DURHO3D_SSE -DURHO3D_STATIC_DEFINE -DURHO3D_URHO2D

CXX_FLAGS = -fexceptions -frtti -std=c++11 -fpermissive -fPIC --sysroot=$(ANDK_DIR)/platforms/$(ANDROID_API)/arch-$(PLATFORM) -funwind-tables -funswitch-loops -finline-limit=300 -fsigned-char -no-canonical-prefixes -fdata-sections -ffunction-sections -Wa,--noexecstack  -Wno-invalid-offsetof -fstack-protector -fomit-frame-pointer -fstrict-aliasing -O3 -DNDEBUG -isystem $(ANDK_DIR)/platforms/$(ANDROID_API)/arch-$(PLATFORM)/usr/include -isystem $(ANDK_DIR)/sources/cxx-stl/gnu-libstdc++/$(TOOLCHAINS_VER)/include -isystem $(ANDK_DIR)/sources/cxx-stl/gnu-libstdc++/$(TOOLCHAINS_VER)/libs/$(ABI)/include -isystem $(ANDK_DIR)/sources/cxx-stl/gnu-libstdc++/$(TOOLCHAINS_VER)/include/backward -I$(URHO3D_ANDROID_DIR)/include -I$(URHO3D_ANDROID_DIR)/include/Urho3D/ThirdParty -I$(URHO3D_ANDROID_DIR)/include/Urho3D/ThirdParty/Bullet -I$(URHO3D_SRC_DIR)/Source/Samples

CXX_DEFINES = -DANDROID -DHAVE_STDINT_H -DKNET_UNIX -DURHO3D_ANGELSCRIPT -DURHO3D_FILEWATCHER -DURHO3D_LOGGING -DURHO3D_NAVIGATION -DURHO3D_NETWORK -DURHO3D_OPENGL -DURHO3D_PHYSICS -DURHO3D_PROFILING -DURHO3D_SSE -DURHO3D_STATIC_DEFINE -DURHO3D_URHO2D

CreateBin:
	mkdir -p $(OUTPUT_DIR)/tmp

AApplicationProxy.o: CreateBin
	$(ANDK_GPP)   $(CXX_DEFINES) $(CXX_FLAGS) -o $(OUTPUT_DIR)/tmp/ApplicationProxy.cpp.o -c src/ApplicationProxy.cpp
	
AGlue.o: CreateBin
	$(ANDK_GPP)   $(CXX_DEFINES) $(CXX_FLAGS) -o $(OUTPUT_DIR)/tmp/glue.cpp.o -c src/glue.cpp
	
AVector.o: CreateBin
	$(ANDK_GPP)   $(CXX_DEFINES) $(CXX_FLAGS) -o $(OUTPUT_DIR)/tmp/Vector.cpp.o -c src/Vector.cpp
	
ABinding.o: CreateBin
	$(ANDK_GPP)   $(CXX_DEFINES) $(CXX_FLAGS) -o $(OUTPUT_DIR)/tmp/binding.cpp.o -c generated/binding.cpp
	
AEvents.o: CreateBin
	$(ANDK_GPP)   $(CXX_DEFINES) $(CXX_FLAGS) -o $(OUTPUT_DIR)/tmp/events.cpp.o -c generated/events.cpp

ASDL.o: CreateBin
	$(ANDK_GCC)  $(C_DEFINES) $(C_FLAGS) -o $(OUTPUT_DIR)/tmp/SDL_android_main.c.o   -c $(URHO3D_SRC_DIR)/Source/ThirdParty/SDL/src/main/android/SDL_android_main.c

Urho3D_Android:
	mkdir -p $(URHO3D_ANDROID_DIR) && cd $(URHO3D_SRC_DIR) && cmake -E make_directory ../Urho3D_Android && cmake -E chdir ../Urho3D_Android cmake -G "Unix Makefiles" -DCMAKE_TOOLCHAIN_FILE=$(CURDIR)/$(URHO3D_SRC_DIR)/CMake/Toolchains/android.toolchain.cmake .../Urho3D_Android -DANDROID=1 -DANDROID_ABI=$(ABI) $(CURDIR)/$(URHO3D_SRC_DIR)/
	
AlibUrho3D.a: Urho3D_Android
	cd $(URHO3D_ANDROID_DIR) && make Urho3D

libmono-urho.so: CreateBin
libmono-urho.so: AlibUrho3D.a
libmono-urho.so: AApplicationProxy.o
libmono-urho.so: AGlue.o
libmono-urho.so: AVector.o
libmono-urho.so: ABinding.o
libmono-urho.so: AEvents.o
libmono-urho.so: ASDL.o
libmono-urho.so: 
	$(ANDK_GCC)  -fPIC -fexceptions -frtti -fPIC --sysroot=$(ANDK_DIR)/platforms/$(ANDROID_API)/arch-$(PLATFORM) -funwind-tables -funswitch-loops -finline-limit=300 -fsigned-char -no-canonical-prefixes -fdata-sections -ffunction-sections -Wa,--noexecstack  -Wno-invalid-offsetof -fstack-protector -fomit-frame-pointer -fstrict-aliasing -O3 -DNDEBUG  -Wl,--no-undefined -Wl,--gc-sections -Wl,-z,noexecstack -Wl,-z,relro -Wl,-z,now  -shared -Wl,-soname,libmono-urho.so -o $(OUTPUT_DIR)/libmono-urho.so $(OUTPUT_DIR)/tmp/ApplicationProxy.cpp.o $(OUTPUT_DIR)/tmp/Vector.cpp.o $(OUTPUT_DIR)/tmp/Glue.cpp.o $(OUTPUT_DIR)/tmp/events.cpp.o $(OUTPUT_DIR)/tmp/binding.cpp.o $(OUTPUT_DIR)/tmp/SDL_android_main.c.o ../Urho3D/Urho3D_Android/libs/$(ABI)/libUrho3D.a -ldl -llog -landroid -lGLESv1_CM -lGLESv2  "$(ANDK_DIR)/sources/cxx-stl/gnu-libstdc++/$(TOOLCHAINS_VER)/libs/$(ABI)/libgnustl_static.a" "$(ANDK_DIR)/sources/cxx-stl/gnu-libstdc++/$(TOOLCHAINS_VER)/libs/$(ABI)/libsupc++.a" -lm && $(ANDK_STRIP) $(OUTPUT_DIR)/libmono-urho.so
