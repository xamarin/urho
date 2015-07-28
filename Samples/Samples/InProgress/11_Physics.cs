using Urho;

class _11_Physics : Sample
{
    private Scene scene;
    private bool drawDebug;
    private Camera camera;

    public _11_Physics(Context ctx) : base(ctx) { }

    public override void Start()
    {
        base.Start();
        CreateScene();
        SimpleCreateInstructionsWithWASD("\nSpace to toggle debug geometry");
        SetupViewport();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        SubscribeToUpdate(args =>
        {
            SimpleMoveCamera(args.TimeStep);
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
        zone.AmbientColor=new Color(0.15f, 0.15f, 0.15f);
        zone.FogColor=new Color(1.0f, 1.0f, 1.0f);
        zone.FogStart=300.0f;
        zone.FogEnd=500.0f;

        // Create a directional light to the world. Enable cascaded shadows on it
        Node lightNode = scene.CreateChild("DirectionalLight");
        lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f));
        Light light = lightNode.CreateComponent<Light>();
        light.LightType=LightType.LIGHT_DIRECTIONAL;
        light.CastShadows=true;
        light.ShadowBias=new BiasParameters(0.00025f, 0.5f);
        // Set cascade splits at 10, 50 and 200 world units, fade shadows out at 80% of maximum shadow distance
        light.ShadowCascade=new CascadeParameters(10.0f, 50.0f, 200.0f, 0.0f, 0.8f);

        // Create skybox. The Skybox component is used like StaticModel, but it will be always located at the camera, giving the
        // illusion of the box planes being far away. Use just the ordinary Box model and a suitable material, whose shader will
        // generate the necessary 3D texture coordinates for cube mapping
        Node skyNode = scene.CreateChild("Sky");
        skyNode.SetScale(500.0f); // The scale actually does not matter
        Skybox skybox = skyNode.CreateComponent<Skybox>();
        skybox.Model=cache.GetModel("Models/Box.mdl");
        skybox.SetMaterial(cache.GetMaterial("Materials/Skybox.xml"));

        {
            // Create a floor object, 1000 x 1000 world units. Adjust position so that the ground is at zero Y
            Node floorNode = scene.CreateChild("Floor");
            floorNode.Position=new Vector3(0.0f, -0.5f, 0.0f);
            floorNode.Scale=new Vector3(1000.0f, 1.0f, 1000.0f);
            StaticModel floorObject = floorNode.CreateComponent<StaticModel>();
            floorObject.Model=cache.GetModel("Models/Box.mdl");
            floorObject.SetMaterial(cache.GetMaterial("Materials/StoneTiled.xml"));

            // Make the floor physical by adding RigidBody and CollisionShape components. The RigidBody's default
            // parameters make the object static (zero mass.) Note that a CollisionShape by itself will not participate
            // in the physics simulation
            /*RigidBody* body = */

#warning MISSING_API
            ////floorNode.CreateComponent<RigidBody>(); 
            ////CollisionShape shape = floorNode.CreateComponent<CollisionShape>();
            ////// Set a box shape of size 1 x 1 x 1 for collision. The shape will be scaled with the scene node scale, so the
            ////// rendering and physics representation sizes should match (the box model is also 1 x 1 x 1.)
            ////shape.Box(Vector3.One);
        }

        {
            // Create a pyramid of movable physics objects
            for (int y = 0; y < 8; ++y)
            {
                for (int x = -y; x <= y; ++x)
                {
                    Node boxNode = scene.CreateChild("Box");
                    boxNode.Position=new Vector3((float)x, -(float)y + 8.0f, 0.0f);
                    StaticModel boxObject = boxNode.CreateComponent<StaticModel>();
                    boxObject.Model=cache.GetModel("Models/Box.mdl");
                    boxObject.SetMaterial(cache.GetMaterial("Materials/StoneEnvMapSmall.xml"));
                    boxObject.CastShadows=true;

                    // Create RigidBody and CollisionShape components like above. Give the RigidBody mass to make it movable
                    // and also adjust friction. The actual mass is not important; only the mass ratios between colliding 
                    // objects are significant
#warning MISSING_API
                    ////RigidBody body = boxNode.CreateComponent<RigidBody>();
                    ////body.Mass(1.0f);
                    ////body.Friction(0.75f);
                    ////CollisionShape shape = boxNode.CreateComponent<CollisionShape>();
                    ////shape.Box(Vector3.One);
                }
            }
        }


        // Create the camera. Limit far clip distance to match the fog
        CameraNode = scene.CreateChild("Camera");
        camera = CameraNode.CreateComponent<Camera>();
        camera.FarClip = 500.0f;
        // Set an initial position for the camera scene node above the plane
        CameraNode.Position = new Vector3(0.0f, 5.0f, -20.0f);
    }
}
