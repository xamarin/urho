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
			helloText.SetFont(font: CoreAssets.Fonts.AnonymousPro, size: 30);
			UI.Root.AddChild(helloText);

			// 3D scene with Octree
			var scene = new Scene(Context);
			scene.CreateComponent<Octree>();

			// Box
			Node boxNode = scene.CreateChild();
			boxNode.Position = new Vector3(x: 0, y: 0, z: 5);
			boxNode.SetScale(1f);
			boxNode.Rotation = new Quaternion(x: 60, y: 0, z: 30);

			var box = boxNode.CreateComponent<Box>();
			box.Color = Color.Magenta;

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

			try
			{
				// Do actions
				await boxNode.RunActionsAsync(new RepeatForever(
					new RotateBy(duration: 1, deltaAngleX: 90, deltaAngleY: 0, deltaAngleZ: 0)));
			}
			catch (OperationCanceledException) { }
		}
	}
}
