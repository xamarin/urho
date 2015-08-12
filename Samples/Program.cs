using System;
using System.Linq;
using System.Reflection;
using System.Runtime;
using Urho;

class Program {

	static void Main()
	{
		Environment.CurrentDirectory = "/cvs/Urho3D/bin"; //Mac
		//Environment.CurrentDirectory = @"C:\Projects\urho_x64\bin"; //Windows
		
		var c = new Context();
		Sample sample = null;

		//sample = new _07_Billboards(c);
		sample = GetSample(c, number: 2);

		var code = sample.Run();
		if (code != 0)
		{
			Console.WriteLine($"Exit code: {code}. Press any key to exit...");
			Console.ReadKey();
		}
	}

	private static Sample GetSample(Context c, int number)
	{
		var types = Assembly.GetExecutingAssembly().GetTypes()
		.Where(t => t.IsSubclassOf(typeof(Sample)) && t.Name.StartsWith("_")).ToArray();
		string prefix = "_" + number.ToString("00");
		var type = types.FirstOrDefault(t => t.Name.StartsWith(prefix));
		if (type == null)
		{
			return null;
		}
		return (Sample)Activator.CreateInstance(type, c);
	}

}

