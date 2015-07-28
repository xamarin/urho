using System.Collections.Generic;
using Urho;

class _24_Urho2DSprite : Sample
{
    private Scene scene;
    private Camera camera;
    private List<Node> spriteNodes;
    private const uint NumSprites = 200;

#warning MISSIN_API //constant
    private const float PIXEL_SIZE = 0.01f;

    private const string VarMovespeed = "MoveSpeed";
    private const string VarRotatespeed = "RotateSpeed";

    public _24_Urho2DSprite(Context ctx) : base(ctx) { }

    public override void Start()
    {
        base.Start();
        CreateScene();
        SimpleCreateInstructionsWithWASD("\nuse PageUp PageDown keys to zoom.");
        SetupViewport();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        SubscribeToUpdate(args =>
            {
                SimpleMoveCamera(args.TimeStep);
                var input = Input;
                if (input.GetKeyDown(Key.PageUp))
                {
                    Camera camera = CameraNode.GetComponent<Camera>();
                    camera.Zoom=camera.Zoom * 1.01f;
                }

                if (input.GetKeyDown(Key.PageDown))
                {
                    Camera camera = CameraNode.GetComponent<Camera>();
                    camera.Zoom=camera.Zoom * 0.99f;
                }

                var graphics = Graphics;
                float halfWidth = (float)graphics.Width * 0.5f * PIXEL_SIZE;
                float halfHeight = (float)graphics.Height * 0.5f * PIXEL_SIZE;

                for (int i = 0; i < spriteNodes.Count; ++i)
                {
                    var node = spriteNodes[i];

                    Vector3 position = node.Position;
#warning MISSING_API //GetVar
                    Vector3 moveSpeed = Vector3.Zero; //node.GetVar(VAR_MOVESPEED).GetVector3();
                    Vector3 newPosition = position + moveSpeed * args.TimeStep;
                    if (newPosition.X < -halfWidth || newPosition.X > halfWidth)
                    {
                        newPosition.X = position.X;
                        moveSpeed.X = -moveSpeed.X;
#warning MISSING_API //SetVar
                        //node.SetVar(VAR_MOVESPEED, moveSpeed);
                    }
                    if (newPosition.Y < -halfHeight || newPosition.Y > halfHeight)
                    {
                        newPosition.Y = position.Y;
                        moveSpeed.Y = -moveSpeed.Y;
#warning MISSING_API //SetVar
                        //node.SetVar(VAR_MOVESPEED, moveSpeed);
                    }

                    node.Position = (newPosition);

#warning MISSING_API //GetVar
                    float rotateSpeed = 1.0f;//node.GetVar(VAR_ROTATESPEED).GetFloat();
                    node.Roll(rotateSpeed * args.TimeStep, TransformSpace.TS_LOCAL);
                }

            });

#warning MISSIN_API 
        //UnsubscribeFromEvent(E_SCENEUPDATE); //is it   new StringHash("E_SCENEUPDATE") ?
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

        var cache = ResourceCache;
        // Get sprite
        Sprite2D sprite = cache.GetSprite2D("Urho2D/Aster.png");
        if (sprite == null)
            return;

        float halfWidth = graphics.Width * 0.5f * PIXEL_SIZE;
        float halfHeight = graphics.Height * 0.5f * PIXEL_SIZE;

        for (uint i = 0; i < NumSprites; ++i)
        {
            Node spriteNode = scene.CreateChild("StaticSprite2D");
            spriteNode.Position = (new Vector3(NextRandom(-halfWidth, halfWidth), NextRandom(-halfHeight, halfHeight), 0.0f));

            StaticSprite2D staticSprite = spriteNode.CreateComponent<StaticSprite2D>();
            // Set random color
            staticSprite.Color = (new Color(NextRandom(1.0f), NextRandom(1.0f), NextRandom(1.0f), 1.0f));
            // Set blend mode
            staticSprite.BlendMode = BlendMode.BLEND_ALPHA;
            // Set sprite
            staticSprite.Sprite=sprite;

#warning MISSIN_API SetVar
            ////// Set move speed
            ////spriteNode.SetVar(VAR_MOVESPEED, new Vector3(NextRandom(-2.0f, 2.0f), NextRandom(-2.0f, 2.0f), 0.0f));
            ////// Set rotate speed
            ////spriteNode.SetVar(VAR_ROTATESPEED, NextRandom(-90.0f, 90.0f));

            // Add to sprite node vector
            spriteNodes.Add(spriteNode);
        }

        // Get animation set
        AnimationSet2D animationSet = cache.GetAnimationSet2D("Urho2D/GoldIcon.scml");
        if (animationSet == null)
            return;

        var spriteNode2 = scene.CreateChild("AnimatedSprite2D");
        spriteNode2.Position = (new Vector3(0.0f, 0.0f, -1.0f));

        AnimatedSprite2D animatedSprite = spriteNode2.CreateComponent<AnimatedSprite2D>();
        // Set animation
        animatedSprite.SetAnimation(animationSet, "idle", LoopMode2D.LM_DEFAULT);

    }
}
