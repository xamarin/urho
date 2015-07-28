using System;
using System.Collections.Generic;
using Urho;

class _17_SceneReplication : Sample
{
    private Scene scene;
    private bool drawDebug;
    private Camera camera;
    
    /// Mapping from client connections to controllable objects.
    Dictionary<Connection, Node> serverObjects_;
    /// Button container element.
    UIElement buttonContainer_;
    /// Server address line editor element.
    LineEdit textEdit_;
    /// Connect button.
    Button connectButton_;
    /// Disconnect button.
    Button disconnectButton_;
    /// Start server button.
    Button startServerButton_;
    /// Instructions text.
    Text instructionsText_;
    /// ID of own controllable object (client only.)
    uint clientObjectID_;

    public const ushort SERVER_PORT = 2345;
#warning MISSING_API StringHash(String)
    //static StringHash E_CLIENTOBJECTID("ClientObjectID");
    //static StringHash P_ID("ID");

    public _17_SceneReplication(Context ctx) : base(ctx) { }

    public override void Start()
    {
        base.Start();

        // Create the scene content
        CreateScene();

        // Create the UI content
        CreateUI();

        // Setup the viewport for displaying the scene
        SetupViewport();

        // Hook up to necessary events
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        SubscribeToPhysicsPreStep(HandlePhysicsPreStep);
        SubscribeToPostUpdate(HandlePostUpdate);

        SubscribeToReleased(args =>
        {
            if (args.Element == connectButton_) HandleConnect();
            if (args.Element == disconnectButton_) HandleDisconnect();
            if (args.Element == startServerButton_) HandleStartServer();
        });

        SubscribeToServerConnected(args => HandleConnectionStatus());
        SubscribeToServerDisconnected(args => HandleConnectionStatus());
        SubscribeToConnectFailed(args => HandleConnectionStatus());
        SubscribeToClientConnected(HandleClientConnected);
        SubscribeToClientDisconnected(HandleClientDisconnected);

#warning MISSING_API E_CLIENTOBJECTID
        ////SubscribeToEvent(E_CLIENTOBJECTID, HANDLER(SceneReplication, HandleClientObjectID));
        ////Network.RegisterRemoteEvent(E_CLIENTOBJECTID);
    }
    
    private void SetupViewport()
    {
        var renderer = Renderer;
        renderer.SetViewport(0, new Viewport(Context, scene, CameraNode.GetComponent<Camera>(), null));
    }

    private void CreateScene()
    {
        scene = new Scene(Context);

        // Create scene content on the server only
        var cache = ResourceCache;
        
        // Create octree and physics world with default settings. Create them as local so that they are not needlessly replicated
        // when a client connects
        scene.CreateComponent<Octree>(CreateMode.LOCAL);
#warning MISSING_API
        ////scene.CreateComponent<PhysicsWorld>(CreateMode.LOCAL);

        // All static scene content and the camera are also created as local, so that they are unaffected by scene replication and are
        // not removed from the client upon connection. Create a Zone component first for ambient lighting & fog control.
        Node zoneNode = scene.CreateChild("Zone", CreateMode.LOCAL, 0);
        Zone zone = zoneNode.CreateComponent<Zone>();
        zone.SetBoundingBox(new BoundingBox(-1000.0f, 1000.0f));
        zone.AmbientColor=new Color(0.1f, 0.1f, 0.1f);
        zone.FogStart = 100.0f;
        zone.FogEnd = 300.0f;

        // Create a directional light without shadows
        Node lightNode = scene.CreateChild("DirectionalLight", CreateMode.LOCAL, 0);
        lightNode.SetDirection(new Vector3(0.5f, -1.0f, 0.5f));
        Light light = lightNode.CreateComponent<Light>();
        light.LightType = LightType.LIGHT_DIRECTIONAL;
        light.Color = new Color(0.2f, 0.2f, 0.2f);
        light.SpecularIntensity = 1.0f;

        // Create a "floor" consisting of several tiles. Make the tiles physical but leave small cracks between them
        for (int y = -20; y <= 20; ++y)
        {
            for (int x = -20; x <= 20; ++x)
            {
                Node floorNode = scene.CreateChild("FloorTile", CreateMode.LOCAL, 0);
                floorNode.Position = new Vector3(x * 20.2f, -0.5f, y * 20.2f);
                floorNode.Scale = new Vector3(20.0f, 1.0f, 20.0f);
                StaticModel floorObject = floorNode.CreateComponent<StaticModel>();
                floorObject.Model = (cache.GetModel("Models/Box.mdl"));
                floorObject.SetMaterial(cache.GetMaterial("Materials/Stone.xml"));

#warning MISSING_API
                ////RigidBody body = floorNode.CreateComponent<RigidBody>();
                ////body.Friction = 1.0f;
                ////CollisionShape shape = floorNode.CreateComponent<CollisionShape>();
                ////shape.SetBox(Vector3.One);
            }
        }

        // Create the camera. Limit far clip distance to match the fog
        // The camera needs to be created into a local node so that each client can retain its own camera, that is unaffected by
        // network messages. Furthermore, because the client removes all replicated scene nodes when connecting to a server scene,
        // the screen would become blank if the camera node was replicated (as only the locally created camera is assigned to a
        // viewport in SetupViewports() below)
        CameraNode = scene.CreateChild("Camera", CreateMode.LOCAL, 0);
        Camera camera = CameraNode.CreateComponent<Camera>();
        camera.FarClip = 300.0f;

        // Set an initial position for the camera scene node above the plane
        CameraNode.Position = new Vector3(0.0f, 5.0f, 0.0f);
    }

