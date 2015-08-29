using System.Linq;
using Urho;

class _08_Decals : Sample
{
	private Scene scene;
	private bool drawDebug;
	private Camera camera;

	public _08_Decals(Context ctx) : base(ctx) { }

	public override void Start()
	{
		base.Start();
		CreateScene();
		CreateUI();
		SetupViewport();
		SubscribeToEvents();
	}

	private void CreateUI()
	{
		var cache = ResourceCache;
		var ui = UI;
		var graphics = Graphics;

		var style = cache.GetXmlFile("UI/DefaultStyle.xml");
		var cursor = new Cursor(Context);
		cursor.SetStyleAuto(style);
		ui.Cursor = cursor;
		cursor.SetPosition(graphics.Width / 2, graphics.Height / 2);

		SimpleCreateInstructionsWithWASD(
			"\nLMB to paint decals, RMB to rotate view\n" +
			"Space to toggle debug geometry\n" +
			"7 to toggle occlusion culling");
	}

	private void SubscribeToEvents()
	{
		SubscribeToPostRenderUpdate(args =>
			{
				// If draw debug mode is enabled, draw viewport debug geometry, which will show eg. drawable bounding boxes and skeleton
				// bones. Note that debug geometry has to be separately requested each frame. Disable depth test so that we can see the
				// bones properly
				if (drawDebug)
					Renderer.DrawDebugGeometry(false);
			});
	}

	protected override void OnUpdate(float timeStep)
	{
		UI ui = UI;
		var input = Input;
		ui.Cursor.SetVisible(!input.GetMouseButtonDown(MouseButton.Right));

		const float mouseSensitivity = .1f;
		const float moveSpeed = 40f;

		if (UI.FocusElement != null)
			return;

		if (!ui.Cursor.IsVisible())
		{
			var mouseMove = input.MouseMove;
			//var mouseMove = Test2 (input.Handle);
			Yaw += mouseSensitivity * mouseMove.X;
			Pitch += mouseSensitivity * mouseMove.Y;
			Pitch = Clamp(Pitch, -90, 90);
		}

		CameraNode.Rotation = new Quaternion(Pitch, Yaw, 0);

		if (input.GetKeyDown(Key.W))
			CameraNode.Translate(new Vector3(0, 0, 1) * moveSpeed * timeStep, TransformSpace.Local);
		if (input.GetKeyDown(Key.S))
			CameraNode.Translate(new Vector3(0, 0, -1) * moveSpeed * timeStep, TransformSpace.Local);
		if (input.GetKeyDown(Key.A))
			CameraNode.Translate(new Vector3(-1, 0, 0) * moveSpeed * timeStep, TransformSpace.Local);
		if (input.GetKeyDown(Key.D))
			CameraNode.Translate(new Vector3(1, 0, 0) * moveSpeed * timeStep, TransformSpace.Local);

		if (Input.GetKeyPress(Key.Space))
			drawDebug = !drawDebug;

		if (UI.Cursor.IsVisible() && Input.GetMouseButtonPress(MouseButton.Left))
			PaintDecal();
	}

	private void SetupViewport()
	{
		var renderer = Renderer;
		renderer.SetViewport(0, new Viewport(Context, scene, CameraNode.GetComponent<Camera>(), null));
	}

