using System;
using System.IO;

namespace Urho
{
	public static class UrhoEngine
	{
		/// <summary>
		/// Init engine
		/// </summary>
		/// <param name="pathToAssets">Path to a folder containing "Data" folder. CurrentDirectory if null</param>
		public static void Init(string pathToAssets = null)
		{
			if (!string.IsNullOrEmpty(pathToAssets))
			{
				const string coreDataFile = "CoreData.pak";
				System.IO.File.Copy(
					sourceFileName: Path.Combine(Environment.CurrentDirectory, coreDataFile), 
					destFileName: Path.Combine(pathToAssets, coreDataFile), 
					overwrite: true);
				Environment.CurrentDirectory = pathToAssets;
			}
			if (Environment.OSVersion.Platform == PlatformID.Win32NT && !Environment.Is64BitProcess)
			{
				throw new NotSupportedException("MonoUrho for Windows supports only 64bit mode (change target platform from Any CPU or x86 to x64)");
			}
		}
	}
}
