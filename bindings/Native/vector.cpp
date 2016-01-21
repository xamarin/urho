//
// Proxy support for Vector<SharedPtr<FOO>>
//
#include <stdio.h>
#include <string.h>
#ifndef _MSC_VER
#   include <unistd.h>
#endif
#include "AllUrho.h"
#include "glue.h"
using namespace Urho3D;

extern "C" {
	DllExport
	int VectorSharedPtr_Count (Vector<SharedPtr<Object> > *vector)
	{
		return vector->Size ();
	}

	DllExport
	void *VectorSharedPtr_GetIdx (Vector<SharedPtr<Object> > *vector, int idx)
	{
		if (idx < 0 || idx > vector->Size ())
			return NULL;
		
		return (*vector) [idx].Get ();
	}

}
