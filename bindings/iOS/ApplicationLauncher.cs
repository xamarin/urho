using System;
using System.Linq;
using System.Runtime.InteropServices;
using Foundation;
using ObjCRuntime;

namespace Urho.iOS
{
	public static class ApplicationLauncher
	{
		[DllImport("mono-urho")]
		extern static void InitSdl(string resDir, string docDir);

		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		extern static void SDL_SetMainReady();

		public static void Run(Func<Application> appCreator)
		{
			string docs = NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.All, true).FirstOrDefault();
			string resources = NSBundle.MainBundle.ResourcePath;
			Run(appCreator, resources, docs);
		}

		public static void Run(Func<Application> appCreator, string resourcesDir, string docsDir)
		{
			Application.SetCustomApplicationCallback(Setup, Start, Stop);
			InitSdl(resourcesDir, docsDir);
			SDL_SetMainReady();
			NSFileManager.DefaultManager.ChangeCurrentDirectory(resourcesDir);
			appCreator().Run();
		}

		[MonoPInvokeCallback(typeof(Application.ActionIntPtr))]
		private static void Stop(IntPtr value)
		{
			Application.GetApp(value).Stop();
		}

		[MonoPInvokeCallback(typeof(Application.ActionIntPtr))]
		private static void Start(IntPtr value)
		{
			Application.GetApp(value).Start();
		}

		[MonoPInvokeCallback(typeof(Application.ActionIntPtr))]
		private static void Setup(IntPtr value)
		{
			Application.GetApp(value).Setup();
		}
	}
}
