using System;
using System.Runtime.InteropServices;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Views;
using Java.Lang;
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

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void RegisterSdlLauncher(SdlCallback callback);

		/// <summary>
		/// Creates a view (SurfaceView) that can be added anywhere
		/// </summary>
		public static SDLSurface CreateSurface<TApplication>(Activity activity, ApplicationOptions options = null, bool finishActivtiyOnExit = false) where TApplication : Application
		{
			return CreateSurface(activity, typeof (TApplication), options, finishActivtiyOnExit);
		}

		/// <summary>
		/// Creates a view (SurfaceView) that can be added anywhere
		/// </summary>
		public static SDLSurface CreateSurface(Activity activity, Type appType, ApplicationOptions options = null, bool finishActivtiyOnExit = false)
		{
			return CreateSurface(activity, () => Application.CreateInstance(appType, options), finishActivtiyOnExit);
		}

		/// <summary>
		/// Creates a view (SurfaceView) that can be added anywhere
		/// </summary>
		public static SDLSurface CreateSurface(Activity activity, Func<Application> applicationFactory, bool finishActivtiyOnExit = false)
		{
			SetSdlMain(applicationFactory, finishActivtiyOnExit);
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
		public static void RunInActivity(Type appType, ApplicationOptions options = null)
		{
			RunInActivity(() => Application.CreateInstance(appType, options));
		}

		/// <summary>
		/// The simpliest way to launch a game. It opens a special full-screen activity
		/// </summary>
		public static void RunInActivity(Func<Application> applicationFactory)
		{
			SetSdlMain(applicationFactory, true);
			var context = Android.App.Application.Context;
			var intent = new Intent(context, typeof(Org.Libsdl.App.UrhoActivity));
			intent.AddFlags(ActivityFlags.NewTask);
			context.StartActivity(intent);
		}

		static void SetSdlMain(Func<Application> applicationFactory, bool finishActivityOnExit)
		{
			RegisterSdlLauncher(_ => {
					var app = applicationFactory();
					var code = app.Run();
					Log.Warn("URHOSHARP", "App exited: " + code);
					return code;
				});
		}
	}
}