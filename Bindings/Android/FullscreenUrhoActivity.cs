using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Org.Libsdl.App;

namespace Urho.Droid
{
	[Activity(Label = "UrhoSharp",
		ConfigurationChanges = ConfigChanges.KeyboardHidden | ConfigChanges.Orientation | ConfigChanges.ScreenSize,
		Theme = "@android:style/Theme.NoTitleBar",
		ScreenOrientation = ScreenOrientation.Unspecified)]
	public class FullscreenUrhoActivity : Activity
	{
		static ApplicationOptions.OrientationType requestedOrientation = 0;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			requestedOrientation = (ApplicationOptions.OrientationType)Intent.GetIntExtra(nameof(ApplicationOptions.OrientationType), 0);
			switch (requestedOrientation)
			{
				case ApplicationOptions.OrientationType.Landscape:
					RequestedOrientation = ScreenOrientation.SensorLandscape;
					break;
				case ApplicationOptions.OrientationType.Portrait:
					RequestedOrientation = ScreenOrientation.SensorPortrait;
					break;
				case ApplicationOptions.OrientationType.LandscapeAndPortrait:
					RequestedOrientation = ScreenOrientation.Unspecified;
					break;
			}

			SDLSurface surface = SDLActivity.CreateSurface(this);
			FrameLayout layout = new FrameLayout(this);
			layout.AddView(surface);
			SetContentView(layout);
		}

		protected override void OnResume()
		{
			base.OnResume();
			SDLActivity.OnResume();
		}

		protected override void OnPause()
		{
			base.OnPause();
			SDLActivity.OnPause();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			SDLActivity.OnDestroy();
		}

		public override void OnLowMemory()
		{
			base.OnLowMemory();
			SDLActivity.OnLowMemory();
		}

		public override void OnWindowFocusChanged(bool hasFocus)
		{
			base.OnWindowFocusChanged(hasFocus);
			SDLActivity.OnWindowFocusChanged(hasFocus);
		}

		public override bool DispatchKeyEvent(KeyEvent e)
		{
			if (!SDLActivity.DispatchKeyEvent(e))
				return false;

			if (e.KeyCode == Keycode.Back)
				Finish();
			return base.DispatchKeyEvent(e);
		}
	}
}