URHO_MAC_DIR=Urho3D/Urho3D_Mac
CUSTOM_CLANG=/cvs/clang370/clang

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

Pch: Mac
	$(CUSTOM_CLANG) -cc1 -emit-pch -o Urho.pch all-urho.cpp  -I$(URHO_DIR)/include -I$(URHO_DIR)/include/Urho3D/ThirdParty

ParseEvents:
	perl parse.pl $(URHO_DIR)/include/Urho3d/*/*h