using System.Threading;
using static System.Console;
using System.Runtime.InteropServices;
using Urho;
using System;

class HelloWorld : Sample {
	void CreateText ()
	{
		var c = ResourceCache;
		var t = new Text (Context) {
			Value = "Hello World from Urho3D + Mono",
			//Color = new Color (0, 1, 0),
			HorizontalAlignment = HorizontalAlignment.HA_CENTER,
			VerticalAlignment = VerticalAlignment.VA_CENTER
		};
		t.SetFont (c.GetFont ("Fonts/Anonymous Pro.ttf"), 30);
		UI.Root.AddChild (t);
		
	}
	
	public override void Start ()
	{
		base.Start ();
		CreateText ();
	}

	public HelloWorld (Context c) : base (c) {}
}

class StaticScene : Sample {
	Camera camera;
	Scene scene;
	
	public override void Start ()
	{
		base.Start ();
		CreateScene ();
		SimpleCreateInstructions ();
		SetupViewport ();
		SubscribeToUpdate (UpdateHandler);
	}

	void CreateScene ()
	{
		var r = new Random ();
		var cache = ResourceCache;
		scene = new Scene (Context);

		// Create the Octree component to the scene. This is required before adding any drawable components, or else nothing will
		// show up. The default octree volume will be from (-1000, -1000, -1000) to (1000, 1000, 1000) in world coordinates; it
		// is also legal to place objects outside the volume but their visibility can then not be checked in a hierarchically
		// optimizing manner
		scene.CreateComponent<Octree> ();

		// Create a child scene node (at world origin) and a StaticModel component into it. Set the StaticModel to show a simple
		// plane mesh with a "stone" material. Note that naming the scene nodes is optional. Scale the scene node larger
		// (100 x 100 world units)
		var planeNode = scene.CreateChild("Plane");
		planeNode.Scale = new Vector3 (100, 1, 100);
		var planeObject = planeNode.CreateComponent<StaticModel> ();
		planeObject.Model = cache.GetModel ("Models/Plane.mdl");
		planeObject.SetMaterial (cache.GetMaterial ("Materials/StoneTiled.xml"));

		// Create a directional light to the world so that we can see something. The light scene node's orientation controls the
		// light direction; we will use the SetDirection() function which calculates the orientation from a forward direction vector.
		// The light will use default settings (white light, no shadows)
		var lightNode = scene.CreateChild("DirectionalLight");
		lightNode.SetDirection (new Vector3(0.6f, -1.0f, 0.8f)); // The direction vector does not need to be normalized
		var light = lightNode.CreateComponent<Light>();
		light.LightType = LightType.LIGHT_DIRECTIONAL;

		for (int i = 0; i < 200; i++){
			var mushroom = scene.CreateChild ("Mushroom");
			mushroom.Position = new Vector3 (r.Next (90)-45, 0, r.Next (90)-45);
			mushroom.Rotation = new Quaternion (0, r.Next (360), 0);
			mushroom.SetScale (0.5f+r.Next (20000)/10000.0f);
			var mushroomObject = mushroom.CreateComponent<StaticModel>();
			mushroomObject.Model = cache.GetModel ("Models/Mushroom.mdl");
			mushroomObject.SetMaterial (cache.GetMaterial ("Materials/Mushroom.xml"));
		}
		CameraNode = scene.CreateChild ("camera");
		camera = CameraNode.CreateComponent<Camera>();
		CameraNode.Position = new Vector3 (0, 5, 0);
	}
		
	void SetupViewport ()
	{
		var renderer = Renderer;
		renderer.SetViewport (0, new Viewport (Context, scene, camera, null));
	}

	void UpdateHandler (UpdateEventArgs args)
	{
		SimpleMoveCamera (args.TimeStep);
	}
	
	public StaticScene (Context c) : base (c) {}
}

class AnimatingScene : Sample {
	Scene scene;
	Camera camera;
	public AnimatingScene (Context c) : base (c) {}

	class Rotator : Component {
		public Rotator (Context ctx) : base (ctx)
		{
			SubscribeToSceneUpdate (SceneUpdate);
		}
		public Vector3 RotationSpeed { get; set; }
		void SceneUpdate (SceneUpdateEventArgs args)
		{
			Node.Rotate (new Quaternion (RotationSpeed.X * args.TimeStep,
						     RotationSpeed.Y * args.TimeStep,
						     RotationSpeed.Z * args.TimeStep),
				     TransformSpace.Local);
		}
	}
	
