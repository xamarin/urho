using Urho;
using Urho.Gui;
using Urho.Resources;

namespace Playgrounds.Console
{
	public class Bug219 : SimpleApplication
	{
		public Bug219(ApplicationOptions options) : base(options) { }

		public static void RunApp()
		{
			var app = new Bug219(new ApplicationOptions());
			app.Run();
		}

		protected override async void Start()
		{
			base.Start();
			Input.SetMouseVisible(true, false);
			XmlFile style = ResourceCache.GetXmlFile("UI/DefaultStyle.xml");
			UI.Root.SetDefaultStyle(style);

			while (IsActive)
			{
				// reload UI each 5 seconds:
				Reload();
				await Delay(5);
			}
		}

		void Reload()
		{
			// clear UI.Root
			UI.Root.RemoveAllChildren();

			const int count = 8;

			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < count; j++)
				{
					var w = Graphics.Width / count;
					var h = Graphics.Height / count;

					var button = new Button();
					UI.Root.AddChild(button);
					button.SetStyle("Button");
					button.SetSize(w, h);
					button.Position = new IntVector2(w * i, h * j);

					var label = new Text();
					button.AddChild(label);
					label.SetStyle("Text");
					label.HorizontalAlignment = HorizontalAlignment.Center;
					label.VerticalAlignment = VerticalAlignment.Center;
					label.Value = $"{i};{j}";

					//button.Pressed += Button_Pressed;
					button.Pressed += args => label.Value += "!";
				}
			}
		}

		void Button_Pressed(PressedEventArgs obj)
		{
			var label = (Text)obj.Element.Children[0];
			label.Value += "!";
		}
	}
}
