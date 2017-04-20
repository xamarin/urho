using System;
using System.Windows.Forms;

namespace Playgrounds.WinForms
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		void restartButton_Click(object sender, EventArgs e)
		{
			urhoSurface.Show<Game>(new Urho.ApplicationOptions());
		}

		private void removeControlBtn_Click(object sender, EventArgs e)
		{
			this.Controls.Remove(urhoSurface);
		}
	}
}
