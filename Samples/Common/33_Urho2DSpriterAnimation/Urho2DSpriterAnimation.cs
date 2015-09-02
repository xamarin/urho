using System;
using Urho;

public class _33_Urho2DSpriterAnimation : Sample
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

	private void SubscribeToEvents()
	{
		SubscribeToMouseButtonDown(args =>
			{
				AnimatedSprite2D animatedSprite = spriteNode.GetComponent<AnimatedSprite2D>();
				animationIndex = (animationIndex + 1) % 7;
				animatedSprite.SetAnimation(AnimationNames[animationIndex], LoopMode2D.LM_FORCE_LOOPED);
			});
	}

	protected override void OnSceneUpdate(float timeStep, Scene scene)
	{
		// Unsubscribe the SceneUpdate event from base class to prevent camera pitch and yaw in 2D sample
	}

	protected override void OnUpdate(float timeStep)
	{
		SimpleMoveCamera2D(timeStep);
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
		camera.OrthoSize=(float)graphics.Height * PixelSize;
		camera.Zoom = (1.5f * Math.Min((float)graphics.Width / 1280.0f, (float)graphics.Height / 800.0f)); // Set zoom according to user's resolution to ensure full visibility (initial zoom (1.5) is set for full visibility at 1280x800 resolution)

		var cache = ResourceCache;
		AnimationSet2D animationSet = cache.GetAnimationSet2D("Urho2D/imp/imp.scml");
		if (animationSet == null)
			return;

		spriteNode = scene.CreateChild("SpriterAnimation");

		AnimatedSprite2D animatedSprite = spriteNode.CreateComponent<AnimatedSprite2D>();
		animatedSprite.SetAnimation(animationSet, AnimationNames[animationIndex], LoopMode2D.LM_DEFAULT);
	}

	protected override string JoystickLayoutPatch => JoystickLayoutPatches.WithZoomInAndOut;
}
