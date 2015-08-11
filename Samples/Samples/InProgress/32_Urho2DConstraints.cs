using System;
using Urho;

class _32_Urho2DConstraints : Sample
{
	private Scene scene;
	private bool drawDebug;
	private Camera camera;
	private RigidBody2D dummyBody;
	private Node pickedNode;
	private Subscription mouseDownEventToken;
	private Subscription touchBeginEventToken;
	private Subscription touchMoveEventToken;
	private Subscription touchEndEventToken;
	private Subscription mouseMoveEventToken;
	private Subscription mouseButtonUpToken;

	public _32_Urho2DConstraints(Context ctx) : base(ctx) { }

	public override void Start()
	{
		base.Start();
		CreateScene();
		SimpleCreateInstructionsWithWASD(", Use PageUp PageDown to zoom.\n Space to toggle debug geometry and joints - F5 to save the scene.");
		Input.SetMouseVisible(true, false);
		SetupViewport();
		SubscribeToEvents();
	}

	private void SubscribeToEvents()
	{
		SubscribeToUpdate(args =>
			{
				SimpleMoveCamera2D(args.TimeStep);

				var input = Input;
				if (input.GetKeyDown(Key.PageUp))
					camera.Zoom = (camera.Zoom * 1.01f);

				if (input.GetKeyDown(Key.PageDown))
					camera.Zoom = (camera.Zoom * 0.99f);

				// Toggle physics debug geometry with space
				if (input.GetKeyPress(Key.Space))
					drawDebug = !drawDebug;

				// Save scene
				if (input.GetKeyPress(Key.F5))
				{
					File saveFile=new File(Context, FileSystem.ProgramDir + "Data/Scenes/Constraints.xml",  FileMode.Write);
#warning MISSIN_API SaveXML
					////scene.SaveXML(saveFile);
				}

			});
		SubscribeToPostRenderUpdate(args =>
			{
				// If draw debug mode is enabled, draw viewport debug geometry, which will show eg. drawable bounding boxes and skeleton
				// bones. Note that debug geometry has to be separately requested each frame. Disable depth test so that we can see the
				// bones properly
				if (drawDebug)
					scene.GetComponent<PhysicsWorld2D>().DrawDebugGeometry();
			});

		mouseDownEventToken = SubscribeToMouseButtonDown(HandleMouseButtonDown);


		// Unsubscribe the SceneUpdate event from base class to prevent camera pitch and yaw in 2D sample
		SceneUpdateEventToken.Unsubscribe();

		if (TouchEnabled)
		{
			touchBeginEventToken = SubscribeToTouchBegin(HandleTouchBegin3);
		}
	}

	private void HandleTouchBegin3(TouchBeginEventArgs args)
	{
		var graphics = Graphics;
		PhysicsWorld2D physicsWorld = scene.GetComponent<PhysicsWorld2D>();
		RigidBody2D rigidBody = physicsWorld.GetRigidBody(new Vector2(args.X, args.Y), uint.MaxValue); // Raycast for RigidBody2Ds to pick
		if (rigidBody != null)
		{
			pickedNode = rigidBody.Node;
			StaticSprite2D staticSprite = pickedNode.GetComponent<StaticSprite2D>();
			staticSprite.Color=(new Color(1.0f, 0.0f, 0.0f, 1.0f)); // Temporary modify color of the picked sprite
			rigidBody = pickedNode.GetComponent<RigidBody2D>();

			// Create a ConstraintMouse2D - Temporary apply this constraint to the pickedNode to allow grasping and moving with touch
			ConstraintMouse2D constraintMouse = pickedNode.CreateComponent<ConstraintMouse2D>();
			Vector3 pos = camera.ScreenToWorldPoint(new Vector3((float)args.X / graphics.Width, (float)args.Y / graphics.Height, 0.0f));
			constraintMouse.Target=new Vector2(pos.X, pos.Y);
			constraintMouse.MaxForce=1000 * rigidBody.Mass;
			constraintMouse.CollideConnected=true;
			constraintMouse.OtherBody=dummyBody;  // Use dummy body instead of rigidBody. It's better to create a dummy body automatically in ConstraintMouse2D
			constraintMouse.DampingRatio=0;
		}

		touchMoveEventToken = SubscribeToTouchMove(HandleTouchMove3);
		touchEndEventToken = SubscribeToTouchEnd(HandleTouchEnd3);
	}

