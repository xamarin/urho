    using Urho;

class _13_Ragdolls : Sample
{
    private Scene scene;
    private bool drawDebug;
    private Camera camera;

    public _13_Ragdolls(Context ctx) : base(ctx) { }

    public override void Start()
    {
        base.Start();
        CreateScene();
        SimpleCreateInstructionsWithWASD(     
            "\nLMB to spawn physics objects\n" +
            "F5 to save scene, F7 to load\n" +
            "Space to toggle physics debug geometry");
        SetupViewport();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        SubscribeToUpdate(args =>
        {
            SimpleMoveCamera(args.TimeStep);
            var input = Input;
                // "Shoot" a physics object with left mousebutton
            if (input.GetMouseButtonPress(MouseButton.Left))
                SpawnObject();

            // Check for loading / saving the scene
#warning MISSING_API
            /*if (input.GetKeyPress(KEY_F5))
            {
                File saveFile(Context, GetSubsystem<FileSystem>()->GetProgramDir() + "Data/Scenes/Ragdolls.xml", FILE_WRITE);
                scene.SaveXML(saveFile);
            }
            if (input.GetKeyPress(KEY_F7))
            {
                File loadFile(context_, GetSubsystem<FileSystem>()->GetProgramDir() + "Data/Scenes/Ragdolls.xml", FILE_READ);
                scene.LoadXML(loadFile);
            }*/

            if (Input.GetKeyDown(Key.Space))
                drawDebug = !drawDebug;
        });

        SubscribeToPostRenderUpdate(args =>
        {
            // If draw debug mode is enabled, draw viewport debug geometry, which will show eg. drawable bounding boxes and skeleton
            // bones. Note that debug geometry has to be separately requested each frame. Disable depth test so that we can see the
            // bones properly
            if (drawDebug)
                Renderer.DrawDebugGeometry(false);
        });
    }
    
    private void SetupViewport()
    {
        var renderer = Renderer;
        renderer.SetViewport(0, new Viewport(Context, scene, CameraNode.GetComponent<Camera>(), null));
    }

