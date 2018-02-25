using System;
using System.Linq;
using ARKit;
using CoreMedia;
using UIKit;
using Urho;
using Urho.Urho2D;

namespace Urho.iOS
{
	public class ARKitComponent : Component
	{
		Texture2D cameraYtexture;
		Texture2D cameraUVtexture;
		bool yuvTexturesInited;
		ARSessionDelegate arSessionDelegate;

		[Preserve]
		public ARKitComponent() { ReceiveSceneUpdates = true; }

		[Preserve]
		public ARKitComponent(IntPtr handle) : base(handle) { ReceiveSceneUpdates = true; }

		public ARConfiguration ARConfiguration { get; set; }
		public UIInterfaceOrientation? Orientation { get; set; }
		public Camera Camera { get; private set; }
		public ARSession ARSession { get; private set; }
		public string ArkitShader { get; set; } = "ARKit";
		public bool RunEngineFramesInARKitCallbakcs { get; set; }

		public override void OnAttachedToNode(Node node)
		{
			base.OnAttachedToNode(node);
		}

		public void Run(Camera camera = null)
		{
			if (Camera == null)
				Camera = base.Scene.GetComponent<Camera>(true);
			if (Camera == null)
				throw new InvalidOperationException("Camera was not set.");

			if (RunEngineFramesInARKitCallbakcs && !Application.Options.DelayedStart)
				throw new InvalidOperationException("ApplicationOptions.DelayedStart should be true if runEngineFramesInArkitCallbacks flag is set");

			if (Orientation == null)
				Orientation = base.Application.Options.Orientation == ApplicationOptions.OrientationType.Landscape ?
					UIInterfaceOrientation.LandscapeRight :
					UIInterfaceOrientation.Portrait;

			arSessionDelegate = new UrhoARSessionDelegate(this, RunEngineFramesInARKitCallbakcs);
			ARSession = new ARSession { Delegate = arSessionDelegate };
			ARConfiguration = ARConfiguration ?? new ARWorldTrackingConfiguration();
			ARSession.Run(ARConfiguration, ARSessionRunOptions.RemoveExistingAnchors);

			if (base.Application is SimpleApplication simpleApp)
			{
				simpleApp.MoveCamera = false;
			}

			if ((Orientation == UIInterfaceOrientation.LandscapeRight ||
				 Orientation == UIInterfaceOrientation.LandscapeLeft) && ARConfiguration is ARFaceTrackingConfiguration)
			{
				throw new Exception("ARFaceTrackingConfiguration in landscape is not supported");
			}
		}

		protected override void OnDeleted()
		{
			base.OnDeleted();
			ARSession.Pause();
		}

		public event Action<ARFrame> ARFrame;

