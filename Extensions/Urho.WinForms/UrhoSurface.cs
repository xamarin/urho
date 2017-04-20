using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Urho.WinForms
{
	[ToolboxItem(true)]
	public partial class UrhoSurface: UserControl
	{
		static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1);
		bool paused;

		public UrhoSurface()
		{
			InitializeComponent();
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			if (Application != null && !Application.IsActive)
				Application.Exit();
			Application = null;
			base.OnHandleDestroyed(e);
		}

		public Application Application { get; private set; }

		public Panel UnderlyingPanel { get; private set; }

		public bool Paused
		{
			get { return paused; }
			set
			{
				if (paused && !value)
				{
					paused = value;
					StartLoop(Application);
				}
				else
					paused = value;
			}
		}

		public int FpsLimit { get; set; } = 60;

		public static Color ConvertColor(System.Drawing.Color color)
		{
			return new Color(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
		}
		
		public async Task<TApplication> Show<TApplication>(ApplicationOptions opts = null) where TApplication : Application
		{
			return (TApplication)(await Show(typeof(TApplication), opts));
		}

		public async Task<Application> Show(Type appType, ApplicationOptions opts = null)
		{
			opts = opts ?? new ApplicationOptions();
			await Semaphore.WaitAsync();
			if (Application.HasCurrent)
				await Application.Current.Exit();
			await Task.Yield();
			Controls.Clear();
			UnderlyingPanel = new Panel { Dock = DockStyle.Fill };
			Controls.Add(UnderlyingPanel);
			UnderlyingPanel.Focus();

			opts.ExternalWindow = UnderlyingPanel.Handle;
			opts.DelayedStart = true;
			opts.LimitFps = false;
			var app = Application.CreateInstance(appType, opts);
			Application = app;
			app.Run();
			Semaphore.Release();
			StartLoop(app);
			return app;
		}

		async void StartLoop(Application app)
		{
			while (!Paused && app != null && app.IsActive)
			{
				var elapsed = app.Engine.RunFrame();
				var targetMax = 1000000L / FpsLimit;
				if (elapsed >= targetMax)
					await Task.Yield();
				else
				{
					var ts = TimeSpan.FromMilliseconds((targetMax - elapsed) / 1000d);
					await Task.Delay(ts);
				}
				System.Windows.Forms.Application.DoEvents();
			}
		}
	}
}
