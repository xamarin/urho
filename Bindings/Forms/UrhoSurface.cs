using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Urho.Forms
{
	public class UrhoSurface : Xamarin.Forms.View
	{
		internal Func<Type, ApplicationOptions, Task<Urho.Application>> UrhoApplicationLauncher { get; set; }

		public async Task<TUrhoApplication> Show<TUrhoApplication>(ApplicationOptions options) where TUrhoApplication : Urho.Application
		{
			if (UrhoApplicationLauncher == null)
				throw new InvalidOperationException("Platform implementation is not referenced");
			return (TUrhoApplication)await UrhoApplicationLauncher(typeof (TUrhoApplication), options);
		}

#if IOS
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
#elif ANDROID
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
