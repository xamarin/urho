using System;
using Urho;

class _18_CharacterDemo : Sample
{
	private Scene scene;

	public const float CAMERA_MIN_DIST = 1.0f;
	public const float CAMERA_INITIAL_DIST = 5.0f;
	public const float CAMERA_MAX_DIST = 20.0f;

	public const float GYROSCOPE_THRESHOLD = 0.1f;

	public const int CTRL_FORWARD = 1;
	public const int CTRL_BACK = 2;
	public const int CTRL_LEFT = 4;
	public const int CTRL_RIGHT = 8;
	public const int CTRL_JUMP = 16;

	public const float MOVE_FORCE = 0.8f;
	public const float INAIR_MOVE_FORCE = 0.02f;
	public const float BRAKE_FORCE = 0.2f;
	public const float JUMP_FORCE = 7.0f;
	public const float YAW_SENSITIVITY = 0.1f;
	public const float INAIR_THRESHOLD_TIME = 0.1f;

	/// Touch utility object.
	Touch touch;
	/// The controllable character component.
	Character character;
	/// First person camera flag.
	bool firstPerson = false;

	public _18_CharacterDemo(Context ctx) : base(ctx) { }

	public override void Start()
	{
		base.Start();
		if (TouchEnabled)
			touch = new Touch(Context, TouchSensitivity, Input);
		CreateScene();
		CreateCharacter();
		SimpleCreateInstructionsWithWASD("\nSpace to jump, F to toggle 1st/3rd person\nF5 to save scene, F7 to load");
		SubscribeToEvents();
	}

	private void SubscribeToEvents()
	{
		SubscribeToUpdate(HandleUpdate);
		SubscribeToPostUpdate(HandlePostUpdate);
		SubscribeToPhysicsPreStep(HandlePhysicsPreStep);
		
		// Unsubscribe the SceneUpdate event from base class as the camera node is being controlled in HandlePostUpdate() in this sample
		SceneUpdateEventToken.Unsubscribe();
	}

	private void HandlePhysicsPreStep(PhysicsPreStepEventArgs args)
	{
		character?.FixedUpdate(args.TimeStep);
	}

	private void HandlePostUpdate(PostUpdateEventArgs args)
	{
		if (character == null)
			return;

		Node characterNode = character.Node;

		// Get camera lookat dir from character yaw + pitch
		Quaternion rot = characterNode.Rotation;
		Quaternion dir = rot * Quaternion.FromAxisAngle(Vector3.UnitX, character.Controls.Pitch);

		// Turn head to camera pitch, but limit to avoid unnatural animation
		Node headNode = characterNode.GetChild("Bip01_Head", true);
		float limitPitch = Clamp(character.Controls.Pitch, -45.0f, 45.0f);
		Quaternion headDir = rot * Quaternion.FromAxisAngle(new Vector3(1.0f, 0.0f, 0.0f), limitPitch);
		// This could be expanded to look at an arbitrary target, now just look at a point in front
		Vector3 headWorldTarget = headNode.WorldPosition + headDir * new Vector3(0.0f, 0.0f, 1.0f);
		headNode.LookAt(headWorldTarget, new Vector3(0.0f, 1.0f, 0.0f), TransformSpace.World);
		// Correct head orientation because LookAt assumes Z = forward, but the bone has been authored differently (Y = forward)
		headNode.Rotate(new Quaternion(0.0f, 90.0f, 90.0f), TransformSpace.Local);

		if (firstPerson)
		{
			CameraNode.Position = headNode.WorldPosition + rot * new Vector3(0.0f, 0.15f, 0.2f);
			CameraNode.Rotation = dir;
		}
		else
		{
			// Third person camera: position behind the character
			Vector3 aimPoint = characterNode.Position + rot * new Vector3(0.0f, 1.7f, 0.0f);

			// Collide camera ray with static physics objects (layer bitmask 2) to ensure we see the character properly
			Vector3 rayDir = dir * new Vector3(0f, 0f, -1f);
			float rayDistance = touch != null ? touch.CameraDistance : CAMERA_INITIAL_DIST;

			PhysicsRaycastResult result = new PhysicsRaycastResult();
			scene.GetComponent<PhysicsWorld>().RaycastSingle(ref result, new Ray { Origin = aimPoint, Direction = rayDir }, rayDistance, 2);
			if (result.Body != null)
				rayDistance = Math.Min(rayDistance, result.Distance);
			rayDistance = Clamp(rayDistance, CAMERA_MIN_DIST, CAMERA_MAX_DIST);

			CameraNode.Position = aimPoint + rayDir * rayDistance;
			CameraNode.Rotation = dir;
		}
	}

