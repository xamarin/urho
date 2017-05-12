using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Urho.Resources
{
	partial class Image
	{
		public byte[] DataBytes
		{
			get
			{
				unsafe
				{
					byte[] data = new byte[Width * Height * Depth * Components];
					Marshal.Copy((IntPtr)Data, data, 0, data.Length);
					return data;
				}
			}
		}


		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		extern static IntPtr Image_SavePNG2(IntPtr handle, out int len);

		public byte[] SavePNG()
		{
			int len;
			var ptr = Image_SavePNG2(Handle, out len);
			byte[] data = new byte[len];
			Marshal.Copy(ptr, data, 0, data.Length);
			UrhoObject.FreeBuffer(ptr);
			return data;
		}
	}
}
