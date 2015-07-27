using System.Collections.Generic;
using Urho;

class _20_HugeObjectCount : Sample
{
    private Scene scene;
    private bool drawDebug;
    private Camera camera;
    private bool animate;
    private bool useGroups;
    private List<Node> boxNodes;

    public _20_HugeObjectCount(Context ctx) : base(ctx) { }

    public override void Start()
    {
        base.Start();
        CreateScene();
        SimpleCreateInstructionsWithWASD(
            "\nSpace to toggle animation\n" +
            "G to toggle object group optimization");
        SetupViewport();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        SubscribeToUpdate(args =>
            {    
                // Toggle animation with space
                Input input = Input;
                if (input.GetKeyPress(' '))//KEY_SPACE
                    animate = !animate;

                // Toggle grouped / ungrouped mode
                if (input.GetKeyPress('G'))
                {
                    useGroups = !useGroups;
                    CreateScene();
                }
                SimpleMoveCamera(args.TimeStep);
                if (Input.GetKeyDown(' '))
                    drawDebug = !drawDebug;

                if (animate)
                    AnimateObjects(args.TimeStep);
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

    private void AnimateObjects(float timeStep)
    {
        const float ROTATE_SPEED = 15.0f;
        // Rotate about the Z axis (roll)
        Quaternion rotateQuat = Quaternion.FromAxisAngle(Vector3.UnitZ, ROTATE_SPEED * timeStep);

        foreach (var boxNode in boxNodes)
        {
            boxNode.Rotate(rotateQuat, TransformSpace.TS_LOCAL);
        }
    }

    private void SetupViewport()
    {
        var renderer = Renderer;
        renderer.SetViewport(0, new Viewport(Context, scene, CameraNode.GetComponent<Camera>(), null));
    }

    private void CreateScene()
    {
        var cache = ResourceCache;
        if (scene == null)
            scene = new Scene(Context);
        else
        {
            scene.Clear(true, true);
        }
        boxNodes = new List<Node>();

        // Create the Octree component to the scene so that drawable objects can be rendered. Use default volume
        // (-1000, -1000, -1000) to (1000, 1000, 1000)
        scene.CreateComponent<Octree>();

        // Create a Zone for ambient light & fog control
        Node zoneNode = scene.CreateChild("Zone");
        Zone zone = zoneNode.CreateComponent<Zone>();
        zone.SetBoundingBox(new BoundingBox(-1000.0f, 1000.0f));
        zone.FogColor = new Color(0.2f, 0.2f, 0.2f);
        zone.FogStart = 200.0f;
        zone.FogEnd = 300.0f;

        // Create a directional light
        Node lightNode = scene.CreateChild("DirectionalLight");
        lightNode.SetDirection(new Vector3(-0.6f, -1.0f, -0.8f)); // The direction vector does not need to be normalized
        Light light = lightNode.CreateComponent<Light>();
        light.LightType = LightType.LIGHT_DIRECTIONAL;

        if (!useGroups)
        {
            light.Color = new Color(0.7f, 0.35f, 0.0f);

            // Create individual box StaticModels in the scene
            for (int y = -125; y < 125; ++y)
            {
                for (int x = -125; x < 125; ++x)
                {
                    Node boxNode = scene.CreateChild("Box");
                    boxNode.Position = new Vector3(x * 0.3f, 0.0f, y * 0.3f);
                    boxNode.SetScale(0.25f);
                    StaticModel boxObject = boxNode.CreateComponent<StaticModel>();
                    boxObject.Model=cache.GetModel("Models/Box.mdl");
                    boxNodes.Add(boxNode);
                }
            }
        }
        else
        {
            light.Color=new Color(0.6f, 0.6f, 0.6f);
            light.SpecularIntensity=1.5f;

            // Create StaticModelGroups in the scene
            StaticModelGroup lastGroup = null;

            for (int y = -125; y< 125; ++y)
            {
                for (int x = -125; x< 125; ++x)
                {
                    // Create new group if no group yet, or the group has already "enough" objects. The tradeoff is between culling
                    // accuracy and the amount of CPU processing needed for all the objects. Note that the group's own transform
                    // does not matter, and it does not render anything if instance nodes are not added to it
                    if (lastGroup == null || lastGroup.NumInstanceNodes >= 25 * 25)
                    {
                        Node boxGroupNode = scene.CreateChild("BoxGroup");
                        lastGroup = boxGroupNode.CreateComponent<StaticModelGroup>();
                        lastGroup.Model=cache.GetModel("Models/Box.mdl");
                    }

                    Node boxNode = scene.CreateChild("Box");
                    boxNode.Position=new Vector3(x* 0.3f, 0.0f, y* 0.3f);
                    boxNode.SetScale(0.25f);
                    boxNodes.Add(boxNode);
                    lastGroup.AddInstanceNode(boxNode);
                }
            }
        }

        if (CameraNode == null)
        {
            // Create the camera. Limit far clip distance to match the fog
            CameraNode = scene.CreateChild("Camera");
            camera = CameraNode.CreateComponent<Camera>();
            camera.FarClip = 300.0f;
            // Set an initial position for the camera scene node above the plane
            CameraNode.Position = new Vector3(0.0f, 10.0f, -100.0f);
        }
    }
}
