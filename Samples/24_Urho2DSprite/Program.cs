using System;
using Urho;

class Program
{
	static void Main()
	{
		var context = new Context();
		var code = new _24_Urho2DSprite(context).Run();
		Console.WriteLine($"Exit code: {code}.\nPress any key to exit...");
		Console.ReadKey();
	}
}