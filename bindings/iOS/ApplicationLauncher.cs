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
			Runtime.SetCustomNativeDeleteCallback(OnNativeDelete);
			Application.SetCustomApplicationCallback(Setup, Start, Stop);
			UrhoObject.SetCustomObjectCallback(ObjectCallback);
			InitSdl(resourcesDir, docsDir);
			SDL_SetMainReady();
			NSFileManager.DefaultManager.ChangeCurrentDirectory(resourcesDir);
			appCreator().Run();
		}

		[MonoPInvokeCallback(typeof(Application.ActionIntPtr))]
		private static void OnNativeDelete(IntPtr ptr)
		{
			Runtime.OnRefCountedNativeDelete(ptr);
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

		[MonoPInvokeCallback(typeof(ObjectCallbackSignature))]
		static void ObjectCallback(IntPtr data, int stringhash, IntPtr variantMap)
		{
			GCHandle gch = GCHandle.FromIntPtr(data);
			Action<IntPtr> a = (Action<IntPtr>)gch.Target;
			a(variantMap);
		}
	}
}
