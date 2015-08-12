using Urho;

class _30_LightAnimation : Sample
{
	private Scene scene;

	public _30_LightAnimation(Context ctx) : base(ctx) { }

	public override void Start()
	{
		base.Start();
		SetupInstructions();
		CreateScene();
		SetupViewport();
		SubscribeToEvents();
	}

	private void SetupInstructions()
	{
		var instructions = new Text(Context)
			{
				Value = "Use WASD keys and mouse/touch to move",
				HorizontalAlignment = HorizontalAlignment.HA_CENTER,
				VerticalAlignment = VerticalAlignment.VA_CENTER
			};
		var font = ResourceCache.GetFont("Fonts/Anonymous Pro.ttf");
		instructions.SetFont(font, 15);
		UI.Root.AddChild(instructions);

		// Animating text
		Text text = new Text(Context);
		text.Name = "animatingText";
        text.SetFont(font, 15);
		text.HorizontalAlignment = HorizontalAlignment.HA_CENTER;
		text.VerticalAlignment = VerticalAlignment.VA_CENTER;
		text.SetPosition(0, UI.Root.Height/4 + 20);
		UI.Root.AddChild(text);
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

	private void CreateScene()
	{
		var cache = ResourceCache;

		scene = new Scene(Context);

		// Create the Octree component to the scene. This is required before adding any drawable components, or else nothing will
		// show up. The default octree volume will be from (-1000, -1000, -1000) to (1000, 1000, 1000) in world coordinates; it
		// is also legal to place objects outside the volume but their visibility can then not be checked in a hierarchically
		// optimizing manner
		scene.CreateComponent<Octree>();

		// Create a child scene node (at world origin) and a StaticModel component into it. Set the StaticModel to show a simple
		// plane mesh with a "stone" material. Note that naming the scene nodes is optional. Scale the scene node larger
		// (100 x 100 world units)
		Node planeNode = scene.CreateChild("Plane");
		planeNode.Scale=new Vector3(100.0f, 1.0f, 100.0f);
		StaticModel planeObject = planeNode.CreateComponent<StaticModel>();
		planeObject.Model = (cache.GetModel("Models/Plane.mdl"));
		planeObject.SetMaterial(cache.GetMaterial("Materials/StoneTiled.xml"));

		// Create a point light to the world so that we can see something. 
		Node lightNode = scene.CreateChild("PointLight");
		Light light = lightNode.CreateComponent<Light>();
		light.LightType = LightType.LIGHT_POINT;
		light.Range = (10.0f);

		// Create light animation
		ObjectAnimation lightAnimation=new ObjectAnimation(Context);

		// Create light position animation
		ValueAnimation positionAnimation=new ValueAnimation(Context);
		// Use spline interpolation method
		positionAnimation.InterpolationMethod= InterpMethod.IM_SPLINE;
		// Set spline tension
		positionAnimation.SplineTension=0.7f;

		positionAnimation.SetKeyFrame(0.0f, new Vector3(-30.0f, 5.0f, -30.0f));
		positionAnimation.SetKeyFrame(1.0f, new Vector3(30.0f, 5.0f, -30.0f));
		positionAnimation.SetKeyFrame(2.0f, new Vector3(30.0f, 5.0f, 30.0f));
		positionAnimation.SetKeyFrame(3.0f, new Vector3(-30.0f, 5.0f, 30.0f));
		positionAnimation.SetKeyFrame(4.0f, new Vector3(-30.0f, 5.0f, -30.0f));
		// Set position animation
		lightAnimation.AddAttributeAnimation("Position", positionAnimation, WrapMode.WM_LOOP, 1f);

		// Create text animation
		ValueAnimation textAnimation=new ValueAnimation(Context);
		textAnimation.SetKeyFrame(0.0f, "WHITE");
		textAnimation.SetKeyFrame(1.0f, "RED");
		textAnimation.SetKeyFrame(2.0f, "YELLOW");
		textAnimation.SetKeyFrame(3.0f, "GREEN");
		textAnimation.SetKeyFrame(4.0f, "WHITE");
		var uiElement = UI.Root.GetChild("animatingText", false);
		uiElement.SetAttributeAnimation("Text", textAnimation, WrapMode.WM_LOOP, 1f);

		// Create light color animation
		ValueAnimation colorAnimation=new ValueAnimation(Context);
		colorAnimation.SetKeyFrame(0.0f, Color.White);
		colorAnimation.SetKeyFrame(1.0f, Color.Red);
		colorAnimation.SetKeyFrame(2.0f, Color.Yellow);
		colorAnimation.SetKeyFrame(3.0f, Color.Green);
		colorAnimation.SetKeyFrame(4.0f, Color.White);
		// Set Light component's color animation
		lightAnimation.AddAttributeAnimation("@Light/Color", colorAnimation, WrapMode.WM_LOOP, 1f);

		// Apply light animation to light node
		lightNode.ObjectAnimation=lightAnimation;

		// Create more StaticModel objects to the scene, randomly positioned, rotated and scaled. For rotation, we construct a
		// quaternion from Euler angles where the Y angle (rotation about the Y axis) is randomized. The mushroom model contains
		// LOD levels, so the StaticModel component will automatically select the LOD level according to the view distance (you'll
		// see the model get simpler as it moves further away). Finally, rendering a large number of the same object with the
		// same material allows instancing to be used, if the GPU supports it. This reduces the amount of CPU work in rendering the
		// scene.
		const uint NUM_OBJECTS = 200;
		for (uint i = 0; i < NUM_OBJECTS; ++i)
		{
			Node mushroomNode = scene.CreateChild("Mushroom");
			mushroomNode.Position = (new Vector3(NextRandom(90.0f) - 45.0f, 0.0f, NextRandom(90.0f) - 45.0f));
			mushroomNode.Rotation=new Quaternion(0.0f, NextRandom(360.0f), 0.0f);
			mushroomNode.SetScale(0.5f + NextRandom(2.0f));
			StaticModel mushroomObject = mushroomNode.CreateComponent<StaticModel>();
			mushroomObject.Model = (cache.GetModel("Models/Mushroom.mdl"));
			mushroomObject.SetMaterial(cache.GetMaterial("Materials/Mushroom.xml"));
		}

		// Create a scene node for the camera, which we will move around
		// The camera will use default settings (1000 far clip distance, 45 degrees FOV, set aspect ratio automatically)
		CameraNode = scene.CreateChild("Camera");
		CameraNode.CreateComponent<Camera>();

		// Set an initial position for the camera scene node above the plane
		CameraNode.Position = (new Vector3(0.0f, 5.0f, 0.0f));
	}
}
