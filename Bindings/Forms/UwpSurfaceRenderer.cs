using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRendererAttribute(typeof(Urho.Forms.UrhoSurface), typeof(Urho.Forms.UwpSurfaceRenderer))]

namespace Urho.Forms
{

	public class UwpSurfaceRenderer : ViewRenderer<Urho.Forms.UrhoSurface, Urho.UWP.UrhoSurface>
	{
		TaskCompletionSource<Urho.UWP.UrhoSurface> surfaceTask = new TaskCompletionSource<UWP.UrhoSurface>();
		SemaphoreSlim semaphore = new SemaphoreSlim(1);

		protected override void OnElementChanged(ElementChangedEventArgs<UrhoSurface> e)
		{
			if (e.NewElement != null)
			{
				var urhoSurface = new Urho.UWP.UrhoSurface();
				e.NewElement.RegisterRunner(UrhoLauncher);
				SetNativeControl(urhoSurface);
				surfaceTask.TrySetResult(urhoSurface);
			}
			base.OnElementChanged(e);
		}

		async Task<Application> UrhoLauncher(Type type, ApplicationOptions opts)
		{
			try
			{
				await semaphore.WaitAsync();
				var urhoSurface = await surfaceTask.Task;
				await urhoSurface.WaitLoadedAsync();
				return urhoSurface.Run(type, opts);
			}
			finally
			{
				semaphore.Release();
			}
		}
	}
}
