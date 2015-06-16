using System;
using System.Runtime.InteropServices;

namespace Urho {

	[StructLayout(LayoutKind.Sequential)]
	public struct StringHash {
		int code;
		public StringHash (int code)
		{
			this.code = code;
		}

		public int Code => code;
	}

	// Points to a StringHash
	public struct StringHashRef {
		IntPtr ptr;
		public StringHashRef (IntPtr ptr)
		{
			this.ptr = ptr;
		}

		public static implicit operator StringHash (StringHashRef r)
		{
			return new StringHash (Marshal.ReadInt32 (r.ptr));
		}
	}
}
