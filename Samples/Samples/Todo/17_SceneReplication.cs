using Urho;

class _17_SceneReplication : Sample
{
    private Scene scene;
    private bool drawDebug;
    private Camera camera;

    public _17_SceneReplication(Context ctx) : base(ctx) { }

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

    private void CreateScene()
    {
        var cache = ResourceCache;
        scene = new Scene(Context);

        // Create the camera. Limit far clip distance to match the fog
        CameraNode = scene.CreateChild("Camera");
        camera = CameraNode.CreateComponent<Camera>();
        camera.FarClip = 300.0f;
        // Set an initial position for the camera scene node above the plane
        CameraNode.Position = new Vector3(0.0f, 5.0f, 0.0f);
    }
}
