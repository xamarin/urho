using System.Collections.Generic;
using System.Linq;
using Urho;

class _05_AnimatingScene : Sample {
	Scene scene;
	List<Rotator> rotators = new List<Rotator>();

	public _05_AnimatingScene (Context c) : base (c) {}

	class Rotator : Component
	{
		public Rotator (Context ctx) : base (ctx)
		{
		}

		public Vector3 RotationSpeed { get; set; }

		public void SceneUpdate(SceneUpdateEventArgs args)
		{
			Node.Rotate (new Quaternion (
				RotationSpeed.X * args.TimeStep,
				RotationSpeed.Y * args.TimeStep,
				RotationSpeed.Z * args.TimeStep),
				TransformSpace.Local);
		}
	}
	
	void CreateScene ()
	{
		var cache = ResourceCache;
		scene = new Scene (Context);

		// Create the Octree component to the scene so that drawable objects can be rendered. Use default volume
		// (-1000, -1000, -1000) to (1000, 1000, 1000)
		scene.CreateComponent<Octree> ();

		// Create a Zone component into a child scene node. The Zone controls ambient lighting and fog settings. Like the Octree,
		// it also defines its volume with a bounding box, but can be rotated (so it does not need to be aligned to the world X, Y
		// and Z axes.) Drawable objects "pick up" the zone they belong to and use it when rendering; several zones can exist
		var zoneNode = scene.CreateChild("Zone");
		var zone = zoneNode.CreateComponent<Zone>();
		
		// Set same volume as the Octree, set a close bluish fog and some ambient light
		zone.SetBoundingBox (new BoundingBox(-1000.0f, 1000.0f));
		zone.AmbientColor = new Color (0.05f, 0.1f, 0.15f);
		zone.FogColor = new Color (0.1f, 0.2f, 0.3f);
		zone.FogStart = 10;
		zone.FogEnd = 100;
	
		const int numObjects = 2000;
		for (var i = 0; i < numObjects; ++i){
			Node boxNode = scene.CreateChild("Box");
			boxNode.Position = new Vector3(NextRandom (200f) - 100f, NextRandom (200f) - 100f, NextRandom (200f) - 100f);
			// Orient using random pitch, yaw and roll Euler angles
			boxNode.Rotation = new Quaternion(NextRandom(360.0f), NextRandom(360.0f), NextRandom(360.0f));
			var boxObject = boxNode.CreateComponent<StaticModel>();
			boxObject.Model = cache.GetModel("Models/Box.mdl");
			boxObject.SetMaterial (cache.GetMaterial("Materials/Stone.xml"));
		
			// Add our custom Rotator component which will rotate the scene node each frame, when the scene sends its update event.
			// The Rotator component derives from the base class LogicComponent, which has convenience functionality to subscribe
			// to the various update events, and forward them to virtual functions that can be implemented by subclasses. This way
			// writing logic/update components in C++ becomes similar to scripting.
			// Now we simply set same rotation speed for all objects

			var rotationSpeed = new Vector3(10.0f, 20.0f, 30.0f);

			// First style: use a Rotator instance, which is a component subclass, and
			// add it to the boxNode.
			var rotator = new Rotator (Context) {
				RotationSpeed = rotationSpeed
			};
			boxNode.AddComponent (rotator);
			rotators.Add(rotator);

		}
		// Create the camera. Let the starting position be at the world origin. As the fog limits maximum visible distance, we can
		// bring the far clip plane closer for more effective culling of distant objects
		CameraNode = scene.CreateChild("Camera");
		var camera = CameraNode.CreateComponent<Camera>();
		camera.FarClip = 100.0f;
	
		// Create a point light to the camera scene node
		var light = CameraNode.CreateComponent<Light>();
		light.LightType = LightType.LIGHT_POINT;
		light.Range = 30.0f;
		
	}

	void SetupViewport ()
	{
		var renderer = Renderer;
		renderer.SetViewport (0, new Viewport (Context, scene, CameraNode.GetComponent<Camera>(), null));
	}

	public override void Start ()
	{
		base.Start ();
		CreateScene ();
		SimpleCreateInstructionsWithWASD ();
		SetupViewport ();
		SubscribeToUpdate (args =>
			{
				SimpleMoveCamera3D(args.TimeStep);
			});
	}

	protected override void OnSceneUpdate(SceneUpdateEventArgs args)
	{
		rotators.ToList().ForEach(r => r.SceneUpdate(args));
		base.OnSceneUpdate(args);
	}
}