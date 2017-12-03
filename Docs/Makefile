all:
	@echo "Use make run to fill some of the standard documentation"

run:
	fsharpc --debug:full -r:System.Xml.Linq fill.fs && mono --debug fill.exe

update:
	-rm Urho.xml
	#mdoc update --lib=/Library/Frameworks/Xamarin.iOS.framework/Versions/Current/lib/mono/Xamarin.iOS/ --delete -o . --type=Urho.iOS.UrhoEngine --type=Urho.iOS.UrhoSurface ../bindings/iOS/bin/iPhone/Debug/Urho.iOS.dll
	#mdoc update --delete -o . --type=Urho.Droid.UrhoEngine --type=Urho.Droid.UrhoSurface ../bindings/Android/bin/Debug/Urho.Droid.dll
	mdoc update --delete -o . ../Bin/Desktop/Urho.dll
	#mdoc update --delete -o . --type=Urho.UrhoEngine ../bindings/Desktop/bin/Debug/Urho.Desktop.dll
	mdoc export-msxdoc --out=Urho.xml .
	perl -pi -e 's/To be added.//' Urho.xml
	
assemble:
	mdoc assemble . --out=Urho
