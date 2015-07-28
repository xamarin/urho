using System.Collections.Generic;
using System.Runtime.InteropServices;
using Urho;

class _39_CrowdNavigation : Sample
{
    private Scene scene;
    private bool drawDebug;
    private Camera camera;
    private List<Vector3> currentPath_;
    private Vector3 endPos_;
    private List<Node> jackNodes_;
    private List<Node> mushroomNodes_;
    private float pitch_;
    private float yaw_;

    public _39_CrowdNavigation(Context ctx) : base(ctx) { }

    public override void Start()
    {
        base.Start();
        CreateScene();
        CreateUI();
        SetupViewport();
        SubscribeToEvents();
    }

    private void CreateUI()
    {
        var cache = ResourceCache;
        UI ui = UI;

        // Create a Cursor UI element because we want to be able to hide and show it at will. When hidden, the mouse cursor will
        // control the camera, and when visible, it will point the raycast target
        XMLFile style = cache.GetXmlFile("UI/DefaultStyle.xml");
        Cursor cursor=new Cursor(Context);
        cursor.SetStyleAuto(style);
        ui.Cursor=cursor;

        // Set starting position of the cursor at the rendering window center
        var graphics = Graphics;
        cursor.SetPosition(graphics.Width / 2, graphics.Height / 2);

        // Construct new Text object, set string to display and font to use
        var instructionText = ui.Root.CreateChild<Text>(Text.TypeStatic);

#warning MISSING_API Text::Text
        /*instructionText.Text = 
            "Use WASD keys to move, RMB to rotate view\n" +
            "LMB to set destination, SHIFT+LMB to spawn a Jack\n" +
            "MMB to add or remove obstacles\n" +
            "F5 to save scene, F7 to load\n" +
            "Space to toggle debug geometry";*/

        instructionText.SetFont(cache.GetFont("Fonts/Anonymous Pro.ttf"), 15);
        // The text has multiple rows. Center them in relation to each other
        instructionText.TextAlignment= HorizontalAlignment.HA_CENTER;

        // Position the text relative to the screen center
        instructionText.HorizontalAlignment = HorizontalAlignment.HA_CENTER;
        instructionText.VerticalAlignment = VerticalAlignment.VA_CENTER;
        instructionText.SetPosition(0, ui.Root.Height / 4);
    }


    private void SubscribeToEvents()
    {
        SubscribeToUpdate(args =>
            {
                MoveCamera(args.TimeStep);

                for (int i = 0; i < jackNodes_.Count; ++i)
                {
                    CrowdAgent agent = jackNodes_[i].GetComponent<CrowdAgent>();
                    Vector3 vel = agent.ActualVelocity;
                    jackNodes_[i].SetWorldDirection(vel);
                }
            });

        SubscribeToPostRenderUpdate(args =>
            {
                // If draw debug mode is enabled, draw viewport debug geometry, which will show eg. drawable bounding boxes and skeleton
                // bones. Note that debug geometry has to be separately requested each frame. Disable depth test so that we can see the
                // bones properly
                if (drawDebug)
                {
                    DebugRenderer debugRenderer = scene.GetComponent<DebugRenderer>();
                    DynamicNavigationMesh navMesh = scene.GetComponent<DynamicNavigationMesh>();
                    navMesh.DrawDebugGeometry(debugRenderer, true);

                    for (int i = 0; i < jackNodes_.Count; ++i)
                    {
                        CrowdAgent agent = jackNodes_[i].GetComponent<CrowdAgent>();
                        agent.DrawDebugGeometry(true);
                    }

                    for (int i = 0; i < mushroomNodes_.Count; ++i)
                    {
                        Obstacle obstacle = mushroomNodes_[i].GetComponent<Obstacle>();
                        obstacle.DrawDebugGeometry(true);
                    }
                }
            });

        SubscribeToCrowdAgentFailure(args =>
            {
                Node node = args.Node;
                CrowdAgent agent = args.CrowdAgent;
                CrowdAgentState agentState = (CrowdAgentState) args.CrowdAgentState;

                // If the agent's state is invalid, likely from spawning on the side of a box, find a point in a larger area
                if (agentState == CrowdAgentState.CROWD_AGENT_INVALID)
                {
                    DynamicNavigationMesh navMesh = scene.GetComponent<DynamicNavigationMesh>();
                    // Get a point on the navmesh using more generous extents
                    Vector3 newPt = navMesh.FindNearestPoint(node.WorldPosition, new Vector3(5.0f, 5.0f, 5.0f));
                    // Set the new node position, CrowdAgent component will automatically reset the state of the agent
                    node.SetWorldPosition(newPt);
                }
            });
    }

