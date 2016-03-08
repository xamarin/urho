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
			UrhoPlatformInitializer.DefaultInit();
			initTaskSource = new TaskCompletionSource<bool>();
			BackgroundColor = UIColor.Black;
		}

		public UrhoSurface(CGRect frame) : base(frame)
		{
			UrhoPlatformInitializer.DefaultInit();
			BackgroundColor = UIColor.Black;
			initTaskSource = new TaskCompletionSource<bool>();
		}

		public override async void WillMoveToWindow(UIWindow window)
		{
			base.WillMoveToWindow(window);
		}

		public override void MovedToWindow()
		{
			base.MovedToWindow();
			SDL_SetExternalViewPlaceholder(Handle, Window?.Handle ?? IntPtr.Zero);
			initTaskSource.TrySetResult(true);
		}
	}
}