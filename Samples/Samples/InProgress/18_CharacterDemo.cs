using System;
using Urho;

class _18_CharacterDemo : Sample
{
    private Scene scene;
    private bool drawDebug;
    private Camera camera;

    const float CAMERA_MIN_DIST = 1.0f;
    const float CAMERA_INITIAL_DIST = 5.0f;
    const float CAMERA_MAX_DIST = 20.0f;

    const float GYROSCOPE_THRESHOLD = 0.1f;

    const int CTRL_FORWARD = 1;
    const int CTRL_BACK = 2;
    const int CTRL_LEFT = 4;
    const int CTRL_RIGHT = 8;
    const int CTRL_JUMP = 16;

    const float MOVE_FORCE = 0.8f;
    const float INAIR_MOVE_FORCE = 0.02f;
    const float BRAKE_FORCE = 0.2f;
    const float JUMP_FORCE = 7.0f;
    const float YAW_SENSITIVITY = 0.1f;
    const float INAIR_THRESHOLD_TIME = 0.1f;

    /// Touch utility object.
    Touch touch_;
    /// The controllable character component.
    Character character_;
    /// First person camera flag.
    bool firstPerson_;


    public _18_CharacterDemo(Context ctx) : base(ctx) { }

    public override void Start()
    {
        base.Start();
        if (TouchEnabled)
            touch_ = new Touch(Context, TouchSensitivity);
        CreateScene();
        CreateCharacter();
        SimpleCreateInstructionsWithWASD("\nSpace to jump, F to toggle 1st/3rd person\nF5 to save scene, F7 to load");
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        SubscribeToUpdate(HandleUpdate);
        SubscribeToPostRenderUpdate(HandlePostUpdate);

#warning MISSING_API character_.controls
        // Unsubscribe the SceneUpdate event from base class as the camera node is being controlled in HandlePostUpdate() in this sample
        ////UnsubscribeFromEvent(E_SCENEUPDATE);
    }

