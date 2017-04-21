namespace Playgrounds.WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.urhoSurface = new Urho.Extensions.WinForms.UrhoSurface();
			this.restartButton = new System.Windows.Forms.Button();
			this.removeControlBtn = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.pausedCb = new System.Windows.Forms.CheckBox();
			this.spawnBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// urhoSurface
			// 
			this.urhoSurface.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.urhoSurface.FpsLimit = 60;
			this.urhoSurface.Location = new System.Drawing.Point(12, 61);
			this.urhoSurface.Name = "urhoSurface";
			this.urhoSurface.Paused = false;
			this.urhoSurface.Size = new System.Drawing.Size(922, 550);
			this.urhoSurface.TabIndex = 0;
			// 
			// restartButton
			// 
			this.restartButton.Location = new System.Drawing.Point(12, 18);
			this.restartButton.Name = "restartButton";
			this.restartButton.Size = new System.Drawing.Size(105, 23);
			this.restartButton.TabIndex = 1;
			this.restartButton.Text = "Restart";
			this.restartButton.UseVisualStyleBackColor = true;
			this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
			// 
			// removeControlBtn
			// 
			this.removeControlBtn.Location = new System.Drawing.Point(127, 18);
			this.removeControlBtn.Name = "removeControlBtn";
			this.removeControlBtn.Size = new System.Drawing.Size(105, 23);
			this.removeControlBtn.TabIndex = 2;
			this.removeControlBtn.Text = "Stop";
			this.removeControlBtn.UseVisualStyleBackColor = true;
			this.removeControlBtn.Click += new System.EventHandler(this.removeControlBtn_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(423, 18);
			this.progressBar1.MarqueeAnimationSpeed = 10;
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(512, 23);
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar1.TabIndex = 3;
			// 
			// pausedCb
			// 
			this.pausedCb.AutoSize = true;
			this.pausedCb.Location = new System.Drawing.Point(357, 22);
			this.pausedCb.Name = "pausedCb";
			this.pausedCb.Size = new System.Drawing.Size(62, 17);
			this.pausedCb.TabIndex = 4;
			this.pausedCb.Text = "Paused";
			this.pausedCb.UseVisualStyleBackColor = true;
			this.pausedCb.CheckedChanged += new System.EventHandler(this.pausedCb_CheckedChanged);
			// 
			// spawnBtn
			// 
			this.spawnBtn.Location = new System.Drawing.Point(242, 18);
			this.spawnBtn.Name = "spawnBtn";
			this.spawnBtn.Size = new System.Drawing.Size(105, 23);
			this.spawnBtn.TabIndex = 5;
			this.spawnBtn.Text = "Spawn";
			this.spawnBtn.UseVisualStyleBackColor = true;
			this.spawnBtn.Click += new System.EventHandler(this.spawnBtn_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(946, 623);
			this.Controls.Add(this.spawnBtn);
			this.Controls.Add(this.pausedCb);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.removeControlBtn);
			this.Controls.Add(this.restartButton);
			this.Controls.Add(this.urhoSurface);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		private Urho.Extensions.WinForms.UrhoSurface urhoSurface;
		private System.Windows.Forms.Button restartButton;
		private System.Windows.Forms.Button removeControlBtn;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.CheckBox pausedCb;
		private System.Windows.Forms.Button spawnBtn;
	}
}

