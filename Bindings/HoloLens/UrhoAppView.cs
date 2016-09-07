using System;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Graphics.Holographic;
using Windows.Media.SpeechRecognition;
using Windows.Perception.Spatial;
using Windows.Perception.Spatial.Surfaces;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Input.Spatial;

namespace Urho.HoloLens
{
	public class UrhoAppView<T> : IFrameworkView, IDisposable where T : HoloApplication
	{
		string assetsPakName;
		bool windowVisible = true;
		bool windowClosed;
		bool assetsLoaded;
		bool appInited;

		HolographicFrame currentFrame;
		SpeechRecognizer speechRecognizer;

		public HolographicSpace HolographicSpace { get; private set; }
		public HoloApplication Game { get; private set; }
		public SpatialInteractionManager InteractionManager { get; private set; }
		public SpatialStationaryFrameOfReference ReferenceFrame { get; private set; }

		public UrhoAppView(string assetsPakName)
		{
			this.assetsPakName = assetsPakName;
			windowVisible = true;
		}

		public void Dispose() {}

		#region IFrameworkView Members

		/// <summary>
		/// The first method called when the IFrameworkView is being created.
		/// Use this method to subscribe for Windows shell events and to initialize your app.
		/// </summary>
		public void Initialize(CoreApplicationView applicationView)
		{
			applicationView.Activated += this.OnViewActivated;

			CoreApplication.Suspending += this.OnSuspending;
			CoreApplication.Resuming += this.OnResuming;
		}

		/// <summary>
		/// Called when the CoreWindow object is created (or re-created).
		/// </summary>
		public void SetWindow(CoreWindow window)
		{
			window.KeyDown += this.OnKeyPressed;
			window.Closed += this.OnWindowClosed;
			window.VisibilityChanged += this.OnVisibilityChanged;
			HolographicSpace = HolographicSpace.CreateForCoreWindow(window);
		}

		/// <summary>
		/// The Load method can be used to initialize scene resources or to load a
		/// previously saved app state.
		/// </summary>
		public void Load(string entryPoint)
		{
		}

		async void LoadAssets(string[] pakFiles)
		{
			ReferenceFrame = SpatialLocator.GetDefault().CreateStationaryFrameOfReferenceAtCurrentLocation();
			foreach (var assetName in pakFiles)
			{
				if (await ApplicationData.Current.LocalFolder.TryGetItemAsync(assetName) == null)
				{
					var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///" + assetName));
					var folder = ApplicationData.Current.LocalFolder;
					await file.CopyAsync(folder);
				}
			}
			await CopyEmbeddedResourceToLocalFolder("Urho.CoreData.pak", "CoreData.pak");
			assetsLoaded = true;
			InteractionManager = SpatialInteractionManager.GetForCurrentView();
			InteractionManager.SourcePressed += OnSourcePressed;
		}

		static async Task CopyEmbeddedResourceToLocalFolder(string embeddedResource, string destFileName)
		{
			if (await ApplicationData.Current.LocalFolder.TryGetItemAsync(destFileName) != null)
				return;

			var file = ApplicationData.Current.LocalFolder.CreateFileAsync(destFileName).GetAwaiter().GetResult();
			using (var fileStream = file.OpenStreamForWriteAsync().GetAwaiter().GetResult())
			using (var embeddedSteam = typeof(UrhoAppView<>).GetTypeInfo().Assembly.GetManifestResourceStream(embeddedResource))
			{
				embeddedSteam.CopyTo(fileStream);
			}
		}

		protected async Task RegisterCortanaCommands(string[] commands)
		{
			speechRecognizer = new SpeechRecognizer();
			var constraint = new SpeechRecognitionListConstraint(commands);
			speechRecognizer.Constraints.Clear();
			speechRecognizer.Constraints.Add(constraint);
			var result = await speechRecognizer.CompileConstraintsAsync();
			if (result.Status == SpeechRecognitionResultStatus.Success)
			{
				await speechRecognizer.ContinuousRecognitionSession.StartAsync();
				speechRecognizer.ContinuousRecognitionSession.ResultGenerated += (s, e) =>
				{
					if (e.Result.RawConfidence > 0.4f)
					{
						Application.InvokeOnMain(() => Game.OnCortanaCommand(e.Result.Text));
					}
				};
			}
		}

		unsafe void OnSourcePressed(SpatialInteractionManager sender, SpatialInteractionSourceEventArgs args)
		{
			var point = args.State.TryGetPointerPose(ReferenceFrame.CoordinateSystem);
			if (point != null)
			{
				var forwardDx = point.Head.ForwardDirection;
				var posDx = point.Head.Position;
				var upDx = point.Head.UpDirection;
				var forward = *(Vector3*)(void*)&forwardDx;
				var position = *(Vector3*)(void*)&posDx;
				var up = *(Vector3*)(void*)&upDx;
				Application.InvokeOnMain(() => Game.OnGestureClick(forward, up, position));
			}
		}

