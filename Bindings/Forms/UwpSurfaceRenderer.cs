using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Urho;
using Urho.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRendererAttribute(typeof(Urho.Forms.UrhoSurface), typeof(Urho.Forms.UwpSurfaceRenderer))]

namespace Urho.Forms
{

	public class UwpSurfaceRenderer : ViewRenderer<Urho.Forms.UrhoSurface, Urho.UWP.UrhoSurface>
	{
		Urho.UWP.UrhoSurface urhoSurface = null;

		protected override void OnElementChanged(ElementChangedEventArgs<UrhoSurface> e)
		{
			urhoSurface = new Urho.UWP.UrhoSurface();
			e.NewElement.UrhoApplicationLauncher = UrhoLauncher;
			SetNativeControl(urhoSurface);
			base.OnElementChanged(e);
		}

		Task<Application> UrhoLauncher(Type type, ApplicationOptions opts)
		{
			var app = urhoSurface.Run(type, opts);
			return Task.FromResult(app);
		}
	}
}
