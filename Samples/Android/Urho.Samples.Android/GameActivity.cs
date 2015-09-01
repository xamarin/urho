using System.Collections.Generic;
using Org.Libsdl.App;

namespace Urho.Samples.Droid
{
	public class GameActivity : SDLActivity
	{
		protected override bool OnLoadLibrary(IList<string> libraryNames)
		{
			//make sure you have Libs\[ABI]\libmono-urho.so file with ContentType=AndroidNativeLibrary
			return base.OnLoadLibrary(new List<string> { "mono-urho" });
		}
	}
}