//
// Support for bubbling up to C# the virtual methods calls in LogicComponent
//
// This is done by using an ComponentProxy in C++ that bubbles up
//
using System;
using System.Runtime.InteropServices;

namespace Urho {

	/// <summary>
	///  Update mask used for UserComponent, to determine which methods to invoke
	/// </summary>
	[Flags]
	public enum ComponentEventMask : byte{
		Update = 1,
		PostUpdate = 2,
		FixedUpdate = 4,
		FixedPostUpdate = 8
	}
	
	public partial class UserComponent  {
		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		extern static IntPtr ApplicationProxy_ApplicationProxy (IntPtr contextHandle, Action<IntPtr> setup, Action<IntPtr> start, Action<IntPtr> stop);

		public Application (Context context) : base (UrhoObjectFlag.Empty)
		{
			if (context == null)
				throw new ArgumentNullException ("context");
			handle = ApplicationProxy_ApplicationProxy (context.Handle, ProxySetup, ProxyStart, ProxyStop);
			Runtime.RegisterObject (this);
		}
		
		static UserComponent GetComponent (IntPtr handle)
		{
			return Runtime.LookupObject<UserComponent> (handle);
		}

		static void ProxyOnSetEnabled (IntPtr handle)
		{
			GetComponent (handle).OnSetEnabled ();
		}
		
		static void ProxyStart() (IntPtr handle)
		{
			GetComponent (handle).Start ();
		}
		
		static void ProxyDelayedStart() (IntPtr handle)
		{
			GetComponent (handle).DelayedStart ();
		}
		
		static void ProxyStop() (IntPtr handle)
		{
			GetComponent (handle).Stop ();
		}
		
		static void ProxyUpdate (IntPtr handle, float timeStep)
		{
			GetComponent (handle).Update (timeStep);
		}
		
		static void ProxyPostUpdate (IntPtr handle, float timeStep)
		{
			GetComponent (handle).PostUpdate (timeStep);
		}
		
		static void ProxyFixedUpdate (IntPtr handle, float timeStep)
		{
			GetComponent (handle).FixedUpdate (timeStep);
		}
		
		static void ProxyFixedPostUpdate (IntPtr handle, float timeStep)
		{
			GetComponent (handle).FixedPostUpdate (timeStep);
		}

		static void ProxyOnNodeSet (IntPtr handle, IntPtr node)
		{
			GetComponent (handle).OnNodeSet (Runtime.LookupObject<Node> (node));
		}
		
		/// <summary>Handle enabled/disabled state change. Changes update event subscription.</summary>
		public virtual void OnSetEnabled()
		{
		}
		
		/// <summary>Called when the component is added to a scene node. Other components may not yet exist.</summary>
		public virtual void Start() 
		{
		}

		/// <summary>Called before the first update. At this
		/// point all other components of the node should
		/// exist. Will also be called if update events are
		/// not wanted; in that case the event is immediately
		/// unsubscribed afterward.</summary>
		public virtual void DelayedStart() 
		{
		}
		
		/// <summary>Called when the component is detached
		/// from a scene node, usually on destruction. Note
		/// that you will no longer have access to the node
		/// and scene at that point.</summary>
		public virtual void Stop()
		{
		}
		
		/// <summary>Called on scene update, variable timestep.</summary>
		public virtual void Update(float timeStep)
		{
		}
		
		/// <summary>Called on scene post-update, variable timestep.</summary>
		public virtual void PostUpdate(float timeStep)
		{
		}
		
		/// <summary>Called on physics update, fixed timestep.</summary>
		public virtual void FixedUpdate(float timeStep)
		{
		}
		
		/// <summary>Called on physics post-update, fixed timestep.</summary>
		public virtual void FixedPostUpdate(float timeStep)
		{
		}

		
		public ComponentEventMask ComponentEventMask {
			get {
				return (ComponentEventMask) LogicComponent_GetUpdateEventMask (handle);
			}
			set {
				return LogicComponent_SetUpdateEventMask (handle, (byte) value);
			}
		}

		protected virtual void OnNodeSet (Node node)
		{
		}
		
		public bool DelayedStartCalled {
			get {
				return LogicComponent_IsDelayedStartCalled (handle);
			}
		}
	}
}