    private void HandlePostUpdate(PostRenderUpdateEventArgs args)
    {
        if (character_ == null)
            return;

        Node characterNode = character_.Node;

        // Get camera lookat dir from character yaw + pitch
        Quaternion rot = characterNode.Rotation;
#warning MISSING_API character_.controls
        Quaternion dir = rot * Quaternion.FromAxisAngle(Vector3.UnitX, /*character_.controls_.pitch_*/ 0f);

        // Turn head to camera pitch, but limit to avoid unnatural animation
        Node headNode = characterNode.GetChild("Bip01_Head", true);
#warning MISSING_API character_.controls
        float limitPitch = 0f;////Clamp(character_.controls_.pitch_, -45.0f, 45.0f);
        Quaternion headDir = rot * Quaternion.FromAxisAngle(new Vector3(1.0f, 0.0f, 0.0f), limitPitch);
        // This could be expanded to look at an arbitrary target, now just look at a point in front
        Vector3 headWorldTarget = headNode.WorldPosition + headDir * new Vector3(0.0f, 0.0f, 1.0f);
        headNode.LookAt(headWorldTarget, new Vector3(0.0f, 1.0f, 0.0f), TransformSpace.TS_WORLD);
        // Correct head orientation because LookAt assumes Z = forward, but the bone has been authored differently (Y = forward)
        headNode.Rotate(new Quaternion(0.0f, 90.0f, 90.0f), TransformSpace.TS_LOCAL);

        if (firstPerson_)
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
#warning MISSING_API RaycastSingle
            ////float rayDistance = touch_ != null ? touch_.cameraDistance_ : CAMERA_INITIAL_DIST;
            ////PhysicsRaycastResult result;
            ////scene.GetComponent<PhysicsWorld>().RaycastSingle(result, Ray(aimPoint, rayDir), rayDistance, 2);
            ////if (result.body_)
            ////    rayDistance = Math.Min(rayDistance, result.distance_);
            ////rayDistance = Clamp(rayDistance, CAMERA_MIN_DIST, CAMERA_MAX_DIST);

            ////CameraNode.Position = aimPoint + rayDir * rayDistance;
            CameraNode.Rotation = dir;
        }

    }

    private void HandleUpdate(UpdateEventArgs args)
    {
        Input input = Input;

        if (character_ != null)
        {
#warning MISSING_API character_.controls, UpdateTouches
            ////// Clear previous controls
            ////character_.controls_.Set(CTRL_FORWARD | CTRL_BACK | CTRL_LEFT | CTRL_RIGHT | CTRL_JUMP, false);

            ////// Update controls using touch utility class
            ////if (touch_ != null)
            ////    touch_.UpdateTouches(character_.controls_);

            // Update controls using keys
            UI ui = UI;
            if (ui.FocusElement == null)
            {
#warning MISSING_API character_.controls_, Input::GetTouch
                ////if (touch_ == null || !touch_.useGyroscope_)
                ////{
                ////    character_.controls_.Set(CTRL_FORWARD, input.GetKeyDown(Keys.W));
                ////    character_.controls_.Set(CTRL_BACK, input.GetKeyDown(Key.S));
                ////    character_.controls_.Set(CTRL_LEFT, input.GetKeyDown(Key.A));
                ////    character_.controls_.Set(CTRL_RIGHT, input.GetKeyDown(Key.D));
                ////}
                ////character_.controls_.Set(CTRL_JUMP, input.GetKeyDown(KEY_SPACE));

                ////// Add character yaw & pitch from the mouse motion or touch input
                ////if (TouchEnabled)
                ////{
                ////    for (uint i = 0; i < input.GetNumTouches(); ++i)
                ////    {
                ////        TouchState* state = input.GetTouch(i);
                ////        if (!state.touchedElement_)    // Touch on empty space
                ////        {
                ////            Camera camera = CameraNode.GetComponent<Camera>();
                ////            if (camera == null)
                ////                return;

                ////            var graphics = Graphics;
                ////            character_.controls_.yaw_ += TouchSensitivity * camera.Fov / graphics.Height * state.delta_.X;
                ////            character_.controls_.pitch_ += TouchSensitivity * camera.Fov / graphics.Height * state.delta_.Y;
                ////        }
                ////    }
                ////}
                ////else
                ////{
                ////    character_.controls_.yaw_ += (float)input.MouseMoveX * YAW_SENSITIVITY;
                ////    character_.controls_.pitch_ += (float)input.MouseMoveY * YAW_SENSITIVITY;
                ////}
                ////// Limit pitch
                ////character_.controls_.pitch_ = Clamp(character_.controls_.pitch_, -80.0f, 80.0f);

                // Switch between 1st and 3rd person
                if (input.GetKeyPress(Key.F))
                    firstPerson_ = !firstPerson_;

                // Turn on/off gyroscope on mobile platform
                if (touch_ != null && input.GetKeyPress(Key.G))
                    touch_.useGyroscope_ = !touch_.useGyroscope_;

                // Check for loading / saving the scene
                if (input.GetKeyPress(Key.F5))
                {
                    File saveFile = new File(Context, FileSystem.ProgramDir + "Data/Scenes/CharacterDemo.xml", FileMode.FILE_WRITE);
#warning MISSING_API Scene::SaveXML
                    ////scene.SaveXML(saveFile);
                }
                if (input.GetKeyPress(Key.F7))
                {
                    File loadFile = new File(Context, FileSystem.ProgramDir + "Data/Scenes/CharacterDemo.xml", FileMode.FILE_READ);
#warning MISSING_API Scene::LoadXML
                    ////scene.LoadXML(loadFile);
                    // After loading we have to reacquire the weak pointer to the Character component, as it has been recreated
                    // Simply find the character's scene node by name as there's only one of them
                    Node characterNode = scene.GetChild("Jack", true);
                    if (characterNode != null)
                        character_ = characterNode.GetComponent<Character>();
                }
            }

#warning MISSING_API character_.controls_
            // Set rotation already here so that it's updated every rendering frame instead of every physics frame
            ////character_.Node.Rotation = Quaternion.FromAxisAngle(Vector3.UnitY, character_.controls_.yaw_);
        }

    }

    private void CreateScene()
    {
        var cache = ResourceCache;

        scene = new Scene(Context);

        // Create scene subsystem components
        scene.CreateComponent<Octree>();
#warning MISSING_API PhysicsWorld
        ////scene.CreateComponent<PhysicsWorld>();

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

#warning MISSING_API AnimatedModel::GetSkeleton()
        // Set the head bone for manual control
        ////obj.GetSkeleton().GetBone("Bip01_Head").animated_ = false;

        // Create rigidbody, and set non-zero mass so that the body becomes dynamic
        RigidBody body = objectNode.CreateComponent<RigidBody>();
        body.CollisionLayer = 1;
        body.Mass = 1.0f;

        // Set zero angular factor so that physics doesn't turn the character on its own.
        // Instead we will control the character yaw manually
        body.SetAngularFactor(Vector3.Zero);

        // Set the rigidbody to signal collision also when in rest, so that we get ground collisions properly
        body.CollisionEventMode = CollisionEventMode.COLLISION_ALWAYS;

        // Set a capsule shape for collision
        CollisionShape shape = objectNode.CreateComponent<CollisionShape>();
        shape.SetCapsule(0.7f, 1.8f, new Vector3(0.0f, 0.9f, 0.0f), Quaternion.Identity);

        // Create the character logic component, which takes care of steering the rigidbody
        // Remember it so that we can set the controls. Use a WeakPtr because the scene hierarchy already owns it
        // and keeps it alive as long as it's not removed from the hierarchy
        character_ = objectNode.CreateComponent<Character>();
    }


    public class Character : LogicComponent
    {
        /// Movement controls. Assigned by the main program each frame.
#warning MISSING_API
        ////Controls controls_;
        /// Grounded flag for movement.
        bool onGround_;
        /// Jump flag.
        bool okToJump_;
        /// In air timer. Due to possible physics inaccuracy, character can be off ground for max. 1/10 second and still be allowed to move.
        float inAirTimer_;


        public Character(Context context) : base(context)
        {
            okToJump_ = true;
            UpdateEventMask = USE_FIXEDUPDATE;
        }


        private void RegisterObject(Context context)
        {
#warning MISSING_API
            ////// These macros register the class attributes to the Context for automatic load / save handling.
            ////// We specify the Default attribute mode which means it will be used both for saving into file, and network replication
            ////ATTRIBUTE("Controls Yaw", float, controls_.yaw_, 0.0f, AM_DEFAULT);
            ////ATTRIBUTE("Controls Pitch", float, controls_.pitch_, 0.0f, AM_DEFAULT);
            ////ATTRIBUTE("On Ground", bool, onGround_, false, AM_DEFAULT);
            ////ATTRIBUTE("OK To Jump", bool, okToJump_, true, AM_DEFAULT);
            ////ATTRIBUTE("In Air Timer", float, inAirTimer_, 0.0f, AM_DEFAULT);
        }

        private void Start()
        {
            // Component has been inserted into its scene node. Subscribe to events now
            SubscribeToNodeCollision(HandleNodeCollision);
        }

        private void FixedUpdate(float timeStep)
        {
            /// \todo Could cache the components for faster access instead of finding them each frame
#warning MISSING_API
            ////RigidBody body = GetComponent<RigidBody>();
            ////AnimationController animCtrl = GetComponent<AnimationController>();

            ////// Update the in air timer. Reset if grounded
            ////if (!onGround_)
            ////    inAirTimer_ += timeStep;
            ////else
            ////    inAirTimer_ = 0.0f;
            ////// When character has been in air less than 1/10 second, it's still interpreted as being on ground
            ////bool softGrounded = inAirTimer_ < INAIR_THRESHOLD_TIME;

            ////// Update movement & animation
            ////var rot = Node.Rotation;
            ////Vector3 moveDir = Vector3.Zero;
            ////var velocity = body.GetLinearVelocity();
            ////// Velocity on the XZ plane
            ////Vector3 planeVelocity(velocity.X, 0.0f, velocity.Z);

            ////if (controls_.IsDown(CTRL_FORWARD))
            ////    moveDir += Vector3.UnitZ;
            ////if (controls_.IsDown(CTRL_BACK))
            ////    moveDir += Vector3::BACK;
            ////if (controls_.IsDown(CTRL_LEFT))
            ////    moveDir += Vector3::LEFT;
            ////if (controls_.IsDown(CTRL_RIGHT))
            ////    moveDir += Vector3.UnitX;

            ////// Normalize move vector so that diagonal strafing is not faster
            ////if (moveDir.LengthSquared > 0.0f)
            ////    moveDir.Normalize();

            ////// If in air, allow control, but slower than when on ground
            ////body.ApplyImpulse(rot * moveDir * (softGrounded ? MOVE_FORCE : INAIR_MOVE_FORCE));

            ////if (softGrounded)
            ////{
            ////    // When on ground, apply a braking force to limit maximum ground velocity
            ////    Vector3 brakeForce = -planeVelocity * BRAKE_FORCE;
            ////    body.ApplyImpulse(brakeForce);

            ////    // Jump. Must release jump control inbetween jumps
            ////    if (controls_.IsDown(CTRL_JUMP))
            ////    {
            ////        if (okToJump_)
            ////        {
            ////            body.ApplyImpulse(Vector3.UnitY * JUMP_FORCE);
            ////            okToJump_ = false;
            ////        }
            ////    }
            ////    else
            ////        okToJump_ = true;
            ////}

            ////// Play walk animation if moving on ground, otherwise fade it out
            ////if (softGrounded && !moveDir.Equals(Vector3.Zero))
            ////    animCtrl.PlayExclusive("Models/Jack_Walk.ani", 0, true, 0.2f);
            ////else
            ////    animCtrl.Stop("Models/Jack_Walk.ani", 0.2f);
            ////// Set walk animation speed proportional to velocity
            ////animCtrl.SetSpeed("Models/Jack_Walk.ani", planeVelocity.Length() * 0.3f);

            ////// Reset grounded flag for next frame
            ////onGround_ = false;
        }

        private void HandleNodeCollision(NodeCollisionEventArgs args)
        {
            foreach (var contact in args.Contacts)
            {
                // If contact is below node center and mostly vertical, assume it's a ground contact
                if (contact.ContactPosition.Y < (Node.Position.Y + 1.0f))
                {
                    float level = Math.Abs(contact.ContactNormal.Y);
                    if (level > 0.75)
                        onGround_ = true;
                }
            }
        }

    }


    public class Touch : Component 
    {
        /// Touch sensitivity.
        float touchSensitivity_;
        /// Current camera zoom distance.
        float cameraDistance_;
        /// Zoom flag.
        bool zoom_;
        /// Gyroscope on/off flag.
        public bool useGyroscope_;

        public Touch(Context ctx, float touchSensitivity) : base(ctx)
        {
            touchSensitivity_ = touchSensitivity;
            cameraDistance_ = CAMERA_INITIAL_DIST;
            zoom_ = false;
            useGyroscope_ = false;
        }

#warning MISSING_API Controls, Input::GetTouch, JoytsikcState,
        /*private void UpdateTouches(Controls controls) // Called from HandleUpdate
        {
            zoom_ = false; // reset bool

            Input input = Input;

            // Zoom in/out
            if (input.NumTouches == 2)
            {
                TouchState* touch1 = input.GetTouch(0);
                TouchState* touch2 = input.GetTouch(1);

                // Check for zoom pattern (touches moving in opposite directions and on empty space)
                if (!touch1.touchedElement_ && !touch2.touchedElement_ && ((touch1.delta_.Y > 0 && touch2.delta_.Y < 0) || (touch1.delta_.Y < 0 && touch2.delta_.Y > 0)))
                    zoom_ = true;
                else
                    zoom_ = false;

                if (zoom_)
                {
                    int sens = 0;
                    // Check for zoom direction (in/out)
                    if (Math.Abs(touch1.position_.Y - touch2.position_.Y) > Math.Abs(touch1.lastPosition_.Y - touch2.lastPosition_.Y))
                        sens = -1;
                    else
                        sens = 1;
                    cameraDistance_ += Math.Abs(touch1.delta_.Y - touch2.delta_.Y) * sens * touchSensitivity_ / 50.0f;
                    cameraDistance_ = Clamp(cameraDistance_, CAMERA_MIN_DIST, CAMERA_MAX_DIST); // Restrict zoom range to [1;20]
                }
            }

            // Gyroscope (emulated by SDL through a virtual joystick)
            if (useGyroscope_ && input.NumJoysticks > 0)  // numJoysticks = 1 on iOS & Android
            {
                JoystickState joystick = input.GetJoystickByIndex(0);
                if (joystick.GetNumAxes() >= 2)
                {
                    if (joystick.GetAxisPosition(0) < -GYROSCOPE_THRESHOLD)
                        controls.Set(CTRL_LEFT, true);
                    if (joystick.GetAxisPosition(0) > GYROSCOPE_THRESHOLD)
                        controls.Set(CTRL_RIGHT, true);
                    if (joystick.GetAxisPosition(1) < -GYROSCOPE_THRESHOLD)
                        controls.Set(CTRL_FORWARD, true);
                    if (joystick.GetAxisPosition(1) > GYROSCOPE_THRESHOLD)
                        controls.Set(CTRL_BACK, true);
                }
            }
        }*/
    }
}
