using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Graphics.Holographic;
using Windows.Perception.Spatial;
using Windows.UI.Core;
using Windows.UI.Input.Spatial;

namespace Urho.SharpReality
{
	public class UrhoAppView : IFrameworkView, IDisposable 
	{
		Type holoAppType;
		ApplicationOptions options;
		CoreWindow window;
		bool windowVisible = true;
		bool windowClosed;
		bool appInited;

		public HolographicFrame CurrentFrame { get; private set; }
		public HolographicSpace HolographicSpace { get; private set; }
		public StereoApplication Game { get; private set; }
		public SpatialInteractionManager InteractionManager { get; private set; }
		public SpatialGestureRecognizer SpatialGerstureRecognizer { get; private set; }
		public SpatialStationaryFrameOfReference ReferenceFrame { get; private set; }
		public GesturesManager GesturesManager { get; private set; }
		public SpatialMappingManager SpatialMappingManager { get; private set; }
		public VoiceManager VoiceManager { get; private set; }

		public static UrhoAppView Current { get; private set; }

		UrhoAppView(Type holoAppType, ApplicationOptions opts)
		{
			this.holoAppType = holoAppType;
			this.options = opts;
			this.windowVisible = true;
			Current = this;
		}

		public static UrhoAppView Create<T>(ApplicationOptions opts) where T : StereoApplication
		{
			return new UrhoAppView(typeof(T), opts);
		}

		public void Dispose()
		{
			Disposed?.Invoke();
		}

		public event Action<CoreApplicationView> Initialized;
		public event Action Uninitialized;
		public event Action Disposed;
		public event Action<CoreWindow> WindowIsSet;
		public event Action<string> Loaded;
		public event Action AppStarting;
		public event Action<StereoApplication> AppStarted;

		#region IFrameworkView Members

		/// <summary>
		/// The first method called when the IFrameworkView is being created.
		/// Use this method to subscribe for Windows shell events and to initialize your app.
		/// </summary>
		public void Initialize(CoreApplicationView applicationView)
		{
			Initialized?.Invoke(applicationView);

			applicationView.Activated += this.OnViewActivated;

			CoreApplication.Suspending += this.OnSuspending;
			CoreApplication.Resuming += this.OnResuming;
		}

		/// <summary>
		/// Called when the CoreWindow object is created (or re-created).
		/// </summary>
		public void SetWindow(CoreWindow coreWindow)
		{
			window = coreWindow;
			window.KeyDown += OnKeyDown;
			window.KeyUp += OnKeyUp;
			window.Closed += OnWindowClosed;
			window.VisibilityChanged += OnVisibilityChanged;
			HolographicSpace = HolographicSpace.CreateForCoreWindow(window);
			WindowIsSet?.Invoke(window);
		}

		void OnKeyUp(CoreWindow sender, KeyEventArgs args)
		{
			// Sdl.SendKeyboardEvent(SdlKeyState.SDL_RELEASED, (int)args.KeyStatus.ScanCode);
		}

		void OnKeyDown(CoreWindow sender, KeyEventArgs args)
		{
			// Sdl.SendKeyboardEvent(SdlKeyState.SDL_PRESSED, (int)args.KeyStatus.ScanCode);
		}

		/// <summary>
		/// The Load method can be used to initialize scene resources or to load a
		/// previously saved app state.
		/// </summary>
		public void Load(string entryPoint)
		{
			Loaded?.Invoke(entryPoint);
		}

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern void InitializeSpace();

		public unsafe void Run()
		{
			AppStarting?.Invoke();
			ReferenceFrame = SpatialLocator.GetDefault().CreateStationaryFrameOfReferenceAtCurrentLocation();

			var coreWindow = CoreWindow.GetForCurrentThread();
			coreWindow.CustomProperties.Add(nameof(HolographicSpace), HolographicSpace);

			InitializeSpace();
			HolographicSpace.CameraAdded += HolographicSpace_CameraAdded;
			InteractionManager = SpatialInteractionManager.GetForCurrentView();
			if (InteractionManager != null)
				InteractionManager.InteractionDetected += (s, e) => GesturesManager?.HandleInteraction(e.Interaction);

			while (!windowClosed)
			{
				if (appInited && windowVisible && (null != HolographicSpace))
				{
					if (Game != null)
					{
						CurrentFrame = HolographicSpace.CreateNextFrame();
						CurrentFrame.UpdateCurrentPrediction();
						var prediction = CurrentFrame.CurrentPrediction;
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

							Matrix4 leftViewMatrixUrho =  *(Matrix4*)(void*)&leftViewMatrixDx;
							Matrix4 rightViewMatrixUrho = *(Matrix4*)(void*)&rightViewMatrixDx;
							Matrix4 leftProjMatrixUrho =  *(Matrix4*)(void*)&leftProjMatrixDx;
							Matrix4 rightProjMatrixUrho = *(Matrix4*)(void*)&rightProjMatrixDx;
							Game.UpdateStereoView(leftViewMatrixUrho, rightViewMatrixUrho, leftProjMatrixUrho, rightProjMatrixUrho);
						}

						var parameters = CurrentFrame.GetRenderingParameters(cameraPose);
						if (Game.FocusWorldPoint != Vector3.Zero)
							parameters.SetFocusPoint(ReferenceFrame.CoordinateSystem, 
								new System.Numerics.Vector3(
									 Game.FocusWorldPoint.X, 
									 Game.FocusWorldPoint.Y, 
									-Game.FocusWorldPoint.Z)); //LH->RH

						Game.Engine.RunFrame();
						CurrentFrame.PresentUsingCurrentPrediction(HolographicFramePresentWaitBehavior.WaitForFrameToFinish);
					}
					CoreWindow.GetForCurrentThread().Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessAllIfPresent);
				}
				else
				{
					CoreWindow.GetForCurrentThread().Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessOneAndAllPending);
				}
			}
		}

		async void HolographicSpace_CameraAdded(HolographicSpace sender, HolographicSpaceCameraAddedEventArgs args)
		{
			if (!appInited)
			{
				await window.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
				{
					SpatialMappingManager = new SpatialMappingManager();
					VoiceManager = new VoiceManager();

					if (options == null)
						options = new ApplicationOptions();

					//override some options:
					options.LimitFps = false;
					options.Width = (int)args.Camera.RenderTargetSize.Width;
					options.Height = (int)args.Camera.RenderTargetSize.Height;

					Game = (StereoApplication)Activator.CreateInstance(holoAppType, options);
					Game.Run();
					GesturesManager = new GesturesManager(Game, ReferenceFrame);
					AppStarted?.Invoke(Game);
					appInited = true;
				});
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
			Uninitialized?.Invoke();
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
			// Sdl.SendWindowEvent(SdlWindowEvent.SDL_WINDOWEVENT_HIDDEN);

			// Save app state asynchronously after requesting a deferral. Holding a deferral
			// indicates that the application is busy performing suspending operations. Be
			// aware that a deferral may not be held indefinitely; after about five seconds,
			// the app will be forced to exit.
			var deferral = args.SuspendingOperation.GetDeferral();
			Task.Run(() => deferral.Complete());
		}

		void OnResuming(object sender, object args)
		{
			// Sdl.SendWindowEvent(SdlWindowEvent.SDL_WINDOWEVENT_RESTORED);
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
	}
}