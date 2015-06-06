URHO_DIR=/cvs/Urho3D

LIBS =   -framework AudioUnit -framework Carbon -framework Cocoa -framework CoreAudio \
	 -framework ForceFeedback -framework IOKit -framework OpenGL -framework CoreServices \
	 -L$(URHO_DIR)/lib -lUrho3D -ldl -lpthread -lpthread -lpthread

CPPFLAGS = -I$(URHO_DIR)/include -I.. -g

RUNTIME_SOURCES = \
	runtime.cpp		\
	../generated/binding.cpp

RUNTIME_OBJECTS = $(patsubst %.cpp,%.o,$(RUNTIME_SOURCES))

all: check-links libmono-urho.a

libmono-urho.a: $(RUNTIME_OBJECTS)
	ar cruv libmono-urho.a $(RUNTIME_OBJECTS)

check-links:
	if test ! -e $(URHO_DIR)/include/SDL; then (cd $(URHO_DIR)/include; ln -s ../Source/ThirdParty/SDL/include SDL); fi
	if test ! -e $(URHO_DIR)/include/Box2D; then (cd $(URHO_DIR)/include; ln -s ../Source/ThirdParty/Box2D/Box2D Box2D); fi
	if test ! -e $(URHO_DIR)/include/Bullet; then (cd $(URHO_DIR)/include; ln -s ../Source/ThirdParty/Bullet/src Bullet); fi
	if test ! -e $(URHO_DIR)/include/GLEW; then (cd $(URHO_DIR)/include; ln -s ../Source/ThirdParty/GLEW GLEW); fi
