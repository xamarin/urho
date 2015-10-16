using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Urho.iOS
{
	public class UrhoController
	{
		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		private extern static int SDL_SendAppEvent(SdlEvent sdlEvent);


		public static void HandleWillTerminate()
		{
			SDL_SendAppEvent(SdlEvent.SDL_APP_TERMINATING);
		}

		public static void HandleWillReceiveMemoryWarning()
		{
			SDL_SendAppEvent(SdlEvent.SDL_APP_LOWMEMORY);
		}

		public static void HandleWillResignActive()
		{

		}

		public static void HandleDidBecomeActive()
		{
			SDL_SendAppEvent(SdlEvent.SDL_APP_DIDENTERFOREGROUND);
		}

		public static void HandleDidEnterBackground()
		{
			SDL_SendAppEvent(SdlEvent.SDL_APP_DIDENTERBACKGROUND);
		}

		public static void HandleWillEnterForeground()
		{
			SDL_SendAppEvent(SdlEvent.SDL_APP_WILLENTERBACKGROUND);
		}
	}
}
