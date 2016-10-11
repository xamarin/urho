In order to run your game application use the following code snippet:

public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
{
	Launch ();
	return true;
}

async void Launch ()
{
	await Task.Yield (); //don't run the game directly in FinishedLaunching 
	new MyGame ().Run ();
}


if you have some custom assets:
1) Add "Data" folder containing your assets to "Resources" project folder and make sure all files have "BundleResource" build action.
2) Pass new ApplicationOptions("Data") to your application like this:
    new Demo(new ApplicationOptions("Data")).Run();
NOTE: pass null if you don't have any assets.