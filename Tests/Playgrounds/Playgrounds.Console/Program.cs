using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace Playgrounds.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			Urho.Desktop.DesktopUrhoInitializer.AssetsDirectory = @"C:\Projects\urho\Urho3D\Source\bin";

			const float scale = 0.65f;
			const float width = 1280f * 2 * scale;
			const float height = 720f * scale;

			var app = new StereoModePerformance(new ApplicationOptions("Data") {Width = (int) width, Height = (int) height});
			app.Run();
		}
	}
}
