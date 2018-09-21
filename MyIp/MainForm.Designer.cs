namespace MyIp
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
            this.components = new System.ComponentModel.Container();
            this.outLabel = new System.Windows.Forms.Label();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // outLabel
            // 
            this.outLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outLabel.Font = new System.Drawing.Font("Consolas", 7.5F);
            this.outLabel.Location = new System.Drawing.Point(0, 0);
            this.outLabel.Margin = new System.Windows.Forms.Padding(0);
            this.outLabel.Name = "outLabel";
            this.outLabel.Padding = new System.Windows.Forms.Padding(5);
            this.outLabel.Size = new System.Drawing.Size(300, 58);
            this.outLabel.TabIndex = 0;
            this.outLabel.Text = "Loading...";
            this.outLabel.Click += new System.EventHandler(this.OnClick);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Interval = 120000;
            this.refreshTimer.Tick += new System.EventHandler(this.OnRefresh);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(300, 58);
            this.ControlBox = false;
            this.Controls.Add(this.outLabel);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Opacity = 0.5D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Shown += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label outLabel;
        private System.Windows.Forms.Timer refreshTimer;
    }
}

