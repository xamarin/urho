using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Urho.Droid
{
	public static class UrhoEngine
	{
		public static bool Inited { get; private set; }

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int SdlCallback(IntPtr context);

		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void RegisterSdlLauncher(SdlCallback callback);

		public static void Init()
		{
			if (Inited)
				return;
			Inited = true;

			var assets = Android.App.Application.Context.Assets.List("");
			if (!assets.Contains("CoreData") || !assets.Contains("Data"))
			{
				throw new InvalidOperationException("Assets folder should contain Data and CoreData folders. All assets should have 'AndroidAsset' as a build action.");
			}
		}
	}
}