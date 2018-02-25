using System;
using System.Threading.Tasks;
using Android;
using Urho;
using Urho.Urho2D;
using Urho.IO;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Com.Google.AR.Core;

namespace Urho.Droid
{
	public class ARCoreComponent : Component
	{
		const uint GL_TEXTURE_EXTERNAL_OES = 36197;
		bool paused;

		public Texture2D CameraTexture { get; private set; }
		public Viewport Viewport { get; private set; }
		public Session Session { get; private set; }
		public Config Config { get; private set; }
		public Camera Camera { get; private set; }
		public string ARCoreShader { get; set; } = "ARCore";

		public event Action<Frame> ARFrameUpdated;
		public event Action<Config> ConfigRequested;

		[Preserve]
		public ARCoreComponent() { ReceiveSceneUpdates = true; }

		[Preserve]
		public ARCoreComponent(IntPtr handle) : base(handle) { ReceiveSceneUpdates = true; }

		public override unsafe void OnAttachedToNode(Node node)
		{
			Application.Paused += OnPause;
			Application.Resumed += OnResume;
		}

		public Task Run(Camera camera = null)
		{
			if (CameraTexture != null)
				throw new InvalidOperationException("ARCore component is already initialized, if you want to re-configure ARCore session - use Session property.");

			Camera = camera;
			if (Camera == null)
				Camera = base.Scene.GetComponent<Camera>(true);

			if (Camera == null)
				throw new InvalidOperationException("Camera component was not found.");

			CameraTexture = new Texture2D();
			CameraTexture.SetCustomTarget(GL_TEXTURE_EXTERNAL_OES);
			CameraTexture.SetNumLevels(1);
			CameraTexture.FilterMode = TextureFilterMode.Bilinear;
			CameraTexture.SetAddressMode(TextureCoordinate.U, TextureAddressMode.Clamp);
			CameraTexture.SetAddressMode(TextureCoordinate.V, TextureAddressMode.Clamp);
			CameraTexture.SetSize(Application.Graphics.Width, Application.Graphics.Height, Graphics.Float32Format, TextureUsage.Dynamic);
			CameraTexture.Name = nameof(CameraTexture);
			Application.ResourceCache.AddManualResource(CameraTexture);

			Viewport = Application.Renderer.GetViewport(0);

			if (base.Application is SimpleApplication simpleApp)
			{
				simpleApp.MoveCamera = false;
			}

			var videoRp = new RenderPathCommand(RenderCommandType.Quad);
			videoRp.PixelShaderName = (UrhoString)ARCoreShader; //see CoreData/Shaders/GLSL/ARCore.glsl
			videoRp.VertexShaderName = (UrhoString)ARCoreShader;
			videoRp.SetOutput(0, "viewport");
			videoRp.SetTextureName(TextureUnit.Diffuse, CameraTexture.Name);
			Viewport.RenderPath.InsertCommand(1, videoRp);

			var tcs = new TaskCompletionSource<bool>();
			var activity = (Activity)Urho.Application.CurrentWindow.Target;
			activity.RunOnUiThread(() =>
			{
				var cameraAllowed = activity.CheckSelfPermission(Manifest.Permission.Camera);
				if (cameraAllowed != Permission.Granted)
					throw new InvalidOperationException("Camera permission: Denied");

				var session = new Session(activity);
				session.SetCameraTextureName((int)CameraTexture.AsGPUObject().GPUObjectName);
				session.SetDisplayGeometry((int)SurfaceOrientation.Rotation0 /*windowManager.DefaultDisplay.Rotation*/,
					Application.Graphics.Width, Application.Graphics.Height);

				Config = new Config(session);
				Config.SetLightEstimationMode(Config.LightEstimationMode.AmbientIntensity);
				Config.SetUpdateMode(Config.UpdateMode.LatestCameraImage);
				Config.SetPlaneFindingMode(Config.PlaneFindingMode.Horizontal);
				ConfigRequested?.Invoke(Config);

				if (!session.IsSupported(Config))
				{
					throw new Exception("AR is not supported on this device with given config");
				}
				session.Configure(Config);

				paused = false;
				session.Resume();
				Session = session;
				Urho.Application.InvokeOnMain(() => tcs.TrySetResult(true));
			});
			return tcs.Task;
		}

		void OnPause()
		{
			paused = true;
			Session?.Pause();
		}

		void OnResume()
		{
			paused = false;
			Session?.Resume();
		}

		protected override void OnDeleted()
		{
			Application.Paused -= OnPause;
			Application.Resumed -= OnResume;

			base.OnDeleted();
			try
			{
				Session?.Pause();
			}
			catch (Exception exc)
			{
				Log.Write(LogLevel.Warning, "ARCore pause error: " + exc);
			}
		}

		protected override void OnUpdate(float timeStep)
		{
			if (paused)
				return;

			if (Camera == null)
				throw new Exception("ARCore.Camera property was not set");

			try
			{
				if (Session == null)
					return;

				var frame = Session.Update();
				if (paused) //in case if Config.UpdateMode.LatestCameraImage is not used
					return;

				var camera = frame.Camera;
				if (camera.TrackingState != TrackingState.Tracking)
					return;

				var far = 100f;
				var near = 0.01f;

				float[] projmx = new float[16];
				camera.GetProjectionMatrix(projmx, 0, near, far);

				var prj = new Urho.Matrix4(
					projmx[0], projmx[4], projmx[8], projmx[12],
					projmx[1], projmx[5], projmx[9], projmx[13],
					projmx[2], projmx[6], projmx[10], projmx[14],
					projmx[3], projmx[7], projmx[11], projmx[15]
				);

				//some OGL -> DX conversion (Urho accepts DX format on all platforms)
				prj.M34 /= 2f;
				prj.M33 = far / (far - near);
				prj.M43 *= -1;
				//prj.M13 = 0; //center of projection
				//prj.M23 = 0;

				Camera.SetProjection(prj);

				TransformByPose(Camera.Node, camera.DisplayOrientedPose);
				ARFrameUpdated?.Invoke(frame);
			}
			catch (Exception exc)
			{
				Log.Write(LogLevel.Warning, "ARCore error: " + exc);
			}
		}

		public static void TransformByPose(Node node, Pose pose)
		{
			// Right-Handed coordinate system to Left-Handed
			node.Rotation = new Quaternion(pose.Qx(), pose.Qy(), -pose.Qz(), -pose.Qw());
			node.Position = new Vector3(pose.Tx(), pose.Ty(), -pose.Tz());
		}
	}
}
