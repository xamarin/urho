using System;
using System.Threading.Tasks;
using Windows.System.Threading;
using Windows.UI.Xaml.Controls;

namespace Urho.UWP
{
	public class UrhoSurface : SwapChainPanel
	{
		bool paused;
		bool stop;
		bool inited;

		public TGame Run<TGame>(ApplicationOptions options = null) where TGame : Urho.Application
		{
			return (TGame) Run(typeof(TGame), options);
		}
		
		public Application Run(Type appType, ApplicationOptions options = null)
		{
			stop = false;
			paused = false;
			inited = false;
			Sdl.SetMainReady();
			
			var app = (Application)Activator.CreateInstance(appType, options);
			app.Run();
			Sdl.SendWindowEvent(SdlWindowEvent.SDL_WINDOWEVENT_RESIZED, (int)this.ActualWidth, (int)this.ActualHeight);
			inited = true;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
			ThreadPool.RunAsync(_ =>
				{
					while (stopRenderingTask == null)
					{
						if (!paused && !app.IsExiting)
						{
							app.Engine.RunFrame();
						}
					}
					stopRenderingTask.TrySetResult(true);
					stopRenderingTask = null;
				});
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
			return app;
		}

		internal static TaskCompletionSource<bool> stopRenderingTask;
		internal static Task StopRendering()
		{
			stopRenderingTask = new TaskCompletionSource<bool>();
			return stopRenderingTask.Task;
		}

		public void Pause()
		{
			paused = true;
		}

		public void Resume()
		{
			paused = false;
		}
	}
}
