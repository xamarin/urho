using Urho;

public class _01_HelloWorld : Application
{
	public _01_HelloWorld(Context c) : base(c) { }

	public override void Start()
	{
		var cache = ResourceCache;
		var helloText = new Text(Context)
		{
			Value = "Hello World from Urho3D + Mono",
			HorizontalAlignment = HorizontalAlignment.HA_CENTER,
			VerticalAlignment = VerticalAlignment.VA_CENTER
		};
		helloText.SetColor(new Color(0f, 1f, 0f));
		helloText.SetFont(cache.GetFont("Fonts/Anonymous Pro.ttf"), 30);
		UI.Root.AddChild(helloText);
	}
}
