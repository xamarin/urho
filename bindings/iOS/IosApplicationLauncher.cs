using System.Linq;
using Foundation;

namespace Urho.iOS
{
	public static class IosApplicationLauncher
	{
		public static void Run(Application application)
		{
			string docs = NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.All, true).FirstOrDefault();
			string resources = NSBundle.MainBundle.ResourcePath;

			Application.InitializeSdl(resources, docs);
			NSFileManager.DefaultManager.ChangeCurrentDirectory(resources);
			application.Run();
		}
	}
}
