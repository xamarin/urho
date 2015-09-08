ARCH=-arch i386 -arch x86_64
URHO3D_SRC_DIR=../Urho3D/Source
URHO3D_MAC_DIR=../Urho3D/Urho3D_Mac
OUTPUT_DIR = ../Bin/Desktop

URHO_FLAGS=-I$(URHO3D_MAC_DIR)/include -I$(URHO3D_MAC_DIR)/include/kNet -I$(URHO3D_MAC_DIR)/include/Urho3D/ThirdParty
URHO_LIBS=-L$(URHO3D_MAC_DIR)/lib -framework AudioUnit -framework Carbon -framework Cocoa -framework CoreAudio -framework ForceFeedback -framework IOKit -framework OpenGL -framework CoreServices -lUrho3D -ldl -lpthread 
CXXFLAGS=-g $(ARCH) -Wno-address-of-temporary -Wno-return-type-c-linkage -Wno-c++11-extensions $(URHO_FLAGS)

Urho3D_Mac:
	$(URHO3D_SRC_DIR)/./cmake_macosx.sh $(URHO3D_MAC_DIR)

libUrho3D_32.a: Urho3D_Mac
	cd $(URHO3D_MAC_DIR) && xcodebuild ARCHS=i386 ONLY_ACTIVE_ARCH=NO -target Urho3D -configuration Release && mv lib/libUrho3D.a lib/libUrho3D_32.a

libUrho3D_64.a: Urho3D_Mac
	cd $(URHO3D_MAC_DIR) && xcodebuild ARCHS=x86_64 ONLY_ACTIVE_ARCH=NO -target Urho3D -configuration Release && mv lib/libUrho3D.a lib/libUrho3D_64.a

libUrho3D_Fat.a: libUrho3D_32.a libUrho3D_64.a
cd $(URHO3D_MAC_DIR) && lipo -create lib/32libUrho3D.a lib/64libUrho3D.a -output lib/libUrho3D.a

Maclibmono-urho.dylib: libUrho3D_Fat.a Macbinding.o Macglue.o Macevents.o MacApplicationProxy.o Macvector.o 
	mkdir -p $(OUTPUT_DIR) && c++  $(ARCH) -dynamiclib -g -o $(OUTPUT_DIR)/libmono-urho.dylib -g $(URHO_LIBS) binding.o glue.o vector.o events.o ApplicationProxy.o

Macbinding.o: AllUrho.h generated/binding.cpp
	c++ -g -c $(X) $(CXXFLAGS) generated/binding.cpp 

Macglue.o: src/glue.cpp src/glue.h 
	c++ -c $(CXXFLAGS) src/glue.cpp 

Macvector.o: src/vector.cpp src/glue.h 
	c++ -c $(CXXFLAGS) src/vector.cpp 

Macevents.o: generated/events.cpp
	c++ -c $(CXXFLAGS) generated/events.cpp $(URHO_FLAGS) 

MacApplicationProxy.o: src/ApplicationProxy.cpp src/ApplicationProxy.h
	c++ -c $(CXXFLAGS) src/ApplicationProxy.cpp $(URHO_FLAGS)