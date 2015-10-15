To launch application please use the following snippet:
ApplicationLauncher.Run(() => new HelloWorldGame(new Context()));
and make sure you've added needed Assets (Assets\CoreData and Assets\Data).

You can also dispaly Urho in any activity you want via SurfaceView using UrhoSurfaceViewController:

public class MyActivity : Activity
{
	protected override void OnCreate(Bundle savedInstanceState)
	{
		base.OnCreate(savedInstanceState);
		var surfaceView = UrhoSurfaceViewController.OnCreate(this, () => new HelloWorldGame(new Context()));

		var layout = new AbsoluteLayout(this);
		layout.AddView(surfaceView);
		SetContentView(layout);
	}

	protected override void OnPause()
	{
		UrhoSurfaceViewController.OnPause();
		base.OnPause();
	}

	protected override void OnResume()
	{
		UrhoSurfaceViewController.OnResume();
		base.OnResume();
	}

	public override void OnLowMemory()
	{
		UrhoSurfaceViewController.OnLowMemory();
		base.OnLowMemory();
	}

	protected override void OnDestroy()
	{
		UrhoSurfaceViewController.OnDestroy();
		base.OnDestroy();
	}

	public override bool DispatchKeyEvent(KeyEvent e)
	{
		if (!UrhoSurfaceViewController.DispatchKeyEvent(e))
			return false;
		return base.DispatchKeyEvent(e);
	}

	public override void OnWindowFocusChanged(bool hasFocus)
	{
		UrhoSurfaceViewController.OnWindowFocusChanged(hasFocus);
		base.OnWindowFocusChanged(hasFocus);
	}
}