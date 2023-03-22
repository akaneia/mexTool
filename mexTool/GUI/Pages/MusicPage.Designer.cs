
namespace mexTool.GUI.Pages
{
    partial class MusicPage
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
            this.editButton = new mexTool.GUI.Controls.MXButton();
            this.deleteButton = new mexTool.GUI.Controls.MXButton();
            this.buttonExport = new mexTool.GUI.Controls.MXButton();
            this.replaceButton = new mexTool.GUI.Controls.MXButton();
            this.importButton = new mexTool.GUI.Controls.MXButton();
            this.mxListBox1 = new mexTool.GUI.MXListBox();
            this.mxPropertyGrid1 = new mexTool.GUI.Controls.MXPropertyGrid();
            this.soundPlayer = new mexTool.GUI.Controls.MxMusicPlayer();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "Music:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(478, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "Music Info:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // editButton
            // 
            this.editButton.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.editButton.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.editButton.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.editButton.BorderColor = System.Drawing.Color.Black;
            this.editButton.CornerRadius = 20;
            this.editButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editButton.ForeColor = System.Drawing.Color.White;
            this.editButton.Image = global::mexTool.Properties.Resources.sound_wave;
            this.editButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editButton.ImageHeight = 30;
            this.editButton.ImageWidth = 30;
            this.editButton.Location = new System.Drawing.Point(258, 10);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(116, 50);
            this.editButton.TabIndex = 8;
            this.editButton.Text = "Edit";
            this.editButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.deleteButton.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.deleteButton.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.deleteButton.BorderColor = System.Drawing.Color.Black;
            this.deleteButton.CornerRadius = 20;
            this.deleteButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Image = global::mexTool.Properties.Resources.delete;
            this.deleteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.deleteButton.ImageHeight = 32;
            this.deleteButton.ImageWidth = 32;
            this.deleteButton.Location = new System.Drawing.Point(504, 10);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(116, 50);
            this.deleteButton.TabIndex = 5;
            this.deleteButton.Text = "Delete";
            this.deleteButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonExport.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonExport.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.buttonExport.BorderColor = System.Drawing.Color.Black;
            this.buttonExport.CornerRadius = 20;
            this.buttonExport.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExport.ForeColor = System.Drawing.Color.White;
            this.buttonExport.Image = global::mexTool.Properties.Resources.export;
            this.buttonExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExport.ImageHeight = 32;
            this.buttonExport.ImageWidth = 32;
            this.buttonExport.Location = new System.Drawing.Point(136, 10);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(116, 50);
            this.buttonExport.TabIndex = 4;
            this.buttonExport.Text = "Export";
            this.buttonExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // replaceButton
            // 
            this.replaceButton.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.replaceButton.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.replaceButton.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.replaceButton.BorderColor = System.Drawing.Color.Black;
            this.replaceButton.CornerRadius = 20;
            this.replaceButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replaceButton.ForeColor = System.Drawing.Color.White;
            this.replaceButton.Image = global::mexTool.Properties.Resources.replace;
            this.replaceButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.replaceButton.ImageHeight = 30;
            this.replaceButton.ImageWidth = 30;
            this.replaceButton.Location = new System.Drawing.Point(380, 10);
            this.replaceButton.Name = "replaceButton";
            this.replaceButton.Size = new System.Drawing.Size(116, 50);
            this.replaceButton.TabIndex = 4;
            this.replaceButton.Text = "Replace";
            this.replaceButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.replaceButton.Click += new System.EventHandler(this.replaceButton_Click);
            // 
            // importButton
            // 
            this.importButton.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.importButton.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.importButton.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.importButton.BorderColor = System.Drawing.Color.Black;
            this.importButton.CornerRadius = 20;
            this.importButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importButton.ForeColor = System.Drawing.Color.White;
            this.importButton.Image = global::mexTool.Properties.Resources.add_music;
            this.importButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.importButton.ImageHeight = 32;
            this.importButton.ImageWidth = 32;
            this.importButton.Location = new System.Drawing.Point(14, 10);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(116, 50);
            this.importButton.TabIndex = 4;
            this.importButton.Text = "Import";
            this.importButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // mxListBox1
            // 
            this.mxListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mxListBox1.CheckboxSize = new System.Drawing.Size(24, 24);
            this.mxListBox1.DataSource = null;
            this.mxListBox1.DisplayItemIndices = true;
            this.mxListBox1.EnableCheckBoxes = false;
            this.mxListBox1.EnableDragReorder = false;
            this.mxListBox1.EnableTOBJ = false;
            this.mxListBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mxListBox1.ForeColor = System.Drawing.Color.White;
            this.mxListBox1.ImageHeight = 24;
            this.mxListBox1.ItemHeight = 24;
            this.mxListBox1.Location = new System.Drawing.Point(14, 90);
            this.mxListBox1.Name = "mxListBox1";
            this.mxListBox1.SelectedIndex = -1;
            this.mxListBox1.SelectedItem = null;
            this.mxListBox1.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.mxListBox1.ShowScrollbar = false;
            this.mxListBox1.Size = new System.Drawing.Size(462, 394);
            this.mxListBox1.StartingItemIndex = 0;
            this.mxListBox1.TabIndex = 2;
            this.mxListBox1.SelectedValueChanged += new System.EventHandler(this.mxListBox1_SelectedObjectChanged);
            this.mxListBox1.DoubleClicked += new System.EventHandler(this.mxListBox1_DoubleClicked);
            // 
            // mxPropertyGrid1
            // 
            this.mxPropertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
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
            this.mxPropertyGrid1.Location = new System.Drawing.Point(482, 90);
            this.mxPropertyGrid1.Name = "mxPropertyGrid1";
            this.mxPropertyGrid1.Size = new System.Drawing.Size(329, 478);
            this.mxPropertyGrid1.TabIndex = 1;
            this.mxPropertyGrid1.ToolbarVisible = false;
            this.mxPropertyGrid1.ViewBackColor = System.Drawing.Color.Black;
            this.mxPropertyGrid1.ViewBorderColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid1.ViewForeColor = System.Drawing.Color.White;
            // 
            // soundPlayer
            // 
            this.soundPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.soundPlayer.BackColor = System.Drawing.Color.Transparent;
            this.soundPlayer.DSP = null;
            this.soundPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.soundPlayer.ForeColor = System.Drawing.Color.White;
            this.soundPlayer.Location = new System.Drawing.Point(14, 490);
            this.soundPlayer.Name = "soundPlayer";
            this.soundPlayer.ProgressBarVisible = true;
            this.soundPlayer.Radius = 10;
            this.soundPlayer.RoundBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(0)))), ((int)(((byte)(115)))));
            this.soundPlayer.Size = new System.Drawing.Size(462, 78);
            this.soundPlayer.TabIndex = 0;
            // 
            // MusicPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.replaceButton);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.mxListBox1);
            this.Controls.Add(this.mxPropertyGrid1);
            this.Controls.Add(this.soundPlayer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.Name = "MusicPage";
            this.Size = new System.Drawing.Size(828, 571);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MxMusicPlayer soundPlayer;
        private Controls.MXPropertyGrid mxPropertyGrid1;
        private MXListBox mxListBox1;
        private Controls.MXButton importButton;
        private Controls.MXButton deleteButton;
        private Controls.MXButton buttonExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Controls.MXButton replaceButton;
        private Controls.MXButton editButton;
    }
}
