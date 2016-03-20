using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Urho.Actions;
using Urho.Shapes;

namespace Urho.Repl
{
	public class Simple3DScene : Application
	{
		public Simple3DScene(ApplicationOptions options) : base(options)
		{
		}

		public static Task<Simple3DScene> RunAsync(int width = 600, int height = 500)
		{
			var taskSource = new TaskCompletionSource<Simple3DScene>();
			Action callback = null;
			callback = () => {
				Started -= callback;
				taskSource.TrySetResult(Current as Simple3DScene);
			};

			Environment.CurrentDirectory = Path.GetDirectoryName(typeof(Simple3DScene).Assembly.Location);

			if (!File.Exists("CoreData.pak")) {
				using (Stream input = typeof(Simple3DScene).Assembly.GetManifestResourceStream("Urho.CoreData.pak"))
				using (Stream output = File.Create(Path.Combine("CoreData.pak")))
					input.CopyTo(output);
			}
			Directory.CreateDirectory("Data");

			Started += callback;

			Task.Delay(1).ContinueWith(r => new Simple3DScene(
				new ApplicationOptions(assetsFolder: "Data") {
					Width = width,
					Height = height
				}).Run(), TaskContinuationOptions.ExecuteSynchronously);

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

			// Light
			LightNode = Scene.CreateChild(name: "light");
			Light = LightNode.CreateComponent<Light>();
			Light.Range = 20;
			Light.Brightness = 1.5f;

			// Camera
			CameraNode = Scene.CreateChild(name: "camera");
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

		/*async void CreateSimpleScene()
		{
			// Box	
			Node boxNode = RootNode.CreateChild(name: "Box node");
			boxNode.SetScale(0f);
			boxNode.Rotation = new Quaternion(x: 60, y: 0, z: 30);


			var box = boxNode.CreateComponent<Box>();
			box.Color = Color.Red;

			//StaticModel boxModel = boxNode.CreateComponent<StaticModel>();
			//boxModel.Model = ResourceCache.GetModel("Models/Box.mdl");
			//boxModel.SetMaterial(ResourceCache.GetMaterial("Materials/BoxMaterial.xml"));

			// Do actions
			await boxNode.RunActionsAsync(new EaseBounceOut(new ScaleTo(duration: 1f, scale: 4)));
			await boxNode.RunActionsAsync(new RepeatForever(
				new RotateBy(duration: 1, deltaAngleX: 90, deltaAngleY: 0, deltaAngleZ: 0)));
		}*/
	}
}
