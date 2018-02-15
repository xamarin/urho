using System;
using System.Threading;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Org.Libsdl.App;
using Urho;
using Urho.Droid;

namespace Playgrounds.Droid
{
	[Activity(Label = "Playgrounds.Droid", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.NoTitleBar")]
	public class MainActivity : Activity
	{
		AbsoluteLayout placeholder;
		ARCoreSample game;
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

			if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != Permission.Granted)
				ActivityCompat.RequestPermissions(this, new []{ Manifest.Permission.Camera}, 42);
		}

		public override bool DispatchKeyEvent(KeyEvent e)
		{
			if (e.KeyCode == Keycode.Back)
			{
				this.Finish();
				return false;
			}

			if (!UrhoSurface.DispatchKeyEvent(e))
				return false;
			return base.DispatchKeyEvent(e);
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

		public override void OnBackPressed()
		{
			UrhoSurface.OnDestroy();
			Finish();
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

		async void OnSpawn(object sender, EventArgs e)
		{
			if (surface == null)
				return;

			surface.Stop();
			if (surface.Parent is ViewGroup)
				((ViewGroup)surface.Parent).RemoveView(surface);
			StartActivity(typeof(MainActivity));
		}

		async void OnStop(object sender, EventArgs e)
		{
			if (surface == null)
				return;

			surface.Stop();
			if (surface.Parent is ViewGroup)
				((ViewGroup)surface.Parent).RemoveView(surface);
		}

		async void OnRestart(object sender, EventArgs e)
		{
			if (surface != null)
			{
				surface.Stop();
				var viewGroup = surface.Parent as ViewGroup;
				viewGroup?.RemoveView(surface);
			}
			surface = UrhoSurface.CreateSurface(this);
			placeholder.AddView(surface);
			//game = await surface.Show<Game>(new Urho.ApplicationOptions());
			game = await surface.Show<ARCoreSample>(new Urho.ApplicationOptions());
		}
	}
}

