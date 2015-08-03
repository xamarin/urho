using Urho;

class _21_AngelScriptIntegration : Sample
{
    private Scene scene;
    private bool drawDebug;
    private Camera camera;

    public _21_AngelScriptIntegration(Context ctx) : base(ctx) { }

    public override void Start()
    {
        base.Start();
        CreateScene();
        SimpleCreateInstructionsWithWASD();
        SetupViewport();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        SubscribeToUpdate(args =>
        {
            SimpleMoveCamera3D(args.TimeStep);
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

        // Create the Octree component to the scene so that drawable objects can be rendered. Use default volume
        // (-1000, -1000, -1000) to (1000, 1000, 1000)
        scene.CreateComponent<Octree>();

        // Create a Zone component into a child scene node. The Zone controls ambient lighting and fog settings. Like the Octree,
        // it also defines its volume with a bounding box, but can be rotated (so it does not need to be aligned to the world X, Y
        // and Z axes.) Drawable objects "pick up" the zone they belong to and use it when rendering; several zones can exist
        Node zoneNode = scene.CreateChild("Zone");
        Zone zone = zoneNode.CreateComponent<Zone>();
        // Set same volume as the Octree, set a close bluish fog and some ambient light
        zone.SetBoundingBox(new BoundingBox(-1000.0f, 1000.0f));
        zone.AmbientColor = new Color(0.05f, 0.1f, 0.15f);
        zone.FogColor = new Color(0.1f, 0.2f, 0.3f);
        zone.FogStart = 10.0f;
        zone.FogEnd = 100.0f;

        // Create randomly positioned and oriented box StaticModels in the scene
        const uint NUM_OBJECTS = 2000;
        for (uint i = 0; i < NUM_OBJECTS; ++i)
        {
            Node boxNode = scene.CreateChild("Box");
            boxNode.Position = new Vector3(NextRandom(200.0f) - 100.0f, NextRandom(200.0f) - 100.0f, NextRandom(200.0f) - 100.0f);
            // Orient using random pitch, yaw and roll Euler angles
            boxNode.Rotation = new Quaternion(NextRandom(360.0f), NextRandom(360.0f), NextRandom(360.0f));
            StaticModel boxObject = boxNode.CreateComponent<StaticModel>();
            boxObject.Model = cache.GetModel("Models/Box.mdl");
            boxObject.SetMaterial(cache.GetMaterial("Materials/Stone.xml"));

            // Add our custom Rotator script object (using the ScriptInstance C++ component to instantiate / store it) which will
            // rotate the scene node each frame, when the scene sends its update event

#warning MISSING_API ScriptInstance 
            ////ScriptInstance instance = boxNode.CreateComponent<ScriptInstance>();
            ////instance.CreateObject(cache.GetScriptFile("Scripts/Rotator.as"), "Rotator");
            ////// Call the script object's "SetRotationSpeed" function. Function arguments need to be passed in a VariantVector
            ////VariantVector parameters;
            ////parameters.Push(new Vector3(10.0f, 20.0f, 30.0f));
            ////instance.Execute("void SetRotationSpeed(const Vector3&in)", parameters);
        }

        // Create the camera. Let the starting position be at the world origin. As the fog limits maximum visible distance, we can
        // bring the far clip plane closer for more effective culling of distant objects
        CameraNode = scene.CreateChild("Camera");
        Camera camera = CameraNode.CreateComponent<Camera>();
        camera.FarClip = 100.0f;

        // Create a point light to the camera scene node
        Light light = CameraNode.CreateComponent<Light>();
        light.LightType = LightType.LIGHT_POINT;
        light.Range = 30.0f;
    }

}
