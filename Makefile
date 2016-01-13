#download clang 3.7 from here: http://llvm.org/releases/download.html 
#CUSTOM_CLANG=/tools/clang370/bin/clang
LOCAL_CLANG=clang+llvm-3.7.0-x86_64-apple-darwin/bin/clang
CUSTOM_CLANG=`pwd`/$(LOCAL_CLANG)
ifeq (, $(shell which brew))
# Running on Windows, let's not care.
else
BREW_MONO_PREFIX=$(shell brew --prefix mono)
BREW_MONO=$(BREW_MONO_PREFIX)/bin/mono
BREW_XBUILD=$(BREW_MONO_PREFIX)/bin/xbuild
NUGET=$(BREW_MONO) ../Nuget/.nuget/Nuget.exe
endif

.PHONY : SharpieBinder

Android_armeabi:
	make -j1 libmono-urho.so -f MakeAndroid ABI="armeabi"
Android_armeabi-v7a:
	make -j1 libmono-urho.so -f MakeAndroid ABI="armeabi-v7a"
Android_x86:
	make -j1 libmono-urho.so -f MakeAndroid ABI="x86"

# Make -j3 Android
Android: Android_armeabi Android_armeabi-v7a Android_x86

Mac:
	make -j1 libmono-urho.dylib -f MakeMac

iOS:
	make -j1 fat-libmono-urho.dylib -f MakeiOS

tvOS:
	make -j1 fat-libmono-urho.dylib -f MaketvOS

Windows32:
	make -j1 libUrho3D.a -f MakeWindows ARCH="" && make -j1 CoreData.pak -f MakeWindows

Windows64:
	make -j1 libUrho3D.a -f MakeWindows && make -j1 CoreData.pak -f MakeWindows

All-Macos: Android Mac iOS

All-Windows: Android Windows64

$(LOCAL_CLANG): 
	if test ! -e clang+llvm-3.7.0-x86_64-apple-darwin.tar.xz; then curl -O http://llvm.org/releases/3.7.0/clang+llvm-3.7.0-x86_64-apple-darwin.tar.xz; fi
	tar xzvf clang+llvm-3.7.0-x86_64-apple-darwin.tar.xz

#compile Urho.pch for SharpieBinder on Mac
PchMac: $(LOCAL_CLANG)
	if test ! -e /usr/include; then xcode-select --install; fi
	make -j1 Urho3D_Mac -f MakeMac && $(CUSTOM_CLANG) -cc1 -emit-pch -o bindings/Urho.pch bindings/all-urho.cpp  -IUrho3D/Urho3D_Mac/include -IUrho3D/Urho3D_Mac/include/Urho3D/ThirdParty

SharpieBinder: bindings/Urho.pch
	cd SharpieBinder && $(NUGET) restore SharpieBinder.sln && $(BREW_XBUILD) SharpieBinder.csproj && cd bin && $(BREW_MONO) SharpieBinder.exe

ParseEventsMac:
	@if test ! -d bindings/generated; then echo "Please generate the C# files using SharpieBinder or use 'make SharpieBinder'" && exit 1; fi
	cd bindings && perl parse.pl ../Urho3D/Urho3D_Mac/include/Urho3d/*/*h

# change references from nuget to projectreferences for Samples/
RemoveNugetFromSamples:
	csc bindings/RemoveNugetFromSamples.cs && ./RemoveNugetFromSamples.exe -refsonly && rm -f RemoveNugetFromSamples.exe
	
# change references from nuget to projectreferences for Samples/
RemoveNugetFromSamplesMono:
	mcs bindings/RemoveNugetFromSamples.cs -r:System.Xml.dll -r:System.Xml.Linq.dll && mono RemoveNugetFromSamples.exe -refsonly && rm -f bindings/RemoveNugetFromSamples.exe