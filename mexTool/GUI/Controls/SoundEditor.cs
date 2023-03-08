using MeleeMedia.Audio;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace mexTool.GUI.Controls
{
    public partial class SoundEditor : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        private DSP _dsp;

        private bool _stopSliderUpdate;

        private float percent = 0;

        private float HandleLeft = 0;

        private float HandleLeftPosition { get => HandleLeft * panel1.Width; }

        private bool HandleLeftSelected = false;

        private float HandleRight = 1;
        private float HandleRightPosition { get => HandleRight * panel1.Width; }

        private bool HandleRightSelected = false;

        private bool Dragging = false;

        private Tools.DSPPlayer _player;

        /// <summary>
        /// 
        /// </summary>
        public SoundEditor()
        {
            InitializeComponent();

            _player = new Tools.DSPPlayer();

            panel1.ForceFocus = false;

            Disposed += (sender, args) =>
            {
                _player.Stop();
                _player.Dispose();
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public void ApplyTrimming()
        {
            var len = _dsp.Length;
            foreach(var c in _dsp.Channels)
            {
                var newLength = (EndPoint.TotalMilliseconds / 1750f) * _dsp.Frequency;

                Array.Resize(ref c.Data, (int)newLength);
                c.NibbleCount = c.Data.Length * 2;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dsp"></param>
        public void SetSound(DSP dsp)
        {
            _dsp = dsp;
            _player.LoadDSP(_dsp, ApplicationSettings.DefaultDevice);
            HandleRight = 1;
            HandleLeft = (float)(TimeSpan.Parse(dsp.LoopPoint).TotalMilliseconds / TimeSpan.Parse(dsp.Length).TotalMilliseconds);

            loopTime.Text = dsp.LoopPoint;
            endTime.Text = dsp.Length;

            channel0 = Sample(GcAdpcmDecoder.Decode(_dsp.Channels[0].Data, _dsp.Channels[0].COEF));
            if (_dsp.Channels.Count > 1)
                channel1 = Sample(GcAdpcmDecoder.Decode(_dsp.Channels[1].Data, _dsp.Channels[1].COEF));
        }

        private short[] channel0;
        private short[] channel1;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private short[] Sample(short[] channel)
        {
            var sampling = 1;

            if (channel.Length >= 400)
                sampling = channel.Length / 400;

            short[] o = new short[channel.Length / sampling];

            for (int i = 0; i < o.Length; i++)
                o[i] = channel[i * sampling];

            return o;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if(_dsp != null)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                DrawChannel(e.Graphics, channel0, Color.FromArgb(0, 195, 255), true);
                if (channel1 != null)
                    DrawChannel(e.Graphics, channel1, Color.FromArgb(0, 115, 255), false);
                else
                    DrawChannel(e.Graphics, channel0, Color.FromArgb(0, 115, 255), false);

                using (var pen = new Pen(Color.FromArgb(150, 230, 255), 2))
                {
                    e.Graphics.DrawLine(pen, 0, panel1.Height / 2, panel1.Width, panel1.Height / 2);
                    e.Graphics.DrawLine(pen, percent * panel1.Width, 0, percent * panel1.Width, panel1.Height);
                }

                using (var region = new SolidBrush(Color.FromArgb(80, 130, 200, 255)))
                {
                    e.Graphics.FillRectangle(region, HandleLeftPosition, 0, HandleRightPosition - HandleLeftPosition, panel1.Height);
                }

                using (var region = new SolidBrush(Color.FromArgb(120, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(region, HandleRightPosition, 0, panel1.Width - HandleRightPosition, panel1.Height);
                }

                using (var pen = new Pen(Color.FromArgb(150, 230, 255), 4))
                using (var selectedPen = new Pen(Color.FromArgb(255, 230, 180), 4))
                {
                    e.Graphics.DrawLine(HandleRightSelected ? selectedPen : pen, HandleRightPosition, 0, HandleRightPosition, panel1.Height);
                    e.Graphics.DrawLine(HandleLeftSelected ? selectedPen : pen, HandleLeftPosition, 0, HandleLeftPosition, panel1.Height);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DrawChannel(Graphics g, short[] channel, Color color, bool top)
        {
            var height = panel1.Height;

            using (var brush = new SolidBrush(color))
            {
                PointF[] points = new PointF[channel.Length + 1];
                for (int i = 0; i < channel.Length; i++)
                {
                    var x1 = (i / (float)channel.Length) * panel1.Width;
                    var y1 = ((channel[i] + 32768) / (float)ushort.MaxValue) * (height / 2);
                    points[i] = new PointF(x1, (panel1.Height / 2) + y1 * (top ? 1 : -1));
                }
                points[0] = new PointF(0, (panel1.Height / 2));
                points[points.Length - 1] = new PointF(panel1.Width, (panel1.Height / 2));
                g.FillPolygon(brush, points);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Resize(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxButton1_Click(object sender, EventArgs e)
        {
            if(!_player.IsPlaying)
            {
                _player.Play();
                mxButton1.Image = Properties.Resources.pause;
            }
            else
            {
                _player.Pause();
                _player.Position = _player.LoopPoint;
                mxButton1.Image = Properties.Resources.play;
            }
        }


        public TimeSpan EndPoint { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan position = _player.Position;
            TimeSpan length = _player.Length;
            if (position > length)
                length = position;

            timeStamp.Text = $"{position.ToString()} / {length.ToString()}";

            if (!_stopSliderUpdate &&
                length != TimeSpan.Zero &&
                position != TimeSpan.Zero)
            {
                percent = (float)(position.TotalMilliseconds / length.TotalMilliseconds);

                panel1.Invalidate();
            }

            if (EndPoint != TimeSpan.Zero && _player.Position > EndPoint)
                _player.Position = _player.LoopPoint;

            if (!Visible)
                _player.Stop();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mousePosition"></param>
        private void SeekMousePosition(int mousePosition)
        {
            _player.Position = GetSeekTime(mousePosition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private TimeSpan GetSeekTime(int mousePosition)
        {
            // clamp mouse position
            mousePosition = Math.Min(Math.Max(mousePosition, 0), panel1.Width);

            percent = mousePosition / (float)panel1.Width;
            if (!double.IsNaN(_player.Length.TotalMilliseconds * percent))
                return TimeSpan.FromMilliseconds(_player.Length.TotalMilliseconds * percent);

            return TimeSpan.Zero;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Default;

            if (_stopSliderUpdate)
                SeekMousePosition(e.X);

            if (!Dragging)
            {
                HandleRightSelected = false;
                HandleLeftSelected = false;

                if (Math.Abs(e.X - HandleLeftPosition) < 4f)
                {
                    HandleLeftSelected = true;
                    Cursor = Cursors.SizeWE;
                }

                if (Math.Abs(e.X - HandleRightPosition) < 4f)
                {
                    HandleRightSelected = true;
                    HandleLeftSelected = false;
                    Cursor = Cursors.SizeWE;
                }
            }
            else
            {
                Cursor = Cursors.SizeWE;

                if (HandleLeftSelected)
                    loopTime.Text = GetSeekTime(e.X).ToString();

                if (HandleRightSelected)
                    endTime.Text = GetSeekTime(e.X).ToString();

                if (HandleLeft > HandleRight)
                    HandleLeft = HandleRight;

                if (HandleRight < HandleLeft)
                    HandleRight = HandleLeft;
            }

            panel1.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = false;

            if(e.Button == MouseButtons.Left)
            {
                if (HandleRightSelected || HandleLeftSelected)
                {
                    Dragging = true;
                }
                else
                {
                    _stopSliderUpdate = true;
                    SeekMousePosition(e.X);
                    panel1.Invalidate();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            Dragging = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Default;
            Dragging = false;

            if (e.Button == MouseButtons.Left)
                _stopSliderUpdate = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loopTime_TextChanged(object sender, EventArgs e)
        {
            loopTime.BackColor = Color.FromArgb(40, 40, 60);
            if (TimeSpan.TryParse(loopTime.Text, out TimeSpan ts))
            {
                if (ts < TimeSpan.Zero)
                    loopTime.Text = TimeSpan.Zero.ToString();
                else
                if (ts > TimeSpan.Parse(_dsp.Length))
                    loopTime.Text = _dsp.Length;
                else
                {
                    _dsp.SetLoopFromTimeSpan(ts);
                    _player.LoopPoint = ts;
                    HandleLeft = (float)(ts.TotalMilliseconds / TimeSpan.Parse(_dsp.Length).TotalMilliseconds);
                }
            }
            else
            {
                //loopTime.Text = _dsp.LoopPoint;
                loopTime.BackColor = Color.Red;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endTime_TextChanged(object sender, EventArgs e)
        {
            endTime.BackColor = Color.FromArgb(40, 40, 60);
            if (TimeSpan.TryParse(endTime.Text, out TimeSpan ts))
            {
                if (ts < TimeSpan.Zero)
                    endTime.Text = TimeSpan.Zero.ToString();
                else
                if (ts > TimeSpan.Parse(_dsp.Length))
                    endTime.Text = _dsp.Length;
                else
                {
                    EndPoint = ts;
                    HandleRight = (float)(ts.TotalMilliseconds / TimeSpan.Parse(_dsp.Length).TotalMilliseconds);
                }
            }
            else
            {
                //endTime.Text = EndPoint.ToString();
                endTime.BackColor = Color.Red;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoundEditor_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                mxButton1_Click(mxButton1, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));

            if (e.KeyCode == Keys.Left)
                _player.Position -= TimeSpan.FromMilliseconds(100);

            if (e.KeyCode == Keys.Right)
                _player.Position += TimeSpan.FromMilliseconds(100);
        }
    }
}
