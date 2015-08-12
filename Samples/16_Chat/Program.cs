using System;
using Urho;

class Program
{
	static void Main()
	{
		var context = new Context();
		var code = new _16_Chat(context).Run();
		Console.WriteLine($"Exit code: {code}.\nPress any key to exit...");
		Console.ReadKey();
	}
}