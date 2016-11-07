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
using Urho.HoloLens;
using Com.Google.Vrtoolkit.Cardboard.Sensors;

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
	public class UrhoSurface : IUrhoSurface
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int SdlCallback(IntPtr context);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void RegisterSdlLauncher(SdlCallback callback);

		static HeadTracker tracker;

		public SDLSurface SdlSurface { get; set; }

		UrhoSurface(SDLSurface sdlSurface)
		{
			SdlSurface = sdlSurface;
		}

		public void Remove()
		{
			var vg = SdlSurface?.Parent as ViewGroup;
			if (SdlSurface != null && vg != null)
			{
				//vg.RemoveView(SdlSurface);
				SdlSurface.Enabled = false;
				SdlSurface.Visibility = ViewStates.Gone;
			}
		}

		public bool IsAlive => SDLActivity.MIsSurfaceReady;

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
			var surface = SDLActivity.CreateSurface(activity);
			SetSdlMain(applicationFactory, finishActivtiyOnExit, surface);
			return surface;
		}

		public static void OnResume()
		{
			SDLActivity.OnResume();
		}

		public static void OnPause()
		{
			tracker?.StopTracking();
			tracker = null;
			SDLActivity.OnPause();
		}

		public static void OnLowMemory()
		{
			SDLActivity.OnLowMemory();
		}

		public static void OnDestroy()
		{
			tracker?.StopTracking();
			tracker = null;
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

		static void SetSdlMain(Func<Application> applicationFactory, bool finishActivityOnExit, SDLSurface surface)
		{
			SDLActivity.FinishActivityOnNativeExit = finishActivityOnExit;
			RegisterSdlLauncher(_ => {
					var app = applicationFactory();
					if (app is HoloApplication)
					{
						tracker = HeadTracker.CreateFromContext(Android.App.Application.Context);
						StartTracker();
					}
					app.UrhoSurface = new UrhoSurface(surface);
					var code = app.Run();
					Log.Warn("URHOSHARP", "App exited: " + code);
					return code;
				});
		}

		static float[] currentHeadView = new float[16];

		static async void StartTracker()
		{
			tracker.StartTracking();
			while (tracker != null)
			{
				await Task.Delay(15);
				if (Application.HasCurrent)
				{
					tracker.GetLastHeadView(currentHeadView, 0);
					Matrix4 m4 = new Matrix4(
						currentHeadView[0],
						currentHeadView[1],
						currentHeadView[2],
						currentHeadView[3],

						currentHeadView[4],
						currentHeadView[5],
						currentHeadView[6],
						currentHeadView[7],

						currentHeadView[8],
						currentHeadView[9],
						currentHeadView[10],
						currentHeadView[11],

						currentHeadView[12],
						currentHeadView[13],
						currentHeadView[14],
						currentHeadView[15]);

					var rot = m4.Rotation;
					//RH -> LH:
					var rotation = new Quaternion(-rot.X, -rot.Y, rot.Z, rot.W);
					((HoloApplication)Application.Current).UpdateStereoView(rotation, new Vector3(currentHeadView[12], currentHeadView[13], -currentHeadView[14]));
				}
			}
		}
	}
}