using System;
using Urho;

class _19_VehicleDemo : Sample
{
	private Scene scene;
	private Vehicle vehicle;

	const float CAMERA_DISTANCE = 10.0f;

	public _19_VehicleDemo(Context ctx) : base(ctx)
	{
	}

	public override void Start()
	{
		base.Start();

		// Create static scene content
		CreateScene();

		// Create the controllable vehicle
		CreateVehicle();

		// Create the UI content
		SimpleCreateInstructionsWithWASD("\nF5 to save scene, F7 to load");

		// Subscribe to necessary events
		SubscribeToEvents();
	}

	private void SubscribeToEvents()
	{
		SubscribeToUpdate(args =>
			{
				Input input = Input;

				if (vehicle != null)
				{
					UI ui = UI;

					// Get movement controls and assign them to the vehicle component. If UI has a focused element, clear controls
					if (ui.FocusElement == null)
					{
						vehicle.Controls.Set(Vehicle.CTRL_FORWARD, input.GetKeyDown(Key.W));
						vehicle.Controls.Set(Vehicle.CTRL_BACK, input.GetKeyDown(Key.S));
						vehicle.Controls.Set(Vehicle.CTRL_LEFT, input.GetKeyDown(Key.A));
						vehicle.Controls.Set(Vehicle.CTRL_RIGHT, input.GetKeyDown(Key.D));

						// Add yaw & pitch from the mouse motion or touch input. Used only for the camera, does not affect motion
						if (TouchEnabled)
						{
							for (uint i = 0; i < input.NumTouches; ++i)
							{
								TouchState state;
								if (input.TryGetTouch(i, out state))
								{
									Camera camera = CameraNode.GetComponent<Camera>();
									if (camera == null)
										return;

									var graphics = Graphics;
									vehicle.Controls.Yaw += TouchSensitivity * camera.Fov / graphics.Height * state.Delta.X;
									vehicle.Controls.Pitch += TouchSensitivity * camera.Fov / graphics.Height * state.Delta.Y;
								}
							}
						}
						else
						{
							vehicle.Controls.Yaw += (float)input.MouseMoveX * Vehicle.YAW_SENSITIVITY;
							vehicle.Controls.Pitch += (float)input.MouseMoveY * Vehicle.YAW_SENSITIVITY;
						}
						// Limit pitch
						vehicle.Controls.Pitch = Clamp(vehicle.Controls.Pitch, 0.0f, 80.0f);

						// Check for loading / saving the scene
						if (input.GetKeyPress(Key.F5))
						{
							//File saveFile = new File(Context, FileSystem.ProgramDir + "Data/Scenes/VehicleDemo.xml", FileMode.Write);
							//scene.SaveXML(saveFile);
						}
						if (input.GetKeyPress(Key.F7))
						{
							//File loadFile = new File(Context, FileSystem.ProgramDir + "Data/Scenes/VehicleDemo.xml", FileMode.Read);
							//scene.LoadXML(loadFile);
							// After loading we have to reacquire the weak pointer to the Vehicle component, as it has been recreated
							// Simply find the vehicle's scene node by name as there's only one of them
							Node vehicleNode = scene.GetChild("Vehicle", true);
							if (vehicleNode != null)
								vehicle = vehicleNode.GetComponent<Vehicle>();
						}
					}
					else
						vehicle.Controls.Set(Vehicle.CTRL_FORWARD | Vehicle.CTRL_BACK | Vehicle.CTRL_LEFT | Vehicle.CTRL_RIGHT, false);
				}
			});

		SubscribeToPostRenderUpdate(args =>
			{
				if (vehicle == null)
					return;

				Node vehicleNode = vehicle.Node;

				// Physics update has completed. Position camera behind vehicle
				Quaternion dir = new Quaternion(Vector3.UnitY, vehicleNode.Rotation.YawAngle);
				dir = dir * new Quaternion(Vector3.UnitY, vehicle.Controls.Yaw);
				dir = dir * new Quaternion(Vector3.UnitX, vehicle.Controls.Pitch);

				Vector3 cameraTargetPos = vehicleNode.Position - dir * new Vector3(0.0f, 0.0f, CAMERA_DISTANCE);
				Vector3 cameraStartPos = vehicleNode.Position;

				// Raycast camera against static objects (physics collision mask 2)
				// and move it closer to the vehicle if something in between
#warning MISSING_API RaycastSingle
				////Ray cameraRay = new Ray(cameraStartPos, cameraTargetPos -cameraStartPos);
				////float cameraRayLength = (cameraTargetPos - cameraStartPos).Length;
				////PhysicsRaycastResult result;
				////scene.GetComponent<PhysicsWorld>().RaycastSingle(result, cameraRay, cameraRayLength, 2);
				////if (result.body_)
				////    cameraTargetPos = cameraStartPos + cameraRay.direction_ * (result.distance_ - 0.5f);

				////CameraNode.Position = cameraTargetPos;
				////CameraNode.Rotation = dir;
			});

		// Unsubscribe the SceneUpdate event from base class as the camera node is being controlled in HandlePostUpdate() in this sample
		SceneUpdateEventToken.Unsubscribe();
	}

