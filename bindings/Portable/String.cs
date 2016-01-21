using System;
using System.Runtime.InteropServices;
namespace Urho {
	[StructLayout(LayoutKind.Sequential)]
	public partial struct String {
		uint length;
		uint capacity;
		IntPtr buffer;
	}

	public partial struct StringPtr {
		public IntPtr ptr;

		public static implicit operator String (StringPtr stringPtr)
		{
			unsafe {
				return *((String *) (stringPtr.ptr));
			}
		}
	}
}
