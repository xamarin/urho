using System;
using System.Threading.Tasks;
using Urho;
using Urho.Actions;

namespace Playgrounds.UWP
{
	public class HelloWorld : Application
	{
		MonoDebugHud debugHud;

		public HelloWorld(ApplicationOptions options = null) : base(options) { }

		protected override async void Start()
		{
			//Renderer.SetDefaultRenderPath(CoreAssets.RenderPaths.ForwardHWDepth);
			var scene = new Scene();
			scene.CreateComponent<Octree>();

			// Box
			var boxNode = scene.CreateChild();
			boxNode.Position = new Vector3(0, 0, 5);
			boxNode.Rotation = new Quaternion(60, 0, 30);
			boxNode.SetScale(1f);
			var modelObject = boxNode.CreateComponent<StaticModel>();
			modelObject.Model = ResourceCache.GetModel("Models/Box.mdl");

			boxNode.RunActions(new RepeatForever(new RotateBy(1,0, 90, 0)));
			//await Task.Delay(1000);
			//await ToMainThreadAsync();

			// Light
			var lightNode = scene.CreateChild(name: "light");
			lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f));
			lightNode.CreateComponent<Light>();

			// Camera
			var cameraNode = scene.CreateChild(name: "camera");
			var camera = cameraNode.CreateComponent<Camera>();

			// Viewport
			Renderer.SetViewport(0, new Viewport(scene, camera, null));

			// DebugHud
			debugHud = new MonoDebugHud(this);
			debugHud.Show();

			Input.TouchMove += Input_TouchMove;
			Input.TouchBegin += InputOnTouchBegin;
			Input.MouseMoved += Input_MouseMove;
			Input.MouseButtonDown += InputOnMouseButtonDown;
		}

		void InputOnTouchBegin(TouchBeginEventArgs touchBeginEventArgs)
		{
		}

		void InputOnMouseButtonDown(MouseButtonDownEventArgs mouseButtonDownEventArgs)
		{
		}

		void Input_MouseMove(MouseMovedEventArgs e)
		{
			debugHud.AdditionalText = $"Mouse: {e.X};{e.Y}";
		}

		void Input_TouchMove(TouchMoveEventArgs e)
		{
			debugHud.AdditionalText = $"Touch: {e.X};{e.Y}";
		}
	}
}
