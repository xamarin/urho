using System;
using Urho;
using Urho.Actions;
using Urho.Gui;
using Urho.Shapes;

namespace Playgrounds.WinForms
{
	class Game : Application
	{
		public Game(ApplicationOptions opts) : base(opts)
		{
		}

		private static int num = 0;

		protected override void Start()
		{
			CreateScene();
		}

		async void CreateScene()
		{
			// UI text 
			var helloText = new Text(Context);
			helloText.Value = "UrhoSharp Instance: " + num++;
			helloText.HorizontalAlignment = HorizontalAlignment.Center;
			helloText.VerticalAlignment = VerticalAlignment.Top;
			helloText.SetColor(Color.Black);
			helloText.SetFont(font: CoreAssets.Fonts.AnonymousPro, size: 20);
			UI.Root.AddChild(helloText);

			// 3D scene with Octree
			var scene = new Scene(Context);
			scene.CreateComponent<Octree>();
			scene.CreateComponent<Zone>().AmbientColor = new Color(0.3f, 0.3f, 0.3f);

			for (int i = 0; i < 10; i++)
			{
				for (int j = -2; j <= 2; j++)
				{
					for (int k = -2; k <= 1; k++)
					{
						SpawnBox(scene, new Vector3(j, k, 7 + i));
					}
				}
			}


			// Light
			Node lightNode = scene.CreateChild(name: "light");
			var light = lightNode.CreateComponent<Light>();
			light.Range = 10;
			light.Brightness = 1.5f;

			// Camera
			Node cameraNode = scene.CreateChild(name: "camera");
			Camera camera = cameraNode.CreateComponent<Camera>();

			// Viewport
			var viewport = new Viewport(Context, scene, camera, null);
			viewport.RenderPath.Append(CoreAssets.PostProcess.FXAA3);
			viewport.SetClearColor(Urho.WinForms.UrhoSurface.ConvertColor(System.Drawing.SystemColors.Control));
			Renderer.SetViewport(0, viewport);

			new MonoDebugHud(this).Show(Color.Red);
		}

		async void SpawnBox(Node parent, Vector3 pos)
		{
			// Box
			Node boxNode = parent.CreateChild();
			boxNode.Position = pos;//new Vector3(x: 0, y: 0, z: 5);
			boxNode.SetScale(1f);
			boxNode.Rotation = new Quaternion(x: 60, y: 0, z: 30);

			var box = boxNode.CreateComponent<Box>();
			box.Color = Randoms.NextColor();

			await boxNode.RunActionsAsync(new RepeatForever(
				new RotateBy(duration: 1, deltaAngleX: Randoms.Next(-90, 90), deltaAngleY: 0, deltaAngleZ: 0)));
		}
	}
}
