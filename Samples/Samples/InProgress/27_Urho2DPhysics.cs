using System;
using Urho;

class _27_Urho2DPhysics : Sample
{
	private Scene scene;
	private const uint NUM_OBJECTS = 100;

	public _27_Urho2DPhysics(Context ctx) : base(ctx) { }

	public override void Start()
	{
		base.Start();
		CreateScene();
		SimpleCreateInstructionsWithWASD(", use PageUp PageDown keys to zoom.");
		SetupViewport();
		SubscribeToEvents();
	}

	private void SubscribeToEvents()
	{
		SubscribeToUpdate(args => SimpleMoveCamera2D(args.TimeStep));
		
		SceneUpdateEventToken.Unsubscribe();
	}

	private void SetupViewport()
	{
		var renderer = Renderer;
		renderer.SetViewport(0, new Viewport(Context, scene, CameraNode.GetComponent<Camera>(), null));
	}

	private void CreateScene()
	{
		scene = new Scene(Context);
		scene.CreateComponent<Octree>();
		scene.CreateComponent<DebugRenderer>();
		// Create camera node
		CameraNode = scene.CreateChild("Camera");
		// Set camera's position
		CameraNode.Position = (new Vector3(0.0f, 0.0f, -10.0f));

		Camera camera = CameraNode.CreateComponent<Camera>();
		camera.SetOrthographic(true);

		var graphics = Graphics;
		camera.OrthoSize=(float)graphics.Height * PIXEL_SIZE;
		camera.Zoom = 1.2f * Math.Min((float)graphics.Width / 1280.0f, (float)graphics.Height / 800.0f); // Set zoom according to user's resolution to ensure full visibility (initial zoom (1.2) is set for full visibility at 1280x800 resolution)

		// Create 2D physics world component
		scene.CreateComponent<PhysicsWorld2D>();

		var cache = ResourceCache;
		Sprite2D boxSprite = cache.GetSprite2D("Urho2D/Box.png");
		Sprite2D ballSprite = cache.GetSprite2D("Urho2D/Ball.png");

		// Create ground.
		Node groundNode = scene.CreateChild("Ground");
		groundNode.Position = (new Vector3(0.0f, -3.0f, 0.0f));
		groundNode.Scale=new Vector3(200.0f, 1.0f, 0.0f);

		// Create 2D rigid body for gound
		/*RigidBody2D groundBody = */
		groundNode.CreateComponent<RigidBody2D>();

		StaticSprite2D groundSprite = groundNode.CreateComponent<StaticSprite2D>();
		groundSprite.Sprite=boxSprite;

		// Create box collider for ground
		CollisionBox2D groundShape = groundNode.CreateComponent<CollisionBox2D>();
		// Set box size
		groundShape.Size=new Vector2(0.32f, 0.32f);
		// Set friction
		groundShape.Friction = 0.5f;

		for (uint i = 0; i < NUM_OBJECTS; ++i)
		{
			Node node = scene.CreateChild("RigidBody");
			node.Position = (new Vector3(NextRandom(-0.1f, 0.1f), 5.0f + i * 0.4f, 0.0f));

			// Create rigid body
			RigidBody2D body = node.CreateComponent<RigidBody2D>();
			body.BodyType = BodyType2D.BT_DYNAMIC; //https://github.com/xamarin/urho/issues/49

			StaticSprite2D staticSprite = node.CreateComponent<StaticSprite2D>();

			if (i % 2 == 0)
			{
				staticSprite.Sprite = boxSprite;

				// Create box
				CollisionBox2D box = node.CreateComponent<CollisionBox2D>();
				// Set size
				box.Size=new Vector2(0.32f, 0.32f);
				// Set density
				box.Density=1.0f;
				// Set friction
				box.Friction = 0.5f;
				// Set restitution
				box.Restitution=0.1f;
			}
			else
			{
				staticSprite.Sprite=ballSprite;

				// Create circle
				CollisionCircle2D circle = node.CreateComponent<CollisionCircle2D>();
				// Set radius
				circle.Radius=0.16f;
				// Set density
				circle.Density=1.0f;
				// Set friction.
				circle.Friction = 0.5f;
				// Set restitution
				circle.Restitution=0.1f;
			}
		}

	}
}