		public unsafe void ProcessARFrame(ARSession session, ARFrame frame)
		{
			var arcamera = frame?.Camera;
			var transform = arcamera.Transform;

			var viewportSize = new CoreGraphics.CGSize(Application.Graphics.Width, Application.Graphics.Height);
			float near = 0.001f;
			float far = 1000f;
			var prj = arcamera.GetProjectionMatrix(Orientation.Value, viewportSize, near, far);
			var dt = frame.GetDisplayTransform(Orientation.Value, viewportSize);

			var urhoProjection = *(Matrix4*)(void*)&prj;
			urhoProjection.M43 /= 2f;
			urhoProjection.M33 = far / (far - near);
			urhoProjection.M34 *= -1;
			//prj.M13 = 0; //center of projection
			//prj.M23 = 0;
			//urhoProjection.Row2 *= -1;
			urhoProjection.Transpose();

			Camera.SetProjection(urhoProjection);
			ApplyOpenTkTransform(Camera.Node, transform);

			if (!yuvTexturesInited)
			{
				var img = frame.CapturedImage;

				// texture for UV-plane;
				cameraUVtexture = new Texture2D();
				cameraUVtexture.SetNumLevels(1);
				cameraUVtexture.SetSize((int)img.GetWidthOfPlane(1), (int)img.GetHeightOfPlane(1), Graphics.LuminanceAlphaFormat, TextureUsage.Dynamic);
				cameraUVtexture.FilterMode = TextureFilterMode.Bilinear;
				cameraUVtexture.SetAddressMode(TextureCoordinate.U, TextureAddressMode.Clamp);
				cameraUVtexture.SetAddressMode(TextureCoordinate.V, TextureAddressMode.Clamp);
				cameraUVtexture.Name = nameof(cameraUVtexture);
				Application.ResourceCache.AddManualResource(cameraUVtexture);

				// texture for Y-plane;
				cameraYtexture = new Texture2D();
				cameraYtexture.SetNumLevels(1);
				cameraYtexture.FilterMode = TextureFilterMode.Bilinear;
				cameraYtexture.SetAddressMode(TextureCoordinate.U, TextureAddressMode.Clamp);
				cameraYtexture.SetAddressMode(TextureCoordinate.V, TextureAddressMode.Clamp);
				cameraYtexture.SetSize((int)img.Width, (int)img.Height, Graphics.LuminanceFormat, TextureUsage.Dynamic);
				cameraYtexture.Name = nameof(cameraYtexture);
				Application.ResourceCache.AddManualResource(cameraYtexture);

				var viewport = Application.Renderer.GetViewport(0);

				var videoRp = new RenderPathCommand(RenderCommandType.Quad);
				videoRp.PixelShaderName = (UrhoString)ArkitShader;
				videoRp.VertexShaderName = (UrhoString)ArkitShader;
				videoRp.SetOutput(0, "viewport");
				videoRp.SetTextureName(TextureUnit.Diffuse, cameraYtexture.Name); //sDiffMap
				videoRp.SetTextureName(TextureUnit.Normal, cameraUVtexture.Name); //sNormalMap

				if (Orientation != UIInterfaceOrientation.Portrait)
					videoRp.PixelShaderDefines = new UrhoString("ARKIT_LANDSCAPE");

				viewport.RenderPath.InsertCommand(1, videoRp);

				var vrp = viewport.RenderPath.GetCommand(1);
				vrp->SetShaderParameter("Tx", (float)dt.x0);
				vrp->SetShaderParameter("Ty", (float)dt.y0);
				vrp->SetShaderParameter("ScaleX", (float)dt.xx);
				vrp->SetShaderParameter("ScaleY", (float)dt.yy);
				vrp->SetShaderParameter("ScaleYX", (float)dt.yx);
				vrp->SetShaderParameter("ScaleXY", (float)dt.xy);

				float imageAspect = (float)img.Width / img.Height;

				float yoffset;
				if (ARConfiguration is ARFaceTrackingConfiguration)
					yoffset = 0.013f;
				else
					yoffset = 64.0f / Math.Max(img.Width, img.Height);
				vrp->SetShaderParameter("YOffset", yoffset);

				yuvTexturesInited = true;
			}

			if (yuvTexturesInited)
				UpdateBackground(frame);

			ARFrame?.Invoke(frame);

			// required!
			frame.Dispose();
		}

		public Vector3? HitTest(float screenX = 0.5f, float screenY = 0.5f, 
			ARHitTestResultType hitTestType = ARHitTestResultType.ExistingPlaneUsingExtent) 
				=> HitTest(ARSession?.CurrentFrame, screenX, screenY, hitTestType);

		public Vector3? HitTest(ARFrame frame, float screenX = 0.5f, float screenY = 0.5f, 
            ARHitTestResultType hitTestType = ARHitTestResultType.ExistingPlaneUsingExtent)
		{
			var result = frame?.HitTest(new CoreGraphics.CGPoint(screenX, screenY),
				ARHitTestResultType.ExistingPlaneUsingExtent)?.FirstOrDefault();

			if (result != null && result.Distance > 0.2f)
			{
				var row = result.WorldTransform.Column3;
				return new Vector3(row.X, row.Y, -row.Z);
			}
			return null;
		}

		unsafe public void ApplyOpenTkTransform(Node node, OpenTK.NMatrix4 matrix, bool rot = false)
		{
			Matrix4 urhoTransform = *(Matrix4*)(void*)&matrix;
			var rotation = urhoTransform.Rotation;
			rotation.Z *= -1;
			var pos = urhoTransform.Row3;
			node.SetWorldPosition(new Vector3(pos.X, pos.Y, -pos.Z));
			node.Rotation = rotation;
			if (!rot)
				node.Rotate(new Quaternion(0, 0, 90));
		}

		unsafe void UpdateBackground(ARFrame frame)
		{
			using (var img = frame.CapturedImage)
			{
				img.Lock(CoreVideo.CVPixelBufferLock.ReadOnly);
				var yPtr = img.BaseAddress;
				var uvPtr = img.GetBaseAddress(1);

				if (yPtr == IntPtr.Zero || uvPtr == IntPtr.Zero)
					return;

				int wY = (int)img.Width;
				int hY = (int)img.Height;
				int wUv = (int)img.GetWidthOfPlane(1);
				int hUv = (int)img.GetHeightOfPlane(1);

				cameraYtexture.SetData(0, 0, 0, wY, hY, (void*)yPtr);
				cameraUVtexture.SetData(0, 0, 0, wUv, hUv, (void*)uvPtr);

				img.Unlock(CoreVideo.CVPixelBufferLock.ReadOnly);
			}
		}

