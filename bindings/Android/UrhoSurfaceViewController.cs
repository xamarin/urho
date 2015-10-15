using System;
using Android.App;
using Org.Libsdl.App;

namespace Urho.Droid
{
	/// <summary>
	/// A controller that provides a SDLSurface that can be used in any activity.
	/// </summary>
	public class UrhoSurfaceViewController : Org.Libsdl.App.SDLActivity
	{
		public static SDLSurface OnCreate(Activity activity, Func<Application> appCreator)
		{
			ApplicationLauncher.RegisterSdlLauncher(_ => appCreator().Run());
			return OnCreate(activity);
		}
	}
}