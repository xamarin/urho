using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Urho.Actions;
using Urho.Physics;

namespace Urho.Holographics
{
	public class HoloApplication : Application
	{
		Matrix4 leftView = Matrix4.Identity;
		Matrix4 leftProj = Matrix4.Identity;
		Matrix4 rightView = Matrix4.Identity;
		Matrix4 rightProj = Matrix4.Identity;
		Material transparentMaterial;
		float yaw;
		float pitch;
		float distanceBetweenEyes;

		public bool Emulator { get; set; }
		public Scene Scene { get; private set; }
		public Camera LeftCamera { get; set; }
		public Camera RightCamera { get; set; }
		public Color HoloTransparentColor { get; } = Color.Transparent;
		public Material HoloTransparentMaterial => transparentMaterial ?? (transparentMaterial = FromColor(HoloTransparentColor));
		public Light CameraLight { get; set; }
		public Light DirectionalLight { get; set; }
		public Vector3 FocusWorldPoint { get; set; }

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

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern void Viewport_SetHolo(IntPtr handle);//TODO: will be removed 

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern void Camera_SetProjection(IntPtr handle, ref Matrix4 view, ref Matrix4 projection);

		public HoloApplication(string pak, bool emulation = false) : base(Configure(pak, emulation)) { Emulator = emulation; }

		static ApplicationOptions Configure(string pak, bool emulation)
		{
			return new ApplicationOptions(pak) {Width = 1286, Height = 720, LimitFps = emulation };
		}

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

		protected override async void Start()
		{
			if (!Emulator)
				Renderer.SetDefaultRenderPath(CoreAssets.RenderPaths.PrepassHWDepth);

			Scene = new Scene();
			Scene.CreateComponent<Octree>();

			Node lightNode = Scene.CreateChild();
			lightNode.SetDirection(new Vector3(1, -1, 1));
			Light light = lightNode.CreateComponent<Light>();
			light.LightType = LightType.Directional;
			light.CastShadows = true;
			light.Brightness = 0.7f;
			light.ShadowBias = new BiasParameters(0.00025f, 0.5f);
			light.ShadowCascade = new CascadeParameters(10.0f, 50.0f, 200.0f, 0.0f, 0.8f);
			light.ShadowIntensity = 0.5f;

			var leftCameraNode = Scene.CreateChild();
			var rightCameraNode = Scene.CreateChild();
			LeftCamera = leftCameraNode.CreateComponent<Camera>();
			RightCamera = rightCameraNode.CreateComponent<Camera>();

			var cameraLight = leftCameraNode.CreateComponent<Light>();
			cameraLight.LightType = LightType.Point;
			cameraLight.Range = 10.0f;
			cameraLight.Brightness = 0.7f;

			CameraLight = cameraLight;
			DirectionalLight = light;

			var leftViewport = new Viewport(Scene, LeftCamera, null);
			Renderer.SetViewport(0, leftViewport);

			Engine.PostRenderUpdate += s =>
				{
					if (Emulator)
					{
						//Scene.GetComponent<PhysicsWorld>()?.DrawDebugGeometry(true);
						//Renderer.DrawDebugGeometry(true);
					}
				};

			if (Emulator)
			{
				//Scene.CreateComponent<RoomSimulation>();
				//pitch = 10;
				var defaultLrp = leftViewport.RenderPath.Clone();
				defaultLrp.Append(CoreAssets.PostProcess.FXAA3);
				leftViewport.RenderPath = defaultLrp;

				LeftCamera.Fov = 30;
				LeftCamera.NearClip = 0.1f;
				LeftCamera.FarClip = 20f;
				LeftCamera.AspectRatio = 1.76f;
				return;
			}

			Renderer.NumViewports = 2; //two eyes
			var rightVp = new Viewport(Scene, RightCamera, null);

			Viewport_SetHolo(rightVp.Handle);
			Renderer.SetViewport(1, rightVp);

			Time.SubscribeToFrameStarted(args =>
				{
					Camera_SetProjection(LeftCamera.Handle, ref leftView, ref leftProj);
					Camera_SetProjection(RightCamera.Handle, ref rightView, ref rightProj);
				});
		}

		public void UpdateStereoView(Matrix4 leftView, Matrix4 rightView, Matrix4 leftProj, Matrix4 rightProj)
		{
			this.leftView = leftView;
			this.rightView = rightView;
			this.leftProj = leftProj;
			this.rightProj = rightProj;
		}

