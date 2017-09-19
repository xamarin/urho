using System;
using System.IO;
using System.Reflection;

namespace Urho.Desktop
{
	public static class DesktopUrhoInitializer
	{
		static string assetsDirectory;

		/// <summary>
		/// Path to a folder containing "Data" folder. CurrentDirectory if null
		/// </summary>
		public static string AssetsDirectory
		{
			get { return assetsDirectory; }
			set
			{
				assetsDirectory = value;
				if (!string.IsNullOrEmpty(assetsDirectory))
				{
					if (!Directory.Exists(assetsDirectory))
					{
						throw new InvalidDataException($"Directory {assetsDirectory} not found");
					}

					const string coreDataFile = "CoreData.pak";
					System.IO.File.Copy(
						sourceFileName: Path.Combine(Environment.CurrentDirectory, coreDataFile),
						destFileName: Path.Combine(assetsDirectory, coreDataFile),
						overwrite: true);
					Environment.CurrentDirectory = assetsDirectory;
				}
			}
		}

		internal static void OnInited() {}
	}
}