	void CreateScene ()
	{
		var r = new Random ();
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
    
		var NUM_OBJECTS = 2000;
		for (var i = 0; i < NUM_OBJECTS; ++i){
			Node boxNode = scene.CreateChild("Box");
			boxNode.Position = new Vector3(NextRandom (), NextRandom (), NextRandom ());
			WriteLine ("At {0}", boxNode.Position);
			// Orient using random pitch, yaw and roll Euler angles
			boxNode.Rotation = new Quaternion(NextRandom(0, 360.0f), NextRandom(0,360.0f), NextRandom(0,360.0f));
			var boxObject = boxNode.CreateComponent<StaticModel>();
			boxObject.Model = cache.GetModel("Models/Box.mdl");
			boxObject.SetMaterial (cache.GetMaterial("Materials/Stone.xml"));
        
			// Add our custom Rotator component which will rotate the scene node each frame, when the scene sends its update event.
			// The Rotator component derives from the base class LogicComponent, which has convenience functionality to subscribe
			// to the various update events, and forward them to virtual functions that can be implemented by subclasses. This way
			// writing logic/update components in C++ becomes similar to scripting.
			// Now we simply set same rotation speed for all objects

			var rotationSpeed = new Vector3(10.0f, 20.0f, 30.0f);
#if true
			// First style: use a Rotator instance, which is a component subclass, and
			// add it to the boxNode.
			var rotator = new Rotator (Context) {
				RotationSpeed = rotationSpeed
			};
			boxNode.AddComponent (rotator);
#else
			//
			// Or directly, hook up to an existing object and attach some code via a
			// subscription
			boxObject.SubscribeToSceneUpdate (args =>
			{
				boxNode.Rotate (new Quaternion (rotationSpeed.X * args.TimeStep,
								rotationSpeed.Y * args.TimeStep,
								rotationSpeed.Z * args.TimeStep),
					TransformSpace.Local);
			});
#endif
		}
		// Create the camera. Let the starting position be at the world origin. As the fog limits maximum visible distance, we can
		// bring the far clip plane closer for more effective culling of distant objects
		CameraNode = scene.CreateChild("Camera");
		camera = CameraNode.CreateComponent<Camera>();
		camera.FarClip = 100.0f;
    
		// Create a point light to the camera scene node
		var light = CameraNode.CreateComponent<Light>();
		light.LightType = LightType.LIGHT_POINT;
		light.Range = 30.0f;
		
	}

	void SetupViewport ()
	{
		var renderer = Renderer;
		renderer.SetViewport (0, new Viewport (Context, scene, camera, null));
	}

	public override void Start ()
	{
		base.Start ();
		CreateScene ();
		SimpleCreateInstructions ();
		SetupViewport ();
		SubscribeToUpdate (args => SimpleMoveCamera (args.TimeStep));
	}

}

class SkeletalAnimation : Sample {
	Scene scene;
	Camera camera;
	bool drawDebug;
	public SkeletalAnimation (Context c) : base (c) {}

	class Mover : Component {
		public float MoveSpeed { get; private set; }
		public float RotationSpeed { get; private set; }
		public BoundingBox Bounds { get; private set; }
		
		public Mover (Context ctx, float moveSpeed, float rotateSpeed, BoundingBox bounds) : base (ctx)
		{
			MoveSpeed = moveSpeed;
			RotationSpeed = rotateSpeed;
			Bounds = bounds;
			SubscribeToSceneUpdate (SceneUpdate);
		}

		public void SceneUpdate (SceneUpdateEventArgs args)
		{
			// This moves the character position
			Node.Translate (Vector3.UnitZ * MoveSpeed * args.TimeStep, TransformSpace.Local);

			// If in risk of going outside the plane, rotate the model right
			var pos = Node.Position;
			if (pos.X < Bounds.Min.X || pos.X > Bounds.Max.X || pos.Z < Bounds.Min.Z || pos.Z > Bounds.Max.Z)
				Node.Yaw (RotationSpeed * args.TimeStep, TransformSpace.Local);

			// Get the model's first (only) animation
			// state and advance its time. Note the
			// convenience accessor to other components in
			// the same scene node
			
			var model = GetComponent<AnimatedModel>();
			if (model.NumAnimationStates > 0){
				var state = model.AnimationStates [0];
				state.AddTime(args.TimeStep);
			}
		}
	}
	
