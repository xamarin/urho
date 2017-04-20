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

		public int FpsLimit { get; set; } = 60;

		public static Color ConvertColor(System.Drawing.Color color)
		{
			return new Color(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
		}
		
		public Task Show<TApplication>(ApplicationOptions opts = null)
		{
			return Show(typeof(TApplication), opts);
		}

		public async Task Show(Type appType, ApplicationOptions opts = null)
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
			while (app != null && app.IsActive)
			{
				app?.Engine?.RunFrame();
				//TODO: RunFrame should return time taken to render
				await Task.Delay(TimeSpan.FromMilliseconds(1000d / FpsLimit));
			}
		}
	}
}
