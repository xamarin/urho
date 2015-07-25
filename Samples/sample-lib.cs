//
// TODO: sample class needs InitTouchInput's patch joystick support
//
// The class Sample contains the basic init sequence and shared code for
// all the samples.    The subclasses represent one class per sample.
// 
using System.Threading;
using static System.Console;
using System.Runtime.InteropServices;
using Urho;
using System;

public class Sample : Application {
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

#warning MISSIN_API //constant
    public const float PIXEL_SIZE = 0.01f;

#warning MISSING_API //constants for Input::GetKeyDown
    public const int KEY_PAGEUP = 1073741899;
    public const int KEY_PAGEDOWN = 1073741902;
    public const int KEY_SPACE = 32;
    public const int KEY_F5 = 1073741886;
    public const int KEY_F7 = 1073741888;
    //more in InputEvents.h

#warning MISSING_API //enum for Log::Write
    public const int LOG_RAW = -1;
    public const int LOG_DEBUG = 0;
    public const int LOG_INFO = 1;
    public const int LOG_WARNING = 2;
    public const int LOG_ERROR = 3;
    public const int LOG_NONE = 4;


#warning MISSING_API //enum for VertexBuffer::SetSize //[Flag]
    public const uint MASK_NONE = 0x0;
    public const uint MASK_POSITION = 0x1;
    public const uint MASK_NORMAL = 0x2;
    public const uint MASK_COLOR = 0x4;
    public const uint MASK_TEXCOORD1 = 0x8;
    public const uint MASK_TEXCOORD2 = 0x10;
    public const uint MASK_CUBETEXCOORD1 = 0x20;
    public const uint MASK_CUBETEXCOORD2 = 0x40;
    public const uint MASK_TANGENT = 0x80;
    public const uint MASK_BLENDWEIGHTS = 0x100;
    public const uint MASK_BLENDINDICES = 0x200;
    public const uint MASK_INSTANCEMATRIX1 = 0x400;
    public const uint MASK_INSTANCEMATRIX2 = 0x800;
    public const uint MASK_INSTANCEMATRIX3 = 0x1000;
    public const uint MASK_DEFAULT = 0xffffffff;
    public const uint NO_ELEMENT = 0xffffffff;

#warning MISSING_API //enum for Input::GetQualifierDown
    public const int QUAL_SHIFT = 1;
    public const int QUAL_CTRL = 2;
    public const int QUAL_ALT = 4;
    public const int QUAL_ANY = 8;

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

	void HandleSceneUpdate (SceneUpdateEventArgs args)
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
	
	void HandleKeyDown (KeyDownEventArgs e)
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
