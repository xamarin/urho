using Urho;

class _01_HelloWorld : Sample
{
    void CreateText()
    {
        var cache = ResourceCache;
        var helloText = new Text(Context)
            {
                Value = "Hello World from Urho3D + Mono",
                //Color = new Color (0, 1, 0),
                HorizontalAlignment = HorizontalAlignment.HA_CENTER,
                VerticalAlignment = VerticalAlignment.VA_CENTER
            };
        helloText.SetColor(new Color(0f, 1f, 0f));
        helloText.SetFont(cache.GetFont("Fonts/Anonymous Pro.ttf"), 30);
        UI.Root.AddChild(helloText);
    }

    public override void Start()
    {
        // Execute base class startup
        base.Start();

        // Create "Hello World" Text
        CreateText();

        // Finally subscribe to the update event. Note that by subscribing events at this point we have already missed some events
        // like the ScreenMode event sent by the Graphics subsystem when opening the application window. To catch those as well we
        // could subscribe in the constructor instead.
        SubscribeToUpdate(args =>
            {
                // Do nothing for now, could be extended to eg. animate the display
            });
    }

    public _01_HelloWorld(Context c) : base(c) { }
}

