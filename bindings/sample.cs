using System.Threading;
using Urho;
using System;

class X {
	static void Main ()
	{
		Console.WriteLine ("Got {0}", ResourceCache.TypeStatic);
		Environment.CurrentDirectory = "/cvs/Urho3D/bin";
		var c = new Context ();
		var app = new Application (c);
		app.SubscribeToKeyDown (x=>Environment.Exit (1));
		var img = app.ResourceCache.GetImage ("Textures/UrhoIcon.png");
		app.Graphics.SetWindowIcon (img);
		app.Graphics.SetWindowTitle ("Urho3D#");
		app.Run ();
	}
}
