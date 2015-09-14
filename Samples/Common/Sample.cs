//
// TODO: sample class needs InitTouchInput's patch joystick support
//
// The class Sample contains the basic init sequence and shared code for
// all the samples.    The subclasses represent one class per sample.
// 
using Urho;
using System;

public class Sample : Application
{
	public const float PixelSize = 0.01f;
	public const float TouchSensitivity = 2;

	protected float Yaw, Pitch;
	protected bool TouchEnabled;
	protected Node CameraNode;
	protected Sprite LogoSprite;
	protected Subscription KeyDownEventToken;
	
	UrhoConsole console;
	DebugHud debugHud;
	ResourceCache cache;
	UI ui;
	
#warning MISSING_API //enum for Input::GetQualifierDown
	public const int QUAL_SHIFT = 1;
	public const int QUAL_CTRL = 2;
	public const int QUAL_ALT = 4;
	public const int QUAL_ANY = 8;

	public Sample (Context ctx) : base (ctx) {}

	public float Clamp (float v, float min, float max)
	{
		if (v < min)
			return min;
		if (v > max)
			return max;
		return v;
	}

	readonly Random random = new Random();
	/// Return a random float between 0.0 (inclusive) and 1.0 (exclusive.)
	public float NextRandom() { return (float)random.NextDouble(); }
	/// Return a random float between 0.0 and range, inclusive from both ends.
	public float NextRandom(float range) { return (float)random.NextDouble() * range; }
	/// Return a random float between min and max, inclusive from both ends.
	public float NextRandom(float min, float max) { return (float)((random.NextDouble() * (max - min)) + min); }
	/// Return a random integer between 0 and range - 1.
	public int NextRandom(int range) { return random.Next(0, 2); }
	/// Return a random integer between min and max - 1.
	public int NextRandom(int min, int max) { return random.Next(min, max); }
	
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

	protected bool IsLogoVisible
	{
		get { return LogoSprite.IsVisible(); }
		set { LogoSprite.SetVisible(value); }
	}
	
	void SetWindowAndTitleIcon ()
	{
		var icon = cache.GetImage ("Textures/UrhoIcon.png");
		Graphics.SetWindowIcon (icon);
		Graphics.WindowTitle = "Mono Urho3D Sample";
	}


	void CreateConsoleAndDebugHud ()
	{
		var xml = cache.GetXmlFile ("UI/DefaultStyle.xml");
		console = Engine.CreateConsole ();
		console.DefaultStyle = xml;
		console.Background.Opacity = 0.8f;

		debugHud = Engine.CreateDebugHud ();
		debugHud.DefaultStyle = xml;
	}

	protected override void OnSceneUpdate(float timeStep, Scene scene)
	{
		if (!TouchEnabled || CameraNode == null)
			return;

		var input = Input;
		for (uint i = 0, num = input.NumTouches; i < num; ++i){
			TouchState state = input.GetTouch(i);
			if (state.TouchedElement () != null)
				continue;

			if (state.Delta.X != 0 || state.Delta.Y != 0){
				var camera = CameraNode.GetComponent<Camera> ();
				if (camera == null)
					return;
				var graphics = Graphics;
				Yaw += TouchSensitivity * camera.Fov / graphics.Height * state.Delta.X;
				Pitch += TouchSensitivity * camera.Fov / graphics.Height * state.Delta.Y;
				CameraNode.Rotation = new Quaternion (Pitch, Yaw, 0);
			} else {
				var cursor = UI.Cursor;
				if (cursor != null && cursor.IsVisible ())
					cursor.Position = state.Position;
			}
		}
	}

	void HandleKeyDown (KeyDownEventArgs e)
	{
		switch (e.Key){
		case Key.Esc: // ESC
			/*if (this.Console.IsVisible ())
				this.Console.SetVisible (false);
			else*/
				Engine.Exit ();
			return;
		case Key.F1: // F1
			console.Toggle ();
			return;
		case Key.F2: // F2
			debugHud.ToggleAll ();
			return;

		// GC tests
		case Key.N0:
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			break;
		}
		if (UI.FocusElement == null)
			return;
		
		var renderer = Renderer;
		switch (e.Key){
		case Key.N1:
			var quality = renderer.TextureQuality;
			++quality;
			if (quality > 2)
				quality = 0;
			renderer.TextureQuality = quality;
			break;
		case Key.N2:
			var mquality = renderer.MaterialQuality;
			++mquality;
			if (mquality > 2)
				mquality = 0;
			renderer.MaterialQuality = mquality;
			break;
		case Key.N3:
			renderer.SpecularLighting = !renderer.SpecularLighting;
			break;
		case Key.N4:
			renderer.DrawShadows = !renderer.DrawShadows;
			break;
		case Key.N5:
			var shadowMapSize = renderer.ShadowMapSize;
			shadowMapSize *= 2;
			if (shadowMapSize > 2048)
				shadowMapSize = 512;
			renderer.ShadowMapSize = shadowMapSize;
			break;

			// shadow depth and filtering quality
		case Key.N6:
			var q = renderer.ShadowQuality;
			q++;
			if (q > 3)
				q = 0;
			renderer.ShadowQuality = q;
			break;

			// occlusion culling
		case Key.N7:
			var o = !(renderer.MaxOccluderTriangles > 0);
			renderer.MaxOccluderTriangles = o ? 5000 : 0;
			break;

			// instancing
		case Key.N8:
			renderer.DynamicInstancing = !renderer.DynamicInstancing;
			break;

			// screenshot
		case Key.N9:
			var screenshot = new Image (Context);

			// Pending "Image&" binding
			//Graphics.TakeScreenshot (screenshot);
			//screenshot.SavePNG ("/tmp/shot.png");
			break;
		}
	}

