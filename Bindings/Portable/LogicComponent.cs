using System;
using System.Runtime.InteropServices;
using Urho.Physics;

namespace Urho
{
	/// <summary>
	/// Helper base class for user-defined game logic components that hooks up to update events and forwards them to virtual functions similar to ScriptInstance class.
	/// </summary>
	public class LogicComponent : Component
	{
		Subscription physicsPreStepSubscription;
		Subscription physicsPostStepSubscription;
		Subscription scenePostUpdateSubscription;

		[Preserve]
		public LogicComponent (IntPtr handle) : base (handle)
		{
		}

		[Preserve]
		public LogicComponent () : this (Application.CurrentContext)
		{
		}

		[DllImport (Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr LogicComponent_LogicComponent (IntPtr context);

		public LogicComponent (Context context) : base (UrhoObjectFlag.Empty)
		{
			Runtime.Validate (typeof(LogicComponent));
			handle = LogicComponent_LogicComponent ((object)context == null ? IntPtr.Zero : context.Handle);
			Runtime.RegisterObject (this);
		}

		public override void OnSceneSet(Scene scene)
		{
			if (scene != null)
			{
				if (ReceiveFixedUpdates)
				{
					var physicsWorld = scene.GetComponent<PhysicsWorld>();
					if (physicsWorld == null)
						throw new InvalidOperationException("Scene must have PhysicsWorld component in order to receive FixedUpdates");
					physicsPreStepSubscription = physicsWorld.SubscribeToPhysicsPreStep(OnFixedUpdate);
				}

				if (ReceiveFixedPostUpdates)
				{
					var physicsWorld = scene.GetComponent<PhysicsWorld>();
					if (physicsWorld == null)
						throw new InvalidOperationException("Scene must have PhysicsWorld component in order to receive FixedUpdates");
					physicsPostStepSubscription = physicsWorld.SubscribeToPhysicsPostStep(OnFixedPostUpdate);
				}

				if (ReceivePostUpdates)
				{
					scenePostUpdateSubscription = scene.SubscribeToScenePostUpdate(OnPostUpdate);
				}
			}
			else
			{
				physicsPreStepSubscription?.Unsubscribe();
				physicsPostStepSubscription?.Unsubscribe();
				scenePostUpdateSubscription?.Unsubscribe();
			}
		}

		protected bool ReceiveFixedUpdates { get; set; }
		protected bool ReceiveFixedPostUpdates { get; set; }
		protected bool ReceivePostUpdates { get; set; }

		protected virtual void OnFixedUpdate(PhysicsPreStepEventArgs e) { }
		protected virtual void OnFixedPostUpdate(PhysicsPostStepEventArgs e) { }
		protected virtual void OnPostUpdate(ScenePostUpdateEventArgs e) { }

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int LogicComponent_GetType(IntPtr handle);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr LogicComponent_GetTypeName(IntPtr handle);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int LogicComponent_GetTypeStatic();

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr LogicComponent_GetTypeNameStatic();

		public override StringHash Type => new StringHash(LogicComponent_GetType(handle));
		public override string TypeName => Marshal.PtrToStringAnsi(LogicComponent_GetTypeName(handle));
		[Preserve]
		public new static StringHash TypeStatic => new StringHash(LogicComponent_GetTypeStatic());
		public new static string TypeNameStatic => Marshal.PtrToStringAnsi(LogicComponent_GetTypeNameStatic());
	}
}
