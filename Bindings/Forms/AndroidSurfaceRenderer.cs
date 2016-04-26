using System;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Views;
using Android.Widget;
using Org.Libsdl.App;
using Urho.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRendererAttribute(typeof(UrhoSurface), typeof(AndroidSurfaceRenderer))]
namespace Urho.Forms
{
	public class AndroidSurfaceRenderer : ViewRenderer<UrhoSurface, AndroidUrhoSurface>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<UrhoSurface> e)
		{
			SDLActivity.OnResume();
			var surface = new AndroidUrhoSurface(Context);
			e.NewElement.UrhoApplicationLauncher = surface.Launcher;
			SetNativeControl(surface);
			base.OnElementChanged(e);
		}
	}

	public class AndroidUrhoSurface : FrameLayout
	{
		static TaskCompletionSource<Application> applicationTaskSource; 
		static readonly SemaphoreSlim launcherSemaphore = new SemaphoreSlim(1);
		readonly FrameLayout surfaceViewPlaceholder;

		public AndroidUrhoSurface(Android.Content.Context context) : base(context)
		{
			AddView(surfaceViewPlaceholder = new FrameLayout(Context));
		}

		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			surfaceViewPlaceholder.Measure(
				MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly),
				MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly));
			surfaceViewPlaceholder.Layout(0, 0, r - l, b - t);
		}

		internal async Task<Urho.Application> Launcher(Type type, ApplicationOptions options)
		{
			await launcherSemaphore.WaitAsync();
			SDLActivity.OnDestroy();
			surfaceViewPlaceholder.RemoveAllViews();
			applicationTaskSource = new TaskCompletionSource<Application>();
			Urho.Application.Started += UrhoApplicationStarted;
			var surfaceView = Urho.Droid.UrhoSurface.CreateSurface((Activity)Context, type, options);
			SDLActivity.OnResume();
			surfaceViewPlaceholder.AddView(surfaceView);
			return await applicationTaskSource.Task;
		}

		void UrhoApplicationStarted()
		{
			Urho.Application.Started -= UrhoApplicationStarted;
			applicationTaskSource?.TrySetResult(Application.Current);
			launcherSemaphore.Release();
		}
	}
}