	private void HandleTouchEnd3(TouchEndEventArgs args)
	{
		if (pickedNode != null)
		{
			StaticSprite2D staticSprite = pickedNode.GetComponent<StaticSprite2D>();
			staticSprite.Color = (new Color(1.0f, 1.0f, 1.0f, 1.0f)); // Restore picked sprite color
			pickedNode.RemoveComponent<ConstraintMouse2D>(); // Remove temporary constraint
			pickedNode = null;
		}

		touchMoveEventToken?.Unsubscribe();
		touchEndEventToken?.Unsubscribe();
	}

	private void HandleTouchMove3(TouchMoveEventArgs args)
	{
		if (pickedNode != null)
		{
			var graphics = Graphics;
			ConstraintMouse2D constraintMouse = pickedNode.GetComponent<ConstraintMouse2D>();
			Vector3 pos = camera.ScreenToWorldPoint(new Vector3((float)args.X / graphics.Width, (float)args.Y / graphics.Height, 0.0f));
			constraintMouse.Target=new Vector2(pos.X, pos.Y);
		}
	}

	private void HandleMouseButtonDown(MouseButtonDownEventArgs args)
	{
		Input input = Input;
		PhysicsWorld2D physicsWorld = scene.GetComponent<PhysicsWorld2D>();
		RigidBody2D rigidBody = physicsWorld.GetRigidBody(input.MousePosition.X, input.MousePosition.Y, uint.MaxValue); // Raycast for RigidBody2Ds to pick
		if (rigidBody != null)
		{
			pickedNode = rigidBody.Node;
			//log.Info(pickedNode.name);
			StaticSprite2D staticSprite = pickedNode.GetComponent<StaticSprite2D>();
			staticSprite.Color = (new Color(1.0f, 0.0f, 0.0f, 1.0f)); // Temporary modify color of the picked sprite

			// Create a ConstraintMouse2D - Temporary apply this constraint to the pickedNode to allow grasping and moving with the mouse
			ConstraintMouse2D constraintMouse = pickedNode.CreateComponent<ConstraintMouse2D>();
			constraintMouse.Target= GetMousePositionXY();
			constraintMouse.MaxForce=1000 * rigidBody.Mass;
			constraintMouse.CollideConnected=true;
			constraintMouse.OtherBody=dummyBody;  // Use dummy body instead of rigidBody. It's better to create a dummy body automatically in ConstraintMouse2D
			constraintMouse.DampingRatio=0.0f;
		}

		mouseMoveEventToken = SubscribeToMouseMove(HandleMouseMove);
		mouseButtonUpToken = SubscribeToMouseButtonUp(HandleMouseButtonUp);
	}

	private Vector2 GetMousePositionXY()
	{
		Input input = Input;
		var graphics = Graphics;
		Vector3 screenPoint = new Vector3((float)input.MousePosition.X / graphics.Width, (float)input.MousePosition.Y / graphics.Height, 0.0f);
		Vector3 worldPoint = camera.ScreenToWorldPoint(screenPoint);
		return new Vector2(worldPoint.X, worldPoint.Y);
	}


	private void HandleMouseMove(MouseMoveEventArgs args)
	{
		if (pickedNode != null)
		{
			ConstraintMouse2D constraintMouse = pickedNode.GetComponent<ConstraintMouse2D>();
			constraintMouse.Target=GetMousePositionXY();
		}

	}

