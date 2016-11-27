#include "../../Native/AllUrho.h"
#include "../../Native/glue.h"
extern "C" {
void urho_unsubscribe (NotificationProxy *proxy);
DllExport void *urho_subscribe_SoundFinished (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_SOUNDFINISHED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_SOUNDFINISHED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_FrameStarted (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_BEGINFRAME);
	receiver->SubscribeToEvent (receiver, Urho3D::E_BEGINFRAME, proxy);
	return proxy;
}

DllExport void *urho_subscribe_Update (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_UPDATE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_UPDATE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_PostUpdate (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_POSTUPDATE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_POSTUPDATE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_RenderUpdate (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_RENDERUPDATE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_RENDERUPDATE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_PostRenderUpdate (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_POSTRENDERUPDATE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_POSTRENDERUPDATE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_FrameEnded (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ENDFRAME);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ENDFRAME, proxy);
	return proxy;
}

DllExport void *urho_subscribe_WorkItemCompleted (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_WORKITEMCOMPLETED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_WORKITEMCOMPLETED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ConsoleCommand (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_CONSOLECOMMAND);
	receiver->SubscribeToEvent (receiver, Urho3D::E_CONSOLECOMMAND, proxy);
	return proxy;
}

DllExport void *urho_subscribe_BoneHierarchyCreated (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_BONEHIERARCHYCREATED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_BONEHIERARCHYCREATED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_AnimationTrigger (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ANIMATIONTRIGGER);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ANIMATIONTRIGGER, proxy);
	return proxy;
}

DllExport void *urho_subscribe_AnimationFinished (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ANIMATIONFINISHED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ANIMATIONFINISHED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ParticleEffectFinished (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_PARTICLEEFFECTFINISHED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_PARTICLEEFFECTFINISHED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_TerrainCreated (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_TERRAINCREATED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_TERRAINCREATED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ScreenMode (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_SCREENMODE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_SCREENMODE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_WindowPos (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_WINDOWPOS);
	receiver->SubscribeToEvent (receiver, Urho3D::E_WINDOWPOS, proxy);
	return proxy;
}

DllExport void *urho_subscribe_RenderSurfaceUpdate (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_RENDERSURFACEUPDATE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_RENDERSURFACEUPDATE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_BeginRendering (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_BEGINRENDERING);
	receiver->SubscribeToEvent (receiver, Urho3D::E_BEGINRENDERING, proxy);
	return proxy;
}

DllExport void *urho_subscribe_EndRendering (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ENDRENDERING);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ENDRENDERING, proxy);
	return proxy;
}

DllExport void *urho_subscribe_BeginViewUpdate (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_BEGINVIEWUPDATE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_BEGINVIEWUPDATE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_EndViewUpdate (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ENDVIEWUPDATE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ENDVIEWUPDATE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_BeginViewRender (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_BEGINVIEWRENDER);
	receiver->SubscribeToEvent (receiver, Urho3D::E_BEGINVIEWRENDER, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ViewBuffersReady (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_VIEWBUFFERSREADY);
	receiver->SubscribeToEvent (receiver, Urho3D::E_VIEWBUFFERSREADY, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ViewGlobalShaderParameters (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_VIEWGLOBALSHADERPARAMETERS);
	receiver->SubscribeToEvent (receiver, Urho3D::E_VIEWGLOBALSHADERPARAMETERS, proxy);
	return proxy;
}

DllExport void *urho_subscribe_EndViewRender (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ENDVIEWRENDER);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ENDVIEWRENDER, proxy);
	return proxy;
}

DllExport void *urho_subscribe_RenderPathEvent (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_RENDERPATHEVENT);
	receiver->SubscribeToEvent (receiver, Urho3D::E_RENDERPATHEVENT, proxy);
	return proxy;
}

DllExport void *urho_subscribe_DeviceLost (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_DEVICELOST);
	receiver->SubscribeToEvent (receiver, Urho3D::E_DEVICELOST, proxy);
	return proxy;
}

DllExport void *urho_subscribe_DeviceReset (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_DEVICERESET);
	receiver->SubscribeToEvent (receiver, Urho3D::E_DEVICERESET, proxy);
	return proxy;
}

DllExport void *urho_subscribe_LogMessage (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_LOGMESSAGE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_LOGMESSAGE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_AsyncExecFinished (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ASYNCEXECFINISHED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ASYNCEXECFINISHED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_MouseButtonDown (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_MOUSEBUTTONDOWN);
	receiver->SubscribeToEvent (receiver, Urho3D::E_MOUSEBUTTONDOWN, proxy);
	return proxy;
}

DllExport void *urho_subscribe_MouseButtonUp (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_MOUSEBUTTONUP);
	receiver->SubscribeToEvent (receiver, Urho3D::E_MOUSEBUTTONUP, proxy);
	return proxy;
}

DllExport void *urho_subscribe_MouseMoved (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_MOUSEMOVE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_MOUSEMOVE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_MouseWheel (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_MOUSEWHEEL);
	receiver->SubscribeToEvent (receiver, Urho3D::E_MOUSEWHEEL, proxy);
	return proxy;
}

DllExport void *urho_subscribe_KeyDown (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_KEYDOWN);
	receiver->SubscribeToEvent (receiver, Urho3D::E_KEYDOWN, proxy);
	return proxy;
}

DllExport void *urho_subscribe_KeyUp (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_KEYUP);
	receiver->SubscribeToEvent (receiver, Urho3D::E_KEYUP, proxy);
	return proxy;
}

