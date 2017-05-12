using System;
using System.Runtime.InteropServices;

namespace Urho.Resources
{
	partial class Image
	{

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr Image_GetDataBytes(IntPtr handle, out int len);

		public byte[] DataBytes
		{
			get
			{
				int len;
				IntPtr ptr = Image_GetDataBytes(Handle, out len);
				byte[] data = new byte[len];
				Marshal.Copy(ptr, data, 0, data.Length);
				return data;
			}
		}


		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr Image_SavePNG2(IntPtr handle, out int len);

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