	private void CreateVehicle()
	{
		Node vehicleNode = scene.CreateChild("Vehicle");
		vehicleNode.Position = (new Vector3(0.0f, 5.0f, 0.0f));

		// Create the vehicle logic component
		vehicle = new Vehicle(Context, ResourceCache); 
		vehicleNode.AddComponent(vehicle);
		// Create the rendering and physics components
		vehicle.Init();
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
		camera.FarClip = 500.0f;
		Renderer.SetViewport(0, new Viewport(Context, scene, camera, null));

		// Create static scene content. First create a zone for ambient lighting and fog control
		Node zoneNode = scene.CreateChild("Zone");
		Zone zone = zoneNode.CreateComponent<Zone>();
		zone.AmbientColor = new Color(0.15f, 0.15f, 0.15f);
		zone.FogColor = new Color(0.5f, 0.5f, 0.7f);
		zone.FogStart = 300.0f;
		zone.FogEnd = 500.0f;
		zone.SetBoundingBox(new BoundingBox(-2000.0f, 2000.0f));

		// Create a directional light with cascaded shadow mapping
		Node lightNode = scene.CreateChild("DirectionalLight");
		lightNode.SetDirection(new Vector3(0.3f, -0.5f, 0.425f));
		Light light = lightNode.CreateComponent<Light>();
		light.LightType = LightType.LIGHT_DIRECTIONAL;
		light.CastShadows = true;
		light.ShadowBias = new BiasParameters(0.00025f, 0.5f);
		light.ShadowCascade = new CascadeParameters(10.0f, 50.0f, 200.0f, 0.0f, 0.8f);
		light.SpecularIntensity = 0.5f;

		// Create heightmap terrain with collision
		Node terrainNode = scene.CreateChild("Terrain");
		terrainNode.Position = (Vector3.Zero);
		Terrain terrain = terrainNode.CreateComponent<Terrain>();
		terrain.PatchSize = 64;
		terrain.Spacing = new Vector3(2.0f, 0.1f, 2.0f); // Spacing between vertices and vertical resolution of the height map
		terrain.Smoothing = true;
		terrain.SetHeightMap(cache.GetImage("Textures/HeightMap.png"));
		terrain.Material = cache.GetMaterial("Materials/Terrain.xml");
		// The terrain consists of large triangles, which fits well for occlusion rendering, as a hill can occlude all
		// terrain patches and other objects behind it
		terrain.SetOccluder(true);

		RigidBody body = terrainNode.CreateComponent<RigidBody>();
		body.CollisionLayer = 2; // Use layer bitmask 2 for static geometry
		CollisionShape shape = terrainNode.CreateComponent<CollisionShape>();
		shape.SetTerrain(0);

		// Create 1000 mushrooms in the terrain. Always face outward along the terrain normal
		const uint NUM_MUSHROOMS = 1000;
		for (uint i = 0; i < NUM_MUSHROOMS; ++i)
		{
			Node objectNode = scene.CreateChild("Mushroom");
			Vector3 position = new Vector3(NextRandom(2000.0f) - 1000.0f, 0.0f, NextRandom(2000.0f) - 1000.0f);
			position.Y = terrain.GetHeight(position) - 0.1f;
			objectNode.Position = (position);
			// Create a rotation quaternion from up vector to terrain normal
			objectNode.Rotation = Quaternion.FromRotationTo(Vector3.UnitY, terrain.GetNormal(position));
			objectNode.SetScale(3.0f);
			StaticModel sm = objectNode.CreateComponent<StaticModel>();
			sm.Model = (cache.GetModel("Models/Mushroom.mdl"));
			sm.SetMaterial(cache.GetMaterial("Materials/Mushroom.xml"));
			sm.CastShadows = true;

			body = objectNode.CreateComponent<RigidBody>();
			body.CollisionLayer = 2;
			shape = objectNode.CreateComponent<CollisionShape>();
			shape.SetTriangleMesh(sm.Model, 0, Vector3.One, Vector3.Zero, Quaternion.Identity);
		}
	}


	/// <summary>
	/// Vehicle component, responsible for physical movement according to controls.
	/// </summary>
	public class Vehicle : Component
	{
		private readonly ResourceCache cache;
		public const int CTRL_FORWARD = 1;
		public const int CTRL_BACK = 2;
		public const int CTRL_LEFT = 4;
		public const int CTRL_RIGHT = 8;

