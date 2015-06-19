using System.Threading;
using Urho;
using System;
using System.Runtime.InteropServices;

class X {
	[DllImport ("mono-urho")]
	extern static void check (IntPtr h);
			
	static void Main ()
	{
		Environment.CurrentDirectory = "/cvs/Urho3D/bin";
		var c = new Context ();
		var app = new Application (c);

		// Observations: at this point, the app needs to run, so that the initialization below works.
		// This will happen in the Application.Start, that we need to manually proxy to managed
		// code.

		//
		// To configure stuff, we need to access "_engineParameters" from the "Setup" method,
		// another callback, that pokes into a hashtable some values (which can also be set from the
		// command line.
		
		app.SubscribeToKeyDown (y=>Environment.Exit (1));
		var img = app.ResourceCache.GetImage ("Data/Textures/UrhoIcon.png");

		// The line below crashes because app.Graphics is null until app.Run starts.
		app.Graphics.SetWindowIcon (img);
		app.Graphics.SetWindowTitle ("Urho3D#");
		//app.Run ();
	}
}
