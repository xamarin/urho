using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urho
{
	class UrhoPlatformInitializer
	{
		internal static bool Initialized { get; set; }

		internal static void DefaultInit()
		{
			if (Initialized)
				return;

#if DESKTOP
			Desktop.DesktopUrhoInitializer.OnInited();
#elif IOS
			iOS.IosUrhoInitializer.OnInited();
#elif ANDROID
			Droid.DroidPlatformInitializer.OnInited();
#elif WINDOWS_UWP
			UWP.UwpUrhoInitializer.OnInited();
#else
			throw new Exception("Implementation assembly (iOS, Android or Desktop) is not referenced");
#endif
			Initialized = true;
		}
	}
}
