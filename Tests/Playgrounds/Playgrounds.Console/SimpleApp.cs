using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Shapes;

namespace Playgrounds.Console
{
	public class SimpleApp : Application
	{
		public SimpleApp(ApplicationOptions options) : base(options)
		{
		}

		public static void RunApp()
		{
			var app = new SimpleApp(new ApplicationOptions("Data"));
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


			var child = scene.CreateChild();
			var box = child.CreateComponent<Box>();
			child.Position = new Vector3(0, 0, 5);

			var cloned = child.Clone(CreateMode.Replicated);
			cloned.Position = new Vector3(1, 0, 5);
			var bb = cloned.GetComponent<Box>();

			var leftEyeNode = scene.CreateChild();
			var leftEye = leftEyeNode.CreateComponent<Camera>();
			
			var leftVp = new Viewport(Context, scene, leftEye, null);

			Renderer.SetViewport(0, leftVp);
		}
	}
}
