using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using CoreGraphics;
using UIKit;

namespace Urho.iOS
{
	public class UrhoSurface : UIView
	{
		[DllImport("@rpath/Urho.framework/Urho", CallingConvention = CallingConvention.Cdecl)]
		static extern void SDL_SetExternalViewPlaceholder(IntPtr viewPtr, IntPtr windowPtr);

		static TaskCompletionSource<bool> initTaskSource = new TaskCompletionSource<bool>();

		public static Task InitializeTask => initTaskSource.Task;

		public UrhoSurface()
		{
			initTaskSource = new TaskCompletionSource<bool>();
			BackgroundColor = UIColor.Black;
		}

		public UrhoSurface(CGRect frame) : base(frame)
		{
			BackgroundColor = UIColor.Black;
			initTaskSource = new TaskCompletionSource<bool>();
		}

		public override void WillMoveToWindow(UIWindow window)
		{
			UrhoPlatformInitializer.DefaultInit();
			SDL_SetExternalViewPlaceholder(Handle, window?.Handle ?? IntPtr.Zero);
			base.WillMoveToWindow(window);
			initTaskSource.TrySetResult(true);
		}
	}
}