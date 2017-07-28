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

$events{"UIMouseClick"} = "Urho.Gui"; 
$events{"UIMouseClickEnd"} = "Urho.Gui"; 
$events{"UIMouseDoubleClick"} = "Urho.Gui"; 
$events{"DragDropTest"} = "Urho.Gui"; 
$events{"DragDropFinish"} = "Urho.Gui"; 
$events{"FocusChanged"} = "Urho.Gui"; 
$events{"NameChanged"} = "Urho.Gui"; 
$events{"Resized"} = "Urho.Gui"; 
$events{"Positioned"} = "Urho.Gui"; 
$events{"VisibleChanged"} = "Urho.Gui"; 
$events{"Focused"} = "Urho.Gui"; 
$events{"Defocused"} = "Urho.Gui"; 
$events{"LayoutUpdated"} = "Urho.Gui"; 
$events{"Pressed"} = "Urho.Gui"; 
$events{"Released"} = "Urho.Gui"; 
$events{"Toggled"} = "Urho.Gui"; 
$events{"SliderChanged"} = "Urho.Gui"; 
$events{"SliderPaged"} = "Urho.Gui"; 
$events{"ScrollBarChanged"} = "Urho.Gui"; 
$events{"ViewChanged"} = "Urho.Gui"; 
$events{"ModalChanged"} = "Urho.Gui"; 
$events{"CharEntry"} = "Urho.Gui"; 
$events{"TextChanged"} = "Urho.Gui"; 
$events{"TextFinished"} = "Urho.Gui"; 
$events{"MenuSelected"} = "Urho.Gui"; 
$events{"ItemSelected"} = "Urho.Gui"; 
$events{"ItemDeselected"} = "Urho.Gui"; 
$events{"SelectionChanged"} = "Urho.Gui"; 
$events{"ItemClicked"} = "Urho.Gui"; 
$events{"ItemDoubleClicked"} = "Urho.Gui"; 
$events{"UnhandledKey"} = "Urho.Gui"; 
$events{"FileSelected"} = "Urho.Gui"; 
$events{"MessageACK"} = "Urho.Gui"; 
$events{"ElementAdded"} = "Urho.Gui"; 
$events{"ElementRemoved"} = "Urho.Gui"; 
$events{"HoverBegin"} = "Urho.Gui"; 
$events{"HoverEnd"} = "Urho.Gui"; 
$events{"DragBegin"} = "Urho.Gui"; 
$events{"DragMove"} = "Urho.Gui"; 
$events{"DragEnd"} = "Urho.Gui"; 
$events{"DragCancel"} = "Urho.Gui"; 
$events{"UIDropFile"} = "Urho.Gui"; 
$events{"TextEntry"} = "Urho.Gui"; 

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


open CS,">Portable/Generated/Object.Events.cs" || die;
open CPP,">Portable/Generated/events.cpp" || die;
print CS "using System;\n";
print CS "using System.Runtime.InteropServices;\n";
print CS "using Urho.Physics;\n";
print CS "using Urho.Navigation;\n";
print CS "using Urho.Network;\n";
print CS "using Urho.Urho2D;\n";
print CS "using Urho.Gui;\n";
print CS "using Urho.Actions;\n";
print CS "using Urho.Audio;\n";
print CS "using Urho.Resources;\n";
print CS "using Urho.IO;\n";

print CS "#pragma warning disable CS0618, CS0649\n";
print CS "namespace Urho {\n\n";
print CS "\t[UnmanagedFunctionPointer(CallingConvention.Cdecl)]";
print CS "\tpublic delegate void ObjectCallbackSignature (IntPtr data, int stringhash, IntPtr variantMap);\n";
print CS "}\n\n";
print CPP "#include \"../../Native/AllUrho.h\"\n";
print CPP "#include \"../../Native/glue.h\"\n";
print CPP "extern \"C\" {\n";
print CPP "void urho_unsubscribe (NotificationProxy *proxy);\n";

sub mapType {
    my ($pt) = @_;
    ($t,$st) = split (/ /, $pt);
    if ($st eq "pointer"){
	return "IntPtr" if $t eq "b2Contact";
	return "$t";
    }
    if ($st eq "ptr"){
	return "$t";
    }

    return "uint" if $t eq "unsigned";
    return "bool" if $t eq "Bool";
    return "float" if $t eq "Float";
    return "IntPtr" if $t eq "User-defined";
    return "IntPtr" if $t eq "SDL_Event";
    return "CollisionData []" if $pt eq "Buffer containing";
    return "byte []" if $t eq "Buffer";
    return $t;
}

sub stringHash {
    my ($str) = @_;
	$str = lc $str;
	$hash = 0;
	foreach $char (split //, $str) {
		$hash = ord($char) + ($hash << 6) + ($hash << 16) - $hash;
		$hash = $hash & 0xffffffff;
	}
	return $hash;
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
			$pccode = &stringHash($pn);
		    print CS "            public $cspt $pn =>$cast UrhoMap.get_$plain (handle, unchecked((int)$pccode) /* $pn ($pc) */);\n";
		}
		if (/}/){
		    print CS "        } /* struct ${eventName}EventArgs */\n\n";

		    for $type (split /,/,$events{$ec}){
			$eccode = &stringHash($en);

			print CS "        public partial class $type {\n"; 
			print CS "             [Obsolete(\"SubscribeTo API may lead to unxpected behaviour and will be removed in a future version. Use C# event '.${eventName} += ...' instead.\")]\n";			
			print CS "             @{[$en eq 'Update' ? 'internal' : 'public']} Subscription SubscribeTo${eventName} (Action<${eventName}EventArgs> handler)\n";
			print CS "             {\n";
			print CS "                  Action<IntPtr> proxy = (x)=> { var d = new ${eventName}EventArgs () { handle = x }; handler (d); };\n";
			print CS "                  var s = new Subscription (proxy);\n";
			print CS "                  s.UnmanagedProxy = UrhoObject.urho_subscribe_event (handle, UrhoObject.ObjectCallbackInstance, GCHandle.ToIntPtr (s.gch), unchecked((int)$eccode) /* $en ($ec) */);\n";
			print CS "                  return s;\n";
			print CS "             }\n\n";
			print CS "             static UrhoEventAdapter<${eventName}EventArgs> eventAdapterFor${eventName};\n";
			print CS "             public event Action<${eventName}EventArgs> ${eventName}\n";
			print CS "             {\n";
			print CS "                 add\n";
			print CS "                 {\n";
			print CS "                      if (eventAdapterFor${eventName} == null)\n";
			print CS "                          eventAdapterFor${eventName} = new UrhoEventAdapter<${eventName}EventArgs>(typeof($type));\n";
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
print CPP "// TODO: remove this file.\n";
print CPP "}\n";
print CS "#pragma warning restore CS0618, CS0649";
close CS;
close CPP;
