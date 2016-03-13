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
		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern void SDL_SendAppEvent(Urho.iOS.SdlEvent sdlEvent);

		public static void OnPause()
		{
			SDL_SendAppEvent(Urho.iOS.SdlEvent.SDL_APP_DIDENTERBACKGROUND);
		}

		public static void OnResume()
		{
			SDL_SendAppEvent(Urho.iOS.SdlEvent.SDL_APP_DIDENTERFOREGROUND);
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
#else
		public static void OnPause()
		{
			throw new InvalidOperationException("Platform implementation is not referenced");
		}

		public static void OnResume()
		{
			throw new InvalidOperationException("Platform implementation is not referenced");
		}
#endif
	}
}
