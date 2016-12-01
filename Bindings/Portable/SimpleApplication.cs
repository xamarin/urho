using System;
using System.Threading;
using System.Threading.Tasks;
using Urho.Actions;
using Urho.Gui;
using System.IO;
using System.Linq;
using Urho.Shapes;

namespace Urho
{
	public class SimpleApplication : Application
	{
		[Preserve]
		public SimpleApplication(ApplicationOptions options) : base(options) {}

		public static Task<SimpleApplication> RunAsync(int width = 600, int height = 500)
		{
#if DESKTOP
			return RunAsync(new ApplicationOptions(assetsFolder: "Data") { Width = width, Height = height, ResizableWindow = true });
#endif
			return RunAsync(new ApplicationOptions(assetsFolder: null));
		}

		public static Task<SimpleApplication> RunAsync(ApplicationOptions options)
		{
#if DESKTOP
			var dataDir = options.ResourcePaths?.FirstOrDefault();
			Environment.CurrentDirectory = Path.GetDirectoryName(typeof(SimpleApplication).Assembly.Location);

			if (!File.Exists("CoreData.pak")) {
				using (Stream input = typeof(SimpleApplication).Assembly.GetManifestResourceStream("Urho.CoreData.pak"))
				using (Stream output = File.Create(Path.Combine("CoreData.pak")))
					input.CopyTo(output);
			}
			if (!string.IsNullOrEmpty(dataDir))
				Directory.CreateDirectory("Data");
#endif
#if !IOS && !UWP

			var taskSource = new TaskCompletionSource<SimpleApplication>();
			Action callback = null;
			callback = () => {
				Started -= callback;
				taskSource.TrySetResult(Current as SimpleApplication);
			};
			Started += callback;
			Task.Factory.StartNew(() => new SimpleApplication(options).Run(), 
					CancellationToken.None, 
					TaskCreationOptions.DenyChildAttach,
					SynchronizationContext.Current == null ? TaskScheduler.Default : TaskScheduler.FromCurrentSynchronizationContext());

			return taskSource.Task;
#else
			var app = new SimpleApplication(options);
			app.Run(); //for iOS and UWP it's not blocking
			return Task.FromResult(app);
#endif
		}

		public Node CameraNode { get; private set; }

		public Camera Camera { get; private set; }

		public Scene Scene { get; private set; }

		public Octree Octree { get; private set; }

		public Node RootNode { get; private set; }

		public Node LightNode { get; private set; }

		public Light Light { get; private set; }

		public Viewport Viewport { get; private set; }

		public bool MoveCamera { get; set; }

		public float Yaw { get; set; }

		public float Pitch { get; set; }

		protected override void Start()
		{
			// 3D scene with Octree
			Scene = new Scene(Context);
			Octree = Scene.CreateComponent<Octree>();
			RootNode = Scene.CreateChild("RootNode");
			RootNode.Position = new Vector3(x: 0, y: 0, z: 8);

			LightNode = Scene.CreateChild("DirectionalLight");
			LightNode.SetDirection(new Vector3(0.5f, 0.0f, 0.8f));
			Light = LightNode.CreateComponent<Light>();
			Light.LightType = LightType.Directional;
			Light.CastShadows = true;
			Light.ShadowBias = new BiasParameters(0.00025f, 0.5f);
			Light.ShadowCascade = new CascadeParameters(10.0f, 50.0f, 200.0f, 0.0f, 0.8f);
			Light.SpecularIntensity = 0.5f;
			Light.Color = new Color(1.2f, 1.2f, 1.2f);

			// Camera
			CameraNode = Scene.CreateChild(name: "Camera");
			Camera = CameraNode.CreateComponent<Camera>();

			// Viewport
			Viewport = new Viewport(Context, Scene, Camera, null);
			Renderer.SetViewport(0, Viewport);
			Viewport.SetClearColor(Color.White);

			// Subscribe to Esc key:
			Input.SubscribeToKeyDown(args => { if (args.Key == Key.Esc) Exit(); });
		}

		public float MoveSpeed { get; set; } = 10f;

		protected override void OnUpdate(float timeStep)
		{
			if (MoveCamera)
			{
				if (!Options.TouchEmulation && Platform != Platforms.Android && Platform != Platforms.iOS)
					MoveCameraMouse(timeStep);
				else
					MoveCameraTouches(timeStep);

				if (Input.GetKeyDown(Key.W)) CameraNode.Translate(Vector3.UnitZ * MoveSpeed * timeStep);
				if (Input.GetKeyDown(Key.S)) CameraNode.Translate(-Vector3.UnitZ * MoveSpeed * timeStep);
				if (Input.GetKeyDown(Key.A)) CameraNode.Translate(-Vector3.UnitX * MoveSpeed * timeStep);
				if (Input.GetKeyDown(Key.D)) CameraNode.Translate(Vector3.UnitX * MoveSpeed * timeStep);
			}
			
			base.OnUpdate(timeStep);
		}

		protected void MoveCameraMouse(float timeStep)
		{
			const float mouseSensitivity = .05f;

			if (UI.FocusElement != null)
				return;

			var mouseMove = Input.MouseMove;
			Yaw += mouseSensitivity * mouseMove.X;
			Pitch += mouseSensitivity * mouseMove.Y;
			Pitch = MathHelper.Clamp(Pitch, -90, 90);

			CameraNode.Rotation = new Quaternion(Pitch, Yaw, 0);
		}

		protected void MoveCameraTouches(float timeStep)
		{
			var input = Input;
			for (uint i = 0, num = input.NumTouches; i < num; ++i)
			{
				TouchState state = input.GetTouch(i);
				if (state.TouchedElement != null)
					continue;

				if (state.Delta.X != 0 || state.Delta.Y != 0)
				{
					var camera = CameraNode.GetComponent<Camera>();
					if (camera == null)
						return;

					var graphics = Graphics;
					Yaw += 2 * camera.Fov / graphics.Height * state.Delta.X;
					Pitch += 2 * camera.Fov / graphics.Height * state.Delta.Y;
					CameraNode.Rotation = new Quaternion(Pitch, Yaw, 0);
				}
				else
				{
					var cursor = UI.Cursor;
					if (cursor != null && cursor.Visible)
						cursor.Position = state.Position;
				}
			}
		}
	}

	public class Bar : Component
	{
		Node barNode;
		Node textNode;
		Color color;
		string name;

		public float Value
		{
			get { return barNode.Scale.Y; }
			set { barNode.RunActionsAsync(new EaseBackOut(new ScaleTo(3f, 1, value, 1))); }
		}

		public Bar(string name, Color color)
		{
			this.name = name;
			this.color = color;
			ReceiveSceneUpdates = true;
		}

		public override void OnAttachedToNode(Node node)
		{
			barNode = node.CreateChild();
			barNode.Scale = new Vector3(1, 0, 1);
			var box = barNode.CreateComponent<Box>();
			box.Color = color;

			textNode = node.CreateChild();
			textNode.Position = new Vector3(0, 3, 0);
			var text3D = textNode.CreateComponent<Text3D>();
			text3D.SetFont(CoreAssets.Fonts.AnonymousPro, 60);
			text3D.TextEffect = TextEffect.Stroke;
			text3D.Text = name;

			base.OnAttachedToNode(node);
		}

		protected override void OnUpdate(float timeStep)
		{
			var pos = barNode.Position;
			var scale = barNode.Scale;
			barNode.Position = new Vector3(pos.X, scale.Y / 2f, pos.Z);
			textNode.Position = new Vector3(-0.5f, scale.Y + 0.5f, 0);
		}
	}
}
