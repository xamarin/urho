using Urho;

namespace $safeprojectname$
{
    public class MyGame : Application
    {
        public MyGame(Context context) : base(context) {}

        public override void Start()
        {
            CreateScene();

            // Subscribe to Esc key:
            SubscribeToKeyDown(args => { if (args.Key == Key.Esc) Engine.Exit(); });
        }

        async void CreateScene()
        {
            // UI text 
            var helloText = new Text(Context)
                {
                    Value = "Hello World from $safeprojectname$",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
            helloText.SetColor(new Color(0f, 1f, 1f));
            helloText.SetFont(font: ResourceCache.GetFont("Fonts/Font.ttf"), size: 30);
            UI.Root.AddChild(helloText);

            // 3D scene with Octree
            var scene = new Scene(Context);
            scene.CreateComponent<Octree>();

            // Box
            Node boxNode = scene.CreateChild();
            boxNode.Position = new Vector3(0, 0, 5);
            boxNode.SetScale(0f);
            boxNode.Rotation = new Quaternion(60, 0, 30);
            StaticModel modelObject = boxNode.CreateComponent<StaticModel>();
            modelObject.Model = ResourceCache.GetModel("Models/Box.mdl");

            // Light
            Node lightNode = scene.CreateChild(name: "light");
            lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f));
            lightNode.CreateComponent<Light>();

            // Camera
            Node cameraNode = scene.CreateChild(name: "camera");
            Camera camera = cameraNode.CreateComponent<Camera>();

            // Viewport
            Renderer.SetViewport(0, new Viewport(Context, scene, camera, null));

            // Do actions
            await boxNode.RunActionsAsync(new EaseBounceOut(new ScaleTo(duration: 1f, scale: 1)));
            await boxNode.RunActionsAsync(new RepeatForever(
                new RotateBy(duration: 1, deltaAngleX: 90, deltaAngleY: 0, deltaAngleZ: 0)));
        }
    }
}
