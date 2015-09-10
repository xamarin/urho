using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Views;

namespace Urho.Android
{
	public class SDLJoystickHandler
	{
		public bool handleMotionEvent(MotionEvent e) {
			return false;
		}

		public void pollInputDevices()
		{
		}
	}

	public class SDLJoystickHandler_API12 : SDLJoystickHandler
	{
		
	}
}
