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
			if (e.NewElement != null) {
				var surface = new IosUrhoSurface();
				surface.BackgroundColor = UIColor.Black;
				e.NewElement.RegisterRunner(surface.Launcher);
				SetNativeControl(surface);
			}
		}
	}

	public class IosUrhoSurface : UIView
	{
		static readonly SemaphoreSlim launcherSemaphore = new SemaphoreSlim(1);
		static Urho.iOS.UrhoSurface surface;
		static Urho.Application app;

		internal async Task<Urho.Application> Launcher(Type type, ApplicationOptions options)
		{
			await launcherSemaphore.WaitAsync();
			if (surface != null)
			{
				await surface.Stop();
				surface.RemoveFromSuperview ();
			}
			surface = new Urho.iOS.UrhoSurface(this.Bounds);
			surface.AutoresizingMask = UIViewAutoresizing.All;
			this.Add(surface);
			app = await surface.Show(type, options);
			launcherSemaphore.Release();
			return app;
		}
	}
}
