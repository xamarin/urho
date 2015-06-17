using System;
using System.Runtime.InteropServices;
namespace Urho {
	//
	// Helper functions to return elements from a VariantMap
	//

	internal static class UrhoMap  {
		[DllImport ("mono-urho")]
		extern static IntPtr urho_map_get_ptr (IntPtr handle, int stringHash);
		static public IntPtr get_Buffer (IntPtr handle, int stringHash)
		{
			return urho_map_get_ptr (handle, stringHash);
		}
		
		static public Camera get_Camera (IntPtr handle, int stringHash)
		{
			return new Camera (urho_map_get_ptr (handle, stringHash));
		}
		
		static public Component get_Component (IntPtr handle, int stringHash)
		{
			return new Component (urho_map_get_ptr (handle, stringHash));
		}
		
		static public Connection get_Connection (IntPtr handle, int stringHash)
		{
			return new Connection (urho_map_get_ptr (handle, stringHash));
		}
		
		static public CrowdAgent get_CrowdAgent (IntPtr handle, int stringHash)
		{
			return new CrowdAgent (urho_map_get_ptr (handle, stringHash));
		}
		
		static public IntPtr get_IntPtr (IntPtr handle, int stringHash)
		{
			return urho_map_get_ptr (handle, stringHash);
		}
		
		static public MouseMode get_MouseMode (IntPtr handle, int stringHash)
		{
			return (MouseMode) urho_map_get_int (handle, stringHash);
		}
		
		static public NavigationMesh get_NavigationMesh (IntPtr handle, int stringHash)
		{
			return new NavigationMesh (urho_map_get_ptr (handle, stringHash));
		}
		
		static public Node get_Node (IntPtr handle, int stringHash)
		{
			return new Node (urho_map_get_ptr (handle, stringHash));
		}
		
		static public UrhoObject get_Object (IntPtr handle, int stringHash)
		{
			return new UrhoObject (urho_map_get_ptr (handle, stringHash));
		}
		
		static public Obstacle get_Obstacle (IntPtr handle, int stringHash)
		{
			return new Obstacle (urho_map_get_ptr (handle, stringHash));
		}

		static public PhysicsWorld get_PhysicsWorld (IntPtr handle, int stringHash)
		{
			return new PhysicsWorld (urho_map_get_ptr (handle, stringHash));
		}
		
		static public PhysicsWorld2D get_PhysicsWorld2D (IntPtr handle, int stringHash)
		{
			return new PhysicsWorld2D (urho_map_get_ptr (handle, stringHash));
		}
		
		static public RenderSurface get_RenderSurface (IntPtr handle, int stringHash)
		{
			return new RenderSurface (urho_map_get_ptr (handle, stringHash));
		}
		
		static public Resource get_Resource  (IntPtr handle, int stringHash)
		{
			return new Resource (urho_map_get_ptr (handle, stringHash));
		}
		
		static public RigidBody get_RigidBody (IntPtr handle, int stringHash)
		{
			return new RigidBody (urho_map_get_ptr (handle, stringHash));
		}
		
		static public RigidBody2D get_RigidBody2D (IntPtr handle, int stringHash)
		{
			return new RigidBody2D (urho_map_get_ptr (handle, stringHash));
		}
		
		static public Scene get_Scene (IntPtr handle, int stringHash)
		{
			return new Scene (urho_map_get_ptr (handle, stringHash));
		}
		
		static public Serializable get_Serializable (IntPtr handle, int stringHash)
		{
			throw new Exception ("Not implemented, as we need to figure out serializable mapping");
		}
		
		[DllImport ("mono-urho")]
		extern static String urho_map_get_String (IntPtr handle, int stringHash);
		static public String get_String (IntPtr handle, int stringHash)
		{
			return urho_map_get_String (handle, stringHash);
		}
		
		[DllImport ("mono-urho")]
		extern static int urho_map_get_StringHash (IntPtr handle, int stringHash);
		static public StringHash get_StringHash (IntPtr handle, int stringHash)
		{
			return new StringHash (urho_map_get_StringHash (handle, stringHash));
		}
		
		static public Texture get_Texture (IntPtr handle, int stringHash)
		{
			return new Texture (urho_map_get_ptr (handle, stringHash));
		}
	
		[DllImport ("mono-urho")]
		extern static Variant urho_map_get_Variant (IntPtr handle, int stringHash);
		static public Variant get_Variant (IntPtr handle, int stringHash)
		{
			return urho_map_get_Variant (handle, stringHash);
		}
		
		[DllImport ("mono-urho")]
		extern static Vector3 urho_map_get_Vector3 (IntPtr handle, int stringHash);
		static public Vector3 get_Vector3 (IntPtr handle, int stringHash)
		{
			return urho_map_get_Vector3 (handle, stringHash);
		}
		
		static public View get_View (IntPtr handle, int stringHash)
		{
			return new View (urho_map_get_ptr (handle, stringHash));
		}
		
		static public UIElement get_UIElement (IntPtr handle, int stringHash)
		{
			return new UIElement (urho_map_get_ptr (handle, stringHash));
		}
		
		static public WorkItem get_WorkItem (IntPtr handle, int stringHash)
		{
			return new WorkItem (urho_map_get_ptr (handle, stringHash));
		}
		
		[DllImport ("mono-urho")]
		extern static bool urho_map_get_bool (IntPtr handle, int stringHash);
		static public bool get_bool (IntPtr handle, int stringHash)
		{
			return urho_map_get_bool (handle, stringHash);
		}
		
		[DllImport ("mono-urho")]
		extern static float urho_map_get_float (IntPtr handle, int stringHash);
		static public float get_float (IntPtr handle, int stringHash)
		{
			return urho_map_get_float (handle, stringHash);
		}
		
		[DllImport ("mono-urho")]
		extern static int urho_map_get_int (IntPtr handle, int stringHash);
		static public int get_int (IntPtr handle, int stringHash)
		{
			return urho_map_get_int (handle, stringHash);
		}
		
		[DllImport ("mono-urho")]
		extern static uint urho_map_get_uint (IntPtr handle, int stringHash);
		static public uint get_uint (IntPtr handle, int stringHash)
		{
			return urho_map_get_uint (handle, stringHash);
		}
		
	}
}
