using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UrhoRunner = System.Func<System.Type, Urho.ApplicationOptions, System.Threading.Tasks.Task<Urho.Application>>;

namespace Urho.Forms
{
	public class UrhoSurface : Xamarin.Forms.View
	{
		readonly TaskCompletionSource<UrhoRunner> runnerInitTaskSource = new TaskCompletionSource<UrhoRunner>();

		internal void RegisterRunner(UrhoRunner runner)
		{
			runnerInitTaskSource.TrySetResult(runner);
		}

		public async Task<TUrhoApplication> Show<TUrhoApplication>(ApplicationOptions options) where TUrhoApplication : Urho.Application
		{
			var runner = await runnerInitTaskSource.Task;
			if (runner == null)
				throw new InvalidOperationException("UrhoRunner should not be null.");
			return (TUrhoApplication)await runner(typeof (TUrhoApplication), options);
		}

#if __IOS__
		public static void OnPause()
		{
			Urho.iOS.UrhoSurface.Pause();
		}

		public static void OnResume()
		{
			Urho.iOS.UrhoSurface.Resume();
		}

		public static void OnDestroy()
		{
			if (Urho.Application.HasCurrent && Urho.Application.Current.IsActive)
				Urho.Application.Current.Exit();
		}
#elif __ANDROID__
		public static void OnPause()
		{
			Urho.Droid.UrhoSurface.OnPause();
		}

		public static void OnResume()
		{
			Urho.Droid.UrhoSurface.OnResume();
		}

		public static void OnDestroy()
		{
			Urho.Droid.UrhoSurface.OnDestroy();
		}
#elif WINDOWS_UWP
		public static void OnPause()
		{
			Urho.UWP.UrhoSurface.Pause();
		}

		public static void OnResume()
		{
			Urho.UWP.UrhoSurface.Resume();
		}

		public static void OnDestroy()
		{
			//OnDestroy for UWP is implemented in UrhoSurface.OnUnloaded
		}
#else
		public static void OnPause()
		{
			throw new InvalidOperationException("Platform implementation is not referenced");
		}

		public static void OnResume()
		{
			throw new InvalidOperationException("Platform implementation is not referenced");
		}

		public static void OnDestroy()
		{
			throw new InvalidOperationException("Platform implementation is not referenced");
		}
#endif
	}
}
