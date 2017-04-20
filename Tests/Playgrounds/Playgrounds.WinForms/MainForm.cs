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
			game = await urhoSurface.Show<Game>(new Urho.ApplicationOptions());
		}

		private void removeControlBtn_Click(object sender, EventArgs e)
		{
			game?.Exit();
		}

		void pausedCb_CheckedChanged(object sender, EventArgs e)
		{
			urhoSurface.Paused = pausedCb.Checked;
		}
	}
}
