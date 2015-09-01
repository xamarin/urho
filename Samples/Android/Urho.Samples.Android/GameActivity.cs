using System.Collections.Generic;
using Org.Libsdl.App;

namespace Urho.Samples.Droid
{
	public class GameActivity : SDLActivity
	{
		protected override bool OnLoadLibrary(IList<string> libraryNames)
		{
			return base.OnLoadLibrary(libraryNames);
		}
	}
}