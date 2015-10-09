#download clang 3.7 from here: http://llvm.org/releases/download.html 
CUSTOM_CLANG=/tools/clang370/bin/clang

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
	
Windows:
	make -j1 libUrho3D.a -f MakeWindows
	
All-Macos: Android Mac iOS

All-Windows: Android Windows

#compile Urho.pch for SharpieBinder on Mac
PchMac:
	make -j1 Urho3D_Mac -f MakeMac && $(CUSTOM_CLANG) -cc1 -emit-pch -o bindings/Urho.pch bindings/all-urho.cpp  -IUrho3D/Urho3D_Mac/include -IUrho3D/Urho3D_Mac/include/Urho3D/ThirdParty

ParseEventsMac:
	cd bindings && perl parse.pl ../Urho3D/Urho3D_Mac/include/Urho3d/*/*h