    private void CreateUI()
    {
        var cache = ResourceCache;
        UI ui = UI;
        UIElement root = ui.Root;
        XMLFile uiStyle = cache.GetXmlFile("UI/DefaultStyle.xml");
        // Set style to the UI root so that elements will inherit it
        root.SetDefaultStyle(uiStyle);

        // Create a Cursor UI element because we want to be able to hide and show it at will. When hidden, the mouse cursor will
        // control the camera, and when visible, it can interact with the login UI
        Cursor cursor = new Cursor(Context);
        cursor.SetStyleAuto(uiStyle);
        ui.Cursor = cursor;
        // Set starting position of the cursor at the rendering window center
        var graphics = Graphics;
        cursor.SetPosition(graphics.Width / 2, graphics.Height / 2);

        // Construct the instructions text element
        instructionsText_ = new Text(Context);
        instructionsText_.Value = "Use WASD keys to move and RMB to rotate view";
        instructionsText_.SetFont(cache.GetFont("Fonts/Anonymous Pro.ttf"), 15);
        // Position the text relative to the screen center
        instructionsText_.HorizontalAlignment = HorizontalAlignment.HA_CENTER;
        instructionsText_.VerticalAlignment = VerticalAlignment.VA_CENTER;
        instructionsText_.SetPosition(0, graphics.Height / 4);
        // Hide until connected
        instructionsText_.SetVisible(false);
        ui.Root.AddChild(instructionsText_);

        buttonContainer_ = new UIElement(Context);
        buttonContainer_.SetFixedSize(500, 20);
        buttonContainer_.SetPosition(20, 20);
        buttonContainer_.LayoutMode = (LayoutMode.LM_HORIZONTAL);
        root.AddChild(buttonContainer_);

        textEdit_ = new LineEdit(Context);
        textEdit_.SetStyleAuto(null);
        buttonContainer_.AddChild(textEdit_);

        connectButton_ = CreateButton("Connect", 90);
        disconnectButton_ = CreateButton("Disconnect", 100);
        startServerButton_ = CreateButton("Start Server", 110);

        UpdateButtons();
    }

    Button CreateButton(string text, int width)
    {
        var cache = ResourceCache;
        Font font = cache.GetFont("Fonts/Anonymous Pro.ttf");

        Button button = new Button(Context);
        button.SetStyleAuto(null);
        button.SetFixedWidth(width);
        buttonContainer_.AddChild(button);

        var buttonText = new Text(Context);
        buttonText.SetFont(font, 12);
        buttonText.SetAlignment(HorizontalAlignment.HA_CENTER, VerticalAlignment.VA_CENTER);
        buttonText.Value = text;
        button.AddChild(buttonText);

        return button;
    }

    private void UpdateButtons()
    {
        Network network = Network;
        Connection serverConnection = network.ServerConnection;
        bool serverRunning = network.IsServerRunning();

        // Show and hide buttons so that eg. Connect and Disconnect are never shown at the same time
        connectButton_.SetVisible(serverConnection == null && !serverRunning);
        disconnectButton_.SetVisible(serverConnection != null || serverRunning);
        startServerButton_.SetVisible(serverConnection == null && !serverRunning);
        textEdit_.SetVisible(serverConnection == null && !serverRunning);
    }