	void CreateScene ()
	{
		var cache = ResourceCache;
		scene = new Scene (Context);

		// Create the Octree component to the scene so that drawable objects can be rendered. Use default volume
		// (-1000, -1000, -1000) to (1000, 1000, 1000)
		scene.CreateComponent<Octree> ();
		scene.CreateComponent<DebugRenderer>();

		// Create scene node & StaticModel component for showing a static plane
		var planeNode = scene.CreateChild("Plane");
		planeNode.Scale = new Vector3 (100, 1, 100);
		var planeObject = planeNode.CreateComponent<StaticModel> ();
		planeObject.Model = cache.GetModel ("Models/Plane.mdl");
		planeObject.SetMaterial (cache.GetMaterial ("Materials/StoneTiled.xml"));

		// Create a Zone component for ambient lighting & fog control
		var zoneNode = scene.CreateChild("Zone");
		var zone = zoneNode.CreateComponent<Zone>();
		
		// Set same volume as the Octree, set a close bluish fog and some ambient light
		zone.SetBoundingBox (new BoundingBox(-1000.0f, 1000.0f));
		zone.AmbientColor = new Color (0.15f, 0.15f, 0.15f);
		zone.FogColor = new Color (0.5f, 0.5f, 0.7f);
		zone.FogStart = 100;
		zone.FogEnd = 300;

		// Create a directional light to the world. Enable cascaded shadows on it
		var lightNode = scene.CreateChild("DirectionalLight");
		lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f));
		var light = lightNode.CreateComponent<Light>();
		light.LightType = LightType.LIGHT_DIRECTIONAL;
		light.CastShadows = true;
		light.ShadowBias = new BiasParameters(0.00025f, 0.5f);
		
		// Set cascade splits at 10, 50 and 200 world units, fade shadows out at 80% of maximum shadow distance
		light.ShadowCascade = new CascadeParameters(10.0f, 50.0f, 200.0f, 0.0f, 0.8f);

		// Create animated models
		const int NUM_MODELS = 100;
		const float MODEL_MOVE_SPEED = 2.0f;
		const float MODEL_ROTATE_SPEED = 100.0f;
		var bounds = new BoundingBox (new Vector3(-47.0f, 0.0f, -47.0f), new Vector3(47.0f, 0.0f, 47.0f));

		for (var i = 0; i < NUM_MODELS; ++i){
			var modelNode = scene.CreateChild("Jack");
			modelNode.Position = new Vector3(NextRandom(-45,45), 0.0f, NextRandom (-45, 45));
			modelNode.Rotation = new Quaternion (0, NextRandom(0, 360), 0);
			//var modelObject = modelNode.CreateComponent<AnimatedModel>();
			var modelObject = new AnimatedModel (Context);
			modelNode.AddComponent (modelObject);
			modelObject.Model = cache.GetModel("Models/Jack.mdl");
			//modelObject.Material = cache.GetMaterial("Materials/Jack.xml");
			modelObject.CastShadows = true;

			// Create an AnimationState for a walk animation. Its time position will need to be manually updated to advance the
			// animation, The alternative would be to use an AnimationController component which updates the animation automatically,
			// but we need to update the model's position manually in any case
			var walkAnimation = cache.GetAnimation("Models/Jack_Walk.ani");
			var state = modelObject.AddAnimationState(walkAnimation);
			// The state would fail to create (return null) if the animation was not found
			if (state != null)
			{
				// Enable full blending weight and looping
				state.Weight = 1;
				state.SetLooped (true);
			}
			
			// Create our custom Mover component that will move & animate the model during each frame's update
			var mover = new Mover (Context, MODEL_MOVE_SPEED, MODEL_ROTATE_SPEED, bounds);
			modelNode.AddComponent (mover);
		}
		
		// Create the camera. Limit far clip distance to match the fog
		CameraNode = scene.CreateChild("Camera");
		camera = CameraNode.CreateComponent<Camera>();
		camera.FarClip = 300;
		
		// Set an initial position for the camera scene node above the plane
		CameraNode.Position = new Vector3(0.0f, 5.0f, 0.0f);
	}

	void SetupViewport ()
	{
		var renderer = Renderer;
		renderer.SetViewport (0, new Viewport (Context, scene, camera, null));
	}

	void SubscribeToEvents ()
	{
		// Handle keyboard input, mouse/touch input, and handle spacebar to debug
		SubscribeToUpdate (args => {
			SimpleMoveCamera (args.TimeStep);
			if (Input.GetKeyDown (Key.Space))
				drawDebug = !drawDebug;
		});

		// Subscribe HandlePostRenderUpdate() function for
		// processing the post-render update event, sent after
		// Renderer subsystem is done with defining the draw
		// calls for the viewports (but before actually
		// executing them.) We will request debug geometry
		// rendering during that event
		
		SubscribeToPostRenderUpdate (args => {
			// If draw debug mode is enabled, draw viewport debug geometry, which will show eg. drawable bounding boxes and skeleton
			// bones. Note that debug geometry has to be separately requested each frame. Disable depth test so that we can see the
			// bones properly
			if (drawDebug)
				Renderer.DrawDebugGeometry (false);
		});
	}

	public override void Start ()
	{
		base.Start ();
		CreateScene ();
		SimpleCreateInstructions ("\nSpace to toggle debug geometry");
		SetupViewport ();
		SubscribeToEvents();
	}
}

class Demo {
	
	static void Main ()
	{
		var c = new Context ();
		//new Sample (c).Run ();
		//new HelloWorld (c).Run ();
		//new StaticScene (c).Run ();
		//new AnimatingScene (c).Run ();
		var a = new RenderPath ();
		var b = a.Clone ();
		new SkeletalAnimation (c).Run ();
	}
}

