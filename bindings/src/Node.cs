//
// Node C# sugar
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

	internal partial class NodeHelper {
		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		internal extern static IntPtr urho_node_get_components(IntPtr node, int code, int recursive, out int count);
	}
	
	public partial class Node {
		static Node[] ZeroArray = new Node[0];
			
		public Node[] GetChildrenWithComponent<T> (bool recursive = false) where T: Component
		{
			var stringhash = Runtime.LookupStringHash (typeof (T));
			int count;
			var ptr = NodeHelper.urho_node_get_components (handle, stringhash.Code, recursive ? 1 : 0, out count);
			if (ptr == IntPtr.Zero)
				return ZeroArray;
			
			var res = new Node[count];
			for (int i = 0; i < count; i++){
				var node = Marshal.ReadIntPtr(ptr, i * IntPtr.Size);
				res [i] = Runtime.LookupObject<Node> (node);
			}
			return res;
		}
		
		public T CreateComponent<T> (StringHash type, CreateMode mode = CreateMode.Replicated, uint id = 0) where T:Component
		{
			var ptr = Node_CreateComponent (handle, type.Code, mode, id);
			return Runtime.LookupObject<T> (ptr);
		}

		public void RemoveComponent<T> ()
		{
			var stringHash = Runtime.LookupStringHash (typeof (T));
			RemoveComponent (stringHash);
		}

		public T CreateComponent<T> (CreateMode mode = CreateMode.Replicated, uint id = 0) where T:Component
		{
			var stringhash = Runtime.LookupStringHash (typeof (T));
			var ptr = Node_CreateComponent (handle, stringhash.Code, mode, id);
			return Runtime.LookupObject<T> (ptr);
		}

		public void AddComponent (Component component, uint id = 0)
		{
			AddComponent (component, id, CreateMode.Replicated);
		}
		
		// Parameters are swapped, so I can use default parameters vs the other method signature
		public Node CreateChild (string name = "", uint id = 0, CreateMode mode = CreateMode.Replicated)
		{
			return CreateChild (name, mode, id);
		}

		public T GetComponent<T> (bool recursive = false) where T:Component
		{
			var stringHash = Runtime.LookupStringHash (typeof (T));
			var ptr = Node_GetComponent (handle, stringHash.Code, recursive);
			return Runtime.LookupObject<T> (ptr);
		}
	}
}