    Node CreateControllableObject()
    {
        var cache = ResourceCache;

        // Create the scene node & visual representation. This will be a replicated object
        Node ballNode = scene.CreateChild("Ball");
        ballNode.Position = (new Vector3(NextRandom(40.0f) - 20.0f, 5.0f, NextRandom(40.0f) - 20.0f));
        ballNode.SetScale(0.5f);
        StaticModel ballObject = ballNode.CreateComponent<StaticModel>();
        ballObject.Model = (cache.GetModel("Models/Sphere.mdl"));
        ballObject.SetMaterial(cache.GetMaterial("Materials/StoneSmall.xml"));

#warning MISSING_API
        // Create the physics components
        ////RigidBody body = ballNode.CreateComponent<RigidBody>();
        ////body.SetMass(1.0f);
        ////body.Friction = (1.0f);
        ////// In addition to friction, use motion damping so that the ball can not accelerate limitlessly
        ////body.SetLinearDamping(0.5f);
        ////body.SetAngularDamping(0.5f);
        ////CollisionShape shape = ballNode.CreateComponent<CollisionShape>();
        ////shape.SetSphere(1.0f);

        // Create a random colored point light at the ball so that can see better where is going
        Light light = ballNode.CreateComponent<Light>();
        light.Range = (3.0f);
        light.Color = (new Color(0.5f + ((int)NextRandom() & 1) * 0.5f, 0.5f + ((int)NextRandom() & 1) * 0.5f, 0.5f + ((int)NextRandom() & 1) * 0.5f));

        return ballNode;
    }

    private void MoveCamera()
    {
        // Right mouse button controls mouse cursor visibility: hide when pressed
        UI ui = UI;
        Input input = Input;
        ui.Cursor.SetVisible(!input.GetMouseButtonDown(MouseButton.Right));

        // Mouse sensitivity as degrees per pixel
        const float MOUSE_SENSITIVITY = 0.1f;

        // Use this frame's mouse motion to adjust camera node yaw and pitch. Clamp the pitch and only move the camera
        // when the cursor is hidden
        if (!ui.Cursor.IsVisible())
        {
            IntVector2 mouseMove = input.MouseMove;
            Yaw += MOUSE_SENSITIVITY * mouseMove.X;
            Pitch += MOUSE_SENSITIVITY * mouseMove.Y;
            Pitch = Clamp(Pitch, 1.0f, 90.0f);
        }

        // Construct new orientation for the camera scene node from yaw and pitch. Roll is fixed to zero
        CameraNode.Rotation = new Quaternion(Pitch, Yaw, 0.0f);

        // Only move the camera / show instructions if we have a controllable object
        bool showInstructions = false;
        if (clientObjectID_ != null)
        {
            Node ballNode = scene.GetNode(clientObjectID_);
            if (ballNode != null)
            {
                const float CAMERA_DISTANCE = 5.0f;

                // Move camera some distance away from the ball
                CameraNode.Position = (ballNode.Position + CameraNode.Rotation * new Vector3(0f, 0f, -1f) * CAMERA_DISTANCE);
                showInstructions = true;
            }
        }

        instructionsText_.SetVisible(showInstructions);
    }

    private void HandlePostUpdate(PostUpdateEventArgs postUpdateEventArgs)
    {
        // We only rotate the camera according to mouse movement since last frame, so do not need the time step
        MoveCamera();
    }

