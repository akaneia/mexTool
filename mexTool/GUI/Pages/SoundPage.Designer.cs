
namespace mexTool.GUI.Pages
{
    partial class SoundPage
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
            this.scriptTabButton = new mexTool.GUI.Controls.MXButton();
            this.bankTabButton = new mexTool.GUI.Controls.MXButton();
            this.mxMusicPlayer1 = new mexTool.GUI.Controls.MxMusicPlayer();
            this.bankListBox = new mexTool.GUI.MXListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonExport = new mexTool.GUI.Controls.MXButton();
            this.buttonClone = new mexTool.GUI.Controls.MXButton();
            this.buttonRemove = new mexTool.GUI.Controls.MXButton();
            this.importButton = new mexTool.GUI.Controls.MXButton();
            this.SuspendLayout();
            // 
            // scriptTabButton
            // 
            this.scriptTabButton.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.scriptTabButton.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.scriptTabButton.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.scriptTabButton.BorderColor = System.Drawing.Color.Black;
            this.scriptTabButton.ForeColor = System.Drawing.Color.White;
            this.scriptTabButton.Location = new System.Drawing.Point(231, 90);
            this.scriptTabButton.Name = "scriptTabButton";
            this.scriptTabButton.Size = new System.Drawing.Size(69, 23);
            this.scriptTabButton.TabIndex = 4;
            this.scriptTabButton.Text = "Scripts";
            this.scriptTabButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.scriptTabButton.Click += new System.EventHandler(this.buttonCSSTab_Click);
            // 
            // bankTabButton
            // 
            this.bankTabButton.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.bankTabButton.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.bankTabButton.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.bankTabButton.BorderColor = System.Drawing.Color.Black;
            this.bankTabButton.ForeColor = System.Drawing.Color.White;
            this.bankTabButton.Location = new System.Drawing.Point(305, 90);
            this.bankTabButton.Name = "bankTabButton";
            this.bankTabButton.Size = new System.Drawing.Size(69, 23);
            this.bankTabButton.TabIndex = 5;
            this.bankTabButton.Text = "Banks";
            this.bankTabButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bankTabButton.Click += new System.EventHandler(this.buttonCSSTab_Click);
            // 
            // mxMusicPlayer1
            // 
            this.mxMusicPlayer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mxMusicPlayer1.BackColor = System.Drawing.Color.Transparent;
            this.mxMusicPlayer1.DSP = null;
            this.mxMusicPlayer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.mxMusicPlayer1.ForeColor = System.Drawing.Color.White;
            this.mxMusicPlayer1.Location = new System.Drawing.Point(416, 8);
            this.mxMusicPlayer1.Name = "mxMusicPlayer1";
            this.mxMusicPlayer1.Radius = 10;
            this.mxMusicPlayer1.RoundBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(0)))), ((int)(((byte)(115)))));
            this.mxMusicPlayer1.Size = new System.Drawing.Size(314, 79);
            this.mxMusicPlayer1.TabIndex = 1;
            // 
            // bankListBox
            // 
            this.bankListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.bankListBox.DataSource = null;
            this.bankListBox.DisplayItemIndices = true;
            this.bankListBox.EnableDragReorder = false;
            this.bankListBox.EnableTOBJ = false;
            this.bankListBox.ForeColor = System.Drawing.Color.White;
            this.bankListBox.ImageHeight = 24;
            this.bankListBox.ItemHeight = 24;
            this.bankListBox.Location = new System.Drawing.Point(8, 90);
            this.bankListBox.Name = "bankListBox";
            this.bankListBox.SelectedIndex = -1;
            this.bankListBox.SelectedItem = null;
            this.bankListBox.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.bankListBox.ShowScrollbar = false;
            this.bankListBox.Size = new System.Drawing.Size(216, 466);
            this.bankListBox.StartingItemIndex = 0;
            this.bankListBox.TabIndex = 0;
            this.bankListBox.SelectedValueChanged += new System.EventHandler(this.bankListBox_SelectedValueChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(231, 119);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(499, 437);
            this.panel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 24);
            this.label1.TabIndex = 12;
            this.label1.Text = "Sounds:";
            // 
            // buttonExport
            // 
            this.buttonExport.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonExport.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonExport.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.buttonExport.BorderColor = System.Drawing.Color.Black;
            this.buttonExport.ForeColor = System.Drawing.Color.White;
            this.buttonExport.Image = global::mexTool.Properties.Resources.export;
            this.buttonExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExport.ImageHeight = 32;
            this.buttonExport.ImageWidth = 32;
            this.buttonExport.ImageXOffset = 10;
            this.buttonExport.Location = new System.Drawing.Point(314, 8);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(96, 52);
            this.buttonExport.TabIndex = 8;
            this.buttonExport.Text = "Export";
            this.buttonExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonClone
            // 
            this.buttonClone.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonClone.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonClone.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.buttonClone.BorderColor = System.Drawing.Color.Black;
            this.buttonClone.ForeColor = System.Drawing.Color.White;
            this.buttonClone.Image = global::mexTool.Properties.Resources.copy;
            this.buttonClone.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonClone.ImageHeight = 32;
            this.buttonClone.ImageWidth = 32;
            this.buttonClone.ImageXOffset = 10;
            this.buttonClone.Location = new System.Drawing.Point(212, 8);
            this.buttonClone.Name = "buttonClone";
            this.buttonClone.Size = new System.Drawing.Size(96, 52);
            this.buttonClone.TabIndex = 9;
            this.buttonClone.Text = "Clone";
            this.buttonClone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonClone.Click += new System.EventHandler(this.buttonClone_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonRemove.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonRemove.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.buttonRemove.BorderColor = System.Drawing.Color.Black;
            this.buttonRemove.ForeColor = System.Drawing.Color.White;
            this.buttonRemove.Image = global::mexTool.Properties.Resources.delete_fighter;
            this.buttonRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRemove.ImageHeight = 32;
            this.buttonRemove.ImageWidth = 32;
            this.buttonRemove.ImageXOffset = 10;
            this.buttonRemove.Location = new System.Drawing.Point(110, 8);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(96, 52);
            this.buttonRemove.TabIndex = 10;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // importButton
            // 
            this.importButton.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.importButton.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.importButton.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.importButton.BorderColor = System.Drawing.Color.Black;
            this.importButton.ForeColor = System.Drawing.Color.White;
            this.importButton.Image = global::mexTool.Properties.Resources.add_fighter;
            this.importButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.importButton.ImageHeight = 32;
            this.importButton.ImageWidth = 32;
            this.importButton.ImageXOffset = 10;
            this.importButton.Location = new System.Drawing.Point(8, 8);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(96, 52);
            this.importButton.TabIndex = 11;
            this.importButton.Text = "Import";
            this.importButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // SoundPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonClone);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.scriptTabButton);
            this.Controls.Add(this.bankTabButton);
            this.Controls.Add(this.mxMusicPlayer1);
            this.Controls.Add(this.bankListBox);
            this.Name = "SoundPage";
            this.Size = new System.Drawing.Size(733, 559);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MXListBox bankListBox;
        private Controls.MxMusicPlayer mxMusicPlayer1;
        private Controls.MXButton scriptTabButton;
        private Controls.MXButton bankTabButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Controls.MXButton buttonExport;
        private Controls.MXButton buttonClone;
        private Controls.MXButton buttonRemove;
        private Controls.MXButton importButton;
    }
}
