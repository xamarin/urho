using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using CoreGraphics;
using UIKit;
using Foundation;
using System.ComponentModel;
using System.Threading;

namespace Urho.iOS
{
	[Register("UrhoSurface"), DesignTimeVisible(true)]
	public class UrhoSurface : UIView
	{
		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern void SDL_SetExternalViewPlaceholder(IntPtr viewPtr, IntPtr windowPtr);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern void UIKit_StopRenderLoop(IntPtr window);

		static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1);
		static readonly SemaphoreSlim ExitSemaphore = new SemaphoreSlim(1);
		TaskCompletionSource<bool> waitWhileSuperviewIsNullTaskSource = new TaskCompletionSource<bool>();
		bool paused;

		public UrhoSurface() { }
		public UrhoSurface(IntPtr handle) : base(handle) { }

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();
		}

		public UrhoSurface(CGRect frame) : base(frame)
		{
			UrhoPlatformInitializer.DefaultInit();
		}

		public static void Pause()
		{
			if (!Application.HasCurrent) return;

			Application.Current.Input.Enabled = false;
			Application.Current.Engine.PauseMinimized = true;
			Sdl.SendWindowEvent(SdlWindowEvent.SDL_WINDOWEVENT_FOCUS_LOST);
			Sdl.SendWindowEvent(SdlWindowEvent.SDL_WINDOWEVENT_MINIMIZED);
			Urho.Application.HandlePause();
		}

		public static void Resume()
		{
			if (!Application.HasCurrent) return;
			Application.Current.Input.Enabled = true;
			Application.Current.Engine.PauseMinimized = false;
			Sdl.SendWindowEvent(SdlWindowEvent.SDL_WINDOWEVENT_FOCUS_GAINED);
			Sdl.SendWindowEvent(SdlWindowEvent.SDL_WINDOWEVENT_RESTORED);
			Urho.Application.HandleResume();
		}

		public Application Application { get; private set; }

		public bool Paused
		{
			get { return paused; }
			set
			{
				if (!value)
					Resume();
				else
					Pause();
				paused = value;
			}
		}

		public async Task<TApplication> Show<TApplication>(ApplicationOptions opts = null) where TApplication : Application
		{
			return (TApplication)(await Show(typeof(TApplication), opts));
		}

		public async Task<Application> Show(Type appType, ApplicationOptions opts = null)
		{
			LogSharp.Debug("UrhoSurface: Show.");
			await waitWhileSuperviewIsNullTaskSource.Task;
			UrhoPlatformInitializer.DefaultInit();
			await Task.Yield();
			paused = false;
			opts = opts ?? new ApplicationOptions();
			LogSharp.Debug("UrhoSurface: Show. Wait semaphore.");
			await Semaphore.WaitAsync();
			//await Stop();
			if (Application.HasCurrent)
				await Application.Current.Exit();
			await Task.Yield();

			SDL_SetExternalViewPlaceholder(Handle, Window.Handle);

			Hidden = true;
			var app = Application.CreateInstance(appType, opts);
			Application = app;
			app.Run();
			Semaphore.Release();

			Urho.Application.CurrentSurface = new WeakReference(this);
			Urho.Application.CurrentWindow = new WeakReference(Window);

			if (!opts.DelayedStart)
				await Application.ToMainThreadAsync();
           	InvokeOnMainThread(() => Hidden = false);
			LogSharp.Debug("UrhoSurface: Finished.");
			return app;
		}

		public static void StopRendering(Application app)
		{
			LogSharp.Debug("UrhoSurface: StopRendering.");
            Resume();
			if (Application.HasCurrent && Application.Current.Graphics?.IsDeleted != true)
			{
				var window = Application.Current.Graphics.SdlWindow;
				if (window != IntPtr.Zero)
				{
					LogSharp.Debug("UrhoSurface: UIKit_StopRenderLoop.");
					UIKit_StopRenderLoop(window);
				}
			}
			LogSharp.Debug("UrhoSurface: Finished.");
		}

		public async Task Stop()
		{
			LogSharp.Debug("UrhoSurface: Stop.");
			if (Application == null || !Application.IsActive)
				return;

			LogSharp.Debug("UrhoSurface: Stop. Wait semaphore.");
			await ExitSemaphore.WaitAsync();
			LogSharp.Debug("UrhoSurface: Stop. Exiting...");
			await Application.Exit();
			LogSharp.Debug("UrhoSurface: Stop. Removing subviews...");
			foreach (var view in Subviews)
			{
				view.RemoveFromSuperview();
			}
			ExitSemaphore.Release();
			LogSharp.Debug("UrhoSurface: Stop. Finished.");
		}

		public override void RemoveFromSuperview()
		{
			base.RemoveFromSuperview();
			waitWhileSuperviewIsNullTaskSource = new TaskCompletionSource<bool>();
		}

		public override async void WillMoveToWindow(UIWindow window)
		{
			base.WillMoveToWindow(window);
			await Task.Yield();
			waitWhileSuperviewIsNullTaskSource.TrySetResult(true);
		}
		
	}
}