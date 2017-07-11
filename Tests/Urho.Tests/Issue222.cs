using System.Threading.Tasks;
using NUnit.Framework;
using Urho.Gui;
using Urho.Tests.Bootstrap;
using Text = Urho.Gui.Text;

namespace Urho.Tests
{
	[TestFixture]
	public class Issue222
	{
		[Test]
		public async Task Issue222_Run()
		{
			TestApp.RunSimpleApp(async app =>
				{
					// UI
					var coinsText = new Text();
					coinsText.Value = "https://github.com/xamarin/urho/issues/222";
					coinsText.HorizontalAlignment = HorizontalAlignment.Right;
					coinsText.SetFont(CoreAssets.Fonts.AnonymousPro, app.Graphics.Width / 50);
					coinsText.SetColor(Color.Red);
					app.UI.Root.AddChild(coinsText);
					app.Input.SetMouseVisible(true);

					await app.Delay(0.1f);

					// Crashes the app:
					bool isInside = coinsText.IsInside(new IntVector2(111, 111), true);

					await app.Exit();
				});
		}
	}
}
