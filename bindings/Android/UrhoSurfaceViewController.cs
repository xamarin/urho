using System;
using Android.App;
using Android.Content;
using Org.Libsdl.App;

namespace Urho.Droid
{
	/// <summary>
	/// A controller that provides a SDLSurface that can be used in any activity.
	/// Make sure you handle these events in your Activity:
	/// - OnResume
	/// - OnPause
	/// - OnLowMemory
	/// - OnDestroy
	/// - DispatchKeyEvent
	/// - OnWindowFocusChanged
	/// - OnCreate(Activity, Func)
	/// </summary>
	public class UrhoSurfaceViewController : Org.Libsdl.App.SDLActivity //it's called Activity but actually it's not an activity
	{
		/// <summary>
		/// Create a view (SurfaceView) that can be added anywhere
		/// </summary>
		public static SDLSurface OnCreate(Activity activity, Func<Application> appCreator)
		{
			UrhoEngine.RegisterSdlLauncher(_ => appCreator().Run());
			return OnCreate(activity);
		}

		/// <summary>
		/// The simpliest way to launch a game. It opens a special full-screen activity
		/// </summary>
		public static void RunInActivity()
		{
			var context = Android.App.Application.Context;
			var intent = new Intent(context, typeof(Org.Libsdl.App.UrhoActivity));
			intent.AddFlags(ActivityFlags.NewTask);
			context.StartActivity(intent);
		}
	}
}