using System;
using Urho;

class _25_Urho2DParticle : Sample
{
    private Scene scene;
    private Node particleNode;

    public _25_Urho2DParticle(Context ctx) : base(ctx) { }

    public override void Start()
    {
        base.Start();
        CreateScene();
        Input.SetMouseVisible(true, false);
        SimpleCreateInstructions("Use mouse/touch to move the particle.");
        SetupViewport();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        SubscribeToMouseMove(args => HandleMouseMove(args.X, args.Y));

        if (TouchEnabled)
            SubscribeToTouchMove(args => HandleMouseMove(args.X, args.Y));

        SceneUpdateEventToken.Unsubscribe();
    }

    private void HandleMouseMove(int x, int y)
    {
        if (particleNode != null)
        {
            var graphics = Graphics;
            Camera camera = CameraNode.GetComponent<Camera>();
            particleNode.Position=(camera.ScreenToWorldPoint(new Vector3((float)x / graphics.Width, (float)y / graphics.Height, 10.0f)));
        }
    }

    private void SetupViewport()
    {
        var renderer = Renderer;
        renderer.SetViewport(0, new Viewport(Context, scene, CameraNode.GetComponent<Camera>(), null));
    }

    private void CreateScene()
    {
        scene = new Scene(Context);
        scene.CreateComponent<Octree>();

        // Create camera node
        CameraNode = scene.CreateChild("Camera");
        // Set camera's position
        CameraNode.Position = (new Vector3(0.0f, 0.0f, -10.0f));

        Camera camera = CameraNode.CreateComponent<Camera>();
        camera.SetOrthographic(true);

        var graphics = Graphics;
        camera.OrthoSize=(float)graphics.Height * PIXEL_SIZE;
        camera.Zoom=1.2f * Math.Min((float)graphics.Width / 1280.0f, (float)graphics.Height / 800.0f); // Set zoom according to user's resolution to ensure full visibility (initial zoom (1.2) is set for full visibility at 1280x800 resolution)

        var cache = ResourceCache;
        ParticleEffect2D particleEffect = cache.GetParticleEffect2D("Urho2D/sun.pex");
        if (particleEffect == null)
            return;

        particleNode = scene.CreateChild("ParticleEmitter2D");
        ParticleEmitter2D particleEmitter = particleNode.CreateComponent<ParticleEmitter2D>();
        particleEmitter.Effect=particleEffect;

        ParticleEffect2D greenSpiralEffect = cache.GetParticleEffect2D("Urho2D/greenspiral.pex");
        if (greenSpiralEffect == null)
            return;

        Node greenSpiralNode = scene.CreateChild("GreenSpiral");
        ParticleEmitter2D greenSpiralEmitter = greenSpiralNode.CreateComponent<ParticleEmitter2D>();
        greenSpiralEmitter.Effect=greenSpiralEffect;

    }
}
