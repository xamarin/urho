using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Urho.Extensions.Wpf
{
	public partial class UrhoSurface : UserControl
	{
		public UrhoSurface()
		{
			InitializeComponent();
			GotFocus += UrhoSurface_GotFocus;
			LostFocus += UrhoSurface_LostFocus;
		}

		void UrhoSurface_LostFocus(object sender, RoutedEventArgs e)
		{
			if (WfUrhoSurface != null)
				WfUrhoSurface.ForceFocus = false;
		}

		void UrhoSurface_GotFocus(object sender, RoutedEventArgs e)
		{
			if (WfUrhoSurface != null)
				WfUrhoSurface.ForceFocus = true;
		}

		void UserControl_Loaded(object sender, RoutedEventArgs e) {}

		public Application Application => WfUrhoSurface.Application;

		public System.Windows.Forms.Panel UnderlyingPanel => WfUrhoSurface.UnderlyingPanel;

		public bool Paused
		{
			set { WfUrhoSurface.Paused = value; }
			get { return WfUrhoSurface.Paused; }
		}

		public int FpsLimit
		{
			set { WfUrhoSurface.FpsLimit = value; }
			get { return WfUrhoSurface.FpsLimit; }
		}

		public Task<Application> Show(Type appType, ApplicationOptions opts = null)
		{
			return WfUrhoSurface.Show(appType, opts);
		}

		public Task<TApplication> Show<TApplication>(ApplicationOptions opts = null) 
			where TApplication : Application
		{
			return WfUrhoSurface.Show<TApplication>(opts);
		}

		public void Stop() => WfUrhoSurface.Stop();

		public static Urho.Color ConvertColor(System.Windows.Media.Color color)
		{
			return new Urho.Color(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
		}
	}
}
