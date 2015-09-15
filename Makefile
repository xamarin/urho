Android:
	make libmono-urho.so -f MakeAndroid ABI="armeabi" && make libmono-urho.so -f MakeAndroid ABI="armeabi-v7a" && make libmono-urho.so -f MakeAndroid ABI="x86"
	
Mac:
	make libmono-urho.dylib -f MakeMac
	
iOS:
	make fat-libmono-urho.dylib -f MakeiOS
	
Windows:
	make libUrho3D.a -f MakeWindows
	
All-Macos: Android Mac iOS

All-Windows: Android Windows

	