using System;
using Urho;

class _33_Urho2DSpriterAnimation : Sample
{
    private Scene scene;
    private Node spriteNode;
    private int animationIndex;
    private static readonly string[] AnimationNames =
        {
            "idle",
            "run",
            "attack",
            "hit",
            "dead",
            "dead2",
            "dead3",
        };
    

    public _33_Urho2DSpriterAnimation(Context ctx) : base(ctx) { }

    public override void Start()
    {
        base.Start();
        CreateScene();
        SimpleCreateInstructions("Mouse click to play next animation, \nUse WASD keys to move, use PageUp PageDown keys to zoom.");
        SetupViewport();
        SubscribeToEvents();
    }

    void MoveCamera(float timeStep)
    {
        // Do not move if the UI has a focused element (the console)
        if (UI.FocusElement != null)
            return;

        Input input = Input;

        // Movement speed as world units per second
        const float moveSpeed = 4.0f;

        // Read WASD keys and move the camera scene node to the corresponding direction if they are pressed
        if (input.GetKeyDown(Key.W))
            CameraNode.Translate(Vector3.UnitY * moveSpeed * timeStep, TransformSpace.TS_LOCAL);
        if (input.GetKeyDown(Key.S))
            CameraNode.Translate(new Vector3(0.0f, -1.0f, 0.0f) * moveSpeed * timeStep, TransformSpace.TS_LOCAL);
        if (input.GetKeyDown(Key.A))
            CameraNode.Translate(new Vector3(-1.0f, 0.0f, 0.0f) * moveSpeed * timeStep, TransformSpace.TS_LOCAL);
        if (input.GetKeyDown(Key.D))
            CameraNode.Translate(Vector3.UnitX * moveSpeed * timeStep, TransformSpace.TS_LOCAL);

        if (input.GetKeyDown(Key.PageUp))
        {
            Camera camera = CameraNode.GetComponent<Camera>();
            camera.Zoom = (camera.Zoom * 1.01f);
        }

        if (input.GetKeyDown(Key.PageDown))
        {
            Camera camera = CameraNode.GetComponent<Camera>();
            camera.Zoom = (camera.Zoom * 0.99f);
        }
    }


    private void SubscribeToEvents()
    {
        SubscribeToUpdate(args => MoveCamera(args.TimeStep));

        SubscribeToMouseButtonDown(args =>
            {
                AnimatedSprite2D animatedSprite = spriteNode.GetComponent<AnimatedSprite2D>();
                animationIndex = (animationIndex + 1) % 7;
                animatedSprite.SetAnimation(AnimationNames[animationIndex], LoopMode2D.LM_FORCE_LOOPED);
            });

        // Unsubscribe the SceneUpdate event from base class to prevent camera pitch and yaw in 2D sample
        SceneUpdateEventToken.Unsubscribe();
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
        camera.Zoom = (1.5f * Math.Min((float)graphics.Width / 1280.0f, (float)graphics.Height / 800.0f)); // Set zoom according to user's resolution to ensure full visibility (initial zoom (1.5) is set for full visibility at 1280x800 resolution)

        var cache = ResourceCache;
        AnimationSet2D animationSet = cache.GetAnimationSet2D("Urho2D/imp/imp.scml");
        if (animationSet == null)
            return;

        spriteNode = scene.CreateChild("SpriterAnimation");

        AnimatedSprite2D animatedSprite = spriteNode.CreateComponent<AnimatedSprite2D>();
        animatedSprite.SetAnimation(animationSet, AnimationNames[animationIndex], LoopMode2D.LM_DEFAULT);
    }
}
