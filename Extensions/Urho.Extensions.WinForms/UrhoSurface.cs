using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace Urho.Extensions.WinForms
{
#if !WPF
	[System.ComponentModel.ToolboxItem(true)]
	public
#endif
	partial class UrhoSurface: UserControl
	{
		static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1);
		bool paused;

		public static Thread RenderThread { get; private set; }

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
			get => paused;
			set => paused = value;
		}

		async void Focuse()
		{
			await Task.Delay(1000);
			UnderlyingPanel.Focus();
		}

		public bool ForceFocus { get; set; }

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
			paused = false;
			opts = opts ?? new ApplicationOptions();
			await Semaphore.WaitAsync();

			if (Application.HasCurrent)
				Application.InvokeOnMain(async () => {
				if (Application.HasCurrent)
					await Application.Current.Exit();
				});

			RenderThread?.Join();

			if (UnderlyingPanel != null)
				UnderlyingPanel.MouseDown -= UrhoSurface_MouseDown;
			Controls.Clear();

			UnderlyingPanel = new Panel { Dock = DockStyle.Fill };
			UnderlyingPanel.MouseDown += UrhoSurface_MouseDown;
			Controls.Add(UnderlyingPanel);
			UnderlyingPanel.Visible = false;

			opts.ExternalWindow = UnderlyingPanel.Handle;
			opts.DelayedStart = true;
			opts.LimitFps = false;

			var mre = new ManualResetEvent(false);
			Urho.Application.Started += () => { mre.Set(); };

			RenderThread = new Thread(_ =>
				{
					Application = Application.CreateInstance(appType, opts);
					Application.Run();
					var sw = new Stopwatch();

					var engine = Application.Engine;
					engine.RunFrame();
					while (Application != null && Application.IsActive)
					{
						if (!Paused)
						{
							sw.Restart();
							engine.RunFrame();
							sw.Stop();

							var elapsed = sw.Elapsed.TotalMilliseconds;
							var targetMax = 1000f / FpsLimit;
							if (elapsed < targetMax)
								Thread.Sleep(TimeSpan.FromMilliseconds(targetMax - elapsed));
						}
						else
						{
							Thread.Sleep(500);
						}
					}
				});
			RenderThread.Start();
			mre.WaitOne();
			Semaphore.Release();
			UnderlyingPanel.Visible = true;
			UnderlyingPanel.Focus();
			return Application;
		}
		
		public void Stop()
		{
			Application.InvokeOnMain(async () => {
				if (Application.HasCurrent)
					await Application.Current.Exit();
			});

			RenderThread?.Join();
			Controls.Clear();
		}

		void UrhoSurface_MouseDown(object sender, MouseEventArgs e)
		{
			UnderlyingPanel.Focus();
		}
	}
}
