using System;
using System.Runtime.InteropServices;

namespace Urho.Audio {
	public partial class BufferedSoundStream {
		public void AddData (byte [] data, int start = 0, int count = -1)
		{
			Runtime.ValidateRefCounted(this);
			if (data == null)
				throw new ArgumentNullException (nameof(data));
			if (start < 0)
				throw new ArgumentException ("start should be positive");
			if (count > 0 && start + count > data.Length)
				throw new ArgumentException ("start and count would go beyond the array");

			unsafe {
				fixed (byte *p = &data[start])
					BufferedSoundStream_AddData (handle, (IntPtr) p, (uint)(count == -1 ? data.Length-start : count));
			}
		}

		public void AddData (short [] data, int start = 0, int count = -1)
		{
			Runtime.ValidateRefCounted(this);
			if (data == null)
				throw new ArgumentNullException (nameof(data));
			if (start < 0)
				throw new ArgumentException ("start should be positive");
			if (count > 0 && start + count > data.Length)
				throw new ArgumentException ("start and count would go beyond the array");

			unsafe {
				fixed (short *p = &data[start])
					BufferedSoundStream_AddData (handle, (IntPtr) p, (uint)(2 * (count == -1 ? data.Length-start : count)));
			}
		}
	}
}
