using System;
using System.IO;
using Urho;
using Urho.Gui;

namespace Playgrounds.NetCoreApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Dir: " + Environment.CurrentDirectory);
			// the current directory is not "bin" ?? https://github.com/dotnet/project-system/issues/589
			// workaround:
			var coreDataPak = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "CoreData.pak");


			new HelloWorld(
				new ApplicationOptions(@"C:\prj\urho\Urho3D\Source\bin")
				{
					AutoloadCoreData = false
				}).Run();
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
