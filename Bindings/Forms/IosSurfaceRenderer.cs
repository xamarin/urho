using System;
using System.Threading;
using System.Threading.Tasks;
using Urho.Forms;
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

		internal async Task<Urho.Application> Launcher(Type type, ApplicationOptions options)
		{
			await launcherSemaphore.WaitAsync();
			applicationTaskSource = new TaskCompletionSource<Application>();
			Urho.Application.Started += UrhoApplicationStarted;
			surface?.RemoveFromSuperview();
			this.Add(surface = new Urho.iOS.UrhoSurface(this.Bounds));
			Urho.Application.CreateInstance(type, options).Run();
			return await applicationTaskSource.Task;
		}

		void UrhoApplicationStarted()
		{
			Urho.Application.Started -= UrhoApplicationStarted;
			applicationTaskSource?.TrySetResult(Application.Current);
			launcherSemaphore.Release();
		}
	}
}

