
namespace mexTool.GUI.Controls
{
    partial class MXCostumeEditor
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
            this.label2 = new System.Windows.Forms.Label();
            this.kirbyPanel = new System.Windows.Forms.Panel();
            this.kirbyPropertyGrid = new mexTool.GUI.Controls.MXPropertyGrid();
            this.kirbyListbox = new mexTool.GUI.MXListBox();
            this.fighterPanel = new System.Windows.Forms.Panel();
            this.mxPropertyGrid1 = new mexTool.GUI.Controls.MXPropertyGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cspBox = new System.Windows.Forms.PictureBox();
            this.cspToolStrip = new mexTool.GUI.Controls.MXToolStrip();
            this.buttonReplaceCSP = new System.Windows.Forms.ToolStripButton();
            this.buttonExportCSP = new System.Windows.Forms.ToolStripButton();
            this.gawButton = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mxListBox1 = new mexTool.GUI.MXListBox();
            this.mxToolStrip2 = new mexTool.GUI.Controls.MXToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mxToolStrip1 = new mexTool.GUI.Controls.MXToolStrip();
            this.addPackage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonAdd = new System.Windows.Forms.ToolStripButton();
            this.buttonClone = new System.Windows.Forms.ToolStripButton();
            this.buttonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.moveUp = new System.Windows.Forms.ToolStripButton();
            this.moveDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.enableKirbyButton = new System.Windows.Forms.ToolStripButton();
            this.buttonExportCostume = new System.Windows.Forms.ToolStripButton();
            this.kirbyPanel.SuspendLayout();
            this.fighterPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cspBox)).BeginInit();
            this.cspToolStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            this.mxToolStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.mxToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(557, 52);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fighter Costumes:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(560, 48);
            this.label2.TabIndex = 3;
            this.label2.Text = "Kirby Costumes:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // kirbyPanel
            // 
            this.kirbyPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.kirbyPanel.Controls.Add(this.kirbyPropertyGrid);
            this.kirbyPanel.Controls.Add(this.kirbyListbox);
            this.kirbyPanel.Controls.Add(this.label2);
            this.kirbyPanel.Location = new System.Drawing.Point(0, 198);
            this.kirbyPanel.Name = "kirbyPanel";
            this.kirbyPanel.Size = new System.Drawing.Size(560, 171);
            this.kirbyPanel.TabIndex = 4;
            // 
            // kirbyPropertyGrid
            // 
            this.kirbyPropertyGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.kirbyPropertyGrid.CategoryForeColor = System.Drawing.Color.White;
            this.kirbyPropertyGrid.CategorySplitterColor = System.Drawing.Color.Transparent;
            this.kirbyPropertyGrid.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.kirbyPropertyGrid.CommandsBorderColor = System.Drawing.Color.Transparent;
            this.kirbyPropertyGrid.CommandsForeColor = System.Drawing.Color.White;
            this.kirbyPropertyGrid.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.kirbyPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kirbyPropertyGrid.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.kirbyPropertyGrid.HelpBorderColor = System.Drawing.Color.Transparent;
            this.kirbyPropertyGrid.HelpForeColor = System.Drawing.Color.White;
            this.kirbyPropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.kirbyPropertyGrid.Location = new System.Drawing.Point(203, 48);
            this.kirbyPropertyGrid.Name = "kirbyPropertyGrid";
            this.kirbyPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.kirbyPropertyGrid.Size = new System.Drawing.Size(357, 123);
            this.kirbyPropertyGrid.TabIndex = 2;
            this.kirbyPropertyGrid.ToolbarVisible = false;
            this.kirbyPropertyGrid.ViewBackColor = System.Drawing.Color.Black;
            this.kirbyPropertyGrid.ViewBorderColor = System.Drawing.Color.Transparent;
            this.kirbyPropertyGrid.ViewForeColor = System.Drawing.Color.White;
            // 
            // kirbyListbox
            // 
            this.kirbyListbox.CheckboxSize = new System.Drawing.Size(24, 24);
            this.kirbyListbox.DataSource = null;
            this.kirbyListbox.DisplayItemIndices = true;
            this.kirbyListbox.Dock = System.Windows.Forms.DockStyle.Left;
            this.kirbyListbox.EnableCheckBoxes = false;
            this.kirbyListbox.EnableDragReorder = false;
            this.kirbyListbox.EnableTOBJ = false;
            this.kirbyListbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kirbyListbox.ForeColor = System.Drawing.Color.White;
            this.kirbyListbox.ImageHeight = 24;
            this.kirbyListbox.ItemHeight = 24;
            this.kirbyListbox.Location = new System.Drawing.Point(0, 48);
            this.kirbyListbox.Name = "kirbyListbox";
            this.kirbyListbox.SelectedIndex = -1;
            this.kirbyListbox.SelectedItem = null;
            this.kirbyListbox.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.kirbyListbox.ShowScrollbar = false;
            this.kirbyListbox.Size = new System.Drawing.Size(203, 123);
            this.kirbyListbox.StartingItemIndex = 0;
            this.kirbyListbox.TabIndex = 1;
            this.kirbyListbox.SelectedValueChanged += new System.EventHandler(this.kirbyListbox_SelectedValueChanged);
            // 
            // fighterPanel
            // 
            this.fighterPanel.Controls.Add(this.mxPropertyGrid1);
            this.fighterPanel.Controls.Add(this.panel3);
            this.fighterPanel.Controls.Add(this.panel2);
            this.fighterPanel.Controls.Add(this.label1);
            this.fighterPanel.Location = new System.Drawing.Point(3, 3);
            this.fighterPanel.Name = "fighterPanel";
            this.fighterPanel.Size = new System.Drawing.Size(557, 192);
            this.fighterPanel.TabIndex = 5;
            // 
            // mxPropertyGrid1
            // 
            this.mxPropertyGrid1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.mxPropertyGrid1.CategoryForeColor = System.Drawing.Color.White;
            this.mxPropertyGrid1.CategorySplitterColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid1.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.mxPropertyGrid1.CommandsBorderColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid1.CommandsForeColor = System.Drawing.Color.White;
            this.mxPropertyGrid1.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mxPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mxPropertyGrid1.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.mxPropertyGrid1.HelpBorderColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid1.HelpForeColor = System.Drawing.Color.White;
            this.mxPropertyGrid1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.mxPropertyGrid1.Location = new System.Drawing.Point(372, 52);
            this.mxPropertyGrid1.Name = "mxPropertyGrid1";
            this.mxPropertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.mxPropertyGrid1.Size = new System.Drawing.Size(185, 140);
            this.mxPropertyGrid1.TabIndex = 2;
            this.mxPropertyGrid1.ToolbarVisible = false;
            this.mxPropertyGrid1.ViewBackColor = System.Drawing.Color.Black;
            this.mxPropertyGrid1.ViewBorderColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid1.ViewForeColor = System.Drawing.Color.White;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cspBox);
            this.panel3.Controls.Add(this.cspToolStrip);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(200, 52);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(172, 140);
            this.panel3.TabIndex = 6;
            // 
            // cspBox
            // 
            this.cspBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cspBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cspBox.Location = new System.Drawing.Point(0, 43);
            this.cspBox.Name = "cspBox";
            this.cspBox.Size = new System.Drawing.Size(172, 97);
            this.cspBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.cspBox.TabIndex = 7;
            this.cspBox.TabStop = false;
            this.cspBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.cspBox_DragDrop);
            this.cspBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.cspBox_DragEnter);
            this.cspBox.DragOver += new System.Windows.Forms.DragEventHandler(this.cspBox_DragOver);
            // 
            // cspToolStrip
            // 
            this.cspToolStrip.AutoSize = false;
            this.cspToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cspToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonReplaceCSP,
            this.buttonExportCSP,
            this.gawButton});
            this.cspToolStrip.Location = new System.Drawing.Point(0, 0);
            this.cspToolStrip.Name = "cspToolStrip";
            this.cspToolStrip.Size = new System.Drawing.Size(172, 43);
            this.cspToolStrip.TabIndex = 8;
            this.cspToolStrip.Text = "mxToolStrip2";
            // 
            // buttonReplaceCSP
            // 
            this.buttonReplaceCSP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonReplaceCSP.Image = global::mexTool.Properties.Resources.csp_replace;
            this.buttonReplaceCSP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonReplaceCSP.Name = "buttonReplaceCSP";
            this.buttonReplaceCSP.Size = new System.Drawing.Size(36, 40);
            this.buttonReplaceCSP.Text = "Replace CSP";
            this.buttonReplaceCSP.Click += new System.EventHandler(this.buttonReplaceCSP_Click);
            // 
            // buttonExportCSP
            // 
            this.buttonExportCSP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonExportCSP.Image = global::mexTool.Properties.Resources.csp_export;
            this.buttonExportCSP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonExportCSP.Name = "buttonExportCSP";
            this.buttonExportCSP.Size = new System.Drawing.Size(36, 40);
            this.buttonExportCSP.Text = "Export CSP";
            this.buttonExportCSP.Click += new System.EventHandler(this.buttonExportCSP_Click);
            // 
            // gawButton
            // 
            this.gawButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.gawButton.Image = global::mexTool.Properties.Resources.gaw;
            this.gawButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.gawButton.Name = "gawButton";
            this.gawButton.Size = new System.Drawing.Size(36, 40);
            this.gawButton.Text = "Generate UI for GaW";
            this.gawButton.Click += new System.EventHandler(this.gawButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.mxListBox1);
            this.panel2.Controls.Add(this.mxToolStrip2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 140);
            this.panel2.TabIndex = 5;
            // 
            // mxListBox1
            // 
            this.mxListBox1.CheckboxSize = new System.Drawing.Size(24, 24);
            this.mxListBox1.DataSource = null;
            this.mxListBox1.DisplayItemIndices = true;
            this.mxListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mxListBox1.EnableCheckBoxes = false;
            this.mxListBox1.EnableDragReorder = false;
            this.mxListBox1.EnableTOBJ = false;
            this.mxListBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mxListBox1.ForeColor = System.Drawing.Color.White;
            this.mxListBox1.ImageHeight = 24;
            this.mxListBox1.ItemHeight = 24;
            this.mxListBox1.Location = new System.Drawing.Point(0, 43);
            this.mxListBox1.Name = "mxListBox1";
            this.mxListBox1.SelectedIndex = -1;
            this.mxListBox1.SelectedItem = null;
            this.mxListBox1.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.mxListBox1.ShowScrollbar = false;
            this.mxListBox1.Size = new System.Drawing.Size(200, 97);
            this.mxListBox1.StartingItemIndex = 0;
            this.mxListBox1.TabIndex = 2;
            this.mxListBox1.SelectedValueChanged += new System.EventHandler(this.mxListBox1_SelectedValueChanged);
            // 
            // mxToolStrip2
            // 
            this.mxToolStrip2.AutoSize = false;
            this.mxToolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.mxToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.toolStripButton5});
            this.mxToolStrip2.Location = new System.Drawing.Point(0, 0);
            this.mxToolStrip2.Name = "mxToolStrip2";
            this.mxToolStrip2.Size = new System.Drawing.Size(200, 43);
            this.mxToolStrip2.TabIndex = 10;
            this.mxToolStrip2.Text = "mxToolStrip2";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::mexTool.Properties.Resources.stc_replace;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(36, 40);
            this.toolStripButton2.Text = "Replace Stock Icon";
            this.toolStripButton2.Click += new System.EventHandler(this.mxButton1_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::mexTool.Properties.Resources.stc_export;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(36, 40);
            this.toolStripButton5.Text = "Export Stock Icon";
            this.toolStripButton5.Click += new System.EventHandler(this.mxButton2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.fighterPanel);
            this.panel1.Controls.Add(this.kirbyPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 436);
            this.panel1.TabIndex = 6;
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // mxToolStrip1
            // 
            this.mxToolStrip1.AutoSize = false;
            this.mxToolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.mxToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPackage,
            this.buttonExportCostume,
            this.toolStripSeparator3,
            this.buttonAdd,
            this.buttonClone,
            this.buttonDelete,
            this.toolStripSeparator1,
            this.moveUp,
            this.moveDown,
            this.toolStripSeparator4,
            this.enableKirbyButton});
            this.mxToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.mxToolStrip1.Name = "mxToolStrip1";
            this.mxToolStrip1.Size = new System.Drawing.Size(560, 44);
            this.mxToolStrip1.TabIndex = 0;
            this.mxToolStrip1.Text = "mxToolStrip1";
            // 
            // addPackage
            // 
            this.addPackage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addPackage.Image = global::mexTool.Properties.Resources.package;
            this.addPackage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addPackage.Name = "addPackage";
            this.addPackage.Size = new System.Drawing.Size(36, 41);
            this.addPackage.Text = "Import Costume";
            this.addPackage.Click += new System.EventHandler(this.addPackage_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(24, 40);
            // 
            // buttonAdd
            // 
            this.buttonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonAdd.Image = global::mexTool.Properties.Resources.plus;
            this.buttonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(36, 41);
            this.buttonAdd.Text = "Add Costume";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonClone
            // 
            this.buttonClone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonClone.Image = global::mexTool.Properties.Resources.copy;
            this.buttonClone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonClone.Name = "buttonClone";
            this.buttonClone.Size = new System.Drawing.Size(36, 41);
            this.buttonClone.Text = "Clone Costume";
            this.buttonClone.Click += new System.EventHandler(this.buttonClone_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonDelete.Image = global::mexTool.Properties.Resources.minus;
            this.buttonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(36, 41);
            this.buttonDelete.Text = "Delete Costume";
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(24, 40);
            // 
            // moveUp
            // 
            this.moveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveUp.Image = global::mexTool.Properties.Resources.up;
            this.moveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveUp.Name = "moveUp";
            this.moveUp.Size = new System.Drawing.Size(36, 41);
            this.moveUp.Text = "Move Costume Up";
            this.moveUp.Click += new System.EventHandler(this.moveUp_Click);
            // 
            // moveDown
            // 
            this.moveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveDown.Image = global::mexTool.Properties.Resources.down;
            this.moveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveDown.Name = "moveDown";
            this.moveDown.Size = new System.Drawing.Size(36, 41);
            this.moveDown.Text = "Move Costume Down";
            this.moveDown.Click += new System.EventHandler(this.moveDown_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(24, 40);
            // 
            // enableKirbyButton
            // 
            this.enableKirbyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.enableKirbyButton.Image = global::mexTool.Properties.Resources.kirby_costume;
            this.enableKirbyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.enableKirbyButton.Name = "enableKirbyButton";
            this.enableKirbyButton.Size = new System.Drawing.Size(36, 41);
            this.enableKirbyButton.Text = "Enable/Disable Kirby Costumes";
            this.enableKirbyButton.Click += new System.EventHandler(this.enableKirbyButton_Click);
            // 
            // buttonExportCostume
            // 
            this.buttonExportCostume.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonExportCostume.Image = global::mexTool.Properties.Resources.export;
            this.buttonExportCostume.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonExportCostume.Name = "buttonExportCostume";
            this.buttonExportCostume.Size = new System.Drawing.Size(36, 41);
            this.buttonExportCostume.Text = "Export Costume";
            this.buttonExportCostume.Click += new System.EventHandler(this.buttonExportCostume_Click);
            // 
            // MXCostumeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mxToolStrip1);
            this.Name = "MXCostumeEditor";
            this.Size = new System.Drawing.Size(560, 480);
            this.kirbyPanel.ResumeLayout(false);
            this.fighterPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cspBox)).EndInit();
            this.cspToolStrip.ResumeLayout(false);
            this.cspToolStrip.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.mxToolStrip2.ResumeLayout(false);
            this.mxToolStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.mxToolStrip1.ResumeLayout(false);
            this.mxToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MXToolStrip mxToolStrip1;
        private System.Windows.Forms.ToolStripButton buttonAdd;
        private System.Windows.Forms.ToolStripButton buttonDelete;
        private MXPropertyGrid mxPropertyGrid1;
        private System.Windows.Forms.ToolStripButton enableKirbyButton;
        private System.Windows.Forms.ToolStripButton buttonClone;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton moveUp;
        private System.Windows.Forms.ToolStripButton moveDown;
        private MXListBox kirbyListbox;
        private MXPropertyGrid kirbyPropertyGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel kirbyPanel;
        private System.Windows.Forms.Panel fighterPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton addPackage;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox cspBox;
        private MXToolStrip cspToolStrip;
        private MXListBox mxListBox1;
        private System.Windows.Forms.ToolStripButton buttonReplaceCSP;
        private System.Windows.Forms.ToolStripButton buttonExportCSP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton gawButton;
        private MXToolStrip mxToolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton buttonExportCostume;
    }
}
