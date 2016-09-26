using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Graphics.DirectX;
using Windows.Graphics.Holographic;
using Windows.Media.SpeechRecognition;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Perception.Spatial;
using Windows.Perception.Spatial.Surfaces;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Input.Spatial;

namespace Urho.HoloLens
{
	public class UrhoAppView : IFrameworkView, IDisposable 
	{
		Type holoAppType;
		string assetsDirectory;
		bool windowVisible = true;
		bool windowClosed;
		bool appInited;

		HolographicFrame currentFrame;
		static Dictionary<string, Action> cortanaCommands = null;
		static SpeechRecognizer speechRecognizer;

		public HolographicSpace HolographicSpace { get; private set; }
		public HoloApplication Game { get; private set; }
		public SpatialInteractionManager InteractionManager { get; private set; }
		public SpatialGestureRecognizer SpatialGerstureRecognizer { get; private set; }
		public SpatialStationaryFrameOfReference ReferenceFrame { get; private set; }
		public GesturesManager GesturesManager { get; private set; }
		public SpatialMappingManager SpatialMappingManager { get; private set; }

		public static UrhoAppView Current { get; private set; }

		UrhoAppView(Type holoAppType, string assetsDirectory)
		{
			this.holoAppType = holoAppType;
			this.assetsDirectory = assetsDirectory;
			windowVisible = true;
			Current = this;
		}

		public static UrhoAppView Create<T>(string assetsPakName) where T : HoloApplication
		{
			return new UrhoAppView(typeof(T), assetsPakName);
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

		internal static async Task<bool> RegisterCortanaCommands(Dictionary<string, Action> commands)
		{
			cortanaCommands = commands;
			speechRecognizer = new SpeechRecognizer();
			var constraint = new SpeechRecognitionListConstraint(cortanaCommands.Keys);
			speechRecognizer.Constraints.Clear();
			speechRecognizer.Constraints.Add(constraint);
			var result = await speechRecognizer.CompileConstraintsAsync();
			if (result.Status == SpeechRecognitionResultStatus.Success)
			{
				speechRecognizer.ContinuousRecognitionSession.StartAsync();
				speechRecognizer.ContinuousRecognitionSession.ResultGenerated += (s, e) =>
				{
					if (e.Result.RawConfidence >= 0.5f)
					{
						Action handler;
						if (cortanaCommands.TryGetValue(e.Result.Text, out handler))
							Application.InvokeOnMain(handler);
					}
				};
				return true;
			}
			return false;
		}

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern void InitializeSpace();

		public unsafe void Run()
		{
			ReferenceFrame = SpatialLocator.GetDefault().CreateStationaryFrameOfReferenceAtCurrentLocation();
			CoreWindow.GetForCurrentThread().CustomProperties.Add("HolographicSpace", HolographicSpace);
			InitializeSpace();
			InteractionManager = SpatialInteractionManager.GetForCurrentView();
			InteractionManager.InteractionDetected += (s, e) => GesturesManager?.HandleInteraction(e.Interaction);

			while (!windowClosed)
			{
				if (!appInited)
				{
					SpatialMappingManager = new SpatialMappingManager();
					appInited = true;
					Game = (HoloApplication) Activator.CreateInstance(holoAppType, assetsDirectory);
					Game.Run();
					Game.Engine.PostUpdate += e => currentFrame?.UpdateCurrentPrediction();
					GesturesManager = new GesturesManager(Game, ReferenceFrame);
				}

				if (windowVisible && (null != HolographicSpace))
				{
					if (Game != null)
					{
						currentFrame = HolographicSpace.CreateNextFrame();

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

							Matrix4 leftViewMatrixUrho =  *(Matrix4*)(void*)&leftViewMatrixDx;
							Matrix4 rightViewMatrixUrho = *(Matrix4*)(void*)&rightViewMatrixDx;
							Matrix4 leftProjMatrixUrho =  *(Matrix4*)(void*)&leftProjMatrixDx;
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
						currentFrame.PresentUsingCurrentPrediction(HolographicFramePresentWaitBehavior.WaitForFrameToFinish);
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
}