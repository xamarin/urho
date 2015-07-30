#include <stdio.h>
#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#define URHO3D_OPENGL
#include "../AllUrho.h"
#include "glue.h"
using namespace Urho3D;

//
// This is just an implemention of EventHandler that can be used with function
// pointers, so we can register delegates from C#
//

extern "C" {

	void * urho_map_urho_map_get_ptr (VariantMap &map, int hash)
	{
		StringHash h (hash);
		return map [h].GetVoidPtr ();
	}
	
	Urho3D::String urho_map_get_String (VariantMap& map, int hash)
	{
		StringHash h (hash);
		return map [h].GetString ();
	}
	
	int urho_map_get_StringHash (VariantMap& map, int hash)
	{
		StringHash h (hash);
		return map [h].GetStringHash ().Value ();
	}
	
	Variant urho_map_get_Variant (VariantMap& map, int hash)
	{
		StringHash h (hash);
		return map [h];
	}
	
	Vector3 urho_map_get_Vector3 (VariantMap& map, int hash)
	{
		StringHash h (hash);
		return map [h].GetVector3 ();
	}

	bool urho_map_get_bool (VariantMap& map, int hash)
	{
		StringHash h (hash);
		return map [h].GetBool ();
	}
	
	float urho_map_get_float (VariantMap& map, int hash)
	{
		StringHash h (hash);
		return map [h].GetFloat ();
	}
		
	int urho_map_get_int (VariantMap& map, int hash)
	{
		StringHash h (hash);
		return map [h].GetInt ();
	}
	
	uint urho_map_get_uint (VariantMap& map, int hash)
	{
		StringHash h (hash);
		return map [h].GetUInt ();
	}

	void urho_map_get_buffer (VariantMap &map, int hash, void **buffer, unsigned *size)
	{
		StringHash h (hash);
		PODVector<unsigned char> p (map [h].GetBuffer ());
		*size  = p.Size ();
		*buffer = (void *) &p.Front();
	}

	int Network_Connect (Network *net, const char *ptr, short port, Scene *scene)
	{
		String s(ptr);
		
		return net->Connect (s, port, scene) ? 1 : 0;
	}
	
	void *TouchState_GetTouchedElement (TouchState *state)
	{
		return (void *) state->GetTouchedElement ();
	}

	const char *Urho_GetPlatform ()
	{
		return strdup (GetPlatform().CString ());
	}

	unsigned urho_stringhash_from_string (const char *p)
	{
		StringHash foo (p);
		return foo.Value ();
	}
	
	Skeleton *AnimatedModel_GetSkeleton (AnimatedModel *model)
	{
		return &model->GetSkeleton ();
	}

	//
	// returns: null on no matches
	// otherwise, a pointer that should be released with free() that
	// contains a first element (pointer sized) with the number of elements
	// followed by the number of pointers
	//
	void *urho_node_get_components (Node *node, int code, int recursive, int *count)
	{
		PODVector<Node*> dest;
		node->GetChildrenWithComponent (dest, StringHash(code), recursive);
		if (dest.Size () == 0)
			return NULL;
		*count = dest.Size ();
		void **t = (void **) malloc (sizeof(Node*)*dest.Size());
		for (int i = 0; i < dest.Size (); i++){
			t [i] = dest [i];
		}
		return t;
	}
	
void *
create_notification (Object *receiver, HandlerFunctionPtr callback, void *data)
{
	return (void *) new NotificationProxy (receiver, callback, data);
}

#if false

	// Stuff to check interop
void check1 (Context *app)
{

	ResourceCache* cache = app->GetSubsystem<ResourceCache>();
	Texture2D* logoTexture = cache->GetResource<Texture2D>("Textures/LogoLarge.png");
	printf ("LOGOTEXTURE %p\n", logoTexture);
    
        auto x = app->GetSubsystems ();
        for (auto i = x.Begin(); i != x.End(); ++i){
                printf ("got %s\n", i->second_.Get ()->GetTypeName().CString ());
        }
        void *g = app->GetSubsystem<Graphics>();
        printf ("GGGG->%p\n", g);
}

	class Vector3i {
	public:
		Vector3i(int a, int b, int c):x(a),y(b),z(c) {}
		int x, y, z;
	
	};

	struct Vector2i {
	public:
		int a, b;
		Vector2i (int a1, int b1):a(a1),b(b1) {}
	};
		
	Vector3i a (0xdeadbeef,0xcafebabe,0xfeed8001);
	Vector3i &Readl_getVector3 ()
	{
		return a;
	}
	
	Vector3i getVector3 ()
	{
		return Readl_getVector3 ();
	}
	
	void check2 (Vector3& vec)
	{
		printf ("Got %g %g %g\n", vec.x_, vec.y_, vec.z_);
	}
	
	Urho3D::IntVector2
	Input_GetMousePosition (Input *_target);

	Vector2i Foo1 ()
	{
		return Vector2i(0xb00bf00d, 0xfeedc0de);
	}

	Urho3D::IntVector2 Foo2 ()
	{
		return Urho3D::IntVector2(0xdeadbeef,0xdecafbab);
	}
	
	Vector2i Test2 (Input *inp)
	{
		return *((Vector2i *)&inp->GetMouseMove ());
	}
	
//void check2 (ResourceCache *rc)
//{
//        auto x = rc->GetSubsystems ();
//        for (auto i = x.Begin(); i != x.End(); ++i){
//                printf ("got %s\n", i->second_.Get ()->GetTypeName().CString ());
//        }
//        void *g = rc->GetSubsystem<Graphics>();
//        printf ("GGGG->%x\n", g);
//}
#endif
	
}

