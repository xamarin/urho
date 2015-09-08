using System.Linq;
using Foundation;
using UIKit;

namespace Urho.Samples.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			string docs = NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.All, true).FirstOrDefault();
			string resources = NSBundle.MainBundle.ResourcePath;

			Urho.Application.InitializeSdl(resources, docs);

			window = new UIWindow(UIScreen.MainScreen.Bounds);
			window.MakeKeyAndVisible();

			NSFileManager.DefaultManager.ChangeCurrentDirectory(NSBundle.MainBundle.ResourcePath);

			//ulike other platforms, Run here will release current thread once app is started
			new _23_Water(new Context()).Run();

			return true;
		}
	}
}