		public unsafe void Run()
		{
			LoadAssets(new[] { assetsPakName });
			CoreWindow.GetForCurrentThread().CustomProperties.Add("HolographicSpace", HolographicSpace);
			UrhoAppViewPinvoke.InitializeSpace();

			while (!windowClosed)
			{
				if (assetsLoaded && !appInited)
				{
					appInited = true;
					Game = (T) Activator.CreateInstance(typeof(T), assetsPakName);
					Game.Run();
				}

				if (windowVisible && (null != HolographicSpace))
				{
					if (Game != null)
					{
						currentFrame = HolographicSpace.CreateNextFrame();
						currentFrame.UpdateCurrentPrediction();

						var prediction = currentFrame.CurrentPrediction;
						if (prediction.CameraPoses.Count < 1)
							continue;
						var cameraPose = prediction.CameraPoses[0];

						var viewBox = cameraPose.TryGetViewTransform(ReferenceFrame.CoordinateSystem);
						if (viewBox != null)
						{
							Matrix4x4 leftViewMatrixDx = viewBox.Value.Left;
							Matrix4x4 rightViewMatrixDx = viewBox.Value.Right;
							Matrix4x4 leftProjMatrixDx = cameraPose.ProjectionTransform.Left;
							Matrix4x4 rightProjMatrixDx = cameraPose.ProjectionTransform.Right;

							Matrix4 leftViewMatrixUrho = *(Matrix4*)(void*)&leftViewMatrixDx;
							Matrix4 rightViewMatrixUrho = *(Matrix4*)(void*)&rightViewMatrixDx;
							Matrix4 leftProjMatrixUrho = *(Matrix4*)(void*)&leftProjMatrixDx;
							Matrix4 rightProjMatrixUrho = *(Matrix4*)(void*)&rightProjMatrixDx;
							Game.UpdateStereoView(leftViewMatrixUrho, rightViewMatrixUrho, leftProjMatrixUrho, rightProjMatrixUrho);
						}

						var parameters = currentFrame.GetRenderingParameters(cameraPose);
						if (Game.FocusWorldPoint != Vector3.Zero)
							parameters.SetFocusPoint(ReferenceFrame.CoordinateSystem, 
								new System.Numerics.Vector3(
									 Game.FocusWorldPoint.X, 
									 Game.FocusWorldPoint.Y, 
									-Game.FocusWorldPoint.Z)); //LH->RH

						Game.Engine.RunFrame();
						currentFrame.PresentUsingCurrentPrediction();
					}
					CoreWindow.GetForCurrentThread().Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessAllIfPresent);
				}
				else
				{
					CoreWindow.GetForCurrentThread().Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessOneAndAllPending);
				}
			}
		}

		/// <summary>
		/// Terminate events do not cause Uninitialize to be called. It will be called if your IFrameworkView
		/// class is torn down while the app is in the foreground.
		// This method is not often used, but IFrameworkView requires it and it will be called for
		// holographic apps.
		/// </summary>
		public void Uninitialize()
		{
		}

		#endregion

		#region Application lifecycle event handlers

		/// <summary>
		/// Called when the app view is activated. Activates the app's CoreWindow.
		/// </summary>
		void OnViewActivated(CoreApplicationView sender, IActivatedEventArgs args)
		{
			// Run() won't start until the CoreWindow is activated.
			sender.CoreWindow.Activate();
		}

		void OnSuspending(object sender, SuspendingEventArgs args)
		{
			// Save app state asynchronously after requesting a deferral. Holding a deferral
			// indicates that the application is busy performing suspending operations. Be
			// aware that a deferral may not be held indefinitely; after about five seconds,
			// the app will be forced to exit.
			var deferral = args.SuspendingOperation.GetDeferral();
			Task.Run(() => deferral.Complete());
		}

		void OnResuming(object sender, object args)
		{
		}

		#endregion;

		#region Window event handlers

		void OnVisibilityChanged(CoreWindow sender, VisibilityChangedEventArgs args)
		{
			windowVisible = args.Visible;
		}

		void OnWindowClosed(CoreWindow sender, CoreWindowEventArgs arg)
		{
			windowClosed = true;
		}

		#endregion

		#region Input event handlers

		void OnKeyPressed(CoreWindow sender, KeyEventArgs args)
		{
		}

		#endregion

	}

	static class UrhoAppViewPinvoke// we can't have DllImport in a class with a generic parameter
	{
		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		public static extern void InitializeSpace();
	}
}