    private void CreateScene()
    {
        var cache = ResourceCache;
        scene = new Scene(Context);

        // Create octree, use default volume (-1000, -1000, -1000) to (1000, 1000, 1000)
        // Create a physics simulation world with default parameters, which will update at 60fps. Like the Octree must
        // exist before creating drawable components, the PhysicsWorld must exist before creating physics components.
        // Finally, create a DebugRenderer component so that we can draw physics debug geometry
        scene.CreateComponent<Octree>();
#warning MISSING_API
        ////scene.CreateComponent<PhysicsWorld>();
        scene.CreateComponent<DebugRenderer>();
    
        // Create a Zone component for ambient lighting & fog control
        Node zoneNode = scene.CreateChild("Zone");
        Zone zone = zoneNode.CreateComponent<Zone>();
        zone.SetBoundingBox(new BoundingBox(-1000.0f, 1000.0f));
        zone.AmbientColor=(new Color(0.15f, 0.15f, 0.15f));
        zone.FogColor=new Color(0.5f, 0.5f, 0.7f);
        zone.FogStart=100.0f;
        zone.FogEnd=300.0f;
    
        // Create a directional light to the world. Enable cascaded shadows on it
        Node lightNode = scene.CreateChild("DirectionalLight");
        lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f));
        Light light = lightNode.CreateComponent<Light>();
        light.LightType=LightType.LIGHT_DIRECTIONAL;
        light.CastShadows=true;
        light.ShadowBias=new BiasParameters(0.00025f, 0.5f);
        // Set cascade splits at 10, 50 and 200 world units, fade shadows out at 80% of maximum shadow distance
        light.ShadowCascade=new CascadeParameters(10.0f, 50.0f, 200.0f, 0.0f, 0.8f);
    
        {
            // Create a floor object, 500 x 500 world units. Adjust position so that the ground is at zero Y
            Node floorNode = scene.CreateChild("Floor");
            floorNode.Position=new Vector3(0.0f, -0.5f, 0.0f);
            floorNode.Scale=new Vector3(500.0f, 1.0f, 500.0f);
            StaticModel floorObject = floorNode.CreateComponent<StaticModel>();
            floorObject.Model=cache.GetModel("Models/Box.mdl");
            floorObject.SetMaterial(cache.GetMaterial("Materials/StoneTiled.xml"));

            // Make the floor physical by adding RigidBody and CollisionShape components
#warning MISSING_API
            ////RigidBody body = floorNode.CreateComponent<RigidBody>();
            ////// We will be spawning spherical objects in this sample. The ground also needs non-zero rolling friction so that
            ////// the spheres will eventually come to rest
            ////body.RollingFriction(0.15f);
            ////CollisionShape shape = floorNode.CreateComponent<CollisionShape>();
            ////// Set a box shape of size 1 x 1 x 1 for collision. The shape will be scaled with the scene node scale, so the
            ////// rendering and physics representation sizes should match (the box model is also 1 x 1 x 1.)
            ////shape.Box(Vector3.One);
        }
    
        // Create animated models
        for (int z = -1; z <= 1; ++z)
        {
            for (int x = -4; x <= 4; ++x)
            {
                Node modelNode = scene.CreateChild("Jack");
                modelNode.Position=new Vector3(x * 5.0f, 0.0f, z * 5.0f);
                modelNode.Rotation=new Quaternion(0.0f, 180.0f, 0.0f);
                AnimatedModel modelObject = modelNode.CreateComponent<AnimatedModel>();
                modelObject.Model=cache.GetModel("Models/Jack.mdl");
                modelObject.SetMaterial(cache.GetMaterial("Materials/Jack.xml"));
                modelObject.CastShadows=true;
                // Set the model to also update when invisible to avoid staying invisible when the model should come into
                // view, but does not as the bounding box is not updated
                modelObject.UpdateInvisible=true;
            
                // Create a rigid body and a collision shape. These will act as a trigger for transforming the
                // model into a ragdoll when hit by a moving object
#warning MISSING_API
                ////RigidBody body = modelNode.CreateComponent<RigidBody>();
                ////// The Trigger mode makes the rigid body only detect collisions, but impart no forces on the
                ////// colliding objects
                ////body.Trigger(true);
                ////CollisionShape shape = modelNode.CreateComponent<CollisionShape>();
                ////// Create the capsule shape with an offset so that it is correctly aligned with the model, which
                ////// has its origin at the feet
                ////shape.Capsule(0.7f, 2.0f, new Vector3(0.0f, 1.0f, 0.0f));
            
                // Create a custom component that reacts to collisions and creates the ragdoll
                modelNode.CreateComponent<CreateRagdoll>();
            }
        }
    



        // Create the camera. Limit far clip distance to match the fog
        CameraNode = scene.CreateChild("Camera");
        camera = CameraNode.CreateComponent<Camera>();
        camera.FarClip = 300.0f;
        // Set an initial position for the camera scene node above the plane
        CameraNode.Position = new Vector3(0.0f, 3.0f, -20.0f);
    }

    private void SpawnObject()
    {
        var cache = ResourceCache;
    
        Node boxNode = scene.CreateChild("Sphere");
        boxNode.Position=CameraNode.Position;
        boxNode.Rotation=CameraNode.Rotation;
        boxNode.SetScale(0.25f);
        StaticModel boxObject = boxNode.CreateComponent<StaticModel>();
        boxObject.Model=cache.GetModel("Models/Sphere.mdl");
        boxObject.SetMaterial(cache.GetMaterial("Materials/StoneSmall.xml"));
        boxObject.CastShadows=true;

#warning MISSING_API
        /*RigidBody body = boxNode.CreateComponent<RigidBody>();
        body.SetMass(1.0f);
        body.SetRollingFriction(0.15f);
        CollisionShape* shape = boxNode.CreateComponent<CollisionShape>();
        shape.SetSphere(1.0f);
    
        const float OBJECT_VELOCITY = 10.0f;
    
        // Set initial velocity for the RigidBody based on camera forward vector. Add also a slight up component
        // to overcome gravity better
        body.SetLinearVelocity(CameraNode.GetRotation() * Vector3(0.0f, 0.25f, 1.0f) * OBJECT_VELOCITY);*/
    }
}

class CreateRagdoll : Component
{
    public CreateRagdoll(Context context) : base(context) {}