    private void HandlePhysicsPreStep(PhysicsPreStepEventArgs physicsPreStepEventArgs)
    {
        // This function is different on the client and server. The client collects controls (WASD controls + yaw angle)
        // and sets them to its server connection object, so that they will be sent to the server automatically at a
        // fixed rate, by default 30 FPS. The server will actually apply the controls (authoritative simulation.)
        Network network = Network;
        Connection serverConnection = network.ServerConnection;

        // Client: collect controls
        if (serverConnection != null)
        {
            UI ui = UI;
            Input input = Input;
#warning MISSING_API Controls
            ////Controls controls;

            ////// Copy mouse yaw
            ////controls._yaw = Yaw;

            ////// Only apply WASD controls if there is no focused UI element
            ////if (!ui.FocusElement)
            ////{
            ////    controls.Set(CTRL_FORWARD, input.GetKeyDown(Keys.W));
            ////    controls.Set(CTRL_BACK, input.GetKeyDown(Key.S));
            ////    controls.Set(CTRL_LEFT, input.GetKeyDown(Key.A));
            ////    controls.Set(CTRL_RIGHT, input.GetKeyDown(Key.D));
            ////}

            ////serverConnection.SetControls(controls);
            // In case the server wants to do position-based interest management using the NetworkPriority components, we should also
            // tell it our observer (camera) position. In this sample it is not in use, but eg. the NinjaSnowWar game uses it
            serverConnection.Position = (CameraNode.Position);
        }
        // Server: apply controls to client objects
        else if (network.IsServerRunning())
        {
#warning MISSING_API Network::GetClientConnections, RigidBody
            ////var connections = network.GetClientConnections();

            ////for (int i = 0; i < connections.Size(); ++i)
            ////{
            ////    Connection connection = connections[i];
            ////    // Get the object this connection is controlling
            ////    Node ballNode = serverObjects_[connection];
            ////    if (ballNode == null)
            ////        continue;

            ////    RigidBody body = ballNode.GetComponent<RigidBody>();

            ////    // Get the last controls sent by the client
            ////    var controls = connection.GetControls();
            ////    // Torque is relative to the forward vector
            ////    Quaternion rotation = new Quaternion(0.0f, controls.yaw_, 0.0f);

            ////    const float MOVE_TORQUE = 3.0f;

            ////    // Movement torque is applied before each simulation step, which happen at 60 FPS. This makes the simulation
            ////    // independent from rendering framerate. We could also apply forces (which would enable in-air control),
            ////    // but want to emphasize that it's a ball which should only control its motion by rolling along the ground
            ////    if (controls.buttons_ & CTRL_FORWARD)
            ////        body.ApplyTorque(rotation * Vector3.UnitX * MOVE_TORQUE);
            ////    if (controls.buttons_ & CTRL_BACK)
            ////        body.ApplyTorque(rotation * Vector3::LEFT * MOVE_TORQUE);
            ////    if (controls.buttons_ & CTRL_LEFT)
            ////        body.ApplyTorque(rotation * Vector3.UnitZ * MOVE_TORQUE);
            ////    if (controls.buttons_ & CTRL_RIGHT)
            ////        body.ApplyTorque(rotation * Vector3::BACK * MOVE_TORQUE);
            ////}
    }
}

    private void HandleConnect()
    {
        Network network = Network;
        String address = textEdit_.Text.Trim();
        if (string.IsNullOrEmpty(address))
            address = "localhost"; // Use localhost to connect if nothing else specified

        // Connect to server, specify scene to use as a client for replication
        clientObjectID_ = 0; // Reset own object ID from possible previous connection
#warning MISSING_API Network::Connect
        ////network.Connect(address, SERVER_PORT, scene);

        UpdateButtons();
    }

    private void HandleDisconnect()
    {
        Network network = Network;
        Connection serverConnection = network.ServerConnection;
        // If we were connected to server, disconnect. Or if we were running a server, stop it. In both cases clear the
        // scene of all replicated content, but let the local nodes & components (the static world + camera) stay
        if (serverConnection != null)
        {
            serverConnection.Disconnect(0);
            scene.Clear(true, false);
            clientObjectID_ = 0;
        }
        // Or if we were running a server, stop it
        else if (network.IsServerRunning())
        {
            network.StopServer();
            scene.Clear(true, false);
        }

        UpdateButtons();
    }

    private void HandleStartServer()
    {
        Network network = Network;
        network.StartServer(SERVER_PORT);

        UpdateButtons();
    }

    private void HandleConnectionStatus()
    {
        UpdateButtons();
    }

    private void HandleClientConnected(ClientConnectedEventArgs args)
    {
        // When a client connects, assign to scene to begin scene replication
        Connection newConnection = args.Connection;
        newConnection.Scene = scene;
    
        // Then create a controllable object for that client
        Node newObject = CreateControllableObject();
        serverObjects_[newConnection] = newObject;

#warning MISSING_API Connection::SendRemoteEvent
        ////// Finally send the object's node ID using a remote event
        ////VariantMap remoteEventData;
        ////remoteEventData[P_ID] = newObject.GetID();
        ////newConnection.SendRemoteEvent(E_CLIENTOBJECTID, true, remoteEventData);
    }

    private void HandleClientDisconnected(ClientDisconnectedEventArgs args)
    {
        // When a client disconnects, remove the controlled object
        Connection connection = args.Connection;
        Node n = serverObjects_[connection];
        if (n != null)
            n.Remove();

        serverObjects_.Remove(connection);
    }

#warning MISSING_API custom event
    ////private void HandleClientObjectID()
    ////{
    ////    clientObjectID_ = eventData[P_ID].GetUInt();
    ////}
}
