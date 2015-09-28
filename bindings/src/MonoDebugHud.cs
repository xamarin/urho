using System;

namespace Urho
{
	public class MonoDebugHud
	{
		const int FrameSampleCount = 50;
		Application application;
		Text text;
		Subscription subscription;
		int frameCount = 0;
		DateTime dateTime;
		TimeSpan span;

		public MonoDebugHud(Application application)
		{
			this.application = application;
		}

		void OnPostUpdate(PostUpdateEventArgs args)
		{
			Renderer renderer = application.Renderer;
			Graphics graphics = application.Graphics;

			var now = DateTime.UtcNow;
			span += now - dateTime;
			dateTime = now;
			if (++frameCount >= FrameSampleCount)
			{
				float average = (float)(span.TotalMilliseconds / frameCount);
				float fps = 1000;
				if (average != 0)
					fps /= average;

				frameCount = 0;
				span = TimeSpan.Zero;
				text.Value = $"{(int)fps} FPS\n{graphics.NumBatches} batches\n{Runtime.KnownObjectsCount} MCW";
			}
		}

		public void Show()
		{
			if (text != null)
				return;

			var ui = application.UI;
			var root = ui.Root;
			var cache = application.ResourceCache;

			text = new Text(application.Context);
			text.VerticalAlignment = VerticalAlignment.VA_TOP;
			text.HorizontalAlignment = HorizontalAlignment.HA_RIGHT;
			text.TextAlignment = HorizontalAlignment.HA_RIGHT;
			var xml = cache.GetXmlFile("UI/DefaultStyle.xml");
			text.SetStyle("DebugHudText", xml);

			root.AddChild(text);
			subscription = application.SubscribeToPostUpdate(OnPostUpdate);
		}

		public void Hide()
		{
			if (text == null)
				return;

			subscription.Unsubscribe();
			application.UI.Root.RemoveChild(text, 0);

			text = null;
			subscription = null;
		}
	}
}
