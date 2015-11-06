#include <stdio.h>
#include <string.h>
#ifndef _MSC_VER
#   include <unistd.h>
#endif
#include <stdlib.h>
#include "../AllUrho.h"
#include "glue.h"
#include "../src/interop.h"
#include "SharpComponent.h"
using namespace Urho3D;

//
// This is just an implemention of EventHandler that can be used with function
// pointers, so we can register delegates from C#
//

extern "C" {

	DllExport
	void * urho_map_get_ptr (VariantMap &map, int hash)
	{
		StringHash h (hash);
		return map [h].GetVoidPtr ();
	}

	DllExport
	void * urho_map_get_object(VariantMap &map, int hash, int* objHash)
	{
		StringHash h(hash);
		void* ptr = map[h].GetVoidPtr();
		Object * object = static_cast<Object *>(ptr);
		*objHash = object->GetType().Value(); //GetType is virtual
		return ptr;
	}

	DllExport
	const char * urho_map_get_String (VariantMap& map, int hash)
	{
		StringHash h (hash);
		return strdup(map [h].GetString ().CString());
	}
	
	DllExport
	int urho_map_get_StringHash (VariantMap& map, int hash)
	{
		StringHash h (hash);
		return map [h].GetStringHash ().Value ();
	}
	
	DllExport
	Variant urho_map_get_Variant (VariantMap& map, int hash)
	{
		StringHash h (hash);
		return map [h];
	}
	
	DllExport
	Vector3 urho_map_get_Vector3 (VariantMap& map, int hash)
	{
		StringHash h (hash);
		return map [h].GetVector3 ();
	}

	DllExport
	int urho_map_get_bool (VariantMap& map, int hash)
	{
		StringHash h (hash);
		return map [h].GetBool ();
	}
	
	DllExport
	float urho_map_get_float (VariantMap& map, int hash)
	{
		StringHash h (hash);
		return map [h].GetFloat ();
	}
		
	DllExport
	int urho_map_get_int (VariantMap& map, int hash)
	{
		StringHash h (hash);
		return map [h].GetInt ();
	}
	
	DllExport
	unsigned int urho_map_get_uint (VariantMap& map, int hash)
	{
		StringHash h (hash);
		return map [h].GetUInt ();
	}

	DllExport unsigned char *
	urho_map_get_buffer (VariantMap &map, int hash, unsigned *size)
	{
		StringHash h (hash);
		PODVector<unsigned char> p (map [h].GetBuffer ());
		*size = p.Size();
		unsigned char * result = new unsigned char[p.Size()];
		for (int i = 0; i < p.Size(); i++) {
			result[i] = p[i];
		}
		return result;
	}

	DllExport
	void urho_unsubscribe (NotificationProxy *proxy)
	{
		proxy->Unsub ();
	}

	DllExport void
	UI_LoadLayoutToElement(Urho3D::UI *_target, Urho3D::UIElement *to, Urho3D::ResourceCache *cache, const char * name)
	{		
		SharedPtr<UIElement> layoutRoot = _target->LoadLayout(cache->GetResource<XMLFile>(name));
		to->AddChild(layoutRoot);
	}

	DllExport int
	Scene_LoadXMLFromCache(Urho3D::Scene *_target, Urho3D::ResourceCache *cache, const char * name)
	{
		SharedPtr<File> file = cache->GetFile(name);
		return _target->LoadXML(*file);
	}

	DllExport int
	Scene_LoadXML(Urho3D::Scene *_target, const char * file)
	{
		File loadFile(_target->GetContext(), Urho3D::String(file), FILE_READ);
		return _target->LoadXML(loadFile);
	}
	
	DllExport int
	Scene_SaveXML(Urho3D::Scene *_target, const char * file, const char * indentation)
	{
		File saveFile(_target->GetContext(), Urho3D::String(file), FILE_WRITE);
		return _target->SaveXML(saveFile, Urho3D::String(indentation));
	}
	
	DllExport
	int Network_Connect (Network *net, const char *ptr, short port, Scene *scene)
	{
		String s(ptr);
		
		return net->Connect (s, port, scene) ? 1 : 0;
	}
	
	DllExport
	void *TouchState_GetTouchedElement (TouchState *state)
	{
		return (void *) state->GetTouchedElement ();
	}

	DllExport
	const char *Urho_GetPlatform ()
	{
		return strdup (GetPlatform().CString ());
	}

	DllExport
	unsigned urho_stringhash_from_string (const char *p)
	{
		StringHash foo (p);
		return foo.Value ();
	}
	
	DllExport
	Skeleton *AnimatedModel_GetSkeleton (AnimatedModel *model)
	{
		return &model->GetSkeleton ();
	}

	DllExport
	unsigned Controls_GetButtons (Controls *controls)
	{
		return controls->buttons_;
	}

	DllExport
	void Controls_SetButtons (Controls *controls, unsigned value)
	{
		controls->buttons_ = value;
	}
	
	DllExport
	float Controls_GetYaw (Controls *controls)
	{
		return controls->yaw_;
	}

	DllExport
	void Controls_SetYaw (Controls *controls, float value)
	{
		controls->yaw_ = value;
	}

	DllExport
	float Controls_GetPitch (Controls *controls)
	{
		return controls->pitch_;
	}

	DllExport
	void Controls_SetPitch (Controls *controls, float value)
	{
		controls->pitch_ = value;
	}

	DllExport void
	Controls_Reset (Urho3D::Controls *_target)
	{
		_target->Reset ();
	}
	
	DllExport void
	Controls_Set (Urho3D::Controls *_target, unsigned int buttons, int down)
	{
		_target->Set (buttons, down);
	}
	
	DllExport int
	Controls_IsDown (Urho3D::Controls *_target, unsigned int button)
	{
		return _target->IsDown (button);
	}
	
	DllExport const Controls *
	Connection_GetControls (Connection *conn)
	{
		return &conn->GetControls ();
	}

	DllExport void
	Connection_SetControls (Connection *conn, Controls *ctl)
	{
		conn->SetControls (*ctl);
	}

	DllExport Controls *
	Controls_Create ()
	{
		return new Controls ();
	}

	DllExport void
	Controls_Destroy (Controls *controls)
	{
		delete controls;
	}

	DllExport RayQueryResult *
	Octree_RaycastSingle(Octree *octree, const Urho3D::Ray & ray, const Urho3D::RayQueryLevel & level, float maxDistance, unsigned int flags, unsigned int viewMask, int *count) {
		PODVector<RayQueryResult> results;
		auto size = sizeof(RayQueryResult);
		RayOctreeQuery query(results, ray, RAY_TRIANGLE, maxDistance, flags, viewMask);
		octree->RaycastSingle(query);

		if (results.Size() == 0)
			return NULL;

		RayQueryResult * result = new RayQueryResult[results.Size()];
		*count = results.Size();
		for (int i = 0; i < results.Size(); i++) {
			result[i] = results[i];
		}
		return result;
	}

	DllExport void 
	Console_OpenConsoleWindow()
	{
		OpenConsoleWindow();
	}

	DllExport const char * 
	Console_GetConsoleInput()
	{
		return strdup(GetConsoleInput().CString());
	}

	//
	// returns: null on no matches
	// otherwise, a pointer that should be released with free() that
	// contains a first element (pointer sized) with the number of elements
	// followed by the number of pointers
	//
	DllExport
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

	DllExport Interop::Vector3 *
	urho_navigationmesh_findpath(NavigationMesh * navMesh, const class Urho3D::Vector3 & start, const class Urho3D::Vector3 & end, int *count)
	{
		PODVector<Vector3> dest;
		navMesh->FindPath(dest, start, end);
		if (dest.Size() == 0)
			return NULL;
		*count = dest.Size();
		Interop::Vector3 * results = new Interop::Vector3[dest.Size()];
		for (int i = 0; i < dest.Size(); i++) {
			auto vector = *((Interop::Vector3  *) &(dest[i]));
			results[i] = vector;
		}
		return results;
	}
	
	DllExport int
	SharpComponent_GetType(SharpComponent *_target)
	{
		return (_target->GetType()).Value();
	}

	DllExport int
	SharpComponent_GetBaseType(SharpComponent *_target)
	{
		return (_target->GetBaseType()).Value();
	}

	DllExport const char *
	SharpComponent_GetTypeName(SharpComponent *_target)
	{
		return strdup((_target->GetTypeName()).CString());
	}

	DllExport int
	SharpComponent_GetTypeStatic()
	{
		return (SharpComponent::GetTypeStatic()).Value();
	}

	DllExport const char *
	SharpComponent_GetTypeNameStatic()
	{
		return strdup((SharpComponent::GetTypeNameStatic()).CString());
	}

	DllExport void *
	SharpComponent_SharpComponent(Context * context)
	{
		return WeakPtr<SharpComponent>(new SharpComponent(context));
	}

	DllExport void *
	SharpComponent_SharpComponent0(const char * typeName, Context * context)
	{
		return WeakPtr<SharpComponent>(new SharpComponent(Urho3D::String(typeName), context));
	}

	DllExport void
	SharpComponent_SetManagedState(SharpComponent *_target, const char * state)
	{
		_target->SetManagedState(Urho3D::String(state));
	}

	DllExport const char *
	SharpComponent_GetManagedState(SharpComponent *_target)
	{
		return strdup((_target->GetManagedState()).CString());
	}

	DllExport const char *
	SharpComponent_GetName(SharpComponent *_target)
	{
		return strdup((_target->GetName()).CString());
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

