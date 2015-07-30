using System.Collections.Generic;
using Urho;

class _37_UIDrag : Sample
{
    public _37_UIDrag(Context ctx) : base(ctx) { }

    private Dictionary<UIElement, ElementInfo> elements; 

    public override void Start()
    {
        base.Start();
        
        elements = new Dictionary<UIElement, ElementInfo>();
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
        var instructionText = new Text(Context);
        instructionText.Value = "Drag on the buttons to move them around.\nMulti- button drag also supported.";
        instructionText.SetFont(cache.GetFont("Fonts/Anonymous Pro.ttf"), 15);
        ui.Root.AddChild(instructionText);

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
        elements[element] = new ElementInfo(element, p, new IntVector2(p.X - lx, p.Y - ly), args.Buttons);

        int buttons = args.Buttons;

        var t = (Text)element.GetChild("Text", false);
        t.Value = "Drag Begin Buttons: " + buttons;

        t = (Text)element.GetChild("Num Touch", false);
        t.Value = "Number of buttons: " + args.NumButtons;
    }

    private void HandleDragMove(DragMoveEventArgs args)
    {
        var element = elements[args.Element];
        int buttons = args.Buttons;
        IntVector2 d = element.Delta;
        int x = args.X + d.X;
        int y = args.Y + d.Y;
        var t = (Text)element.Element.GetChild("Event Touch", false);
        t.Value = "Drag Move Buttons: " + buttons;

        if (buttons == element.Buttons)
            element.Element.Position = new IntVector2(x, y);
    }

    private void HandleDragCancel(DragCancelEventArgs args)
    {
        args.Element.Position = elements[args.Element].Start;
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
            var text = (Text)root.GetChild("Touch " + i, false);
            TouchState ts;
            input.TryGetTouch(i, out ts);
            text.Value = "Touch " + ts.TouchID;

            IntVector2 pos = ts.Position;
            pos.Y -= 30;

            text.Position = (pos);
            text.SetVisible(true);
        }

        for (uint i = n; i < 10; i++)
        {
            var text = (Text)root.GetChild("Touch " + i, false);
            text.SetVisible(false);
        }
    }

    class ElementInfo
    {
        public UIElement Element { get; set; }
        public IntVector2 Start { get; set; }
        public IntVector2 Delta { get; set; }
        public int Buttons { get; set; }

        public ElementInfo(UIElement element, IntVector2 start, IntVector2 delta, int buttons)
        {
            Element = element;
            Start = start;
            Delta = delta;
            Buttons = buttons;
        }
    }

}
