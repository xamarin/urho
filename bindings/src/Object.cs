//
// Urho's Object C# sugar
//
// Authors:
//   Miguel de Icaza (miguel@xamarin.com)
//
// Copyrigh 2015 Xamarin INc
//

using System;
using System.Runtime.InteropServices;

namespace Urho {

	public partial class UrhoObject : RefCounted
	{
		static ObjectCallbackSignature customObjectCallback;

		// Invoked by the subscribe methods
		static ObjectCallbackSignature ObjectCallback => customObjectCallback ?? ObjectCallbackDefault;

		static void ObjectCallbackDefault(IntPtr data, int stringHash, IntPtr variantMap)
		{
			GCHandle gch = GCHandle.FromIntPtr(data);
			Action<IntPtr> a = (Action<IntPtr>)gch.Target;
			a(variantMap);
		}

		public static void SetCustomObjectCallback(ObjectCallbackSignature callback)
		{
			customObjectCallback = callback;
		}
	}
}
