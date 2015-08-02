using System;
using System.Runtime.InteropServices;

namespace Urho {
	public partial class RenderPath {
		[DllImport ("mono-urho")]
		extern static IntPtr RenderPath_Clone (IntPtr handle);
		
		public RenderPath Clone ()
		{
			return Runtime.LookupObject<RenderPath> (RenderPath_Clone (handle));
		}
	}
}
