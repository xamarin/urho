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
		bool subscribedToSceneUpdate = false;

		public T GetComponent<T> () where T:Component
		{
			return (T)Node.Components.FirstOrDefault(c => c is T);
		}

		public Application Application => Application.Current;

		public virtual void OnSerialize(IComponentSerializer serializer) { }

		public virtual void OnDeserialize(IComponentDeserializer deserializer) { }

		public virtual void OnAttachedToNode()
		{
			if (!subscribedToSceneUpdate && GetType().Name != TypeName)
			{
				// GetType().Name != TypeName --- it means we subscribe to Update only for user-defined components
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

		protected virtual void OnSceneUpdate(SceneUpdateEventArgs args) { }
	}
}
