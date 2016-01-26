# UrhoSharp

C# Bindings to the [Urho3D](http://urho3d.github.io/) engine.

The bindings/ for UrhoSharp are licensed under the MIT license, as
found on the LICENSE file.   

# Sample

Sample code lives in https://github.com/xamarin/urho-samples and
repository has them as a git submodule. Samples use Urho via nuget.

![Very simple sample](https://hsto.org/files/ec1/1c8/d0c/ec11c8d0c4494048bc614e3166df4f3b.png)

Some screencasts:

* http://screencast.com/t/EmFj3O0K8 
* http://screencast.com/t/Xh8G4StiABY

# How to build

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