    private void MoveCamera(float timeStep)
    {
        // Right mouse button controls mouse cursor visibility: hide when pressed
        UI ui = UI;
        Input input = Input;
        ui.Cursor.SetVisible(!input.GetMouseButtonDown(MouseButton.Right));

        // Do not move if the UI has a focused element (the console)
        if (ui.FocusElement != null)
            return;

        // Movement speed as world units per second
        const float MOVE_SPEED = 20.0f;
        // Mouse sensitivity as degrees per pixel
        const float MOUSE_SENSITIVITY = 0.1f;

        // Use this frame's mouse motion to adjust camera node yaw and pitch. Clamp the pitch between -90 and 90 degrees
        // Only move the camera when the cursor is hidden
        if (!ui.Cursor.IsVisible())
        {
            IntVector2 mouseMove = input.MouseMove;
            yaw_ += MOUSE_SENSITIVITY * mouseMove.X;
            pitch_ += MOUSE_SENSITIVITY * mouseMove.Y;
            pitch_ = Clamp(pitch_, -90.0f, 90.0f);

            // Construct new orientation for the camera scene node from yaw and pitch. Roll is fixed to zero
            CameraNode.Rotation=new Quaternion(pitch_, yaw_, 0.0f);
        }

        // Read WASD keys and move the camera scene node to the corresponding direction if they are pressed
        if (input.GetKeyDown(Key.W))
            CameraNode.Translate(new Vector3(0, 0, 1) * MOVE_SPEED * timeStep, TransformSpace.TS_LOCAL);
        if (input.GetKeyDown(Key.S))
            CameraNode.Translate(new Vector3(0, 0, -1) * MOVE_SPEED * timeStep, TransformSpace.TS_LOCAL);
        if (input.GetKeyDown(Key.A))
            CameraNode.Translate(new Vector3(1, 0, 0) * MOVE_SPEED * timeStep, TransformSpace.TS_LOCAL);
        if (input.GetKeyDown(Key.D))
            CameraNode.Translate(new Vector3(-1, 0, 0) * MOVE_SPEED * timeStep, TransformSpace.TS_LOCAL);

        // Set destination or spawn a new jack with left mouse button
        if (input.GetMouseButtonPress(MouseButton.Left))
            SetPathPoint();
        // Add or remove objects with middle mouse button, then rebuild navigation mesh partially
        if (input.GetMouseButtonPress(MouseButton.Middle))
            AddOrRemoveObject();

        // Check for loading/saving the scene. Save the scene to the file Data/Scenes/CrowdNavigation.xml relative to the executable
        // directory
        if (input.GetKeyPress(Key.F5))
        {
            File saveFile = new File(Context, FileSystem.ProgramDir + "Data/Scenes/CrowdNavigation.xml", FileMode.FILE_WRITE);
#warning MISSING_API Scene::SaveXML
            //scene.SaveXML(saveFile);
        }
        if (input.GetKeyPress(Key.F7))
        {
            File loadFile = new File(Context, FileSystem.ProgramDir + "Data/Scenes/CrowdNavigation.xml", FileMode.FILE_READ);
#warning MISSING_API Scene::LoadXML
            //scene.LoadXML(loadFile);

            // Redetermine which nodes are "jacks" and "mushrooms"
            jackNodes_.Clear();

#warning MISSING_API GetChildrenWithComponent
            ////PODVector<Node> nodes;
            ////scene.GetChildrenWithComponent<CrowdAgent>(nodes, true);
            ////for (uint i = 0; i < nodes.Size(); ++i)
            ////{
            ////    jackNodes_.Push(Node(nodes[i]));
            ////}
            ////mushroomNodes_.Clear();
            ////scene.GetChildrenWithComponent<Obstacle>(nodes, true);
            ////for (uint i = 0; i < nodes.Size(); ++i)
            ////{
            ////    mushroomNodes_.Push(Node(nodes[i]));
            ////}
        }

        // Toggle debug geometry with space
        if (input.GetKeyPress(Key.Space))
            drawDebug = !drawDebug;
    }


