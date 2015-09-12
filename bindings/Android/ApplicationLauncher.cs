using System;
using System.Runtime.InteropServices;
using Android.Content;

namespace Urho.Droid
{
	public static class ApplicationLauncher
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int SdlCallback(IntPtr context);

		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		extern static void RegisterSdlLauncher(SdlCallback callback);

		public static void Run(Func<Application> appCreator)
		{
			RegisterSdlLauncher(_ => appCreator().Run());
			var context = Android.App.Application.Context;
			var intent = new Intent(context, typeof(GameActivity));
			intent.AddFlags(ActivityFlags.NewTask);
			context.StartActivity(intent);
		}
	}
}