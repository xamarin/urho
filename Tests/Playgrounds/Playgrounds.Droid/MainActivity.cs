using Android.App;
using Android.OS;
using Android.Widget;
using Urho.Droid;

namespace Playgrounds.Droid
{
	[Activity(Label = "Playgrounds.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var button = FindViewById<Button>(Resource.Id.runButton);
			button.Click += Button_Click;
		}

		void Button_Click(object sender, System.EventArgs e)
		{
			//UrhoSurface.RunInActivity<VrTestApp>();
		}
	}
}