    private bool Raycast(float maxDistance, out Vector3 hitPos, out Drawable hitDrawable)
    {
        hitDrawable = null;
        hitPos = Vector3.Zero;

        UI ui = UI;
        IntVector2 pos = ui.CursorPosition;
        // Check the cursor is visible and there is no UI element in front of the cursor
        if (!ui.Cursor.IsVisible() || ui.GetElementAt(pos, true) != null)
            return false;

        var graphics = Graphics;
        Camera camera = CameraNode.GetComponent<Camera>();
        Ray cameraRay = camera.GetScreenRay((float)pos.X / graphics.Width, (float)pos.Y / graphics.Height);
        // Pick only geometry objects, not eg. zones or lights, only get the first (closest) hit

#warning MISSING_API Octree::RaycastSingle
        ////PODVector<RayQueryResult> results;
        ////RayOctreeQuery query(results, cameraRay, RAY_TRIANGLE, maxDistance, DRAWABLE_GEOMETRY);
        ////scene.GetComponent<Octree>().RaycastSingle(query);
        ////if (results.Size())
        ////{
        ////    RayQueryResult & result = results[0];
        ////    hitPos = result.position_;
        ////    hitDrawable = result.drawable_;
        ////    return true;
        ////}

        return false;
    }

    private void SetPathPoint()
    {
        Vector3 hitPos;
        Drawable hitDrawable;
        DynamicNavigationMesh navMesh = scene.GetComponent<DynamicNavigationMesh>();

        if (Raycast(250.0f, out hitPos, out hitDrawable))
        {
            Vector3 pathPos = navMesh.FindNearestPoint(hitPos, new Vector3(1.0f, 1.0f, 1.0f));

            if (Input.GetQualifierDown(QUAL_SHIFT))
            {
                // spawn a jack at the target position
                CreateJack(pathPos);
            }
            else
            {
                // Set crowd agents to move to the target
                for (int i = 0; i < jackNodes_.Count; ++i)
                {
                    CrowdAgent agent = jackNodes_[i].GetComponent<CrowdAgent>();
                    // The first agent will always move to the exact position, all other agents will select a random point nearby
                    if (i == 0)
                    {
                        agent.SetMoveTarget(pathPos);
                    }
                    else
                    {
                        // Keep the random point somewhere on the navigation mesh
                        Vector3 targetPos = navMesh.FindNearestPoint(pathPos + new Vector3(NextRandom(-4.5f, 4.5f), 0.0f, NextRandom(-4.5f, 4.5f)), new Vector3(1.0f, 1.0f, 1.0f));
                        agent.SetMoveTarget(targetPos);
                    }
                }
            }
        }
    }


    private void AddOrRemoveObject()
    {
        // Raycast and check if we hit a mushroom node. If yes, remove it, if no, create a new one
        Vector3 hitPos;
        Drawable hitDrawable;

        if (Raycast(250.0f, out hitPos, out hitDrawable))
        {
            Node hitNode = hitDrawable.Node;
            if (hitNode.Name == "Mushroom")
            {
                mushroomNodes_.Remove(hitNode);
                hitNode.Remove();
            }
            else
            {
                CreateMushroom(hitPos);
            }
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

        scene = new Scene(Context);

        // Create octree, use default volume (-1000, -1000, -1000) to (1000, 1000, 1000)
        // Also create a DebugRenderer component so that we can draw debug geometry
        scene.CreateComponent<Octree>();
        scene.CreateComponent<DebugRenderer>();

        // Create scene node & StaticModel component for showing a static plane
        Node planeNode = scene.CreateChild("Plane");
        planeNode.Scale=new Vector3(100.0f, 1.0f, 100.0f);
        StaticModel planeObject = planeNode.CreateComponent<StaticModel>();
        planeObject.Model = (cache.GetModel("Models/Plane.mdl"));
        planeObject.SetMaterial(cache.GetMaterial("Materials/StoneTiled.xml"));

        // Create a Zone component for ambient lighting & fog control
        Node zoneNode = scene.CreateChild("Zone");
        Zone zone = zoneNode.CreateComponent<Zone>();
        zone.SetBoundingBox(new BoundingBox(-1000.0f, 1000.0f));
        zone.AmbientColor=new Color(0.15f, 0.15f, 0.15f);
        zone.FogColor = (new Color(0.5f, 0.5f, 0.7f));
        zone.FogStart = (100.0f);
        zone.FogEnd = (300.0f);

        // Create a directional light to the world. Enable cascaded shadows on it
        Node lightNode = scene.CreateChild("DirectionalLight");
        lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f));
        Light light = lightNode.CreateComponent<Light>();
        light.LightType = LightType.LIGHT_DIRECTIONAL;
        light.CastShadows=true;
        light.ShadowBias=new BiasParameters(0.00025f, 0.5f);
        // Set cascade splits at 10, 50 and 200 world units, fade shadows out at 80% of maximum shadow distance
        light.ShadowCascade=new CascadeParameters(10.0f, 50.0f, 200.0f, 0.0f, 0.8f);

