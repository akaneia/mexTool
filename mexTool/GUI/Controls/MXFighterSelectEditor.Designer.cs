
namespace mexTool.GUI.Controls
{
    partial class MXFighterSelectEditor
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.renderer = new mexTool.GUI.Controls.SelectScreenRenderer();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonExportIcon = new mexTool.GUI.Controls.MXButton();
            this.buttonImportIcon = new mexTool.GUI.Controls.MXButton();
            this.mxPictureBox1 = new mexTool.GUI.Controls.MXPictureBox();
            this.removeStage = new mexTool.GUI.Controls.MXButton();
            this.addStage = new mexTool.GUI.Controls.MXButton();
            this.mxPropertyGrid1 = new mexTool.GUI.Controls.MXPropertyGrid();
            this.fighterListBox = new mexTool.GUI.MXListBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mxPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.renderer);
            this.panel1.Location = new System.Drawing.Point(209, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(652, 502);
            this.panel1.TabIndex = 2;
            // 
            // renderer
            // 
            this.renderer.BackColor = System.Drawing.Color.Black;
            this.renderer.DataSource = null;
            this.renderer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderer.DrawBackColor = System.Drawing.Color.Black;
            this.renderer.EnableSnap = true;
            this.renderer.Frame = 0;
            this.renderer.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.renderer.GridEnabled = true;
            this.renderer.GridHeight = 40;
            this.renderer.GridWidth = 40;
            this.renderer.Location = new System.Drawing.Point(0, 0);
            this.renderer.MaxFrame = 1600;
            this.renderer.Name = "renderer";
            this.renderer.ScreenSize = new System.Drawing.Size(64, 48);
            this.renderer.Size = new System.Drawing.Size(652, 502);
            this.renderer.TabIndex = 0;
            this.renderer.Zoom = 7F;
            this.renderer.SelectedIconChanged += new System.EventHandler(this.renderer_SelectedIconChanged);
            this.renderer.Resize += new System.EventHandler(this.renderer_Resize);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "Fighters";
            // 
            // buttonExportIcon
            // 
            this.buttonExportIcon.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonExportIcon.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonExportIcon.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.buttonExportIcon.BorderColor = System.Drawing.Color.Black;
            this.buttonExportIcon.ForeColor = System.Drawing.Color.White;
            this.buttonExportIcon.Image = global::mexTool.Properties.Resources.image_export;
            this.buttonExportIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExportIcon.ImageHeight = 16;
            this.buttonExportIcon.ImageWidth = 16;
            this.buttonExportIcon.Location = new System.Drawing.Point(279, 56);
            this.buttonExportIcon.Name = "buttonExportIcon";
            this.buttonExportIcon.Size = new System.Drawing.Size(100, 24);
            this.buttonExportIcon.TabIndex = 9;
            this.buttonExportIcon.Text = "Export Icon";
            this.buttonExportIcon.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExportIcon.Click += new System.EventHandler(this.buttonExportIcon_Click);
            // 
            // buttonImportIcon
            // 
            this.buttonImportIcon.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonImportIcon.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonImportIcon.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.buttonImportIcon.BorderColor = System.Drawing.Color.Black;
            this.buttonImportIcon.ForeColor = System.Drawing.Color.White;
            this.buttonImportIcon.Image = global::mexTool.Properties.Resources.image_replace;
            this.buttonImportIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonImportIcon.ImageHeight = 16;
            this.buttonImportIcon.ImageWidth = 16;
            this.buttonImportIcon.Location = new System.Drawing.Point(279, 24);
            this.buttonImportIcon.Name = "buttonImportIcon";
            this.buttonImportIcon.Size = new System.Drawing.Size(100, 24);
            this.buttonImportIcon.TabIndex = 10;
            this.buttonImportIcon.Text = "Replace Icon";
            this.buttonImportIcon.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonImportIcon.Click += new System.EventHandler(this.buttonImportIcon_Click);
            // 
            // mxPictureBox1
            // 
            this.mxPictureBox1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.mxPictureBox1.Location = new System.Drawing.Point(209, 24);
            this.mxPictureBox1.Name = "mxPictureBox1";
            this.mxPictureBox1.Size = new System.Drawing.Size(64, 56);
            this.mxPictureBox1.TabIndex = 8;
            this.mxPictureBox1.TabStop = false;
            // 
            // removeStage
            // 
            this.removeStage.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.removeStage.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.removeStage.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.removeStage.BorderColor = System.Drawing.Color.Black;
            this.removeStage.ForeColor = System.Drawing.Color.White;
            this.removeStage.Image = global::mexTool.Properties.Resources.minus;
            this.removeStage.ImageHeight = 32;
            this.removeStage.ImageWidth = 32;
            this.removeStage.Location = new System.Drawing.Point(62, 35);
            this.removeStage.Name = "removeStage";
            this.removeStage.Size = new System.Drawing.Size(53, 48);
            this.removeStage.TabIndex = 4;
            this.removeStage.Click += new System.EventHandler(this.removeStage_Click);
            // 
            // addStage
            // 
            this.addStage.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.addStage.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.addStage.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.addStage.BorderColor = System.Drawing.Color.Black;
            this.addStage.ForeColor = System.Drawing.Color.White;
            this.addStage.Image = global::mexTool.Properties.Resources.plus;
            this.addStage.ImageHeight = 32;
            this.addStage.ImageWidth = 32;
            this.addStage.Location = new System.Drawing.Point(3, 35);
            this.addStage.Name = "addStage";
            this.addStage.Size = new System.Drawing.Size(53, 48);
            this.addStage.TabIndex = 4;
            this.addStage.Click += new System.EventHandler(this.addStage_Click);
            // 
            // mxPropertyGrid1
            // 
            this.mxPropertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.mxPropertyGrid1.Location = new System.Drawing.Point(3, 288);
            this.mxPropertyGrid1.Name = "mxPropertyGrid1";
            this.mxPropertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.mxPropertyGrid1.Size = new System.Drawing.Size(200, 300);
            this.mxPropertyGrid1.TabIndex = 3;
            this.mxPropertyGrid1.ToolbarVisible = false;
            this.mxPropertyGrid1.ViewBackColor = System.Drawing.Color.Black;
            this.mxPropertyGrid1.ViewBorderColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid1.ViewForeColor = System.Drawing.Color.White;
            this.mxPropertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.mxPropertyGrid1_PropertyValueChanged);
            // 
            // fighterListBox
            // 
            this.fighterListBox.DataSource = null;
            this.fighterListBox.DisplayItemIndices = false;
            this.fighterListBox.EnableDragReorder = false;
            this.fighterListBox.EnableTOBJ = false;
            this.fighterListBox.ForeColor = System.Drawing.Color.White;
            this.fighterListBox.ImageHeight = 24;
            this.fighterListBox.ItemHeight = 24;
            this.fighterListBox.Location = new System.Drawing.Point(3, 86);
            this.fighterListBox.Name = "fighterListBox";
            this.fighterListBox.SelectedIndex = -1;
            this.fighterListBox.SelectedItem = null;
            this.fighterListBox.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.fighterListBox.ShowScrollbar = false;
            this.fighterListBox.Size = new System.Drawing.Size(200, 196);
            this.fighterListBox.StartingItemIndex = 0;
            this.fighterListBox.TabIndex = 1;
            this.fighterListBox.SelectedValueChanged += new System.EventHandler(this.stageListBox_SelectedValueChanged);
            // 
            // MXFighterSelectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonExportIcon);
            this.Controls.Add(this.mxPictureBox1);
            this.Controls.Add(this.buttonImportIcon);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.removeStage);
            this.Controls.Add(this.addStage);
            this.Controls.Add(this.mxPropertyGrid1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.fighterListBox);
            this.Name = "MXFighterSelectEditor";
            this.Size = new System.Drawing.Size(865, 591);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mxPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SelectScreenRenderer renderer;
        private MXListBox fighterListBox;
        private System.Windows.Forms.Panel panel1;
        private MXPropertyGrid mxPropertyGrid1;
        private System.Windows.Forms.Label label1;
        private MXButton addStage;
        private MXButton removeStage;
        private MXButton buttonExportIcon;
        private MXButton buttonImportIcon;
        private MXPictureBox mxPictureBox1;
    }
}
