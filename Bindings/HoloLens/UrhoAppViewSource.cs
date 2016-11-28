using System;
using Windows.ApplicationModel.Core;
using Urho.HoloLens;

namespace Urho
{
	public class UrhoAppViewSource<T> : IFrameworkViewSource where T : HoloApplication
	{
		readonly ApplicationOptions opts;

		public event Action<UrhoAppView> UrhoAppViewCreated;

		public UrhoAppViewSource() { }

		public UrhoAppViewSource(ApplicationOptions opts)
		{
			this.opts = opts;
		}

		public IFrameworkView CreateView()
		{
			var appView = UrhoAppView.Create<T>(opts);
			UrhoAppViewCreated?.Invoke(appView);
			return appView;
		}
	}
}
