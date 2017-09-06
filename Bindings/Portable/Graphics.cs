using System;
using System.Runtime.InteropServices;

namespace Urho
{
	public partial class Graphics
	{
		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr Graphics_GetSdlWindow(IntPtr graphics);

		/// <summary>
		/// Pointer to SDL window
		/// </summary>
		public IntPtr SdlWindow => Graphics_GetSdlWindow(Handle);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal extern static IntPtr Graphics_GetMultiSampleLevels(IntPtr target, out int count);

		public int[] MultiSampleLevels
		{
			get
			{
				Runtime.ValidateRefCounted(this);
				int count;
				var ptr = Graphics_GetMultiSampleLevels(Handle, out count);
				if (ptr == IntPtr.Zero)
					return new int[0];

				var res = new int[count];
				Marshal.Copy(ptr, res, 0, count);
				return res;
			}
		}
	}
}
