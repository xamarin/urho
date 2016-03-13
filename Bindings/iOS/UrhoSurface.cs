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

		TaskCompletionSource<bool> initTaskSource = new TaskCompletionSource<bool>();

		public Task InitializeTask => initTaskSource.Task;

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
		
		public override void MovedToWindow()
		{
			base.MovedToWindow();
			var wndHandle = Window?.Handle;
			SDL_SetExternalViewPlaceholder(Handle, wndHandle ?? IntPtr.Zero);
			if (wndHandle != null) {
				initTaskSource.TrySetResult (true);
			} else {
				initTaskSource = new TaskCompletionSource<bool> ();
			}
		}
	}
}