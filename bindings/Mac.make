#
#
# This really should use an installed Urho
# and use `pkg-config --libs Urho3D`
#
ARCH=-arch i386 -arch x86_64
URHO_DIR=../Urho3D/Urho3D_Mac
URHO_FLAGS=-I$(URHO_DIR)/include -I$(URHO_DIR)/include/kNet -I$(URHO_DIR)/include/Urho3D/ThirdParty
URHO_LIBS=-L$(URHO_DIR)/lib -framework AudioUnit -framework Carbon -framework Cocoa -framework CoreAudio -framework ForceFeedback -framework IOKit -framework OpenGL -framework CoreServices -lUrho3D -ldl -lpthread 
CXXFLAGS=-g $(ARCH) -Wno-address-of-temporary -Wno-return-type-c-linkage -Wno-c++11-extensions $(URHO_FLAGS)
BIN_DIR = ../Bin/Desktop

FatLibUrho3d.a:
	../Urho3D/Urho3D_Mac/./compile_fat_static_lib.sh

Mlibmono-urho.dylib: 
	mkdir -p $(BIN_DIR)
Mlibmono-urho.dylib: FatLibUrho3d.a Mbinding.o Mglue.o Mevents.o MApplicationProxy.o Mvector.o 
	c++  $(ARCH) -dynamiclib -g -o $(BIN_DIR)/libmono-urho.dylib -g $(URHO_LIBS) binding.o glue.o vector.o events.o ApplicationProxy.o

Mbinding.o: AllUrho.h generated/binding.cpp
	c++ -g -c $(X) $(CXXFLAGS) generated/binding.cpp 

Mglue.o: src/glue.cpp src/glue.h 
	c++ -c $(CXXFLAGS) src/glue.cpp 

Mvector.o: src/vector.cpp src/glue.h 
	c++ -c $(CXXFLAGS) src/vector.cpp 

Mevents.o: generated/events.cpp
	c++ -c $(CXXFLAGS) generated/events.cpp $(URHO_FLAGS) 

MApplicationProxy.o: src/ApplicationProxy.cpp src/ApplicationProxy.h
	c++ -c $(CXXFLAGS) src/ApplicationProxy.cpp $(URHO_FLAGS)