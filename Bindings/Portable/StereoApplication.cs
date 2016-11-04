using System;
using System.Collections.Generic;
using System.Text;

namespace Urho
{
	public class StereoApplication : Application
	{
		public StereoApplication(ApplicationOptions options) : base(options)
		{
		}
		
		public Scene Scene { get; private set; }

		public Octree Octree { get; private set; }

		public Light Light { get; private set; }

		public Camera RightCamera { get; set; }

		public Camera LeftCamera { get; set; }

		protected override void Start()
		{
			// 3D scene with Octree
			Scene = new Scene(Context);
			Octree = Scene.CreateComponent<Octree>();

			var lightNode = Scene.CreateChild("DirectionalLight");
			lightNode.SetDirection(new Vector3(0.5f, 0.0f, 0.8f));
			Light = lightNode.CreateComponent<Light>();
			Light.LightType = LightType.Directional;
			Light.CastShadows = true;
			Light.ShadowBias = new BiasParameters(0.00025f, 0.5f);
			Light.ShadowCascade = new CascadeParameters(10.0f, 50.0f, 200.0f, 0.0f, 0.8f);
			Light.SpecularIntensity = 0.5f;
			Light.Color = new Color(1.2f, 1.2f, 1.2f);

			var leftEyeNode = Scene.CreateChild();
			LeftCamera = leftEyeNode.CreateComponent<Camera>();

			var rightEyeNode = Scene.CreateChild();
			RightCamera = rightEyeNode.CreateComponent<Camera>();
			
			leftEyeNode.Translate(new Vector3(-0.032f, 0, 0));
			rightEyeNode.Translate(new Vector3(0.032f, 0, 0));

			Renderer.NumViewports = 2;

			var leftEyeRect = new IntRect(0, 0, Graphics.Width / 2, Graphics.Height);
			var rightEyeRect = new IntRect(Graphics.Width / 2, 0, Graphics.Width, Graphics.Height);

			var leftVp = new Viewport(Context, Scene, LeftCamera, leftEyeRect, null);
			var rightVp = new Viewport(Context, Scene, RightCamera, rightEyeRect, null);

			leftVp.SetClearColor(Color.Red);
			rightVp.SetClearColor(Color.Blue);

			Renderer.SetViewport(0, leftVp);
			Renderer.SetViewport(1, rightVp);
		}

		public void SetHeadPostion(Quaternion rotation, Vector3 position)
		{
			InvokeOnMain(() =>
			{
				LeftCamera.Node.Rotation = rotation;
				RightCamera.Node.Rotation = rotation;
			});
		}
	}
}
