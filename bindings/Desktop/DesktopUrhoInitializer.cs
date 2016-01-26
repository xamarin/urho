using System;
using System.IO;

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
			//unlike the OS X, windows doesn't support FAT binaries
			if (Environment.OSVersion.Platform == PlatformID.Win32NT &&
				!Environment.Is64BitProcess &&
				Is64Bit("mono-urho.dll"))
			{
				throw new NotSupportedException("mono-urho.dll is 64bit, but current process is x86 (change target platform from Any CPU/x86 to x64)");
			}
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