		public const float YAW_SENSITIVITY = 0.1f;
		public const float ENGINE_POWER = 10.0f;
		public float DOWN_FORCE = 10.0f;
		public const float MAX_WHEEL_ANGLE = 22.5f;

		// Movement controls.
		public Controls Controls => new Controls();

		// Wheel scene nodes.
		Node frontLeft;
		Node frontRight;
		Node rearLeft;
		Node rearRight;

		// Steering axle constraints.
		Constraint frontLeftAxis;
		Constraint frontRightAxis;

		// Hull and wheel rigid bodies.
		RigidBody hullBody;
		RigidBody frontLeftBody;
		RigidBody frontRightBody;
		RigidBody rearLeftBody;
		RigidBody rearRightBody;

		// IDs of the wheel scene nodes for serialization.
		uint frontLeftId;
		uint frontRightId;
		uint rearLeftId;
		uint rearRightId;

		/// Current left/right steering amount (-1 to 1.)
		float steering;

		public Vehicle(Context context, ResourceCache cache) : base(context)
		{
			this.cache = cache;
			//UpdateEventMask = USE_FIXEDUPDATE;
		}

		private void RegisterObject(Context context)
		{

#warning MISSING_API RegisterAttribute
			////ATTRIBUTE("Controls Yaw", float, controls_.yaw_, 0.0f, AM_DEFAULT);
			////ATTRIBUTE("Controls Pitch", float, controls_.pitch_, 0.0f, AM_DEFAULT);
			////ATTRIBUTE("Steering", float, steering_, 0.0f, AM_DEFAULT);
			////// Register wheel node IDs as attributes so that the wheel nodes can be reaquired on deserialization. They need to be tagged
			////// as node ID's so that the deserialization code knows to rewrite the IDs in case they are different on load than on save
			////ATTRIBUTE("Front Left Node", int, frontLeftID_, 0, AM_DEFAULT | AM_NODEID);
			////ATTRIBUTE("Front Right Node", int, frontRightID_, 0, AM_DEFAULT | AM_NODEID);
			////ATTRIBUTE("Rear Left Node", int, rearLeftID_, 0, AM_DEFAULT | AM_NODEID);
			////ATTRIBUTE("Rear Right Node", int, rearRightID_, 0, AM_DEFAULT | AM_NODEID);
		}

		private void ApplyAttributes()
		{
			// This function is called on each Serializable after the whole scene has been loaded. Reacquire wheel nodes from ID's
			// as well as all required physics components
			Scene scene = Scene;

			frontLeft = scene.GetNode(frontLeftId);
			frontRight = scene.GetNode(frontRightId);
			rearLeft = scene.GetNode(rearLeftId);
			rearRight = scene.GetNode(rearRightId);
			hullBody = Node.GetComponent<RigidBody>();
			GetWheelComponents();
		}

		private void FixedUpdate(float timeStep)
		{
			float newSteering = 0.0f;
			float accelerator = 0.0f;

			// Read controls
			if ((Controls.Buttons & CTRL_LEFT) != 0)
				newSteering = -1.0f;
			if ((Controls.Buttons & CTRL_RIGHT) != 0)
				newSteering = 1.0f;
			if ((Controls.Buttons & CTRL_FORWARD) != 0)
				accelerator = 1.0f;
			if ((Controls.Buttons & CTRL_BACK) != 0)
				accelerator = -0.5f;

			// When steering, wake up the wheel rigidbodies so that their orientation is updated
			if (newSteering != 0.0f)
			{
				frontLeftBody.Activate();
				frontRightBody.Activate();
				steering = steering * 0.95f + newSteering * 0.05f;
			}
			else
				steering = steering * 0.8f + newSteering * 0.2f;

			// Set front wheel angles
			Quaternion steeringRot = new Quaternion(0, steering * MAX_WHEEL_ANGLE, 0);
			frontLeftAxis.SetOtherAxis(steeringRot * new Vector3(-1f, 0f, 0f));
			frontRightAxis.SetOtherAxis(steeringRot * Vector3.UnitX);

			Quaternion hullRot = hullBody.Rotation;
			if (accelerator != 0.0f)
			{
				// Torques are applied in world space, so need to take the vehicle & wheel rotation into account
				Vector3 torqueVec = new Vector3(ENGINE_POWER * accelerator, 0.0f, 0.0f);

				frontLeftBody.ApplyTorque(hullRot * steeringRot * torqueVec);
				frontRightBody.ApplyTorque(hullRot * steeringRot * torqueVec);
				rearLeftBody.ApplyTorque(hullRot * torqueVec);
				rearRightBody.ApplyTorque(hullRot * torqueVec);
			}

			// Apply downforce proportional to velocity
			Vector3 localVelocity = Quaternion.Invert(hullRot) * hullBody.LinearVelocity;
			hullBody.ApplyForce(hullRot * new Vector3(0f, -1f, 0f) * Math.Abs(localVelocity.Z) * DOWN_FORCE);
		}

