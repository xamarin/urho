using System.Collections.Generic;
using Urho;

class _19_VehicleDemo : Sample
{
    private Scene scene;
    private bool drawDebug;
    private Camera camera;
    private Vehicle vehicle_;

    const float CAMERA_DISTANCE = 10.0f;

    public _19_VehicleDemo(Context ctx) : base(ctx)
    {
        Vehicle.RegisterObject(ctx);
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

                if (vehicle_ != null)
                {
                    UI ui = UI;

                    // Get movement controls and assign them to the vehicle component. If UI has a focused element, clear controls
                    if (ui.FocusElement == null)
                    {
#warning MISSING_API Controls, Input::GetTouch, Scene::LoadXML, Scene::SaveXML
                        ////        vehicle_.controls_.Set(CTRL_FORWARD, input.GetKeyDown('W'));
                        ////    vehicle_.controls_.Set(CTRL_BACK, input.GetKeyDown('S'));
                        ////    vehicle_.controls_.Set(CTRL_LEFT, input.GetKeyDown('A'));
                        ////    vehicle_.controls_.Set(CTRL_RIGHT, input.GetKeyDown('D'));

                        ////    // Add yaw & pitch from the mouse motion or touch input. Used only for the camera, does not affect motion
                        ////    if (TouchEnabled)
                        ////    {
                        ////        for (uint i = 0; i < input.GetNumTouches(); ++i)
                        ////        {
                        ////            TouchState* state = input.GetTouch(i);
                        ////            if (!state.touchedElement_)    // Touch on empty space
                        ////            {
                        ////                Camera camera = CameraNode.GetComponent<Camera>();
                        ////                if (!camera)
                        ////                    return;

                        ////                var graphics = Graphics;
                        ////                vehicle_.controls_.yaw_ += TOUCH_SENSITIVITY * camera.GetFov() / graphics.Height * state.delta_.X;
                        ////                vehicle_.controls_.pitch_ += TOUCH_SENSITIVITY * camera.GetFov() / graphics.Height * state.delta_.Y;
                        ////            }
                        ////        }
                        ////    }
                        ////    else
                        ////    {
                        ////        vehicle_.controls_.yaw_ += (float)input.GetMouseMoveX() * YAW_SENSITIVITY;
                        ////        vehicle_.controls_.pitch_ += (float)input.GetMouseMoveY() * YAW_SENSITIVITY;
                        ////    }
                        ////    // Limit pitch
                        ////    vehicle_.controls_.pitch_ = Clamp(vehicle_.controls_.pitch_, 0.0f, 80.0f);

                        ////    // Check for loading / saving the scene
                        ////    if (input.GetKeyPress(KEY_F5))
                        ////    {
                        ////        File saveFile = new File(Context, FileSystem.ProgramDir + "Data/Scenes/VehicleDemo.xml", FileMode.FILE_WRITE);
                        ////        scene.SaveXML(saveFile);
                        ////    }
                        ////    if (input.GetKeyPress(KEY_F7))
                        ////    {
                        ////        File loadFile = new File(Context, FileSystem.ProgramDir + "Data/Scenes/VehicleDemo.xml", FileMode.FILE_READ);
                        ////        scene.LoadXML(loadFile);
                        ////        // After loading we have to reacquire the weak pointer to the Vehicle component, as it has been recreated
                        ////        // Simply find the vehicle's scene node by name as there's only one of them
                        ////        Node vehicleNode = scene.GetChild("Vehicle", true);
                        ////        if (vehicleNode != null)
                        ////            vehicle_ = vehicleNode.GetComponent<Vehicle>();
                        ////    }
                        ////}
                        ////else
                        ////    vehicle_.controls_.Set(CTRL_FORWARD | CTRL_BACK | CTRL_LEFT | CTRL_RIGHT, false);
                    }
                }
            });

        SubscribeToPostRenderUpdate(args =>
            {
                if (vehicle_ == null)
                    return;

                Node vehicleNode = vehicle_.Node;

                // Physics update has completed. Position camera behind vehicle
#warning MISSING_API
                ////Quaternion dir = new Quaternion(vehicleNode.Rotation.YawAngle(), Vector3.UnitY);
                ////dir = dir * new Quaternion(vehicle_.controls_.yaw_, Vector3.UnitY);
                ////dir = dir * new Quaternion(vehicle_.controls_.pitch_, Vector3.UnitX);

                ////Vector3 cameraTargetPos = vehicleNode.Position - dir * new Vector3(0.0f, 0.0f, CAMERA_DISTANCE);
                Vector3 cameraStartPos = vehicleNode.Position;

                // Raycast camera against static objects (physics collision mask 2)
                // and move it closer to the vehicle if something in between
#warning MISSING_API
                ////Ray cameraRay = new Ray(cameraStartPos, cameraTargetPos -cameraStartPos);
                ////float cameraRayLength = (cameraTargetPos - cameraStartPos).Length;
                ////PhysicsRaycastResult result;
                ////scene.GetComponent<PhysicsWorld>().RaycastSingle(result, cameraRay, cameraRayLength, 2);
                ////if (result.body_)
                ////    cameraTargetPos = cameraStartPos + cameraRay.direction_ * (result.distance_ - 0.5f);

                ////CameraNode.Position = cameraTargetPos;
                ////CameraNode.Rotation = dir;
            });

