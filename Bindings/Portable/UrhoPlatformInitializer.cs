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

#if NET45
			Desktop.DesktopUrhoInitializer.OnInited();
#elif __IOS__
			iOS.IosUrhoInitializer.OnInited();
#elif __ANDROID__
			Droid.DroidPlatformInitializer.OnInited();
#elif UWP_HOLO
			//Do nothing.
#elif WINDOWS_UWP
			UWP.UwpUrhoInitializer.OnInited();
#elif NETCOREAPP2_1
			NetCore.NetCoreUrhoInitializer.OnInited();
#else
			throw new Exception("Implementation assembly (iOS, Android or Desktop) is not referenced");
#endif
			Initialized = true;
		}
	}
}
