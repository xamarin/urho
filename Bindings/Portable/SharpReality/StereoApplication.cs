using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Urho.Resources;

namespace Urho.SharpReality
{
	public class StereoApplication : Application
	{
		Matrix4 leftView = Matrix4.Identity;
		Matrix4 leftProj = Matrix4.Identity;
		Matrix4 rightView = Matrix4.Identity;
		Matrix4 rightProj = Matrix4.Identity;
		float yaw;
		float pitch;
		float distanceBetweenEyes;

		public bool Emulator { get; set; }
		public Scene Scene { get; private set; }
		public Octree Octree { get; private set; }
		public Zone Zone { get; private set; }
		public Camera LeftCamera { get; set; }
		public Camera RightCamera { get; set; }
		public Camera CullingCamera { get; set; }
		public Light DirectionalLight { get; set; }
		public virtual Vector3 FocusWorldPoint { get; set; }

		public float DistanceBetweenEyes
		{
			get
			{
				if (distanceBetweenEyes == 0 && !Emulator)
				{
					var l = LeftCamera.Node.WorldPosition;
					var r = RightCamera.Node.WorldPosition;
					distanceBetweenEyes = (float)Math.Sqrt((r.X-l.X)*(r.X-l.X)+(r.Y-l.Y)*(r.Y-l.Y)+(r.Z-l.Z)*(r.Z-l.Z));
				}
				return distanceBetweenEyes;
			}
		}

		public Vector3 HeadPosition
		{
			get
			{
				var leftCameraNode = LeftCamera.Node.WorldPosition;
				if (Emulator) return leftCameraNode;
				leftCameraNode.X += DistanceBetweenEyes / 2;
				return leftCameraNode;
			}
		}

		public bool EnableGestureTapped { get; set; }
		public bool EnableGestureHold { get; set; }
		public bool EnableGestureManipulation { get; set; }

		protected StereoApplication(ApplicationOptions opts) : base(opts) { }
		
		protected override void OnUpdate(float timeStep)
		{
			if (Emulator)
				EmulateCamera(timeStep);
			base.OnUpdate(timeStep);
		}

		void EmulateCamera(float timeStep, float moveSpeed = 2.0f)
		{
			const float mouseSensitivity = .1f;

			if (UI.FocusElement != null)
				return;

			var mouseMove = Input.MouseMove;
			yaw += mouseSensitivity * mouseMove.X;
			pitch += mouseSensitivity * mouseMove.Y;
			pitch = MathHelper.Clamp(pitch, -90, 90);

			var cameraNode = LeftCamera.Node;
			cameraNode.Rotation = new Quaternion(pitch, yaw, 0);

			if (Input.GetKeyDown(Key.W)) cameraNode.Translate( Vector3.UnitZ * moveSpeed * timeStep);
			if (Input.GetKeyDown(Key.S)) cameraNode.Translate(-Vector3.UnitZ * moveSpeed * timeStep);
			if (Input.GetKeyDown(Key.A)) cameraNode.Translate(-Vector3.UnitX * moveSpeed * timeStep);
			if (Input.GetKeyDown(Key.D)) cameraNode.Translate( Vector3.UnitX * moveSpeed * timeStep);
		}

		protected virtual XmlFile DefaultRenderPath => CoreAssets.RenderPaths.Forward;

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern void Camera_SetHoloProjections(
			IntPtr leftEyeCamera, IntPtr rightEyeCamera, IntPtr cullingCamera, 
			ref Matrix4 leftView, ref Matrix4 leftProjection,
			ref Matrix4 rightView, ref Matrix4 rightProjection);

		protected override void Start()
		{
			Renderer.SetDefaultRenderPath(DefaultRenderPath);
			Renderer.DrawShadows = false;
			
			Scene = new Scene();
			Octree = Scene.CreateComponent<Octree>();
			Zone = Scene.CreateComponent<Zone>();
			Zone.AmbientColor = new Color(0.5f, 0.5f, 0.5f);

			Node lightNode = Scene.CreateChild();
			lightNode.SetDirection(new Vector3(0.5f, -1, 1));
			DirectionalLight = lightNode.CreateComponent<Light>();
			DirectionalLight.LightType = LightType.Directional;
			DirectionalLight.CastShadows = false;
			DirectionalLight.Brightness = 0.7f;

			Renderer.TextureAnisotropy = 0;
			Renderer.TextureFilterMode = TextureFilterMode.Bilinear;

			var leftCameraNode = Scene.CreateChild();
			var rightCameraNode = Scene.CreateChild();
			LeftCamera = leftCameraNode.CreateComponent<Camera>();
			RightCamera = rightCameraNode.CreateComponent<Camera>();

			if (Emulator)
			{
				LeftCamera.Fov = 30;
				LeftCamera.NearClip = 0.1f;
				LeftCamera.FarClip = 20f;
				LeftCamera.AspectRatio = 1.76f;
				return;
			}

			Renderer.NumViewports = 2; //two eyes

#if UWP_HOLO
			var leftViewport = new Viewport(Scene, LeftCamera, null);
			var rightVp = new Viewport(Scene, RightCamera, null);
			Renderer.SetViewport(0, leftViewport);
			Renderer.SetViewport(1, rightVp);

			var cullingCameraNode = Scene.CreateChild();
			CullingCamera = cullingCameraNode.CreateComponent<Camera>();
			rightVp.CullCamera = CullingCamera;
			leftViewport.CullCamera = CullingCamera;
			rightVp.SetStereoMode(true);

			Time.SubscribeToFrameStarted(args =>
				Camera_SetHoloProjections(
					LeftCamera.Handle, RightCamera.Handle, CullingCamera.Handle,
					ref leftView, ref leftProj,
					ref rightView, ref rightProj));
#else
			var leftEyeRect = new IntRect(0, 0, Graphics.Width / 2, Graphics.Height);
			var rightEyeRect = new IntRect(Graphics.Width / 2, 0, Graphics.Width, Graphics.Height);
			
			var leftViewport = new Viewport(Scene, LeftCamera, leftEyeRect, null);
			var rightVp = new Viewport(Scene, RightCamera, rightEyeRect, null);
			Renderer.SetViewport(0, leftViewport);
			Renderer.SetViewport(1, rightVp);

			leftCameraNode.Translate(new Vector3(-0.032f, 0, 0));
			rightCameraNode.Translate(new Vector3(0.032f, 0, 0));
#endif
		}

