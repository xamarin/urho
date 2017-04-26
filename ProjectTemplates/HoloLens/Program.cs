using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Core;
using Urho;
using Urho.Actions;
using Urho.HoloLens;
using Urho.Shapes;
using Urho.Resources;

namespace $safeprojectname$
{
	internal class Program
	{
		[MTAThread]
		static void Main()
		{
			var appViewSource = new UrhoAppViewSource<HelloWorldApplication>(new ApplicationOptions("Data"));
			appViewSource.UrhoAppViewCreated += OnViewCreated;
			CoreApplication.Run(appViewSource);
		}

		static void OnViewCreated(UrhoAppView view) { }
	}

	public class HelloWorldApplication : HoloApplication
	{
		Node earthNode;
		Material earthMaterial;
		float cloudsOffset;

		public HelloWorldApplication(ApplicationOptions opts) : base(opts) { }

		protected override async void Start()
		{
			// Create a basic scene, see HoloApplication
			base.Start();

			// Enable input
			EnableGestureManipulation = true;
			EnableGestureTapped = true;

			// Create a node for the Earth
			earthNode = Scene.CreateChild();
			earthNode.Position = new Vector3(0, 0, 1.5f);
			earthNode.SetScale(0.3f);

			// Scene has a lot of pre-configured components, such as Cameras (eyes), Lights, etc.
			DirectionalLight.Brightness = 1f;
			DirectionalLight.Node.SetDirection(new Vector3(-1, 0, 0.5f));

			//Sphere is just a StaticModel component with Sphere.mdl as a Model.
			var earth = earthNode.CreateComponent<Sphere>();
			earthMaterial = ResourceCache.GetMaterial("Materials/Earth.xml");
			earth.SetMaterial(earthMaterial);

			var moonNode = earthNode.CreateChild();
			moonNode.SetScale(0.27f);
			moonNode.Position = new Vector3(1.2f, 0, 0);
			var moon = moonNode.CreateComponent<Sphere>();
			moon.SetMaterial(ResourceCache.GetMaterial("Materials/Moon.xml"));

			var marsNode = earthNode.CreateChild();
			marsNode.Scale = new Vector3(0.7f, 0.7f, 0.7f);
			marsNode.Rotation = new Quaternion(x: 0, y: 0, z: 45);
			marsNode.Position = new Vector3(3f, 0.5f, 1);
			var mars = marsNode.CreateComponent<StaticModel>();
			mars.Model = CoreAssets.Models.Sphere; //Same as Sphere, here is just as an example.
			//A simple material created from an image: 
			mars.SetMaterial(Material.FromImage("Textures/Mars.jpg"));

			//requires >=15063 build
			//var display = Windows.Graphics.Holographic.HolographicDisplay.GetDefault();
			if (true)// display != null && !display.IsOpaque)
			{
				//HoloLens - do nothing
			}
			else
			{
				//Since the display is opaque - we can display a custom skybox
				var skyboxNode = Scene.CreateChild();
				skyboxNode.SetScale(100);
				var skybox = skyboxNode.CreateComponent<Skybox>();
				skybox.Model = CoreAssets.Models.Box; //see CoreAssets
				//Skybox is usally a set of six textures joined together, see FeatureSamples/Core/23_Water sample 
				skybox.SetMaterial(Material.SkyboxFromImage("Textures/Space.png"));
			}

			// Run a few actions to spin the Earth, the Moon and the clouds.
			earthNode.RunActions(new RepeatForever(new RotateBy(duration: 1f, deltaAngleX: 0, deltaAngleY: -4, deltaAngleZ: 0)));
			await TextToSpeech("Hello world from UrhoSharp!");
		}

		protected override void OnUpdate(float timeStep)
		{
			// Move clouds via CloudsOffset (see CustomLitSolid.hlsl)
			cloudsOffset += 0.00005f;
			earthMaterial.SetShaderParameter("CloudsOffset", new Vector2(cloudsOffset, 0));
			//NOTE: this could be done via SetShaderParameterAnimation
		}

		// For HL optical stabilization (optional)
		public override Vector3 FocusWorldPoint => earthNode.WorldPosition;

		//Handle input:

		Vector3 earthPosBeforeManipulations;
		public override void OnGestureManipulationStarted() => earthPosBeforeManipulations = earthNode.Position;
		public override void OnGestureManipulationUpdated(Vector3 relativeHandPosition) =>
			earthNode.Position = relativeHandPosition + earthPosBeforeManipulations;

		public override void OnGestureTapped() {}
		public override void OnGestureDoubleTapped() {}
	}
}