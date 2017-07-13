using System;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Org.Libsdl.App;
using Urho;
using Urho.Droid;

namespace Playgrounds.Droid
{
	[Activity(Label = "Playgrounds.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		AbsoluteLayout placeholder;
		Game game;
		UrhoSurfacePlaceholder surface;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			FindViewById<Button>(Resource.Id.restartBtn).Click += OnRestart;
			FindViewById<Button>(Resource.Id.stopBtn).Click += OnStop;
			FindViewById<Button>(Resource.Id.spawnBtn).Click += OnSpawn;
			FindViewById<Button>(Resource.Id.pauseBtn).Click += OnPause;
			placeholder = FindViewById<AbsoluteLayout>(Resource.Id.UrhoSurfacePlaceHolder);
		}

		protected override void OnPause()
		{
			UrhoSurface.OnPause();
			base.OnPause();
		}

		protected override void OnResume()
		{
			UrhoSurface.OnResume();
			base.OnResume();
		}

		protected override void OnDestroy()
		{
			UrhoSurface.OnDestroy();
			base.OnDestroy();
		}

		public override void OnLowMemory()
		{
			UrhoSurface.OnLowMemory();
			base.OnLowMemory();
		}

		static bool paused;
		void OnPause(object sender, EventArgs e)
		{
			paused = !paused;
			if (paused)
				UrhoSurface.OnPause();
			else
				UrhoSurface.OnResume();
		}

		void OnSpawn(object sender, EventArgs e)
		{
			if (game?.IsActive == true)
				Urho.Application.InvokeOnMain(() => game.SpawnRandomShape());
		}

		async void OnStop(object sender, EventArgs e)
		{
			await surface.Stop();
			if (surface.Parent is ViewGroup)
				((ViewGroup)surface.Parent).RemoveView(surface);
		}

		async void OnRestart(object sender, EventArgs e)
		{
			//UrhoSurface.RunInActivity<Game>(new ApplicationOptions(null));
			//return;
			if (surface != null)
			{
				await surface.Stop();
				await Task.Yield();
				var viewGroup = surface.Parent as ViewGroup;
				viewGroup?.RemoveView(surface);
			}

			surface = UrhoSurface.CreateSurface(this);
			placeholder.AddView(surface);
			game = await surface.Show<Game>(new Urho.ApplicationOptions());
		}
	}
}

