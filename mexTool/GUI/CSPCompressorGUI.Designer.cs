
namespace mexTool.GUI.Controls
{
    partial class CSPCompressorGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CSPCompressorGUI));
            this.saveProgressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mxPictureBox1 = new mexTool.GUI.Controls.MXPictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mxPictureBox2 = new mexTool.GUI.Controls.MXPictureBox();
            this.saveLabel = new System.Windows.Forms.Label();
            this.mxPropertyGrid1 = new mexTool.GUI.Controls.MXPropertyGrid();
            this.mxButton1 = new mexTool.GUI.Controls.MXButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mxPictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mxPictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // saveProgressBar
            // 
            this.saveProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveProgressBar.Location = new System.Drawing.Point(12, 351);
            this.saveProgressBar.Name = "saveProgressBar";
            this.saveProgressBar.Size = new System.Drawing.Size(642, 23);
            this.saveProgressBar.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.mxPictureBox1);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 285);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Before";
            // 
            // mxPictureBox1
            // 
            this.mxPictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mxPictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mxPictureBox1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.mxPictureBox1.Location = new System.Drawing.Point(3, 16);
            this.mxPictureBox1.Name = "mxPictureBox1";
            this.mxPictureBox1.Size = new System.Drawing.Size(194, 266);
            this.mxPictureBox1.TabIndex = 0;
            this.mxPictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.mxPictureBox2);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox2.Location = new System.Drawing.Point(454, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 285);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "After";
            // 
            // mxPictureBox2
            // 
            this.mxPictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mxPictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mxPictureBox2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.mxPictureBox2.Location = new System.Drawing.Point(3, 16);
            this.mxPictureBox2.Name = "mxPictureBox2";
            this.mxPictureBox2.Size = new System.Drawing.Size(194, 266);
            this.mxPictureBox2.TabIndex = 0;
            this.mxPictureBox2.TabStop = false;
            // 
            // saveLabel
            // 
            this.saveLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.saveLabel.AutoSize = true;
            this.saveLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.saveLabel.Location = new System.Drawing.Point(12, 383);
            this.saveLabel.Name = "saveLabel";
            this.saveLabel.Size = new System.Drawing.Size(0, 13);
            this.saveLabel.TabIndex = 5;
            // 
            // mxPropertyGrid1
            // 
            this.mxPropertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mxPropertyGrid1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.mxPropertyGrid1.CategoryForeColor = System.Drawing.Color.White;
            this.mxPropertyGrid1.CategorySplitterColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid1.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.mxPropertyGrid1.CommandsBorderColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid1.CommandsForeColor = System.Drawing.Color.White;
            this.mxPropertyGrid1.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mxPropertyGrid1.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.mxPropertyGrid1.HelpBorderColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid1.HelpForeColor = System.Drawing.Color.White;
            this.mxPropertyGrid1.HelpVisible = false;
            this.mxPropertyGrid1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.mxPropertyGrid1.Location = new System.Drawing.Point(216, 13);
            this.mxPropertyGrid1.Name = "mxPropertyGrid1";
            this.mxPropertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.mxPropertyGrid1.Size = new System.Drawing.Size(235, 278);
            this.mxPropertyGrid1.TabIndex = 4;
            this.mxPropertyGrid1.ToolbarVisible = false;
            this.mxPropertyGrid1.ViewBackColor = System.Drawing.Color.Black;
            this.mxPropertyGrid1.ViewBorderColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid1.ViewForeColor = System.Drawing.Color.White;
            this.mxPropertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.mxPropertyGrid1_PropertyValueChanged);
            // 
            // mxButton1
            // 
            this.mxButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mxButton1.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.mxButton1.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.mxButton1.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.mxButton1.BorderColor = System.Drawing.Color.Black;
            this.mxButton1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.mxButton1.Location = new System.Drawing.Point(12, 303);
            this.mxButton1.Name = "mxButton1";
            this.mxButton1.Size = new System.Drawing.Size(642, 42);
            this.mxButton1.TabIndex = 3;
            this.mxButton1.Text = "Compress CSPs";
            this.mxButton1.Click += new System.EventHandler(this.mxButton1_Click);
            // 
            // CSPCompressorGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(666, 405);
            this.Controls.Add(this.saveLabel);
            this.Controls.Add(this.mxPropertyGrid1);
            this.Controls.Add(this.mxButton1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.saveProgressBar);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CSPCompressorGUI";
            this.Text = "CSP Compressor";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mxPictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mxPictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar saveProgressBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private MXButton mxButton1;
        private MXPictureBox mxPictureBox1;
        private MXPictureBox mxPictureBox2;
        private MXPropertyGrid mxPropertyGrid1;
        private System.Windows.Forms.Label saveLabel;
    }
}