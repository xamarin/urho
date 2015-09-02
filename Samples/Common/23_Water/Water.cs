using Urho;

public class _23_Water : Sample
{
	private Scene scene;
	private Node waterNode;
	private Node reflectionCameraNode;
	private Plane waterPlane;
	private Plane waterClipPlane;

	public _23_Water(Context ctx) : base(ctx) { }

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
		var camera = reflectionCameraNode.GetComponent<Camera>();
		camera.AspectRatio = (float)Graphics.Width / Graphics.Height;
	}

	private void SetupViewport()
	{
		var graphics = Graphics;
		var renderer = Renderer;
		var cache = ResourceCache;
		
		renderer.SetViewport(0, new Viewport(Context, scene, CameraNode.GetComponent<Camera>(), null));

		// Create a mathematical plane to represent the water in calculations

		waterPlane = new Plane(waterNode.WorldRotation * new Vector3(0.0f, 1.0f, 0.0f), waterNode.WorldPosition);
		// Create a downward biased plane for reflection view clipping. Biasing is necessary to avoid too aggressive clipping
		waterClipPlane = new Plane(waterNode.WorldRotation * new Vector3(0.0f, 1.0f, 0.0f), waterNode.WorldPosition - new Vector3(0.0f, 0.1f, 0.0f));

		// Create camera for water reflection
		// It will have the same farclip and position as the main viewport camera, but uses a reflection plane to modify
		// its position when rendering
		reflectionCameraNode = CameraNode.CreateChild();
		var reflectionCamera = reflectionCameraNode.CreateComponent<Camera>();
		reflectionCamera.FarClip = 750.0f;
		reflectionCamera.ViewMask= 0x7fffffff; // Hide objects with only bit 31 in the viewmask (the water plane)
		reflectionCamera.AutoAspectRatio = false;
		reflectionCamera.UseReflection = true;
		reflectionCamera.ReflectionPlane = waterPlane;
		reflectionCamera.UseClipping = true; // Enable clipping of geometry behind water plane
		reflectionCamera.ClipPlane = waterClipPlane;
		// The water reflection texture is rectangular. Set reflection camera aspect ratio to match
		reflectionCamera.AspectRatio = (float)graphics.Width / graphics.Height;
		// View override flags could be used to optimize reflection rendering. For example disable shadows
		//reflectionCamera.ViewOverrideFlags = ViewOverrideFlags.DisableShadows;

		// Create a texture and setup viewport for water reflection. Assign the reflection texture to the diffuse
		// texture unit of the water material
		int texSize = 1024;
		Texture2D renderTexture = new Texture2D(Context);
		renderTexture.SetSize(texSize, texSize, Graphics.RGBFormat, TextureUsage.TEXTURE_RENDERTARGET);
		renderTexture.FilterMode = TextureFilterMode.FILTER_BILINEAR;
		RenderSurface surface = renderTexture.RenderSurface;
		var rttViewport = new Viewport(Context, scene, reflectionCamera, null);
		surface.SetViewport(0, rttViewport);
		var waterMat = cache.GetMaterial("Materials/Water.xml");
		waterMat.SetTexture(TextureUnit.TU_DIFFUSE, renderTexture);
	}

	private void CreateScene()
	{
		var cache = ResourceCache;
		scene = new Scene(Context);

		// Create octree, use default volume (-1000, -1000, -1000) to (1000, 1000, 1000)
		scene.CreateComponent<Octree>();

		// Create a Zone component for ambient lighting & fog control
		var zoneNode = scene.CreateChild("Zone");
		var zone = zoneNode.CreateComponent<Zone>();
		zone.SetBoundingBox(new BoundingBox(-1000.0f, 1000.0f));
		zone.AmbientColor = new Color(0.15f, 0.15f, 0.15f);
		zone.FogColor = new Color(1.0f, 1.0f, 1.0f);
		zone.FogStart = 500.0f;
		zone.FogEnd = 750.0f;

		// Create a directional light to the world. Enable cascaded shadows on it
		var lightNode = scene.CreateChild("DirectionalLight");
		lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f));
		var light = lightNode.CreateComponent<Light>();
		light.LightType = LightType.LIGHT_DIRECTIONAL;
		light.CastShadows = true;
		light.ShadowBias = new BiasParameters(0.00025f, 0.5f);
		light.ShadowCascade = new CascadeParameters(10.0f, 50.0f, 200.0f, 0.0f, 0.8f);
		light.SpecularIntensity = 0.5f;
		// Apply slightly overbright lighting to match the skybox
		light.Color = new Color(1.2f, 1.2f, 1.2f);

		// Create skybox. The Skybox component is used like StaticModel, but it will be always located at the camera, giving the
		// illusion of the box planes being far away. Use just the ordinary Box model and a suitable material, whose shader will
		// generate the necessary 3D texture coordinates for cube mapping
		var skyNode = scene.CreateChild("Sky");
		skyNode.SetScale(500.0f); // The scale actually does not matter
		var skybox = skyNode.CreateComponent<Skybox>();
		skybox.Model = cache.GetModel("Models/Box.mdl");
		skybox.SetMaterial(cache.GetMaterial("Materials/Skybox.xml"));

		// Create heightmap terrain
		var terrainNode = scene.CreateChild("Terrain");
		terrainNode.Position = new Vector3(0.0f, 0.0f, 0.0f);
		var terrain = terrainNode.CreateComponent<Terrain>();
		terrain.PatchSize = 64;
		terrain.Spacing = new Vector3(2.0f, 0.5f, 2.0f); // Spacing between vertices and vertical resolution of the height map
		terrain.Smoothing =true;
		terrain.SetHeightMap(cache.GetImage("Textures/HeightMap.png"));
		terrain.Material = cache.GetMaterial("Materials/Terrain.xml");
		// The terrain consists of large triangles, which fits well for occlusion rendering, as a hill can occlude all
		// terrain patches and other objects behind it
		terrain.SetOccluder(true);

		// Create 1000 boxes in the terrain. Always face outward along the terrain normal
		uint NUM_OBJECTS = 1000;
		for (uint i = 0; i < NUM_OBJECTS; ++i)
		{
			var objectNode = scene.CreateChild("Box");
			Vector3 position = new Vector3(NextRandom(2000.0f) - 1000.0f, 0.0f, NextRandom(2000.0f) - 1000.0f);
			position.Y = terrain.GetHeight(position) + 2.25f;
			objectNode.Position = position;
			// Create a rotation quaternion from up vector to terrain normal
			objectNode.Rotation = Quaternion.FromRotationTo(new Vector3(0.0f, 1.0f, 0.0f), terrain.GetNormal(position));
			objectNode.SetScale(5.0f);
			var obj = objectNode.CreateComponent<StaticModel>();
			obj.Model = cache.GetModel("Models/Box.mdl");
			obj.SetMaterial(cache.GetMaterial("Materials/Stone.xml"));
			obj.CastShadows = true;
		}

		// Create a water plane object that is as large as the terrain
		waterNode = scene.CreateChild("Water");
		waterNode.Scale = new Vector3(2048.0f, 1.0f, 2048.0f);
		waterNode.Position = new Vector3(0.0f, 5.0f, 0.0f);
		var water = waterNode.CreateComponent<StaticModel>();
		water.Model = cache.GetModel("Models/Plane.mdl");
		water.SetMaterial(cache.GetMaterial("Materials/Water.xml"));
		// Set a different viewmask on the water plane to be able to hide it from the reflection camera
		water.ViewMask = 0x80000000;


		// Create the camera. Limit far clip distance to match the fog
		CameraNode = new Node(Context);
		var camera = CameraNode.CreateComponent<Camera>();
		camera.FarClip = 750.0f;
		// Set an initial position for the camera scene node above the plane
		CameraNode.Position = new Vector3(0.0f, 7.0f, -20.0f);
	}
}
