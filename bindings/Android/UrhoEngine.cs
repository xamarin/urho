using System;
using System.Runtime.InteropServices;

namespace Urho.Droid
{
	public static class UrhoEngine
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int SdlCallback(IntPtr context);

		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		internal extern static void RegisterSdlLauncher(SdlCallback callback);

		public static void Init()
		{
			//TODO: check if libmono-urho.so exists
		}
	}
}