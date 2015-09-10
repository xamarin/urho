using System.Linq;
using Foundation;

namespace Urho.iOS
{
	public static class ApplicationLauncher
	{
		public static void Run(Application application)
		{
			string docs = NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.All, true).FirstOrDefault();
			string resources = NSBundle.MainBundle.ResourcePath;
			Run(application, resources, docs);
		}

		public static void Run(Application application, string resourcesDir, string docsDir)
		{
			Application.InitializeSdl(resourcesDir, docsDir);
			NSFileManager.DefaultManager.ChangeCurrentDirectory(resourcesDir);
			application.Run();
		}
	}
}
