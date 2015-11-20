all:
	@echo "Use make run to fill some of the standard documentation"

run:
	fsharpc --debug:full -r:System.Xml.Linq fill.fs && mono --debug fill.exe

.PHONY: Urho.xml

Urho.xml:
	rm Urho.xml
	mdoc export-msxdoc --out=Urho.xml .
	perl -pi -e 's/To be added.//' Urho.xml

update:
	rm Urho.xml
	mdoc update --delete -o . ../bindings/obj/Release/Urho.dll
	mdoc export-msxdoc --out=Urho.xml .
