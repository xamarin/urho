using Urho;
using Urho.Forms;
using Xamarin.Forms;

namespace Playgrounds.Forms
{
	public class App : Xamarin.Forms.Application
	{
		public App()
		{
			MainPage = new NavigationPage(new StartPage { });
		}

		protected override void OnResume()
		{
			UrhoSurface.OnResume();
			base.OnResume();
		}

		protected override void OnSleep()
		{
			UrhoSurface.OnPause();
			base.OnSleep();
		}
	}

	public class StartPage : ContentPage
	{
		public StartPage()
		{
			var b = new Button { Text = "Launch sample" };
			b.Clicked += (sender, e) => Navigation.PushAsync(new UrhoPage());
			Content = new StackLayout { Children = { b }, VerticalOptions = LayoutOptions.Center };
		}
	}


	public class UrhoPage : ContentPage
	{
		UrhoSurface urhoSurface;
		Charts urhoApp;
		Slider selectedBarSlider;

		public UrhoPage()
		{
			var restartBtn = new Button { Text = "Restart" };
			restartBtn.Clicked += (sender, e) => StartUrhoApp();

			bool paused = false;
			var pauseBtn = new Button { Text = "(Un)pause" };
			pauseBtn.HorizontalOptions = LayoutOptions.EndAndExpand;
			pauseBtn.Clicked += (sender, e) =>
			{
				paused = !paused;
				if (paused)
					UrhoSurface.OnPause();
				else
					UrhoSurface.OnResume();
			};

			urhoSurface = new UrhoSurface();
			urhoSurface.VerticalOptions = LayoutOptions.FillAndExpand;

			Slider rotationSlider = new Slider(0, 500, 250);
			rotationSlider.ValueChanged += (s, e) =>
				{
					if (urhoApp?.IsActive == true)
						Urho.Application.InvokeOnMain(() => urhoApp.Rotate((float)(e.NewValue - e.OldValue)));
				};

			selectedBarSlider = new Slider(0, 5, 2.5);
			selectedBarSlider.ValueChanged += OnValuesSliderValueChanged;

			Title = " UrhoSharp + Xamarin.Forms";
			Content = new StackLayout
			{
				Padding = new Thickness(12, 12, 12, 40),
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					urhoSurface,
					new StackLayout {
						Orientation = StackOrientation.Horizontal,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Children = {
							restartBtn,
							pauseBtn
						}
					},
					new Label { Text = "ROTATION::" },
					rotationSlider,
					new Label { Text = "SELECTED VALUE:" },
					selectedBarSlider,
				}
			};
		}

		protected override void OnDisappearing()
		{
			UrhoSurface.OnDestroy();
			base.OnDisappearing();
		}

		void OnValuesSliderValueChanged(object sender, ValueChangedEventArgs e)
		{
			if (urhoApp?.SelectedBar != null)
				Urho.Application.InvokeOnMain(() => urhoApp.SelectedBar.Value = (float)e.NewValue);
		}

		private void OnBarSelection(Bar bar)
		{
			//reset value
			selectedBarSlider.ValueChanged -= OnValuesSliderValueChanged;
			selectedBarSlider.Value = bar.Value;
			selectedBarSlider.ValueChanged += OnValuesSliderValueChanged;
		}

		protected override async void OnAppearing()
		{
			StartUrhoApp();
		}

		async void StartUrhoApp()
		{
			urhoApp = await urhoSurface.Show<Charts>(new ApplicationOptions(assetsFolder: null) { Orientation = ApplicationOptions.OrientationType.LandscapeAndPortrait });
		}
	}
}
