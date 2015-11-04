using System;
using System.Runtime.InteropServices;
using UIKit;

namespace Urho.iOS
{
	public class UrhoSurface : UIView
	{
		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		static extern void SDL_SetExternalViewPlaceholder(IntPtr viewPtr, IntPtr windowPtr);

		public override void WillMoveToWindow(UIWindow window)
		{
			SDL_SetExternalViewPlaceholder(this.Handle, window.Handle);
			base.WillMoveToWindow(window);
		}
	}
}
