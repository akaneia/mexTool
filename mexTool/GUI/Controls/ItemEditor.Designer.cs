
namespace mexTool.GUI.Controls
{
    partial class ItemEditor
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
            this.toolStrip = new mexTool.GUI.Controls.MXToolStrip();
            this.addItemStrip = new System.Windows.Forms.ToolStripDropDownButton();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addBlankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cloneCommonItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneFighterItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clonePokemonItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneStageItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeItemButton = new System.Windows.Forms.ToolStripButton();
            this.exportItemButton = new System.Windows.Forms.ToolStripButton();
            this.buttonUp = new System.Windows.Forms.ToolStripButton();
            this.buttonDown = new System.Windows.Forms.ToolStripButton();
            this.mxPropertyGrid1 = new mexTool.GUI.Controls.MXPropertyGrid();
            this.mxListBox1 = new mexTool.GUI.MXListBox();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            this.toolStrip.ForeColor = System.Drawing.Color.White;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addItemStrip,
            this.removeItemButton,
            this.exportItemButton,
            this.buttonUp,
            this.buttonDown});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(453, 37);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip_ItemClicked);
            // 
            // addItemStrip
            // 
            this.addItemStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addItemStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.addBlankToolStripMenuItem,
            this.toolStripSeparator1,
            this.cloneCommonItemToolStripMenuItem,
            this.cloneFighterItemToolStripMenuItem,
            this.clonePokemonItemToolStripMenuItem,
            this.cloneStageItemToolStripMenuItem});
            this.addItemStrip.Image = global::mexTool.Properties.Resources.plus;
            this.addItemStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addItemStrip.Name = "addItemStrip";
            this.addItemStrip.Size = new System.Drawing.Size(45, 34);
            this.addItemStrip.Text = "Add Item";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.importToolStripMenuItem.Text = "Import Item";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // addBlankToolStripMenuItem
            // 
            this.addBlankToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.addBlankToolStripMenuItem.Name = "addBlankToolStripMenuItem";
            this.addBlankToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.addBlankToolStripMenuItem.Text = "Add Blank";
            this.addBlankToolStripMenuItem.Click += new System.EventHandler(this.addBlankToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // cloneCommonItemToolStripMenuItem
            // 
            this.cloneCommonItemToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.cloneCommonItemToolStripMenuItem.Name = "cloneCommonItemToolStripMenuItem";
            this.cloneCommonItemToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.cloneCommonItemToolStripMenuItem.Text = "Clone Common Item";
            // 
            // cloneFighterItemToolStripMenuItem
            // 
            this.cloneFighterItemToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.cloneFighterItemToolStripMenuItem.Name = "cloneFighterItemToolStripMenuItem";
            this.cloneFighterItemToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.cloneFighterItemToolStripMenuItem.Text = "Clone Fighter Item";
            // 
            // clonePokemonItemToolStripMenuItem
            // 
            this.clonePokemonItemToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.clonePokemonItemToolStripMenuItem.Name = "clonePokemonItemToolStripMenuItem";
            this.clonePokemonItemToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.clonePokemonItemToolStripMenuItem.Text = "Clone Pokemon Item";
            // 
            // cloneStageItemToolStripMenuItem
            // 
            this.cloneStageItemToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.cloneStageItemToolStripMenuItem.Name = "cloneStageItemToolStripMenuItem";
            this.cloneStageItemToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.cloneStageItemToolStripMenuItem.Text = "Clone Stage Item";
            // 
            // removeItemButton
            // 
            this.removeItemButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeItemButton.Image = global::mexTool.Properties.Resources.minus;
            this.removeItemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeItemButton.Name = "removeItemButton";
            this.removeItemButton.Size = new System.Drawing.Size(36, 34);
            this.removeItemButton.Text = "Remove Item";
            this.removeItemButton.Click += new System.EventHandler(this.removeItemButton_Click);
            // 
            // exportItemButton
            // 
            this.exportItemButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exportItemButton.Image = global::mexTool.Properties.Resources.export;
            this.exportItemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportItemButton.Name = "exportItemButton";
            this.exportItemButton.Size = new System.Drawing.Size(36, 34);
            this.exportItemButton.Text = "Export Item";
            this.exportItemButton.Click += new System.EventHandler(this.exportItemButton_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonUp.Image = global::mexTool.Properties.Resources.up;
            this.buttonUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(36, 34);
            this.buttonUp.Text = "Move Up";
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonDown.Image = global::mexTool.Properties.Resources.down;
            this.buttonDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(36, 34);
            this.buttonDown.Text = "Move Down";
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
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
            this.mxPropertyGrid1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.mxPropertyGrid1.Location = new System.Drawing.Point(208, 40);
            this.mxPropertyGrid1.Name = "mxPropertyGrid1";
            this.mxPropertyGrid1.Size = new System.Drawing.Size(242, 197);
            this.mxPropertyGrid1.TabIndex = 1;
            this.mxPropertyGrid1.ToolbarVisible = false;
            this.mxPropertyGrid1.ViewBackColor = System.Drawing.Color.Black;
            this.mxPropertyGrid1.ViewBorderColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid1.ViewForeColor = System.Drawing.Color.White;
            // 
            // mxListBox1
            // 
            this.mxListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.mxListBox1.DataSource = null;
            this.mxListBox1.DisplayItemIndices = false;
            this.mxListBox1.EnableTOBJ = false;
            this.mxListBox1.ForeColor = System.Drawing.Color.White;
            this.mxListBox1.ImageHeight = 24;
            this.mxListBox1.ItemHeight = 24;
            this.mxListBox1.Location = new System.Drawing.Point(4, 40);
            this.mxListBox1.Name = "mxListBox1";
            this.mxListBox1.SelectedIndex = -1;
            this.mxListBox1.SelectedItem = null;
            this.mxListBox1.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.mxListBox1.ShowScrollbar = false;
            this.mxListBox1.Size = new System.Drawing.Size(198, 197);
            this.mxListBox1.TabIndex = 0;
            this.mxListBox1.SelectedValueChanged += new System.EventHandler(this.mxListBox1_SelectedValueChanged);
            this.mxListBox1.VisibleChanged += new System.EventHandler(this.mxListBox1_VisibleChanged);
            // 
            // ItemEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.mxPropertyGrid1);
            this.Controls.Add(this.mxListBox1);
            this.Name = "ItemEditor";
            this.Size = new System.Drawing.Size(453, 248);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MXListBox mxListBox1;
        private MXPropertyGrid mxPropertyGrid1;
        private MXToolStrip toolStrip;
        private System.Windows.Forms.ToolStripDropDownButton addItemStrip;
        private System.Windows.Forms.ToolStripMenuItem addBlankToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton removeItemButton;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton exportItemButton;
        private System.Windows.Forms.ToolStripMenuItem cloneCommonItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cloneFighterItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clonePokemonItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cloneStageItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton buttonUp;
        private System.Windows.Forms.ToolStripButton buttonDown;
    }
}
