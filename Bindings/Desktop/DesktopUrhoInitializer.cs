using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

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

		[DllImport("kernel32.dll")]
		static extern IntPtr LoadLibrary(string dllToLoad);

		internal static void OnInited()
		{
			if (Environment.OSVersion.Platform == PlatformID.MacOSX ||
				Environment.OSVersion.Platform == PlatformID.Unix)
				return; //on macOS/Linux the libs are fat and there is no DirectX

			var isD3D = ApplicationOptions.LastUsedOptions?.UseDirectX11 == true;
			var rootFolder = Path.GetDirectoryName(typeof(DesktopUrhoInitializer).Assembly.Location);
			var relativePathToLib = Path.Combine($@"Win{(IntPtr.Size == 8 ? "64" : "32")}_{(isD3D ? "DirectX" : "OpenGL")}", $"{Consts.NativeImport}.dll");
			var file = Path.Combine(rootFolder, relativePathToLib);

			LoadLibrary(file);
		}
	}
}