DllExport void *urho_subscribe_TextInput (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_TEXTINPUT);
	receiver->SubscribeToEvent (receiver, Urho3D::E_TEXTINPUT, proxy);
	return proxy;
}

DllExport void *urho_subscribe_TextEditing (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_TEXTEDITING);
	receiver->SubscribeToEvent (receiver, Urho3D::E_TEXTEDITING, proxy);
	return proxy;
}

DllExport void *urho_subscribe_JoystickConnected (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_JOYSTICKCONNECTED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_JOYSTICKCONNECTED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_JoystickDisconnected (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_JOYSTICKDISCONNECTED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_JOYSTICKDISCONNECTED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_JoystickButtonDown (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_JOYSTICKBUTTONDOWN);
	receiver->SubscribeToEvent (receiver, Urho3D::E_JOYSTICKBUTTONDOWN, proxy);
	return proxy;
}

DllExport void *urho_subscribe_JoystickButtonUp (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_JOYSTICKBUTTONUP);
	receiver->SubscribeToEvent (receiver, Urho3D::E_JOYSTICKBUTTONUP, proxy);
	return proxy;
}

DllExport void *urho_subscribe_JoystickAxisMove (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_JOYSTICKAXISMOVE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_JOYSTICKAXISMOVE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_JoystickHatMove (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_JOYSTICKHATMOVE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_JOYSTICKHATMOVE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_TouchBegin (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_TOUCHBEGIN);
	receiver->SubscribeToEvent (receiver, Urho3D::E_TOUCHBEGIN, proxy);
	return proxy;
}

DllExport void *urho_subscribe_TouchEnd (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_TOUCHEND);
	receiver->SubscribeToEvent (receiver, Urho3D::E_TOUCHEND, proxy);
	return proxy;
}

DllExport void *urho_subscribe_TouchMove (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_TOUCHMOVE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_TOUCHMOVE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_GestureRecorded (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_GESTURERECORDED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_GESTURERECORDED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_GestureInput (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_GESTUREINPUT);
	receiver->SubscribeToEvent (receiver, Urho3D::E_GESTUREINPUT, proxy);
	return proxy;
}

DllExport void *urho_subscribe_MultiGesture (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_MULTIGESTURE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_MULTIGESTURE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_DropFile (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_DROPFILE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_DROPFILE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_InputFocus (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_INPUTFOCUS);
	receiver->SubscribeToEvent (receiver, Urho3D::E_INPUTFOCUS, proxy);
	return proxy;
}

DllExport void *urho_subscribe_MouseVisibleChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_MOUSEVISIBLECHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_MOUSEVISIBLECHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_MouseModeChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_MOUSEMODECHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_MOUSEMODECHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ExitRequested (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_EXITREQUESTED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_EXITREQUESTED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_SDLRawInput (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_SDLRAWINPUT);
	receiver->SubscribeToEvent (receiver, Urho3D::E_SDLRAWINPUT, proxy);
	return proxy;
}

DllExport void *urho_subscribe_InputBegin (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_INPUTBEGIN);
	receiver->SubscribeToEvent (receiver, Urho3D::E_INPUTBEGIN, proxy);
	return proxy;
}

DllExport void *urho_subscribe_InputEnd (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_INPUTEND);
	receiver->SubscribeToEvent (receiver, Urho3D::E_INPUTEND, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NavigationMeshRebuilt (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NAVIGATION_MESH_REBUILT);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NAVIGATION_MESH_REBUILT, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NavigationAreaRebuilt (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NAVIGATION_AREA_REBUILT);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NAVIGATION_AREA_REBUILT, proxy);
	return proxy;
}

DllExport void *urho_subscribe_CrowdAgentFormation (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_CROWD_AGENT_FORMATION);
	receiver->SubscribeToEvent (receiver, Urho3D::E_CROWD_AGENT_FORMATION, proxy);
	return proxy;
}

DllExport void *urho_subscribe_CrowdAgentNodeFormation (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_CROWD_AGENT_NODE_FORMATION);
	receiver->SubscribeToEvent (receiver, Urho3D::E_CROWD_AGENT_NODE_FORMATION, proxy);
	return proxy;
}

DllExport void *urho_subscribe_CrowdAgentReposition (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_CROWD_AGENT_REPOSITION);
	receiver->SubscribeToEvent (receiver, Urho3D::E_CROWD_AGENT_REPOSITION, proxy);
	return proxy;
}

DllExport void *urho_subscribe_CrowdAgentNodeReposition (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_CROWD_AGENT_NODE_REPOSITION);
	receiver->SubscribeToEvent (receiver, Urho3D::E_CROWD_AGENT_NODE_REPOSITION, proxy);
	return proxy;
}

