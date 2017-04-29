using System;
using Windows.ApplicationModel.Core;
using Urho.SharpReality;

namespace Urho
{
	public class UrhoAppViewSource<T> : IFrameworkViewSource where T : StereoApplication
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
