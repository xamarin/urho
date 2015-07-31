using System;
using Urho;

class _28_Urho2DPhysicsRope : Sample
{
    private Scene scene;
    private const uint NUM_OBJECTS = 10;

    public _28_Urho2DPhysicsRope(Context ctx) : base(ctx) { }

    public override void Start()
    {
        base.Start();
        CreateScene();
        SimpleCreateInstructionsWithWASD(", Use PageUp PageDown to zoom.");
        SetupViewport();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        var updateEventToken = SubscribeToUpdate(args =>
            {
                SimpleMoveCamera(args.TimeStep, false, 4f);

                if (Input.GetKeyDown(Key.PageUp))
                {
                    Camera camera = CameraNode.GetComponent<Camera>();
                    camera.Zoom = (camera.Zoom * 1.01f);
                }

                if (Input.GetKeyDown(Key.PageDown))
                {
                    Camera camera = CameraNode.GetComponent<Camera>();
                    camera.Zoom = (camera.Zoom * 0.99f);
                }

#warning MISSING_API GetComponent generic
                ((PhysicsWorld2D)scene.GetComponent(PhysicsWorld2D.TypeStatic)).DrawDebugGeometry();
            });
        
        updateEventToken.Unsubscribe();
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
        scene.CreateComponent<DebugRenderer>();
        // Create camera node
        CameraNode = scene.CreateChild("Camera");
        // Set camera's position
        CameraNode.Position = (new Vector3(0.0f, 5.0f, -10.0f));

        Camera camera = CameraNode.CreateComponent<Camera>();
        camera.SetOrthographic(true);

        var graphics = Graphics;
        camera.OrthoSize=graphics.Height * 0.05f;
        camera.Zoom = 1.5f * Math.Min(graphics.Width / 1280.0f, graphics.Height / 800.0f); // Set zoom according to user's resolution to ensure full visibility (initial zoom (1.5) is set for full visibility at 1280x800 resolution)

        // Create 2D physics world component
        PhysicsWorld2D physicsWorld = scene.CreateComponent<PhysicsWorld2D>();
        physicsWorld.DrawJoint = (true);

        // Create ground
        Node groundNode = scene.CreateChild("Ground");
        // Create 2D rigid body for gound
        RigidBody2D groundBody = groundNode.CreateComponent<RigidBody2D>();
        // Create edge collider for ground
        CollisionEdge2D groundShape = groundNode.CreateComponent<CollisionEdge2D>();
        groundShape.SetVertices(new Vector2(-40.0f, 0.0f), new Vector2(40.0f, 0.0f));

        const float y = 15.0f;
        RigidBody2D prevBody = groundBody;

        for (uint i = 0; i < NUM_OBJECTS; ++i)
        {
            Node node = scene.CreateChild("RigidBody");

            // Create rigid body
            RigidBody2D body = node.CreateComponent<RigidBody2D>();
            body.BodyType= BodyType2D.BT_DYNAMIC;

            // Create box
            CollisionBox2D box = node.CreateComponent<CollisionBox2D>();
            // Set friction
            box.Friction = 0.2f;
            // Set mask bits.
            box.MaskBits = 0xFFFF & ~0x0002;

            if (i == NUM_OBJECTS - 1)
            {
                node.Position = new Vector3(1.0f * i, y, 0.0f);
                body.AngularDamping=0.4f;
                box.SetSize(3.0f, 3.0f);
                box.Density=100.0f;
                box.CategoryBits=0x0002;
            }
            else
            {
                node.Position = new Vector3(0.5f + 1.0f * i, y, 0.0f);
                box.SetSize(1.0f, 0.25f);
                box.Density=20.0f;
                box.CategoryBits=0x0001;
            }

            ConstraintRevolute2D joint = node.CreateComponent<ConstraintRevolute2D>();
            joint.OtherBody=prevBody;
            joint.Anchor=new Vector2(i, y);
            joint.CollideConnected=false;

            prevBody = body;
        }

        ConstraintRope2D constraintRope = groundNode.CreateComponent<ConstraintRope2D>();
        constraintRope.OtherBody=prevBody;
        constraintRope.OwnerBodyAnchor=new Vector2(0.0f, y);
        constraintRope.MaxLength=NUM_OBJECTS - 1.0f + 0.01f;

    }
}
