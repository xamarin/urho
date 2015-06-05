using System;
using System.Runtime.InteropServices;

namespace Urho {

	public struct StringHash {
		int code;
		public StringHash (int code)
		{
			this.code = code;
		}
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
			return StringHash (Marshal.ReadInt32 (ptr));
		}
	}
}
