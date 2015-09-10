using System;
using System.Reflection;

namespace Urho.Mac
{
	public static class ApplicationLauncher
	{
		public static int Run(Func<Application> applicationCreator, string resourcesDirectory)
		{
			Environment.CurrentDirectory = resourcesDirectory;
			const string libName = "libmono-urho.dylib";
			if (System.IO.File.Exists(libName))
			{
				return applicationCreator().Run();
			}

			var asm = Assembly.GetExecutingAssembly();
			using (var erStream = asm.GetManifestResourceStream($"Urho.Mac.{libName}"))
			using (var fileStream = System.IO.File.Create(libName))
			{
				if (erStream == null)
					throw new InvalidOperationException($"{libName} was not found as embedded resource");

				erStream.CopyTo(fileStream);
			}
			return applicationCreator().Run();
		}
	}
}
