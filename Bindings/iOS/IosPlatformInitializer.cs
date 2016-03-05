using System;
using System.Threading;
using System.Threading.Tasks;
using Urho.Forms;
using System.Runtime.InteropServices;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRendererAttribute(typeof(UrhoSurface), typeof(IosSurfaceRenderer))]

namespace Urho.Forms
{
	public class IosSurfaceRenderer : ViewRenderer<UrhoSurface, IosUrhoSurface>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<UrhoSurface> e)
		{
			var surface = new IosUrhoSurface();
			surface.BackgroundColor = UIColor.Red;
			e.NewElement.UrhoApplicationLauncher = surface.Launcher;
			SetNativeControl(surface);
		}
	}

	public class IosUrhoSurface : UIView
	{
		static TaskCompletionSource<Application> applicationTaskSource;
		static readonly SemaphoreSlim launcherSemaphore = new SemaphoreSlim(1);
		static Urho.iOS.UrhoSurface surface;
		static Urho.Application app;

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern void SDL_SendAppEvent(Urho.iOS.SdlEvent sdlEvent);

		internal async Task<Urho.Application> Launcher(Type type, ApplicationOptions options)
		{
			await launcherSemaphore.WaitAsync();
			applicationTaskSource = new TaskCompletionSource<Application>();
			Urho.Application.Started += UrhoApplicationStarted;
			if (surface != null)
			{
				//app.Graphics.Release (false, false);
			}
			else {
				this.Add(surface = new Urho.iOS.UrhoSurface(this.Bounds));
			}
			app = Urho.Application.CreateInstance(type, options);
			app.Run();

			return await applicationTaskSource.Task;
		}

		void UrhoApplicationStarted()
		{
			Urho.Application.Started -= UrhoApplicationStarted;
			applicationTaskSource?.TrySetResult(app);
			launcherSemaphore.Release();
		}
	}

}

