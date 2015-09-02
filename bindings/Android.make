# ANDROID_NDK -- C:/Users/Egor/Documents/Android/ndk/android-ndk-r10d
ANDK_DIR = $(ANDROID_NDK)
ANDK_GCC = $(ANDK_DIR)/toolchains/x86-4.9/prebuilt/windows-x86_64/bin/i686-linux-android-gcc.exe
ANDK_GPP = $(ANDK_DIR)/toolchains/x86-4.9/prebuilt/windows-x86_64/bin/i686-linux-android-g++.exe

URHO3D_ANDROID_DIR = C:/Projects/urho_android
URHO3D_SRC_DIR = ../Submodules/Urho3D

C_FLAGS = -fexceptions -fPIC --sysroot=$(ANDK_DIR)/platforms/android-12/arch-x86 -funwind-tables -funswitch-loops -finline-limit=300 -fsigned-char -no-canonical-prefixes -fdata-sections -ffunction-sections -Wa,--noexecstack  -fstack-protector -fomit-frame-pointer -fstrict-aliasing -O3 -DNDEBUG -isystem $(ANDK_DIR)/platforms/android-12/arch-x86/usr/include -isystem $(ANDK_DIR)/sources/cxx-stl/gnu-libstdc++/4.9/include -isystem $(ANDK_DIR)/sources/cxx-stl/gnu-libstdc++/4.9/libs/x86/include -isystem $(ANDK_DIR)/sources/cxx-stl/gnu-libstdc++/4.9/include/backward -I$(URHO3D_ANDROID_DIR)/include -I$(URHO3D_ANDROID_DIR)/include/Urho3D/ThirdParty -I$(URHO3D_ANDROID_DIR)/include/Urho3D/ThirdParty/Bullet -I$(URHO3D_SRC_DIR)/Source/Samples 

C_DEFINES = -DANDROID -DHAVE_STDINT_H -DKNET_UNIX -DURHO3D_ANGELSCRIPT -DURHO3D_FILEWATCHER -DURHO3D_LOGGING -DURHO3D_NAVIGATION -DURHO3D_NETWORK -DURHO3D_OPENGL -DURHO3D_PHYSICS -DURHO3D_PROFILING -DURHO3D_SSE -DURHO3D_STATIC_DEFINE -DURHO3D_URHO2D -D_23_Water_EXPORTS

CXX_FLAGS = -fexceptions -frtti -std=c++11 -fpermissive -fPIC --sysroot=$(ANDK_DIR)/platforms/android-12/arch-x86 -funwind-tables -funswitch-loops -finline-limit=300 -fsigned-char -no-canonical-prefixes -fdata-sections -ffunction-sections -Wa,--noexecstack  -Wno-invalid-offsetof -fstack-protector -fomit-frame-pointer -fstrict-aliasing -O3 -DNDEBUG -isystem $(ANDK_DIR)/platforms/android-12/arch-x86/usr/include -isystem $(ANDK_DIR)/sources/cxx-stl/gnu-libstdc++/4.9/include -isystem $(ANDK_DIR)/sources/cxx-stl/gnu-libstdc++/4.9/libs/x86/include -isystem $(ANDK_DIR)/sources/cxx-stl/gnu-libstdc++/4.9/include/backward -I$(URHO3D_ANDROID_DIR)/include -I$(URHO3D_ANDROID_DIR)/include/Urho3D/ThirdParty -I$(URHO3D_ANDROID_DIR)/include/Urho3D/ThirdParty/Bullet -I$(URHO3D_SRC_DIR)/Source/Samples

CXX_DEFINES = -DANDROID -DHAVE_STDINT_H -DKNET_UNIX -DURHO3D_ANGELSCRIPT -DURHO3D_FILEWATCHER -DURHO3D_LOGGING -DURHO3D_NAVIGATION -DURHO3D_NETWORK -DURHO3D_OPENGL -DURHO3D_PHYSICS -DURHO3D_PROFILING -DURHO3D_SSE -DURHO3D_STATIC_DEFINE -DURHO3D_URHO2D -D_23_Water_EXPORTS


AApplicationProxy.o: 
	$(ANDK_GPP)   $(CXX_DEFINES) $(CXX_FLAGS) -o ApplicationProxy.cpp.o -c src/ApplicationProxy.cpp
	
AGlue.o: 
	$(ANDK_GPP)   $(CXX_DEFINES) $(CXX_FLAGS) -o glue.cpp.o -c src/glue.cpp
	
AVector.o: 
	$(ANDK_GPP)   $(CXX_DEFINES) $(CXX_FLAGS) -o Vector.cpp.o -c src/Vector.cpp
	
ABinding.o:
	$(ANDK_GPP)   $(CXX_DEFINES) $(CXX_FLAGS) -o binding.cpp.o -c generated/binding.cpp
	
AEvents.o: 
	$(ANDK_GPP)   $(CXX_DEFINES) $(CXX_FLAGS) -o events.cpp.o -c generated/events.cpp

ASDL.o:
	$(ANDK_GCC)  $(C_DEFINES) $(C_FLAGS) -o SDL_android_main.c.o $(URHO3D_SRC_DIR)/Source/ThirdParty/SDL/src/main/android/SDL_android_main.c 

libUrho3D.a:
	TODO:...
	
libmono-urho.so: ApplicationProxy.o
libmono-urho.so: Glue.o
libmono-urho.so: Vector.o
libmono-urho.so: events.o
libmono-urho.so: binding.o
libmono-urho.so:
	$(ANDK_GCC)  -fPIC -fexceptions -frtti -fPIC --sysroot=$(ANDK_DIR)/platforms/android-12/arch-x86 -funwind-tables -funswitch-loops -finline-limit=300 -fsigned-char -no-canonical-prefixes -fdata-sections -ffunction-sections -Wa,--noexecstack  -Wno-invalid-offsetof -fstack-protector -fomit-frame-pointer -fstrict-aliasing -O3 -DNDEBUG  -Wl,--no-undefined -Wl,--gc-sections -Wl,-z,noexecstack -Wl,-z,relro -Wl,-z,now  -shared -Wl,-soname,libmono-urho.so -o ../../../libs/x86/libmono-urho.so ApplicationProxy.cpp.o Vector.cpp.o Glue.cpp.o events.cpp.o binding.cpp.o SDL_android_main.c.o ../../../libs/x86/libUrho3D.a -ldl -llog -landroid -lGLESv1_CM -lGLESv2  "$(ANDK_DIR)/sources/cxx-stl/gnu-libstdc++/4.9/libs/x86/libgnustl_static.a" "$(ANDK_DIR)/sources/cxx-stl/gnu-libstdc++/4.9/libs/x86/libsupc++.a" -lm
