//
// Component C# sugar
//
// Authors:
//   Miguel de Icaza (miguel@xamarin.com)
//
// Copyrigh 2015 Xamarin INc
//

using System.Linq;
using System.Reflection;
using Urho.Resources;

namespace Urho
{
	public partial class Component
	{
		bool subscribedToSceneUpdate;

		protected bool ReceiveSceneUpdates { get; set; }

		public T GetComponent<T> () where T : Component
		{
			Runtime.ValidateRefCounted(this);
			return (T)Node.Components.FirstOrDefault(c => c is T);
		}

		public Application Application => Application.Current;

		public virtual void OnSerialize(IComponentSerializer serializer) { }

		public virtual void OnDeserialize(IComponentDeserializer deserializer) { }

		public virtual void OnAttachedToNode(Node node)
		{
			if (!subscribedToSceneUpdate && ReceiveSceneUpdates)
			{
				subscribedToSceneUpdate = true;
				Application.Update += HandleUpdate;
			}
		}

		protected override void OnDeleted()
		{
			if (subscribedToSceneUpdate)
			{
				Application.Update -= HandleUpdate;
			}
			base.OnDeleted();
		}

		/// <summary>
		/// Make sure you set SubscribeToSceneUpdate property to true in order to receive Update events
		/// </summary>
		protected virtual void OnUpdate(float timeStep) { }

		internal static bool IsDefinedInManagedCode<T>() => typeof(T).GetRuntimeProperty("TypeStatic") == null;

		void HandleUpdate(UpdateEventArgs args)
		{
			OnUpdate(args.TimeStep);
		}
	}
}
