using System;
using System.Collections.Generic;
using Urho;

class _24_Urho2DSprite : Sample
{
    private Scene scene;
    private List<NodeInfo> spriteNodes;
    private const uint NumSprites = 200;

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
                float halfWidth = graphics.Width * 0.5f * PIXEL_SIZE;
                float halfHeight = graphics.Height * 0.5f * PIXEL_SIZE;

                foreach (var nodeInfo in spriteNodes)
                {
                    Vector3 position = nodeInfo.Node.Position;
                    Vector3 moveSpeed = nodeInfo.MoveSpeed;
                    Vector3 newPosition = position + moveSpeed * args.TimeStep;
                    if (newPosition.X < -halfWidth || newPosition.X > halfWidth)
                    {
                        newPosition.X = position.X;
                        moveSpeed.X = -moveSpeed.X;
                        nodeInfo.MoveSpeed = moveSpeed;
                    }
                    if (newPosition.Y < -halfHeight || newPosition.Y > halfHeight)
                    {
                        newPosition.Y = position.Y;
                        moveSpeed.Y = -moveSpeed.Y;
                        nodeInfo.MoveSpeed = moveSpeed;
                    }

                    nodeInfo.Node.Position = (newPosition);
                    nodeInfo.Node.Roll(nodeInfo.RotateSpeed * args.TimeStep, TransformSpace.TS_LOCAL);
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
        spriteNodes = new List<NodeInfo>((int) NumSprites);

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
            // Add to sprite node vector
            spriteNodes.Add(new NodeInfo(spriteNode, new Vector3(NextRandom(-2.0f, 2.0f), NextRandom(-2.0f, 2.0f), 0.0f), NextRandom(-90.0f, 90.0f)));
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

    class NodeInfo
    {
        public Node Node { get; set; }
        public Vector3 MoveSpeed { get; set; }
        public float RotateSpeed { get; set; }

        public NodeInfo(Node node, Vector3 moveSpeed, float rotateSpeed)
        {
            Node = node;
            MoveSpeed = moveSpeed;
            RotateSpeed = rotateSpeed;
        }
    }
}
