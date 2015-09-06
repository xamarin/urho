using System;
using System.Linq;

namespace Urho
{
	class Program
	{
		static void Main(string[] args)
		{
			Environment.CurrentDirectory = @"../../Urho3D/Source/bin";
			Sample sample = null;
#if __MonoCS__
			if (args.Length > 0)
			{
				sample = ParseSampleFromNumber(args[0]);
			}
			else
			{
				sample = new _11_Physics(new Context());
			}
#else
			while (sample == null)
			{
				Console.WriteLine("Enter a sample number [1-40]:");
				sample = ParseSampleFromNumber(Console.ReadLine());
			}
#endif
			var code = sample.Run();
			Console.WriteLine($"Exit code: {code}. Press any key to exit...");
			Console.ReadKey();
		}


		static Sample ParseSampleFromNumber(string input)
		{
			var samples = typeof(Sample).Assembly.GetTypes().Where(t => t.BaseType == typeof(Sample)).ToArray();
			int number;
			if (!int.TryParse(input, out number))
			{
				Console.WriteLine("Invalid format.");
				return null;
			}

			var sample = samples.FirstOrDefault(s => s.Name.StartsWith($"_{number.ToString("00")}"));
			if (sample == null)
			{
				Console.WriteLine("Sample was not found");
				return null;
			}

			return (Sample)Activator.CreateInstance(sample, new Context());
		}
	}
}
