open CS,">generated/Object.Events.cs" || die;
open CPP,">generated/events.cpp" || die;
print CS "using System;\n";
print CS "using System.Runtime.InteropServices;\n";
print CS "namespace Urho {\n\n";
print CS "\tpublic delegate void ObjectCallbackSignature (IntPtr data, int stringhash, IntPtr variantMap);\n";
print CPP "#define URHO3D_OPENGL\n";
print CPP "#include \"../AllUrho.h\"\n";
print CPP "#include \"../src/glue.h\"\n";
print CPP "extern \"C\" {\n";
print CPP "void urho_unsubscribe (NotificationProxy *proxy);\n";

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
    return "CollisionData []" if $pt eq "Buffer containing";
    return "byte []" if $t eq "Buffer";
    return $t;
}

open EVENTS, "events.txt" || die "Need the file events.txt to figure out where to generate events";
while (<EVENTS>){
    ($event, $classes) = split;
    @events{$event} = $classes;
}

RESTART:
while (<>){
    chop;
    next if (/#define/);
    if (/EVENT\(/){
	($ec,$en) = $_ =~ /EVENT\((\w+), ?(\w+)/;
	if ($en ne "DbCursor"){
	    $eventName = $en;
	    $eventName = "MouseMoved" if ($en eq "MouseMove");
	    $eventName = "FrameStarted" if ($en eq "BeginFrame");
	    $eventName = "FrameEnded" if ($en eq "EndFrame");
	    
	    print CS "    public partial struct ${eventName}EventArgs {\n";
	    print CS "        internal IntPtr handle;\n";
	    print CPP "DllExport void *urho_subscribe_$eventName (void *_receiver, HandlerFunctionPtr callback, void *data)\n";
	    print CPP "{\n";
	    print CPP "\tUrho3D::Object *receiver = (Urho3D::Object *) _receiver;\n";
	    print CPP "\tNotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::$ec);\n";
	    print CPP "\treceiver->SubscribeToEvent (receiver, Urho3D::$ec, proxy);\n";
	    print CPP "\treturn proxy;\n";
	    print CPP "}\n\n";

	    while (<>){
		chop;
		$cast = "";
		if (/PARAM/){
		    ($pc,$pn,$pt) = $_ =~ /PARAM\((\w+), ?(\w+).*\/\/\W*(\w+(\W+\w+)?)/;
		    $cspt = $plain = &mapType ($pt);
		    if (/P_KEY.*Key/){
			$cspt = "Key";
			$cast = "(Key)";
		    }
		    $plain =~ s/ .*//;
		    if ($plain eq "byte"){
			$plain = "Buffer";
		    }
		    $hashgetters{$pc} = $en;
		    
		    print CS "        public $cspt $pn =>$cast UrhoMap.get_$plain (handle, UrhoHash.$pc);\n";
		}
		if (/}/){
		    print CS "    }\n\n";

		    for $type (split /,/,$events{$ec}){

			print CS "    public partial class $type {\n"; 
			print CS "         ObjectCallbackSignature callback${eventName};\n";
			print CS "         [DllImport(\"mono-urho\", CallingConvention=CallingConvention.Cdecl)]\n";
			print CS "         extern static IntPtr urho_subscribe_$eventName (IntPtr target, ObjectCallbackSignature act, IntPtr data);\n";
			print CS "         @{[$en eq 'Update' ? 'internal' : 'public']} Subscription SubscribeTo${eventName} (Action<${eventName}EventArgs> handler)\n";
			print CS "         {\n";
			print CS "              Action<IntPtr> proxy = (x)=> { var d = new ${eventName}EventArgs () { handle = x }; handler (d); };\n";
			print CS "              var s = new Subscription (proxy);\n";
			print CS "              callback${eventName} = ObjectCallback;\n";
			print CS "              s.UnmanagedProxy = urho_subscribe_$eventName (handle, callback${eventName}, GCHandle.ToIntPtr (s.gch));\n";
			print CS "              return s;\n";
			print CS "         }\n\n";
			print CS "         static UrhoEventAdapter<${eventName}EventArgs> eventAdapterFor${eventName};\n";
			print CS "         public event Action<${eventName}EventArgs> ${eventName}\n";
			print CS "         {\n";
			print CS "             add\n";
			print CS "             {\n";
			print CS "                  if (eventAdapterFor${eventName} == null)\n";
			print CS "                      eventAdapterFor${eventName} = new UrhoEventAdapter<${eventName}EventArgs>();\n";
			print CS "                  eventAdapterFor${eventName}.AddManagedSubscriber(handle, value, SubscribeTo${eventName});\n";
			print CS "             }\n";
			print CS "             remove { eventAdapterFor${eventName}.RemoveManagedSubscriber(handle, value); }\n";
			print CS "         }\n";
			print CS "    }\n\n";
		    }
		}
		next RESTART if (/}/);
	    }
	}
    }
}
print CPP "// Hash Getters\n";
print CS "// Hash Getters\n";
print CS "internal class UrhoHash {\n";
foreach $pc (keys %hashgetters){
    $en = $hashgetters{$pc};
    print CPP "DllExport int urho_hash_get_$pc ()\n{\n";
    print CPP "\treturn Urho3D::$en::$pc.Value ();\n}\n\n";
    print CS "        [DllImport(\"mono-urho\", CallingConvention=CallingConvention.Cdecl)]\n";
    print CS "        extern static int urho_hash_get_$pc ();\n";
    print CS "        static int _$pc;\n";
    print CS "        internal static int $pc { get { if (_$pc == 0){ _$pc = urho_hash_get_$pc (); } return _$pc; }}\n\n";
}

print CPP "}\n";
print CS "    }\n}";
close CS;
close CPP;
