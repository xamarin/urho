using System;
using System.Linq;

namespace Urho.Samples.Desktop
{
	class Program
	{
		static System.Type[] samples;

		static void Main(string[] args)
		{
			FindAvailableSamplesAndPrint();
            System.Type selectedSampleType = null;

			if (args.Length > 0)
				selectedSampleType = ParseSampleFromNumber(args[0]);

			if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				while (selectedSampleType == null)
				{
					Console.WriteLine("Enter a sample number [1-40]:");
					selectedSampleType = ParseSampleFromNumber(Console.ReadLine());
				}
			}
			else if (selectedSampleType == null)
				return;

			var resourcesDirectory = @"../../../../Urho3D/Source/bin";
			var code = ApplicationLauncher.Run(() => (Application)Activator.CreateInstance(selectedSampleType, new Context()), resourcesDirectory);
			Console.WriteLine($"Exit code: {code}. Press any key to exit...");
			Console.ReadKey();
		}

		static System.Type ParseSampleFromNumber(string input)
		{
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

		static void FindAvailableSamplesAndPrint()
		{
			samples = typeof(Sample).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Application)) && t != typeof(Sample)).ToArray();
			foreach (var sample in samples)
				Console.WriteLine(sample.Name);
			Console.WriteLine();
		}
	}
}