	void InitTouchInput ()
	{
		TouchEnabled = true;
		var layout = ResourceCache.GetXmlFile ("UI/ScreenJoystick_Samples.xml");
		if (!string.IsNullOrEmpty(JoystickLayoutPatch))
		{
			XMLFile patchXmlFile = new XMLFile(Context);
			patchXmlFile.FromString(JoystickLayoutPatch);
			layout.Patch(patchXmlFile);
		}
		var screenJoystickIndex = Input.AddScreenJoystick (layout, ResourceCache.GetXmlFile ("UI/DefaultStyle.xml"));
		Input.SetScreenJoystickVisible (screenJoystickIndex, true);
	}

	protected virtual string JoystickLayoutPatch => string.Empty;

	public override void Start ()
	{
		var platform = Runtime.Platform;
		switch (platform){
		case "Android":
		case "iOS":
			InitTouchInput ();
			break;
		}
		//Test ();
		CreateLogo ();
		SetWindowAndTitleIcon ();
		CreateConsoleAndDebugHud ();
		KeyDownEventToken = SubscribeToKeyDown (HandleKeyDown);
	}

	protected void SimpleMoveCamera2D (float timeStep)
	{
		// Do not move if the UI has a focused element (the console)
		if (UI.FocusElement != null)
			return;

		Input input = Input;

		// Movement speed as world units per second
		const float moveSpeed = 4.0f;

		// Read WASD keys and move the camera scene node to the corresponding direction if they are pressed
		if (input.GetKeyDown(Key.W))
			CameraNode.Translate(Vector3.UnitY * moveSpeed * timeStep, TransformSpace.Local);
		if (input.GetKeyDown(Key.S))
			CameraNode.Translate(new Vector3(0.0f, -1.0f, 0.0f) * moveSpeed * timeStep, TransformSpace.Local);
		if (input.GetKeyDown(Key.A))
			CameraNode.Translate(new Vector3(-1.0f, 0.0f, 0.0f) * moveSpeed * timeStep, TransformSpace.Local);
		if (input.GetKeyDown(Key.D))
			CameraNode.Translate(Vector3.UnitX * moveSpeed * timeStep, TransformSpace.Local);

		if (input.GetKeyDown(Key.PageUp))
		{
			Camera camera = CameraNode.GetComponent<Camera>();
			camera.Zoom = (camera.Zoom * 1.01f);
		}

		if (input.GetKeyDown(Key.PageDown))
		{
			Camera camera = CameraNode.GetComponent<Camera>();
			camera.Zoom = (camera.Zoom * 0.99f);
		}
	}

	protected void SimpleMoveCamera3D (float timeStep)
	{
		const float mouseSensitivity = .1f;
		
		if (UI.FocusElement != null)
			return;
		var input = Input;
		const float moveSpeed = 40f;

		var mouseMove = input.MouseMove;
		//var mouseMove = Test2 (input.Handle);
		Yaw += mouseSensitivity * mouseMove.X;
		Pitch += mouseSensitivity * mouseMove.Y;
		Pitch = Clamp(Pitch, -90, 90);

		CameraNode.Rotation = new Quaternion(Pitch, Yaw, 0);

		if (input.GetKeyDown (Key.W))
			CameraNode.Translate (new Vector3(0,0,1) * moveSpeed * timeStep, TransformSpace.Local);
		if (input.GetKeyDown (Key.S))
			CameraNode.Translate (new Vector3(0,0,-1) * moveSpeed * timeStep, TransformSpace.Local);
		if (input.GetKeyDown (Key.A))
			CameraNode.Translate (new Vector3(-1,0,0) * moveSpeed * timeStep, TransformSpace.Local);
		if (input.GetKeyDown (Key.D))
			CameraNode.Translate (new Vector3(1,0,0) * moveSpeed * timeStep, TransformSpace.Local);
	}

	protected void SimpleCreateInstructionsWithWASD (string extra = "")
	{
		SimpleCreateInstructions("Use WASD keys and mouse/touch to move" + extra);
	}
	
	protected void SimpleCreateInstructions(string text = "")
	{
		var t = new Text(Context)
		{
			Value = text,
			HorizontalAlignment = HorizontalAlignment.HA_CENTER,
			VerticalAlignment = VerticalAlignment.VA_CENTER
		};
		t.SetFont(ResourceCache.GetFont("Fonts/Anonymous Pro.ttf"), 15);
		UI.Root.AddChild(t);
	}
}
