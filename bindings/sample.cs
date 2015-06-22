//
// TODO: Sample class needs the HandleSceneUpdate, which updates yaw/pitch
// TODO: sample class needs InitTouchInput
// TODO: StaticScene, enable the mouse updates once the above are done, they seemed to be rotating forever
// 
using System.Threading;
using static System.Console;
using System.Runtime.InteropServices;
using Urho;
using System;
using System.Runtime.InteropServices;

class Sample : Application {
	[DllImport ("mono-urho")]
	extern static void check (IntPtr p);

	[DllImport ("mono-urho")]
	extern static void check2 (ref Vector3 p);

	protected float Yaw, Pitch;
	protected bool TouchEnabled;
	
	ResourceCache cache;
	UI ui;
	
	public Sample (Context ctx) : base (ctx)
	{
		Environment.CurrentDirectory = "/cvs/Urho3D/bin";
	}

	public override void Setup ()
	{
		WriteLine ("MonoUrho.Setup: This is where we would setup engine flags");
	}

	public float Clamp (float v, float min, float max)
	{
		if (v < min)
			return min;
		if (v > max)
			return max;
		return v;
	}
	
	void CreateLogo ()
	{
		cache = ResourceCache;
		var logoTexture = cache.GetTexture2D ("Textures/LogoLarge.png");
		
		if (logoTexture == null)
			return;

		ui = UI;
		var logoSprite = ui.Root.CreateSprite ();
		logoSprite.Texture = logoTexture;
		int w = logoTexture.Width;
		int h = logoTexture.Height;
		logoSprite.SetScale (256.0f / w);
		logoSprite.SetSize (w, h);
		logoSprite.SetHotSpot (0, h);
		logoSprite.SetAlignment (HorizontalAlignment.HA_LEFT, VerticalAlignment.VA_BOTTOM);
		logoSprite.Opacity = 0.75f;
		logoSprite.Priority = -100;
	}

	void SetWindowAndTitleIcon ()
	{
		var icon = cache.GetImage ("Textures/UrhoIcon.png");
		Graphics.SetWindowIcon (icon);
		Graphics.WindowTitle = "Mono Urho3D Sample";
	}

	UrhoConsole console;
	DebugHud debugHud;
	
	void CreateConsoleAndDebugHud ()
	{
		var xml = cache.GetXMLFile ("UI/DefaultStyle.xml");
		console = Engine.CreateConsole ();
		console.DefaultStyle = xml;
		console.Background.Opacity = 0.8f;

		debugHud = Engine.CreateDebugHud ();
		debugHud.DefaultStyle = xml;
	}

	void HandleKeyDown (EventArgsKeyDown e)
	{
		WriteLine (e.Key);
		switch (e.Key){
		case 27: // ESC
			if (this.Console.IsVisible ())
				this.Console.SetVisible (false);
			else
				Engine.Exit ();
			return;
		case 1073741882: // F1
			console.Toggle ();
			return;
		case 1073741883: // F2
			debugHud.ToggleAll ();
			return;
		}
		if (UI.FocusElement == null)
			return;
		
		var renderer = Renderer;
		switch (e.Key){
		case '1':
			var quality = renderer.TextureQuality;
			++quality;
			if (quality > 2)
				quality = 0;
			renderer.TextureQuality = quality;
			break;
		case '2':
			var mquality = renderer.MaterialQuality;
			++mquality;
			if (mquality > 2)
				mquality = 0;
			renderer.MaterialQuality = mquality;
			break;
		case '3':
			renderer.SpecularLighting = !renderer.SpecularLighting;
			break;
		case '4':
			renderer.DrawShadows = !renderer.DrawShadows;
			break;
		case '5':
			var shadowMapSize = renderer.ShadowMapSize;
			shadowMapSize *= 2;
			if (shadowMapSize > 2048)
				shadowMapSize = 512;
			renderer.ShadowMapSize = shadowMapSize;
			break;

			// shadow depth and filtering quality
		case '6':
			var q = renderer.ShadowQuality;
			q++;
			if (q > 3)
				q = 0;
			renderer.ShadowQuality = q;
			break;

			// occlusion culling
		case '7':
			var o = !(renderer.MaxOccluderTriangles > 0);
			renderer.MaxOccluderTriangles = o ? 5000 : 0;
			break;

			// instancing
		case '8':
			renderer.DynamicInstancing = !renderer.DynamicInstancing;
			break;

			// screenshot
		case '9':
			var screenshot = new Image (Context);

			// Pending "Image&" binding
			//Graphics.TakeScreenshot (screenshot);
			//screenshot.SavePNG ("/tmp/shot.png");
			break;
		}
	}

#if false
	[StructLayout(LayoutKind.Sequential)]
	struct Vector3i { public int X, Y, Z; }
	
	[DllImport ("mono-urho")]
	extern unsafe static Vector3i getVector3 ();

	unsafe void Test ()
	{
		Vector3 j = new Vector3 { X = -100, Y = 200, Z = 300 };
		
		check2 (ref j);

		var v = getVector3 ();
		WriteLine ("getVector3: {0:x} {1:x} {2:x}", v.X, v.Y, v.Z);
	}
#endif
	
	public override void Start ()
	{
		//Test ();
		CreateLogo ();
		SetWindowAndTitleIcon ();
		CreateConsoleAndDebugHud ();
		SubscribeToKeyDown (HandleKeyDown);
	}

}

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
	Node cameraNode;
	Scene scene;
	
	public override void Start ()
	{
		base.Start ();
		CreateScene ();
		//CreateInstructions ();
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
		cameraNode = scene.CreateChild ("camera");
		camera = cameraNode.CreateComponent<Camera>();
		cameraNode.Position = new Vector3 (0, 5, 0);
	}
		
	void SetupViewport ()
	{
		var renderer = Renderer;
		renderer.SetViewport (0, new Viewport (Context, scene, camera, null));
	}

	void MoveCamera (float timeStep)
	{
		const float moveSpeed = 20f;
		const float mouseSensitivity = .1f;
		
		if (UI.FocusElement != null)
			return;
		var input = Input;
		var mouseMove = input.MouseMove;
		Yaw += mouseSensitivity * mouseMove.X;
		Pitch += mouseSensitivity * mouseMove.Y;
		Pitch = Clamp (Pitch, -90, 90);

		//cameraNode.Rotation = new Quaternion (Pitch, Yaw, 0);
		if (input.GetKeyDown ('W'))
			cameraNode.Translate (new Vector3(0,0,1) * moveSpeed * timeStep, TransformSpace.TS_LOCAL);
		if (input.GetKeyDown ('S'))
			cameraNode.Translate (new Vector3(0,0,-1) * moveSpeed * timeStep, TransformSpace.TS_LOCAL);
		if (input.GetKeyDown ('A'))
			cameraNode.Translate (new Vector3(1,0,0) * moveSpeed * timeStep, TransformSpace.TS_LOCAL);
		if (input.GetKeyDown ('D'))
			cameraNode.Translate (new Vector3(-1,0,0) * moveSpeed * timeStep, TransformSpace.TS_LOCAL);
	}
	
	void UpdateHandler (EventArgsUpdate args)
	{
		MoveCamera (args.TimeStep);
	}
	
	public StaticScene (Context c) : base (c) {}
}

class Demo {
	
	static void Main ()
	{
		var c = new Context ();
		//new Sample (c).Run ();
		//new HelloWorld (c).Run ();
		new StaticScene (c).Run ();
	}
}

