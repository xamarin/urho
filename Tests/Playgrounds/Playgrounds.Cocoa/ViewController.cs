using System;
using Urho;
using Urho.Extensions.Cocoa;
using Playgrounds.WinForms;
using AppKit;
using Foundation;
using System.Reflection;
using System.IO;

namespace Playgrounds.Cocoa
{
	public partial class ViewController : NSViewController
	{
		UrhoSurface urhoSurface;
		Game game;

		public ViewController(IntPtr handle) : base(handle) {}

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();

			urhoSurface = new UrhoSurface();
			urhoSurface.Frame = UrhoSurfacePlaceholder.Bounds;
			urhoSurface.AutoresizingMask = NSViewResizingMask.WidthSizable | NSViewResizingMask.HeightSizable;

			//Add UrhoSurface to a NSView defined in the storyboard.
			UrhoSurfacePlaceholder.AddSubview(urhoSurface);
		}

		async partial void RestartClicked(NSObject sender)
		{
			game = await urhoSurface.Show<Game>(new ApplicationOptions());
		}

		partial void PausedClicked(NSObject sender)
		{
			urhoSurface.Paused = Paused.State == NSCellStateValue.On;
		}

		partial void SpawnClicked(NSObject sender)
		{
			Urho.Application.InvokeOnMain(() => game.SpawnRandomShape());
		}

		partial void StopClicked(NSObject sender)
		{
			Urho.Application.InvokeOnMain(() => urhoSurface.Stop());
		}

		public override NSObject RepresentedObject
		{
			get
			{
				return base.RepresentedObject;
			}
			set
			{
				base.RepresentedObject = value;
				// Update the view, if already loaded.
			}
		}
	}
}
