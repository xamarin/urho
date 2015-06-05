SOURCES =	\
	generated/Context.cs		\
	generated/Object.cs		\
	generated/ObjectFactory.cs	\
	generated/RefCount.cs		\
	generated/RefCounted.cs		\
	src/AttributeInfo.cs		\
	src/Object.cs			\
	src/RefCounted.cs		\
	src/StringHash.cs		\
	src/Stubs.cs

Urho.dll: $(SOURCES) Makefile
	mcs -unsafe -out:Urho.dll -debug $(SOURCES)