	private void HandleMouseButtonUp(MouseButtonUpEventArgs args)
	{
		if (pickedNode != null)
		{
			StaticSprite2D staticSprite = pickedNode.GetComponent<StaticSprite2D>();
			staticSprite.Color = (new Color(1.0f, 1.0f, 1.0f, 1.0f)); // Restore picked sprite color
			pickedNode.RemoveComponent<ConstraintMouse2D>(); 
			pickedNode = null;
		}

		mouseMoveEventToken?.Unsubscribe();
		mouseButtonUpToken?.Unsubscribe();
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
		PhysicsWorld2D physicsWorld = scene.CreateComponent<PhysicsWorld2D>(); // Create 2D physics world component
		physicsWorld.DrawJoint=true; // Display the joints (Note that DrawDebugGeometry() must be set to true to acually draw the joints)
		drawDebug = true; // Set DrawDebugGeometry() to true

		// Create camera
		CameraNode = scene.CreateChild("Camera");
		// Set camera's position
		CameraNode.Position = (new Vector3(0.0f, 0.0f, 0.0f)); // Note that Z setting is discarded; use camera.zoom instead (see MoveCamera() below for example)

		camera = CameraNode.CreateComponent<Camera>();
		camera.SetOrthographic(true);

		var graphics = Graphics;
		
		camera.OrthoSize=(float)graphics.Height * PIXEL_SIZE;
		camera.Zoom=1.2f * Math.Min((float)graphics.Width / 1280.0f, (float)graphics.Height / 800.0f); // Set zoom according to user's resolution to ensure full visibility (initial zoom (1.2) is set for full visibility at 1280x800 resolution)

		// Set up a viewport to the Renderer subsystem so that the 3D scene can be seen
		Viewport viewport=new Viewport(Context, scene, camera, null);
		Renderer renderer = Renderer;
		renderer.SetViewport(0, viewport);

		Zone zone = renderer.DefaultZone;
		zone.FogColor = (new Color(0.1f, 0.1f, 0.1f)); // Set background color for the scene

		// Create 4x3 grid
		for (uint i = 0; i < 5; ++i)
		{
			Node edgeNode = scene.CreateChild("VerticalEdge");
			RigidBody2D edgeBody = edgeNode.CreateComponent<RigidBody2D>();
			if (dummyBody == null)
				dummyBody = edgeBody; // Mark first edge as dummy body (used by mouse pick)
			CollisionEdge2D edgeShape = edgeNode.CreateComponent<CollisionEdge2D>();
			edgeShape.SetVertices(new Vector2(i * 2.5f - 5.0f, -3.0f), new Vector2(i * 2.5f - 5.0f, 3.0f));
			edgeShape.Friction=0.5f; // Set friction
		}

		for (uint j = 0; j < 4; ++j)
		{
			Node edgeNode = scene.CreateChild("HorizontalEdge");
			/*RigidBody2D edgeBody = */
			edgeNode.CreateComponent<RigidBody2D>();
			CollisionEdge2D edgeShape = edgeNode.CreateComponent<CollisionEdge2D>();
			edgeShape.SetVertices(new Vector2(-5.0f, j * 2.0f - 3.0f), new Vector2(5.0f, j * 2.0f - 3.0f));
			edgeShape.Friction=0.5f; // Set friction
		}

		var cache = ResourceCache;

		// Create a box (will be cloned later)
		Node box = scene.CreateChild("Box");
		box.Position = (new Vector3(0.8f, -2.0f, 0.0f));
		StaticSprite2D boxSprite = box.CreateComponent<StaticSprite2D>();
		boxSprite.Sprite=cache.GetSprite2D("Urho2D/Box.png");
		RigidBody2D boxBody = box.CreateComponent<RigidBody2D>();
		boxBody.BodyType= BodyType2D.Dynamic;
		boxBody.LinearDamping=0.0f;
		boxBody.AngularDamping=0.0f;
		CollisionBox2D shape = box.CreateComponent<CollisionBox2D>(); // Create box shape
		shape.Size=new Vector2(0.32f, 0.32f); // Set size
		shape.Density=1.0f; // Set shape density (kilograms per meter squared)
		shape.Friction=0.5f; // Set friction
		shape.Restitution=0.1f; // Set restitution (slight bounce)

		// Create a ball (will be cloned later)
		Node ball = scene.CreateChild("Ball");
		ball.Position = (new Vector3(1.8f, -2.0f, 0.0f));
		StaticSprite2D ballSprite = ball.CreateComponent<StaticSprite2D>();
		ballSprite.Sprite=cache.GetSprite2D("Urho2D/Ball.png");
		RigidBody2D ballBody = ball.CreateComponent<RigidBody2D>();
		ballBody.BodyType= BodyType2D.Dynamic;
		ballBody.LinearDamping=0.0f;
		ballBody.AngularDamping=0.0f;
		CollisionCircle2D ballShape = ball.CreateComponent<CollisionCircle2D>(); // Create circle shape
		ballShape.Radius=0.16f; // Set radius
		ballShape.Density=1.0f; // Set shape density (kilograms per meter squared)
		ballShape.Friction=0.5f; // Set friction
		ballShape.Restitution=0.6f; // Set restitution: make it bounce

		// Create a polygon
		Node polygon = scene.CreateChild("Polygon");
		polygon.Position = (new Vector3(1.6f, -2.0f, 0.0f));
		polygon.SetScale(0.7f);
		StaticSprite2D polygonSprite = polygon.CreateComponent<StaticSprite2D>();
		polygonSprite.Sprite=cache.GetSprite2D("Urho2D/Aster.png");
		RigidBody2D polygonBody = polygon.CreateComponent<RigidBody2D>();
		polygonBody.BodyType= BodyType2D.Dynamic;
		CollisionPolygon2D polygonShape = polygon.CreateComponent<CollisionPolygon2D>();
		polygonShape.VertexCount=6; // Set number of vertices (mandatory when using SetVertex())
		polygonShape.SetVertex(0, new Vector2(-0.8f, -0.3f));
		polygonShape.SetVertex(1, new Vector2(0.5f, -0.8f));
		polygonShape.SetVertex(2, new Vector2(0.8f, -0.3f));
		polygonShape.SetVertex(3, new Vector2(0.8f, 0.5f));
		polygonShape.SetVertex(4, new Vector2(0.5f, 0.9f));
		polygonShape.SetVertex(5, new Vector2(-0.5f, 0.7f));
		polygonShape.Density=1.0f; // Set shape density (kilograms per meter squared)
		polygonShape.Friction=0.3f; // Set friction
		polygonShape.Restitution=0.0f; // Set restitution (no bounce)

		// Create a ConstraintDistance2D
		CreateFlag("ConstraintDistance2D", -4.97f, 3.0f); // Display Text3D flag
		Node boxDistanceNode = box.Clone(CreateMode.Replicated);
		Node ballDistanceNode = ball.Clone(CreateMode.Replicated);
		RigidBody2D ballDistanceBody = ballDistanceNode.GetComponent<RigidBody2D>();
		boxDistanceNode.Position = (new Vector3(-4.5f, 2.0f, 0.0f));
		ballDistanceNode.Position = (new Vector3(-3.0f, 2.0f, 0.0f));

		ConstraintDistance2D constraintDistance = boxDistanceNode.CreateComponent<ConstraintDistance2D>(); // Apply ConstraintDistance2D to box
		constraintDistance.OtherBody=ballDistanceBody; // Constrain ball to box
		constraintDistance.OwnerBodyAnchor=boxDistanceNode.Position2D;
		constraintDistance.OtherBodyAnchor=ballDistanceNode.Position2D;
		// Make the constraint soft (comment to make it rigid, which is its basic behavior)
		constraintDistance.FrequencyHz=4.0f;
		constraintDistance.DampingRatio=0.5f;

		// Create a ConstraintFriction2D ********** Not functional. From Box2d samples it seems that 2 anchors are required, Urho2D only provides 1, needs investigation ***********
		CreateFlag("ConstraintFriction2D", 0.03f, 1.0f); // Display Text3D flag
		Node boxFrictionNode = box.Clone(CreateMode.Replicated);
		Node ballFrictionNode = ball.Clone(CreateMode.Replicated);
		boxFrictionNode.Position = (new Vector3(0.5f, 0.0f, 0.0f));
		ballFrictionNode.Position = (new Vector3(1.5f, 0.0f, 0.0f));

		ConstraintFriction2D constraintFriction = boxFrictionNode.CreateComponent<ConstraintFriction2D>(); // Apply ConstraintDistance2D to box
		constraintFriction.OtherBody=ballFrictionNode.GetComponent<RigidBody2D>(); // Constraint ball to box

		// Create a ConstraintGear2D
		CreateFlag("ConstraintGear2D", -4.97f, -1.0f); // Display Text3D flag
		Node baseNode = box.Clone(CreateMode.Replicated);
		RigidBody2D tempBody = baseNode.GetComponent<RigidBody2D>(); // Get body to make it static
		tempBody.BodyType= BodyType2D.Static;
		baseNode.Position = (new Vector3(-3.7f, -2.5f, 0.0f));
		Node ball1Node = ball.Clone(CreateMode.Replicated);
		ball1Node.Position = (new Vector3(-4.5f, -2.0f, 0.0f));
		RigidBody2D ball1Body = ball1Node.GetComponent<RigidBody2D>();
		Node ball2Node = ball.Clone(CreateMode.Replicated);
		ball2Node.Position = (new Vector3(-3.0f, -2.0f, 0.0f));
		RigidBody2D ball2Body = ball2Node.GetComponent<RigidBody2D>();

		ConstraintRevolute2D gear1 = baseNode.CreateComponent<ConstraintRevolute2D>(); // Apply constraint to baseBox
		gear1.OtherBody=ball1Body; // Constrain ball1 to baseBox
		gear1.Anchor=ball1Node.Position2D;
		ConstraintRevolute2D gear2 = baseNode.CreateComponent<ConstraintRevolute2D>(); // Apply constraint to baseBox
		gear2.OtherBody=ball2Body; // Constrain ball2 to baseBox
		gear2.Anchor=ball2Node.Position2D;

		ConstraintGear2D constraintGear = ball1Node.CreateComponent<ConstraintGear2D>(); // Apply constraint to ball1
		constraintGear.OtherBody=ball2Body; // Constrain ball2 to ball1
		constraintGear.OwnerConstraint=gear1;
		constraintGear.OtherConstraint=gear2;
		constraintGear.Ratio=1.0f;

		ball1Body.ApplyAngularImpulse(0.015f, true); // Animate

		// Create a vehicle from a compound of 2 ConstraintWheel2Ds
		CreateFlag("ConstraintWheel2Ds compound", -2.45f, -1.0f); // Display Text3D flag
		Node car = box.Clone(CreateMode.Replicated);
		car.Scale=new Vector3(4.0f, 1.0f, 0.0f);
		car.Position = (new Vector3(-1.2f, -2.3f, 0.0f));
		StaticSprite2D tempSprite = car.GetComponent<StaticSprite2D>(); // Get car Sprite in order to draw it on top
		tempSprite.OrderInLayer=0; // Draw car on top of the wheels (set to -1 to draw below)
		Node ball1WheelNode = ball.Clone(CreateMode.Replicated);
		ball1WheelNode.Position = (new Vector3(-1.6f, -2.5f, 0.0f));
		Node ball2WheelNode = ball.Clone(CreateMode.Replicated);
		ball2WheelNode.Position = (new Vector3(-0.8f, -2.5f, 0.0f));

		ConstraintWheel2D wheel1 = car.CreateComponent<ConstraintWheel2D>();
		wheel1.OtherBody=ball1WheelNode.GetComponent<RigidBody2D>();
		wheel1.Anchor=ball1WheelNode.Position2D;
		wheel1.Axis=new Vector2(0.0f, 1.0f);
		wheel1.MaxMotorTorque=20.0f;
		wheel1.FrequencyHz=4.0f;
		wheel1.DampingRatio=0.4f;

		ConstraintWheel2D wheel2 = car.CreateComponent<ConstraintWheel2D>();
		wheel2.OtherBody=ball2WheelNode.GetComponent<RigidBody2D>();
		wheel2.Anchor=ball2WheelNode.Position2D;
		wheel2.Axis=new Vector2(0.0f, 1.0f);
		wheel2.MaxMotorTorque=10.0f;
		wheel2.FrequencyHz=4.0f;
		wheel2.DampingRatio=0.4f;

		// ConstraintMotor2D
		CreateFlag("ConstraintMotor2D", 2.53f, -1.0f); // Display Text3D flag
		Node boxMotorNode = box.Clone(CreateMode.Replicated);
		tempBody = boxMotorNode.GetComponent<RigidBody2D>(); // Get body to make it static
		tempBody.BodyType = BodyType2D.Static;
		Node ballMotorNode = ball.Clone(CreateMode.Replicated);
		boxMotorNode.Position = (new Vector3(3.8f, -2.1f, 0.0f));
		ballMotorNode.Position = (new Vector3(3.8f, -1.5f, 0.0f));

		ConstraintMotor2D constraintMotor = boxMotorNode.CreateComponent<ConstraintMotor2D>();
		constraintMotor.OtherBody=ballMotorNode.GetComponent<RigidBody2D>(); // Constrain ball to box
		constraintMotor.LinearOffset=new Vector2(0.0f, 0.8f); // Set ballNode position relative to boxNode position = (0,0)
		constraintMotor.AngularOffset=0.1f;
		constraintMotor.MaxForce=5.0f;
		constraintMotor.MaxTorque=10.0f;
		constraintMotor.CorrectionFactor=1.0f;
		constraintMotor.CollideConnected=true; // doesn't work

		// ConstraintMouse2D is demonstrated in HandleMouseButtonDown() function. It is used to "grasp" the sprites with the mouse.
		CreateFlag("ConstraintMouse2D", 0.03f, -1.0f); // Display Text3D flag

		// Create a ConstraintPrismatic2D
		CreateFlag("ConstraintPrismatic2D", 2.53f, 3.0f); // Display Text3D flag
		Node boxPrismaticNode = box.Clone(CreateMode.Replicated);
		tempBody = boxPrismaticNode.GetComponent<RigidBody2D>(); // Get body to make it static
		tempBody.BodyType = BodyType2D.Static;
		Node ballPrismaticNode = ball.Clone(CreateMode.Replicated);
		boxPrismaticNode.Position = new Vector3(3.3f, 2.5f, 0.0f);
		ballPrismaticNode.Position = new Vector3(4.3f, 2.0f, 0.0f);

		ConstraintPrismatic2D constraintPrismatic = boxPrismaticNode.CreateComponent<ConstraintPrismatic2D>();
		constraintPrismatic.OtherBody=ballPrismaticNode.GetComponent<RigidBody2D>(); // Constrain ball to box
		constraintPrismatic.Axis=new Vector2(1.0f, 1.0f); // Slide from [0,0] to [1,1]
		constraintPrismatic.Anchor=new Vector2(4.0f, 2.0f);
		constraintPrismatic.LowerTranslation=-1.0f;
		constraintPrismatic.UpperTranslation=0.5f;
		constraintPrismatic.EnableLimit=true;
		constraintPrismatic.MaxMotorForce=1.0f;
		constraintPrismatic.MotorSpeed=0.0f;

		// ConstraintPulley2D
		CreateFlag("ConstraintPulley2D", 0.03f, 3.0f); // Display Text3D flag
		Node boxPulleyNode = box.Clone(CreateMode.Replicated);
		Node ballPulleyNode = ball.Clone(CreateMode.Replicated);
		boxPulleyNode.Position = (new Vector3(0.5f, 2.0f, 0.0f));
		ballPulleyNode.Position = (new Vector3(2.0f, 2.0f, 0.0f));

		ConstraintPulley2D constraintPulley = boxPulleyNode.CreateComponent<ConstraintPulley2D>(); // Apply constraint to box
		constraintPulley.OtherBody=ballPulleyNode.GetComponent<RigidBody2D>(); // Constrain ball to box
		constraintPulley.OwnerBodyAnchor=boxPulleyNode.Position2D;
		constraintPulley.OtherBodyAnchor=ballPulleyNode.Position2D;
		constraintPulley.OwnerBodyGroundAnchor=boxPulleyNode.Position2D + new Vector2(0.0f, 1.0f);
		constraintPulley.OtherBodyGroundAnchor=ballPulleyNode.Position2D + new Vector2(0.0f, 1.0f);
		constraintPulley.Ratio=1.0f; // Weight ratio between ownerBody and otherBody

		// Create a ConstraintRevolute2D
		CreateFlag("ConstraintRevolute2D", -2.45f, 3.0f); // Display Text3D flag
		Node boxRevoluteNode = box.Clone(CreateMode.Replicated);
		tempBody = boxRevoluteNode.GetComponent<RigidBody2D>(); // Get body to make it static
		tempBody.BodyType = BodyType2D.Static;
		Node ballRevoluteNode = ball.Clone(CreateMode.Replicated);
		boxRevoluteNode.Position = (new Vector3(-2.0f, 1.5f, 0.0f));
		ballRevoluteNode.Position = (new Vector3(-1.0f, 2.0f, 0.0f));

		ConstraintRevolute2D constraintRevolute = boxRevoluteNode.CreateComponent<ConstraintRevolute2D>(); // Apply constraint to box
		constraintRevolute.OtherBody=ballRevoluteNode.GetComponent<RigidBody2D>(); // Constrain ball to box
		constraintRevolute.Anchor=new Vector2(-1.0f, 1.5f);
		constraintRevolute.LowerAngle=-1.0f; // In radians
		constraintRevolute.UpperAngle=0.5f; // In radians
		constraintRevolute.EnableLimit=true;
		constraintRevolute.MaxMotorTorque=10.0f;
		constraintRevolute.MotorSpeed=0.0f;
		constraintRevolute.EnableMotor=true;

		// Create a ConstraintRope2D
		CreateFlag("ConstraintRope2D", -4.97f, 1.0f); // Display Text3D flag
		Node boxRopeNode = box.Clone(CreateMode.Replicated);
		tempBody = boxRopeNode.GetComponent<RigidBody2D>();
		tempBody.BodyType = BodyType2D.Static;
		Node ballRopeNode = ball.Clone(CreateMode.Replicated);
		boxRopeNode.Position = (new Vector3(-3.7f, 0.7f, 0.0f));
		ballRopeNode.Position = (new Vector3(-4.5f, 0.0f, 0.0f));

		ConstraintRope2D constraintRope = boxRopeNode.CreateComponent<ConstraintRope2D>();
		constraintRope.OtherBody=ballRopeNode.GetComponent<RigidBody2D>(); // Constrain ball to box
		constraintRope.OwnerBodyAnchor=new Vector2(0.0f, -0.5f); // Offset from box (OwnerBody) : the rope is rigid from OwnerBody center to this ownerBodyAnchor
		constraintRope.MaxLength=0.9f; // Rope length
		constraintRope.CollideConnected=true;

		// Create a ConstraintWeld2D
		CreateFlag("ConstraintWeld2D", -2.45f, 1.0f); // Display Text3D flag
		Node boxWeldNode = box.Clone(CreateMode.Replicated);
		Node ballWeldNode = ball.Clone(CreateMode.Replicated);
		boxWeldNode.Position = (new Vector3(-0.5f, 0.0f, 0.0f));
		ballWeldNode.Position = (new Vector3(-2.0f, 0.0f, 0.0f));

		ConstraintWeld2D constraintWeld = boxWeldNode.CreateComponent<ConstraintWeld2D>();
		constraintWeld.OtherBody=ballWeldNode.GetComponent<RigidBody2D>(); // Constrain ball to box
		constraintWeld.Anchor=boxWeldNode.Position2D;
		constraintWeld.FrequencyHz=4.0f;
		constraintWeld.DampingRatio=0.5f;

		// Create a ConstraintWheel2D
		CreateFlag("ConstraintWheel2D", 2.53f, 1.0f); // Display Text3D flag
		Node boxWheelNode = box.Clone(CreateMode.Replicated);
		Node ballWheelNode = ball.Clone(CreateMode.Replicated);
		boxWheelNode.Position = (new Vector3(3.8f, 0.0f, 0.0f));
		ballWheelNode.Position = (new Vector3(3.8f, 0.9f, 0.0f));

		ConstraintWheel2D constraintWheel = boxWheelNode.CreateComponent<ConstraintWheel2D>();
		constraintWheel.OtherBody=ballWheelNode.GetComponent<RigidBody2D>(); // Constrain ball to box
		constraintWheel.Anchor=ballWheelNode.Position2D;
		constraintWheel.Axis=new Vector2(0.0f, 1.0f);
		constraintWheel.EnableMotor=true;
		constraintWheel.MaxMotorTorque=1.0f;
		constraintWheel.MotorSpeed=0.0f;
		constraintWheel.FrequencyHz=4.0f;
		constraintWheel.DampingRatio=0.5f;
		constraintWheel.CollideConnected=true; // doesn't work


	}

	private void CreateFlag(string text, float x, float y) // Used to create Tex3D flags
	{
		Node flagNode = scene.CreateChild("Flag");
		flagNode.Position = (new Vector3(x, y, 0.0f));
		Text3D flag3D = flagNode.CreateComponent<Text3D>(); // We use Text3D in order to make the text affected by zoom (so that it sticks to 2D)
		flag3D.Text=text;
		var cache = ResourceCache;
		flag3D.SetFont(cache.GetFont("Fonts/Anonymous Pro.ttf"), 15);
	}
}
