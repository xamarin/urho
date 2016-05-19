using System;
using System.Runtime.InteropServices;

namespace Urho
{
	public partial class Graphics
	{
		[DllImport("@rpath/Urho.framework/Urho", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr Graphics_GetSdlWindow(IntPtr graphics);

		/// <summary>
		/// Pointer to SDL window
		/// </summary>
		public IntPtr SdlWindow => Graphics_GetSdlWindow(Handle);
	}
}
