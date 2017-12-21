using System;
using System.Linq;
using ARKit;
using UIKit;
using Urho;
using Urho.Physics;
using Urho.Urho2D;

namespace Playgrounds.iOS
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
		public UIInterfaceOrientation Orientation { get; set; }
		public Camera Camera { get; set; }
		public ARSession ARSession { get; private set; }
		public Node AnchorsNode { get; private set; }
		public bool ContinuesHitTestAtCenter { get; set; }
		public Vector3? LastHitTest { get; private set; }
		public bool PlaneDetectionEnabled { get; set; } = true;

		public override void OnAttachedToNode(Node node)
		{
			base.OnAttachedToNode(node);
			AnchorsNode = Scene.CreateChild();
		}

		public void Run()
		{
			if (Camera == null)
				throw new InvalidOperationException("Camera was not set.");

			arSessionDelegate = new UrhoARSessionDelegate(this);
			ARSession = new ARSession { Delegate = arSessionDelegate };
			ARConfiguration = ARConfiguration ?? new ARWorldTrackingConfiguration();
			ARConfiguration.LightEstimationEnabled = true;
			ARSession.Run(ARConfiguration, ARSessionRunOptions.RemoveExistingAnchors);

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
			var prj = arcamera.GetProjectionMatrix(Orientation, viewportSize, near, far);
			var dt = frame.GetDisplayTransform(Orientation, viewportSize);

			var urhoProjection = *(Matrix4*)(void*)&prj;
			urhoProjection.M43 /= 2f;
			urhoProjection.M33 = far / (far - near);
			urhoProjection.M34 *= -1;
			//prj.M13 = 0; //center of projection
			//prj.M23 = 0;

			//urhoProjection.Row2 *= -1;
			urhoProjection.Transpose();

			Camera.SetProjection(urhoProjection);
			ApplyTransform(Camera.Node, transform);

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
				videoRp.PixelShaderName = (UrhoString)"ARKit";
				videoRp.VertexShaderName = (UrhoString)"ARKit";
				videoRp.SetOutput(0, "viewport");
				videoRp.SetTextureName(TextureUnit.Diffuse, cameraYtexture.Name); //sDiffMap
				videoRp.SetTextureName(TextureUnit.Normal, cameraUVtexture.Name); //sNormalMap

				if (ARConfiguration is ARFaceTrackingConfiguration)
					videoRp.PixelShaderDefines = new UrhoString("ARKIT_FACEX");
				else if (Orientation != UIInterfaceOrientation.Portrait)
					videoRp.PixelShaderDefines = new UrhoString("ARKIT_LANDSCAPE");

				viewport.RenderPath.InsertCommand(1, videoRp);

				var vrp = viewport.RenderPath.GetCommand(1);
				vrp->SetShaderParameter("Tx", (float)dt.x0);
				vrp->SetShaderParameter("Ty", (float)dt.y0);
				vrp->SetShaderParameter("ScaleX", (float)dt.xx);
				vrp->SetShaderParameter("ScaleY", (float)dt.yy);
				vrp->SetShaderParameter("ScaleYX", (float)dt.yx);
				vrp->SetShaderParameter("ScaleXY", (float)dt.xy);


				yuvTexturesInited = true;
			}

			if (ContinuesHitTestAtCenter)
				LastHitTest = HitTest();

			if (yuvTexturesInited)
				UpdateBackground(frame);

			ARFrame?.Invoke(frame);

			// required!
			frame.Dispose();
		}

		public Vector3? HitTest(float screenX = 0.5f, float screenY = 0.5f) =>
			HitTest(ARSession?.CurrentFrame, screenX, screenY);

		Vector3? HitTest(ARFrame frame, float screenX = 0.5f, float screenY = 0.5f)
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

		unsafe public void ApplyTransform(Node node, OpenTK.NMatrix4 matrix, bool rot = false)
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

		internal void DidAddAnchors(ARAnchor[] anchors)
		{
			if (!PlaneDetectionEnabled)
				return;

			foreach (var anchor in anchors)
			{
				UpdateAnchor(null, anchor);
			}
		}

		internal void DidRemoveAnchors(ARAnchor[] anchors)
		{
			if (!PlaneDetectionEnabled)
				return;

			foreach (var anchor in anchors)
			{
				AnchorsNode.GetChild(anchor.Identifier.ToString())?.Remove();
			}
		}

		internal void DidUpdateAnchors(ARAnchor[] anchors)
		{
			if (!PlaneDetectionEnabled)
				return;

			foreach (var anchor in anchors)
			{
				var node = AnchorsNode.GetChild(anchor.Identifier.ToString());
				UpdateAnchor(node, anchor);
			}
		}

		public event Action<Node> PlaneUpdated;

		unsafe void UpdateAnchor(Node node, ARAnchor anchor)
		{
			if (anchor is ARPlaneAnchor planeAnchor)
			{
				Material tileMaterial = null;
				Node planeNode = null;
				if (node == null)
				{
					var id = planeAnchor.Identifier.ToString();
					node = AnchorsNode.CreateChild(id);
					planeNode = node.CreateChild("SubPlane");
					var plane = planeNode.CreateComponent<StaticModel>();
					planeNode.Position = new Vector3();
					plane.Model = CoreAssets.Models.Plane;
					plane.Material = Material.FromColor(Color.Transparent);

					/*tileMaterial = new Material();
					tileMaterial.SetTexture(TextureUnit.Diffuse, Application.ResourceCache.GetTexture2D("Textures/PlaneTile.png"));
					var tech = new Technique();
					var pass = tech.CreatePass("alpha");
					pass.DepthWrite = false;
					pass.BlendMode = BlendMode.Alpha;
					pass.PixelShader = "PlaneTile";
					pass.VertexShader = "PlaneTile";
					tileMaterial.SetTechnique(0, tech);
					tileMaterial.SetShaderParameter("MeshColor", Color.White);
					tileMaterial.SetShaderParameter("MeshAlpha", 0.8f); // set 0.0f if you want to hide them
					tileMaterial.SetShaderParameter("MeshScale", 15.0f);

					var planeRb = planeNode.CreateComponent<RigidBody>();
					planeRb.Friction = 1.5f;
					CollisionShape shape = planeNode.CreateComponent<CollisionShape>();
					shape.SetBox(Vector3.One, Vector3.Zero, Quaternion.Identity);
					plane.Material = tileMaterial;
					*/
				}
				else
				{
					planeNode = node.GetChild("SubPlane");
					//tileMaterial = planeNode.GetComponent<StaticModel>().Material;
				}

				ApplyTransform(node, planeAnchor.Transform, true);
				planeNode.Scale = new Vector3(planeAnchor.Extent.X, 0.1f, planeAnchor.Extent.Z);
				planeNode.Position = new Vector3(planeAnchor.Center.X, planeAnchor.Center.Y, -planeAnchor.Center.Z);

				PlaneUpdated?.Invoke(planeNode);

				/*var animation = new ValueAnimation();
				animation.SetKeyFrame(0.0f, 0.8f);
				animation.SetKeyFrame(0.5f, 0.0f);
				tileMaterial.SetShaderParameterAnimation("MeshAlpha", animation, WrapMode.Once, 1.0f);*/
				//Debug.WriteLine($"ARPlaneAnchor  Extent({planeAnchor.Extent}), Center({planeAnchor.Center}), Position({planeAnchor.Transform.Row3}");
			}
			if (anchor is ARFaceAnchor faceAnchor)
			{
			}
		}
	}

	class UrhoARSessionDelegate : ARSessionDelegate
	{
		WeakReference<ARKitComponent> arkit;

		public UrhoARSessionDelegate(ARKitComponent arkit)
		{
			this.arkit = new WeakReference<ARKitComponent>(arkit);
		}

		public override void CameraDidChangeTrackingState(ARSession session, ARCamera camera)
		{
			Console.WriteLine("CameraDidChangeTrackingState");
		}

		public override void DidUpdateFrame(ARSession session, ARFrame frame)
		{
			if (arkit.TryGetTarget(out var ap))
			{
				Urho.Application.InvokeOnMain(() => ap.ProcessARFrame(session, frame));
			}
		}

		public override void DidFail(ARSession session, Foundation.NSError error)
		{
			Console.WriteLine("DidFail");
		}

		public override void WasInterrupted(ARSession session)
		{
			Console.WriteLine("WasInterrupted");
		}

		public override void InterruptionEnded(ARSession session)
		{
			Console.WriteLine("InterruptionEnded");
		}

		public override void DidAddAnchors(ARSession session, ARAnchor[] anchors)
		{
			if (arkit.TryGetTarget(out var ap))
				Urho.Application.InvokeOnMain(() => ap.DidAddAnchors(anchors));
		}

		public override void DidRemoveAnchors(ARSession session, ARAnchor[] anchors)
		{
			if (arkit.TryGetTarget(out var ap))
				Urho.Application.InvokeOnMain(() => ap.DidRemoveAnchors(anchors));
		}

		public override void DidUpdateAnchors(ARSession session, ARAnchor[] anchors)
		{
			if (arkit.TryGetTarget(out var ap))
				Urho.Application.InvokeOnMain(() => ap.DidUpdateAnchors(anchors));
		}
	}
}