		public void Init()
		{
			// This function is called only from the main program when initially creating the vehicle, not on scene load
			var node = Node;
			StaticModel hullObject = node.CreateComponent<StaticModel>();
			hullBody = node.CreateComponent<RigidBody>();
			CollisionShape hullShape = node.CreateComponent<CollisionShape>();

			node.Scale = new Vector3(1.5f, 1.0f, 3.0f);
			hullObject.Model = cache.GetModel("Models/Box.mdl");
			hullObject.SetMaterial(cache.GetMaterial("Materials/Stone.xml"));
			hullObject.CastShadows = true;
			hullShape.SetBox(Vector3.One, Vector3.Zero, Quaternion.Identity);
			hullBody.Mass = 4.0f;
			hullBody.LinearDamping = 0.2f; // Some air resistance
			hullBody.AngularDamping = 0.5f;
			hullBody.CollisionLayer = 1;

			InitWheel("FrontLeft", new Vector3(-0.6f, -0.4f, 0.3f), out frontLeft, out frontLeftId);
			InitWheel("FrontRight", new Vector3(0.6f, -0.4f, 0.3f), out frontRight, out frontRightId);
			InitWheel("RearLeft", new Vector3(-0.6f, -0.4f, -0.3f), out rearLeft, out rearLeftId);
			InitWheel("RearRight", new Vector3(0.6f, -0.4f, -0.3f), out rearRight, out rearRightId);

			GetWheelComponents();
		}

		private void InitWheel(string name, Vector3 offset, out Node wheelNode, out uint wheelNodeId)
		{
			// Note: do not parent the wheel to the hull scene node. Instead create it on the root level and let the physics
			// constraint keep it together
			wheelNode = Scene.CreateChild(name);
			wheelNode.Position = Node.LocalToWorld(offset);
			wheelNode.Rotation = Node.Rotation * (offset.X >= 0.0 ? new Quaternion(0.0f, 0.0f, -90.0f) : new Quaternion(0.0f, 0.0f, 90.0f));
			wheelNode.Scale = new Vector3(0.8f, 0.5f, 0.8f);
			// Remember the ID for serialization
			wheelNodeId = wheelNode.ID;
	
			StaticModel wheelObject = wheelNode.CreateComponent<StaticModel>();
			RigidBody wheelBody = wheelNode.CreateComponent<RigidBody>();
			CollisionShape wheelShape = wheelNode.CreateComponent<CollisionShape>();
			Constraint wheelConstraint = wheelNode.CreateComponent<Constraint>();

			wheelObject.Model = (cache.GetModel("Models/Cylinder.mdl"));
			wheelObject.SetMaterial(cache.GetMaterial("Materials/Stone.xml"));
			wheelObject.CastShadows = true;
			wheelShape.SetSphere(1.0f, Vector3.Zero, Quaternion.Identity);
			wheelBody.Friction = (1.0f);
			wheelBody.Mass = 1.0f;
			wheelBody.LinearDamping = 0.2f; // Some air resistance
			wheelBody.AngularDamping = 0.75f; // Could also use rolling friction
			wheelBody.CollisionLayer = 1;
			wheelConstraint.ConstraintType = ConstraintType.Hinge;
			wheelConstraint.OtherBody = GetComponent<RigidBody>(); // Connect to the hull body
			wheelConstraint.SetWorldPosition(wheelNode.Position); // Set constraint's both ends at wheel's location
			wheelConstraint.SetAxis(Vector3.UnitY); // Wheel rotates around its local Y-axis
			wheelConstraint.SetOtherAxis(offset.X >= 0.0 ? Vector3.UnitX : new Vector3(-1f, 0f, 0f)); // Wheel's hull axis points either left or right
			wheelConstraint.LowLimit = new Vector2(-180.0f, 0.0f); // Let the wheel rotate freely around the axis
			wheelConstraint.HighLimit = new Vector2(180.0f, 0.0f);
			wheelConstraint.DisableCollision = true; // Let the wheel intersect the vehicle hull
		}

		private void GetWheelComponents()
		{
			frontLeftAxis = frontLeft.GetComponent<Constraint>();
			frontRightAxis = frontRight.GetComponent<Constraint>();
			frontLeftBody = frontLeft.GetComponent<RigidBody>();
			frontRightBody = frontRight.GetComponent<RigidBody>();
			rearLeftBody = rearLeft.GetComponent<RigidBody>();
			rearRightBody = rearRight.GetComponent<RigidBody>();
		}
	}
}
