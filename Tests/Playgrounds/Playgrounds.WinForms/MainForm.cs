using System;
using System.Windows.Forms;

namespace Playgrounds.WinForms
{
	public partial class MainForm : Form
	{
		Game game;

		public MainForm()
		{
			InitializeComponent();
		}

		async void restartButton_Click(object sender, EventArgs e)
		{
			game = await urhoSurface.Show<Game>(new Urho.ApplicationOptions() { UseDirectX11 = true });
			if (game.IsActive) //in case if user clicks "restart" too quickly
				game.Viewport.SetClearColor(Urho.Extensions.WinForms.UrhoSurface.ConvertColor(urhoSurface.BackColor));
		}

		void removeControlBtn_Click(object sender, EventArgs e)
		{
			urhoSurface.Stop();
			//or just:
			//game?.Exit();
		}

		void pausedCb_CheckedChanged(object sender, EventArgs e)
		{
			urhoSurface.Paused = pausedCb.Checked;
		}

		void spawnBtn_Click(object sender, EventArgs e)
		{
			if (game == null || !game.IsActive)
				return;

			Game.InvokeOnMain(() =>
				{
					for (int i = 0; i < 10; i++)
					{
						game.SpawnRandomShape();
					}
				});
		}
	}
}
