using Urho;

class _12_PhysicsStressTest : Sample
{
    private Scene scene;
    private bool drawDebug;

    public _12_PhysicsStressTest(Context ctx) : base(ctx) { }

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
                if (input.GetKeyPress(KEY_F5))
                {
                    File saveFile = new File(Context, FileSystem.ProgramDir + "Data/Scenes/PhysicsStressTest.xml", FileMode.FILE_WRITE);
#warning MISSING_API SaveXML
                    ////scene.SaveXML(saveFile);
                }
                if (input.GetKeyPress(KEY_F7))
                {
                    File loadFile = new File(Context, FileSystem.ProgramDir + "Data/Scenes/PhysicsStressTest.xml", FileMode.FILE_READ);
#warning MISSING_API LoadXML
                    ////scene.LoadXML(loadFile);
                }

                if (Input.GetKeyDown(' '))
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

    private void SpawnObject()
    {
        var cache = ResourceCache;

        // Create a smaller box at camera position
        Node boxNode = scene.CreateChild("SmallBox");
        boxNode.Position = CameraNode.Position;
        boxNode.Rotation=CameraNode.Rotation;
        boxNode.SetScale(0.25f);
        StaticModel boxObject = boxNode.CreateComponent<StaticModel>();
        boxObject.Model = (cache.GetModel("Models/Box.mdl"));
        boxObject.SetMaterial(cache.GetMaterial("Materials/StoneSmall.xml"));
        boxObject.CastShadows=true;

        // Create physics components, use a smaller mass also
#warning MISSING_API RigidBody, CollisionShape
        ////RigidBody body = boxNode.CreateComponent<RigidBody>();
        ////body.SetMass(0.25f);
        ////body.Friction = 0.75f;
        ////CollisionShape shape = boxNode.CreateComponent<CollisionShape>();
        ////shape.SetBox(Vector3.One);

        ////const float OBJECT_VELOCITY = 10.0f;

        ////// Set initial velocity for the RigidBody based on camera forward vector. Add also a slight up component
        ////// to overcome gravity better
        ////body.SetLinearVelocity(CameraNode.GetRotation() * Vector3(0.0f, 0.25f, 1.0f) * OBJECT_VELOCITY);
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
#warning MISSING_API PhysicsWorld is not derived from Component
        //scene.CreateComponent<PhysicsWorld>();
        scene.CreateComponent<DebugRenderer>();

        // Create a Zone component for ambient lighting & fog control
        Node zoneNode = scene.CreateChild("Zone");
        Zone zone = zoneNode.CreateComponent<Zone>();
        zone.SetBoundingBox(new BoundingBox(-1000.0f, 1000.0f));
        zone.AmbientColor=new Color(0.15f, 0.15f, 0.15f);
        zone.FogColor = new Color(0.5f, 0.5f, 0.7f);
        zone.FogStart = 100.0f;
        zone.FogEnd = 300.0f;

        // Create a directional light to the world. Enable cascaded shadows on it
        Node lightNode = scene.CreateChild("DirectionalLight");
        lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f));
        Light light = lightNode.CreateComponent<Light>();
        light.LightType = LightType.LIGHT_DIRECTIONAL;
        light.CastShadows=true;
        light.ShadowBias=new BiasParameters(0.00025f, 0.5f);
        // Set cascade splits at 10, 50 and 200 world units, fade shadows out at 80% of maximum shadow distance
        light.ShadowCascade=new CascadeParameters(10.0f, 50.0f, 200.0f, 0.0f, 0.8f);

        {
            // Create a floor object, 500 x 500 world units. Adjust position so that the ground is at zero Y
            Node floorNode = scene.CreateChild("Floor");
            floorNode.Position = new Vector3(0.0f, -0.5f, 0.0f);
            floorNode.Scale=new Vector3(500.0f, 1.0f, 500.0f);
            StaticModel floorObject = floorNode.CreateComponent<StaticModel>();
            floorObject.Model = cache.GetModel("Models/Box.mdl");
            floorObject.SetMaterial(cache.GetMaterial("Materials/StoneTiled.xml"));

            // Make the floor physical by adding RigidBody and CollisionShape components
            /*RigidBody* body = */
#warning MISSING_API RigidBody, CollisionShape
            ////floorNode.CreateComponent<RigidBody>();
            ////CollisionShape shape = floorNode.CreateComponent<CollisionShape>();
            ////shape.SetBox(Vector3.One);
        }

        {
            // Create static mushrooms with triangle mesh collision
            const uint NUM_MUSHROOMS = 50;
            for (uint i = 0; i < NUM_MUSHROOMS; ++i)
            {
                Node mushroomNode = scene.CreateChild("Mushroom");
                mushroomNode.Position = new Vector3(NextRandom(400.0f) - 200.0f, 0.0f, NextRandom(400.0f) - 200.0f);
                mushroomNode.Rotation=new Quaternion(0.0f, NextRandom(360.0f), 0.0f);
                mushroomNode.SetScale(5.0f + NextRandom(5.0f));
                StaticModel mushroomObject = mushroomNode.CreateComponent<StaticModel>();
                mushroomObject.Model = (cache.GetModel("Models/Mushroom.mdl"));
                mushroomObject.SetMaterial(cache.GetMaterial("Materials/Mushroom.xml"));
                mushroomObject.CastShadows=true;

                /*RigidBody* body = */
#warning MISSING_API RigidBody, CollisionShape
                ////mushroomNode.CreateComponent<RigidBody>();
                ////CollisionShape shape = mushroomNode.CreateComponent<CollisionShape>();
                ////// By default the highest LOD level will be used, the LOD level can be passed as an optional parameter
                ////shape.SetTriangleMesh(mushroomObject.GetModel());
            }
        }

        {
            // Create a large amount of falling physics objects
            const uint NUM_OBJECTS = 1000;
            for (uint i = 0; i < NUM_OBJECTS; ++i)
            {
                Node boxNode = scene.CreateChild("Box");
                boxNode.Position = new Vector3(0.0f, i * 2.0f + 100.0f, 0.0f);
                StaticModel boxObject = boxNode.CreateComponent<StaticModel>();
                boxObject.Model = cache.GetModel("Models/Box.mdl");
                boxObject.SetMaterial(cache.GetMaterial("Materials/StoneSmall.xml"));
                boxObject.CastShadows=true;

                // Give the RigidBody mass to make it movable and also adjust friction
#warning MISSING_API RigidBody, CollisionShape
                ////RigidBody body = boxNode.CreateComponent<RigidBody>();
                ////body.SetMass(1.0f);
                ////body.Friction = 1.0f;
                ////// Disable collision event signaling to reduce CPU load of the physics simulation
                ////body.SetCollisionEventMode(COLLISION_NEVER);
                ////CollisionShape shape = boxNode.CreateComponent<CollisionShape>();
                ////shape.SetBox(Vector3.One);
            }
        }

        // Create the camera. Limit far clip distance to match the fog. Note: now we actually create the camera node outside
        // the scene, because we want it to be unaffected by scene load / save
        CameraNode = new Node(Context);
        Camera camera = CameraNode.CreateComponent<Camera>();
        camera.FarClip=300.0f;

        // Set an initial position for the camera scene node above the floor
        CameraNode.Position = (new Vector3(0.0f, 3.0f, -20.0f));

    }
}
