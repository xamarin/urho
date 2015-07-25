using System.Collections.Generic;
using Urho;

class _03_Sprites : Sample
{
    private Scene scene;
    private bool drawDebug;
    private List<Sprite> sprites_;
    private Camera camera;
    // Number of sprites to draw
    private const uint NUM_SPRITES = 100;

    public _03_Sprites(Context ctx) : base(ctx) { }

    public override void Start()
    {
        base.Start();
        CreateSprites();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        SubscribeToUpdate(args =>
            {
                MoveSprites(args.TimeStep);
            });
    }

    private void CreateSprites()
    {
        var cache = ResourceCache;
        var graphics = Graphics;
        UI ui = UI;

        // Get the Urho3D fish texture
        Texture2D decalTex = cache.GetTexture2D("Textures/UrhoDecal.dds");

        for (uint i = 0; i < NUM_SPRITES; ++i)
        {
            // Create a new sprite, set it to use the texture
            Sprite sprite=new Sprite(Context);
            sprite.Texture=decalTex;

            // The UI root element is as big as the rendering window, set random position within it
            sprite.Position=new IntVector2((int)NextRandom() * graphics.Width, (int)NextRandom() * graphics.Height);

            // Set sprite size & hotspot in its center
            sprite.Size=new IntVector2(128, 128);
            sprite.HotSpot=new IntVector2(64, 64);

            // Set random rotation in degrees and random scale
            sprite.Rotation=NextRandom() * 360.0f;
            sprite.SetScale(NextRandom(1.0f) + 0.5f);

            // Set random color and additive blending mode
            sprite.SetColor(new Color(NextRandom(0.5f) + 0.5f, NextRandom(0.5f) + 0.5f, NextRandom(0.5f) + 0.5f));
            sprite.BlendMode= BlendMode.BLEND_ADD;

            // Add as a child of the root UI element
            ui.Root.AddChild(sprite);

            // Store sprite's velocity as a custom variable
#warning MISSING_API SetVar
            //sprite.SetVar(VAR_VELOCITY, new Vector2(NextRandom(200.0f) - 100.0f, NextRandom(200.0f) - 100.0f));

            // Store sprites to our own container for easy movement update iteration
            sprites_.Add(sprite);
        }
    }

    private void MoveSprites(float timeStep)
    {
        var graphics = Graphics;
        float width = (float)graphics.Width;
        float height = (float)graphics.Height;

        // Go through all sprites
        for (int i = 0; i < sprites_.Count; ++i)
        {
            Sprite sprite = sprites_[i];

            // Rotate
            float newRot = sprite.Rotation + timeStep * 30.0f;
            sprite.Rotation=newRot;

            // Move, wrap around rendering window edges
#warning MISSING_API GetVar
            //IntVector2 newPos = spritePosition + sprite.GetVar(VAR_VELOCITY).GetVector2() * timeStep;
            //if (newPos.X < 0.0f)
            //    newPos.X += width;
            //if (newPos.X >= width)
            //    newPos.X -= width;
            //if (newPos.Y < 0.0f)
            //    newPos.Y += height;
            //if (newPos.Y >= height)
            //    newPos.Y -= height;
            //sprite.Position = (newPos);
        }
    }

}
