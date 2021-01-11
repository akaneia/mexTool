
namespace mexTool.GUI.Controls
{
    partial class MxMusicPlayer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelNowPlaying = new System.Windows.Forms.Label();
            this.soundProgressBar = new mexTool.GUI.Controls.MXProgressBar();
            this.buttonStop = new mexTool.GUI.Controls.MXButton();
            this.buttonPause = new mexTool.GUI.Controls.MXButton();
            this.buttonPlay = new mexTool.GUI.Controls.MXButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(287, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "0:00 / 0:00";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 24;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelNowPlaying
            // 
            this.labelNowPlaying.AutoSize = true;
            this.labelNowPlaying.Location = new System.Drawing.Point(106, 6);
            this.labelNowPlaying.Name = "labelNowPlaying";
            this.labelNowPlaying.Size = new System.Drawing.Size(69, 13);
            this.labelNowPlaying.TabIndex = 4;
            this.labelNowPlaying.Text = "Now Playing:";
            // 
            // soundProgressBar
            // 
            this.soundProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.soundProgressBar.BackColor = System.Drawing.Color.Transparent;
            this.soundProgressBar.BarBackColor = System.Drawing.Color.White;
            this.soundProgressBar.BarLineColor = System.Drawing.Color.Black;
            this.soundProgressBar.BarProgressColor = System.Drawing.Color.LightGray;
            this.soundProgressBar.Location = new System.Drawing.Point(3, 49);
            this.soundProgressBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.soundProgressBar.Maximum = 100;
            this.soundProgressBar.Minimum = 0;
            this.soundProgressBar.Name = "soundProgressBar";
            this.soundProgressBar.Size = new System.Drawing.Size(444, 20);
            this.soundProgressBar.TabIndex = 3;
            this.soundProgressBar.TinyHeight = 10;
            this.soundProgressBar.Value = 0;
            this.soundProgressBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.soundProgressBar_MouseDown);
            this.soundProgressBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.soundProgressBar_MouseMove);
            this.soundProgressBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.soundProgressBar_MouseUp);
            // 
            // buttonStop
            // 
            this.buttonStop.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.buttonStop.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.buttonStop.BackFillColor = System.Drawing.Color.Transparent;
            this.buttonStop.BorderColor = System.Drawing.Color.Black;
            this.buttonStop.CornerRadius = 20;
            this.buttonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStop.Image = global::mexTool.Properties.Resources.stop;
            this.buttonStop.ImageHeight = 32;
            this.buttonStop.ImageWidth = 32;
            this.buttonStop.Location = new System.Drawing.Point(60, 6);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(40, 40);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.buttonPause.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.buttonPause.BackFillColor = System.Drawing.Color.Transparent;
            this.buttonPause.BorderColor = System.Drawing.Color.Black;
            this.buttonPause.CornerRadius = 20;
            this.buttonPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPause.Image = global::mexTool.Properties.Resources.pause;
            this.buttonPause.ImageHeight = 32;
            this.buttonPause.ImageWidth = 32;
            this.buttonPause.Location = new System.Drawing.Point(14, 6);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(40, 40);
            this.buttonPause.TabIndex = 1;
            this.buttonPause.Visible = false;
            this.buttonPause.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.buttonPlay.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.buttonPlay.BackFillColor = System.Drawing.Color.Transparent;
            this.buttonPlay.BorderColor = System.Drawing.Color.Black;
            this.buttonPlay.CornerRadius = 20;
            this.buttonPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPlay.Image = global::mexTool.Properties.Resources.play;
            this.buttonPlay.ImageHeight = 32;
            this.buttonPlay.ImageWidth = 32;
            this.buttonPlay.Location = new System.Drawing.Point(14, 6);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(40, 40);
            this.buttonPlay.TabIndex = 1;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // MxMusicPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.labelNowPlaying);
            this.Controls.Add(this.soundProgressBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonPlay);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "MxMusicPlayer";
            this.Size = new System.Drawing.Size(450, 91);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MxMusicPlayer_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MXButton buttonPlay;
        private System.Windows.Forms.Label label1;
        private MXButton buttonStop;
        private MXButton buttonPause;
        private MXProgressBar soundProgressBar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelNowPlaying;
    }
}
