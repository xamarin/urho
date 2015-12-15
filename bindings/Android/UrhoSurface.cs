using System;
using Android.App;
using Android.Content;
using Android.Views;
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
	/// </summary>
	public static class UrhoSurface
	{
		/// <summary>
		/// Creates a view (SurfaceView) that can be added anywhere
		/// </summary>
		public static SDLSurface CreateSurface<TApplication>(Activity activity) where TApplication : Application
		{
			return CreateSurface(activity, typeof (TApplication));
		}

		/// <summary>
		/// Creates a view (SurfaceView) that can be added anywhere
		/// </summary>
		public static SDLSurface CreateSurface(Activity activity, Type applicationType)
		{
			UrhoEngine.Init();
			UrhoEngine.RegisterSdlLauncher(contextPtr => Application.CreateInstance(applicationType).Run());
			return SDLActivity.CreateSurface(activity);
		}

		public static void OnResume()
		{
			SDLActivity.OnResume();
		}

		public static void OnPause()
		{
			SDLActivity.OnPause();
		}

		public static void OnLowMemory()
		{
			SDLActivity.OnLowMemory();
		}

		public static void OnDestroy()
		{
			SDLActivity.OnDestroy();
		}

		public static bool DispatchKeyEvent(KeyEvent keyEvent)
		{
			return SDLActivity.DispatchKeyEvent(keyEvent);
		}

		public static void OnWindowFocusChanged(bool focus)
		{
			SDLActivity.OnWindowFocusChanged(focus);
		}

		/// <summary>
		/// The simpliest way to launch a game. It opens a special full-screen activity
		/// </summary>
		public static void RunInActivity<TApplication>(ApplicationOptions options = null) where TApplication : Application
		{
			RunInActivity(typeof (TApplication), options);
		}

		/// <summary>
		/// The simpliest way to launch a game. It opens a special full-screen activity
		/// </summary>
		public static void RunInActivity(Type applicationType, ApplicationOptions options = null)
		{
			UrhoEngine.Init();
			UrhoEngine.RegisterSdlLauncher(_ => Application.CreateInstance(applicationType, options).Run());
			var context = Android.App.Application.Context;
			var intent = new Intent(context, typeof(Org.Libsdl.App.UrhoActivity));
			intent.AddFlags(ActivityFlags.NewTask);
			context.StartActivity(intent);
		}
	}
}