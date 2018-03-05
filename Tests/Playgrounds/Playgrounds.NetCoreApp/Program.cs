using System;
using System.IO;
using System.Runtime.InteropServices;
using Urho;
using Urho.Gui;

namespace Playgrounds.NetCoreApp
{
	class Program
	{
		[DllImport("kernel32.dll")]
		static extern IntPtr LoadLibrary(string dllToLoad);

		static void Main(string[] args)
		{
			throw new NotImplementedException("Not fully implemented yet, requires changes in Urho3D (FileSystem)");
			Console.WriteLine("Dir: " + Environment.CurrentDirectory);
			// the current directory is not "bin" ?? https://github.com/dotnet/project-system/issues/589
			// workaround:
			var coreDataPak = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "CoreData.pak");
		}
	}

	public class HelloWorld : Application
	{
		public HelloWorld(ApplicationOptions options = null) : base(options) { }

		protected override void Start()
		{
			var cache = ResourceCache;
			var helloText = new Text()
			{
				Value = "Hello World from UrhoSharp for .NET Core",
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			helloText.SetColor(new Color(0f, 1f, 0f));
			helloText.SetFont(font: CoreAssets.Fonts.AnonymousPro, size: 30);
			UI.Root.AddChild(helloText);


			// Subscribe to Esc key:
			Input.SubscribeToKeyDown(args => { if (args.Key == Key.Esc) Exit(); });
		}
	}
}
