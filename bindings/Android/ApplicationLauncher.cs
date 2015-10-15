using System;
using System.Runtime.InteropServices;
using Android.Content;
using Org.Libsdl.App;

namespace Urho.Droid
{
	public static class ApplicationLauncher
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int SdlCallback(IntPtr context);

		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		internal extern static void RegisterSdlLauncher(SdlCallback callback);

		public static void RunInActivity(Func<Application> appCreator)
		{
			RegisterSdlLauncher(_ => appCreator().Run());
			var context = Android.App.Application.Context;
			var intent = new Intent(context, typeof(UrhoSurfaceViewController));
			intent.AddFlags(ActivityFlags.NewTask);
			context.StartActivity(intent);
		}
	}
}