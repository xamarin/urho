#include <stdio.h>
#include <unistd.h>
#define URHO3D_OPENGL
#include "../AllUrho.h"
#include "ApplicationProxy.h"

void *
ApplicationProxy_ApplicationProxy (Urho3D::Context *context, callback_t setup, callback_t start, callback_t stop)
{
	return new ApplicationProxy (context, setup, start, stop);
}

extern "C" {
void *
Application_GetEngine (ApplicationProxy *application)
{
	return application->GetEngine ();
}
}
