
The stubs were created by first generating the XML documentation from the source files, 
and then compiling a special Urho.dll that is a regular binary, not a PCL (as the
Monodoc tools do not seem to run with PCL references).

The result was imported, and after this point, it is unlikely that it would be wise
to update from the C++ sources.

# Auto-documenting features

Easing functions
BaseType - StringHash
Type - Stringhash
Typename - string
Typenamestatic -string
TaskT returns
IntPtr constructors


