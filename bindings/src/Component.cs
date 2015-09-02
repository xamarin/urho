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
using System.Runtime.InteropServices;
using System.Reflection;

namespace Urho {
	
	public partial class Component {
		public T GetComponent<T> () where T:Component
		{
			var stringHash = Runtime.LookupStringHash (typeof (T));
			var ptr = Component_GetComponent (handle, stringHash.Code);
			return Runtime.LookupObject<T> (ptr);
		}
	}
}
