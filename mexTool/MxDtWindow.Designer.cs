namespace mexTool
{
    partial class MxDtWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MxDtWindow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSound = new System.Windows.Forms.Button();
            this.buttonMusic = new System.Windows.Forms.Button();
            this.buttonMenu = new System.Windows.Forms.Button();
            this.buttonStages = new System.Windows.Forms.Button();
            this.buttonFighter = new System.Windows.Forms.Button();
            this.pictureBoxBanner = new mexTool.GUI.Controls.MXPictureBox();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.labelGameName = new System.Windows.Forms.Label();
            this.buttonMin = new System.Windows.Forms.Button();
            this.buttonMinMax = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openISOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeFileSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iSOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelSaving = new System.Windows.Forms.Label();
            this.progressBarSaving = new System.Windows.Forms.ProgressBar();
            this.savePanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).BeginInit();
            this.headerPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.savePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(50)))));
            this.panel1.Controls.Add(this.buttonSound);
            this.panel1.Controls.Add(this.buttonMusic);
            this.panel1.Controls.Add(this.buttonMenu);
            this.panel1.Controls.Add(this.buttonStages);
            this.panel1.Controls.Add(this.buttonFighter);
            this.panel1.Controls.Add(this.pictureBoxBanner);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 58);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 727);
            this.panel1.TabIndex = 0;
            // 
            // buttonSound
            // 
            this.buttonSound.BackColor = System.Drawing.Color.Transparent;
            this.buttonSound.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonSound.FlatAppearance.BorderSize = 0;
            this.buttonSound.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
            this.buttonSound.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.buttonSound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSound.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSound.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(200)))), ((int)(((byte)(220)))));
            this.buttonSound.Image = global::mexTool.Properties.Resources.category_sound;
            this.buttonSound.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSound.Location = new System.Drawing.Point(0, 356);
            this.buttonSound.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSound.Name = "buttonSound";
            this.buttonSound.Size = new System.Drawing.Size(268, 69);
            this.buttonSound.TabIndex = 4;
            this.buttonSound.Text = " Sound";
            this.buttonSound.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSound.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonSound.UseVisualStyleBackColor = false;
            this.buttonSound.Click += new System.EventHandler(this.buttonSound_Click);
            // 
            // buttonMusic
            // 
            this.buttonMusic.BackColor = System.Drawing.Color.Transparent;
            this.buttonMusic.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonMusic.FlatAppearance.BorderSize = 0;
            this.buttonMusic.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
            this.buttonMusic.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.buttonMusic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMusic.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMusic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(200)))), ((int)(((byte)(220)))));
            this.buttonMusic.Image = global::mexTool.Properties.Resources.category_music;
            this.buttonMusic.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMusic.Location = new System.Drawing.Point(0, 287);
            this.buttonMusic.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonMusic.Name = "buttonMusic";
            this.buttonMusic.Size = new System.Drawing.Size(268, 69);
            this.buttonMusic.TabIndex = 3;
            this.buttonMusic.Text = " Music";
            this.buttonMusic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonMusic.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonMusic.UseVisualStyleBackColor = false;
            this.buttonMusic.Click += new System.EventHandler(this.buttonMusic_Click);
            // 
            // buttonMenu
            // 
            this.buttonMenu.BackColor = System.Drawing.Color.Transparent;
            this.buttonMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonMenu.FlatAppearance.BorderSize = 0;
            this.buttonMenu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
            this.buttonMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.buttonMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMenu.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(200)))), ((int)(((byte)(220)))));
            this.buttonMenu.Image = global::mexTool.Properties.Resources.category_menu;
            this.buttonMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMenu.Location = new System.Drawing.Point(0, 218);
            this.buttonMenu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonMenu.Name = "buttonMenu";
            this.buttonMenu.Size = new System.Drawing.Size(268, 69);
            this.buttonMenu.TabIndex = 2;
            this.buttonMenu.Text = " Menus";
            this.buttonMenu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonMenu.UseVisualStyleBackColor = false;
            this.buttonMenu.Click += new System.EventHandler(this.buttonMenu_Click);
            // 
            // buttonStages
            // 
            this.buttonStages.BackColor = System.Drawing.Color.Transparent;
            this.buttonStages.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonStages.FlatAppearance.BorderSize = 0;
            this.buttonStages.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
            this.buttonStages.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.buttonStages.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStages.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(200)))), ((int)(((byte)(220)))));
            this.buttonStages.Image = global::mexTool.Properties.Resources.category_stage;
            this.buttonStages.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonStages.Location = new System.Drawing.Point(0, 149);
            this.buttonStages.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonStages.Name = "buttonStages";
            this.buttonStages.Size = new System.Drawing.Size(268, 69);
            this.buttonStages.TabIndex = 1;
            this.buttonStages.Text = " Stages";
            this.buttonStages.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonStages.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonStages.UseVisualStyleBackColor = false;
            this.buttonStages.Click += new System.EventHandler(this.buttonStages_Click);
            // 
            // buttonFighter
            // 
            this.buttonFighter.BackColor = System.Drawing.Color.Transparent;
            this.buttonFighter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonFighter.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonFighter.FlatAppearance.BorderSize = 0;
            this.buttonFighter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
            this.buttonFighter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.buttonFighter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFighter.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFighter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(200)))), ((int)(((byte)(220)))));
            this.buttonFighter.Image = global::mexTool.Properties.Resources.category_fighter;
            this.buttonFighter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonFighter.Location = new System.Drawing.Point(0, 80);
            this.buttonFighter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonFighter.Name = "buttonFighter";
            this.buttonFighter.Size = new System.Drawing.Size(268, 69);
            this.buttonFighter.TabIndex = 0;
            this.buttonFighter.Text = " Fighters";
            this.buttonFighter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonFighter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonFighter.UseVisualStyleBackColor = false;
            this.buttonFighter.Click += new System.EventHandler(this.buttonFighter_Click);
            // 
            // pictureBoxBanner
            // 
            this.pictureBoxBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxBanner.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.pictureBoxBanner.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBanner.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxBanner.Name = "pictureBoxBanner";
            this.pictureBoxBanner.Size = new System.Drawing.Size(268, 80);
            this.pictureBoxBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBanner.TabIndex = 4;
            this.pictureBoxBanner.TabStop = false;
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.headerPanel.Controls.Add(this.labelGameName);
            this.headerPanel.Controls.Add(this.buttonMin);
            this.headerPanel.Controls.Add(this.buttonMinMax);
            this.headerPanel.Controls.Add(this.buttonClose);
            this.headerPanel.Controls.Add(this.menuStrip1);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerPanel.ForeColor = System.Drawing.Color.White;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1224, 58);
            this.headerPanel.TabIndex = 1;
            this.headerPanel.DoubleClick += new System.EventHandler(this.headerPanel_DoubleClick);
            this.headerPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.headerPanel_MouseDown);
            // 
            // labelGameName
            // 
            this.labelGameName.AutoSize = true;
            this.labelGameName.Location = new System.Drawing.Point(3, 30);
            this.labelGameName.Name = "labelGameName";
            this.labelGameName.Size = new System.Drawing.Size(122, 25);
            this.labelGameName.TabIndex = 7;
            this.labelGameName.Text = "Game Name";
            // 
            // buttonMin
            // 
            this.buttonMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonMin.FlatAppearance.BorderSize = 0;
            this.buttonMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMin.Location = new System.Drawing.Point(1113, 2);
            this.buttonMin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonMin.Name = "buttonMin";
            this.buttonMin.Size = new System.Drawing.Size(32, 32);
            this.buttonMin.TabIndex = 1;
            this.buttonMin.Text = "-";
            this.buttonMin.UseVisualStyleBackColor = false;
            this.buttonMin.Click += new System.EventHandler(this.buttonMin_Click);
            // 
            // buttonMinMax
            // 
            this.buttonMinMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMinMax.FlatAppearance.BorderSize = 0;
            this.buttonMinMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMinMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMinMax.Location = new System.Drawing.Point(1151, 2);
            this.buttonMinMax.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonMinMax.Name = "buttonMinMax";
            this.buttonMinMax.Size = new System.Drawing.Size(32, 32);
            this.buttonMinMax.TabIndex = 1;
            this.buttonMinMax.Text = "□";
            this.buttonMinMax.UseVisualStyleBackColor = true;
            this.buttonMinMax.Click += new System.EventHandler(this.buttonMinMax_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(1189, 2);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(32, 32);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "x";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1224, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseDown);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openISOToolStripMenuItem,
            this.openFileSystemToolStripMenuItem,
            this.closeFileSystemToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openISOToolStripMenuItem
            // 
            this.openISOToolStripMenuItem.Name = "openISOToolStripMenuItem";
            this.openISOToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openISOToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openISOToolStripMenuItem.Text = "Open ISO";
            this.openISOToolStripMenuItem.Click += new System.EventHandler(this.openISOToolStripMenuItem_Click);
            // 
            // openFileSystemToolStripMenuItem
            // 
            this.openFileSystemToolStripMenuItem.Name = "openFileSystemToolStripMenuItem";
            this.openFileSystemToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openFileSystemToolStripMenuItem.Text = "Open File System";
            this.openFileSystemToolStripMenuItem.Click += new System.EventHandler(this.openFileSystemToolStripMenuItem_Click);
            // 
            // closeFileSystemToolStripMenuItem
            // 
            this.closeFileSystemToolStripMenuItem.Enabled = false;
            this.closeFileSystemToolStripMenuItem.Name = "closeFileSystemToolStripMenuItem";
            this.closeFileSystemToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.closeFileSystemToolStripMenuItem.Text = "Close File System";
            this.closeFileSystemToolStripMenuItem.Click += new System.EventHandler(this.closeFileSystemToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(221, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iSOToolStripMenuItem,
            this.fileSystemToolStripMenuItem});
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveAsToolStripMenuItem.Text = "Export As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // iSOToolStripMenuItem
            // 
            this.iSOToolStripMenuItem.Enabled = false;
            this.iSOToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.iSOToolStripMenuItem.Name = "iSOToolStripMenuItem";
            this.iSOToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.iSOToolStripMenuItem.Text = "ISO";
            this.iSOToolStripMenuItem.Click += new System.EventHandler(this.iSOToolStripMenuItem_Click);
            // 
            // fileSystemToolStripMenuItem
            // 
            this.fileSystemToolStripMenuItem.Enabled = false;
            this.fileSystemToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileSystemToolStripMenuItem.Name = "fileSystemToolStripMenuItem";
            this.fileSystemToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.fileSystemToolStripMenuItem.Text = "File System";
            this.fileSystemToolStripMenuItem.Click += new System.EventHandler(this.fileSystemToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // labelSaving
            // 
            this.labelSaving.AutoSize = true;
            this.labelSaving.Location = new System.Drawing.Point(-1, 5);
            this.labelSaving.Name = "labelSaving";
            this.labelSaving.Size = new System.Drawing.Size(88, 25);
            this.labelSaving.TabIndex = 6;
            this.labelSaving.Text = "Saving...";
            // 
            // progressBarSaving
            // 
            this.progressBarSaving.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarSaving.Location = new System.Drawing.Point(5, 32);
            this.progressBarSaving.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBarSaving.Name = "progressBarSaving";
            this.progressBarSaving.Size = new System.Drawing.Size(948, 46);
            this.progressBarSaving.TabIndex = 5;
            // 
            // savePanel
            // 
            this.savePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.savePanel.Controls.Add(this.progressBarSaving);
            this.savePanel.Controls.Add(this.labelSaving);
            this.savePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.savePanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savePanel.ForeColor = System.Drawing.Color.White;
            this.savePanel.Location = new System.Drawing.Point(268, 58);
            this.savePanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.savePanel.Name = "savePanel";
            this.savePanel.Size = new System.Drawing.Size(956, 80);
            this.savePanel.TabIndex = 2;
            this.savePanel.Visible = false;
            // 
            // MxDtWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1224, 785);
            this.ControlBox = false;
            this.Controls.Add(this.savePanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.headerPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(714, 476);
            this.Name = "MxDtWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).EndInit();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.savePanel.ResumeLayout(false);
            this.savePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Button buttonFighter;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonMin;
        private System.Windows.Forms.Button buttonMinMax;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openISOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileSystemToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button buttonSound;
        private System.Windows.Forms.Button buttonMusic;
        private System.Windows.Forms.Button buttonMenu;
        private System.Windows.Forms.Button buttonStages;
        private GUI.Controls.MXPictureBox pictureBoxBanner;
        private System.Windows.Forms.Label labelSaving;
        private System.Windows.Forms.ProgressBar progressBarSaving;
        private System.Windows.Forms.Label labelGameName;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iSOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileSystemToolStripMenuItem;
        private System.Windows.Forms.Panel savePanel;
        private System.Windows.Forms.ToolStripMenuItem closeFileSystemToolStripMenuItem;
    }
}