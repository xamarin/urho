//
// Node C# sugar
//
// Authors:
//   Miguel de Icaza (miguel@xamarin.com)
//
// Copyrigh 2015 Xamarin INc
//
using System;
using static System.Console;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Urho {
	
	public partial class Node {
		public T CreateComponent<T> (StringHash type, CreateMode mode, uint id) where T:Component
		{
			var ptr = Node_CreateComponent (handle, type.Code, mode, id);
			return Runtime.LookupObject<T> (ptr);
		}
	}
}
