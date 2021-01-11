
namespace mexTool.GUI.Pages
{
    partial class MenuPage
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
            this.buttonPlaylistTab = new mexTool.GUI.Controls.MXButton();
            this.buttonCSSTab = new mexTool.GUI.Controls.MXButton();
            this.buttonSSSTab = new mexTool.GUI.Controls.MXButton();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.buttonEmblemTab = new mexTool.GUI.Controls.MXButton();
            this.SuspendLayout();
            // 
            // buttonPlaylistTab
            // 
            this.buttonPlaylistTab.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonPlaylistTab.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonPlaylistTab.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.buttonPlaylistTab.BorderColor = System.Drawing.Color.Black;
            this.buttonPlaylistTab.ForeColor = System.Drawing.Color.White;
            this.buttonPlaylistTab.Location = new System.Drawing.Point(16, 15);
            this.buttonPlaylistTab.Name = "buttonPlaylistTab";
            this.buttonPlaylistTab.Size = new System.Drawing.Size(85, 23);
            this.buttonPlaylistTab.TabIndex = 2;
            this.buttonPlaylistTab.Text = "Menu Playlist";
            this.buttonPlaylistTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPlaylistTab.Click += new System.EventHandler(this.SelectTab);
            // 
            // buttonCSSTab
            // 
            this.buttonCSSTab.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonCSSTab.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonCSSTab.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.buttonCSSTab.BorderColor = System.Drawing.Color.Black;
            this.buttonCSSTab.ForeColor = System.Drawing.Color.White;
            this.buttonCSSTab.Location = new System.Drawing.Point(107, 15);
            this.buttonCSSTab.Name = "buttonCSSTab";
            this.buttonCSSTab.Size = new System.Drawing.Size(75, 23);
            this.buttonCSSTab.TabIndex = 2;
            this.buttonCSSTab.Text = "CSS Editor";
            this.buttonCSSTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCSSTab.Click += new System.EventHandler(this.SelectTab);
            // 
            // buttonSSSTab
            // 
            this.buttonSSSTab.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonSSSTab.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonSSSTab.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.buttonSSSTab.BorderColor = System.Drawing.Color.Black;
            this.buttonSSSTab.ForeColor = System.Drawing.Color.White;
            this.buttonSSSTab.Location = new System.Drawing.Point(188, 15);
            this.buttonSSSTab.Name = "buttonSSSTab";
            this.buttonSSSTab.Size = new System.Drawing.Size(75, 23);
            this.buttonSSSTab.TabIndex = 2;
            this.buttonSSSTab.Text = "SSS Editor";
            this.buttonSSSTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSSSTab.Click += new System.EventHandler(this.SelectTab);
            // 
            // contentPanel
            // 
            this.contentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contentPanel.Location = new System.Drawing.Point(16, 44);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(582, 428);
            this.contentPanel.TabIndex = 3;
            // 
            // buttonEmblemTab
            // 
            this.buttonEmblemTab.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonEmblemTab.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonEmblemTab.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.buttonEmblemTab.BorderColor = System.Drawing.Color.Black;
            this.buttonEmblemTab.ForeColor = System.Drawing.Color.White;
            this.buttonEmblemTab.Location = new System.Drawing.Point(269, 15);
            this.buttonEmblemTab.Name = "buttonEmblemTab";
            this.buttonEmblemTab.Size = new System.Drawing.Size(85, 23);
            this.buttonEmblemTab.TabIndex = 2;
            this.buttonEmblemTab.Text = "Emblems";
            this.buttonEmblemTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEmblemTab.Click += new System.EventHandler(this.SelectTab);
            // 
            // MenuPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.buttonSSSTab);
            this.Controls.Add(this.buttonCSSTab);
            this.Controls.Add(this.buttonEmblemTab);
            this.Controls.Add(this.buttonPlaylistTab);
            this.Name = "MenuPage";
            this.Size = new System.Drawing.Size(615, 487);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.MXButton buttonPlaylistTab;
        private Controls.MXButton buttonCSSTab;
        private Controls.MXButton buttonSSSTab;
        private System.Windows.Forms.Panel contentPanel;
        private Controls.MXButton buttonEmblemTab;
    }
}
