using System;
using System.Runtime.InteropServices;
using Urho.Audio;
using Urho.Navigation;
using Urho.Physics;
using Urho.Urho2D;
using Urho.Resources;
using Urho.Gui;
using Urho.Network;

namespace Urho {

	/// <summary>
	/// Helper functions to return elements from a VariantMap
	/// </summary>
	public class EventDataContainer
	{
		public IntPtr Handle { get; }

		public EventDataContainer(IntPtr handle)
		{
			Handle = handle;
		}
		
		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		static extern IntPtr urho_map_get_ptr (IntPtr handle, int paramNameHash);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr urho_map_get_buffer(IntPtr handle, int paramNameHash, out int size);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr urho_map_get_String(IntPtr handle, int paramNameHash);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern int urho_map_get_StringHash(IntPtr handle, int paramNameHash);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern bool urho_map_get_bool(IntPtr handle, int paramNameHash);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern int urho_map_get_int(IntPtr handle, int paramNameHash);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern float urho_map_get_float(IntPtr handle, int paramNameHash);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern uint urho_map_get_uint(IntPtr handle, int paramNameHash);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern Vector3 urho_map_get_Vector3(IntPtr handle, int paramNameHash);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern IntVector2 urho_map_get_IntVector2(IntPtr handle, int paramNameHash);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern Variant urho_map_get_Variant(IntPtr handle, int paramNameHash);

		public T get_Object<T>(int paramNameHash) where T : UrhoObject
		{
			return Runtime.LookupObject<T>(urho_map_get_ptr(Handle, paramNameHash));
		}

		public T get_Object<T>(string paramName) where T : UrhoObject
		{
			return Runtime.LookupObject<T>(urho_map_get_ptr(Handle, new StringHash(paramName).Code));
		}

		public SoundSource get_SoundSource(int paramNameHash)
		{
			return Runtime.LookupObject<SoundSource>(urho_map_get_ptr(Handle, paramNameHash));
		}

		public Sound get_Sound(int paramNameHash)
		{
			return Runtime.LookupObject<Sound>(urho_map_get_ptr(Handle, paramNameHash));
		}

		public Animation get_Animation(int paramNameHash)
		{
			return Runtime.LookupObject<Animation>(urho_map_get_ptr(Handle, paramNameHash));
		}

		public ParticleEffect get_ParticleEffect(int paramNameHash)
		{
			return Runtime.LookupObject<ParticleEffect>(urho_map_get_ptr(Handle, paramNameHash));
		}

		public Camera get_Camera (int paramNameHash)
		{
			return Runtime.LookupObject<Camera>(urho_map_get_ptr(Handle, paramNameHash));
		}
		
		public Component get_Component (int paramNameHash)
		{
			return Runtime.LookupObject<Component>(urho_map_get_ptr(Handle, paramNameHash));
		}
		
		public Connection get_Connection (int paramNameHash)
		{
			return Runtime.LookupObject<Connection>(urho_map_get_ptr(Handle, paramNameHash));
		}
		
		public CrowdAgent get_CrowdAgent (int paramNameHash)
		{
			return Runtime.LookupObject<CrowdAgent>(urho_map_get_ptr(Handle, paramNameHash));
		}
		
		public IntPtr get_IntPtr (int paramNameHash)
		{
			return urho_map_get_ptr (Handle, paramNameHash);
		}
		
		public MouseMode get_MouseMode (int paramNameHash)
		{
			return (MouseMode) urho_map_get_int (Handle, paramNameHash);
		}
		
		public NavigationMesh get_NavigationMesh (int paramNameHash)
		{
			return Runtime.LookupObject<NavigationMesh>(urho_map_get_ptr(Handle, paramNameHash));
		}
		
		public Node get_Node (int paramNameHash)
		{
			return Runtime.LookupObject<Node>(urho_map_get_ptr(Handle, paramNameHash));
		}
		
		public UrhoObject get_Object (int paramNameHash)
		{
			return Runtime.LookupObject<UrhoObject>(urho_map_get_ptr(Handle, paramNameHash));
		}
		
		public Obstacle get_Obstacle (int paramNameHash)
		{
			return Runtime.LookupObject<Obstacle>(urho_map_get_ptr(Handle, paramNameHash));
		}

