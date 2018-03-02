using System.Windows;
using System.Windows.Media;

namespace Playgrounds.Wpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Game app;

		public MainWindow()
		{
			InitializeComponent();
		}

		async void RestartBtn_Click(object sender, RoutedEventArgs e)
		{
			app = await UrhoSurface.Show<Game>(new Urho.ApplicationOptions() { UseDirectX11 = true });
			if (app.IsActive) //in case if user clicks "restart" too quickly
				app.Viewport.SetClearColor(Urho.Extensions.Wpf.UrhoSurface.ConvertColor(((SolidColorBrush)Background).Color));
		}

		void StopBtn_Click(object sender, RoutedEventArgs e)
		{
			UrhoSurface.Stop();
			//or just:
			//app?.Exit();
		}

		void PausedCb_Checked(object sender, RoutedEventArgs e)
		{
			UrhoSurface.Paused = PausedCb.IsChecked == true;
		}

		void SpawnBtn_Click(object sender, RoutedEventArgs e)
		{
			if (app == null || !app.IsActive)
				return;

			// required!
			Game.InvokeOnMain(() =>
				{
					for (int i = 0; i < 10; i++)
					{
						app.SpawnRandomShape();
					}
				});
		}
	}
}
