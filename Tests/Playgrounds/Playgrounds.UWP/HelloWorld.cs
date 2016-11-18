using System.Threading.Tasks;
using Urho;

namespace Playgrounds.UWP
{
	public class HelloWorld : Application
	{
		public HelloWorld(ApplicationOptions options = null) : base(options) { }

		protected override async void Start()
		{
			Renderer.SetDefaultRenderPath(CoreAssets.RenderPaths.ForwardHWDepth);
			var scene = new Scene();
			scene.CreateComponent<Octree>();

			// Box
			var boxNode = scene.CreateChild();
			boxNode.Position = new Vector3(0, 0, 5);
			boxNode.Rotation = new Quaternion(60, 0, 30);
			boxNode.SetScale(1f);
			var modelObject = boxNode.CreateComponent<StaticModel>();
			modelObject.Model = ResourceCache.GetModel("Models/Box.mdl");
			modelObject.SetMaterial(ResourceCache.GetMaterial("Materials/StoneSmall.xml"));

			await Task.Delay(1000);
			await ToMainThreadAsync();

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
			var debugHud = new MonoDebugHud(this);
			debugHud.Show();
		}
	}
}
