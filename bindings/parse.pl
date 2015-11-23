# These contains the pre-mapped events
    
$events{"ReloadStarted"} = "Urho.Resources"; 
$events{"ReloadFinished"} = "Urho.Resources"; 
$events{"ReloadFailed"} = "Urho.Resources"; 
$events{"FileChanged"} = "Urho.Resources"; 
$events{"LoadFailed"} = "Urho.Resources"; 
$events{"ResourceNotFound"} = "Urho.Resources"; 
$events{"UnknownResourceType"} = "Urho.Resources"; 
$events{"ResourceBackgroundLoaded"} = "Urho.Resources"; 
$events{"ChangeLanguage"} = "Urho.Resources"; 

$events{"LogMessage"} = "Urho.IO"; 
$events{"AsyncExecFinished"} = "Urho.IO"; 

$events{"UIMouseClick"} = "Urho.UI"; 
$events{"UIMouseClickEnd"} = "Urho.UI"; 
$events{"UIMouseDoubleClick"} = "Urho.UI"; 
$events{"DragDropTest"} = "Urho.UI"; 
$events{"DragDropFinish"} = "Urho.UI"; 
$events{"FocusChanged"} = "Urho.UI"; 
$events{"NameChanged"} = "Urho.UI"; 
$events{"Resized"} = "Urho.UI"; 
$events{"Positioned"} = "Urho.UI"; 
$events{"VisibleChanged"} = "Urho.UI"; 
$events{"Focused"} = "Urho.UI"; 
$events{"Defocused"} = "Urho.UI"; 
$events{"LayoutUpdated"} = "Urho.UI"; 
$events{"Pressed"} = "Urho.UI"; 
$events{"Released"} = "Urho.UI"; 
$events{"Toggled"} = "Urho.UI"; 
$events{"SliderChanged"} = "Urho.UI"; 
$events{"SliderPaged"} = "Urho.UI"; 
$events{"ScrollBarChanged"} = "Urho.UI"; 
$events{"ViewChanged"} = "Urho.UI"; 
$events{"ModalChanged"} = "Urho.UI"; 
$events{"CharEntry"} = "Urho.UI"; 
$events{"TextChanged"} = "Urho.UI"; 
$events{"TextFinished"} = "Urho.UI"; 
$events{"MenuSelected"} = "Urho.UI"; 
$events{"ItemSelected"} = "Urho.UI"; 
$events{"ItemDeselected"} = "Urho.UI"; 
$events{"SelectionChanged"} = "Urho.UI"; 
$events{"ItemClicked"} = "Urho.UI"; 
$events{"ItemDoubleClicked"} = "Urho.UI"; 
$events{"UnhandledKey"} = "Urho.UI"; 
$events{"FileSelected"} = "Urho.UI"; 
$events{"MessageACK"} = "Urho.UI"; 
$events{"ElementAdded"} = "Urho.UI"; 
$events{"ElementRemoved"} = "Urho.UI"; 
$events{"HoverBegin"} = "Urho.UI"; 
$events{"HoverEnd"} = "Urho.UI"; 
$events{"DragBegin"} = "Urho.UI"; 
$events{"DragMove"} = "Urho.UI"; 
$events{"DragEnd"} = "Urho.UI"; 
$events{"DragCancel"} = "Urho.UI"; 
$events{"UIDropFile"} = "Urho.UI"; 

$events{"PhysicsPreStep2D"} = "Urho.Urho2D"; 
$events{"PhysicsPostStep2D"} = "Urho.Urho2D"; 
$events{"PhysicsBeginContact2D"} = "Urho.Urho2D"; 
$events{"PhysicsEndContact2D"} = "Urho.Urho2D"; 

$events{"PhysicsPreStep"} = "Urho.Physics"; 
$events{"PhysicsPostStep"} = "Urho.Physics"; 
$events{"PhysicsCollisionStart"} = "Urho.Physics"; 
$events{"PhysicsCollision"} = "Urho.Physics"; 
$events{"PhysicsCollisionEnd"} = "Urho.Physics"; 

$events{"NavigationMeshRebuilt"} = "Urho.Navigation"; 
$events{"NavigationAreaRebuilt"} = "Urho.Navigation"; 
$events{"CrowdAgentFormation"} = "Urho.Navigation"; 
$events{"CrowdAgentReposition"} = "Urho.Navigation"; 
$events{"CrowdAgentFailure"} = "Urho.Navigation"; 
$events{"CrowdAgentStateChanged"} = "Urho.Navigation"; 
$events{"NavigationObstacleAdded"} = "Urho.Navigation"; 
$events{"NavigationObstacleRemoved"} = "Urho.Navigation"; 

