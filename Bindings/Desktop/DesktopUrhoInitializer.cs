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

		internal static void OnInited()
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
				return;

			var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			bool is64BitProcess = IntPtr.Size == 8;
			bool is64BitLib = Is64Bit(Path.Combine(currentPath, Consts.NativeImport + ".dll"));

			if (is64BitProcess && !is64BitLib)
				throw new NotSupportedException("mono-urho.dll is 32bit, but current process is x64");

			if (!is64BitProcess && is64BitLib)
				throw new NotSupportedException("mono-urho.dll is 64bit, but current process is x86 (change target platform from Any CPU/x86 to x64). Or rename mono-urho_32bit.dll to mono-urho.dll in the output dir.");
		}

		public static void CopyEmbeddedCoreDataTo(string destinationFolder)
		{
			using (Stream input = typeof(SimpleApplication).Assembly.GetManifestResourceStream("Urho.CoreData.pak"))
			using (Stream output = File.Create(Path.Combine(destinationFolder, "CoreData.pak")))
				input.CopyTo(output);
		}

		static bool Is64Bit(string file)
		{
			using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
			using (var binaryReader = new BinaryReader(fileStream))
			{
				if (binaryReader.ReadUInt16() == 23117)
				{
					fileStream.Seek(0x3A, SeekOrigin.Current);
					fileStream.Seek(binaryReader.ReadUInt32(), SeekOrigin.Begin);
					if (binaryReader.ReadUInt32() == 17744)
					{
						fileStream.Seek(20, SeekOrigin.Current);
						return binaryReader.ReadUInt16() != 0x10B; //PE32
					}
				}
			}
			return false;
		}
	}
}