#warning MISSING_API
        // Unsubscribe the SceneUpdate event from base class as the camera node is being controlled in HandlePostUpdate() in this sample
        ////UnsubscribeFromEvent(E_SCENEUPDATE);
    }

    private void CreateVehicle()
    {
        Node vehicleNode = scene.CreateChild("Vehicle");
        vehicleNode.Position = (new Vector3(0.0f, 5.0f, 0.0f));

        // Create the vehicle logic component
        vehicle_ = vehicleNode.CreateComponent<Vehicle>(Vehicle.TypeStatic);
        // Create the rendering and physics components
        vehicle_.Init();
    }


    private void CreateScene()
    {
        var cache = ResourceCache;

        scene = new Scene(Context);

        // Create scene subsystem components
        scene.CreateComponent<Octree>();
#warning MISSING_API
        ////scene.CreateComponent<PhysicsWorld>();

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

#warning MISSING_API
        ////RigidBody body = terrainNode.CreateComponent<RigidBody>();
        ////body.SetCollisionLayer(2); // Use layer bitmask 2 for static geometry
        ////CollisionShape shape = terrainNode.CreateComponent<CollisionShape>();
        ////shape.SetTerrain();

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

#warning MISSING_API
            ////RigidBody body = objectNode.CreateComponent<RigidBody>();
            ////body.SetCollisionLayer(2);
            ////CollisionShape shape = objectNode.CreateComponent<CollisionShape>();
            ////shape.SetTriangleMesh(sm.Model, 0);
        }
    }


