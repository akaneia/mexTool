
namespace mexTool.GUI.Pages
{
    partial class FileSystemPage
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mxPropertyGrid1 = new mexTool.GUI.Controls.MXPropertyGrid();
            this.mxToolStrip1 = new mexTool.GUI.Controls.MXToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.treeView1.ItemHeight = 24;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(514, 485);
            this.treeView1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mxPropertyGrid1);
            this.splitContainer1.Size = new System.Drawing.Size(766, 485);
            this.splitContainer1.SplitterDistance = 514;
            this.splitContainer1.TabIndex = 3;
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
            this.mxPropertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.mxPropertyGrid1.Name = "mxPropertyGrid1";
            this.mxPropertyGrid1.Size = new System.Drawing.Size(248, 485);
            this.mxPropertyGrid1.TabIndex = 2;
            this.mxPropertyGrid1.ToolbarVisible = false;
            this.mxPropertyGrid1.ViewBackColor = System.Drawing.Color.Black;
            this.mxPropertyGrid1.ViewBorderColor = System.Drawing.Color.Transparent;
            this.mxPropertyGrid1.ViewForeColor = System.Drawing.Color.White;
            // 
            // mxToolStrip1
            // 
            this.mxToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.mxToolStrip1.Name = "mxToolStrip1";
            this.mxToolStrip1.Size = new System.Drawing.Size(766, 25);
            this.mxToolStrip1.TabIndex = 0;
            this.mxToolStrip1.Text = "mxToolStrip1";
            this.mxToolStrip1.Visible = false;
            // 
            // FileSystemPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mxToolStrip1);
            this.Name = "FileSystemPage";
            this.Size = new System.Drawing.Size(766, 513);
            this.Load += new System.EventHandler(this.FileSystemPage_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MXToolStrip mxToolStrip1;
        private System.Windows.Forms.TreeView treeView1;
        private Controls.MXPropertyGrid mxPropertyGrid1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