	private void HandleUpdate(UpdateEventArgs args)
	{
		Input input = Input;

		if (character != null)
		{
			// Clear previous controls
			character.Controls.Set(CTRL_FORWARD | CTRL_BACK | CTRL_LEFT | CTRL_RIGHT | CTRL_JUMP, false);

			// Update controls using touch utility class
			if (touch != null)
				touch.UpdateTouches(character.Controls);

			// Update controls using keys
			UI ui = UI;
			if (ui.FocusElement == null)
			{
				if (touch == null || !touch.UseGyroscope)
				{
					character.Controls.Set(CTRL_FORWARD, input.GetKeyDown(Key.W));
					character.Controls.Set(CTRL_BACK, input.GetKeyDown(Key.S));
					character.Controls.Set(CTRL_LEFT, input.GetKeyDown(Key.A));
					character.Controls.Set(CTRL_RIGHT, input.GetKeyDown(Key.D));
				}
				character.Controls.Set(CTRL_JUMP, input.GetKeyDown(Key.Space));

				// Add character yaw & pitch from the mouse motion or touch input
				if (TouchEnabled)
				{
					for (uint i = 0; i < input.NumTouches; ++i)
					{
						TouchState state;
						input.TryGetTouch(i, out state);
						if (state.TouchedElement() != null)    // Touch on empty space
						{
							Camera camera = CameraNode.GetComponent<Camera>();
							if (camera == null)
								return;

							var graphics = Graphics;
							character.Controls.Yaw += TouchSensitivity * camera.Fov / graphics.Height * state.Delta.X;
							character.Controls.Pitch += TouchSensitivity * camera.Fov / graphics.Height * state.Delta.Y;
						}
					}
				}
				else
				{
					character.Controls.Yaw += (float)input.MouseMove.X * YAW_SENSITIVITY;
					character.Controls.Pitch += (float)input.MouseMove.Y * YAW_SENSITIVITY;
				}
				// Limit pitch
				character.Controls.Pitch = Clamp(character.Controls.Pitch, -80.0f, 80.0f);

				// Switch between 1st and 3rd person
				if (input.GetKeyPress(Key.F))
					firstPerson = !firstPerson;

				// Turn on/off gyroscope on mobile platform
				if (touch != null && input.GetKeyPress(Key.G))
					touch.UseGyroscope = !touch.UseGyroscope;

				if (input.GetKeyPress(Key.F5))
				{
					scene.SaveXML(FileSystem.ProgramDir + "Data/Scenes/CharacterDemo.xml", "\t");
				}
				if (input.GetKeyPress(Key.F7))
				{
					scene.LoadXML(FileSystem.ProgramDir + "Data/Scenes/CharacterDemo.xml");
					Node characterNode = scene.GetChild("Jack", true);
					if (characterNode != null)
					{
						character = characterNode.GetComponent<Character>();
					}
				}
			}
			
			// Set rotation already here so that it's updated every rendering frame instead of every physics frame
			if (character != null)
				character.Node.Rotation = Quaternion.FromAxisAngle(Vector3.UnitY, character.Controls.Yaw);
		}
	}

