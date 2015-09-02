using System;
using Urho;

public class _04_StaticScene : Sample {
	Camera camera;
	Scene scene;
	
	public override void Start ()
	{
		base.Start ();
		CreateScene ();
		SimpleCreateInstructionsWithWASD ();
		SetupViewport ();
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

	protected override void OnUpdate(float timeStep)
	{
		SimpleMoveCamera3D(timeStep);
	}

	public _04_StaticScene (Context c) : base (c) {}
}