		public PhysicsWorld get_PhysicsWorld (int paramNameHash)
		{
			return Runtime.LookupObject<PhysicsWorld>(urho_map_get_ptr(Handle, paramNameHash));
		}
		
		public PhysicsWorld2D get_PhysicsWorld2D (int paramNameHash)
		{
			return Runtime.LookupObject<PhysicsWorld2D>(urho_map_get_ptr(Handle, paramNameHash));
		}
		
		public RenderSurface get_RenderSurface (int paramNameHash)
		{
			var ptr = urho_map_get_ptr(Handle, paramNameHash);
			return ptr == IntPtr.Zero ? null : new RenderSurface(ptr);
		}
		
		public Resource get_Resource  (int paramNameHash)
		{
			return Runtime.LookupObject<Resource>(urho_map_get_ptr(Handle, paramNameHash));
		}
		
		public RigidBody get_RigidBody (int paramNameHash)
		{
			return Runtime.LookupObject<RigidBody>(urho_map_get_ptr(Handle, paramNameHash));
		}
		
		public RigidBody2D get_RigidBody2D (int paramNameHash)
		{
			return Runtime.LookupObject<RigidBody2D>(urho_map_get_ptr(Handle, paramNameHash));
		}
		
		public Scene get_Scene (int paramNameHash)
		{
			return Runtime.LookupObject<Scene>(urho_map_get_ptr(Handle, paramNameHash));
		}

		public CollisionShape2D get_CollisionShape2D(int paramNameHash)
		{
			return Runtime.LookupObject<CollisionShape2D>(urho_map_get_ptr(Handle, paramNameHash));
		}

		public ParticleEffect2D get_ParticleEffect2D(int paramNameHash)
		{
			return Runtime.LookupObject<ParticleEffect2D>(urho_map_get_ptr(Handle, paramNameHash));
		}
		
		public Serializable get_Serializable (int paramNameHash)
		{
			throw new Exception ("Not implemented, as we need to figure out serializable mapping");
		}

		public CollisionData [] get_CollisionData (int paramNameHash)
		{
			int size;
			var buffer = urho_map_get_buffer (Handle, paramNameHash, out size);
			return CollisionData.FromContactData (buffer, size);
		}

		public byte[] get_Buffer(int paramNameHash)
		{
			int size;
			var buffer = urho_map_get_buffer(Handle, paramNameHash, out size);
			var bytes = new byte[size];
			Marshal.Copy(buffer, bytes, 0, size);
			return bytes;
		}

		public string get_String (int paramNameHash)
		{
			return Marshal.PtrToStringAnsi(urho_map_get_String (Handle, paramNameHash));
		}
		
		public StringHash get_StringHash (int paramNameHash)
		{
			return new StringHash (urho_map_get_StringHash (Handle, paramNameHash));
		}
		
		public Texture get_Texture (int paramNameHash)
		{
			return Runtime.LookupObject<Texture>(urho_map_get_ptr(Handle, paramNameHash));
		}
	
		public Variant get_Variant (int paramNameHash)
		{
			return urho_map_get_Variant (Handle, paramNameHash);
		}

		public Vector3 get_Vector3 (int paramNameHash)
		{
			return urho_map_get_Vector3 (Handle, paramNameHash);
		}

		public IntVector2 get_IntVector2(int paramNameHash)
		{
			return urho_map_get_IntVector2(Handle, paramNameHash);
		}
		
		public View get_View (int paramNameHash)
		{
			var ptr = urho_map_get_ptr(Handle, paramNameHash);
			return ptr == IntPtr.Zero ? null : new View(ptr);
		}
		
		public UIElement get_UIElement (int paramNameHash)
		{
			return Runtime.LookupObject<UIElement>(urho_map_get_ptr(Handle, paramNameHash));
		}

		public WorkItem get_WorkItem (int paramNameHash)
		{
			return new WorkItem (urho_map_get_ptr (Handle, paramNameHash));
		}

		public bool get_bool (int paramNameHash)
		{
			return urho_map_get_bool (Handle, paramNameHash);
		}

		public float get_float (int paramNameHash)
		{
			return urho_map_get_float (Handle, paramNameHash);
		}
		
		public int get_int (int paramNameHash)
		{
			return urho_map_get_int (Handle, paramNameHash);
		}

		public uint get_uint (int paramNameHash)
		{
			return urho_map_get_uint (Handle, paramNameHash);
		}
	}
}
