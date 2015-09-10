using System;
using System.Linq;
using Urho.Mac;

namespace Urho.Samples.Mac
{
	class Program
	{
		static void Main(string[] args)
		{
			System.Type sampleType = null;

			if (args.Length > 0)
			{
				sampleType = ParseSampleFromNumber(args[0]);
				if (sampleType == null)
					return;
			}

			if (args.Length == 0)
				sampleType = typeof(_23_Water);

			var resourcesDirectory = @"../../../../Urho3D/Source/bin";
			var code = ApplicationLauncher.Run(() => (Application)Activator.CreateInstance(sampleType, new Context()), resourcesDirectory);
			Console.WriteLine($"Exit code: {code}. Press any key to exit...");
			Console.ReadKey();
		}

		static System.Type ParseSampleFromNumber(string input)
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

			return sample;
		}
	}
}