        // Create randomly sized boxes. If boxes are big enough, make them occluders
        const uint NUM_BOXES = 20;
        for (uint i = 0; i < NUM_BOXES; ++i)
        {
            Node boxNode = scene.CreateChild("Box");
            float size = 1.0f + NextRandom(10.0f);
            boxNode.Position = (new Vector3(NextRandom(80.0f) - 40.0f, size * 0.5f, NextRandom(80.0f) - 40.0f));
            boxNode.SetScale(size);
            StaticModel boxObject = boxNode.CreateComponent<StaticModel>();
            boxObject.Model = (cache.GetModel("Models/Box.mdl"));
            boxObject.SetMaterial(cache.GetMaterial("Materials/Stone.xml"));
            boxObject.CastShadows=true;
            if (size >= 3.0f)
                boxObject.SetOccluder(true);
        }

        // Create a DynamicNavigationMesh component to the scene root
        DynamicNavigationMesh navMesh = scene.CreateComponent<DynamicNavigationMesh>();
        // Set the agent height large enough to exclude the layers under boxes
        navMesh.AgentHeight=10.0f;
        navMesh.TileSize=64;
        // Create a Navigable component to the scene root. This tags all of the geometry in the scene as being part of the
        // navigation mesh. By default this is recursive, but the recursion could be turned off from Navigable
        scene.CreateComponent<Navigable>();
        // Add padding to the navigation mesh in Y-direction so that we can add objects on top of the tallest boxes
        // in the scene and still update the mesh correctly
        navMesh.Padding=new Vector3(0.0f, 10.0f, 0.0f);
        // Now build the navigation geometry. This will take some time. Note that the navigation mesh will prefer to use
        // physics geometry from the scene nodes, as it often is simpler, but if it can not find any (like in this example)
        // it will use renderable geometry instead
        navMesh.Build();

        DetourCrowdManager crowdManager = scene.CreateComponent<DetourCrowdManager>();

        // Create Jack node that will follow the path
        CreateJack(new Vector3(-5.0f, 0.0f, 20.0f));

        // Create some mushrooms
        const uint NUM_MUSHROOMS = 100;
        for (uint i = 0; i < NUM_MUSHROOMS; ++i)
            CreateMushroom(new Vector3(NextRandom(90.0f) - 45.0f, 0.0f, NextRandom(90.0f) - 45.0f));

        // Create the camera. Limit far clip distance to match the fog
        CameraNode = scene.CreateChild("Camera");
        Camera camera = CameraNode.CreateComponent<Camera>();
        camera.FarClip=300.0f;

        // Set an initial position for the camera scene node above the plane
        CameraNode.Position = (new Vector3(0.0f, 5.0f, 0.0f));
    }

    private void CreateJack(Vector3 pos)
    {
        var cache = ResourceCache;
        Node jackNode=scene.CreateChild("Jack");
        jackNode.Position=(pos);
        AnimatedModel modelObject = jackNode.CreateComponent<AnimatedModel>();
        modelObject.Model=(cache.GetModel("Models/Jack.mdl"));
        modelObject.SetMaterial(cache.GetMaterial("Materials/Jack.xml"));
        modelObject.CastShadows=true;

        // Create the CrowdAgent
        CrowdAgent agent = jackNode.CreateComponent<CrowdAgent>();
        agent.Height=2.0f;
        jackNodes_.Add(jackNode);
    }

    private Node CreateMushroom(Vector3 pos)
    {
        var cache = ResourceCache;

        Node mushroomNode = scene.CreateChild("Mushroom");
        mushroomNode.Position = (pos);
        mushroomNode.Rotation=new Quaternion(0.0f, NextRandom(360.0f), 0.0f);
        mushroomNode.SetScale(2.0f + NextRandom(0.5f));
        StaticModel mushroomObject = mushroomNode.CreateComponent<StaticModel>();
        mushroomObject.Model = (cache.GetModel("Models/Mushroom.mdl"));
        mushroomObject.SetMaterial(cache.GetMaterial("Materials/Mushroom.xml"));
        mushroomObject.CastShadows=true;
        // Create the navigation obstacle
        Obstacle obstacle = mushroomNode.CreateComponent<Obstacle>();
        obstacle.Radius=mushroomNode.Scale.X;
        mushroomNodes_.Add(mushroomNode);

        return mushroomNode;
    }

}
