using System.Threading.Tasks;

namespace Urho
{
	public class _40_Actions : Sample
	{
		public _40_Actions(Context ctx) : base(ctx) { }

		Scene scene;
		Node mushroom;

		public override void Start()
		{
			base.Start();
			CreateScene();
			SimpleCreateInstructionsWithWASD();
			SubscribeToUpdate(OnUpdate);
		}

		private void OnUpdate(UpdateEventArgs args)
		{
			var timeStep = args.TimeStep;
			SimpleMoveCamera3D(timeStep);
			mushroom.ActionManager.Update(timeStep);
			//mushroom.Rotate(new Quaternion(0, 50 * timeStep, 0), TransformSpace.Local);
		}

		void CreateScene()
		{
			var cache = ResourceCache;
			scene = new Scene(Context);

			// Create the Octree component to the scene. This is required before adding any drawable components, or else nothing will
			// show up. The default octree volume will be from (-1000, -1000, -1000) to (1000, 1000, 1000) in world coordinates; it
			// is also legal to place objects outside the volume but their visibility can then not be checked in a hierarchically
			// optimizing manner
			scene.CreateComponent<Octree>();

			// Create a child scene node (at world origin) and a StaticModel component into it. Set the StaticModel to show a simple
			// plane mesh with a "stone" material. Note that naming the scene nodes is optional. Scale the scene node larger
			// (100 x 100 world units)
			var planeNode = scene.CreateChild("Plane");
			planeNode.Scale = new Vector3(100, 1, 100);
			var planeObject = planeNode.CreateComponent<StaticModel>();
			planeObject.Model = cache.GetModel("Models/Plane.mdl");
			planeObject.SetMaterial(cache.GetMaterial("Materials/StoneTiled.xml"));

			// Create a directional light to the world so that we can see something. The light scene node's orientation controls the
			// light direction; we will use the SetDirection() function which calculates the orientation from a forward direction vector.
			// The light will use default settings (white light, no shadows)
			var lightNode = scene.CreateChild("DirectionalLight");
			lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f)); // The direction vector does not need to be normalized
			var light = lightNode.CreateComponent<Light>();
			light.LightType = LightType.LIGHT_DIRECTIONAL;

			mushroom = scene.CreateChild("Mushroom");
			mushroom.Position = new Vector3(0, 0, 15);
			mushroom.Rotation = new Quaternion(0, 180, 0);
			mushroom.SetScale(2f);

			var mushroomObject = mushroom.CreateComponent<StaticModel>();
			mushroomObject.Model = cache.GetModel("Models/Mushroom.mdl");
			mushroomObject.SetMaterial(cache.GetMaterial("Materials/Mushroom.xml"));

			CameraNode = scene.CreateChild("camera");
			var camera = CameraNode.CreateComponent<Camera>();
			CameraNode.Position = new Vector3(0, 5, 0);
			Renderer.SetViewport(0, new Viewport(Context, scene, camera, null));

			DoActions();
		}

		private async void DoActions()
		{
			MoveBy moveForwardAction = new MoveBy(duration: 1.5f, position: new Vector3(0, 0, 15));
			MoveBy moveLeftAction = new MoveBy(duration: 1.5f, position: new Vector3(10, 0, 0));
			ScaleBy makeBiggerAction = new ScaleBy(duration: 1.5f, scale: 3);
			RotateTo rotateYAction = new RotateTo(duration: 2f, deltaAngleX: 0, deltaAngleY: 5);
			MoveTo moveToInitialPositionAction = new MoveTo(duration: 2, position: new Vector3(0, 0, 15));

			await mushroom.RunActionsAsync(
				moveForwardAction,
				new Parallel(moveLeftAction, makeBiggerAction),
				new Parallel(moveToInitialPositionAction, rotateYAction, makeBiggerAction.Reverse()));
		}
	}
}
