using System;
using System.Runtime.InteropServices;
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
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int SdlCallback(IntPtr context);

		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void RegisterSdlLauncher(SdlCallback callback);

		/// <summary>
		/// Creates a view (SurfaceView) that can be added anywhere
		/// </summary>
		public static SDLSurface CreateSurface<TApplication>(Activity activity, ApplicationOptions options = null) where TApplication : Application
		{
			return CreateSurface(activity, typeof (TApplication), options);
		}

		/// <summary>
		/// Creates a view (SurfaceView) that can be added anywhere
		/// </summary>
		public static SDLSurface CreateSurface(Activity activity, Type applicationType, ApplicationOptions options = null)
		{
			RegisterSdlLauncher(contextPtr => Application.CreateInstance(applicationType, options).Run());
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
			RegisterSdlLauncher(_ => Application.CreateInstance(applicationType, options).Run());
			var context = Android.App.Application.Context;
			var intent = new Intent(context, typeof(Org.Libsdl.App.UrhoActivity));
			intent.AddFlags(ActivityFlags.NewTask);
			context.StartActivity(intent);
		}
	}
}