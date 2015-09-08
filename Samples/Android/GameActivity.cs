using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Org.Libsdl.App;

namespace Urho.Samples.Droid
{
	[Activity(ScreenOrientation = ScreenOrientation.Landscape, Theme = "@android:style/Theme.NoTitleBar.Fullscreen", ConfigurationChanges = ConfigChanges.KeyboardHidden | ConfigChanges.Orientation)]
	public class GameActivity : SDLActivity
	{
		protected override bool OnLoadLibrary(IList<string> libraryNames)
		{
			//make sure you have Libs\[ABI]\libmono-urho.so file with ContentType=AndroidNativeLibrary
			return base.OnLoadLibrary(new List<string> { "mono-urho" });
		}
	}
}