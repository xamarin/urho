using System;
using System.Linq;
using static System.Console;

namespace Urho
{
	class Program
	{
		static void Main(string[] args)
		{
			Environment.CurrentDirectory = @"..\..\Submodules\Urho3D\bin";
			var sample = GetSampleFromUserInput();
			var code = sample.Run();
			WriteLine($"Exit code: {code}. Press any key to exit...");
		}

		static Sample GetSampleFromUserInput()
		{
			WriteLine("Enter a sample number [1-40]:");
			var samples = typeof(Sample).Assembly.GetTypes().Where(t => t.BaseType == typeof(Sample)).ToArray();
			string input = Console.ReadLine();
			int number;
			if (!int.TryParse(input, out number))
			{
				WriteLine("Invalid format.");
				return GetSampleFromUserInput();
			}

			var sample = samples.FirstOrDefault(s => s.Name.StartsWith($"_{number.ToString("00")}"));
			if (sample == null)
			{
				WriteLine("Sample was not found");
				return GetSampleFromUserInput();
			}

			return (Sample)Activator.CreateInstance(sample, new Context());
		}
	}
}
