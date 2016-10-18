using Windows.ApplicationModel.Core;
using Urho.HoloLens;

namespace Urho
{
	public class UrhoAppViewSource<T> : IFrameworkViewSource where T : HoloApplication
	{
		readonly string assets;

		public UrhoAppViewSource() { }

		public UrhoAppViewSource(string assets)
		{
			this.assets = assets;
		}

		public IFrameworkView CreateView() => UrhoAppView.Create<T>(assets);
	}
}
