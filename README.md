# ![](http://developer.xamarin.com/guides/cross-platform/urho/introduction/Images/UrhoSharp_icon.png) UrhoSharp

UrhoSharp is a lightweight Game Engine suitable for using with C# and F# to create games
that run on Android, iOS, Mac, Windows and Unix. The game engine is available as a 
portable class library, allowing your game code to be written once and shared across 
all platforms. UrhoSharp is powered by [Urho3D](http://urho3d.github.io/), 
a game engine that has been under development for more than a decade. 
More information can be found in the [UrhoSharp documentation](http://developer.xamarin.com/guides/cross-platform/urho/introduction/).
The bindings for Urho3D are licensed under the MIT license, as
found on the LICENSE file.   

# Samples

Sample code lives in https://github.com/xamarin/urho-samples and
repository has them as a git submodule. Samples use UrhoSharp via nuget.

![Sample](https://github.com/xamarin/urho-samples/raw/master/SamplyGame/Screenshots/Video.gif) ![Sample](https://github.com/xamarin/urho-samples/raw/master/SamplyGame/Screenshots/Video.gif)

# Setup

* Available on NuGet: http://www.nuget.org/packages/UrhoSharp
* Install into your PCL project and Client projects.

# Quick start

To help developers get up and running quickly with UrhoSharp we are providing 
a [solution template](https://visualstudiogallery.msdn.microsoft.com/0851993e-16e9-417e-92f2-6bdb39308ed2) for Visual Studio (you can find it in "Online templates" tab). 
This template consists of PCL+Android+iOS+Mac/Windows with a simple scene and 
some assets (Xamarin Studio templates will be available soon):

![VS](https://habrastorage.org/files/f22/b49/ded/f22b49dedc264396a47015784bd9b35f.gif)

# How to build bindings

This is currently a little messy, so YMMV.

In order to compile binaries for all platforms you will need both
Windows and OS X environment.  Please follow these steps:

**1. Install:**

- XCode
- Xamarin Studio
- CMake (i.e. "brew install cmake")
- Mono 64 bit (i.e. "brew install mono")
- Command Line tools ("xcode-select --install")
- Android NDK (and ANDROID_NDK variable)

**2. Clone the repository including submodules**

```
git clone git@github.com:xamarin/urho.git --recursive
```

**3. Compile Urho.pch**

The following command will download Clang 3.7.0 if you do not have it
installed, and use this to scan the Urho header files:

```
make PchMac
```

**4. Generate C# bindings from Urho.pch**

Open SharpieBinder/SharpieBinder.sln via Xamarin Studio and change
.NET runtime to 64 bit mono (installed from homebrew is usually
located in "/usr/local/Cellar/4.x.x.x"). Run SharpieBinder project and
make sure it generated *.cs files in /bindings/generated dir.

Alternatively, you can do `make SharpieBinder`.

Then execute:

```
make ParseEventsMac
```

it should generate bindings/generated/events.cpp file

**5. Compile UrhoSharp for Mac (fat dylib)**
```
make Mac
```
it takes 5-10 minutes.

**6. Compile UrhoSharp for iOS (fat dylib: i386, armv7, arm64)**
```
make iOS
```
Note: Make sure you have an iOS 9.0 simulator target or modify [SDKVER](https://github.com/xamarin/urho/blob/master/MakeiOS#L3) to target another simulator.

**7. Compile UrhoSharp for Android (armeabi, armeabi-v7a, x86)** 
```
make -j3 Android
```
-j3 means a job per ABI. Make sure you have installed Android SDK and NDK (see MakeAndroid file)

**8. Compile UrhoSharp for Windows (64 bit)**

Obviously you can't do it on OS X so you have to switch to Windows environment. Make sure you have installed:
- Visual Studio 2015
- CMake
- GNU make (e.g. cygwin)

SharpieBinder doesn't work on Windows yet so you will have to copy
bindings/generated folder from OS X environment to Windows.

Execute:
```
make Windows64  (or Windows32)
```

(you can also compile Android on Windows via *"make Android"*)
Then, open Urho.sln and compile MonoUrho.Windows project in Release configuration.

All compiled binaries could be found in the Bin/{platform} folder.

