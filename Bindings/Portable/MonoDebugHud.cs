using System;
using Urho.Gui;

namespace Urho
{
	public class MonoDebugHud
	{
		const int FrameSampleCount = 20;
		Application application;
		Text text;
		int frameCount = 0;
		DateTime dateTime;
		TimeSpan span;

		public MonoDebugHud(Application application)
		{
			this.application = application;
		}

		void OnPostUpdate(PostUpdateEventArgs args)
		{
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
				if (FpsOnly)
					text.Value = $"{(int)fps} FPS\n{AdditionalText}";
				else if (!InnerCacheDetails)
					text.Value = $"{(int)fps} FPS\n{graphics.NumBatches} batches\n{Runtime.RefCountedCache.Count} MCW\n{GetEngineInfo()}\n{AdditionalText}";
				else
					text.Value = $"{(int)fps} FPS\n{graphics.NumBatches} batches\n{Runtime.RefCountedCache.GetCacheStatus()}\n{AdditionalText}";
			}
		}

		string GetEngineInfo()
		{
			var graphics = application.Graphics;
			return $"{graphics.Width}x{graphics.Height}\n{graphics.ApiName} {(IntPtr.Size == 8 ? "x64" : "x86")}"; //TODO: commit
		}

		public bool InnerCacheDetails { get; set; }

		public bool FpsOnly { get; set; }

		public string AdditionalText { get; set; }

		public void Show()
		{
			Show(Color.White);
		}

		public void Show(Color color, int fontSize = 18)
		{
			if (text != null)
				return;

			text = new Text();
			text.SetColor(color);
			text.VerticalAlignment = VerticalAlignment.Top;
			text.HorizontalAlignment = HorizontalAlignment.Right;
			text.TextAlignment = HorizontalAlignment.Right;
			text.SetFont(CoreAssets.Fonts.AnonymousPro, fontSize);

			application.UI.Root.AddChild(text);
			application.Engine.PostUpdate += OnPostUpdate;
		}

		public void Hide()
		{
			if (text == null)
				return;

			application.Engine.PostUpdate -= OnPostUpdate;
			application.UI.Root.RemoveChild(text, 0);

			text = null;
		}
	}
}
