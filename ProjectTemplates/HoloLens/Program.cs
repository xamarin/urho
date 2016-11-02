using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Core;
using Urho;
using Urho.Actions;
using Urho.HoloLens;
using Urho.Shapes;

namespace $safeprojectname$
{
	/// <summary>
	/// Windows Holographic application using SharpDX.
	/// </summary>
	internal class Program
	{
		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		[MTAThread]
		private static void Main()
		{
			CoreApplication.Run(new AppViewSource());
		}

		class AppViewSource : IFrameworkViewSource
		{
			public IFrameworkView CreateView() => UrhoAppView.Create<HelloWorldApplication>(new ApplicationOptions("Data"));
		}
	}


	public class HelloWorldApplication : HoloApplication
	{
		Node earthNode;

		public HelloWorldApplication(ApplicationOptions opts) : base(opts) { }

		protected override async void Start()
		{
			// base.Start() creates a basic scene
			base.Start();

			// Create a node for the Earth
			earthNode = Scene.CreateChild();
			earthNode.Position = new Vector3(0, 0, 1); //one meter away
			earthNode.SetScale(0.2f); //20cm

			//subscribe to some input events:
			EnableGestureManipulation = true;
			EnableGestureTapped = true;

			// Create a Sphere component which is basically 
			// a StaticModel with CoreData\Models\Sphere.mdl model and NoTexture material.
			var earth = earthNode.CreateComponent<Sphere>();
			// Override the default material (material is a set of tecniques, parameters and textures)
			Material earthMaterial = ResourceCache.GetMaterial("Materials/Earth.xml");
			earth.SetMaterial(earthMaterial);
			
			// Same for the Moon
			var moonNode = earthNode.CreateChild();
			const float moonRelativeSize = 1738.1f / 3963.2f;
			moonNode.SetScale(moonRelativeSize); 
			moonNode.Position = new Vector3(1.6f, 0, 0);
			var moon = moonNode.CreateComponent<Sphere>();
			// Material.FromImage is the easiest way to create a material from an image (using Diff.xml technique)
			moon.SetMaterial(Material.FromImage("Textures/Moon.jpg"));

			// Run a few actions to spin the Earth and the Moon, do not await these calls as they have RepeatForever action
			earthNode.RunActions(new RepeatForever(new RotateBy(duration: 1f, deltaAngleX: 0, deltaAngleY: -4, deltaAngleZ: 0)));
			moonNode.RunActions(new RepeatForever(new RotateAroundBy(1f, earthNode.WorldPosition, 0, -3, 0)));

			// requires Microphone capability enabled
			await RegisterCortanaCommands(new Dictionary<string, Action> {
					{ "bigger",  () => earthNode.Scale *= 1.2f },
					{ "smaller", () => earthNode.Scale *= 0.8f }
				});
			await TextToSpeech("Hello world from UrhoSharp!");
		}

		// HoloLens optical stabilization (optional)
		public override Vector3 FocusWorldPoint => earthNode.WorldPosition;

		protected override void OnUpdate(float timeStep)
		{
		}

		// handle input:

		Vector3 earthPostionBeforeManipulations;
		public override void OnGestureManipulationStarted()
		{
			earthPostionBeforeManipulations = earthNode.Position;
		}

		public override void OnGestureManipulationUpdated(Vector3 relativeHandPosition)
		{
			earthNode.Position = relativeHandPosition + earthPostionBeforeManipulations;
		}


		public override void OnGestureTapped()
		{
		}
	}
}