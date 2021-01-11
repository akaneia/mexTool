
namespace mexTool.GUI.Controls
{
    partial class MXEmblemEditor
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
            this.buttonRemove = new mexTool.GUI.Controls.MXButton();
            this.buttonAdd = new mexTool.GUI.Controls.MXButton();
            this.mxListBox1 = new mexTool.GUI.MXListBox();
            this.buttonExport = new mexTool.GUI.Controls.MXButton();
            this.SuspendLayout();
            // 
            // buttonRemove
            // 
            this.buttonRemove.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonRemove.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonRemove.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.buttonRemove.BorderColor = System.Drawing.Color.Black;
            this.buttonRemove.Image = global::mexTool.Properties.Resources.minus;
            this.buttonRemove.ImageHeight = 32;
            this.buttonRemove.ImageWidth = 32;
            this.buttonRemove.Location = new System.Drawing.Point(57, 3);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(48, 47);
            this.buttonRemove.TabIndex = 1;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonAdd.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonAdd.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.buttonAdd.BorderColor = System.Drawing.Color.Black;
            this.buttonAdd.Image = global::mexTool.Properties.Resources.plus;
            this.buttonAdd.ImageHeight = 32;
            this.buttonAdd.ImageWidth = 32;
            this.buttonAdd.Location = new System.Drawing.Point(3, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(48, 47);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // mxListBox1
            // 
            this.mxListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mxListBox1.DataSource = null;
            this.mxListBox1.DisplayItemIndices = true;
            this.mxListBox1.EnableDragReorder = false;
            this.mxListBox1.EnableTOBJ = true;
            this.mxListBox1.ImageHeight = 48;
            this.mxListBox1.ItemHeight = 48;
            this.mxListBox1.Location = new System.Drawing.Point(3, 56);
            this.mxListBox1.Name = "mxListBox1";
            this.mxListBox1.SelectedIndex = -1;
            this.mxListBox1.SelectedItem = null;
            this.mxListBox1.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.mxListBox1.ShowScrollbar = false;
            this.mxListBox1.Size = new System.Drawing.Size(476, 413);
            this.mxListBox1.StartingItemIndex = 0;
            this.mxListBox1.TabIndex = 0;
            // 
            // buttonExport
            // 
            this.buttonExport.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.buttonExport.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonExport.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.buttonExport.BorderColor = System.Drawing.Color.Black;
            this.buttonExport.Image = global::mexTool.Properties.Resources.export;
            this.buttonExport.ImageHeight = 32;
            this.buttonExport.ImageWidth = 32;
            this.buttonExport.Location = new System.Drawing.Point(111, 3);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(48, 47);
            this.buttonExport.TabIndex = 1;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // MXEmblemEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.mxListBox1);
            this.Name = "MXEmblemEditor";
            this.Size = new System.Drawing.Size(482, 472);
            this.ResumeLayout(false);

        }

        #endregion

        private MXListBox mxListBox1;
        private MXButton buttonAdd;
        private MXButton buttonRemove;
        private MXButton buttonExport;
    }
}
