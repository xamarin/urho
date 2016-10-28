﻿using System;
using System.Threading.Tasks;
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
