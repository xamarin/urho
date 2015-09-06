#
ARCH=-arch i386 -arch x86_64
URHO_DIR=../Urho3D/Urho3D_Mac
URHO_FLAGS=-I$(URHO_DIR)/include -I$(URHO_DIR)/include/kNet -I$(URHO_DIR)/include/Urho3D/ThirdParty
URHO_LIBS=-L$(URHO_DIR)/lib -framework AudioUnit -framework Carbon -framework Cocoa -framework CoreAudio -framework ForceFeedback -framework IOKit -framework OpenGL -framework CoreServices -lUrho3D -ldl -lpthread 
CXXFLAGS=-g $(ARCH) -Wno-address-of-temporary -Wno-return-type-c-linkage -Wno-c++11-extensions $(URHO_FLAGS)
BIN_DIR = ../Bin/Desktop

# generates project and compiles it twice for both 32 and 64 bit. then uses lipo to merge into fat binary
FatLibUrho3d.a:
	cd ../Urho3D/Urho3D_Mac && ./compile_fat_static_lib.sh

Maclibmono-urho.dylib: 
	mkdir -p $(BIN_DIR)
Maclibmono-urho.dylib: FatLibUrho3d.a Macbinding.o Macglue.o Macevents.o MacApplicationProxy.o Macvector.o 
	c++  $(ARCH) -dynamiclib -g -o $(BIN_DIR)/libmono-urho.dylib -g $(URHO_LIBS) binding.o glue.o vector.o events.o ApplicationProxy.o

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