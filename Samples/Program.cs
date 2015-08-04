using System;
using System.Linq;
using System.Reflection;
using Urho;

class Program {

	static void Main()
	{
		Environment.CurrentDirectory = "/cvs/Urho3D/bin"; //Mac
		//Environment.CurrentDirectory = @"C:\Projects\UrhoGen\bin"; //Windows

		var c = new Context();
		Sample sample = null;

		//sample = new _02_HelloGUI(c);
		sample = new _07_Billboards(c);
		//sample = AskUserForSampleNumber(c);

		var code = sample.Run();
		if (code != 0)
		{
			Console.WriteLine($"Exit code: {code}. Press any key to exit...");
			Console.ReadKey();
		}
	}

	private static Sample AskUserForSampleNumber(Context c)
	{
		Console.WriteLine("Enter sample number [1-39]:");
		int number = int.Parse(Console.ReadLine());
		var types = Assembly.GetExecutingAssembly().GetTypes()
			.Where(t => t.IsSubclassOf(typeof(Sample)) && t.Name.StartsWith("_")).ToArray();
		string prefix = "_" + number.ToString("00");
		var type = types.FirstOrDefault(t => t.Name.StartsWith(prefix));
		if (type == null)
		{
			Console.WriteLine($"Sample {number} not found");
			return AskUserForSampleNumber(c);
		}
		return (Sample)Activator.CreateInstance(type, c);
	}
}

