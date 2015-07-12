//
// Proxy support for Vector<SharedPtr<FOO>>
//
#include <stdio.h>
#include <string.h>
#include <unistd.h>
#define URHO3D_OPENGL
#include "../AllUrho.h"
#include "glue.h"
using namespace Urho3D;

extern "C" {
	int VectorSharedPtr_Count (Vector<SharedPtr<Object> > *vector)
	{
		return vector->Size ();
	}

	void *VectorSharedPtr_GetIdx (Vector<SharedPtr<Object> > *vector, int idx)
	{
		if (idx < 0 || idx > vector->Size ())
			return NULL;
		
		return (*vector) [idx].Get ();
	}

}
