
Findings
========

Struct vector3 can be passed by ref to an unmanaged method that will take that as a Vector3&

Binding
=======

Node.CreateCompoennt
--------------------

Currently using the ugly StringHash API, and since we can not call methods
through type interfaces, the challenge is then to find a way where we can 
quickly look this up, based on a type.

API
---
Produce default parameters

Object System
-------------

Need support most derived object creation in Runtime.LookupObject.

Bind
----
[ ] Support for the EngineParameters
[ ] Support for the command line arguments
[ ] Input/inputEvents constant definitions.
[ ] Color&

WorkItem issues
---------------

structs that subclass RefCounted should be bound as classes, see
WorkItem (which is currently surfaced as a struct)

Build
-----
PhysicsWorld and RigidBody are pending, since they surface some bt data types
that I do not currently bind.

Structures
----------

Should fix code generation for structures, they should likely use
"ref" to initialize themselves?

Enumerations
------------

They currently just dump the name, we need to dump the value

We should also likely dump the values as constants, and use the
constants inside real enumeration definitions (because trying to
prettify this automatically is a lot of work for little gain,
85 or so enumerations)

Generator
=========

Serializer, Deserializer
------------------------
They are only used in a couple of places as base classes, and the type
is in general not used to pass data around.   So we should inline the
methods from those classes in the classes that adopt them.

(File, MemoryBuffer, VectorBuffer)


Build Issues
============

Currently we hardcode /cvs/Urho3D as well as hardcoding URHO3D_OPENGL,
and the reality is that we should instead install Urho3D into a
prefix, and use pkg-config to fetch the flags for the install (so we
actually get the configured flags correctly, and we do not need to
hardcode URHO3D_OPENGL everywhere)


Optimizations
=============

Events
------

Object.cs/ObjectCallback: do we even need the stringHash in the event?

Seems like we do not, since we proxy everything.
