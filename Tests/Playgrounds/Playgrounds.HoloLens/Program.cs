using System;
using Windows.ApplicationModel.Core;
using Urho;

namespace Playgrounds.HoloLens
{
	internal class Program
	{
		[MTAThread]
		private static void Main() => CoreApplication.Run(new UrhoAppViewSource<PerformanceTests>());
	}
}