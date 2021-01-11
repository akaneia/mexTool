
namespace mexTool.GUI.Controls
{
    partial class MXSemEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mxListBox1 = new mexTool.GUI.MXListBox();
            this.mxToolStrip1 = new mexTool.GUI.Controls.MXToolStrip();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new mexTool.GUI.Controls.DoubleBufferedPanel();
            this.scriptListBox = new mexTool.GUI.MXListBox();
            this.mxToolStrip2 = new mexTool.GUI.Controls.MXToolStrip();
            this.mxPropertyGrid2 = new mexTool.GUI.Controls.MXPropertyGrid();
            this.buttonAddSound = new System.Windows.Forms.ToolStripButton();
            this.buttonRemoveSound = new System.Windows.Forms.ToolStripButton();
            this.buttonClean = new System.Windows.Forms.ToolStripButton();
            this.buttonAddCode = new System.Windows.Forms.ToolStripButton();
            this.buttonRemoveCode = new System.Windows.Forms.ToolStripButton();
            this.panel2.SuspendLayout();
            this.mxToolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.mxToolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(279, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 26);
            this.label1.TabIndex = 6;
            this.label1.Text = "Script";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.Controls.Add(this.mxListBox1);
            this.panel2.Controls.Add(this.mxToolStrip1);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(270, 485);
            this.panel2.TabIndex = 7;
            // 
            // mxListBox1
            // 
            this.mxListBox1.DataSource = null;
            this.mxListBox1.DisplayItemIndices = true;
            this.mxListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mxListBox1.EnableDragReorder = false;
            this.mxListBox1.EnableTOBJ = false;
            this.mxListBox1.ForeColor = System.Drawing.Color.White;
            this.mxListBox1.ImageHeight = 24;
            this.mxListBox1.ItemHeight = 24;
            this.mxListBox1.Location = new System.Drawing.Point(0, 40);
            this.mxListBox1.Name = "mxListBox1";
            this.mxListBox1.SelectedIndex = -1;
            this.mxListBox1.SelectedItem = null;
            this.mxListBox1.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.mxListBox1.ShowScrollbar = false;
            this.mxListBox1.Size = new System.Drawing.Size(270, 445);
            this.mxListBox1.StartingItemIndex = 0;
            this.mxListBox1.TabIndex = 0;
            this.mxListBox1.SelectedValueChanged += new System.EventHandler(this.mxListBox1_SelectedValueChanged);
            this.mxListBox1.DoubleClicked += new System.EventHandler(this.mxListBox1_DoubleClicked);
            // 
            // mxToolStrip1
            // 
            this.mxToolStrip1.AutoSize = false;
            this.mxToolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.mxToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonAddSound,
            this.buttonRemoveSound,
            this.buttonClean});
            this.mxToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.mxToolStrip1.Name = "mxToolStrip1";
            this.mxToolStrip1.Size = new System.Drawing.Size(270, 40);
            this.mxToolStrip1.TabIndex = 2;
            this.mxToolStrip1.Text = "mxToolStrip1";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(353, 8);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(314, 32);
            this.textBox1.TabIndex = 8;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.scriptListBox);
            this.panel1.Controls.Add(this.mxToolStrip2);
            this.panel1.Controls.Add(this.mxPropertyGrid2);
            this.panel1.Location = new System.Drawing.Point(280, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(387, 442);
            this.panel1.TabIndex = 3;
            this.panel1.TabStop = true;
            // 
            // scriptListBox
            // 
            this.scriptListBox.DataSource = null;
            this.scriptListBox.DisplayItemIndices = true;
            this.scriptListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptListBox.EnableDragReorder = true;
            this.scriptListBox.EnableTOBJ = false;
            this.scriptListBox.ForeColor = System.Drawing.Color.White;
            this.scriptListBox.ImageHeight = 24;
            this.scriptListBox.ItemHeight = 24;
            this.scriptListBox.Location = new System.Drawing.Point(0, 40);
            this.scriptListBox.Name = "scriptListBox";
            this.scriptListBox.SelectedIndex = -1;
            this.scriptListBox.SelectedItem = null;
            this.scriptListBox.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.scriptListBox.ShowScrollbar = false;
            this.scriptListBox.Size = new System.Drawing.Size(387, 222);
            this.scriptListBox.StartingItemIndex = 0;
            this.scriptListBox.TabIndex = 1;
            this.scriptListBox.SelectedValueChanged += new System.EventHandler(this.scriptListBox_SelectedValueChanged);
            // 
            // mxToolStrip2
            // 
            this.mxToolStrip2.AutoSize = false;
            this.mxToolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.mxToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonAddCode,
            this.buttonRemoveCode});
            this.mxToolStrip2.Location = new System.Drawing.Point(0, 0);
            this.mxToolStrip2.Name = "mxToolStrip2";
            this.mxToolStrip2.Size = new System.Drawing.Size(387, 40);
            this.mxToolStrip2.TabIndex = 3;
            this.mxToolStrip2.Text = "mxToolStrip2";
            // 
            // mxPropertyGrid2
            // 
            this.mxPropertyGrid2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.mxPropertyGrid2.CategoryForeColor = System.Drawing.Color.White;
            this.mxPropertyGrid2.CategorySplitterColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid2.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.mxPropertyGrid2.CommandsBorderColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid2.CommandsForeColor = System.Drawing.Color.White;
            this.mxPropertyGrid2.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mxPropertyGrid2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mxPropertyGrid2.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.mxPropertyGrid2.HelpBorderColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid2.HelpForeColor = System.Drawing.Color.White;
            this.mxPropertyGrid2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.mxPropertyGrid2.Location = new System.Drawing.Point(0, 262);
            this.mxPropertyGrid2.Name = "mxPropertyGrid2";
            this.mxPropertyGrid2.Size = new System.Drawing.Size(387, 180);
            this.mxPropertyGrid2.TabIndex = 2;
            this.mxPropertyGrid2.ToolbarVisible = false;
            this.mxPropertyGrid2.ViewBackColor = System.Drawing.Color.Black;
            this.mxPropertyGrid2.ViewBorderColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid2.ViewForeColor = System.Drawing.Color.White;
            this.mxPropertyGrid2.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.mxPropertyGrid2_PropertyValueChanged);
            // 
            // buttonAddSound
            // 
            this.buttonAddSound.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonAddSound.Image = global::mexTool.Properties.Resources.plus;
            this.buttonAddSound.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAddSound.Name = "buttonAddSound";
            this.buttonAddSound.Size = new System.Drawing.Size(36, 37);
            this.buttonAddSound.Text = "Add Sound";
            this.buttonAddSound.Click += new System.EventHandler(this.buttonAddSound_Click);
            // 
            // buttonRemoveSound
            // 
            this.buttonRemoveSound.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonRemoveSound.Image = global::mexTool.Properties.Resources.minus;
            this.buttonRemoveSound.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRemoveSound.Name = "buttonRemoveSound";
            this.buttonRemoveSound.Size = new System.Drawing.Size(36, 37);
            this.buttonRemoveSound.Text = "Remove Sound";
            this.buttonRemoveSound.Click += new System.EventHandler(this.buttonRemoveSound_Click);
            // 
            // buttonClean
            // 
            this.buttonClean.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonClean.Image = global::mexTool.Properties.Resources.clean;
            this.buttonClean.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonClean.Name = "buttonClean";
            this.buttonClean.Size = new System.Drawing.Size(36, 37);
            this.buttonClean.Text = "Clean Scripts";
            this.buttonClean.Click += new System.EventHandler(this.buttonClean_Click);
            // 
            // buttonAddCode
            // 
            this.buttonAddCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonAddCode.Image = global::mexTool.Properties.Resources.plus;
            this.buttonAddCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAddCode.Name = "buttonAddCode";
            this.buttonAddCode.Size = new System.Drawing.Size(36, 37);
            this.buttonAddCode.Text = "Add Sound";
            this.buttonAddCode.Click += new System.EventHandler(this.buttonAddCode_Click);
            // 
            // buttonRemoveCode
            // 
            this.buttonRemoveCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonRemoveCode.Image = global::mexTool.Properties.Resources.minus;
            this.buttonRemoveCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRemoveCode.Name = "buttonRemoveCode";
            this.buttonRemoveCode.Size = new System.Drawing.Size(36, 37);
            this.buttonRemoveCode.Text = "Remove Sound";
            this.buttonRemoveCode.Click += new System.EventHandler(this.buttonRemoveCode_Click);
            // 
            // MXSemEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "MXSemEditor";
            this.Size = new System.Drawing.Size(670, 491);
            this.panel2.ResumeLayout(false);
            this.mxToolStrip1.ResumeLayout(false);
            this.mxToolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.mxToolStrip2.ResumeLayout(false);
            this.mxToolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MXListBox mxListBox1;
        private DoubleBufferedPanel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private MXToolStrip mxToolStrip1;
        private System.Windows.Forms.ToolStripButton buttonAddSound;
        private System.Windows.Forms.ToolStripButton buttonRemoveSound;
        private MXListBox scriptListBox;
        private MXPropertyGrid mxPropertyGrid2;
        private MXToolStrip mxToolStrip2;
        private System.Windows.Forms.ToolStripButton buttonAddCode;
        private System.Windows.Forms.ToolStripButton buttonRemoveCode;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripButton buttonClean;
    }
}
