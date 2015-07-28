using Urho;

class _10_RenderToTexture : Sample
{
    private Scene scene;
    private Scene rttScene;
    private Node rttCameraNode;

    public _10_RenderToTexture(Context ctx) : base(ctx)
    {
    }

    public override void Start()
    {
        base.Start();
        CreateScene();
        SimpleCreateInstructionsWithWASD();
        SetupViewport();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        SubscribeToUpdate(args =>
        {
            SimpleMoveCamera(args.TimeStep);
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

        {
            rttScene = new Scene(Context);
            // Create octree, use default volume (-1000, -1000, -1000) to (1000, 1000, 1000)
            rttScene.CreateComponent<Octree>();

            // Create a Zone for ambient light & fog control
            Node zoneNode = rttScene.CreateChild("Zone");
            Zone zone = zoneNode.CreateComponent<Zone>();
            // Set same volume as the Octree, set a close bluish fog and some ambient light
            zone.SetBoundingBox(new BoundingBox(-1000.0f, 1000.0f));
            zone.AmbientColor = new Color(0.05f, 0.1f, 0.15f);
            zone.FogColor = new Color(0.1f, 0.2f, 0.3f);
            zone.FogStart = 10.0f;
            zone.FogEnd = 100.0f;

            // Create randomly positioned and oriented box StaticModels in the scene
            const uint NUM_OBJECTS = 2000;
            for (uint i = 0; i < NUM_OBJECTS; ++i)
            {
                Node boxNode = rttScene.CreateChild("Box");
                boxNode.Position = new Vector3(NextRandom(200.0f) - 100.0f, NextRandom(200.0f) - 100.0f,
                    NextRandom(200.0f) - 100.0f);
                // Orient using random pitch, yaw and roll Euler angles
                boxNode.Rotation = new Quaternion(NextRandom(360.0f), NextRandom(360.0f), NextRandom(360.0f));
                StaticModel boxObject = boxNode.CreateComponent<StaticModel>();
                boxObject.Model = cache.GetModel("Models/Box.mdl");
                boxObject.SetMaterial(cache.GetMaterial("Materials/Stone.xml"));

                // Add our custom Rotator component which will rotate the scene node each frame, when the scene sends its update event.
                // Simply set same rotation speed for all objects
                Rotator rotator = new Rotator(Context);
                boxNode.AddComponent(rotator);
                rotator.SetRotationSpeed(new Vector3(10.0f, 20.0f, 30.0f));
            }

            // Create a camera for the render-to-texture scene. Simply leave it at the world origin and let it observe the scene
            rttCameraNode = rttScene.CreateChild("Camera");
            Camera camera = rttCameraNode.CreateComponent<Camera>();
            camera.FarClip = 100.0f;

            // Create a point light to the camera scene node
            Light light = rttCameraNode.CreateComponent<Light>();
            light.LightType = LightType.LIGHT_POINT;
            light.Range = 30.0f;
        }

        {
            // Create the scene in which we move around

            scene = new Scene(Context);

            // Create octree, use also default volume (-1000, -1000, -1000) to (1000, 1000, 1000)
            scene.CreateComponent<Octree>();

            // Create a Zone component for ambient lighting & fog control
            Node zoneNode = scene.CreateChild("Zone");
            Zone zone = zoneNode.CreateComponent<Zone>();
            zone.SetBoundingBox(new BoundingBox(-1000.0f, 1000.0f));
            zone.AmbientColor = new Color(0.1f, 0.1f, 0.1f);
            zone.FogStart = 100.0f;
            zone.FogEnd = 300.0f;

            // Create a directional light without shadows
            Node lightNode = scene.CreateChild("DirectionalLight");
            lightNode.SetDirection(new Vector3(0.5f, -1.0f, 0.5f));
            Light light = lightNode.CreateComponent<Light>();
            light.LightType = LightType.LIGHT_DIRECTIONAL;
            light.Color = new Color(0.2f, 0.2f, 0.2f);
            light.SpecularIntensity = 1.0f;

            // Create a "floor" consisting of several tiles
            for (int y = -5; y <= 5; ++y)
            {
                for (int x = -5; x <= 5; ++x)
                {
                    Node floorNode = scene.CreateChild("FloorTile");
                    floorNode.Position = new Vector3(x*20.5f, -0.5f, y*20.5f);
                    floorNode.Scale = new Vector3(20.0f, 1.0f, 20.0f);
                    StaticModel floorObject = floorNode.CreateComponent<StaticModel>();
                    floorObject.Model = cache.GetModel("Models/Box.mdl");
                    floorObject.SetMaterial(cache.GetMaterial("Materials/Stone.xml"));
                }
            }

            // Create a "screen" like object for viewing the second scene. Construct it from two StaticModels, a box for the frame
            // and a plane for the actual view
            {
                Node boxNode = scene.CreateChild("ScreenBox");
                boxNode.Position = new Vector3(0.0f, 10.0f, 0.0f);
                boxNode.Scale = new Vector3(21.0f, 16.0f, 0.5f);
                StaticModel boxObject = boxNode.CreateComponent<StaticModel>();
                boxObject.Model = cache.GetModel("Models/Box.mdl");
                boxObject.SetMaterial(cache.GetMaterial("Materials/Stone.xml"));

                Node screenNode = scene.CreateChild("Screen");
                screenNode.Position = new Vector3(0.0f, 10.0f, -0.27f);
                screenNode.Rotation = new Quaternion(-90.0f, 0.0f, 0.0f);
                screenNode.Scale = new Vector3(20.0f, 0.0f, 15.0f);
                StaticModel screenObject = screenNode.CreateComponent<StaticModel>();
                screenObject.Model = cache.GetModel("Models/Plane.mdl");

                // Create a renderable texture (1024x768, RGB format), enable bilinear filtering on it
                Texture2D renderTexture = new Texture2D(Context);
                renderTexture.SetSize(1024, 768, Graphics.RGBFormat, TextureUsage.TEXTURE_RENDERTARGET);
                renderTexture.FilterMode = TextureFilterMode.FILTER_BILINEAR;

                // Create a new material from scratch, use the diffuse unlit technique, assign the render texture
                // as its diffuse texture, then assign the material to the screen plane object
                Material renderMaterial = new Material(Context);
                renderMaterial.SetTechnique(0, cache.GetTechnique("Techniques/DiffUnlit.xml"), 0, 0);
                renderMaterial.SetTexture(TextureUnit.TU_DIFFUSE, renderTexture);
                screenObject.SetMaterial(renderMaterial);

                // Get the texture's RenderSurface object (exists when the texture has been created in rendertarget mode)
                // and define the viewport for rendering the second scene, similarly as how backbuffer viewports are defined
                // to the Renderer subsystem. By default the texture viewport will be updated when the texture is visible
                // in the main view
                RenderSurface surface = renderTexture.RenderSurface;
                Viewport rttViewport = new Viewport(Context, rttScene, rttCameraNode.GetComponent<Camera>(), null);
                surface.SetViewport(0, rttViewport);
            }


            // Create the camera. Limit far clip distance to match the fog
            CameraNode = scene.CreateChild("Camera");
            var camera = CameraNode.CreateComponent<Camera>();
            camera.FarClip = 300.0f;
            // Set an initial position for the camera scene node above the plane
            CameraNode.Position = new Vector3(0.0f, 7.0f, -30.0f);
        }
    }
}

public class Rotator : LogicComponent
{
    private Vector3 rotationSpeed;

    public Rotator(Context c) : base(c) {}

    public void SetRotationSpeed(Vector3 vector)
    {
        rotationSpeed = vector;
    }

    public void Update(float timeStep)
    {
        Node.Rotate(new Quaternion(rotationSpeed.X * timeStep, rotationSpeed.Y * timeStep, rotationSpeed.Z * timeStep), TransformSpace.TS_LOCAL);       
    }
}