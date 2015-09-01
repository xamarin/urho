#include <stdio.h>
#ifndef _MSC_VER
#   include <unistd.h>
#endif
#define URHO3D_OPENGL
#include "../AllUrho.h"
#include "ApplicationProxy.h"
#include <Urho3D/Urho3D.h>
#include <Urho3D/Core/ProcessUtils.h>
#include <Urho3D/DebugNew.h>

using namespace Urho3D;
sdl_callback sdlCallback;

DllExport void *
ApplicationProxy_ApplicationProxy (Context *context, callback_t setup, callback_t start, callback_t stop)
{
	return new ApplicationProxy (context, setup, start, stop);
}

DllExport void
RegisterSdlLauncher(sdl_callback callback)
{
	sdlCallback = callback;
}

extern "C" {
DllExport void *
Application_GetEngine (ApplicationProxy *application)
{
	return application->GetEngine ();
}
}

#if defined(ANDROID) || defined(IOS)
// Entry point for SDL (iOS and Android)
int RunApplication()
{
	SharedPtr<Context> context(new Context());
	return sdlCallback(context);// currentApp->Run();
}
DEFINE_MAIN(RunApplication());
#endif