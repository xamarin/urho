using System;
using System.IO;

namespace Urho.Desktop
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
				if (!Directory.Exists(pathToAssets))
				{
					throw new InvalidDataException($"Directory {pathToAssets} not found");
				}

				const string coreDataFile = "CoreData.pak";
				System.IO.File.Copy(
					sourceFileName: Path.Combine(Environment.CurrentDirectory, coreDataFile), 
					destFileName: Path.Combine(pathToAssets, coreDataFile), 
					overwrite: true);
				Environment.CurrentDirectory = pathToAssets;
			}
			if (Environment.OSVersion.Platform == PlatformID.Win32NT && 
				!Environment.Is64BitProcess &&
				Is64Bit("mono-urho.dll"))
			{
				throw new NotSupportedException("mono-urho.dll is 64bit, but current process is x86 (change target platform from Any CPU/x86 to x64)");
			}
			Application.EngineInited = true;
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
