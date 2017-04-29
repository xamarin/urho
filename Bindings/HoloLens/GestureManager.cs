using Windows.Perception.Spatial;
using Windows.UI.Input.Spatial;
using Urho.SharpReality;

namespace Urho
{
	public class GesturesManager
	{
		readonly StereoApplication app;
		readonly SpatialStationaryFrameOfReference referenceFrame;
		readonly SpatialGestureRecognizer tap = new SpatialGestureRecognizer(SpatialGestureSettings.Tap | SpatialGestureSettings.DoubleTap);
		readonly SpatialGestureRecognizer manipulationTranslate = new SpatialGestureRecognizer(SpatialGestureSettings.ManipulationTranslate);
		readonly SpatialGestureRecognizer hold = new SpatialGestureRecognizer(SpatialGestureSettings.Hold);
		//TODO: handle Navigation/Rails (SpatialGestureSettings)

		public GesturesManager(StereoApplication app, SpatialStationaryFrameOfReference referenceFrame)
		{
			this.app = app;
			this.referenceFrame = referenceFrame;

			tap.Tapped += Tap_Tapped;

			hold.HoldCanceled += Hold_HoldCanceled;
			hold.HoldCompleted += Hold_HoldCompleted;
			hold.HoldStarted += Hold_HoldStarted;
			
			manipulationTranslate.ManipulationCanceled += ManipulationTranslate_ManipulationCanceled;
			manipulationTranslate.ManipulationCompleted += ManipulationTranslate_ManipulationCompleted;
			manipulationTranslate.ManipulationStarted += ManipulationTranslate_ManipulationStarted;
			manipulationTranslate.ManipulationUpdated += ManipulationTranslate_ManipulationUpdated;
		}

		public void HandleInteraction(SpatialInteraction interaction)
		{
			if (app.EnableGestureTapped)
				tap.CaptureInteraction(interaction);
			if (app.EnableGestureHold)
				hold.CaptureInteraction(interaction);
			if (app.EnableGestureManipulation)
				manipulationTranslate.CaptureInteraction(interaction);
		}

		void ManipulationTranslate_ManipulationUpdated(SpatialGestureRecognizer sender, SpatialManipulationUpdatedEventArgs args)
		{
			var data = args.TryGetCumulativeDelta(referenceFrame.CoordinateSystem);
			if (data != null)
			{
				var vector = new Vector3(data.Translation.X, data.Translation.Y, -data.Translation.Z);
				Application.InvokeOnMain(() => app.OnGestureManipulationUpdated(vector));
			}
		}

		void ManipulationTranslate_ManipulationStarted(SpatialGestureRecognizer sender, SpatialManipulationStartedEventArgs args)
		{
			Application.InvokeOnMain(() => app.OnGestureManipulationStarted());
		}

		void ManipulationTranslate_ManipulationCompleted(SpatialGestureRecognizer sender, SpatialManipulationCompletedEventArgs args)
		{
			var data = args.TryGetCumulativeDelta(referenceFrame.CoordinateSystem);
			if (data != null)
			{
				var vector = new Vector3(data.Translation.X, data.Translation.Y, -data.Translation.Z);
				Application.InvokeOnMain(() => app.OnGestureManipulationCompleted(vector));
			}
		}

		void ManipulationTranslate_ManipulationCanceled(SpatialGestureRecognizer sender, SpatialManipulationCanceledEventArgs args)
		{
			Application.InvokeOnMain(() => app.OnGestureManipulationCanceled());
		}

		void Hold_HoldStarted(SpatialGestureRecognizer sender, SpatialHoldStartedEventArgs args)
		{
			Application.InvokeOnMain(() => app.OnGestureHoldStarted());
		}

		void Hold_HoldCompleted(SpatialGestureRecognizer sender, SpatialHoldCompletedEventArgs args)
		{
			Application.InvokeOnMain(() => app.OnGestureHoldCompleted());
		}

		void Hold_HoldCanceled(SpatialGestureRecognizer sender, SpatialHoldCanceledEventArgs args)
		{
			Application.InvokeOnMain(() => app.OnGestureHoldCanceled());
		}

		void Tap_Tapped(SpatialGestureRecognizer sender, SpatialTappedEventArgs args)
		{
			if (args.TapCount == 1)
				Application.InvokeOnMain(() => app.OnGestureTapped());
			if (args.TapCount == 2)
				Application.InvokeOnMain(() => app.OnGestureDoubleTapped());
		}
	}
}
