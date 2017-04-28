using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Views;
using Java.Lang;
using Org.Libsdl.App;
using Urho;

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
	public class UrhoSurface
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int SdlCallback(IntPtr context);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void RegisterSdlLauncher(SdlCallback callback);

		public static bool IsAlive => SDLActivity.MIsSurfaceReady;

		/// <summary>
		/// Creates a view (SurfaceView) that can be added anywhere
		/// </summary>
		public static SDLSurface CreateSurface(Activity activity) => SDLActivity.CreateSurface(activity);

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
			SetSdlMain(applicationFactory, true, null);
			var context = Android.App.Application.Context;
			var intent = new Intent(context, typeof(Org.Libsdl.App.UrhoActivity));
			intent.AddFlags(ActivityFlags.NewTask);
			context.StartActivity(intent);
		}

		internal static void SetSdlMain(Func<Application> applicationFactory, bool finishActivityOnExit, SDLSurface surface)
		{
			SDLActivity.FinishActivityOnNativeExit = finishActivityOnExit;
			RegisterSdlLauncher(_ => {
					var app = applicationFactory();
					app.UrhoSurface = surface;
					var code = app.Run();
					Log.Warn("URHOSHARP", "App exited: " + code);
					return code;
				});
		}
	}

	public static class SDLSurfaceExtensions
	{
		public static SemaphoreSlim semaphore = new SemaphoreSlim(1);

		public static async Task<Application> Show(this SDLSurface surface, Type appType, ApplicationOptions options = null, bool finishActivityOnExit = false)
		{
			//await semaphore.WaitAsync();
			await Stop(surface);
			var tcs = new TaskCompletionSource<Application>();
			Action startedHandler = null;
			startedHandler = () =>
				{
					Application.Started -= startedHandler;
					tcs.TrySetResult(Application.Current);
					//semaphore.Release();
				};

			Application.Started += startedHandler;
			UrhoSurface.SetSdlMain(() => Application.CreateInstance(appType, options), finishActivityOnExit, surface);
			return await tcs.Task;
		}

		public static async Task<TApplication> Show<TApplication>(this SDLSurface surface, ApplicationOptions options = null, bool finishActivityOnExit = false)
			where TApplication : Application
		{
			return (TApplication) await Show(surface, typeof(TApplication), options, finishActivityOnExit);
		}

		public static async Task Stop(this SDLSurface surface)
		{
			if (Application.HasCurrent && Application.Current.IsActive)
				await Application.Current.Exit();
		}

		public static void Remove(this SDLSurface surface)
		{
			var vg = surface?.Parent as ViewGroup;
			if (surface != null && vg != null)
			{
				//vg.RemoveView(SdlSurface);
				surface.Enabled = false;
				surface.Visibility = ViewStates.Gone;
			}
		}
	}
}