using System;
using CoreGraphics;
using UIKit;
using Urho;
using Urho.iOS;

namespace Playgrounds.iOS
{
	public partial class FirstViewController : UIViewController
	{
		UrhoSurface urhoSurface;
		Game game;

		protected FirstViewController(IntPtr handle) : base(handle)
		{
		}

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Restart button
			UIButton restartBtn = new UIButton(UIButtonType.RoundedRect);
			restartBtn.Frame = new CGRect(10f, 0f, 50, 50f);
			restartBtn.SetTitle("Restart", UIControlState.Normal);
			restartBtn.TouchUpInside += async (sender, e) => game = await urhoSurface.Show<Game>(new ApplicationOptions());
			View.AddSubview(restartBtn);

			// Stop button
			UIButton stopBtn = new UIButton(UIButtonType.RoundedRect);
			stopBtn.Frame = new CGRect(75f, 0f, 50, 50f);
			stopBtn.SetTitle("Stop", UIControlState.Normal);
			stopBtn.TouchUpInside += (sender, e) =>
				{
					urhoSurface.Stop();
					game = null;
				};
			View.AddSubview(stopBtn);

			// Spawn button
			UIButton spawnBtn = new UIButton(UIButtonType.RoundedRect);
			spawnBtn.Frame = new CGRect(135f, 0f, 50, 50f);
			spawnBtn.SetTitle("Spawn", UIControlState.Normal);
			spawnBtn.TouchUpInside += (sender, e) =>
				{
					if (game != null)
						Urho.Application.InvokeOnMain(() => game?.SpawnRandomShape());
				};
			View.AddSubview(spawnBtn);

			// Pause/Unpause button
			UIButton pauseBtn = new UIButton(UIButtonType.RoundedRect);
			pauseBtn.Frame = new CGRect(200f, 0f, 80, 50f);
			pauseBtn.SetTitle("(Un)pause", UIControlState.Normal);
			pauseBtn.TouchUpInside += (sender, e) => urhoSurface.Paused = !urhoSurface.Paused;
			View.AddSubview(pauseBtn);

			urhoSurface = new UrhoSurface();
			urhoSurface.Frame = new CoreGraphics.CGRect(0, 50, View.Bounds.Width, View.Bounds.Height - 50);
			urhoSurface.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			View.Add(urhoSurface);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
