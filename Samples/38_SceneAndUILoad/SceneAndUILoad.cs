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

		// Subscribe to global events for camera movement
		SubscribeToEvents();
	}

	private void CreateScene()
	{
		var cache = ResourceCache;

		scene = new Scene(Context);

		// Load scene content prepared in the editor (XML format). GetFile() returns an open file from the resource system
		// which scene.LoadXML() will read
#warning MISSING_API ResourceCache::GetFile, Scene::LoadXml
		////File file = cache.GetFile("Scenes/SceneLoadExample.xml");
		////scene.LoadXML(file);

		// Create the camera (not included in the scene file)
		CameraNode = scene.CreateChild("Camera");
		CameraNode.CreateComponent<Camera>();

		// Set an initial position for the camera scene node above the plane
		CameraNode.Position = (new Vector3(0.0f, 2.0f, -10.0f));
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
#warning MISSING_API UI::LoadLayout
		UIElement layoutRoot = null; ////ui.LoadLayout(cache.GetXmlFile("UI/UILoadExample.xml"));
		ui.Root.AddChild(layoutRoot);

		// Subscribe to button actions (toggle scene lights when pressed then released)
		var button1 = (Button)layoutRoot.GetChild("ToggleLight1", true);
		var button2 = (Button)layoutRoot.GetChild("ToggleLight2", true);

		SubscribeToReleased(args =>
			{
				if (args.Element == button1)
					ToggleLight1();
				if (args.Element == button2)
					ToggleLight2();
			});
	}


	private void SubscribeToEvents()
	{
		SubscribeToUpdate(args => SimpleMoveCamera3D(args.TimeStep));
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
