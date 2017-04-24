using Urho;
using Urho.Actions;
using Urho.Gui;
using Urho.Shapes;

namespace Playgrounds.WinForms
{
	class Game : Application
	{
		public Game(ApplicationOptions opts) : base(opts) { }

		Text helloText;
		static int instanceId = 0;

		public Viewport Viewport { get; private set; }
		public Scene Scene { get; private set; }
		public Node CameraNode { get; private set; }
		public float Yaw { get; private set; }
		public float Pitch { get; private set; }

		protected override void Start()
		{
			instanceId++;
			Log.LogLevel = LogLevel.Debug;
			// UI text 
			helloText = new Text(Context);
			helloText.HorizontalAlignment = HorizontalAlignment.Center;
			helloText.VerticalAlignment = VerticalAlignment.Top;
			helloText.SetColor(Color.Black);
			helloText.SetFont(font: CoreAssets.Fonts.AnonymousPro, size: 20);
			UI.Root.AddChild(helloText);

			// 3D scene with Octree
			Scene = new Scene(Context);
			Scene.CreateComponent<Octree>();
			Scene.CreateComponent<Zone>().AmbientColor = new Color(0.5f, 0.5f, 0.5f);

			for (int i = 0; i < 70; i++)
			{
				SpawnRandomShape();
			}


			// Light
			Node lightNode = Scene.CreateChild(name: "light");
			var light = lightNode.CreateComponent<Light>();
			light.Range = 50;
			light.Brightness = 1f;

			// Camera
			CameraNode = Scene.CreateChild(name: "camera");
			Camera camera = CameraNode.CreateComponent<Camera>();

			// Viewport
			Viewport = new Viewport(Context, Scene, camera, null);
			Viewport.RenderPath.Append(CoreAssets.PostProcess.FXAA3);
			Viewport.SetClearColor(Color.White);
			Renderer.SetViewport(0, Viewport);

			new MonoDebugHud(this).Show(Color.Red);
		}

		async void SpawnBox(Node parent, Vector3 pos)
		{
			// Box
			Node boxNode = parent.CreateChild();
			boxNode.Position = pos;
			boxNode.SetScale(1f);
			boxNode.Rotation = new Quaternion(x: Randoms.Next(-90, 90), y: Randoms.Next(-90, 90), z: Randoms.Next(-90, 90));

			var box = boxNode.CreateComponent<Box>();
			box.Color = Randoms.NextColor();

			await boxNode.RunActionsAsync(new RepeatForever(
				new RotateBy(duration: 1, deltaAngleX: Randoms.Next(-90, 90), deltaAngleY: 0, deltaAngleZ: 0)));
		}

		public void SpawnRandomShape()
		{
			SpawnBox(Scene, new Vector3(Randoms.Next(-20, 20), Randoms.Next(-10, 10), Randoms.Next(10, 60)));
		}

		protected void SimpleMoveCamera3D(float timeStep, float moveSpeed = 10.0f)
		{
			const float mouseSensitivity = .1f;

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

		protected override void OnUpdate(float timeStep)
		{
			if (Input.GetMouseButtonDown(MouseButton.Left))
                SimpleMoveCamera3D(timeStep);
			helloText.Value = $"UrhoSharp Instance: {instanceId}\nResolution: {Graphics.Width}x{Graphics.Height}";
			base.OnUpdate(timeStep);
		}
	}
}
