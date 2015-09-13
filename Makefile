Android:
	make libmono-urho.so -f MakeAndroid.make ABI="armeabi" && make libmono-urho.so -f MakeAndroid.make ABI="armeabi-v7a" && make libmono-urho.so -f MakeAndroid.make ABI="x86"
	
Mac:
	make libmono-urho.dylib -f MakeMac.make
	
iOS:
	make fat-libmono-urho.dylib -f MakeiOS.make
	
All: Android Mac iOS

	