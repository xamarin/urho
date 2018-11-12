#download clang 3.7 from here: http://llvm.org/releases/download.html 
#CUSTOM_CLANG=/tools/clang370/bin/clang
LOCAL_CLANG=clang+llvm-3.7.0-x86_64-apple-darwin/bin/clang
CUSTOM_CLANG=`pwd`/$(LOCAL_CLANG)
ifeq (, $(shell which brew))
# Running on Windows, let's not care.
else
  ifeq (, $(shell which mono64))
    # Use default mono installation.
    MONO64=mono64
    XBUILD=xbuild
  else
    MONO64=mono64
    XBUILD=xbuild
  endif
  NUGET=$(MONO64) ../Nuget/.nuget/Nuget.exe
endif

ifeq ($(OS),Windows_NT)
    OSNAME=Windows
else
    OSNAME=Mac
endif

.PHONY : SharpieBinder

Android_armeabi:
	make -j1 libmono-urho.so -f MakeAndroid ABI="armeabi"
Android_armeabi-v7a:
	make -j1 libmono-urho.so -f MakeAndroid ABI="armeabi-v7a"
Android_x86:
	make -j1 libmono-urho.so -f MakeAndroid ABI="x86"
Android_x86_64:
	make -j1 libmono-urho.so -f MakeAndroid ABI="x86_64"
Android_arm64-v8a:
	make -j1 libmono-urho.so -f MakeAndroid ABI="arm64-v8a"

# Make -j4 Android
Android: Android_x86_64 Android_x86 Android_armeabi Android_armeabi-v7a Android_arm64-v8a

macOS: Mac
Mac:
	make -j1 libmono-urho.dylib -f MakeMac

iOS:
	make -j1 Urho.framework -f MakeiOS

tvOS:
	make -j1 fat-libmono-urho.dylib -f MaketvOS

Windows: Windows32 Windows64
Windows32:
	make -j1 libUrho3D.a -f MakeWindows ARCH="Win32" RENDERER=OPENGL
Windows64:
	make -j1 libUrho3D.a -f MakeWindows ARCH="Win64" RENDERER=OPENGL

Windows_D3D11: Windows32_D3D11 Windows64_D3D11
Windows32_D3D11:
	make -j1 libUrho3D.a -f MakeWindows ARCH="Win32" RENDERER=D3D11
Windows64_D3D11:
	make -j1 libUrho3D.a -f MakeWindows ARCH="Win64" RENDERER=D3D11

UWP: UWP32 UWP64
UWP32:
	make -j1 libUrho3D.a -f MakeUWP ARCH=x86
UWP64:
	make -j1 libUrho3D.a -f MakeUWP ARCH=x64

SharpReality: SharpReality32 SharpReality64
SharpReality32:
	make -j1 libUrho3D.a -f MakeSharpReality ARCH=x86
SharpReality64:
	make -j1 libUrho3D.a -f MakeSharpReality ARCH=x64

HoloLens32: HoloLens
HoloLens64: HoloLens
HoloLens:
	@echo "HoloLens was renamed to SharpReality"

All-Macos: Android Mac iOS
All-Windows: Windows Windows_D3D11 UWP SharpReality

UpdateCoreDataPak:
	make -j1 CoreData.pak -f MakeWindows

Tools:
	make -j1 Tools -f Make$(OSNAME)

Linux:
	make -j1 libmono-urho.so -f MakeLinux

$(LOCAL_CLANG): 
	if test ! -e clang+llvm-3.7.0-x86_64-apple-darwin.tar.xz; then curl -O http://releases.llvm.org/3.7.0/clang+llvm-3.7.0-x86_64-apple-darwin.tar.xz; fi
	tar xzvf clang+llvm-3.7.0-x86_64-apple-darwin.tar.xz

#compile Urho.pch for SharpieBinder on Mac
PchMac: $(LOCAL_CLANG)
	if test ! -e /usr/include; then xcode-select --install; fi
	make -j1 Urho3D_Mac -f MakeMac && $(CUSTOM_CLANG) -cc1 -stdlib=libc++ -std=c++0x -emit-pch -DURHO3D_OPENGL -DURHO3D_CXX11=1 -o Bindings/Urho.pch Bindings/Native/all-urho.cpp  -IUrho3D/Urho3D_Mac/include -IUrho3D/Urho3D_Mac/include/Urho3D/ThirdParty

SharpieBinder: Bindings/Urho.pch
	cd SharpieBinder && $(NUGET) restore SharpieBinder.sln && $(XBUILD) SharpieBinder.csproj && cd bin && $(MONO64) SharpieBinder.exe

ParseEventsMac:
	@if test ! -d Bindings/Portable/Generated; then echo "Please generate the C# files using SharpieBinder or use 'make SharpieBinder'" && exit 1; fi
	cd Bindings && perl ParseEvents.pl ../Urho3D/Urho3D_Mac/include/Urho3d/*/*h

Generated: PchMac SharpieBinder ParseEventsMac
	
refresh-docs: Generated
	xbuild /target:clean bindings/Desktop/Urho.Desktop.csproj
	xbuild bindings/Desktop/Urho.Desktop.csproj
	(cd docs; make update)
