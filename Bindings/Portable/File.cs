using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Urho.IO
{
	partial class File
	{
		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern uint File_GetSize(IntPtr handle);

		public uint Size => File_GetSize(Handle);

		public uint Read(byte[] buffer, uint size)
		{
			unsafe
			{
				fixed (byte* b = buffer)
				{
					return Read((IntPtr)b, size);
				}
			}
		}
	}
}