/// <summary>
/// Vehicle component, responsible for physical movement according to controls.
/// </summary>
public class Vehicle : LogicComponent
    {
        const int CTRL_FORWARD = 1;
        const int CTRL_BACK = 2;
        const int CTRL_LEFT = 4;
        const int CTRL_RIGHT = 8;

        const float YAW_SENSITIVITY = 0.1f;
        const float ENGINE_POWER = 10.0f;
        const float DOWN_FORCE = 10.0f;
        const float MAX_WHEEL_ANGLE = 22.5f;

        /// Movement controls.
#warning MISSING_API Controls
        ////Controls controls_;

        // Wheel scene nodes.
        Node frontLeft_;
        Node frontRight_;
        Node rearLeft_;
        Node rearRight_;

        // Steering axle constraints.
#warning MISSING_API Constraint
        ////Constraint frontLeftAxis_;
        ////Constraint frontRightAxis_;

        // Hull and wheel rigid bodies.
        RigidBody hullBody_;
        RigidBody frontLeftBody_;
        RigidBody frontRightBody_;
        RigidBody rearLeftBody_;
        RigidBody rearRightBody_;

        // IDs of the wheel scene nodes for serialization.
        uint frontLeftID_;
        uint frontRightID_;
        uint rearLeftID_;
        uint rearRightID_;

        /// Current left/right steering amount (-1 to 1.)
        float steering_;

        public Vehicle(Context context) : base(context)
        {
            UpdateEventMask = USE_FIXEDUPDATE;
        }

        private void RegisterObject(Context context)
        {
#warning MISSING_API RegisterFactory<> generic
            ////context.RegisterFactory<Vehicle>();

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

            frontLeft_ = scene.GetNode(frontLeftID_);
            frontRight_ = scene.GetNode(frontRightID_);
            rearLeft_ = scene.GetNode(rearLeftID_);
            rearRight_ = scene.GetNode(rearRightID_);

#warning MISSING_API Controls, RigidBody
            ////hullBody_ = Node.GetComponent<RigidBody>();

            GetWheelComponents();
        }

        private void FixedUpdate(float timeStep)
        {
            float newSteering = 0.0f;
            float accelerator = 0.0f;

            // Read controls
#warning MISSING_API Controls, RigidBody
            ////if (controls_.buttons_ & CTRL_LEFT)
            ////    newSteering = -1.0f;
            ////if (controls_.buttons_ & CTRL_RIGHT)
            ////    newSteering = 1.0f;
            ////if (controls_.buttons_ & CTRL_FORWARD)
            ////    accelerator = 1.0f;
            ////if (controls_.buttons_ & CTRL_BACK)
            ////    accelerator = -0.5f;

            ////// When steering, wake up the wheel rigidbodies so that their orientation is updated
            ////if (newSteering != 0.0f)
            ////{
            ////    frontLeftBody_.Activate();
            ////    frontRightBody_.Activate();
            ////    steering_ = steering_ * 0.95f + newSteering * 0.05f;
            ////}
            ////else
            ////    steering_ = steering_ * 0.8f + newSteering * 0.2f;

            ////// Set front wheel angles
            ////Quaternion steeringRot(0, steering_ * MAX_WHEEL_ANGLE, 0);
            ////frontLeftAxis_.SetOtherAxis(steeringRot * Vector3::LEFT);
            ////frontRightAxis_.SetOtherAxis(steeringRot * Vector3.UnitX);

            ////Quaternion hullRot = hullBody_.GetRotation();
            ////if (accelerator != 0.0f)
            ////{
            ////    // Torques are applied in world space, so need to take the vehicle & wheel rotation into account
            ////    Vector3 torqueVec = Vector3(ENGINE_POWER * accelerator, 0.0f, 0.0f);

            ////    frontLeftBody_.ApplyTorque(hullRot * steeringRot * torqueVec);
            ////    frontRightBody_.ApplyTorque(hullRot * steeringRot * torqueVec);
            ////    rearLeftBody_.ApplyTorque(hullRot * torqueVec);
            ////    rearRightBody_.ApplyTorque(hullRot * torqueVec);
            ////}

            ////// Apply downforce proportional to velocity
            ////Vector3 localVelocity = hullRot.Inverse() * hullBody_.GetLinearVelocity();
            ////hullBody_.ApplyForce(hullRot * Vector3::DOWN * Abs(localVelocity.Z) * DOWN_FORCE);
        }

        public void Init()
        {
            // This function is called only from the main program when initially creating the vehicle, not on scene load

#warning RigidBody, CollisionShape
            ////var cache = ResourceCache;
            ////var node_ = Node;
            ////StaticModel hullObject = node_.CreateComponent<StaticModel>();
            ////hullBody_ = node_.CreateComponent<RigidBody>();
            ////CollisionShape hullShape = node_.CreateComponent<CollisionShape>();

            ////node_.Scale = new Vector3(1.5f, 1.0f, 3.0f);
            ////hullObject.Model = cache.GetModel("Models/Box.mdl");
            ////hullObject.SetMaterial(cache.GetMaterial("Materials/Stone.xml"));
            ////hullObject.CastShadows = true;
            ////hullShape.SetBox(Vector3.One);
            ////hullBody_.SetMass(4.0f);
            ////hullBody_.SetLinearDamping(0.2f); // Some air resistance
            ////hullBody_.SetAngularDamping(0.5f);
            ////hullBody_.SetCollisionLayer(1);

            InitWheel("FrontLeft", new Vector3(-0.6f, -0.4f, 0.3f), frontLeft_, out frontLeftID_);
            InitWheel("FrontRight", new Vector3(0.6f, -0.4f, 0.3f), frontRight_, out frontRightID_);
            InitWheel("RearLeft", new Vector3(-0.6f, -0.4f, -0.3f), rearLeft_, out rearLeftID_);
            InitWheel("RearRight", new Vector3(0.6f, -0.4f, -0.3f), rearRight_, out rearRightID_);

            GetWheelComponents();
        }

        private void InitWheel(string name, Vector3 offset, Node wheelNode, out uint wheelNodeID)
        {
            ////var cache = ResourceCache;

            // Note: do not parent the wheel to the hull scene node. Instead create it on the root level and let the physics
            // constraint keep it together
            wheelNode = Scene.CreateChild(name);
            wheelNode.Position = Node.LocalToWorld(offset);
            wheelNode.Rotation = Node.Rotation * (offset.X >= 0.0 ? new Quaternion(0.0f, 0.0f, -90.0f) : new Quaternion(0.0f, 0.0f, 90.0f));
            wheelNode.Scale = new Vector3(0.8f, 0.5f, 0.8f);
            // Remember the ID for serialization
            wheelNodeID = wheelNode.ID;
    
            StaticModel wheelObject = wheelNode.CreateComponent<StaticModel>();
#warning MISSING_API RigidBody, Constraint, CollisionShape
            ////RigidBody wheelBody = wheelNode.CreateComponent<RigidBody>();
            ////CollisionShape wheelShape = wheelNode.CreateComponent<CollisionShape>();
            ////Constraint wheelConstraint = wheelNode.CreateComponent<Constraint>();

            ////wheelObject.Model=(cache.GetModel("Models/Cylinder.mdl"));
            ////wheelObject.SetMaterial(cache.GetMaterial("Materials/Stone.xml"));
            ////wheelObject.CastShadows = true;
            ////wheelShape.SetSphere(1.0f);
            ////wheelBody.Friction=(1.0f);
            ////wheelBody.SetMass(1.0f);
            ////wheelBody.SetLinearDamping(0.2f); // Some air resistance
            ////wheelBody.SetAngularDamping(0.75f); // Could also use rolling friction
            ////wheelBody.SetCollisionLayer(1);
            ////wheelConstraint.SetConstraintType(CONSTRAINT_HINGE);
            ////wheelConstraint.SetOtherBody(GetComponent<RigidBody>()); // Connect to the hull body
            ////wheelConstraint.SetWorldPosition(wheelNode.Position); // Set constraint's both ends at wheel's location
            ////wheelConstraint.SetAxis(Vector3.UnitY); // Wheel rotates around its local Y-axis
            ////wheelConstraint.SetOtherAxis(offset.X >= 0.0 ? Vector3.UnitX : Vector3::LEFT); // Wheel's hull axis points either left or right
            ////wheelConstraint.SetLowLimit(new Vector2(-180.0f, 0.0f)); // Let the wheel rotate freely around the axis
            ////wheelConstraint.SetHighLimit(new Vector2(180.0f, 0.0f));
            ////wheelConstraint.SetDisableCollision(true); // Let the wheel intersect the vehicle hull
        }

        private void GetWheelComponents()
        {
#warning MISSING_API RigidBody, Constraint
            //frontLeftAxis_ = frontLeft_.GetComponent<Constraint>();
            //frontRightAxis_ = frontRight_.GetComponent<Constraint>();
            //frontLeftBody_ = frontLeft_.GetComponent<RigidBody>();
            //frontRightBody_ = frontRight_.GetComponent<RigidBody>();
            //rearLeftBody_ = rearLeft_.GetComponent<RigidBody>();
            //rearRightBody_ = rearRight_.GetComponent<RigidBody>();
        }

    }
}
