using System;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Urho.Actions;
using Urho.Gui;
using System.IO;
using System.Linq;
using System.Diagnostics;
using Urho.Shapes;

namespace Urho
{
	public class SimpleApplication : Application
	{
		[Preserve]
		public SimpleApplication(ApplicationOptions options) : base(options) {}

		public static Task<SimpleApplication> RunAsync(int width = 600, int height = 500)
		{
#if NET45
			return RunAsync(new ApplicationOptions(assetsFolder: "Data") { Width = width, Height = height, ResizableWindow = true });
#endif
			return RunAsync(new ApplicationOptions(assetsFolder: null));
		}

		[Obsolete("RunAsync is Obsolete. Use Show() instead.")]
		public static Task<SimpleApplication> RunAsync(ApplicationOptions options)
		{
#if NET45
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
#if !__IOS__ && !WINDOWS_UWP
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

#if NET45
		[DllImport("user32")]
		static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwnd2, int x, int y, int cx, int cy, int flags);
#endif

		public static SimpleApplication Show(ApplicationOptions opts = null)
		{
#if !NET45
			throw new NotSupportedException();
#else
			if (SynchronizationContext.Current == null)
				throw new NotSupportedException("SynchronizationContext.Current should not be null.");

			//Close active UrhoSharp instances=windows if any
			StopCurrent().Wait();

			opts = opts ?? new ApplicationOptions();
			opts.ResizableWindow = true;
			opts.DelayedStart = true;

			if (opts.Width < 1)
				opts.Width = 900;
			if (opts.Height < 1)
				opts.Height = 800;

			var app = new SimpleApplication(opts);
			StartGameCycle(app);

			if (Platform == Platforms.Windows)
			{
				var handle = Process.GetCurrentProcess().MainWindowHandle;
				SetWindowPos(handle, new IntPtr(-1), 0, 0, 0, 0, 0x1 | 0x2 /*SWP_NOMOVE | SWP_NOSIZE */);
			}
			//on macOS it does [window setLevel:CGWindowLevelForKey(kCGMaximumWindowLevelKey)];

			return app;
#endif
		}

		static async void StartGameCycle(SimpleApplication app)
		{
			app.Run();
			const int FpsLimit = 60;
			while (app.IsActive)
			{
				var elapsed = app.Engine.RunFrame();
				var targetMax = 1000000L / FpsLimit;
				if (elapsed >= targetMax)
					await Task.Yield();
				else
				{
					var ts = TimeSpan.FromMilliseconds((targetMax - elapsed) / 1000d);
					await Task.Delay(ts);
				}
			}
		}

		public Node CameraNode { get; private set; }

		public Camera Camera { get; private set; }

		public Scene Scene { get; private set; }

		public Octree Octree { get; private set; }

		public Zone Zone { get; private set; }

		public Node RootNode { get; private set; }

		public Node LightNode { get; private set; }

		public Light Light { get; private set; }

		public Viewport Viewport { get; private set; }

		public bool MoveCamera { get; set; } = true;

		public float Yaw { get; set; }

		public float Pitch { get; set; }

		protected override void Start()
		{
			// 3D scene with Octree
			Scene = new Scene(Context);
			Octree = Scene.CreateComponent<Octree>();
			Zone = Scene.CreateComponent<Zone>();
			Zone.AmbientColor = new Color(0.6f, 0.6f, 0.6f);
			RootNode = Scene.CreateChild("RootNode");
			RootNode.Position = new Vector3(x: 0, y: -2, z: 8);

			// Camera
			CameraNode = Scene.CreateChild(name: "Camera");
			CameraNode.Rotation = new Quaternion(Pitch = 0, 0, 0);
			Camera = CameraNode.CreateComponent<Camera>();

			// Light
			LightNode = CameraNode.CreateChild();
			LightNode.Position = new Vector3(-5, 10, 0);
			Light = LightNode.CreateComponent<Light>();
			Light.Range = 100;
			Light.Brightness = 0.5f;
			Light.LightType = LightType.Point;

			// Viewport
			Viewport = new Viewport(Context, Scene, Camera, null);
			Renderer.SetViewport(0, Viewport);
			Viewport.SetClearColor(new Color(0.88f, 0.88f, 0.88f));

			if (Platform == Platforms.Android || Platform == Platforms.iOS)
			{
				Viewport.RenderPath.Append(CoreAssets.PostProcess.FXAA2);
			}
			else if (Platform == Platforms.Windows || Platform == Platforms.MacOSX)
			{
				ResourceCache.AutoReloadResources = true;
				Renderer.HDRRendering = true;
				Viewport.RenderPath.Append(CoreAssets.PostProcess.BloomHDR);
				Viewport.RenderPath.Append(CoreAssets.PostProcess.FXAA3);
			}

#if NET45
			Input.SubscribeToMouseWheel(args => CameraNode.Translate(-Vector3.UnitZ * 1f * args.Wheel * -1));
			Input.SetMouseVisible(true, true);
			Input.SubscribeToKeyDown(args => {
				if (args.Key == Key.Esc)
				{
					Exit();
				}
			});
#endif
		}

		public float MoveSpeed { get; set; } = 10f;

		protected override void OnUpdate(float timeStep)
		{
			if (MoveCamera)
			{
				if (Input.GetMouseButtonDown(MouseButton.Left))
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
