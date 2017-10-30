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

[assembly: ExportRendererAttribute(typeof(Urho.Forms.UrhoSurface), typeof(AndroidSurfaceRenderer))]
namespace Urho.Forms
{
	public class AndroidSurfaceRenderer : ViewRenderer<Urho.Forms.UrhoSurface, AndroidUrhoSurface>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Urho.Forms.UrhoSurface> e)
		{
			SDLActivity.OnResume();
			var surface = new AndroidUrhoSurface(Context);
			if (e.NewElement == null)
				return;

			e.NewElement.RegisterRunner(surface.Launcher);
			SetNativeControl(surface);
			base.OnElementChanged(e);
		}
	}

	public class AndroidUrhoSurface : FrameLayout
	{
		static readonly SemaphoreSlim launcherSemaphore = new SemaphoreSlim(1);
		FrameLayout surfaceViewPlaceholder;

		Urho.Droid.UrhoSurfacePlaceholder surfaceView;

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
			Urho.Application.StopCurrent();
			surfaceViewPlaceholder.RemoveAllViews();
			surfaceView = Urho.Droid.UrhoSurface.CreateSurface((Activity)Context);
			surfaceViewPlaceholder.AddView(surfaceView);
			var app = await surfaceView.Show(type, options);
			launcherSemaphore.Release();
			return app;
		}
	}
}