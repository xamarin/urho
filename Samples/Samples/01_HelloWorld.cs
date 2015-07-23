using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;


class _01_HelloWorld : Sample
{
    void CreateText()
    {
        var c = ResourceCache;
        var t = new Text(Context)
        {
            Value = "Hello World from Urho3D + Mono",
            //Color = new Color (0, 1, 0),
            HorizontalAlignment = HorizontalAlignment.HA_CENTER,
            VerticalAlignment = VerticalAlignment.VA_CENTER
        };
        t.SetFont(c.GetFont("Fonts/Anonymous Pro.ttf"), 30);
        UI.Root.AddChild(t);

    }

    public override void Start()
    {
        base.Start();
        CreateText();
    }

    public _01_HelloWorld(Context c) : base(c) { }
}

