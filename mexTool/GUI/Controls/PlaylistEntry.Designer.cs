
namespace mexTool.GUI.Controls
{
    partial class PlaylistEntry
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.playButton = new mexTool.GUI.Controls.MXButton();
            this.deleteButton = new mexTool.GUI.Controls.MXButton();
            this.mxProgressBar1 = new mexTool.GUI.Controls.MXProgressBar();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(50)))));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.ForeColor = System.Drawing.Color.White;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(127, 11);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(333, 32);
            this.comboBox1.Sorted = true;
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.Resize += new System.EventHandler(this.comboBox1_Resize);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(20, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Rarely";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(417, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Often";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // playButton
            // 
            this.playButton.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.playButton.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.playButton.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.playButton.BorderColor = System.Drawing.Color.Black;
            this.playButton.ForeColor = System.Drawing.Color.White;
            this.playButton.Image = global::mexTool.Properties.Resources.play;
            this.playButton.ImageHeight = 32;
            this.playButton.ImageWidth = 32;
            this.playButton.Location = new System.Drawing.Point(71, 11);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(50, 32);
            this.playButton.TabIndex = 1;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.deleteButton.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.deleteButton.BackFillColor = System.Drawing.Color.DarkRed;
            this.deleteButton.BorderColor = System.Drawing.Color.Black;
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Image = global::mexTool.Properties.Resources.minus;
            this.deleteButton.ImageHeight = 32;
            this.deleteButton.ImageWidth = 32;
            this.deleteButton.Location = new System.Drawing.Point(16, 11);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(50, 32);
            this.deleteButton.TabIndex = 1;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // mxProgressBar1
            // 
            this.mxProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mxProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.mxProgressBar1.BarBackColor = System.Drawing.Color.DimGray;
            this.mxProgressBar1.BarLineColor = System.Drawing.Color.MintCream;
            this.mxProgressBar1.BarProgressColor = System.Drawing.Color.Aqua;
            this.mxProgressBar1.Location = new System.Drawing.Point(71, 51);
            this.mxProgressBar1.LoopPosition = 0;
            this.mxProgressBar1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mxProgressBar1.Maximum = 100;
            this.mxProgressBar1.Minimum = 0;
            this.mxProgressBar1.Name = "mxProgressBar1";
            this.mxProgressBar1.Size = new System.Drawing.Size(340, 24);
            this.mxProgressBar1.TabIndex = 0;
            this.mxProgressBar1.TinyHeight = 24;
            this.mxProgressBar1.Value = 50;
            this.mxProgressBar1.ValueChanged += new System.EventHandler(this.mxProgressBar1_ValueChanged);
            this.mxProgressBar1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mxProgressBar1_MouseDown);
            this.mxProgressBar1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mxProgressBar1_MouseMove);
            // 
            // PlaylistEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.mxProgressBar1);
            this.Name = "PlaylistEntry";
            this.Size = new System.Drawing.Size(474, 87);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MXProgressBar mxProgressBar1;
        private MXButton deleteButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private MXButton playButton;
    }
}
