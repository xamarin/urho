using UIKit;

namespace Urho.Samples.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main(string[] args)
		{
			Urho.Application.RegisterSdlLauncher(() => new _23_Water(new Context()));
			UIApplication.Main(args, null, "AppDelegate");
		}
	}
}