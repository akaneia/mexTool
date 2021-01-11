using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace mexTool.GUI.Controls
{
    public partial class MXProgressBar : UserControl
    {
        public int Value
        {
            get => _value;
            set
            {
                _value = Math.Max(Math.Min(value, Maximum), Minimum);
                OnValuePropertyChanged(new PropertyValueChangedEventArgs(null, _value));
                Invalidate();
            }
        }
        private int _value = 0;

        [Browsable(false)]
        public int MouseValue
        {
            get
            {
                return (int)(Minimum + (Maximum - Minimum) * PointToClient(MousePosition).X / (float)Width);
            }
        }

        public Color BarBackColor { get; set; } = Color.White;

        public Color BarProgressColor { get; set; } = Color.LightGray;

        public Color BarLineColor { get; set; } = Color.Black;

        public int Minimum { get; set; } = 0;

        public int Maximum { get; set; } = 100;

        public int TinyHeight { get; set; } = 24;

        [Category("Action")]
        public event EventHandler ValueChanged;

        private int _progress
        {
            get => (int)(((_value - Minimum) / (float)(Maximum - Minimum)) * Width);
        }

        private int _progressWidth = 0;

        private int _displayHeight = 24;

        public int LoopPosition { get; set; } = 0;

        private int _loopPosition
        {
            get => (int)(((LoopPosition - Minimum) / (float)(Maximum - Minimum)) * Width);
        }

        /// <summary>
        /// 
        /// </summary>
        public MXProgressBar()
        {
            InitializeComponent();

            _displayHeight = TinyHeight;



            DoubleBuffered = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MXProgressBar_Paint(object sender, PaintEventArgs e)
        {
            using (var brush = new SolidBrush(BarBackColor))
                e.Graphics.FillRectangle(brush, new RectangleF(0, 0, Width, _displayHeight));

            using (var brush = new SolidBrush(BarProgressColor))
                e.Graphics.FillRectangle(brush, new RectangleF(0, 0, _progressWidth, _displayHeight));

            if(LoopPosition != 0)
            {
                using (var brush = new SolidBrush(Color.FromArgb(100, 80, 80, 80)))
                    e.Graphics.FillRectangle(brush, new RectangleF(_loopPosition, 0, Width, _displayHeight));

                using (var pen = new Pen(Color.Black))
                    e.Graphics.DrawLine(pen, _loopPosition, 0, _loopPosition, _displayHeight);
            }

            if (ClientRectangle.Contains(PointToClient(MousePosition)))
            {
                var mouse = PointToClient(MousePosition).X;

                using (var pen = new Pen(Color.Black))
                    e.Graphics.DrawLine(pen, mouse, 0, mouse, _displayHeight);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnValuePropertyChanged(PropertyValueChangedEventArgs e)
        {
            EventHandler handler = ValueChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_progressWidth != _progress)
            {
                _progressWidth = (_progressWidth + _progress) / 2;
                Invalidate();
            }

            if (ClientRectangle.Contains(PointToClient(MousePosition)))
            {
                if(_displayHeight < Height)
                {
                    _displayHeight = (_displayHeight + Height) / 2;
                    Invalidate();
                }
            }
            else
            {
                if (_displayHeight > TinyHeight)
                {
                    _displayHeight = (_displayHeight + TinyHeight) / 2;
                    Invalidate();
                }
            }
        }
    }
}
