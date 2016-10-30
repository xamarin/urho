using Urho;
using Urho.Shapes;

namespace Playgrounds.Console
{
	public class StereoModePerformance : Application
	{
		public StereoModePerformance(ApplicationOptions opts) : base(opts)
		{
		}

		public static void RunApp()
		{
			const float scale = 0.65f;
			const float width = 1280f * 2 * scale;
			const float height = 720f * scale;

			var app = new StereoModePerformance(new ApplicationOptions("Data") { Width = (int)width, Height = (int)height });
			app.Run();
		}

		protected override void Start()
		{
			var scene = new Scene();
			scene.CreateComponent<Octree>();
			scene.CreateComponent<Zone>().AmbientColor = new Color(0.3f, 0.3f, 0.3f);

			var lightNode = scene.CreateChild();
			lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f)); 
			var light = lightNode.CreateComponent<Light>();
			light.LightType = LightType.Directional;


			var leftEyeNode = scene.CreateChild();
			var leftEye = leftEyeNode.CreateComponent<Camera>();

			var rightEyeNode = scene.CreateChild();
			var rightEye = rightEyeNode.CreateComponent<Camera>();

			//some fake offset between eyes
			leftEyeNode.Translate(new Vector3(-0.2f, 0, 0));
			rightEyeNode.Translate(new Vector3(0.2f, 0, 0));

			Renderer.NumViewports = 2;

			var leftEyeRect = new IntRect(0, 0, Graphics.Width / 2, Graphics.Height);
			var rightEyeRect = new IntRect(Graphics.Width / 2, 0, Graphics.Width, Graphics.Height);

			var leftVp = new Viewport(Context, scene, leftEye, leftEyeRect, null);
			var rightVp = new Viewport(Context, scene, rightEye, rightEyeRect, null);

			var cullCamera = scene.CreateComponent<Camera>();
			leftVp.CullCamera = cullCamera;
			rightVp.CullCamera = cullCamera;

			var hud = Engine.CreateDebugHud();
			var xml = ResourceCache.GetXmlFile("UI/DefaultStyle.xml");
			hud.DefaultStyle = xml;
			hud.ToggleAll();

			Renderer.SetViewport(0, leftVp);
			Renderer.SetViewport(1, rightVp);

			for (int x = -10; x < 10; x++)
			{
				for (int y = -10; y < 10; y++)
				{
					for (int z = 1; z < 30; z++)
					{
						var child = scene.CreateChild();
						child.Position = new Vector3(x, y, z);
						child.SetScale(0.4f);
						var box = child.CreateComponent<Sphere>();
						box.Color = new Color(-x, -y, -z);
					}
				}
			}
		}
	}
}
