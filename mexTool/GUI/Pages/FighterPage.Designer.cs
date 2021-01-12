
namespace mexTool.GUI.Pages
{
    partial class FighterPage
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonExport = new mexTool.GUI.Controls.MXButton();
            this.buttonClone = new mexTool.GUI.Controls.MXButton();
            this.buttonRemove = new mexTool.GUI.Controls.MXButton();
            this.importButton = new mexTool.GUI.Controls.MXButton();
            this.buttonBoneTab = new mexTool.GUI.Controls.MXButton();
            this.buttonFunctionTab = new mexTool.GUI.Controls.MXButton();
            this.buttonItemsTab = new mexTool.GUI.Controls.MXButton();
            this.buttonCostumeTab = new mexTool.GUI.Controls.MXButton();
            this.buttonGeneralTab = new mexTool.GUI.Controls.MXButton();
            this.fighterListBox = new mexTool.GUI.MXListBox();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(224, 119);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(542, 391);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fighters:";
            // 
            // buttonExport
            // 
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
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(120, 52);
            this.buttonExport.TabIndex = 1;
            this.buttonExport.Text = "Export";
            this.buttonExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonClone
            // 
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
            this.buttonClone.Name = "buttonClone";
            this.buttonClone.Size = new System.Drawing.Size(120, 52);
            this.buttonClone.TabIndex = 1;
            this.buttonClone.Text = "Clone";
            this.buttonClone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonClone.Click += new System.EventHandler(this.mxButton2_Click);
            // 
            // buttonRemove
            // 
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
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(120, 52);
            this.buttonRemove.TabIndex = 1;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // importButton
            // 
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
            this.importButton.TabIndex = 1;
            this.importButton.Text = "Import";
            this.importButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.importButton.Click += new System.EventHandler(this.importButton_Click_1);
            // 
            // buttonBoneTab
            // 
            this.buttonBoneTab.BorderColor = System.Drawing.Color.Black;
            this.buttonBoneTab.CornerRadius = 2;
            this.buttonBoneTab.ForeColor = System.Drawing.Color.White;
            this.buttonBoneTab.Location = new System.Drawing.Point(548, 90);
            this.buttonBoneTab.Name = "buttonBoneTab";
            this.buttonBoneTab.Size = new System.Drawing.Size(75, 23);
            this.buttonBoneTab.TabIndex = 1;
            this.buttonBoneTab.Text = "Bones";
            this.buttonBoneTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonBoneTab.Click += new System.EventHandler(this.SelectTab);
            // 
            // buttonFunctionTab
            // 
            this.buttonFunctionTab.BorderColor = System.Drawing.Color.Black;
            this.buttonFunctionTab.CornerRadius = 2;
            this.buttonFunctionTab.ForeColor = System.Drawing.Color.White;
            this.buttonFunctionTab.Location = new System.Drawing.Point(467, 90);
            this.buttonFunctionTab.Name = "buttonFunctionTab";
            this.buttonFunctionTab.Size = new System.Drawing.Size(75, 23);
            this.buttonFunctionTab.TabIndex = 1;
            this.buttonFunctionTab.Text = "Functions";
            this.buttonFunctionTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonFunctionTab.Click += new System.EventHandler(this.SelectTab);
            // 
            // buttonItemsTab
            // 
            this.buttonItemsTab.BorderColor = System.Drawing.Color.Black;
            this.buttonItemsTab.CornerRadius = 2;
            this.buttonItemsTab.ForeColor = System.Drawing.Color.White;
            this.buttonItemsTab.Location = new System.Drawing.Point(386, 90);
            this.buttonItemsTab.Name = "buttonItemsTab";
            this.buttonItemsTab.Size = new System.Drawing.Size(75, 23);
            this.buttonItemsTab.TabIndex = 1;
            this.buttonItemsTab.Text = "Items";
            this.buttonItemsTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonItemsTab.Click += new System.EventHandler(this.SelectTab);
            // 
            // buttonCostumeTab
            // 
            this.buttonCostumeTab.BorderColor = System.Drawing.Color.Black;
            this.buttonCostumeTab.CornerRadius = 2;
            this.buttonCostumeTab.ForeColor = System.Drawing.Color.White;
            this.buttonCostumeTab.Location = new System.Drawing.Point(305, 90);
            this.buttonCostumeTab.Name = "buttonCostumeTab";
            this.buttonCostumeTab.Size = new System.Drawing.Size(75, 23);
            this.buttonCostumeTab.TabIndex = 1;
            this.buttonCostumeTab.Text = "Costumes";
            this.buttonCostumeTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCostumeTab.Click += new System.EventHandler(this.SelectTab);
            // 
            // buttonGeneralTab
            // 
            this.buttonGeneralTab.BorderColor = System.Drawing.Color.Black;
            this.buttonGeneralTab.CornerRadius = 2;
            this.buttonGeneralTab.ForeColor = System.Drawing.Color.White;
            this.buttonGeneralTab.Location = new System.Drawing.Point(224, 90);
            this.buttonGeneralTab.Name = "buttonGeneralTab";
            this.buttonGeneralTab.Size = new System.Drawing.Size(75, 23);
            this.buttonGeneralTab.TabIndex = 1;
            this.buttonGeneralTab.Text = "General";
            this.buttonGeneralTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGeneralTab.Click += new System.EventHandler(this.SelectTab);
            // 
            // fighterListBox
            // 
            this.fighterListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.fighterListBox.DataSource = null;
            this.fighterListBox.DisplayItemIndices = false;
            this.fighterListBox.EnableDragReorder = false;
            this.fighterListBox.EnableTOBJ = false;
            this.fighterListBox.ForeColor = System.Drawing.Color.White;
            this.fighterListBox.ImageHeight = 24;
            this.fighterListBox.ItemHeight = 24;
            this.fighterListBox.Location = new System.Drawing.Point(8, 90);
            this.fighterListBox.Name = "fighterListBox";
            this.fighterListBox.SelectedIndex = -1;
            this.fighterListBox.SelectedItem = null;
            this.fighterListBox.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.fighterListBox.ShowScrollbar = false;
            this.fighterListBox.Size = new System.Drawing.Size(210, 420);
            this.fighterListBox.StartingItemIndex = 0;
            this.fighterListBox.TabIndex = 0;
            this.fighterListBox.SelectedValueChanged += new System.EventHandler(this.fighterListBox_SelectedObjectChanged);
            // 
            // FighterPage
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonClone);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.buttonBoneTab);
            this.Controls.Add(this.buttonFunctionTab);
            this.Controls.Add(this.buttonItemsTab);
            this.Controls.Add(this.buttonCostumeTab);
            this.Controls.Add(this.buttonGeneralTab);
            this.Controls.Add(this.fighterListBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "FighterPage";
            this.Size = new System.Drawing.Size(766, 513);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MXListBox fighterListBox;
        private Controls.MXButton buttonGeneralTab;
        private System.Windows.Forms.Panel panel1;
        private Controls.MXButton importButton;
        private Controls.MXButton buttonCostumeTab;
        private Controls.MXButton buttonItemsTab;
        private Controls.MXButton buttonFunctionTab;
        private Controls.MXButton buttonBoneTab;
        private System.Windows.Forms.Label label1;
        private Controls.MXButton buttonRemove;
        private Controls.MXButton buttonClone;
        private Controls.MXButton buttonExport;
    }
}
