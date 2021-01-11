
namespace mexTool.GUI.Controls
{
    partial class SelectScreenRenderer
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
            this.frameTimer = new System.Windows.Forms.Timer(this.components);
            this.drawPanel = new mexTool.GUI.Controls.DoubleBufferedPanel();
            this.mxToolStrip1 = new mexTool.GUI.Controls.MXToolStrip();
            this.playButton = new System.Windows.Forms.ToolStripButton();
            this.startEndToggle = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.undoButton = new System.Windows.Forms.ToolStripButton();
            this.redoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.backColorItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridColorItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.snapButton = new System.Windows.Forms.ToolStripButton();
            this.enableGridButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.gridWidthBox = new System.Windows.Forms.ToolStripTextBox();
            this.gridHeightLabel = new System.Windows.Forms.ToolStripLabel();
            this.gridHeightBox = new System.Windows.Forms.ToolStripTextBox();
            this.buttonResetAnimation = new System.Windows.Forms.ToolStripButton();
            this.mxToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // frameTimer
            // 
            this.frameTimer.Interval = 16;
            this.frameTimer.Tick += new System.EventHandler(this.frameTimer_Tick);
            // 
            // drawPanel
            // 
            this.drawPanel.BackColor = System.Drawing.Color.DimGray;
            this.drawPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawPanel.ForeColor = System.Drawing.Color.Black;
            this.drawPanel.Location = new System.Drawing.Point(0, 40);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(720, 510);
            this.drawPanel.TabIndex = 1;
            this.drawPanel.TabStop = true;
            this.drawPanel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.drawPanel_Scroll);
            this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanel_Paint);
            this.drawPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseDown);
            this.drawPanel.MouseLeave += new System.EventHandler(this.drawPanel_MouseLeave);
            this.drawPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseMove);
            this.drawPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseUp);
            // 
            // mxToolStrip1
            // 
            this.mxToolStrip1.AutoSize = false;
            this.mxToolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.mxToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playButton,
            this.startEndToggle,
            this.buttonResetAnimation,
            this.toolStripSeparator1,
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator3,
            this.toolStripDropDownButton1,
            this.toolStripSeparator2,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator4,
            this.snapButton,
            this.enableGridButton,
            this.toolStripLabel4,
            this.gridWidthBox,
            this.gridHeightLabel,
            this.gridHeightBox});
            this.mxToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.mxToolStrip1.Name = "mxToolStrip1";
            this.mxToolStrip1.Size = new System.Drawing.Size(720, 40);
            this.mxToolStrip1.TabIndex = 0;
            this.mxToolStrip1.Text = "mxToolStrip1";
            // 
            // playButton
            // 
            this.playButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.playButton.Image = global::mexTool.Properties.Resources.play;
            this.playButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(36, 37);
            this.playButton.Text = "Play Animation";
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // startEndToggle
            // 
            this.startEndToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.startEndToggle.Image = global::mexTool.Properties.Resources.end;
            this.startEndToggle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startEndToggle.Name = "startEndToggle";
            this.startEndToggle.Size = new System.Drawing.Size(36, 37);
            this.startEndToggle.Text = "Start/End Position Toggle";
            this.startEndToggle.Click += new System.EventHandler(this.startEndToggle_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(25, 40);
            // 
            // undoButton
            // 
            this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoButton.Image = global::mexTool.Properties.Resources.undo;
            this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(36, 37);
            this.undoButton.Text = "Undo Edit";
            this.undoButton.Click += new System.EventHandler(this.undoButton_Click);
            // 
            // redoButton
            // 
            this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoButton.Image = global::mexTool.Properties.Resources.redo;
            this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(36, 37);
            this.redoButton.Text = "Redo Edit";
            this.redoButton.Click += new System.EventHandler(this.redoButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(25, 40);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.BackColor = System.Drawing.Color.Black;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backColorItem,
            this.gridColorItem});
            this.toolStripDropDownButton1.ForeColor = System.Drawing.Color.White;
            this.toolStripDropDownButton1.Image = global::mexTool.Properties.Resources.color;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(45, 37);
            this.toolStripDropDownButton1.Text = "Colors";
            // 
            // backColorItem
            // 
            this.backColorItem.BackColor = System.Drawing.SystemColors.Control;
            this.backColorItem.ForeColor = System.Drawing.Color.White;
            this.backColorItem.Image = global::mexTool.Properties.Resources.color;
            this.backColorItem.Name = "backColorItem";
            this.backColorItem.Size = new System.Drawing.Size(131, 22);
            this.backColorItem.Text = "Back Color";
            this.backColorItem.Click += new System.EventHandler(this.backColorItem_Click);
            // 
            // gridColorItem
            // 
            this.gridColorItem.ForeColor = System.Drawing.Color.White;
            this.gridColorItem.Image = global::mexTool.Properties.Resources.color;
            this.gridColorItem.Name = "gridColorItem";
            this.gridColorItem.Size = new System.Drawing.Size(131, 22);
            this.gridColorItem.Text = "Grid Color";
            this.gridColorItem.Click += new System.EventHandler(this.backColorItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(25, 40);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::mexTool.Properties.Resources.zoom_in;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 37);
            this.toolStripButton1.Text = "Zoom In";
            this.toolStripButton1.Click += new System.EventHandler(this.ZoomIn);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::mexTool.Properties.Resources.zoom_out;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(36, 37);
            this.toolStripButton2.Text = "Zoom Out";
            this.toolStripButton2.Click += new System.EventHandler(this.ZoomOut);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(25, 40);
            // 
            // snapButton
            // 
            this.snapButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.snapButton.Enabled = false;
            this.snapButton.Image = global::mexTool.Properties.Resources.snap_enable;
            this.snapButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.snapButton.Name = "snapButton";
            this.snapButton.Size = new System.Drawing.Size(36, 37);
            this.snapButton.Text = "Enable Icon Snap";
            this.snapButton.Click += new System.EventHandler(this.snapButton_Click);
            // 
            // enableGridButton
            // 
            this.enableGridButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.enableGridButton.Image = global::mexTool.Properties.Resources.grid;
            this.enableGridButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.enableGridButton.Name = "enableGridButton";
            this.enableGridButton.Size = new System.Drawing.Size(36, 37);
            this.enableGridButton.Text = "Toggle Alignment Grid";
            this.enableGridButton.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(21, 37);
            this.toolStripLabel4.Text = "W:";
            // 
            // gridWidthBox
            // 
            this.gridWidthBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gridWidthBox.Name = "gridWidthBox";
            this.gridWidthBox.Size = new System.Drawing.Size(40, 40);
            this.gridWidthBox.Text = "40";
            this.gridWidthBox.TextChanged += new System.EventHandler(this.xOffTextBox_TextChanged);
            // 
            // gridHeightLabel
            // 
            this.gridHeightLabel.ForeColor = System.Drawing.Color.White;
            this.gridHeightLabel.Name = "gridHeightLabel";
            this.gridHeightLabel.Size = new System.Drawing.Size(19, 37);
            this.gridHeightLabel.Text = "H:";
            // 
            // gridHeightBox
            // 
            this.gridHeightBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gridHeightBox.Name = "gridHeightBox";
            this.gridHeightBox.Size = new System.Drawing.Size(40, 40);
            this.gridHeightBox.Text = "40";
            this.gridHeightBox.TextChanged += new System.EventHandler(this.xOffTextBox_TextChanged);
            // 
            // buttonResetAnimation
            // 
            this.buttonResetAnimation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonResetAnimation.Image = global::mexTool.Properties.Resources.replace;
            this.buttonResetAnimation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonResetAnimation.Name = "buttonResetAnimation";
            this.buttonResetAnimation.Size = new System.Drawing.Size(36, 37);
            this.buttonResetAnimation.Text = "Reset Animation";
            this.buttonResetAnimation.Click += new System.EventHandler(this.buttonResetAnimation_Click);
            // 
            // SelectScreenRenderer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.drawPanel);
            this.Controls.Add(this.mxToolStrip1);
            this.Name = "SelectScreenRenderer";
            this.Size = new System.Drawing.Size(720, 550);
            this.mxToolStrip1.ResumeLayout(false);
            this.mxToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer frameTimer;
        private MXToolStrip mxToolStrip1;
        private DoubleBufferedPanel drawPanel;
        private System.Windows.Forms.ToolStripButton undoButton;
        private System.Windows.Forms.ToolStripButton redoButton;
        private System.Windows.Forms.ToolStripButton playButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton startEndToggle;
        private System.Windows.Forms.ToolStripButton enableGridButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem backColorItem;
        private System.Windows.Forms.ToolStripMenuItem gridColorItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox gridWidthBox;
        private System.Windows.Forms.ToolStripLabel gridHeightLabel;
        private System.Windows.Forms.ToolStripTextBox gridHeightBox;
        private System.Windows.Forms.ToolStripButton snapButton;
        private System.Windows.Forms.ToolStripButton buttonResetAnimation;
    }
}
