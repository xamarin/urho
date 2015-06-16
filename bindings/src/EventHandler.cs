using System;
using System.Collections.Generic;
using System.Threading;

namespace Urho {
	public partial class EventHandler : IDisposable {
		delegate void HandlerFunctionPtr (IntPtr data, StringHash hash, IntPtr variantMap);
		
		[DllImport ("mono-urho")]
		extern static IntPtr create_notification (IntPtr receiver, HandlerFunctionPtr callback, IntPtr data);

		void Callback (IntPtr data, StringHash hash, IntPtr variantMap)
		{
			GCHandle gch = GCHandle.FromIntPtr(data);
			Action a = (Action) gch.Target;
			a ();
		}
		
		public static IntPtr CreateNotification (Object target, Action a)
		{
			GCHandle gch = GCHandle.Alloc(a);

			return create_notification (target.Handle, Callback, GCHandle.ToIntPtr (gch));
		}
	}
}
