# urho
Code to integrate with the Urho3D engine

This is designed to be a binding to the C++ API of the Urho3D engine.

For information on the binding strategy, see the document at:

https://docs.google.com/document/d/1uuPwkmnGWdlhRe0-8VqLVtYyo1Hv1cgl_ZxmjwxMiFA/edit#heading=h.ozhfa8u99ynm

# Requirements

You will need a checkout of Urho from http://github.com/urho3d/Urho3D so far I have tested the bindings
using this version of Urho3D: ae59d55580415227318b5c807de5bdd55b8ddcde

The binding generator needs a 64-bit Mono to run, so this means that if you want to debug/work on it
from Xamarin Studio, you need to choose from Xamarin Studio's Preferences a Mono installation that
has been built with 64 bit support (in .NET Runtimes).

# Sample

Sample code lives in bindings/sample.cs and it is built by the Makefile, it
currently shows a couple of intereseting samples from Urho and what has so 
far been bound.
