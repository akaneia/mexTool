
namespace mexTool.GUI.Pages
{
    partial class CodesPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodesPage));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.codeList = new mexTool.GUI.MXListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCodeDescription = new System.Windows.Forms.TextBox();
            this.tbCodeData = new System.Windows.Forms.TextBox();
            this.tbCreator = new System.Windows.Forms.TextBox();
            this.tbCodeName = new System.Windows.Forms.TextBox();
            this.mxToolStrip2 = new mexTool.GUI.Controls.MXToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.mxToolStrip1 = new mexTool.GUI.Controls.MXToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.mxToolStrip2.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.codeList);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.labelError);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.tbCodeDescription);
            this.splitContainer1.Panel2.Controls.Add(this.tbCodeData);
            this.splitContainer1.Panel2.Controls.Add(this.tbCreator);
            this.splitContainer1.Panel2.Controls.Add(this.tbCodeName);
            this.splitContainer1.Size = new System.Drawing.Size(766, 485);
            this.splitContainer1.SplitterDistance = 226;
            this.splitContainer1.TabIndex = 3;
            // 
            // codeList
            // 
            this.codeList.CheckboxSize = new System.Drawing.Size(24, 24);
            this.codeList.DataSource = null;
            this.codeList.DisplayItemIndices = false;
            this.codeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeList.EnableCheckBoxes = true;
            this.codeList.EnableDragReorder = false;
            this.codeList.EnableTOBJ = false;
            this.codeList.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeList.ForeColor = System.Drawing.Color.White;
            this.codeList.ImageHeight = 24;
            this.codeList.ItemHeight = 24;
            this.codeList.Location = new System.Drawing.Point(0, 20);
            this.codeList.Name = "codeList";
            this.codeList.SelectedIndex = -1;
            this.codeList.SelectedItem = null;
            this.codeList.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.codeList.ShowScrollbar = false;
            this.codeList.Size = new System.Drawing.Size(226, 465);
            this.codeList.StartingItemIndex = 0;
            this.codeList.TabIndex = 0;
            this.codeList.SelectedValueChanged += new System.EventHandler(this.codeList_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Code List:";
            // 
            // labelError
            // 
            this.labelError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelError.AutoSize = true;
            this.labelError.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelError.ForeColor = System.Drawing.Color.White;
            this.labelError.Location = new System.Drawing.Point(13, 453);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(17, 20);
            this.labelError.TabIndex = 2;
            this.labelError.Text = "  ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(13, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Code:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(13, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(13, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "Creator:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name:";
            // 
            // tbCodeDescription
            // 
            this.tbCodeDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCodeDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbCodeDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCodeDescription.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCodeDescription.ForeColor = System.Drawing.Color.White;
            this.tbCodeDescription.Location = new System.Drawing.Point(17, 101);
            this.tbCodeDescription.Multiline = true;
            this.tbCodeDescription.Name = "tbCodeDescription";
            this.tbCodeDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbCodeDescription.Size = new System.Drawing.Size(503, 66);
            this.tbCodeDescription.TabIndex = 1;
            this.tbCodeDescription.TextChanged += new System.EventHandler(this.tbCodeDescription_TextChanged);
            // 
            // tbCodeData
            // 
            this.tbCodeData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCodeData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbCodeData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCodeData.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCodeData.ForeColor = System.Drawing.Color.White;
            this.tbCodeData.Location = new System.Drawing.Point(17, 193);
            this.tbCodeData.Multiline = true;
            this.tbCodeData.Name = "tbCodeData";
            this.tbCodeData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbCodeData.Size = new System.Drawing.Size(503, 257);
            this.tbCodeData.TabIndex = 1;
            this.tbCodeData.TextChanged += new System.EventHandler(this.tbCodeData_TextChanged);
            // 
            // tbCreator
            // 
            this.tbCreator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCreator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbCreator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCreator.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCreator.ForeColor = System.Drawing.Color.White;
            this.tbCreator.Location = new System.Drawing.Point(85, 44);
            this.tbCreator.Name = "tbCreator";
            this.tbCreator.Size = new System.Drawing.Size(435, 26);
            this.tbCreator.TabIndex = 0;
            this.tbCreator.TextChanged += new System.EventHandler(this.tbCreator_TextChanged);
            // 
            // tbCodeName
            // 
            this.tbCodeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCodeName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbCodeName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCodeName.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCodeName.ForeColor = System.Drawing.Color.White;
            this.tbCodeName.Location = new System.Drawing.Point(85, 12);
            this.tbCodeName.Name = "tbCodeName";
            this.tbCodeName.Size = new System.Drawing.Size(435, 26);
            this.tbCodeName.TabIndex = 0;
            this.tbCodeName.TextChanged += new System.EventHandler(this.tbCodeName_TextChanged);
            // 
            // mxToolStrip2
            // 
            this.mxToolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mxToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.mxToolStrip2.Location = new System.Drawing.Point(0, 0);
            this.mxToolStrip2.Name = "mxToolStrip2";
            this.mxToolStrip2.Size = new System.Drawing.Size(766, 31);
            this.mxToolStrip2.Stretch = true;
            this.mxToolStrip2.TabIndex = 7;
            this.mxToolStrip2.Text = "mxToolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::mexTool.Properties.Resources.plus;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::mexTool.Properties.Resources.minus;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.Visible = false;
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
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
            // CodesPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.mxToolStrip2);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mxToolStrip1);
            this.Name = "CodesPage";
            this.Size = new System.Drawing.Size(766, 513);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.mxToolStrip2.ResumeLayout(false);
            this.mxToolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MXToolStrip mxToolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MXListBox codeList;
        private System.Windows.Forms.TextBox tbCodeData;
        private System.Windows.Forms.TextBox tbCodeName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCodeDescription;
        private Controls.MXToolStrip mxToolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbCreator;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
    }
}
