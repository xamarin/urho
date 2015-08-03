using Urho;

class _31_MaterialAnimation : Sample
{
    private Scene scene;
    private bool drawDebug;
    private Camera camera;

    public _31_MaterialAnimation(Context ctx) : base(ctx) { }

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

        // Create the Octree component to the scene. This is required before adding any drawable components, or else nothing will
        // show up. The default octree volume will be from (-1000, -1000, -1000) to (1000, 1000, 1000) in world coordinates; it
        // is also legal to place objects outside the volume but their visibility can then not be checked in a hierarchically
        // optimizing manner
        scene.CreateComponent<Octree>();

        // Create a child scene node (at world origin) and a StaticModel component into it. Set the StaticModel to show a simple
        // plane mesh with a "stone" material. Note that naming the scene nodes is optional. Scale the scene node larger
        // (100 x 100 world units)
        Node planeNode = scene.CreateChild("Plane");
        planeNode.Scale=new Vector3(100.0f, 1.0f, 100.0f);
        StaticModel planeObject = planeNode.CreateComponent<StaticModel>();
        planeObject.Model = (cache.GetModel("Models/Plane.mdl"));
        planeObject.SetMaterial(cache.GetMaterial("Materials/StoneTiled.xml"));

        // Create a directional light to the world so that we can see something. The light scene node's orientation controls the
        // light direction; we will use the SetDirection() function which calculates the orientation from a forward direction vector.
        // The light will use default settings (white light, no shadows)
        Node lightNode = scene.CreateChild("DirectionalLight");
        lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f)); // The direction vector does not need to be normalized
        Light light = lightNode.CreateComponent<Light>();
        light.LightType = LightType.LIGHT_DIRECTIONAL;

        // Create more StaticModel objects to the scene, randomly positioned, rotated and scaled. For rotation, we construct a
        // quaternion from Euler angles where the Y angle (rotation about the Y axis) is randomized. The mushroom model contains
        // LOD levels, so the StaticModel component will automatically select the LOD level according to the view distance (you'll
        // see the model get simpler as it moves further away). Finally, rendering a large number of the same object with the
        // same material allows instancing to be used, if the GPU supports it. This reduces the amount of CPU work in rendering the
        // scene.
        Material mushroomMat = cache.GetMaterial("Materials/Mushroom.xml");
        // Apply shader parameter animation to material
        ValueAnimation specColorAnimation=new ValueAnimation(Context);

#warning MISSING_API SetKeyFrame
        ////specColorAnimation.SetKeyFrame(0.0f, new Color(0.1f, 0.1f, 0.1f, 16.0f));
        ////specColorAnimation.SetKeyFrame(1.0f, new Color(1.0f, 0.0f, 0.0f, 2.0f));
        ////specColorAnimation.SetKeyFrame(2.0f, new Color(1.0f, 1.0f, 0.0f, 2.0f));
        ////specColorAnimation.SetKeyFrame(3.0f, new Color(0.1f, 0.1f, 0.1f, 16.0f));
        // Optionally associate material with scene to make sure shader parameter animation respects scene time scale
        mushroomMat.Scene=scene;
        mushroomMat.SetShaderParameterAnimation("MatSpecColor", specColorAnimation, WrapMode.WM_LOOP, 1.0f);

        const uint NUM_OBJECTS = 200;
        for (uint i = 0; i < NUM_OBJECTS; ++i)
        {
            Node mushroomNode = scene.CreateChild("Mushroom");
            mushroomNode.Position = (new Vector3(NextRandom(90.0f) - 45.0f, 0.0f, NextRandom(90.0f) - 45.0f));
            mushroomNode.Rotation=new Quaternion(0.0f, NextRandom(360.0f), 0.0f);
            mushroomNode.SetScale(0.5f + NextRandom(2.0f));
            StaticModel mushroomObject = mushroomNode.CreateComponent<StaticModel>();
            mushroomObject.Model = (cache.GetModel("Models/Mushroom.mdl"));
            mushroomObject.SetMaterial(mushroomMat);
        }

        // Create a scene node for the camera, which we will move around
        // The camera will use default settings (1000 far clip distance, 45 degrees FOV, set aspect ratio automatically)
        CameraNode = scene.CreateChild("Camera");
        CameraNode.CreateComponent<Camera>();

        // Set an initial position for the camera scene node above the plane
        CameraNode.Position = (new Vector3(0.0f, 5.0f, 0.0f));

    }
}
