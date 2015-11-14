using System.Linq;
using System.Runtime.InteropServices;
using Foundation;

namespace Urho.iOS
{
	public static class UrhoEngine
	{
		internal static bool Inited { get; set; }

		[DllImport("mono-urho")]
		extern static void InitSdl(string resDir, string docDir);

		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		extern static void SDL_SetMainReady();

		public static void Init()
		{
			string docs = NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.All, true).FirstOrDefault();
			string resources = NSBundle.MainBundle.ResourcePath;
			Init(resources, docs);
		}

		public static void Init(string resourcesDir, string docsDir)
		{
			Inited = true;
			InitSdl(resourcesDir, docsDir);
			SDL_SetMainReady();
			NSFileManager.DefaultManager.ChangeCurrentDirectory(resourcesDir);
		}
	}
}
