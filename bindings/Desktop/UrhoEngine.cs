using System;

namespace Urho
{
	public static class UrhoEngine
	{
		public static int Init<TUrhoApplication>(string resourceDirectory) where TUrhoApplication : Application
		{
			SetResourceDirectory(resourceDirectory);
			// https://github.com/dotnet/roslyn/issues/2206 :(
			return ((Application) Activator.CreateInstance(typeof (TUrhoApplication), new Context())).Run();
		}

		public static int Init(Func<Application> applicationCreator, string resourcesDirectory)
		{
			SetResourceDirectory(resourcesDirectory);
			return applicationCreator().Run();
		}

		static void SetResourceDirectory(string resourceDirectory)
		{
			Environment.CurrentDirectory = resourceDirectory;
			if (Environment.OSVersion.Platform == PlatformID.Win32NT && !Environment.Is64BitProcess)
			{
				throw new NotSupportedException("MonoUrho for Windows supports only 64bit mode (change target platform from Any CPU or x86 to x64)");
			}
		}
	}
}
