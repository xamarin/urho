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
#include "glue.h"

using namespace Urho3D;

sdl_callback sdlCallback;
RefCountedEventCallback refCountedEventCallback;
const char *sdlResourceDir;
const char *sdlDocumentsDir;

extern "C" {
	
	DllExport void *
	ApplicationProxy_ApplicationProxy (Context *context, callback_t setup, callback_t start, callback_t stop, const char* args)
	{
		return new ApplicationProxy (context, setup, start, stop, args);
	}

	DllExport void *
	Application_GetEngine (ApplicationProxy *application)
	{
		return application->GetEngine ();
	}

	DllExport void
	RegisterSdlLauncher(sdl_callback callback)
	{
		sdlCallback = callback;
	}

	DllExport void
	InitSdl(const char *resourceDir, const char *documentsDir)
	{
		sdlResourceDir = resourceDir;
		sdlDocumentsDir = documentsDir;
	}
	
	DllExport void 
	SetRefCountedEventCallback(RefCountedEventCallback callback)
	{
		refCountedEventCallback = callback;
	}

	//see RefCounted.cpp
	void HandleRefCountedEvent(void * ptr, Urho3D::RefCountedEvent rcEvent)
	{
		if (refCountedEventCallback)
			refCountedEventCallback(ptr, rcEvent);
	}

	DllExport void 
	TryDeleteRefCounted(Urho3D::RefCounted *refCounted)
	{
		if (refCounted && refCounted->RefCountPtr() && !refCounted->Refs())
			delete refCounted;
	}
}


//FileSystem.cpp uses these functions as external.
#if defined(IOS)
const char* SDL_IOS_GetResourceDir()
{
	return sdlResourceDir;
}

const char* SDL_IOS_GetDocumentsDir()
{
	return sdlDocumentsDir;
}
#endif

#if defined(ANDROID) || defined(IOS)
// Entry point for SDL (Android)
int RunApplication()
{
	SharedPtr<Context> context(new Context());
	return sdlCallback(context);
}
DEFINE_MAIN(RunApplication());
#endif