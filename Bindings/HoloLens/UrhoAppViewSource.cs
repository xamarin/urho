using Windows.ApplicationModel.Core;
using Urho.HoloLens;

namespace Urho
{
	public class UrhoAppViewSource<T> : IFrameworkViewSource where T : HoloApplication
	{
		readonly ApplicationOptions opts;

		public UrhoAppViewSource() { }

		public UrhoAppViewSource(ApplicationOptions opts)
		{
			this.opts = opts;
		}

		public IFrameworkView CreateView() => UrhoAppView.Create<T>(opts);
	}
}
