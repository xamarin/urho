using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Urho.Samples.Droid
{
	[Activity(Label = "MonoUrho Samples", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : ListActivity
	{
		public const string LibraryNames = "libraryNames";
		public const string PickedLibrary = "pickedLibrary";
		public const int ObtainingLibnames = 1;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			
			StartActivityForResult(new Intent(this, typeof(GameActivity)).PutExtra(PickedLibrary, Intent.GetStringExtra(PickedLibrary)), ObtainingLibnames);
			RequestWindowFeature(WindowFeatures.NoTitle);
			Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

			ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Resource.Layout.samples_list_text_view);
			SetContentView(Resource.Layout.samples_list);
			ListAdapter = adapter;
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			if (ObtainingLibnames != requestCode || resultCode == Result.Canceled)
				return;

			// Populate the list view with library names as pickable items
			string[] libraryNames = data.GetStringArrayExtra(LibraryNames);
			ArrayAdapter<string> adapter = (ArrayAdapter<string>)ListAdapter;
			foreach (var libraryName in libraryNames)
				adapter.Add(libraryName);
		}

		protected override void OnListItemClick(Android.Widget.ListView l, Android.Views.View v, int position, long id)
		{
			ArrayAdapter<string> adapter = (ArrayAdapter<string>)ListAdapter;
			StartActivity(new Intent(this, typeof(GameActivity)).PutExtra(PickedLibrary, adapter.GetItem(position)));
		}
	}
}

