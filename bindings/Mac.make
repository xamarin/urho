URHO_DIR=../Submodules/Urho3D_Mac
URHO_FLAGS=-I$(URHO_DIR)/include -I$(URHO_DIR)/include/kNet  -I$(URHO_DIR)/include/Urho3D/ThirdParty 
URHO_LIBS=-L$(URHO_DIR)/lib -framework AudioUnit -framework Carbon -framework Cocoa -framework CoreAudio -framework ForceFeedback -framework IOKit -framework OpenGL -framework CoreServices -lUrho3D -ldl -lpthread 
NO_CLS_WARNINGS=-nowarn:3021

CXXFLAGS=-g -Wno-address-of-temporary -Wno-return-type-c-linkage -Wno-c++11-extensions $(URHO_FLAGS)

libmono-urho.dylib: binding.o glue.o events.o ApplicationProxy.o vector.o Makefile 
	c++ -dynamiclib -g -o libmono-urho.dylib -g $(URHO_LIBS) binding.o glue.o vector.o events.o ApplicationProxy.o

binding.o: AllUrho.h generated/binding.cpp Makefile
	c++ -g -c $(X) $(CXXFLAGS) generated/binding.cpp 

glue.o: src/glue.cpp src/glue.h Makefile 
	c++ -c $(CXXFLAGS) src/glue.cpp 

vector.o: src/vector.cpp src/glue.h Makefile 
	c++ -c $(CXXFLAGS) src/vector.cpp 

events.o: generated/events.cpp Makefile parse.pl
	c++ -c generated/events.cpp $(URHO_FLAGS) 

ApplicationProxy.o: src/ApplicationProxy.cpp src/ApplicationProxy.h Makefile
	c++ -c src/ApplicationProxy.cpp $(URHO_FLAGS)