		static Material FromColor(Color color)
		{
			var material = new Material();
			material.SetTechnique(0, CoreAssets.Techniques.NoTextureUnlit, 1, 1);
			material.SetShaderParameter("MatDiffColor", color);
			return material;
		}

		/// <summary>
		/// NOTE: Make sure "Microphone" capability is declared.
		/// </summary>
		protected Task<bool> RegisterCortanaCommands(Dictionary<string, Action> commands)
		{
#if UWP_HOLO
			return Urho.HoloLens.UrhoAppView.RegisterCortanaCommands(commands);
#else
			return Task.FromResult<bool>(false);
#endif
		}

		/// <summary>
		/// NOTE: Make sure "spatialMapping" capability is declared.
		/// </summary>
		protected Task<bool> StartSpatialMapping(Vector3 extents, int trianglesPerCubicMeter = 1000)
		{
#if UWP_HOLO
			var appView = Urho.HoloLens.UrhoAppView.Current;
			return appView.SpatialMappingManager.Register(this, 
				appView.ReferenceFrame.CoordinateSystem, 
				new System.Numerics.Vector3(extents.X, extents.Y, extents.Z), 
				trianglesPerCubicMeter);
#else
			return Task.FromResult<bool>(false);
#endif
		}

		protected void StopSpatialMapping()
		{
#if UWP_HOLO
			Urho.HoloLens.UrhoAppView.Current.SpatialMappingManager.Stop();
#endif
		}

		public virtual void OnGestureTapped(GazeInfo gaze) { }
		public virtual void OnGestureDoubleTapped(GazeInfo gaze) { }
		public virtual void OnGestureHoldStarted(GazeInfo gaze) { }
		public virtual void OnGestureHoldCompleted() { }
		public virtual void OnGestureHoldCanceled() { }
		public virtual void OnGestureManipulationStarted() { }
		public virtual void OnGestureManipulationUpdated(Vector3 relativeHandPosition) { }
		public virtual void OnGestureManipulationCompleted(Vector3 relativeHandPosition) { }
		public virtual void OnGestureManipulationCanceled() { }
		public virtual void OnSurfaceAddedOrUpdated(string surfaceId, DateTimeOffset lastUpdateTimeUtc, float[] vertexData, short[] indexData, Vector3 boundsCenter, Quaternion boundsRotation) {}
		public virtual void OnActiveSurfaceListChanged(HashSet<string> activeSurfaces) { }

		public HashSet<string> ActiveSurfaces { get; private set; }

		internal void HandleSurfaceUpdated(string surfaceId,
			DateTimeOffset lastUpdateTimeUtc,
			float[] vertexData,
			short[] indexData,
			Vector3 boundsCenter,
			Quaternion boundsRotation)
		{
			OnSurfaceAddedOrUpdated(surfaceId, lastUpdateTimeUtc, vertexData, indexData, boundsCenter, boundsRotation);
		}

		internal void HandleActiveSurfacesChanged(HashSet<string> activeSurfaces)
		{
			ActiveSurfaces = activeSurfaces;
			OnActiveSurfaceListChanged(ActiveSurfaces);
		}

		protected Model CreateModelFromVertexData(float[] vertexData, short[] indexData)
		{
			var model = new Model();
			var vertexBuffer = new VertexBuffer(Context, false);
			var indexBuffer = new IndexBuffer(Context, false);
			var geometry = new Geometry();

			vertexBuffer.Shadowed = true;
			vertexBuffer.SetSize((uint) vertexData.Length / 6, ElementMask.Position | ElementMask.Normal, false);
			vertexBuffer.SetData(vertexData);

			indexBuffer.Shadowed = true;
			indexBuffer.SetSize((uint)indexData.Length, false, false);
			indexBuffer.SetData(indexData);

			geometry.SetVertexBuffer(0, vertexBuffer);
			geometry.IndexBuffer = indexBuffer;
			geometry.SetDrawRange(PrimitiveType.TriangleList, 0, (uint)indexData.Length, true);

			model.NumGeometries = 1;
			model.SetGeometry(0, 0, geometry);
			model.BoundingBox = new BoundingBox(new Vector3(-1.0f, -1.0f, -1.0f), new Vector3(1.0f, 1.0f, 1.0f));
			
			return model;
		}
	}
}