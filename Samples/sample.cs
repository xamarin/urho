using System;
using Urho;

class Demo {
	
	static void Main ()
    {
        var c = new Context ();
		//var code = new _01_HelloWorld(c).Run ();
        var code = new _23_Water(c).Run();
        Console.WriteLine($"Exit code: {code}. Press any key to exit...");
	    Console.ReadKey();
    }
}

