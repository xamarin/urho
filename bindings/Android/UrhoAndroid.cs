using System;
using Android.App;
using Org.Libsdl.App;

namespace Urho.Droid
{
	public class UrhoAndroid : Org.Libsdl.App.SDLActivity
	{
		public static SDLSurface OnCreate(Activity activity, Func<Application> appCreator)
		{
			ApplicationLauncher.RegisterSdlLauncher(_ => appCreator().Run());
			return OnCreate(activity);
		}
	}
}