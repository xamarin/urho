open CS,">generated/Object.Events.cs" || die;
open CPP,">generated/events.cpp" || die;
print CS "using System;\n";
print CS "using System.Runtime.InteropServices;\n";
print CS "namespace Urho {\n\n";
print CPP "#define URHO3D_OPENGL\n";
print CPP "#include \"../AllUrho.h\"\n";
print CPP "#include \"../src/glue.h\"\n";
print CPP "extern \"C\" {\n";

sub mapType {
    my ($pt) = @_;
    ($t,$st) = split (/ /, $pt);
    if ($st eq "pointer"){
	return "$t";
    }
    if ($st eq "ptr"){
	return "$t";
    }

    return "uint" if $t eq "unsigned";
    return "float" if $t eq "Float";
    return "IntPtr" if $t eq "User-defined";
    return $t;
}

RESTART:
while (<>){
    chop;
    next if (/#define/);
    if (/EVENT\(/){
	($ec,$en) = $_ =~ /EVENT\((\w+), ?(\w+)/;
	print CS "    public struct EventArgs$en {\n";
	print CS "        internal IntPtr handle;\n";
	print CPP "void urho_subscribe_$en (void *_receiver, HandlerFunctionPtr callback, void *data)\n";
	print CPP "{\n";
	print CPP "\tUrho3D::Object *receiver = (Urho3D::Object *) _receiver;\n";
	print CPP "\treceiver->SubscribeToEvent (Urho3D::$ec, new NotificationProxy (receiver, callback, data));\n";
	print CPP "}\n\n";
	while (<>){
	    chop;
	    if (/PARAM/){
		($pc,$pn,$pt) = $_ =~ /PARAM\((\w+), ?(\w+).*\/\/\W*(\w+(\W+\w+)?)/;
		$cspt = &mapType ($pt);
		$hashgetters{$pc} = $en;

		print CS "        public $cspt $pn => UrhoMap.get_$cspt (handle, UrhoHash.$pc);\n";
	    }
	    if (/}/){
		print CS "    }\n\n";
		print CS "    public partial class Object {\n"; 
		print CS "         [DllImport(\"mono-urho\")]\n";
		print CS "         extern static void urho_subscribe_$en (IntPtr target, Action<IntPtr> act, IntPtr data);\n";
                print CS "         public void SubscribeTo$en (Action<EventArgs$en> handler)\n";
		print CS "         {\n";
		print CS "              Action<Intptr> proxy = (x)=> { var d = new EventArgs$en () { handle = x }; handler (d); }\n";
	        print CS "              GCHandle gch = GCHandle.Alloc (proxy);\n";
		print CS "              urho_subscribe_$en (handle, ObjectCallback, GCHandle.ToIntPtr (gch));\n";
		print CS "         }\n";
                print CS "    }\n\n";
	    }
	    next RESTART if (/}/);
	}
    }
}
print CS "    }\n";
print CPP "// Hash Getters\n";
print CS "// Hash Getters\n";
print CS "internal class UrhoHash {\n";
foreach $pc (keys %hashgetters){
    $en = $hashgetters{$pc};
    print CPP "int urho_hash_get_$pc ()\n{\n";
    print CPP "\treturn Urho3D::$en::$pc.Value ();\n}\n\n";
    print CS "        [DllImport(\"mono-urho\")]\n";
    print CS "        extern static int urho_hash_get_$pc ();\n";
    print CS "        static int _$pc;\n";
    print CS "        static int $pc { get { if (_$pc == 0){ _$pc = urho_hash_get_$pc (); } return _$pc; }};\n\n";
}

print CPP "}\n";
print CS "    }\n}";
close CS;
close CPP;
