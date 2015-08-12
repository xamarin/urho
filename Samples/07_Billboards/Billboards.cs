using System;
using System.Linq;
using Urho;

class _07_Billboards : Sample
{
	private Scene scene;
	private bool drawDebug;

	public _07_Billboards(Context ctx) : base(ctx) {}

	public override void Start()
	{
		base.Start();
		CreateScene();
		SimpleCreateInstructionsWithWASD("\nSpace to toggle debug geometry");
		SetupViewport();
		SubscribeToEvents();
	}

	private void SubscribeToEvents()
	{
		SubscribeToUpdate(args =>
		{
			SimpleMoveCamera3D(args.TimeStep);
			AnimateScene(args.TimeStep);
			if (Input.GetKeyDown(Key.Space))
				drawDebug = !drawDebug;
		});

		SubscribeToPostRenderUpdate(args =>
		{
			// If draw debug mode is enabled, draw viewport debug geometry, which will show eg. drawable bounding boxes and skeleton
			// bones. Note that debug geometry has to be separately requested each frame. Disable depth test so that we can see the
			// bones properly
			if (drawDebug)
				Renderer.DrawDebugGeometry(false);
		});
	}

	private void AnimateScene(float timeStep)
	{
		var lightNodes = scene.GetChildrenWithComponent<Light>();
		var billboardNodes = scene.GetChildrenWithComponent<BillboardSet>();

		const float LIGHT_ROTATION_SPEED = 20.0f;
		const float BILLBOARD_ROTATION_SPEED = 50.0f;

		foreach (var lightNode in lightNodes)
		{
			lightNode.Rotate(new Quaternion(0f, LIGHT_ROTATION_SPEED * timeStep, 0f), TransformSpace.World);
		}

		foreach (var billboardNode in billboardNodes)
		{
			var billboardSet = billboardNode.GetComponent<BillboardSet>();
			for (uint i = 0; i < billboardSet.NumBillboards; i++)
			{
				var bb = billboardSet.GetBillboardSafe(i);
				bb.Rotation += BILLBOARD_ROTATION_SPEED*timeStep;
			}
			billboardSet.Commit();
		}
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

		var zoneNode = scene.CreateChild("Zone");
		Zone zone = zoneNode.CreateComponent<Zone>();
		zone.SetBoundingBox(new BoundingBox(-1000.0f, 1000.0f));
		zone.AmbientColor = new Color(0.1f, 0.1f, 0.1f);
		zone.FogStart = 100.0f;
		zone.FogEnd = 300.0f;

		var lightNode = scene.CreateChild("DirectionalLight");
		lightNode.SetDirection(new Vector3(0.5f, -1.0f, 0.5f));
		var light = lightNode.CreateComponent<Light>();
		light.LightType = LightType.LIGHT_DIRECTIONAL;
		light.Color = new Color(0.2f, 0.2f, 0.2f);
		light.SpecularIntensity = 1.0f;

		for (int y = -5; y <= 5; ++y)
		{
			for (int x = -5; x <= 5; ++x)
			{
				var floorNode = scene.CreateChild("FloorTile");
				floorNode.Position = new Vector3(x*20.5f, -0.5f, y*20.5f);
				floorNode.Scale = new Vector3(20.0f, 1.0f, 20.0f);
				var floorObject = floorNode.CreateComponent<StaticModel>();
				floorObject.Model = cache.GetModel("Models/Box.mdl");
				floorObject.SetMaterial(cache.GetMaterial("Materials/Stone.xml"));
			}
		}

		
		// Create groups of mushrooms, which act as shadow casters
		const uint NUM_MUSHROOMGROUPS = 25;
		const uint NUM_MUSHROOMS = 25;

		for (uint i = 0; i < NUM_MUSHROOMGROUPS; ++i)
		{
			var groupNode = scene.CreateChild("MushroomGroup");
			groupNode.Position = new Vector3(NextRandom(190.0f) - 95.0f, 0.0f, NextRandom(190.0f) - 95.0f);

			for (uint j = 0; j < NUM_MUSHROOMS; ++j)
			{
				var mushroomNode = groupNode.CreateChild("Mushroom");
				mushroomNode.Position = new Vector3(NextRandom(25.0f) - 12.5f, 0.0f, NextRandom(25.0f) - 12.5f);
				mushroomNode.Rotation = new Quaternion(0.0f, NextRandom() * 360.0f, 0.0f);
				mushroomNode.SetScale(1.0f + NextRandom() * 4.0f);
				var mushroomObject = mushroomNode.CreateComponent<StaticModel>();
				mushroomObject.Model = cache.GetModel("Models/Mushroom.mdl");
				mushroomObject.SetMaterial(cache.GetMaterial("Materials/Mushroom.xml"));
				mushroomObject.CastShadows = true;
			}
		}
	   
		// Create billboard sets (floating smoke)
		const uint NUM_BILLBOARDNODES = 25;
		const uint NUM_BILLBOARDS = 10;

		for (uint i = 0; i < NUM_BILLBOARDNODES; ++i)
		{
			var smokeNode = scene.CreateChild("Smoke");
			smokeNode.Position = new Vector3(NextRandom(200.0f) - 100.0f, NextRandom(20.0f) + 10.0f, NextRandom(200.0f) - 100.0f);

			var billboardObject = smokeNode.CreateComponent<BillboardSet>();
			billboardObject.NumBillboards = NUM_BILLBOARDS;
			billboardObject.Material = cache.GetMaterial("Materials/LitSmoke.xml");
			billboardObject.SetSorted(true);

			for (uint j = 0; j < NUM_BILLBOARDS; ++j)
			{
				//NOTE: temp working solution. TODO: avoid using "unsafe"
				var bb = billboardObject.GetBillboardSafe(j);
				bb.Position = new Vector3(NextRandom(12.0f) - 6.0f, NextRandom(8.0f) - 4.0f, NextRandom(12.0f) - 6.0f);
				bb.Size = new Vector2(NextRandom(2.0f) + 3.0f, NextRandom(2.0f) + 3.0f);
				bb.Rotation = NextRandom()*360.0f;
				bb.Enabled = true;
			}

			// After modifying the billboards, they need to be "commited" so that the BillboardSet updates its internals
			billboardObject.Commit();
		}

		// Create shadow casting spotlights
		const uint NUM_LIGHTS = 9;

		for (uint i = 0; i < NUM_LIGHTS; ++i)
		{
			lightNode = scene.CreateChild("SpotLight");
			light = lightNode.CreateComponent<Light>();

			float angle = 0.0f;

			Vector3 position = new Vector3((i % 3) * 60.0f - 60.0f, 45.0f, (i / 3) * 60.0f - 60.0f);
			Color color = new Color(((i + 1) & 1) * 0.5f + 0.5f, (((i + 1) >> 1) & 1) * 0.5f + 0.5f, (((i + 1) >> 2) & 1) * 0.5f + 0.5f);

			lightNode.Position = position;
			lightNode.SetDirection(new Vector3((float)Math.Sin(angle), -1.5f, (float)Math.Cos(angle)));

			light.LightType = LightType.LIGHT_SPOT;
			light.Range = 90.0f;
			light.RampTexture = cache.GetTexture2D("Textures/RampExtreme.png");
			light.Fov = 45.0f;
			light.Color = color;
			light.SpecularIntensity = 1.0f;
			light.CastShadows = true;
			light.ShadowBias = new BiasParameters(0.00002f, 0.0f);

			// Configure shadow fading for the lights. When they are far away enough, the lights eventually become unshadowed for
			// better GPU performance. Note that we could also set the maximum distance for each object to cast shadows
			light.ShadowFadeDistance = (100.0f); // Fade start distance
			light.ShadowDistance = 125.0f; // Fade end distance, shadows are disabled
			// Set half resolution for the shadow maps for increased performance
			light.ShadowResolution = 0.5f;
			// The spot lights will not have anything near them, so move the near plane of the shadow camera farther
			// for better shadow depth resolution
			light.ShadowNearFarRatio = 0.01f;
		}

		// Create the camera. Limit far clip distance to match the fog
		CameraNode = scene.CreateChild("Camera");
		var camera = CameraNode.CreateComponent<Camera>();
		camera.FarClip = 300.0f;
		// Set an initial position for the camera scene node above the plane
		CameraNode.Position = new Vector3(0.0f, 5.0f, 0.0f);
	}
}
