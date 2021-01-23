
namespace mexTool.GUI.Controls
{
    partial class MXSsmEditor
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
            this.mxPropertyGrid1 = new mexTool.GUI.Controls.MXPropertyGrid();
            this.mxToolStrip1 = new mexTool.GUI.Controls.MXToolStrip();
            this.buttonAddSound = new System.Windows.Forms.ToolStripButton();
            this.buttonRemoveSound = new System.Windows.Forms.ToolStripButton();
            this.buttonReplaceSound = new System.Windows.Forms.ToolStripButton();
            this.mxListBox1 = new mexTool.GUI.MXListBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.mxToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mxPropertyGrid1
            // 
            this.mxPropertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
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
            this.mxPropertyGrid1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.mxPropertyGrid1.Location = new System.Drawing.Point(3, 292);
            this.mxPropertyGrid1.Name = "mxPropertyGrid1";
            this.mxPropertyGrid1.Size = new System.Drawing.Size(415, 196);
            this.mxPropertyGrid1.TabIndex = 2;
            this.mxPropertyGrid1.ToolbarVisible = false;
            this.mxPropertyGrid1.ViewBackColor = System.Drawing.Color.Black;
            this.mxPropertyGrid1.ViewBorderColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid1.ViewForeColor = System.Drawing.Color.White;
            // 
            // mxToolStrip1
            // 
            this.mxToolStrip1.AutoSize = false;
            this.mxToolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.mxToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonAddSound,
            this.buttonRemoveSound,
            this.toolStripButton1,
            this.buttonReplaceSound});
            this.mxToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.mxToolStrip1.Name = "mxToolStrip1";
            this.mxToolStrip1.Size = new System.Drawing.Size(421, 40);
            this.mxToolStrip1.TabIndex = 1;
            this.mxToolStrip1.Text = "mxToolStrip1";
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
            // buttonReplaceSound
            // 
            this.buttonReplaceSound.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonReplaceSound.Image = global::mexTool.Properties.Resources.replace;
            this.buttonReplaceSound.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonReplaceSound.Name = "buttonReplaceSound";
            this.buttonReplaceSound.Size = new System.Drawing.Size(36, 37);
            this.buttonReplaceSound.Text = "Replace Sound";
            this.buttonReplaceSound.Click += new System.EventHandler(this.buttonReplaceSound_Click);
            // 
            // mxListBox1
            // 
            this.mxListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mxListBox1.DataSource = null;
            this.mxListBox1.DisplayItemIndices = true;
            this.mxListBox1.EnableDragReorder = false;
            this.mxListBox1.EnableTOBJ = false;
            this.mxListBox1.ForeColor = System.Drawing.Color.White;
            this.mxListBox1.ImageHeight = 24;
            this.mxListBox1.ItemHeight = 24;
            this.mxListBox1.Location = new System.Drawing.Point(3, 57);
            this.mxListBox1.Name = "mxListBox1";
            this.mxListBox1.SelectedIndex = -1;
            this.mxListBox1.SelectedItem = null;
            this.mxListBox1.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.mxListBox1.ShowScrollbar = false;
            this.mxListBox1.Size = new System.Drawing.Size(415, 229);
            this.mxListBox1.StartingItemIndex = 0;
            this.mxListBox1.TabIndex = 0;
            this.mxListBox1.SelectedValueChanged += new System.EventHandler(this.mxListBox1_SelectedValueChanged);
            this.mxListBox1.DoubleClicked += new System.EventHandler(this.mxListBox1_DoubleClicked);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::mexTool.Properties.Resources.export;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 37);
            this.toolStripButton1.Text = "Export Sound";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // MXSsmEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mxPropertyGrid1);
            this.Controls.Add(this.mxToolStrip1);
            this.Controls.Add(this.mxListBox1);
            this.Name = "MXSsmEditor";
            this.Size = new System.Drawing.Size(421, 491);
            this.mxToolStrip1.ResumeLayout(false);
            this.mxToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MXListBox mxListBox1;
        private MXToolStrip mxToolStrip1;
        private System.Windows.Forms.ToolStripButton buttonAddSound;
        private System.Windows.Forms.ToolStripButton buttonRemoveSound;
        private System.Windows.Forms.ToolStripButton buttonReplaceSound;
        private MXPropertyGrid mxPropertyGrid1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}
