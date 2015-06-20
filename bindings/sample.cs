using System.Threading;
using static System.Console;
using System.Runtime.InteropServices;
using Urho;
using System;
using System.Runtime.InteropServices;

class Demo {
	[DllImport ("mono-urho")]
	extern static void check (IntPtr p);

	Demo ()
	{
		Environment.CurrentDirectory = "/cvs/Urho3D/bin";
	}

	Application app;
	ResourceCache cache;
	UI ui;
	
	void CreateLogo ()
	{
		cache = app.ResourceCache;
		Console.WriteLine ("one");
		var logoTexture = cache.GetTexture2D ("Textures/LogoLarge.png");
		Console.WriteLine ("two");
		
		if (logoTexture == null)
			return;

		ui = app.UI;
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
		app.Graphics.SetWindowIcon (icon);
		app.Graphics.WindowTitle = "Mono Urho3D Sample";
	}

	UrhoConsole console;
	DebugHud debugHud;
	
	void CreateConsoleAndDebugHud ()
	{
		var xml = cache.GetXMLFile ("UI/DefaultStyle.xml");
		console = app.Engine.CreateConsole ();
		console.DefaultStyle = xml;
		console.Background.Opacity = 0.8f;

		debugHud = app.Engine.CreateDebugHud ();
		debugHud.DefaultStyle = xml;
	}

	void HandleKeyDown (EventArgsKeyDown e)
	{
		Console.WriteLine (e.Key);
		switch (e.Key){
		case 27: // ESC
			if (app.Console.IsVisible ())
				app.Console.SetVisible (false);
			else
				app.Engine.Exit ();
			return;
		case 1073741882: // F1
			console.Toggle ();
			return;
		case 1073741883: // F2
			debugHud.ToggleAll ();
			return;
		}
		try {
		if (app.UI.FocusElement == null)
			return;
		} catch {
			// Ignore for now -- need to replace new Foo(xx) with Runtime.LookupObject(xx)
		}
		
		var renderer = app.Renderer;
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
			var screenshot = new Image (app.Context);

			// Pending "Image&" binding
			//app.Graphics.TakeScreenshot (screenshot);
			//screenshot.SavePNG ("/tmp/shot.png");
			break;
		}
	}
	
	public void Run ()
	{
		var c = new Context ();
		Console.WriteLine (c.Handle);
		app = new Application (
			c,
			setup=>{
				Console.WriteLine ("MonoUrho.Setup: This is where we would setup engine flags");
			},
			start=>{
				CreateLogo ();
				SetWindowAndTitleIcon ();
				CreateConsoleAndDebugHud ();
				Console.WriteLine ("OOOPS");
			},
			stop=>{
				Console.WriteLine ("Stop");
			});

		app.SubscribeToKeyDown (HandleKeyDown);
		//var a = app.ResourceCache;
		//var img = app.ResourceCache.GetImage ("Textures/UrhoIcon.png");
		//app.Graphics.SetWindowIcon (img);
		//app.Graphics.SetWindowTitle ("Urho3D#");
		app.Run ();
	}
	
	static void Main ()
	{
		new Demo ().Run ();
	}
}
