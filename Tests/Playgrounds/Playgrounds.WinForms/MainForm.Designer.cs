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
			this.urhoSurface = new Urho.WinForms.UrhoSurface();
			this.restartButton = new System.Windows.Forms.Button();
			this.removeControlBtn = new System.Windows.Forms.Button();
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
			this.urhoSurface.Size = new System.Drawing.Size(573, 411);
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
			this.removeControlBtn.Text = "Remove control";
			this.removeControlBtn.UseVisualStyleBackColor = true;
			this.removeControlBtn.Click += new System.EventHandler(this.removeControlBtn_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(597, 484);
			this.Controls.Add(this.removeControlBtn);
			this.Controls.Add(this.restartButton);
			this.Controls.Add(this.urhoSurface);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.ResumeLayout(false);

        }

		#endregion

		private Urho.WinForms.UrhoSurface urhoSurface;
		private System.Windows.Forms.Button restartButton;
		private System.Windows.Forms.Button removeControlBtn;
	}
}

