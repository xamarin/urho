using Urho;

class _38_SceneAndUILoad : Sample
{
	private Scene scene;

	public _38_SceneAndUILoad(Context ctx) : base(ctx) { }

	public override void Start()
	{
		base.Start();

		// Create the scene content
		CreateScene();

		// Create the UI content
		CreateUI();

		// Setup the viewport for displaying the scene
		SetupViewport();
	}

	private void CreateScene()
	{
		var cache = ResourceCache;

		scene = new Scene(Context);

		// Load scene content prepared in the editor (XML format). GetFile() returns an open file from the resource system
		// which scene.LoadXML() will read
		scene.LoadXmlFromCache(cache, "Scenes/SceneLoadExample.xml");

		// Create the camera (not included in the scene file)
		CameraNode = scene.CreateChild("Camera");
		CameraNode.CreateComponent<Camera>();

		// Set an initial position for the camera scene node above the plane
		CameraNode.Position = new Vector3(0.0f, 2.0f, -10.0f);
	}

	private void CreateUI()
	{
		var cache = ResourceCache;
		UI ui = UI;

		// Set up global UI style into the root UI element
		XMLFile style = cache.GetXmlFile("UI/DefaultStyle.xml");
		ui.Root.SetDefaultStyle(style);

		// Create a Cursor UI element because we want to be able to hide and show it at will. When hidden, the mouse cursor will
		// control the camera, and when visible, it will interact with the UI
		Cursor cursor=new Cursor(Context);
		cursor.SetStyleAuto(null);
		ui.Cursor=cursor;
		// Set starting position of the cursor at the rendering window center
		var graphics = Graphics;
		cursor.SetPosition(graphics.Width / 2, graphics.Height / 2);

		// Load UI content prepared in the editor and add to the UI hierarchy
		ui.LoadLayoutToElement(ui.Root, cache, "UI/UILoadExample.xml");

		// Subscribe to button actions (toggle scene lights when pressed then released)
		var button1 = ui.Root.GetChild("ToggleLight1", true);
		var button2 = ui.Root.GetChild("ToggleLight2", true);

		SubscribeToReleased(args =>
			{
				if (args.Element == button1)
					ToggleLight1();
				if (args.Element == button2)
					ToggleLight2();
			});
	}

	protected override void OnUpdate(float timeStep)
	{
		SimpleMoveCamera2D(timeStep);
	}

	private void SetupViewport()
	{
		var renderer = Renderer;
		renderer.SetViewport(0, new Viewport(Context, scene, CameraNode.GetComponent<Camera>(), null));
	}

	private void ToggleLight1()
	{
		Node lightNode = scene.GetChild("Light1", true);
		lightNode?.SetEnabled(!lightNode.IsEnabled());
	}

	private void ToggleLight2()
	{
		Node lightNode = scene.GetChild("Light2", true);
		lightNode?.SetEnabled(!lightNode.IsEnabled());
	}

}
