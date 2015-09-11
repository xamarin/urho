using System;

namespace Urho
{
	public static class ApplicationLauncher
	{
		public static int Run(Func<Application> applicationCreator, string resourcesDirectory)
		{
			Environment.CurrentDirectory = resourcesDirectory;
			if (Environment.OSVersion.Platform == PlatformID.Win32NT && !Environment.Is64BitProcess)
			{
				throw new NotSupportedException("MonoUrho for Windows only supports 64 bit yet.");
			}
			return applicationCreator().Run();
		}
	}
}
