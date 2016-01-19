using System.Linq;
using System.Runtime.InteropServices;
using Foundation;

namespace Urho.iOS
{
	public static class UrhoEngine
	{
		[DllImport(Consts.NativeImport)]
		static extern void InitSdl(string resDir, string docDir);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern void SDL_SetMainReady();

		public static void Init()
		{
			string docs = NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.All, true).FirstOrDefault();
			string resources = NSBundle.MainBundle.ResourcePath;
			Init(resources, docs);
		}

		public static void Init(string resourcesDir, string docsDir)
		{
			Application.EngineInited = true;
			InitSdl(resourcesDir, docsDir);
			SDL_SetMainReady();
			NSFileManager.DefaultManager.ChangeCurrentDirectory(resourcesDir);
		}
	}
}
