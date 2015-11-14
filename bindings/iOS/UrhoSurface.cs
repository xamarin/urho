using System;
using System.Runtime.InteropServices;
using CoreGraphics;
using UIKit;

namespace Urho.iOS
{
	public class UrhoSurface : UIView
	{
		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		static extern void SDL_SetExternalViewPlaceholder(IntPtr viewPtr, IntPtr windowPtr);

		public UrhoSurface()
		{
			BackgroundColor = UIColor.Black;
		}

		public UrhoSurface(CGRect frame) : base(frame)
		{
			BackgroundColor = UIColor.Black;
		}

		public override void WillMoveToWindow(UIWindow window)
		{
			if (!UrhoEngine.Inited)
			{
				UrhoEngine.Init();
			}
			SDL_SetExternalViewPlaceholder(Handle, window?.Handle ?? IntPtr.Zero);
			base.WillMoveToWindow(window);
		}
	}
}