    public void OnNodeSet(Node node)
    {
        // If the node pointer is non-null, this component has been created into a scene node. Subscribe to physics collisions that
        // concern this scene node
        //if (node != null)
            //SubscribeToEvent(node, E_NODECOLLISION, HANDLER(CreateRagdoll, HandleNodeCollision));
    }

#warning MISSING_API
/*void HandleNodeCollision(StringHash eventType, VariantMap& eventData)
{
    // Get the other colliding body, make sure it is moving (has nonzero mass)
    RigidBody otherBody = static_cast<RigidBody*>(eventData[P_OTHERBODY].GetPtr());

    if (otherBody.GetMass() > 0.0f)
    {
        // We do not need the physics components in the AnimatedModel's root scene node anymore
        node_.RemoveComponent<RigidBody>();
        node_.RemoveComponent<CollisionShape>();
        
        // Create RigidBody & CollisionShape components to bones
        CreateRagdollBone("Bip01_Pelvis", SHAPE_BOX, new Vector3(0.3f, 0.2f, 0.25f), new Vector3(0.0f, 0.0f, 0.0f),
            new Quaternion(0.0f, 0.0f, 0.0f));
        CreateRagdollBone("Bip01_Spine1", SHAPE_BOX, new Vector3(0.35f, 0.2f, 0.3f), new Vector3(0.15f, 0.0f, 0.0f),
            new Quaternion(0.0f, 0.0f, 0.0f));
        CreateRagdollBone("Bip01_L_Thigh", SHAPE_CAPSULE, new Vector3(0.175f, 0.45f, 0.175f), new Vector3(0.25f, 0.0f, 0.0f),
            new Quaternion(0.0f, 0.0f, 90.0f));
        CreateRagdollBone("Bip01_R_Thigh", SHAPE_CAPSULE, new Vector3(0.175f, 0.45f, 0.175f), new Vector3(0.25f, 0.0f, 0.0f),
            new Quaternion(0.0f, 0.0f, 90.0f));
        CreateRagdollBone("Bip01_L_Calf", SHAPE_CAPSULE, new Vector3(0.15f, 0.55f, 0.15f), new Vector3(0.25f, 0.0f, 0.0f),
            new Quaternion(0.0f, 0.0f, 90.0f));
        CreateRagdollBone("Bip01_R_Calf", SHAPE_CAPSULE, new Vector3(0.15f, 0.55f, 0.15f), new Vector3(0.25f, 0.0f, 0.0f),
            new Quaternion(0.0f, 0.0f, 90.0f));
        CreateRagdollBone("Bip01_Head", SHAPE_BOX, new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.1f, 0.0f, 0.0f),
            new Quaternion(0.0f, 0.0f, 0.0f));
        CreateRagdollBone("Bip01_L_UpperArm", SHAPE_CAPSULE, new Vector3(0.15f, 0.35f, 0.15f), new Vector3(0.1f, 0.0f, 0.0f),
            new Quaternion(0.0f, 0.0f, 90.0f));
        CreateRagdollBone("Bip01_R_UpperArm", SHAPE_CAPSULE, new Vector3(0.15f, 0.35f, 0.15f), new Vector3(0.1f, 0.0f, 0.0f),
            new Quaternion(0.0f, 0.0f, 90.0f));
        CreateRagdollBone("Bip01_L_Forearm", SHAPE_CAPSULE, new Vector3(0.125f, 0.4f, 0.125f), new Vector3(0.2f, 0.0f, 0.0f),
            new Quaternion(0.0f, 0.0f, 90.0f));
        CreateRagdollBone("Bip01_R_Forearm", SHAPE_CAPSULE, new Vector3(0.125f, 0.4f, 0.125f), new Vector3(0.2f, 0.0f, 0.0f),
            new Quaternion(0.0f, 0.0f, 90.0f));
        
        // Create Constraints between bones
        CreateRagdollConstraint("Bip01_L_Thigh", "Bip01_Pelvis", CONSTRAINT_CONETWIST, Vector3::BACK, Vector3::FORWARD,
            new Vector2(45.0f, 45.0f), new Vector2.Zero);
        CreateRagdollConstraint("Bip01_R_Thigh", "Bip01_Pelvis", CONSTRAINT_CONETWIST, Vector3::BACK, Vector3::FORWARD,
            new Vector2(45.0f, 45.0f), new Vector2.Zero);
        CreateRagdollConstraint("Bip01_L_Calf", "Bip01_L_Thigh", CONSTRAINT_HINGE, Vector3::BACK, Vector3::BACK,
            new Vector2(90.0f, 0.0f), new Vector2.Zero);
        CreateRagdollConstraint("Bip01_R_Calf", "Bip01_R_Thigh", CONSTRAINT_HINGE, Vector3::BACK, Vector3::BACK,
            new Vector2(90.0f, 0.0f), new Vector2.Zero);
        CreateRagdollConstraint("Bip01_Spine1", "Bip01_Pelvis", CONSTRAINT_HINGE, Vector3::FORWARD, Vector3::FORWARD,
            new Vector2(45.0f, 0.0f), new Vector2(-10.0f, 0.0f));
        CreateRagdollConstraint("Bip01_Head", "Bip01_Spine1", CONSTRAINT_CONETWIST, Vector3::LEFT, Vector3::LEFT,
            new Vector2(0.0f, 30.0f), new Vector2.Zero);
        CreateRagdollConstraint("Bip01_L_UpperArm", "Bip01_Spine1", CONSTRAINT_CONETWIST, Vector3::DOWN, Vector3::UP,
            new Vector2(45.0f, 45.0f), new Vector2.Zero, false);
        CreateRagdollConstraint("Bip01_R_UpperArm", "Bip01_Spine1", CONSTRAINT_CONETWIST, Vector3::DOWN, Vector3::UP,
            new Vector2(45.0f, 45.0f), new Vector2.Zero, false);
        CreateRagdollConstraint("Bip01_L_Forearm", "Bip01_L_UpperArm", CONSTRAINT_HINGE, Vector3::BACK, Vector3::BACK,
            new Vector2(90.0f, 0.0f), new Vector2.Zero);
        CreateRagdollConstraint("Bip01_R_Forearm", "Bip01_R_UpperArm", CONSTRAINT_HINGE, Vector3::BACK, Vector3::BACK,
            new Vector2(90.0f, 0.0f), new Vector2.Zero);
        
        // Disable keyframe animation from all bones so that they will not interfere with the ragdoll
        AnimatedModel model = GetComponent<AnimatedModel>();
        Skeleton skeleton = model.GetSkeleton();
        for (unsigned i = 0; i < skeleton.GetNumBones(); ++i)
            skeleton.GetBone(i).animated_ = false;
        
        // Finally remove self from the scene node. Note that this must be the last operation performed in the function
        Remove();
    }
}*/

#warning MISSING_API
/*void CreateRagdollBone(string boneName, ShapeType type, const Vector3& size, const Vector3& position,
    const Quaternion& rotation)
{
    // Find the correct child scene node recursively
    Node boneNode = node_.GetChild(boneName, true);
    if (!boneNode)
    {
        LOGWARNING("Could not find bone " + boneName + " for creating ragdoll physics components");
        return;
    }
    
    RigidBody* body = boneNode.CreateComponent<RigidBody>();
    // Set mass to make movable
    body.SetMass(1.0f);
    // Set damping parameters to smooth out the motion
    body.SetLinearDamping(0.05f);
    body.SetAngularDamping(0.85f);
    // Set rest thresholds to ensure the ragdoll rigid bodies come to rest to not consume CPU endlessly
    body.SetLinearRestThreshold(1.5f);
    body.SetAngularRestThreshold(2.5f);

    CollisionShape* shape = boneNode.CreateComponent<CollisionShape>();
    // We use either a box or a capsule shape for all of the bones
    if (type == SHAPE_BOX)
        shape.SetBox(size, position, rotation);
    else
        shape.SetCapsule(size.x_, size.y_, position, rotation);
}*/

#warning MISSING_API
/*void CreateRagdollConstraint(string boneName, string parentName, ConstraintType type,
    const Vector3& axis, const Vector3& parentAxis, const Vector2& highLimit, const Vector2& lowLimit,
    bool disableCollision)
{
    Node boneNode = node_.GetChild(boneName, true);
    Node parentNode = node_.GetChild(parentName, true);
    if (!boneNode)
    {
        LOGWARNING("Could not find bone " + boneName + " for creating ragdoll constraint");
        return;
    }
    if (!parentNode)
    {
        LOGWARNING("Could not find bone " + parentName + " for creating ragdoll constraint");
        return;
    }
    
    Constraint constraint = boneNode.CreateComponent<Constraint>();
    constraint.SetConstraintType(type);
    // Most of the constraints in the ragdoll will work better when the connected bodies don't collide against each other
    constraint.SetDisableCollision(disableCollision);
    // The connected body must be specified before setting the world position
    constraint.SetOtherBody(parentNode.GetComponent<RigidBody>());
    // Position the constraint at the child bone we are connecting
    constraint.SetWorldPosition(boneNode.GetWorldPosition());
    // Configure axes and limits
    constraint.SetAxis(axis);
    constraint.SetOtherAxis(parentAxis);
    constraint.SetHighLimit(highLimit);
    constraint.SetLowLimit(lowLimit);
}*/
}