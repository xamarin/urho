//
// Component C# sugar
//
// Authors:
//   Miguel de Icaza (miguel@xamarin.com)
//
// Copyrigh 2015 Xamarin INc
//

using System.Linq;

namespace Urho {
	
	public partial class Component
	{
		bool subscribedToSceneUpdate;

		protected bool SubscribeToSceneUpdate { get; set; }

		public T GetComponent<T> () where T:Component
		{
			return (T)Node.Components.FirstOrDefault(c => c is T);
		}

		public Application Application => Application.Current;

		public virtual void OnSerialize(IComponentSerializer serializer) { }

		public virtual void OnDeserialize(IComponentDeserializer deserializer) { }

		public virtual void OnAttachedToNode()
		{
			if (!subscribedToSceneUpdate && SubscribeToSceneUpdate)
			{
				subscribedToSceneUpdate = true;
				Application.SceneUpdate += OnSceneUpdate;
			}
		}

		protected override void OnDeleted()
		{
			if (subscribedToSceneUpdate)
			{
				Application.SceneUpdate -= OnSceneUpdate;
			}
			base.OnDeleted();
		}

		/// <summary>
		/// Make sure you set SubscribeToSceneUpdate property to true in order to receive Update events
		/// </summary>
		protected virtual void OnSceneUpdate(SceneUpdateEventArgs args) { }
	}
}
