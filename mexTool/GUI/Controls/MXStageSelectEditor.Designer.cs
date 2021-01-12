
namespace mexTool.GUI.Controls
{
    partial class MXStageSelectEditor
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
            this.iconBox = new System.Windows.Forms.PictureBox();
            this.nameTagBox = new System.Windows.Forms.PictureBox();
            this.imageEditPanel = new System.Windows.Forms.Panel();
            this.stageNameBox = new System.Windows.Forms.TextBox();
            this.locationBox = new System.Windows.Forms.TextBox();
            this.buttonGenerateTag = new mexTool.GUI.Controls.MXButton();
            this.buttonImportIcon = new mexTool.GUI.Controls.MXButton();
            this.buttonExportNameTag = new mexTool.GUI.Controls.MXButton();
            this.buttonExportIcon = new mexTool.GUI.Controls.MXButton();
            this.removeStage = new mexTool.GUI.Controls.MXButton();
            this.addStage = new mexTool.GUI.Controls.MXButton();
            this.mxPropertyGrid1 = new mexTool.GUI.Controls.MXPropertyGrid();
            this.stageListBox = new mexTool.GUI.MXListBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameTagBox)).BeginInit();
            this.imageEditPanel.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(737, 502);
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
            this.renderer.Size = new System.Drawing.Size(737, 502);
            this.renderer.TabIndex = 0;
            this.renderer.Zoom = 7F;
            this.renderer.SelectedIconChanged += new System.EventHandler(this.renderer_SelectedIconChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "Stages";
            // 
            // iconBox
            // 
            this.iconBox.Location = new System.Drawing.Point(122, 3);
            this.iconBox.Name = "iconBox";
            this.iconBox.Size = new System.Drawing.Size(64, 56);
            this.iconBox.TabIndex = 6;
            this.iconBox.TabStop = false;
            // 
            // nameTagBox
            // 
            this.nameTagBox.Location = new System.Drawing.Point(484, 5);
            this.nameTagBox.Name = "nameTagBox";
            this.nameTagBox.Size = new System.Drawing.Size(224, 56);
            this.nameTagBox.TabIndex = 6;
            this.nameTagBox.TabStop = false;
            // 
            // imageEditPanel
            // 
            this.imageEditPanel.Controls.Add(this.stageNameBox);
            this.imageEditPanel.Controls.Add(this.locationBox);
            this.imageEditPanel.Controls.Add(this.buttonGenerateTag);
            this.imageEditPanel.Controls.Add(this.buttonImportIcon);
            this.imageEditPanel.Controls.Add(this.buttonExportNameTag);
            this.imageEditPanel.Controls.Add(this.nameTagBox);
            this.imageEditPanel.Controls.Add(this.buttonExportIcon);
            this.imageEditPanel.Controls.Add(this.iconBox);
            this.imageEditPanel.Location = new System.Drawing.Point(209, 16);
            this.imageEditPanel.Name = "imageEditPanel";
            this.imageEditPanel.Size = new System.Drawing.Size(735, 67);
            this.imageEditPanel.TabIndex = 7;
            this.imageEditPanel.Visible = false;
            // 
            // stageNameBox
            // 
            this.stageNameBox.Location = new System.Drawing.Point(311, 39);
            this.stageNameBox.Name = "stageNameBox";
            this.stageNameBox.Size = new System.Drawing.Size(167, 20);
            this.stageNameBox.TabIndex = 7;
            // 
            // locationBox
            // 
            this.locationBox.Location = new System.Drawing.Point(311, 9);
            this.locationBox.Name = "locationBox";
            this.locationBox.Size = new System.Drawing.Size(167, 20);
            this.locationBox.TabIndex = 7;
            // 
            // buttonGenerateTag
            // 
            this.buttonGenerateTag.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonGenerateTag.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonGenerateTag.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.buttonGenerateTag.BorderColor = System.Drawing.Color.Black;
            this.buttonGenerateTag.ForeColor = System.Drawing.Color.White;
            this.buttonGenerateTag.Location = new System.Drawing.Point(192, 3);
            this.buttonGenerateTag.Name = "buttonGenerateTag";
            this.buttonGenerateTag.Size = new System.Drawing.Size(113, 26);
            this.buttonGenerateTag.TabIndex = 4;
            this.buttonGenerateTag.Text = "Generate Tag";
            this.buttonGenerateTag.Click += new System.EventHandler(this.buttonGenerateTag_Click);
            // 
            // buttonImportIcon
            // 
            this.buttonImportIcon.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonImportIcon.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonImportIcon.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.buttonImportIcon.BorderColor = System.Drawing.Color.Black;
            this.buttonImportIcon.ForeColor = System.Drawing.Color.White;
            this.buttonImportIcon.Location = new System.Drawing.Point(3, 3);
            this.buttonImportIcon.Name = "buttonImportIcon";
            this.buttonImportIcon.Size = new System.Drawing.Size(113, 26);
            this.buttonImportIcon.TabIndex = 4;
            this.buttonImportIcon.Text = "Import Icon";
            this.buttonImportIcon.Click += new System.EventHandler(this.buttonImportIcon_Click);
            // 
            // buttonExportNameTag
            // 
            this.buttonExportNameTag.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonExportNameTag.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonExportNameTag.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.buttonExportNameTag.BorderColor = System.Drawing.Color.Black;
            this.buttonExportNameTag.ForeColor = System.Drawing.Color.White;
            this.buttonExportNameTag.Location = new System.Drawing.Point(192, 35);
            this.buttonExportNameTag.Name = "buttonExportNameTag";
            this.buttonExportNameTag.Size = new System.Drawing.Size(113, 26);
            this.buttonExportNameTag.TabIndex = 4;
            this.buttonExportNameTag.Text = "Import Custom";
            this.buttonExportNameTag.Click += new System.EventHandler(this.imageNameTagClick);
            // 
            // buttonExportIcon
            // 
            this.buttonExportIcon.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonExportIcon.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonExportIcon.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.buttonExportIcon.BorderColor = System.Drawing.Color.Black;
            this.buttonExportIcon.ForeColor = System.Drawing.Color.White;
            this.buttonExportIcon.Location = new System.Drawing.Point(3, 35);
            this.buttonExportIcon.Name = "buttonExportIcon";
            this.buttonExportIcon.Size = new System.Drawing.Size(113, 26);
            this.buttonExportIcon.TabIndex = 4;
            this.buttonExportIcon.Text = "Export Icon";
            this.buttonExportIcon.Click += new System.EventHandler(this.buttonExportIcon_Click);
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
            this.mxPropertyGrid1.Location = new System.Drawing.Point(3, 307);
            this.mxPropertyGrid1.Name = "mxPropertyGrid1";
            this.mxPropertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.mxPropertyGrid1.Size = new System.Drawing.Size(200, 281);
            this.mxPropertyGrid1.TabIndex = 3;
            this.mxPropertyGrid1.ToolbarVisible = false;
            this.mxPropertyGrid1.ViewBackColor = System.Drawing.Color.Black;
            this.mxPropertyGrid1.ViewBorderColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid1.ViewForeColor = System.Drawing.Color.White;
            this.mxPropertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.mxPropertyGrid1_PropertyValueChanged);
            // 
            // stageListBox
            // 
            this.stageListBox.DataSource = null;
            this.stageListBox.DisplayItemIndices = true;
            this.stageListBox.EnableDragReorder = false;
            this.stageListBox.EnableTOBJ = false;
            this.stageListBox.ForeColor = System.Drawing.Color.White;
            this.stageListBox.ImageHeight = 24;
            this.stageListBox.ItemHeight = 24;
            this.stageListBox.Location = new System.Drawing.Point(3, 86);
            this.stageListBox.Name = "stageListBox";
            this.stageListBox.SelectedIndex = -1;
            this.stageListBox.SelectedItem = null;
            this.stageListBox.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.stageListBox.ShowScrollbar = false;
            this.stageListBox.Size = new System.Drawing.Size(200, 215);
            this.stageListBox.StartingItemIndex = 0;
            this.stageListBox.TabIndex = 1;
            this.stageListBox.SelectedValueChanged += new System.EventHandler(this.stageListBox_SelectedValueChanged);
            // 
            // MXStageSelectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imageEditPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.removeStage);
            this.Controls.Add(this.addStage);
            this.Controls.Add(this.mxPropertyGrid1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stageListBox);
            this.Name = "MXStageSelectEditor";
            this.Size = new System.Drawing.Size(950, 591);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameTagBox)).EndInit();
            this.imageEditPanel.ResumeLayout(false);
            this.imageEditPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SelectScreenRenderer renderer;
        private MXListBox stageListBox;
        private System.Windows.Forms.Panel panel1;
        private MXPropertyGrid mxPropertyGrid1;
        private MXButton buttonImportIcon;
        private System.Windows.Forms.Label label1;
        private MXButton addStage;
        private MXButton removeStage;
        private MXButton buttonExportIcon;
        private System.Windows.Forms.PictureBox iconBox;
        private System.Windows.Forms.PictureBox nameTagBox;
        private System.Windows.Forms.Panel imageEditPanel;
        private MXButton buttonGenerateTag;
        private MXButton buttonExportNameTag;
        private System.Windows.Forms.TextBox stageNameBox;
        private System.Windows.Forms.TextBox locationBox;
    }
}
