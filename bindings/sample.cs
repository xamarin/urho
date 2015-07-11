//
// TODO: sample class needs InitTouchInput's patch joystick support
// TODO: instead of using the LogicComponent/ScriptComponent in AnimatingSample, I just add the event monitor to the cube, which is not as pretty.
//
// The class Sample contains the basic init sequence and shared code for
// all the samples.    The subclasses represent one class per sample.
// 
using System.Threading;
using static System.Console;
using System.Runtime.InteropServices;
using Urho;
using System;

class Sample : Application {
	[DllImport ("mono-urho")]
	extern static void check (IntPtr p);

	[DllImport ("mono-urho")]
	extern static void check2 (ref Vector3 p);

	const float TouchSensitivity = 2;
	protected float Yaw, Pitch;
	protected bool TouchEnabled;
	protected Node CameraNode;
	protected Scene Scene;
	protected Sprite LogoSprite;
		
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

	Random r = new Random();
	public float NextRandom (float min=-100, float max=100)
	{
		return (float)((r.NextDouble () * (max-min)) + min);
	}
	
	void CreateLogo ()
	{
		cache = ResourceCache;
		var logoTexture = cache.GetTexture2D ("Textures/LogoLarge.png");
		
		if (logoTexture == null)
			return;

		ui = UI;
		LogoSprite = ui.Root.CreateSprite ();
		LogoSprite.Texture = logoTexture;
		int w = logoTexture.Width;
		int h = logoTexture.Height;
		LogoSprite.SetScale (256.0f / w);
		LogoSprite.SetSize (w, h);
		LogoSprite.SetHotSpot (0, h);
		LogoSprite.SetAlignment (HorizontalAlignment.HA_LEFT, VerticalAlignment.VA_BOTTOM);
		LogoSprite.Opacity = 0.75f;
		LogoSprite.Priority = -100;
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
		var xml = cache.GetXmlFile ("UI/DefaultStyle.xml");
		console = Engine.CreateConsole ();
		console.DefaultStyle = xml;
		console.Background.Opacity = 0.8f;

		debugHud = Engine.CreateDebugHud ();
		debugHud.DefaultStyle = xml;
	}

	void HandleSceneUpdate (EventArgsSceneUpdate args)
	{
		if (!TouchEnabled || CameraNode == null)
			return;

		var input = Input;
		for (uint i = 0, num = input.NumTouches; i < num; ++i){
			TouchState state;
			UIElement e;
			if (!input.TryGetTouch (i, out state) || (e = state.TouchedElement ()) == null)
				continue;

			if (state.Delta.X != 0 || state.Delta.Y != 0){
				var camera = CameraNode.GetComponent<Camera> ();
				if (camera == null)
					return;
				var graphics = Graphics;
				Yaw += TouchSensitivity * camera.Fov / graphics.Height * state.Delta.X;
				Pitch += TouchSensitivity * camera.Fov / graphics.Height * state.Delta.Y;
				CameraNode.Rotation = new Quaternion (Pitch, Yaw, 0);
				WriteLine ("at {0}/{1}", Yaw, Pitch);
			} else {
				var cursor = UI.Cursor;
				if (cursor != null && cursor.IsVisible ())
					cursor.Position = state.Position;
			}
		}
	}
	
	void HandleKeyDown (EventArgsKeyDown e)
	{
		WriteLine ("KeyEvent: " + e.Key);
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

	[DllImport ("mono-urho")]
	public extern unsafe static IntVector2 Test2 (IntPtr handle);

	unsafe void Test ()
	{
		Vector3 j = new Vector3 { X = -100, Y = 200, Z = 300 };
		
		check2 (ref j);

		var v = getVector3 ();
		WriteLine ("getVector3: {0:x} {1:x} {2:x}", v.X, v.Y, v.Z);
	}
#endif

	void InitTouchInput ()
	{
		TouchEnabled = true;
		var layout = ResourceCache.GetXmlFile ("UI/ScreenJoystick_Samples.xml");
		var screenJoystickIndex = Input.AddScreenJoystick (layout, ResourceCache.GetXmlFile ("UI/DefaultStyle.xml"));
		Input.SetScreenJoystickVisible (screenJoystickIndex, true);
	}

	public override void Start ()
	{
		switch (Runtime.Platform){
		case "Android":
		case "iOS":
			InitTouchInput ();
			break;
		}
		//Test ();
		CreateLogo ();
		SetWindowAndTitleIcon ();
		CreateConsoleAndDebugHud ();
		SubscribeToKeyDown (HandleKeyDown);
		SubscribeToSceneUpdate (HandleSceneUpdate);
	}

	protected void SimpleMoveCamera (float timeStep)
	{
		const float moveSpeed = 20f;
		const float mouseSensitivity = .1f;
		
		if (UI.FocusElement != null)
			return;
		var input = Input;
		var mouseMove = input.MouseMove;
		//var mouseMove = Test2 (input.Handle);
		Yaw += mouseSensitivity * mouseMove.X;
		Pitch += mouseSensitivity * mouseMove.Y;
		Pitch = Clamp (Pitch, -90, 90);

		CameraNode.Rotation = new Quaternion (Pitch, Yaw, 0);
		if (input.GetKeyDown ('W'))
			CameraNode.Translate (new Vector3(0,0,1) * moveSpeed * timeStep, TransformSpace.TS_LOCAL);
		if (input.GetKeyDown ('S'))
			CameraNode.Translate (new Vector3(0,0,-1) * moveSpeed * timeStep, TransformSpace.TS_LOCAL);
		if (input.GetKeyDown ('A'))
			CameraNode.Translate (new Vector3(1,0,0) * moveSpeed * timeStep, TransformSpace.TS_LOCAL);
		if (input.GetKeyDown ('D'))
			CameraNode.Translate (new Vector3(-1,0,0) * moveSpeed * timeStep, TransformSpace.TS_LOCAL);
	}

	protected void SimpleCreateInstructions ()
	{
		var t = new Text (Context) {
			Value = "Use WASD keys and mouse/touch to move",
			HorizontalAlignment = HorizontalAlignment.HA_CENTER,
			VerticalAlignment = VerticalAlignment.VA_CENTER
		};
		t.SetFont  (ResourceCache.GetFont ("Fonts/Anonymous Pro.ttf"), 15);
		UI.Root.AddChild (t);
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

	void UpdateHandler (EventArgsUpdate args)
	{
		SimpleMoveCamera (args.TimeStep);
	}
	
	public StaticScene (Context c) : base (c) {}
}

class AnimatingScene : Sample {
	Scene scene;
	Camera camera;
	public AnimatingScene (Context c) : base (c) {}

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

			boxObject.SubscribeToSceneUpdate (args =>
			{
				boxNode.Rotate (new Quaternion (rotationSpeed.X * args.TimeStep,
								rotationSpeed.Y * args.TimeStep,
								rotationSpeed.Z * args.TimeStep),
					TransformSpace.TS_LOCAL);
			});
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

class Demo {
	
	static void Main ()
	{
		var c = new Context ();
		//new Sample (c).Run ();
		//new HelloWorld (c).Run ();
		//new StaticScene (c).Run ();
		new AnimatingScene (c).Run ();
	}
}

