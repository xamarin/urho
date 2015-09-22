using System;
using System.Runtime.InteropServices;

namespace Urho {
	public partial class RenderPath {
		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		extern static IntPtr RenderPath_Clone (IntPtr handle);
		
		public RenderPath Clone ()
		{
			return Runtime.LookupRefCounted<RenderPath> (RenderPath_Clone (handle));
		}
	}
}
