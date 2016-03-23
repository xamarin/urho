using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Urho.Actions;
using Urho.Gui;
using Urho.Shapes;

namespace Urho.Repl
{
	public class Simple3DScene : Application
	{
		public Simple3DScene(ApplicationOptions options) : base(options) {}

		public static Task<Simple3DScene> RunAsync(int width = 600, int height = 500)
		{
			return RunAsync(new ApplicationOptions("Data") { Width = width, Height = height, ResizableWindow = true });
		}

		public static Task<Simple3DScene> RunAsync(ApplicationOptions options)
		{
			var taskSource = new TaskCompletionSource<Simple3DScene>();
			Action callback = null;
			callback = () => {
				Started -= callback;
				taskSource.TrySetResult(Current as Simple3DScene);
			};
			var dataDir = options.ResourcePaths.FirstOrDefault();
#if DESKTOP
			Environment.CurrentDirectory = Path.GetDirectoryName(typeof(Simple3DScene).Assembly.Location);

			if (!File.Exists("CoreData.pak")) {
				using (Stream input = typeof(Simple3DScene).Assembly.GetManifestResourceStream("Urho.CoreData.pak"))
				using (Stream output = File.Create(Path.Combine("CoreData.pak")))
					input.CopyTo(output);
			}
			if (!string.IsNullOrEmpty(dataDir))
				Directory.CreateDirectory("Data");
#endif
			Started += callback;
			Task.Factory.StartNew(() => new Simple3DScene(options).Run(), 
					CancellationToken.None, 
					TaskCreationOptions.DenyChildAttach,
					SynchronizationContext.Current == null ? TaskScheduler.Default : TaskScheduler.FromCurrentSynchronizationContext());

			return taskSource.Task;
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

		//NOTE: currently working only in 64bit
		public unsafe void SetBackgroundColor(Color color)
		{
			var rp = Viewport.RenderPath;
			for (int i = 0; i < rp.NumCommands; i++)
			{
				var cmd = rp.GetCommand((uint) i);
				if (cmd->Type == RenderCommandType.Clear)
				{
					cmd->UseFogColor = 0;
					cmd->ClearColor = color;
				}
			}
		}

		public Node AddShape<T>(Color color) where T : Shape
		{
			var child = RootNode.CreateChild();
			var shape = child.CreateComponent<T>();
			shape.Color = color;
			return child;
		}

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
			SetBackgroundColor(Color.White);

			// Subscribe to Esc key:
			Input.SubscribeToKeyDown(args => { if (args.Key == Key.Esc) Engine.Exit(); });
		}

		protected override void OnUpdate(float timeStep)
		{
			if (MoveCamera)
				SimpleMoveCamera3D(timeStep);
			base.OnUpdate(timeStep);
		}

		protected void SimpleMoveCamera3D(float timeStep, float moveSpeed = 10.0f)
		{
			const float mouseSensitivity = .05f;

			if (UI.FocusElement != null)
				return;

			var mouseMove = Input.MouseMove;
			Yaw += mouseSensitivity * mouseMove.X;
			Pitch += mouseSensitivity * mouseMove.Y;
			Pitch = MathHelper.Clamp(Pitch, -90, 90);

			CameraNode.Rotation = new Quaternion(Pitch, Yaw, 0);

			if (Input.GetKeyDown(Key.W)) CameraNode.Translate(Vector3.UnitZ * moveSpeed * timeStep);
			if (Input.GetKeyDown(Key.S)) CameraNode.Translate(-Vector3.UnitZ * moveSpeed * timeStep);
			if (Input.GetKeyDown(Key.A)) CameraNode.Translate(-Vector3.UnitX * moveSpeed * timeStep);
			if (Input.GetKeyDown(Key.D)) CameraNode.Translate(Vector3.UnitX * moveSpeed * timeStep);
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
