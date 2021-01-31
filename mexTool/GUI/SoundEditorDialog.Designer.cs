
namespace mexTool.GUI
{
    partial class SoundEditorDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoundEditorDialog));
            this.editor = new mexTool.GUI.Controls.SoundEditor();
            this.mxButton1 = new mexTool.GUI.Controls.MXButton();
            this.SuspendLayout();
            // 
            // editor
            // 
            this.editor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editor.Location = new System.Drawing.Point(0, 0);
            this.editor.Name = "editor";
            this.editor.Size = new System.Drawing.Size(800, 427);
            this.editor.TabIndex = 0;
            // 
            // mxButton1
            // 
            this.mxButton1.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.mxButton1.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.mxButton1.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.mxButton1.BorderColor = System.Drawing.Color.Black;
            this.mxButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mxButton1.ForeColor = System.Drawing.Color.White;
            this.mxButton1.Location = new System.Drawing.Point(0, 427);
            this.mxButton1.Name = "mxButton1";
            this.mxButton1.Size = new System.Drawing.Size(800, 23);
            this.mxButton1.TabIndex = 1;
            this.mxButton1.Text = "Done";
            this.mxButton1.Click += new System.EventHandler(this.mxButton1_Click);
            // 
            // SoundEditorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.editor);
            this.Controls.Add(this.mxButton1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SoundEditorDialog";
            this.Text = "Loop Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private mexTool.GUI.Controls.SoundEditor editor;
        private Controls.MXButton mxButton1;
    }
}