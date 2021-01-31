
namespace mexTool.GUI.Controls
{
    partial class SoundEditor
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.endTime = new System.Windows.Forms.TextBox();
            this.loopTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timeStamp = new System.Windows.Forms.Label();
            this.mxButton1 = new mexTool.GUI.Controls.MXButton();
            this.panel1 = new mexTool.GUI.Controls.DoubleBufferedPanel();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 24;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // endTime
            // 
            this.endTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.endTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.endTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.endTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.endTime.ForeColor = System.Drawing.Color.White;
            this.endTime.Location = new System.Drawing.Point(487, 331);
            this.endTime.Name = "endTime";
            this.endTime.Size = new System.Drawing.Size(288, 25);
            this.endTime.TabIndex = 10;
            this.endTime.TextChanged += new System.EventHandler(this.endTime_TextChanged);
            // 
            // loopTime
            // 
            this.loopTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.loopTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.loopTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.loopTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.loopTime.ForeColor = System.Drawing.Color.White;
            this.loopTime.Location = new System.Drawing.Point(25, 331);
            this.loopTime.Name = "loopTime";
            this.loopTime.Size = new System.Drawing.Size(287, 25);
            this.loopTime.TabIndex = 11;
            this.loopTime.TextChanged += new System.EventHandler(this.loopTime_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(22, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 30);
            this.label1.TabIndex = 12;
            this.label1.Text = "Loop Point";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(700, 298);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 30);
            this.label2.TabIndex = 13;
            this.label2.Text = "Length";
            // 
            // timeStamp
            // 
            this.timeStamp.AutoSize = true;
            this.timeStamp.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeStamp.ForeColor = System.Drawing.Color.White;
            this.timeStamp.Location = new System.Drawing.Point(22, 16);
            this.timeStamp.Name = "timeStamp";
            this.timeStamp.Size = new System.Drawing.Size(0, 30);
            this.timeStamp.TabIndex = 14;
            // 
            // mxButton1
            // 
            this.mxButton1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mxButton1.BackColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.mxButton1.BackColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.mxButton1.BackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.mxButton1.BorderColor = System.Drawing.Color.Black;
            this.mxButton1.Image = global::mexTool.Properties.Resources.play;
            this.mxButton1.ImageHeight = 32;
            this.mxButton1.ImageWidth = 32;
            this.mxButton1.Location = new System.Drawing.Point(348, 298);
            this.mxButton1.Name = "mxButton1";
            this.mxButton1.Size = new System.Drawing.Size(100, 45);
            this.mxButton1.TabIndex = 2;
            this.mxButton1.Click += new System.EventHandler(this.mxButton1_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(65)))));
            this.panel1.Location = new System.Drawing.Point(25, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 224);
            this.panel1.TabIndex = 1;
            this.panel1.TabStop = true;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // SoundEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.Controls.Add(this.timeStamp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loopTime);
            this.Controls.Add(this.endTime);
            this.Controls.Add(this.mxButton1);
            this.Controls.Add(this.panel1);
            this.Name = "SoundEditor";
            this.Size = new System.Drawing.Size(800, 359);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SoundEditor_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private mexTool.GUI.Controls.DoubleBufferedPanel panel1;
        private MXButton mxButton1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox endTime;
        private System.Windows.Forms.TextBox loopTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label timeStamp;
    }
}