	private void CreateScene()
	{
		var cache = ResourceCache;
		scene = new Scene(Context);


		// Create octree, use default volume (-1000, -1000, -1000) to (1000, 1000, 1000)
		// Also create a DebugRenderer component so that we can draw debug geometry
		scene.CreateComponent<Octree>();
		scene.CreateComponent<DebugRenderer>();

		// Create scene node & StaticModel component for showing a static plane
		var planeNode = scene.CreateChild("Plane");
		planeNode.Scale = new Vector3(100.0f, 1.0f, 100.0f);
		var planeObject = planeNode.CreateComponent<StaticModel>();
		planeObject.Model = cache.GetModel("Models/Plane.mdl");
		planeObject.SetMaterial(cache.GetMaterial("Materials/StoneTiled.xml"));

		// Create a Zone component for ambient lighting & fog control
		var zoneNode = scene.CreateChild("Zone");
		var zone = zoneNode.CreateComponent<Zone>();
		zone.SetBoundingBox(new BoundingBox(-1000.0f, 1000.0f));
		zone.AmbientColor=new Color(0.15f, 0.15f, 0.15f);
		zone.FogColor = new Color(0.5f, 0.5f, 0.7f);
		zone.FogStart=100.0f;
		zone.FogEnd=300.0f;

		// Create a directional light to the world. Enable cascaded shadows on it
		var lightNode = scene.CreateChild("DirectionalLight");
		lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f));
		var light = lightNode.CreateComponent<Light>();
		light.LightType= LightType.LIGHT_DIRECTIONAL;
		light.CastShadows=true;
		light.ShadowBias=new BiasParameters(0.00025f, 0.5f);
		// Set cascade splits at 10, 50 and 200 world units, fade shadows out at 80% of maximum shadow distance
		light.ShadowCascade=new CascadeParameters(10.0f, 50.0f, 200.0f, 0.0f, 0.8f);

		// Create some mushrooms
		const uint NUM_MUSHROOMS = 240;
		for (uint i = 0; i < NUM_MUSHROOMS; ++i)
		{
			var mushroomNode = scene.CreateChild("Mushroom");
			mushroomNode.Position=new Vector3(NextRandom(90.0f) - 45.0f, 0.0f, NextRandom(90.0f) - 45.0f);
			mushroomNode.Rotation=new Quaternion(0.0f, NextRandom(360.0f), 0.0f);
			mushroomNode.SetScale(0.5f + NextRandom(2.0f));
			var mushroomObject = mushroomNode.CreateComponent<StaticModel>();
			mushroomObject.Model=cache.GetModel("Models/Mushroom.mdl");
			mushroomObject.SetMaterial(cache.GetMaterial("Materials/Mushroom.xml"));
			mushroomObject.CastShadows=true;
		}

		// Create randomly sized boxes. If boxes are big enough, make them occluders. Occluders will be software rasterized before
		// rendering to a low-resolution depth-only buffer to test the objects in the view frustum for visibility
		const uint NUM_BOXES = 20;
		for (uint i = 0; i < NUM_BOXES; ++i)
		{
			var boxNode = scene.CreateChild("Box");
			float size = 1.0f + NextRandom(10.0f);
			boxNode.Position=new Vector3(NextRandom(80.0f) - 40.0f, size * 0.5f, NextRandom(80.0f) - 40.0f);
			boxNode.SetScale(size);
			var boxObject = boxNode.CreateComponent<StaticModel>();
			boxObject.Model=cache.GetModel("Models/Box.mdl");
			boxObject.SetMaterial(cache.GetMaterial("Materials/Stone.xml"));
			boxObject.CastShadows=true;
			if (size >= 3.0f)
				boxObject.SetOccluder(true);
		}

		// Create the camera. Limit far clip distance to match the fog
		CameraNode = scene.CreateChild("Camera");
		camera = CameraNode.CreateComponent<Camera>();
		camera.FarClip = 300.0f;
		// Set an initial position for the camera scene node above the plane
		CameraNode.Position = new Vector3(0.0f, 5.0f, 0.0f);
	}

	private bool Raycast(float maxDistance, out Vector3 hitPos, out Drawable hitDrawable)
	{
		hitDrawable = null;
		hitPos = Vector3.Zero;
		
		var graphics = Graphics;
		var ui = UI;

		IntVector2 pos = ui.CursorPosition; 
		// Check the cursor is visible and there is no UI element in front of the cursor
		if (!ui.Cursor.IsVisible() || ui.GetElementAt(pos, true) != null)
			return false;

		Ray cameraRay = camera.GetScreenRay((float) pos.X/graphics.Width, (float) pos.Y/graphics.Height);
		var results = scene.GetComponent<Octree>().RaycastSingle(cameraRay, RayQueryLevel.RAY_TRIANGLE, maxDistance, DrawableFlags.Geometry, uint.MaxValue);
		if (results != null && results.Any())
		{
			var first = results.First();
			hitPos = first.Position;
			hitDrawable = first.Drawable;
			return true;
		}
		return false;
	}

	private void PaintDecal()
	{
		Vector3 hitPos;
		Drawable hitDrawable;

		if (Raycast(250.0f, out hitPos, out hitDrawable))
		{
			var targetNode = hitDrawable.Node;
			var decal = targetNode.GetComponent<DecalSet>();

			if (decal == null)
			{
				var cache = ResourceCache;
				decal = targetNode.CreateComponent<DecalSet>();
				decal.Material = cache.GetMaterial("Materials/UrhoDecal.xml");
			}

			// Add a square decal to the decal set using the geometry of the drawable that was hit, orient it to face the camera,
			// use full texture UV's (0,0) to (1,1). Note that if we create several decals to a large object (such as the ground
			// plane) over a large area using just one DecalSet component, the decals will all be culled as one unit. If that is
			// undesirable, it may be necessary to create more than one DecalSet based on the distance
			decal.AddDecal(hitDrawable, hitPos, CameraNode.Rotation, 0.5f, 1.0f, 1.0f, Vector2.Zero,
				Vector2.One, 0.0f, 0.1f, uint.MaxValue);
		}
	}
}
