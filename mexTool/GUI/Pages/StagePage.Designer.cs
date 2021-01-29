
namespace mexTool.GUI.Pages
{
    partial class StagePage
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
            this.contentPanel = new System.Windows.Forms.Panel();
            this.buttonPlaylistTab = new mexTool.GUI.Controls.MXButton();
            this.buttonItemTab = new mexTool.GUI.Controls.MXButton();
            this.buttonGeneralTab = new mexTool.GUI.Controls.MXButton();
            this.listBoxStage = new mexTool.GUI.MXListBox();
            this.importButton = new mexTool.GUI.Controls.MXButton();
            this.buttonExport = new mexTool.GUI.Controls.MXButton();
            this.buttonClone = new mexTool.GUI.Controls.MXButton();
            this.buttonRemove = new mexTool.GUI.Controls.MXButton();
            this.buttonGobjCopy = new mexTool.GUI.Controls.MXButton();
            this.contentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Stages:";
            // 
            // contentPanel
            // 
            this.contentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contentPanel.Controls.Add(this.buttonGobjCopy);
            this.contentPanel.Location = new System.Drawing.Point(224, 119);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(388, 375);
            this.contentPanel.TabIndex = 2;
            // 
            // buttonPlaylistTab
            // 
            this.buttonPlaylistTab.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonPlaylistTab.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonPlaylistTab.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.buttonPlaylistTab.BorderColor = System.Drawing.Color.Black;
            this.buttonPlaylistTab.ForeColor = System.Drawing.Color.White;
            this.buttonPlaylistTab.Location = new System.Drawing.Point(386, 90);
            this.buttonPlaylistTab.Name = "buttonPlaylistTab";
            this.buttonPlaylistTab.Size = new System.Drawing.Size(75, 23);
            this.buttonPlaylistTab.TabIndex = 4;
            this.buttonPlaylistTab.Text = "Playlist";
            this.buttonPlaylistTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPlaylistTab.Click += new System.EventHandler(this.SelectTab);
            // 
            // buttonItemTab
            // 
            this.buttonItemTab.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonItemTab.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonItemTab.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.buttonItemTab.BorderColor = System.Drawing.Color.Black;
            this.buttonItemTab.ForeColor = System.Drawing.Color.White;
            this.buttonItemTab.Location = new System.Drawing.Point(305, 90);
            this.buttonItemTab.Name = "buttonItemTab";
            this.buttonItemTab.Size = new System.Drawing.Size(75, 23);
            this.buttonItemTab.TabIndex = 4;
            this.buttonItemTab.Text = "Items";
            this.buttonItemTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonItemTab.Click += new System.EventHandler(this.SelectTab);
            // 
            // buttonGeneralTab
            // 
            this.buttonGeneralTab.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonGeneralTab.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonGeneralTab.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.buttonGeneralTab.BorderColor = System.Drawing.Color.Black;
            this.buttonGeneralTab.ForeColor = System.Drawing.Color.White;
            this.buttonGeneralTab.Location = new System.Drawing.Point(224, 90);
            this.buttonGeneralTab.Name = "buttonGeneralTab";
            this.buttonGeneralTab.Size = new System.Drawing.Size(75, 23);
            this.buttonGeneralTab.TabIndex = 4;
            this.buttonGeneralTab.Text = "General";
            this.buttonGeneralTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGeneralTab.Click += new System.EventHandler(this.SelectTab);
            // 
            // listBoxStage
            // 
            this.listBoxStage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxStage.DataSource = null;
            this.listBoxStage.DisplayItemIndices = true;
            this.listBoxStage.EnableDragReorder = false;
            this.listBoxStage.EnableTOBJ = false;
            this.listBoxStage.ForeColor = System.Drawing.Color.White;
            this.listBoxStage.ImageHeight = 24;
            this.listBoxStage.ItemHeight = 24;
            this.listBoxStage.Location = new System.Drawing.Point(8, 90);
            this.listBoxStage.Name = "listBoxStage";
            this.listBoxStage.SelectedIndex = -1;
            this.listBoxStage.SelectedItem = null;
            this.listBoxStage.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.listBoxStage.ShowScrollbar = false;
            this.listBoxStage.Size = new System.Drawing.Size(210, 404);
            this.listBoxStage.StartingItemIndex = 0;
            this.listBoxStage.TabIndex = 0;
            this.listBoxStage.SelectedValueChanged += new System.EventHandler(this.listBoxStage_SelectedValueChanged);
            // 
            // importButton
            // 
            this.importButton.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.importButton.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.importButton.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.importButton.BorderColor = System.Drawing.Color.Black;
            this.importButton.CornerRadius = 20;
            this.importButton.Font = new System.Drawing.Font("Arial", 12F);
            this.importButton.ForeColor = System.Drawing.Color.White;
            this.importButton.Image = global::mexTool.Properties.Resources.add_fighter;
            this.importButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.importButton.ImageHeight = 32;
            this.importButton.ImageWidth = 32;
            this.importButton.ImageXOffset = 10;
            this.importButton.Location = new System.Drawing.Point(8, 8);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(120, 52);
            this.importButton.TabIndex = 5;
            this.importButton.Text = "Import";
            this.importButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonExport.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonExport.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.buttonExport.BorderColor = System.Drawing.Color.Black;
            this.buttonExport.CornerRadius = 20;
            this.buttonExport.Font = new System.Drawing.Font("Arial", 12F);
            this.buttonExport.ForeColor = System.Drawing.Color.White;
            this.buttonExport.Image = global::mexTool.Properties.Resources.export;
            this.buttonExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExport.ImageHeight = 32;
            this.buttonExport.ImageWidth = 32;
            this.buttonExport.ImageXOffset = 10;
            this.buttonExport.Location = new System.Drawing.Point(386, 8);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(2);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(120, 52);
            this.buttonExport.TabIndex = 6;
            this.buttonExport.Text = "Export";
            this.buttonExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonClone
            // 
            this.buttonClone.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonClone.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonClone.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.buttonClone.BorderColor = System.Drawing.Color.Black;
            this.buttonClone.CornerRadius = 20;
            this.buttonClone.Font = new System.Drawing.Font("Arial", 12F);
            this.buttonClone.ForeColor = System.Drawing.Color.White;
            this.buttonClone.Image = global::mexTool.Properties.Resources.copy;
            this.buttonClone.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonClone.ImageHeight = 32;
            this.buttonClone.ImageWidth = 32;
            this.buttonClone.ImageXOffset = 10;
            this.buttonClone.Location = new System.Drawing.Point(260, 8);
            this.buttonClone.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClone.Name = "buttonClone";
            this.buttonClone.Size = new System.Drawing.Size(120, 52);
            this.buttonClone.TabIndex = 7;
            this.buttonClone.Text = "Clone";
            this.buttonClone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonClone.Click += new System.EventHandler(this.buttonClone_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonRemove.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonRemove.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.buttonRemove.BorderColor = System.Drawing.Color.Black;
            this.buttonRemove.CornerRadius = 20;
            this.buttonRemove.Font = new System.Drawing.Font("Arial", 12F);
            this.buttonRemove.ForeColor = System.Drawing.Color.White;
            this.buttonRemove.Image = global::mexTool.Properties.Resources.delete_fighter;
            this.buttonRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRemove.ImageHeight = 32;
            this.buttonRemove.ImageWidth = 32;
            this.buttonRemove.ImageXOffset = 10;
            this.buttonRemove.Location = new System.Drawing.Point(134, 8);
            this.buttonRemove.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(120, 52);
            this.buttonRemove.TabIndex = 8;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonGobjCopy
            // 
            this.buttonGobjCopy.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonGobjCopy.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonGobjCopy.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.buttonGobjCopy.BorderColor = System.Drawing.Color.Black;
            this.buttonGobjCopy.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonGobjCopy.ForeColor = System.Drawing.Color.White;
            this.buttonGobjCopy.Location = new System.Drawing.Point(0, 0);
            this.buttonGobjCopy.Name = "buttonGobjCopy";
            this.buttonGobjCopy.Size = new System.Drawing.Size(388, 23);
            this.buttonGobjCopy.TabIndex = 0;
            this.buttonGobjCopy.Text = "Copy Map GOBJ Struct to Clipboard";
            this.buttonGobjCopy.Click += new System.EventHandler(this.buttonGobjCopy_Click);
            // 
            // StagePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonClone);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.buttonPlaylistTab);
            this.Controls.Add(this.buttonItemTab);
            this.Controls.Add(this.buttonGeneralTab);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxStage);
            this.Name = "StagePage";
            this.Size = new System.Drawing.Size(637, 511);
            this.contentPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MXListBox listBoxStage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel contentPanel;
        private Controls.MXButton buttonGeneralTab;
        private Controls.MXButton buttonItemTab;
        private Controls.MXButton buttonPlaylistTab;
        private Controls.MXButton importButton;
        private Controls.MXButton buttonExport;
        private Controls.MXButton buttonClone;
        private Controls.MXButton buttonRemove;
        private Controls.MXButton buttonGobjCopy;
    }
}
