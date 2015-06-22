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
		public T CreateComponent<T> (StringHash type, CreateMode mode = CreateMode.REPLICATED, uint id = 0) where T:Component
		{
			var ptr = Node_CreateComponent (handle, type.Code, mode, id);
			return Runtime.LookupObject<T> (ptr);
		}

		
		public T CreateComponent<T> (CreateMode mode = CreateMode.REPLICATED, uint id = 0) where T:Component
		{
			var stringhash = Runtime.LookupStringHash (typeof (T));
			var ptr = Node_CreateComponent (handle, stringhash.Code, mode, id);
			return Runtime.LookupObject<T> (ptr);
		}

		// Parameters are swapped, so I can use default parameters vs the other method signature
		public Node CreateChild (string name = "", uint id = 0, CreateMode mode = CreateMode.REPLICATED)
		{
			return CreateChild (name, mode, id);
		}				
	}
}
