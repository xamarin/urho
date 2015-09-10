using System;
using System.Reflection;

namespace Urho
{
	public static class ApplicationLauncher
	{
		public static void Run(Application application)
		{
			const string libName = "libmono-urho.dylib";
            if (System.IO.File.Exists(libName))
				return;

			var asm = Assembly.GetExecutingAssembly();
			using (var erStream = asm.GetManifestResourceStream($"{asm.FullName}.{libName}"))
			using (var fileStream = System.IO.File.Create(libName))
			{
				if (erStream == null)
					throw new InvalidOperationException($"{libName} was not found as embedded resource");

				erStream.CopyTo(fileStream);
			}
		}
	}
}
