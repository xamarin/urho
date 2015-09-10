using Android.Views;

namespace Urho.Android
{
	public class SDLGenericMotionListener_API12 : Java.Lang.Object, View.IOnGenericMotionListener
	{
		public bool OnGenericMotion(View v, MotionEvent e)
		{
			return SDLActivity.handleJoystickMotionEvent(e);
		}
	}
}