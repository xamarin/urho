# armv7s, armv7, arm64, i386
ARCH=i386

ifeq ($(ARCH), i386)
TARGET=iPhoneSimulator
else
TARGET=iPhoneOS
endif

SDK_VER=8.4
MIN_IOS_VER=7.0

BIN_DIR=../Bin/iOS
URHO_DIR=../Urho3D/Urho3D_iOS
URHO_FLAGS=-I$(URHO_DIR)/include -I$(URHO_DIR)/include/Urho3D/ThirdParty -I$(URHO_DIR)/include/Urho3D/ThirdParty/Bullet -DURHO3D_SSE -DURHO3D_FILEWATCHER -DURHO3D_PROFILING -DURHO3D_LOGGING -DKNET_UNIX -DURHO3D_OPENGL -DURHO3D_ANGELSCRIPT -DURHO3D_NAVIGATION -DURHO3D_NETWORK -DURHO3D_PHYSICS -DURHO3D_URHO2D -DURHO3D_STATIC_DEFINE -DIOS
URHO_LIBS=-framework AudioToolbox -framework CoreAudio -framework CoreGraphics -framework Foundation -framework OpenGLES -framework QuartzCore -framework UIKit -Wl,-search_paths_first -Wl,-headerpad_max_install_names $(URHO_DIR)/lib/libUrho3D.a -ldl -lpthread
CXXFLAGS=-g -Wno-address-of-temporary -Wno-return-type-c-linkage -Wno-c++11-extensions $(URHO_FLAGS) 

XCODE_BASE=/Applications/Xcode.app/Contents
TARGET_BASE=$(XCODE_BASE)/Developer/Platforms/$(TARGET).platform
XCODE_FRAMEWORKS=$(TARGET_BASE)/Developer/SDKs/$(TARGET)$(SDK_VER).sdk/System/Library/Frameworks/
XCODE_INCLUDES=$(TARGET_BASE)/Developer/SDKs/$(TARGET)$(SDK_VER).sdk/usr/include
XCODE_SDK=$(XCODE_BASE)/Developer/Platforms/$(TARGET).platform/Developer/SDKs/$(TARGET)$(SDK_VER).sdk

CPP=clang++ -arch $(ARCH) -F$(XCODE_FRAMEWORKS) -I$(XCODE_INCLUDES) -miphoneos-version-min=$(MIN_IOS_VER) -isysroot $(XCODE_SDK) $(CXXFLAGS) 

# generates project and compiles it (doesn't work for i386 yet)
libUrho3D.a:
	cd ../Urho3D/Urho3D_iOS && ./compile_$(ARCH)_static_lib.sh

ioslibmono-urho.dylib: iosvector.o iosbinding.o iosglue.o iosevents.o iosApplicationProxy.o
	mkdir -p $(BIN_DIR) && $(CPP) -dynamiclib -g -o $(BIN_DIR)/libmono-urho.dylib -g $(URHO_LIBS) binding.o glue.o vector.o events.o ApplicationProxy.o

iosbinding.o: 
	$(CPP) -c generated/binding.cpp 

iosglue.o: src/glue.cpp src/glue.h 
	$(CPP) -c src/glue.cpp 

iosvector.o: src/vector.cpp src/glue.h 
	$(CPP) -c src/vector.cpp 

iosevents.o: generated/events.cpp
	$(CPP) -c generated/events.cpp

iosApplicationProxy.o: src/ApplicationProxy.cpp src/ApplicationProxy.h
	$(CPP) -c src/ApplicationProxy.cpp