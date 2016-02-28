using System;
using System.Runtime.InteropServices;

namespace Urho
{
	public class Subscription {
		internal GCHandle gch;
		internal IntPtr   UnmanagedProxy;

		internal Subscription (Action<IntPtr> proxy)
		{
			gch = GCHandle.Alloc (proxy);
		}

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		extern static void urho_unsubscribe (IntPtr notificationProxy);
		
		public void Unsubscribe ()
		{
			if (UnmanagedProxy == IntPtr.Zero)
				return;
			
			urho_unsubscribe (UnmanagedProxy);
			UnmanagedProxy = IntPtr.Zero;
			gch.Free ();
		}
	}
}