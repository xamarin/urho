using System;
using System.Runtime.InteropServices;
using Foundation;
using UIKit;

namespace EmbeddedFrameworkTestApp
{
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window { get; set; }

		[DllImport("@rpath/Urho.framework/Urho")]
		static extern IntPtr Context_Context();

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			Urho.iOS.UrhoEngine.Init ();
			var contextPtr = Context_Context();

			Window = new UIWindow(UIScreen.MainScreen.Bounds);
			Window.MakeKeyAndVisible();
			return true;
		}

		public override void OnResignActivation(UIApplication application) {}

		public override void DidEnterBackground(UIApplication application) {}

		public override void WillEnterForeground(UIApplication application) {}

		public override void OnActivated(UIApplication application) {}

		public override void WillTerminate(UIApplication application) {}
	}
}


