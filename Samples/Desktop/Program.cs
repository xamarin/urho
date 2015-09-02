using System;
using System.Linq;

namespace Urho
{
	class Program
	{
		static void Main(string[] args)
		{
			//Environment.CurrentDirectory = "/cvs/Urho3D/bin"; //Mac
			Environment.CurrentDirectory = @"C:\Projects\urho_x64\bin"; //Windows

			var sample = GetSampleFromUserInput();
			var code = sample.Run();
			Console.WriteLine($"Exit code: {code}. Press any key to exit...");
			Console.ReadKey();
		}

		static Sample GetSampleFromUserInput()
		{
			Console.WriteLine("Enter a sample number [1-40]:");
			var samples = typeof(Sample).Assembly.GetTypes().Where(t => t.BaseType == typeof(Sample)).ToArray();
			string input = Console.ReadLine();
			int number;
			if (!int.TryParse(input, out number))
			{
				Console.WriteLine("Invalid format.");
				return GetSampleFromUserInput();
			}

			var sample = samples.FirstOrDefault(s => s.Name.StartsWith($"_{number.ToString("00")}"));
			if (sample == null)
			{
				Console.WriteLine("Sample was not found");
				return GetSampleFromUserInput();
			}

			return (Sample)Activator.CreateInstance(sample, new Context());
		}
	}
}