		public event Action<ARAnchor[]> DidAddAnchors;
		internal void OnDidAddAnchors(ARAnchor[] anchors)
			=> DidAddAnchors?.Invoke(anchors);

		public event Action<ARAnchor[]> DidRemoveAnchors;
		internal void OnDidRemoveAnchors(ARAnchor[] anchors)
			=> DidRemoveAnchors?.Invoke(anchors);

		public event Action<ARAnchor[]> DidUpdateAnchors;
		internal void OnDidUpdateAnchors(ARAnchor[] anchors)
			=> DidUpdateAnchors?.Invoke(anchors);

		public event Action<CMSampleBuffer> DidOutputAudioSampleBuffer;
		internal void OnDidOutputAudioSampleBuffer(CMSampleBuffer audioSampleBuffer)
			=> DidOutputAudioSampleBuffer?.Invoke(audioSampleBuffer);

		public event Action<Foundation.NSError> DidFail;
		internal void OnDidFail(Foundation.NSError error)
			=> DidFail?.Invoke(error);

		public event Action WasInterrupted;
		internal void OnWasInterrupted()
			=> WasInterrupted?.Invoke();

		public event Action InterruptionEnded;
		internal void OnInterruptionEnded()
			=> InterruptionEnded?.Invoke();

		public event Action<ARCamera> CameraDidChangeTrackingState;
		internal void OnCameraDidChangeTrackingState(ARCamera camera)
			=> CameraDidChangeTrackingState?.Invoke(camera);
	}

	class UrhoARSessionDelegate : ARSessionDelegate
	{
		WeakReference<ARKitComponent> arkit;
		bool runFrames;

		public UrhoARSessionDelegate(ARKitComponent arkit, bool runFrames)
		{
			this.runFrames = runFrames;
			this.arkit = new WeakReference<ARKitComponent>(arkit);
		}

		public override void CameraDidChangeTrackingState(ARSession session, ARCamera camera)
		{
			if (arkit.TryGetTarget(out var ap) && ap.Application.IsActive)
				Urho.Application.InvokeOnMain(() => ap.OnCameraDidChangeTrackingState(camera));
		}

		public override void DidUpdateFrame(ARSession session, ARFrame frame)
		{
			if (arkit.TryGetTarget(out var ap))
			{
				var app = ap.Application;
				if (!app.IsActive)
					return;

				Urho.Application.InvokeOnMain(() => ap.ProcessARFrame(session, frame));
				if (runFrames)
				{
					app.Engine.RunFrame();
				}
			}
		}

		public override void DidOutputAudioSampleBuffer(ARSession session, CMSampleBuffer audioSampleBuffer)
		{
			if (arkit.TryGetTarget(out var ap) && ap.Application.IsActive)
				Urho.Application.InvokeOnMain(() => ap.OnDidOutputAudioSampleBuffer(audioSampleBuffer));
		}

		public override void DidFail(ARSession session, Foundation.NSError error)
		{
			if (arkit.TryGetTarget(out var ap) && ap.Application.IsActive)
				Urho.Application.InvokeOnMain(() => ap.OnDidFail(error));
		}

		public override void WasInterrupted(ARSession session)
		{
			if (arkit.TryGetTarget(out var ap) && ap.Application.IsActive)
				Urho.Application.InvokeOnMain(() => ap.OnWasInterrupted());
		}

		public override void InterruptionEnded(ARSession session)
		{
			if (arkit.TryGetTarget(out var ap) && ap.Application.IsActive)
				Urho.Application.InvokeOnMain(() => ap.OnInterruptionEnded());
		}

		public override void DidAddAnchors(ARSession session, ARAnchor[] anchors)
		{
			if (arkit.TryGetTarget(out var ap) && ap.Application.IsActive)
				Urho.Application.InvokeOnMain(() => ap.OnDidAddAnchors(anchors));
		}

		public override void DidRemoveAnchors(ARSession session, ARAnchor[] anchors)
		{
			if (arkit.TryGetTarget(out var ap) && ap.Application.IsActive)
				Urho.Application.InvokeOnMain(() => ap.OnDidRemoveAnchors(anchors));
		}

		public override void DidUpdateAnchors(ARSession session, ARAnchor[] anchors)
		{
			if (arkit.TryGetTarget(out var ap) && ap.Application.IsActive)
				Urho.Application.InvokeOnMain(() => ap.OnDidUpdateAnchors(anchors));
		}
	}
}
