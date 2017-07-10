using Urho;
using Urho.Gui;

namespace Playgrounds.Console
{
	class Bug222 : SimpleApplication
	{
		public Bug222(ApplicationOptions options) : base(options) { }

		public static void RunApp()
		{
			var app = new Bug222(new ApplicationOptions());
			app.Run();
		}

		protected override void Start()
		{
			base.Start();

			// UI
			var coinsText = new Text();
			coinsText.Value = "https://github.com/xamarin/urho/issues/222";
			coinsText.HorizontalAlignment = HorizontalAlignment.Right;
			coinsText.SetFont(CoreAssets.Fonts.AnonymousPro, Graphics.Width / 50);
			coinsText.SetColor(Color.Red);
			UI.Root.AddChild(coinsText);
			Input.SetMouseVisible(true, false);

			// Crashes the app:
			bool isInside = coinsText.IsInside(new IntVector2(111, 111), true);
		}
	}
}
