//
// Component C# sugar
//
// Authors:
//   Miguel de Icaza (miguel@xamarin.com)
//
// Copyrigh 2015 Xamarin INc
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Urho {
	
	public partial class Component
	{
		public T GetComponent<T> () where T:Component
		{
			return (T)Node.Components.FirstOrDefault(c => c is T);
		}

		public Application Application => Application.Current;

		public virtual void OnSerialize(IComponentSerializer serializer) { }

		public virtual void OnDeserialize(IComponentSerializer serializer) { }

		public virtual void OnAttachedToNode() { }
	}
}
