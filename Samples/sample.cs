using System.Threading;
using System.Runtime.InteropServices;
using Urho;

class Demo {
	
	static void Main ()
	{
		var c = new Context ();
		//new Sample (c).Run ();
		//new _01_HelloWorld (c).Run ();
		//new _04_StaticScene (c).Run ();
		//new _05_AnimatingScene (c).Run ();
		new _23_Water(c).Run ();
	}
}

