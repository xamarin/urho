// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Playgrounds.Cocoa
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSButton Paused { get; set; }

		[Outlet]
		AppKit.NSButton RestartBtn { get; set; }

		[Outlet]
		AppKit.NSButton SpawnBtn { get; set; }

		[Outlet]
		AppKit.NSButton StopBtn { get; set; }

		[Outlet]
		AppKit.NSView UrhoSurfacePlaceholder { get; set; }

		[Action ("PausedClicked:")]
		partial void PausedClicked (Foundation.NSObject sender);

		[Action ("RestartClicked:")]
		partial void RestartClicked (Foundation.NSObject sender);

		[Action ("SpawnClicked:")]
		partial void SpawnClicked (Foundation.NSObject sender);

		[Action ("StopClicked:")]
		partial void StopClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Paused != null) {
				Paused.Dispose ();
				Paused = null;
			}

			if (RestartBtn != null) {
				RestartBtn.Dispose ();
				RestartBtn = null;
			}

			if (SpawnBtn != null) {
				SpawnBtn.Dispose ();
				SpawnBtn = null;
			}

			if (StopBtn != null) {
				StopBtn.Dispose ();
				StopBtn = null;
			}

			if (UrhoSurfacePlaceholder != null) {
				UrhoSurfacePlaceholder.Dispose ();
				UrhoSurfacePlaceholder = null;
			}
		}
	}
}
