//
// Support for bubbling up to C# the virtual methods calls for Setup, Start and Stop in Application
//
// This is done by using an ApplicationProxy in C++ that bubbles up
//
using System;
using System.Runtime.InteropServices;

namespace Urho {
	
	public partial class Application {
		[DllImport ("mono-urho")]
		extern static IntPtr ApplicationProxy_ApplicationProxy (IntPtr contextHandle, Action<IntPtr> setup, Action<IntPtr> start, Action<IntPtr> stop);

		//
		// Supports the simple style with callbacks
		//
		public Application (Context context, Action<IntPtr> setup, Action<IntPtr> start, Action<IntPtr> stop) : base (UrhoObjectFlag.Empty)
		{
			handle = ApplicationProxy_ApplicationProxy (context.Handle, setup, start, stop);
			
		}

		
	}
}
