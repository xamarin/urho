using Urho;

class _37_UIDrag : Sample
{
    public _37_UIDrag(Context ctx) : base(ctx) { }

    public override void Start()
    {
        base.Start();
        
        // Set mouse visible
        
        string platform = Urho.Runtime.Platform;
        if (platform != "Android" && platform != "iOS")
            Input.SetMouseVisible(true, false);

        // Create the UI content
        CreateGUI();
        CreateInstructions();

        // Hook up to the frame update events
        SubscribeToEvents();

    }

    private void CreateGUI()
    {
        var cache = ResourceCache;
        UI ui = UI;

        UIElement root = ui.Root;
        // Load the style sheet from xml
        root.SetDefaultStyle(cache.GetXmlFile("UI/DefaultStyle.xml"));

        for (int i = 0; i < 10; i++)
        {
            Button b = new Button(Context);
            root.AddChild(b);
            // Reference a style from the style sheet loaded earlier:
            b.SetStyle("Button", null);
            b.SetSize(300, 100);
            b.Position = new IntVector2(50 * i, 50 * i);

            SubscribeToDragMove(HandleDragMove);
            SubscribeToDragBegin(HandleDragBegin);
            SubscribeToDragCancel(HandleDragCancel);
            SubscribeToDragEnd(HandleDragEnd);

            {
                var t = new Text(Context);
                b.AddChild(t);
                t.SetStyle("Text", null);
                t.HorizontalAlignment = HorizontalAlignment.HA_CENTER;
                t.VerticalAlignment = VerticalAlignment.VA_CENTER;
                t.Name = ("Text");
            }

            {
                var t = new Text(Context);
                b.AddChild(t);
                t.SetStyle("Text", null);
                t.Name=("Event Touch");
                t.HorizontalAlignment=HorizontalAlignment.HA_CENTER;
                t.VerticalAlignment=VerticalAlignment.VA_BOTTOM;
            }

            {
                var t = new Text(Context);
                b.AddChild(t);
                t.SetStyle("Text", null);
                t.Name=("Num Touch");
                t.HorizontalAlignment=HorizontalAlignment.HA_CENTER;
                t.VerticalAlignment=VerticalAlignment.VA_TOP;
            }
        }

        for (int i = 0; i< 10; i++)
        {
            var t = new Text(Context);
            root.AddChild(t);
            t.SetStyle("Text", null);
            t.Name=("Touch "+ i);
            t.SetVisible(false);
        }
    }

    private void CreateInstructions()
    {
        var cache = ResourceCache;
        UI ui = UI;

        // Construct new Text object, set string to display and font to use
        var instructionText = ui.Root.CreateChild<Text>(Text.TypeStatic);
#warning MISSING_API Text::Text
        ////instructionText.Text = ("Drag on the buttons to move them around.\nMulti- button drag also supported.");
        instructionText.SetFont(cache.GetFont("Fonts/Anonymous Pro.ttf"), 15);

        // Position the text relative to the screen center
        instructionText.HorizontalAlignment = HorizontalAlignment.HA_CENTER;
        instructionText.VerticalAlignment = VerticalAlignment.VA_CENTER;
        instructionText.SetPosition(0, ui.Root.Height/4);
    }

    private void SubscribeToEvents()
    {
        SubscribeToUpdate(HandleUpdate);
    }

    private void HandleDragBegin(DragBeginEventArgs args)
    {
        Button element = (Button)args.Element;

        int lx = args.X;
        int ly = args.Y;

        IntVector2 p = element.Position;
#warning MISSING_API UIElement::Var, Text::Text
        ////element.SetVar("START", p);
        ////element.SetVar("DELTA", new IntVector2(p.X - lx, p.Y - ly));

        ////int buttons = args.Buttons;
        ////element.SetVar("BUTTONS", buttons);

        ////var t = (Text)element.GetChild("Text", false);
        ////t.Text="Drag Begin Buttons: " + buttons;

        ////t = (Text)element.GetChild("Num Touch", false);
        ////t.Text="Number of buttons: " + args.NumButtons;
    }

    private void HandleDragMove(DragMoveEventArgs args)
    {
#warning MISSING_API UIElement::Var, Text::Text
        ////Button element = (Button)args.Element;
        ////int buttons = args.Buttons;
        ////IntVector2 d = element.GetVar("DELTA").GetIntVector2();
        ////int X = args.X + d.X;
        ////int Y = args.Y + d.Y;
        ////int BUTTONS = element.GetVar("BUTTONS").GetInt();

        ////var t = (Text)element.GetChild("Event Touch", false);
        ////t.Text = "Drag Move Buttons: " + buttons;

        ////if (buttons == BUTTONS)
        ////    element.Position=new IntVector2(X, Y);
    }

    private void HandleDragCancel(DragCancelEventArgs args)
    {
        Button element = (Button)args.Element;
#warning MISSING_API UIElement::Var
        ////IntVector2 P = element.GetVar("START").GetIntVector2();
        ////element.Position=(P);
    }

    private void HandleDragEnd(DragEndEventArgs args)
    {
        Button element = (Button)args.Element;
    }

    private void HandleUpdate(UpdateEventArgs args)
    {
        UI ui = UI;
        UIElement root = ui.Root;

        Input input = Input;

        uint n = input.NumTouches;
        for (uint i = 0; i < n; i++)
        {
#warning MISSING_API Input::GetTouch, Text::Text
            ////var t = (Text)root.GetChild("Touch " + i, false);
            ////TouchState ts = input.GetTouch(i);
            ////t.Text = "Touch " + ts.touchID_;

            ////IntVector2 pos = ts.position_;
            ////pos.Y -= 30;

            ////t.Position = (pos);
            ////t.SetVisible(true);
        }

        for (uint i = n; i < 10; i++)
        {
            var t = (Text)root.GetChild("Touch " + i, false);
            t.SetVisible(false);
        }
    }

}
