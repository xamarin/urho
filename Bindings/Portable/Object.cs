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
		[MonoPInvokeCallback(typeof(ObjectCallbackSignature))]
		internal static void ObjectCallback(IntPtr data, int stringHash, IntPtr variantMap)
		{
			GCHandle gch = GCHandle.FromIntPtr(data);
			Action<IntPtr> a = (Action<IntPtr>)gch.Target;
			a(variantMap);
		}

		internal static ObjectCallbackSignature ObjectCallbackInstance = ObjectCallback;

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FreeBuffer(IntPtr buffer);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr urho_subscribe_event(IntPtr target, ObjectCallbackSignature act, IntPtr data, int eventNameHash);

		public Subscription SubscribeToEvent(string eventName, Action<EventDataContainer> handler)
		{
			return SubscribeToEvent(new StringHash(eventName), handler);
		}

		public Subscription SubscribeToEvent(StringHash eventName, Action<EventDataContainer> handler)
		{
			var s = new Subscription(ptr => handler(new EventDataContainer(ptr)));
			s.UnmanagedProxy = urho_subscribe_event(handle, ObjectCallbackInstance, GCHandle.ToIntPtr(s.gch), eventName.Code);
			return s;
		}
	}
}
