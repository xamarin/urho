Requirements
============

* ObjectiveSharpie built and installed
* 64-bit Mono
* libclang-mono.dylib installed in /usr/lib (from ObjectiveSharpie)
* PCH file generated from parsing all Urho3D header files

When executed, currently dumps the C# bindigns in /tmp/ra

Design Binding Notes
====================

The API in Urho3D is luckily not designed to be used as an object
oriented toolbox, so it is rare the case where users must subclass.
This has the advantage for a binding that there is no need to support
the scenario where subclasses in C# must be able to override methods
in the system.

Consideration: should we allow subclassing of the Urho classes?  If we
do, then we need to have a "Runtime.GetObejct"-like system to ensure
that given an unmanaged pointer, we always return the same instance to
managed code.

If not, we could just make all wrappers be dumb wrappers and override
the Equals, operator == and operator != methods.

Overview of SharpieBinder
=========================

The Urho binder in this directory works by using the precompiled
header file for all of Urho's API and producing C# bindings that
P/Invoke into a glue library written in C, which in turn calls into
the C++ API.

This uses the ObjectieSharpie binding to Clang which surfaces the
information, and the Sharpie's binder library, which provides a
mechanism to walk the AST.

The program creates two AST walkers, one that does a first pass to
collect information like core types and pairs of getters and setters,
and a second step that redoes the work and produces an NRefactory set
of AST nodes that eventually generate C# sources and some manual print
commands to generate the C binding.
