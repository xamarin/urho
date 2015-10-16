using System;
using System.Runtime.InteropServices;
using Android.Content;
using Org.Libsdl.App;

namespace Urho.Droid
{
	public static class UrhoEngine
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int SdlCallback(IntPtr context);

		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		internal extern static void RegisterSdlLauncher(SdlCallback callback);


		public static void Init(Func<Application> appCreator)
		{
			RegisterSdlLauncher(_ => appCreator().Run());
		}

		public static void Init<TApplication>() where TApplication : Urho.Application
		{
			RegisterSdlLauncher(_ => ((Application)Activator.CreateInstance(typeof(TApplication), new Context())).Run());
		}
	}
}