	private void CreateScene()
	{
		var cache = ResourceCache;

		scene = new Scene(Context);

		// Create scene subsystem components
		scene.CreateComponent<Octree>();
		scene.CreateComponent<PhysicsWorld>();

		// Create camera and define viewport. We will be doing load / save, so it's convenient to create the camera outside the scene,
		// so that it won't be destroyed and recreated, and we don't have to redefine the viewport on load
		CameraNode = new Node(Context);
		Camera camera = CameraNode.CreateComponent<Camera>();
		camera.FarClip = 300.0f;
		Renderer.SetViewport(0, new Viewport(Context, scene, camera, null));

		// Create static scene content. First create a zone for ambient lighting and fog control
		Node zoneNode = scene.CreateChild("Zone");
		Zone zone = zoneNode.CreateComponent<Zone>();
		zone.AmbientColor = new Color(0.15f, 0.15f, 0.15f);
		zone.FogColor = new Color(0.5f, 0.5f, 0.7f);
		zone.FogStart = 100.0f;
		zone.FogEnd = 300.0f;
		zone.SetBoundingBox(new BoundingBox(-1000.0f, 1000.0f));

		// Create a directional light with cascaded shadow mapping
		Node lightNode = scene.CreateChild("DirectionalLight");
		lightNode.SetDirection(new Vector3(0.3f, -0.5f, 0.425f));
		Light light = lightNode.CreateComponent<Light>();
		light.LightType = LightType.LIGHT_DIRECTIONAL;
		light.CastShadows = true;
		light.ShadowBias = new BiasParameters(0.00025f, 0.5f);
		light.ShadowCascade = new CascadeParameters(10.0f, 50.0f, 200.0f, 0.0f, 0.8f);
		light.SpecularIntensity = 0.5f;

		// Create the floor object
		Node floorNode = scene.CreateChild("Floor");
		floorNode.Position = new Vector3(0.0f, -0.5f, 0.0f);
		floorNode.Scale = new Vector3(200.0f, 1.0f, 200.0f);
		StaticModel sm = floorNode.CreateComponent<StaticModel>();
		sm.Model = cache.GetModel("Models/Box.mdl");
		sm.SetMaterial(cache.GetMaterial("Materials/Stone.xml"));

		RigidBody body = floorNode.CreateComponent<RigidBody>();
		// Use collision layer bit 2 to mark world scenery. This is what we will raycast against to prevent camera from going
		// inside geometry
		body.CollisionLayer = 2;
		CollisionShape shape = floorNode.CreateComponent<CollisionShape>();
		shape.SetBox(Vector3.One, Vector3.Zero, Quaternion.Identity);

		// Create mushrooms of varying sizes
		const uint NUM_MUSHROOMS = 60;
		for (uint i = 0; i < NUM_MUSHROOMS; ++i)
		{
			Node objectNode = scene.CreateChild("Mushroom");
			objectNode.Position = new Vector3(NextRandom(180.0f) - 90.0f, 0.0f, NextRandom(180.0f) - 90.0f);
			objectNode.Rotation = new Quaternion(0.0f, NextRandom(360.0f), 0.0f);
			objectNode.SetScale(2.0f + NextRandom(5.0f));
			StaticModel o = objectNode.CreateComponent<StaticModel>();
			o.Model = cache.GetModel("Models/Mushroom.mdl");
			o.SetMaterial(cache.GetMaterial("Materials/Mushroom.xml"));
			o.CastShadows = true;

			body = objectNode.CreateComponent<RigidBody>();
			body.CollisionLayer = 2;
			shape = objectNode.CreateComponent<CollisionShape>();
			shape.SetTriangleMesh(o.Model, 0, Vector3.One, Vector3.Zero, Quaternion.Identity);
		}

		// Create movable boxes. Let them fall from the sky at first
		const uint NUM_BOXES = 100;
		for (uint i = 0; i < NUM_BOXES; ++i)
		{
			float scale = NextRandom(2.0f) + 0.5f;

			Node objectNode = scene.CreateChild("Box");
			objectNode.Position = new Vector3(NextRandom(180.0f) - 90.0f, NextRandom(10.0f) + 10.0f, NextRandom(180.0f) - 90.0f);
			objectNode.Rotation = new Quaternion(NextRandom(360.0f), NextRandom(360.0f), NextRandom(360.0f));
			objectNode.SetScale(scale);
			StaticModel o = objectNode.CreateComponent<StaticModel>();
			o.Model = cache.GetModel("Models/Box.mdl");
			o.SetMaterial(cache.GetMaterial("Materials/Stone.xml"));
			o.CastShadows = true;

			body = objectNode.CreateComponent<RigidBody>();
			body.CollisionLayer = 2;
			// Bigger boxes will be heavier and harder to move
			body.Mass = scale * 2.0f;
			shape = objectNode.CreateComponent<CollisionShape>();
			shape.SetBox(Vector3.One, Vector3.Zero, Quaternion.Identity);
		}
	}

	private void CreateCharacter()
	{
		var cache = ResourceCache;

		Node objectNode = scene.CreateChild("Jack");
		objectNode.Position = (new Vector3(0.0f, 1.0f, 0.0f));

		// Create the rendering component + animation controller
		AnimatedModel obj = objectNode.CreateComponent<AnimatedModel>();
		obj.Model = cache.GetModel("Models/Jack.mdl");
		obj.SetMaterial(cache.GetMaterial("Materials/Jack.xml"));
		obj.CastShadows = true;
		objectNode.CreateComponent<AnimationController>();

		// Set the head bone for manual control
		//obj.Skeleton.GetBoneSafe("Bip01_Head").Animated = false;

		// Create rigidbody, and set non-zero mass so that the body becomes dynamic
		RigidBody body = objectNode.CreateComponent<RigidBody>();
		body.CollisionLayer = 1;
		body.Mass = 1.0f;

		// Set zero angular factor so that physics doesn't turn the character on its own.
		// Instead we will control the character yaw manually
		body.SetAngularFactor(Vector3.Zero);

		// Set the rigidbody to signal collision also when in rest, so that we get ground collisions properly
		body.CollisionEventMode = CollisionEventMode.Always;

		// Set a capsule shape for collision
		CollisionShape shape = objectNode.CreateComponent<CollisionShape>();
		shape.SetCapsule(0.7f, 1.8f, new Vector3(0.0f, 0.9f, 0.0f), Quaternion.Identity);

		// Create the character logic component, which takes care of steering the rigidbody
		// Remember it so that we can set the controls. Use a WeakPtr because the scene hierarchy already owns it
		// and keeps it alive as long as it's not removed from the hierarchy
		character = new Character(Context);
		objectNode.AddComponent(character);
		character.Start();
	}
}
