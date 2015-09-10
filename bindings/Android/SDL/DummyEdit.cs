using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;

namespace Urho.Android
{
	public class DummyEdit : View, View.IOnKeyListener
	{
		private SDLInputConnection ic;

		protected DummyEdit(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		public DummyEdit(Context context) : base(context)
		{
			FocusableInTouchMode = true;
			Focusable = true;
			SetOnKeyListener(this);
		}

		public override bool OnCheckIsTextEditor()
		{
			return true;
		}

		public bool OnKey(View v, Keycode keyCode, KeyEvent e)
		{        
			// This handles the hardware keyboard input
			if (e.IsPrintingKey) {
				if (e.Action == KeyEventActions.Down)
				{
					ic.CommitText(Java.Lang.String.ValueOf(e.GetUnicodeChar(0)), 1);
				}
				return true;
			}

			if (e.Action == KeyEventActions.Down)
			{
				SDLActivity.onNativeKeyDown(keyCode);
				return true;
			}
			else if (e.Action == KeyEventActions.Up)
			{
				SDLActivity.onNativeKeyUp(keyCode);
				return true;
			}

			return false;
		}

		public override bool OnKeyPreIme(Keycode keyCode, KeyEvent e)
		{
			// As seen on StackOverflow: http://stackoverflow.com/questions/7634346/keyboard-hide-event
			// FIXME: Discussion at http://bugzilla.libsdl.org/show_bug.cgi?id=1639
			// FIXME: This is not a 100% effective solution to the problem of detecting if the keyboard is showing or not
			// FIXME: A more effective solution would be to change our Layout from AbsoluteLayout to Relative or Linear
			// FIXME: And determine the keyboard presence doing this: http://stackoverflow.com/questions/2150078/how-to-check-visibility-of-software-keyboard-in-android
			// FIXME: An even more effective way would be if Android provided this out of the box, but where would the fun be in that :)
			if (e.Action == KeyEventActions.Up && keyCode == Keycode.Back)
			{
				if (SDLActivity.mTextEdit != null && SDLActivity.mTextEdit.Visibility == ViewStates.Visible)
				{
					SDLActivity.onNativeKeyboardFocusLost();
				}
			}

			return base.OnKeyPreIme(keyCode, e);
		}

		public override IInputConnection OnCreateInputConnection(EditorInfo outAttrs)
		{
			ic = new SDLInputConnection(this, true);
			outAttrs.ImeOptions = ImeFlags.NoExtractUi | (ImeFlags)33554432 /* API 11: EditorInfo.IME_FLAG_NO_FULLSCREEN */;
			return base.OnCreateInputConnection(outAttrs);
		}
	}
}