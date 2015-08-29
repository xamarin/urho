using Urho;

class _35_SignedDistanceFieldText : Sample
{
	private Scene scene;
	private Camera camera;

	public _35_SignedDistanceFieldText(Context ctx) : base(ctx) { }

	public override void Start()
	{
		base.Start();
		CreateScene();
		SimpleCreateInstructionsWithWASD();
		SetupViewport();
	}

	protected override void OnUpdate(float timeStep)
	{
		SimpleMoveCamera3D(timeStep);
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

		// Create a directional light to the world so that we can see something. The light scene node's orientation controls the
		// light direction; we will use the SetDirection() function which calculates the orientation from a forward direction vector.
		// The light will use default settings (white light, no shadows)
		Node lightNode = scene.CreateChild("DirectionalLight");
		lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f)); // The direction vector does not need to be normalized
		Light light = lightNode.CreateComponent<Light>();
		light.LightType = LightType.LIGHT_DIRECTIONAL;

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
			mushroomNode.SetScale(0.5f + NextRandom(2.0f));
			StaticModel mushroomObject = mushroomNode.CreateComponent<StaticModel>();
			mushroomObject.Model = (cache.GetModel("Models/Mushroom.mdl"));
			mushroomObject.SetMaterial(cache.GetMaterial("Materials/Mushroom.xml"));

			Node mushroomTitleNode = mushroomNode.CreateChild("MushroomTitle");
			mushroomTitleNode.Position = (new Vector3(0.0f, 1.2f, 0.0f));
			Text3D mushroomTitleText = mushroomTitleNode.CreateComponent<Text3D>();
			mushroomTitleText.Text="Mushroom " + i;
			mushroomTitleText.SetFont(cache.GetFont("Fonts/BlueHighway.sdf"), 24);

			mushroomTitleText.SetColor(Color.Red);

			if (i % 3 == 1)
			{
				mushroomTitleText.SetColor(Color.Green);
				mushroomTitleText.TextEffect= TextEffect.TE_SHADOW;
				mushroomTitleText.EffectColor = new Color(0.5f, 0.5f, 0.5f);
			}
			else if (i % 3 == 2)
			{
				mushroomTitleText.SetColor(Color.Yellow);
				mushroomTitleText.TextEffect = TextEffect.TE_STROKE;
				mushroomTitleText.EffectColor = new Color(0.5f, 0.5f, 0.5f);
			}

			mushroomTitleText.SetAlignment(HorizontalAlignment.HA_CENTER, VerticalAlignment.VA_CENTER);
		}

		// Create a scene node for the camera, which we will move around
		// The camera will use default settings (1000 far clip distance, 45 degrees FOV, set aspect ratio automatically)
		CameraNode = scene.CreateChild("Camera");
		CameraNode.CreateComponent<Camera>();

		// Set an initial position for the camera scene node above the plane
		CameraNode.Position = (new Vector3(0.0f, 5.0f, 0.0f));

	}
}