$events{"ServerConnected"} = "Urho.Network"; 
$events{"ServerDisconnected"} = "Urho.Network"; 
$events{"ConnectFailed"} = "Urho.Network"; 
$events{"ClientConnected"} = "Urho.Network"; 
$events{"ClientDisconnected"} = "Urho.Network"; 
$events{"ClientIdentity"} = "Urho.Network"; 
$events{"ClientSceneLoaded"} = "Urho.Network"; 
$events{"NetworkMessage"} = "Urho.Network"; 
$events{"NetworkUpdate"} = "Urho.Network"; 
$events{"NetworkUpdateSent"} = "Urho.Network"; 
$events{"NetworkSceneLoadFailed"} = "Urho.Network"; 
$events{"RemoteEventData"} = "Urho.Network"; 


open CS,">generated/Object.Events.cs" || die;
open CPP,">generated/events.cpp" || die;
print CS "using System;\n";
print CS "using System.Runtime.InteropServices;\n";
print CS "using Urho.Physics;\n";
print CS "using Urho.Navigation;\n";
print CS "using Urho.Network;\n";
print CS "using Urho.Urho2D;\n";
print CS "using Urho.UI;\n";
print CS "using Urho.Actions;\n";
print CS "using Urho.Audio;\n";
print CS "using Urho.Resources;\n";
print CS "using Urho.IO;\n";

print CS "namespace Urho {\n\n";
print CS "\tpublic delegate void ObjectCallbackSignature (IntPtr data, int stringhash, IntPtr variantMap);\n";
print CS "}\n\n";
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
$hasNamespace = 0;

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
	    
	    if (exists $events{$en}){
		print CS "namespace ${events{$en}} {\n"
	    } else {
		print CS "namespace Urho {\n";
	    }
	    
	    print CS "        public partial struct ${eventName}EventArgs {\n";
	    print CS "            internal IntPtr handle;\n";
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
		    
		    print CS "            public $cspt $pn =>$cast UrhoMap.get_$plain (handle, UrhoHash.$pc);\n";
		}
		if (/}/){
		    print CS "        } /* struct ${eventName}EventArgs */\n\n";

		    for $type (split /,/,$events{$ec}){

			print CS "        public partial class $type {\n"; 
			print CS "             ObjectCallbackSignature callback${eventName};\n";
			print CS "             [DllImport(\"mono-urho\", CallingConvention=CallingConvention.Cdecl)]\n";
			print CS "             extern static IntPtr urho_subscribe_$eventName (IntPtr target, ObjectCallbackSignature act, IntPtr data);\n";
			print CS "             @{[$en eq 'Update' ? 'internal' : 'public']} Subscription SubscribeTo${eventName} (Action<${eventName}EventArgs> handler)\n";
			print CS "             {\n";
			print CS "                  Action<IntPtr> proxy = (x)=> { var d = new ${eventName}EventArgs () { handle = x }; handler (d); };\n";
			print CS "                  var s = new Subscription (proxy);\n";
			print CS "                  callback${eventName} = ObjectCallback;\n";
			print CS "                  s.UnmanagedProxy = urho_subscribe_$eventName (handle, callback${eventName}, GCHandle.ToIntPtr (s.gch));\n";
			print CS "                  return s;\n";
			print CS "             }\n\n";
			print CS "             static UrhoEventAdapter<${eventName}EventArgs> eventAdapterFor${eventName};\n";
			print CS "             public event Action<${eventName}EventArgs> ${eventName}\n";
			print CS "             {\n";
			print CS "                 add\n";
			print CS "                 {\n";
			print CS "                      if (eventAdapterFor${eventName} == null)\n";
			print CS "                          eventAdapterFor${eventName} = new UrhoEventAdapter<${eventName}EventArgs>();\n";
			print CS "                      eventAdapterFor${eventName}.AddManagedSubscriber(handle, value, SubscribeTo${eventName});\n";
			print CS "                 }\n";
			print CS "                 remove { eventAdapterFor${eventName}.RemoveManagedSubscriber(handle, value); }\n";
			print CS "             }\n";
			print CS "        } /* class $type */ \n\n";
		    }
		}
		if (/}/){
		    print CS "} /* namespace */\n\n";
		}			
		next RESTART if (/}/);
	    }
	}
    }
}
print CPP "// Hash Getters\n";
print CS "// Hash Getters\n";
print CS "namespace Urho {";
print CS "    internal class UrhoHash {\n";
foreach $pc (keys %hashgetters){
    $en = $hashgetters{$pc};
    print CPP "DllExport int urho_hash_get_$pc ()\n{\n";
    print CPP "\treturn Urho3D::$en::$pc.Value ();\n}\n\n";
    print CS "            [DllImport(\"mono-urho\", CallingConvention=CallingConvention.Cdecl)]\n";
    print CS "            extern static int urho_hash_get_$pc ();\n";
    print CS "            static int _$pc;\n";
    print CS "            internal static int $pc { get { if (_$pc == 0){ _$pc = urho_hash_get_$pc (); } return _$pc; }}\n\n";
}
print CPP "}\n";
print CS "        }\n    }";
close CS;
close CPP;
