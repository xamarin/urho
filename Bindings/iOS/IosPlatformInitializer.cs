using System.Linq;
using System.Runtime.InteropServices;
using Foundation;

namespace Urho.iOS
{
	public static class IosUrhoInitializer
	{
		[DllImport(Consts.NativeImport)]
		static extern void InitSdl(string resDir, string docDir);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern void SDL_SetMainReady();

		internal static void OnInited()
		{
			string docsDir = NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.All, true).FirstOrDefault();
			string resourcesDir = NSBundle.MainBundle.ResourcePath;
			InitSdl(resourcesDir, docsDir);
			SDL_SetMainReady();
			NSFileManager.DefaultManager.ChangeCurrentDirectory(resourcesDir);
		}
	}
}