		internal void UpdateStereoView(Matrix4 leftView, Matrix4 rightView, Matrix4 leftProj, Matrix4 rightProj)
		{
			this.leftView = leftView;
			this.rightView = rightView;
			this.leftProj = leftProj;
			this.rightProj = rightProj;
		}

		public Dictionary<string, Action> CortanaCommands { get; private set; }

		/// <summary>
		/// NOTE: Make sure "Microphone" capability is declared.
		/// </summary>
		protected Task<bool> RegisterCortanaCommands(Dictionary<string, Action> commands)
		{
			CortanaCommands = commands;
#if UWP_HOLO
			return Urho.SharpReality.UrhoAppView.Current.VoiceManager.RegisterCortanaCommands(commands);
#else
			return Task.FromResult<bool>(false);
#endif
		}

		public Task TextToSpeech(string text)
		{
#if UWP_HOLO
			return Urho.SharpReality.UrhoAppView.Current.VoiceManager.TextToSpeech(text);
#else
			return Task.FromResult<bool>(false);
#endif
		}

		/// <summary>
		/// NOTE: Make sure "spatialMapping" capability is declared.
		/// </summary>
		protected Task<bool> StartSpatialMapping(Vector3 extents, int trianglesPerCubicMeter = 1000, Color color = default(Color), bool onlyAdd = false, bool convertToLeftHanded = true)
		{
#if UWP_HOLO
			var appView = Urho.SharpReality.UrhoAppView.Current;
			appView.SpatialMappingManager.DefaultColor = color;
			return appView.SpatialMappingManager.Register(this, 
				appView.ReferenceFrame.CoordinateSystem, 
				new System.Numerics.Vector3(extents.X, extents.Y, extents.Z), 
				trianglesPerCubicMeter, onlyAdd, convertToLeftHanded);
#else
			return Task.FromResult<bool>(false);
#endif
		}

		protected void StopSpatialMapping()
		{
#if UWP_HOLO
			Urho.SharpReality.UrhoAppView.Current.SpatialMappingManager.Stop();
#endif
		}

		public virtual void OnGestureTapped() { }
		public virtual void OnGestureDoubleTapped() { }
		public virtual void OnGestureHoldStarted() { }
		public virtual void OnGestureHoldCompleted() { }
		public virtual void OnGestureHoldCanceled() { }
		public virtual void OnGestureManipulationStarted() { }
		public virtual void OnGestureManipulationUpdated(Vector3 relativeHandPosition) { }
		public virtual void OnGestureManipulationCompleted(Vector3 relativeHandPosition) { }
		public virtual void OnGestureManipulationCanceled() { }
		public virtual void OnSurfaceAddedOrUpdated(SpatialMeshInfo surface, Model generatedModel) { }

		/// <summary>
		/// NOTE: called from a background thread
		/// </summary>
		public virtual Model GenerateModelFromSpatialSurface(SpatialMeshInfo surface)
		{
			return CreateModelFromVertexData(surface.VertexData, surface.IndexData, surface.BoundsRotation);
		}

		internal void HandleSurfaceUpdated(SpatialMeshInfo surface)
		{
			var model = GenerateModelFromSpatialSurface(surface);
			InvokeOnMain(() => OnSurfaceAddedOrUpdated(surface, model));
		}

		protected unsafe Model CreateModelFromVertexData(SpatialVertex[] vertexData, short[] indexData, Quaternion rotation = default(Quaternion))
		{
			var model = new Model();
			var vertexBuffer = new VertexBuffer(Context, false);
			var indexBuffer = new IndexBuffer(Context, false);
			var geometry = new Geometry();

			vertexBuffer.Shadowed = true;
			vertexBuffer.SetSize((uint) vertexData.Length, ElementMask.Position | ElementMask.Normal | ElementMask.Color , false);

			BoundingBox boundingBox;
			if (rotation != default(Quaternion))
			{
				boundingBox  = new BoundingBox();
				for (int i = 0; i < vertexData.Length; i++)
				{
					var position = rotation * vertexData[i].Position;
					vertexData[i].Position = position;
					boundingBox.Merge(position);

					var normal = rotation * vertexData[i].Normal;
					vertexData[i].Normal = normal;
				}
			}
			else
			{
				boundingBox = new BoundingBox(-Vector3.One * 1.26f, Vector3.One * 1.26f);
			}

			fixed (SpatialVertex* p = &vertexData[0])
				vertexBuffer.SetData((void*) p);

			indexBuffer.Shadowed = true;
			indexBuffer.SetSize((uint)indexData.Length, false, false);
			indexBuffer.SetData(indexData);

			geometry.SetVertexBuffer(0, vertexBuffer);
			geometry.IndexBuffer = indexBuffer;
			geometry.SetDrawRange(PrimitiveType.TriangleList, 0, (uint)indexData.Length, 0,(uint) vertexData.Length, true);

			model.NumGeometries = 1;
			model.SetGeometry(0, 0, geometry);
			model.BoundingBox = boundingBox;

			return model;
		}
	}
}
