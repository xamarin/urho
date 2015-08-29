using System.Collections.Generic;
using Urho;

class _03_Sprites : Sample
{
	private readonly Dictionary<Sprite, Vector2> spritesWithVelocities = new Dictionary<Sprite, Vector2>();
	// Number of sprites to draw
	private const uint NumSprites = 100;

	public _03_Sprites(Context ctx) : base(ctx) { }

	public override void Start()
	{
		base.Start();
		CreateSprites();
	}

	protected override void OnUpdate(float timeStep)
	{
		MoveSprites(timeStep);
	}

	private void CreateSprites()
	{
		var cache = ResourceCache;
		var graphics = Graphics;
		UI ui = UI;

		// Get the Urho3D fish texture
		Texture2D decalTex = cache.GetTexture2D("Textures/UrhoDecal.dds");

		for (uint i = 0; i < NumSprites; ++i)
		{
			// Create a new sprite, set it to use the texture
			Sprite sprite=new Sprite(Context);
			sprite.Texture=decalTex;

			// The UI root element is as big as the rendering window, set random position within it
			sprite.Position=new IntVector2((int)(NextRandom() * graphics.Width), (int)(NextRandom() * graphics.Height));

			// Set sprite size & hotspot in its center
			sprite.Size=new IntVector2(128, 128);
			sprite.HotSpot=new IntVector2(64, 64);

			// Set random rotation in degrees and random scale
			sprite.Rotation=NextRandom() * 360.0f;
			sprite.SetScale(NextRandom(1.0f) + 0.5f);

			// Set random color and additive blending mode
			sprite.SetColor(new Color(NextRandom(0.5f) + 0.5f, NextRandom(0.5f) + 0.5f, NextRandom(0.5f) + 0.5f));
			sprite.BlendMode= BlendMode.Add;

			// Add as a child of the root UI element
			ui.Root.AddChild(sprite);

			// Store sprites to our own container for easy movement update iteration
			spritesWithVelocities[sprite] = new Vector2(NextRandom(200.0f) - 100.0f, NextRandom(200.0f) - 100.0f);
		}
	}

	private void MoveSprites(float timeStep)
	{
		var graphics = Graphics;
		int width = graphics.Width;
		int height = graphics.Height;

		// Go through all sprites

		foreach (var item in spritesWithVelocities)
		{
			var sprite = item.Key;
			var vector = item.Value;

			// Rotate
			float newRot = sprite.Rotation + timeStep * 30.0f;
			sprite.Rotation=newRot;

			var x = vector.X * timeStep + sprite.Position.X;
			var y = vector.Y * timeStep + sprite.Position.Y;

			if (x < 0.0f)
				x += width;
			if (x >= width)
				x -= width;
			if (y < 0.0f)
				y += height;
			if (y >= height)
				y -= height;

			sprite.Position = new IntVector2((int) x, (int) y);
		}
	}
}
