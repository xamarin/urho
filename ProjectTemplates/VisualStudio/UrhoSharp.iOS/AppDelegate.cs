using Foundation;
using UIKit;
using Urho;
using Urho.iOS;

namespace $safeprojectname$
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			UrhoEngine.Init();
			new MyGame(new Context()).Run();
			return true;
		}
	}
}