DllExport void *urho_subscribe_CrowdAgentFailure (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_CROWD_AGENT_FAILURE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_CROWD_AGENT_FAILURE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_CrowdAgentNodeFailure (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_CROWD_AGENT_NODE_FAILURE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_CROWD_AGENT_NODE_FAILURE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_CrowdAgentStateChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_CROWD_AGENT_STATE_CHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_CROWD_AGENT_STATE_CHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_CrowdAgentNodeStateChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_CROWD_AGENT_NODE_STATE_CHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_CROWD_AGENT_NODE_STATE_CHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NavigationObstacleAdded (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NAVIGATION_OBSTACLE_ADDED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NAVIGATION_OBSTACLE_ADDED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NavigationObstacleRemoved (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NAVIGATION_OBSTACLE_REMOVED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NAVIGATION_OBSTACLE_REMOVED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ServerConnected (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_SERVERCONNECTED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_SERVERCONNECTED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ServerDisconnected (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_SERVERDISCONNECTED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_SERVERDISCONNECTED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ConnectFailed (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_CONNECTFAILED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_CONNECTFAILED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ClientConnected (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_CLIENTCONNECTED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_CLIENTCONNECTED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ClientDisconnected (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_CLIENTDISCONNECTED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_CLIENTDISCONNECTED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ClientIdentity (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_CLIENTIDENTITY);
	receiver->SubscribeToEvent (receiver, Urho3D::E_CLIENTIDENTITY, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ClientSceneLoaded (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_CLIENTSCENELOADED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_CLIENTSCENELOADED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NetworkMessage (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NETWORKMESSAGE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NETWORKMESSAGE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NetworkUpdate (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NETWORKUPDATE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NETWORKUPDATE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NetworkUpdateSent (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NETWORKUPDATESENT);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NETWORKUPDATESENT, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NetworkSceneLoadFailed (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NETWORKSCENELOADFAILED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NETWORKSCENELOADFAILED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_RemoteEventData (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_REMOTEEVENTDATA);
	receiver->SubscribeToEvent (receiver, Urho3D::E_REMOTEEVENTDATA, proxy);
	return proxy;
}

DllExport void *urho_subscribe_PhysicsPreStep (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_PHYSICSPRESTEP);
	receiver->SubscribeToEvent (receiver, Urho3D::E_PHYSICSPRESTEP, proxy);
	return proxy;
}

DllExport void *urho_subscribe_PhysicsPostStep (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_PHYSICSPOSTSTEP);
	receiver->SubscribeToEvent (receiver, Urho3D::E_PHYSICSPOSTSTEP, proxy);
	return proxy;
}

DllExport void *urho_subscribe_PhysicsCollisionStart (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_PHYSICSCOLLISIONSTART);
	receiver->SubscribeToEvent (receiver, Urho3D::E_PHYSICSCOLLISIONSTART, proxy);
	return proxy;
}

DllExport void *urho_subscribe_PhysicsCollision (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_PHYSICSCOLLISION);
	receiver->SubscribeToEvent (receiver, Urho3D::E_PHYSICSCOLLISION, proxy);
	return proxy;
}

DllExport void *urho_subscribe_PhysicsCollisionEnd (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_PHYSICSCOLLISIONEND);
	receiver->SubscribeToEvent (receiver, Urho3D::E_PHYSICSCOLLISIONEND, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NodeCollisionStart (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NODECOLLISIONSTART);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NODECOLLISIONSTART, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NodeCollision (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NODECOLLISION);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NODECOLLISION, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NodeCollisionEnd (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NODECOLLISIONEND);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NODECOLLISIONEND, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ReloadStarted (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_RELOADSTARTED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_RELOADSTARTED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ReloadFinished (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_RELOADFINISHED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_RELOADFINISHED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ReloadFailed (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_RELOADFAILED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_RELOADFAILED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_FileChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_FILECHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_FILECHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_LoadFailed (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_LOADFAILED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_LOADFAILED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ResourceNotFound (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_RESOURCENOTFOUND);
	receiver->SubscribeToEvent (receiver, Urho3D::E_RESOURCENOTFOUND, proxy);
	return proxy;
}

DllExport void *urho_subscribe_UnknownResourceType (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_UNKNOWNRESOURCETYPE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_UNKNOWNRESOURCETYPE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ResourceBackgroundLoaded (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_RESOURCEBACKGROUNDLOADED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_RESOURCEBACKGROUNDLOADED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ChangeLanguage (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_CHANGELANGUAGE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_CHANGELANGUAGE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_SceneUpdate (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_SCENEUPDATE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_SCENEUPDATE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_SceneSubsystemUpdate (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_SCENESUBSYSTEMUPDATE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_SCENESUBSYSTEMUPDATE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_UpdateSmoothing (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_UPDATESMOOTHING);
	receiver->SubscribeToEvent (receiver, Urho3D::E_UPDATESMOOTHING, proxy);
	return proxy;
}

DllExport void *urho_subscribe_SceneDrawableUpdateFinished (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_SCENEDRAWABLEUPDATEFINISHED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_SCENEDRAWABLEUPDATEFINISHED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_TargetPositionChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_TARGETPOSITION);
	receiver->SubscribeToEvent (receiver, Urho3D::E_TARGETPOSITION, proxy);
	return proxy;
}

DllExport void *urho_subscribe_TargetRotationChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_TARGETROTATION);
	receiver->SubscribeToEvent (receiver, Urho3D::E_TARGETROTATION, proxy);
	return proxy;
}

DllExport void *urho_subscribe_AttributeAnimationUpdate (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ATTRIBUTEANIMATIONUPDATE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ATTRIBUTEANIMATIONUPDATE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_AttributeAnimationAdded (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ATTRIBUTEANIMATIONADDED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ATTRIBUTEANIMATIONADDED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_AttributeAnimationRemoved (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ATTRIBUTEANIMATIONREMOVED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ATTRIBUTEANIMATIONREMOVED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ScenePostUpdate (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_SCENEPOSTUPDATE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_SCENEPOSTUPDATE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_AsyncLoadProgress (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ASYNCLOADPROGRESS);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ASYNCLOADPROGRESS, proxy);
	return proxy;
}

DllExport void *urho_subscribe_AsyncLoadFinished (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ASYNCLOADFINISHED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ASYNCLOADFINISHED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NodeAdded (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NODEADDED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NODEADDED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NodeRemoved (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NODEREMOVED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NODEREMOVED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ComponentAdded (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_COMPONENTADDED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_COMPONENTADDED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ComponentRemoved (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_COMPONENTREMOVED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_COMPONENTREMOVED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NodeNameChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NODENAMECHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NODENAMECHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NodeEnabledChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NODEENABLEDCHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NODEENABLEDCHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NodeTagAdded (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NODETAGADDED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NODETAGADDED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NodeTagRemoved (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NODETAGREMOVED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NODETAGREMOVED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ComponentEnabledChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_COMPONENTENABLEDCHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_COMPONENTENABLEDCHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_TemporaryChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_TEMPORARYCHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_TEMPORARYCHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NodeCloned (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NODECLONED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NODECLONED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ComponentCloned (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_COMPONENTCLONED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_COMPONENTCLONED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_InterceptNetworkUpdate (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_INTERCEPTNETWORKUPDATE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_INTERCEPTNETWORKUPDATE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_UIMouseClick (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_UIMOUSECLICK);
	receiver->SubscribeToEvent (receiver, Urho3D::E_UIMOUSECLICK, proxy);
	return proxy;
}

DllExport void *urho_subscribe_UIMouseClickEnd (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_UIMOUSECLICKEND);
	receiver->SubscribeToEvent (receiver, Urho3D::E_UIMOUSECLICKEND, proxy);
	return proxy;
}

DllExport void *urho_subscribe_UIMouseDoubleClick (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_UIMOUSEDOUBLECLICK);
	receiver->SubscribeToEvent (receiver, Urho3D::E_UIMOUSEDOUBLECLICK, proxy);
	return proxy;
}

DllExport void *urho_subscribe_Click (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_CLICK);
	receiver->SubscribeToEvent (receiver, Urho3D::E_CLICK, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ClickEnd (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_CLICKEND);
	receiver->SubscribeToEvent (receiver, Urho3D::E_CLICKEND, proxy);
	return proxy;
}

DllExport void *urho_subscribe_DoubleClick (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_DOUBLECLICK);
	receiver->SubscribeToEvent (receiver, Urho3D::E_DOUBLECLICK, proxy);
	return proxy;
}

DllExport void *urho_subscribe_DragDropTest (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_DRAGDROPTEST);
	receiver->SubscribeToEvent (receiver, Urho3D::E_DRAGDROPTEST, proxy);
	return proxy;
}

DllExport void *urho_subscribe_DragDropFinish (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_DRAGDROPFINISH);
	receiver->SubscribeToEvent (receiver, Urho3D::E_DRAGDROPFINISH, proxy);
	return proxy;
}

DllExport void *urho_subscribe_FocusChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_FOCUSCHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_FOCUSCHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NameChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NAMECHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NAMECHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_Resized (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_RESIZED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_RESIZED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_Positioned (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_POSITIONED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_POSITIONED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_VisibleChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_VISIBLECHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_VISIBLECHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_Focused (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_FOCUSED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_FOCUSED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_Defocused (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_DEFOCUSED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_DEFOCUSED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_LayoutUpdated (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_LAYOUTUPDATED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_LAYOUTUPDATED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_Pressed (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_PRESSED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_PRESSED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_Released (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_RELEASED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_RELEASED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_Toggled (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_TOGGLED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_TOGGLED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_SliderChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_SLIDERCHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_SLIDERCHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_SliderPaged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_SLIDERPAGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_SLIDERPAGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ProgressBarChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_PROGRESSBARCHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_PROGRESSBARCHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ScrollBarChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_SCROLLBARCHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_SCROLLBARCHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ViewChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_VIEWCHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_VIEWCHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ModalChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_MODALCHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_MODALCHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_TextEntry (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_TEXTENTRY);
	receiver->SubscribeToEvent (receiver, Urho3D::E_TEXTENTRY, proxy);
	return proxy;
}

DllExport void *urho_subscribe_TextChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_TEXTCHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_TEXTCHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_TextFinished (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_TEXTFINISHED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_TEXTFINISHED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_MenuSelected (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_MENUSELECTED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_MENUSELECTED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ItemSelected (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ITEMSELECTED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ITEMSELECTED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ItemDeselected (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ITEMDESELECTED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ITEMDESELECTED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_SelectionChanged (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_SELECTIONCHANGED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_SELECTIONCHANGED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ItemClicked (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ITEMCLICKED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ITEMCLICKED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ItemDoubleClicked (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ITEMDOUBLECLICKED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ITEMDOUBLECLICKED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_UnhandledKey (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_UNHANDLEDKEY);
	receiver->SubscribeToEvent (receiver, Urho3D::E_UNHANDLEDKEY, proxy);
	return proxy;
}

DllExport void *urho_subscribe_FileSelected (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_FILESELECTED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_FILESELECTED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_MessageACK (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_MESSAGEACK);
	receiver->SubscribeToEvent (receiver, Urho3D::E_MESSAGEACK, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ElementAdded (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ELEMENTADDED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ELEMENTADDED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_ElementRemoved (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_ELEMENTREMOVED);
	receiver->SubscribeToEvent (receiver, Urho3D::E_ELEMENTREMOVED, proxy);
	return proxy;
}

DllExport void *urho_subscribe_HoverBegin (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_HOVERBEGIN);
	receiver->SubscribeToEvent (receiver, Urho3D::E_HOVERBEGIN, proxy);
	return proxy;
}

DllExport void *urho_subscribe_HoverEnd (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_HOVEREND);
	receiver->SubscribeToEvent (receiver, Urho3D::E_HOVEREND, proxy);
	return proxy;
}

DllExport void *urho_subscribe_DragBegin (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_DRAGBEGIN);
	receiver->SubscribeToEvent (receiver, Urho3D::E_DRAGBEGIN, proxy);
	return proxy;
}

DllExport void *urho_subscribe_DragMove (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_DRAGMOVE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_DRAGMOVE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_DragEnd (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_DRAGEND);
	receiver->SubscribeToEvent (receiver, Urho3D::E_DRAGEND, proxy);
	return proxy;
}

DllExport void *urho_subscribe_DragCancel (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_DRAGCANCEL);
	receiver->SubscribeToEvent (receiver, Urho3D::E_DRAGCANCEL, proxy);
	return proxy;
}

DllExport void *urho_subscribe_UIDropFile (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_UIDROPFILE);
	receiver->SubscribeToEvent (receiver, Urho3D::E_UIDROPFILE, proxy);
	return proxy;
}

DllExport void *urho_subscribe_PhysicsBeginContact2D (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_PHYSICSBEGINCONTACT2D);
	receiver->SubscribeToEvent (receiver, Urho3D::E_PHYSICSBEGINCONTACT2D, proxy);
	return proxy;
}

DllExport void *urho_subscribe_PhysicsEndContact2D (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_PHYSICSENDCONTACT2D);
	receiver->SubscribeToEvent (receiver, Urho3D::E_PHYSICSENDCONTACT2D, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NodeBeginContact2D (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NODEBEGINCONTACT2D);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NODEBEGINCONTACT2D, proxy);
	return proxy;
}

DllExport void *urho_subscribe_NodeEndContact2D (void *_receiver, HandlerFunctionPtr callback, void *data)
{
	Urho3D::Object *receiver = (Urho3D::Object *) _receiver;
	NotificationProxy *proxy = new NotificationProxy (receiver, callback, data, Urho3D::E_NODEENDCONTACT2D);
	receiver->SubscribeToEvent (receiver, Urho3D::E_NODEENDCONTACT2D, proxy);
	return proxy;
}

// Hash Getters
DllExport int urho_hash_get_P_SDLEVENT ()
{
	return Urho3D::SDLRawInput::P_SDLEVENT.Value ();
}

DllExport int urho_hash_get_P_BUTTONS ()
{
	return Urho3D::DragCancel::P_BUTTONS.Value ();
}

DllExport int urho_hash_get_P_COMPOSITION ()
{
	return Urho3D::TextEditing::P_COMPOSITION.Value ();
}

DllExport int urho_hash_get_P_CONTACT ()
{
	return Urho3D::NodeEndContact2D::P_CONTACT.Value ();
}

DllExport int urho_hash_get_P_TARGET ()
{
	return Urho3D::DragDropFinish::P_TARGET.Value ();
}

DllExport int urho_hash_get_P_BODY ()
{
	return Urho3D::NodeEndContact2D::P_BODY.Value ();
}

DllExport int urho_hash_get_P_OTHERNODE ()
{
	return Urho3D::NodeEndContact2D::P_OTHERNODE.Value ();
}

DllExport int urho_hash_get_P_NUMFINGERS ()
{
	return Urho3D::MultiGesture::P_NUMFINGERS.Value ();
}

DllExport int urho_hash_get_P_WIDTH ()
{
	return Urho3D::Resized::P_WIDTH.Value ();
}

DllExport int urho_hash_get_P_TOTALNODES ()
{
	return Urho3D::AsyncLoadProgress::P_TOTALNODES.Value ();
}

DllExport int urho_hash_get_P_AXIS ()
{
	return Urho3D::JoystickAxisMove::P_AXIS.Value ();
}

DllExport int urho_hash_get_P_CONNECTION ()
{
	return Urho3D::RemoteEventData::P_CONNECTION.Value ();
}

DllExport int urho_hash_get_P_BOUNDSMAX ()
{
	return Urho3D::NavigationAreaRebuilt::P_BOUNDSMAX.Value ();
}

DllExport int urho_hash_get_P_MODAL ()
{
	return Urho3D::ModalChanged::P_MODAL.Value ();
}

DllExport int urho_hash_get_P_FULLSCREEN ()
{
	return Urho3D::ScreenMode::P_FULLSCREEN.Value ();
}

DllExport int urho_hash_get_P_ERROR ()
{
	return Urho3D::GestureInput::P_ERROR.Value ();
}

DllExport int urho_hash_get_P_LEVEL ()
{
	return Urho3D::LogMessage::P_LEVEL.Value ();
}

DllExport int urho_hash_get_P_NAME ()
{
	return Urho3D::InterceptNetworkUpdate::P_NAME.Value ();
}

DllExport int urho_hash_get_P_STATE ()
{
	return Urho3D::Toggled::P_STATE.Value ();
}

DllExport int urho_hash_get_P_ELEMENTX ()
{
	return Urho3D::UIDropFile::P_ELEMENTX.Value ();
}

DllExport int urho_hash_get_P_DX ()
{
	return Urho3D::DragMove::P_DX.Value ();
}

DllExport int urho_hash_get_P_MESSAGEID ()
{
	return Urho3D::NetworkMessage::P_MESSAGEID.Value ();
}

DllExport int urho_hash_get_P_CROWD_AGENT_STATE ()
{
	return Urho3D::CrowdAgentNodeStateChanged::P_CROWD_AGENT_STATE.Value ();
}

DllExport int urho_hash_get_P_HAT ()
{
	return Urho3D::JoystickHatMove::P_HAT.Value ();
}

DllExport int urho_hash_get_P_VELOCITY ()
{
	return Urho3D::CrowdAgentNodeStateChanged::P_VELOCITY.Value ();
}

DllExport int urho_hash_get_P_BOUNDSMIN ()
{
	return Urho3D::NavigationAreaRebuilt::P_BOUNDSMIN.Value ();
}

DllExport int urho_hash_get_P_ARRIVED ()
{
	return Urho3D::CrowdAgentNodeReposition::P_ARRIVED.Value ();
}

DllExport int urho_hash_get_P_SOURCE ()
{
	return Urho3D::DragDropFinish::P_SOURCE.Value ();
}

DllExport int urho_hash_get_P_MESSAGE ()
{
	return Urho3D::LogMessage::P_MESSAGE.Value ();
}

DllExport int urho_hash_get_P_KEY ()
{
	return Urho3D::UnhandledKey::P_KEY.Value ();
}

DllExport int urho_hash_get_P_HEIGHT ()
{
	return Urho3D::Resized::P_HEIGHT.Value ();
}

DllExport int urho_hash_get_P_SELECTION ()
{
	return Urho3D::ItemDoubleClicked::P_SELECTION.Value ();
}

DllExport int urho_hash_get_P_MOUSELOCKED ()
{
	return Urho3D::MouseModeChanged::P_MOUSELOCKED.Value ();
}

DllExport int urho_hash_get_P_CONTACTS ()
{
	return Urho3D::NodeCollision::P_CONTACTS.Value ();
}

DllExport int urho_hash_get_P_MODE ()
{
	return Urho3D::MouseModeChanged::P_MODE.Value ();
}

DllExport int urho_hash_get_P_OBSTACLE ()
{
	return Urho3D::NavigationObstacleRemoved::P_OBSTACLE.Value ();
}

DllExport int urho_hash_get_P_FOCUS ()
{
	return Urho3D::InputFocus::P_FOCUS.Value ();
}

DllExport int urho_hash_get_P_LOADEDRESOURCES ()
{
	return Urho3D::AsyncLoadProgress::P_LOADEDRESOURCES.Value ();
}

DllExport int urho_hash_get_P_X ()
{
	return Urho3D::UIDropFile::P_X.Value ();
}

DllExport int urho_hash_get_P_ACCEPT ()
{
	return Urho3D::DragDropFinish::P_ACCEPT.Value ();
}

DllExport int urho_hash_get_P_GESTUREID ()
{
	return Urho3D::GestureInput::P_GESTUREID.Value ();
}

DllExport int urho_hash_get_P_COMPONENT ()
{
	return Urho3D::ComponentCloned::P_COMPONENT.Value ();
}

DllExport int urho_hash_get_P_DATA ()
{
	return Urho3D::NetworkMessage::P_DATA.Value ();
}

DllExport int urho_hash_get_P_SOUNDSOURCE ()
{
	return Urho3D::SoundFinished::P_SOUNDSOURCE.Value ();
}

DllExport int urho_hash_get_P_ELEMENT ()
{
	return Urho3D::UIDropFile::P_ELEMENT.Value ();
}

DllExport int urho_hash_get_P_ROOT ()
{
	return Urho3D::ElementRemoved::P_ROOT.Value ();
}

DllExport int urho_hash_get_P_BUTTON ()
{
	return Urho3D::ItemDoubleClicked::P_BUTTON.Value ();
}

DllExport int urho_hash_get_P_EXITCODE ()
{
	return Urho3D::AsyncExecFinished::P_EXITCODE.Value ();
}

DllExport int urho_hash_get_P_CURSOR ()
{
	return Urho3D::TextEditing::P_CURSOR.Value ();
}

DllExport int urho_hash_get_P_FILENAME ()
{
	return Urho3D::UIDropFile::P_FILENAME.Value ();
}

DllExport int urho_hash_get_P_WHEEL ()
{
	return Urho3D::MouseWheel::P_WHEEL.Value ();
}

DllExport int urho_hash_get_P_CENTERX ()
{
	return Urho3D::MultiGesture::P_CENTERX.Value ();
}

DllExport int urho_hash_get_P_VISIBLE ()
{
	return Urho3D::VisibleChanged::P_VISIBLE.Value ();
}

DllExport int urho_hash_get_P_TOTALRESOURCES ()
{
	return Urho3D::AsyncLoadProgress::P_TOTALRESOURCES.Value ();
}

DllExport int urho_hash_get_P_ITEM ()
{
	return Urho3D::ItemDoubleClicked::P_ITEM.Value ();
}

DllExport int urho_hash_get_P_ANIMATION ()
{
	return Urho3D::AnimationFinished::P_ANIMATION.Value ();
}

DllExport int urho_hash_get_P_RESOURCENAME ()
{
	return Urho3D::ResourceBackgroundLoaded::P_RESOURCENAME.Value ();
}

DllExport int urho_hash_get_P_TIMESTAMP ()
{
	return Urho3D::InterceptNetworkUpdate::P_TIMESTAMP.Value ();
}

DllExport int urho_hash_get_P_ID ()
{
	return Urho3D::ConsoleCommand::P_ID.Value ();
}

DllExport int urho_hash_get_P_QUALIFIERS ()
{
	return Urho3D::UnhandledKey::P_QUALIFIERS.Value ();
}

DllExport int urho_hash_get_P_MINIMIZED ()
{
	return Urho3D::InputFocus::P_MINIMIZED.Value ();
}

DllExport int urho_hash_get_P_HIGHDPI ()
{
	return Urho3D::ScreenMode::P_HIGHDPI.Value ();
}

DllExport int urho_hash_get_P_SOUND ()
{
	return Urho3D::SoundFinished::P_SOUND.Value ();
}

DllExport int urho_hash_get_P_LOADEDNODES ()
{
	return Urho3D::AsyncLoadProgress::P_LOADEDNODES.Value ();
}

DllExport int urho_hash_get_P_CAMERA ()
{
	return Urho3D::EndViewRender::P_CAMERA.Value ();
}

DllExport int urho_hash_get_P_Y ()
{
	return Urho3D::UIDropFile::P_Y.Value ();
}

DllExport int urho_hash_get_P_PRESSED ()
{
	return Urho3D::SliderPaged::P_PRESSED.Value ();
}

DllExport int urho_hash_get_P_REQUESTID ()
{
	return Urho3D::AsyncExecFinished::P_REQUESTID.Value ();
}

DllExport int urho_hash_get_P_CLICKEDELEMENT ()
{
	return Urho3D::FocusChanged::P_CLICKEDELEMENT.Value ();
}

DllExport int urho_hash_get_P_NODEA ()
{
	return Urho3D::PhysicsEndContact2D::P_NODEA.Value ();
}

DllExport int urho_hash_get_P_NUMBUTTONS ()
{
	return Urho3D::DragCancel::P_NUMBUTTONS.Value ();
}

DllExport int urho_hash_get_P_OBJECTANIMATION ()
{
	return Urho3D::AttributeAnimationRemoved::P_OBJECTANIMATION.Value ();
}

DllExport int urho_hash_get_P_ELEMENTY ()
{
	return Urho3D::UIDropFile::P_ELEMENTY.Value ();
}

DllExport int urho_hash_get_P_CLONENODE ()
{
	return Urho3D::NodeCloned::P_CLONENODE.Value ();
}

DllExport int urho_hash_get_P_ALLOW ()
{
	return Urho3D::ClientIdentity::P_ALLOW.Value ();
}

DllExport int urho_hash_get_P_SUCCESS ()
{
	return Urho3D::ResourceBackgroundLoaded::P_SUCCESS.Value ();
}

DllExport int urho_hash_get_P_VALUE ()
{
	return Urho3D::TextFinished::P_VALUE.Value ();
}

DllExport int urho_hash_get_P_COMMAND ()
{
	return Urho3D::ConsoleCommand::P_COMMAND.Value ();
}

DllExport int urho_hash_get_P_RESIZABLE ()
{
	return Urho3D::ScreenMode::P_RESIZABLE.Value ();
}

DllExport int urho_hash_get_P_CROWD_AGENT ()
{
	return Urho3D::CrowdAgentNodeStateChanged::P_CROWD_AGENT.Value ();
}

DllExport int urho_hash_get_P_CROWD_TARGET_STATE ()
{
	return Urho3D::CrowdAgentNodeStateChanged::P_CROWD_TARGET_STATE.Value ();
}

DllExport int urho_hash_get_P_RESOURCETYPE ()
{
	return Urho3D::UnknownResourceType::P_RESOURCETYPE.Value ();
}

DllExport int urho_hash_get_P_FILTER ()
{
	return Urho3D::FileSelected::P_FILTER.Value ();
}

DllExport int urho_hash_get_P_BYKEY ()
{
	return Urho3D::Focused::P_BYKEY.Value ();
}

DllExport int urho_hash_get_P_BODYB ()
{
	return Urho3D::PhysicsEndContact2D::P_BODYB.Value ();
}

DllExport int urho_hash_get_P_TIMESTEP ()
{
	return Urho3D::ScenePostUpdate::P_TIMESTEP.Value ();
}

DllExport int urho_hash_get_P_CONSUMED ()
{
	return Urho3D::SDLRawInput::P_CONSUMED.Value ();
}

DllExport int urho_hash_get_P_ATTRIBUTEANIMATIONNAME ()
{
	return Urho3D::AttributeAnimationRemoved::P_ATTRIBUTEANIMATIONNAME.Value ();
}

DllExport int urho_hash_get_P_WORLD ()
{
	return Urho3D::PhysicsEndContact2D::P_WORLD.Value ();
}

DllExport int urho_hash_get_P_SCANCODE ()
{
	return Urho3D::KeyUp::P_SCANCODE.Value ();
}

DllExport int urho_hash_get_P_NODEB ()
{
	return Urho3D::PhysicsEndContact2D::P_NODEB.Value ();
}

DllExport int urho_hash_get_P_SQUAREDSNAPTHRESHOLD ()
{
	return Urho3D::UpdateSmoothing::P_SQUAREDSNAPTHRESHOLD.Value ();
}

DllExport int urho_hash_get_P_JOYSTICKID ()
{
	return Urho3D::JoystickHatMove::P_JOYSTICKID.Value ();
}

DllExport int urho_hash_get_P_VIEW ()
{
	return Urho3D::EndViewRender::P_VIEW.Value ();
}

DllExport int urho_hash_get_P_FRAMENUMBER ()
{
	return Urho3D::BeginFrame::P_FRAMENUMBER.Value ();
}

DllExport int urho_hash_get_P_TOUCHID ()
{
	return Urho3D::TouchMove::P_TOUCHID.Value ();
}

DllExport int urho_hash_get_P_PARENT ()
{
	return Urho3D::ElementRemoved::P_PARENT.Value ();
}

DllExport int urho_hash_get_P_CLONECOMPONENT ()
{
	return Urho3D::ComponentCloned::P_CLONECOMPONENT.Value ();
}

DllExport int urho_hash_get_P_TIME ()
{
	return Urho3D::AnimationTrigger::P_TIME.Value ();
}

DllExport int urho_hash_get_P_RADIUS ()
{
	return Urho3D::NavigationObstacleRemoved::P_RADIUS.Value ();
}

DllExport int urho_hash_get_P_LOOPED ()
{
	return Urho3D::AnimationFinished::P_LOOPED.Value ();
}

DllExport int urho_hash_get_P_BEGINELEMENT ()
{
	return Urho3D::ClickEnd::P_BEGINELEMENT.Value ();
}

DllExport int urho_hash_get_P_RESOURCE ()
{
	return Urho3D::ResourceBackgroundLoaded::P_RESOURCE.Value ();
}

DllExport int urho_hash_get_P_BODYA ()
{
	return Urho3D::PhysicsEndContact2D::P_BODYA.Value ();
}

DllExport int urho_hash_get_P_CENTERY ()
{
	return Urho3D::MultiGesture::P_CENTERY.Value ();
}

DllExport int urho_hash_get_P_INDEX ()
{
	return Urho3D::InterceptNetworkUpdate::P_INDEX.Value ();
}

DllExport int urho_hash_get_P_MESH ()
{
	return Urho3D::NavigationAreaRebuilt::P_MESH.Value ();
}

DllExport int urho_hash_get_P_TEXT ()
{
	return Urho3D::TextFinished::P_TEXT.Value ();
}

DllExport int urho_hash_get_P_SIZE ()
{
	return Urho3D::CrowdAgentNodeFormation::P_SIZE.Value ();
}

DllExport int urho_hash_get_P_DTHETA ()
{
	return Urho3D::MultiGesture::P_DTHETA.Value ();
}

DllExport int urho_hash_get_P_NODE ()
{
	return Urho3D::NodeCloned::P_NODE.Value ();
}

DllExport int urho_hash_get_P_SURFACE ()
{
	return Urho3D::EndViewRender::P_SURFACE.Value ();
}

DllExport int urho_hash_get_P_SERIALIZABLE ()
{
	return Urho3D::InterceptNetworkUpdate::P_SERIALIZABLE.Value ();
}

DllExport int urho_hash_get_P_OK ()
{
	return Urho3D::MessageACK::P_OK.Value ();
}

DllExport int urho_hash_get_P_DDIST ()
{
	return Urho3D::MultiGesture::P_DDIST.Value ();
}

DllExport int urho_hash_get_P_OTHERBODY ()
{
	return Urho3D::NodeEndContact2D::P_OTHERBODY.Value ();
}

DllExport int urho_hash_get_P_TEXTURE ()
{
	return Urho3D::EndViewRender::P_TEXTURE.Value ();
}

DllExport int urho_hash_get_P_POSITION ()
{
	return Urho3D::NavigationObstacleRemoved::P_POSITION.Value ();
}

DllExport int urho_hash_get_P_EFFECT ()
{
	return Urho3D::ParticleEffectFinished::P_EFFECT.Value ();
}

DllExport int urho_hash_get_P_TRIGGER ()
{
	return Urho3D::NodeCollisionEnd::P_TRIGGER.Value ();
}

DllExport int urho_hash_get_P_DY ()
{
	return Urho3D::DragMove::P_DY.Value ();
}

DllExport int urho_hash_get_P_CONSTANT ()
{
	return Urho3D::UpdateSmoothing::P_CONSTANT.Value ();
}

DllExport int urho_hash_get_P_PROGRESS ()
{
	return Urho3D::AsyncLoadProgress::P_PROGRESS.Value ();
}

DllExport int urho_hash_get_P_TAG ()
{
	return Urho3D::NodeTagRemoved::P_TAG.Value ();
}

DllExport int urho_hash_get_P_BORDERLESS ()
{
	return Urho3D::ScreenMode::P_BORDERLESS.Value ();
}

DllExport int urho_hash_get_P_SCENE ()
{
	return Urho3D::ComponentCloned::P_SCENE.Value ();
}

DllExport int urho_hash_get_P_SELECTION_LENGTH ()
{
	return Urho3D::TextEditing::P_SELECTION_LENGTH.Value ();
}

DllExport int urho_hash_get_P_OFFSET ()
{
	return Urho3D::SliderPaged::P_OFFSET.Value ();
}

DllExport int urho_hash_get_P_PRESSURE ()
{
	return Urho3D::TouchMove::P_PRESSURE.Value ();
}

DllExport int urho_hash_get_P_REPEAT ()
{
	return Urho3D::KeyDown::P_REPEAT.Value ();
}

}
