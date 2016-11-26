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

		static bool Is64Bit(string dllPath)
		{
			using (var fs = new FileStream(dllPath, FileMode.Open, FileAccess.Read))
			using (var br = new BinaryReader(fs))
			{
				fs.Seek(0x3c, SeekOrigin.Begin);
				var peOffset = br.ReadInt32();
				fs.Seek(peOffset, SeekOrigin.Begin);
				var value = br.ReadUInt16();

				const ushort IMAGE_FILE_MACHINE_AMD64 = 0x8664;
				const ushort IMAGE_FILE_MACHINE_IA64 = 0x200;
				return value == IMAGE_FILE_MACHINE_AMD64 ||
						value == IMAGE_FILE_MACHINE_IA64;
			}
		}
	}
}
