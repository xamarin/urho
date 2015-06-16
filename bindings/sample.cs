using System.Threading;
using Urho;
using System;

class X {
	static void Main ()
	{
		var c = new Context ();
		var app = new Application (c);
		app.Run ();
	}
}
