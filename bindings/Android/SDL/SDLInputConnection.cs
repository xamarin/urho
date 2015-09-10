using System;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Java.Lang;

namespace Urho.Android
{
	public class SDLInputConnection : BaseInputConnection
	{
		public SDLInputConnection(View targetView, bool fullEditor) : base(targetView, fullEditor)
		{
		}

		public override bool SendKeyEvent(KeyEvent e)
		{
			var keyCode = e.KeyCode;
			if (e.Action == KeyEventActions.Down)
			{
				if (e.IsPrintingKey)
				{
					CommitText(Java.Lang.String.ValueOf(e.GetUnicodeChar(0)), 1);
				}
				SDLActivity.onNativeKeyDown(keyCode);
				return true;
			}
			else if (e.Action == KeyEventActions.Up)
			{
				SDLActivity.onNativeKeyUp(keyCode);
				return true;
			}

			return base.SendKeyEvent(e);
		}

		public override bool DeleteSurroundingText(int beforeLength, int afterLength)
		{
			return base.DeleteSurroundingText(beforeLength, afterLength);
		}

		public override bool SetComposingText(ICharSequence text, int newCursorPosition)
		{
			return base.SetComposingText(text, newCursorPosition);
		}


		public native void nativeCommitText(String text, int newCursorPosition);

		public native void nativeSetComposingText(String text, int newCursorPosition);
	}
}