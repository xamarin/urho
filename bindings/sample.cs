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
		var logoTexture = cache.GetTexture2D ("Textures/LogoLarge.png");
		if (logoTexture == null)
			return;

		ui = app.UI;
		var logoSprite = ui.Root.CreateChild (Sprite.TypeStatic, "", 0xffffffff);
		//logoSprite.SetTexture (logoTexture);
	}
	
	public void Run ()
	{
		var c = new Context ();
		Console.WriteLine (c.Handle);
		var app = new Application (
			c,
			setup=>{
				Console.WriteLine ("MonoUrho.Setup: This is where we would setup engine flags");
			},
			start=>{
				//CreateLogo ();
				Console.WriteLine ("OOOPS");
			},
			stop=>{
				Console.WriteLine ("Stop");
			});

		app.SubscribeToKeyDown (
			x=> {
				Console.WriteLine ("keystroke received, terminating");
				Environment.Exit (1);
			});

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
