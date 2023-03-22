using MeleeMedia.Audio;
using mexTool.Tools;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace mexTool.GUI.Controls
{
    public partial class MxMusicPlayer : UserControl
    {
        private DSPPlayer _player;

        private bool _stopSliderUpdate;

        public int Radius { get; set; } = 10;

        public Color RoundBackColor { get; set; } = Color.FromArgb(216, 0, 115);

        public bool ProgressBarVisible { get => soundProgressBar.Visible; set => soundProgressBar.Visible = value; }

        /// <summary>
        /// 
        /// </summary>
        public DSP DSP
        {
            get => _dsp;
            set
            {
                _dsp = value;

                if (_player != null)
                {
                    StopSound();

                    _player.LoadDSP(_dsp, ApplicationSettings.DefaultDevice);
                    _player.Position = TimeSpan.Zero;

                    soundProgressBar.Value = 0;
                    soundProgressBar.Maximum = _player.Length.Milliseconds;

                    soundProgressBar.LoopPosition = 0;
                    soundProgressBar.EnableLoop = false;
                    if (_dsp != null)
                    {
                        soundProgressBar.EnableLoop = _dsp.LoopSound;
                        soundProgressBar.LoopPosition = (int)(_dsp.Channels[0].LoopStart / 2 / (double)_dsp.Frequency * 1.75f * 1000);

                        label2.Text = "Loop Point: " + DSP.LoopPoint;
                    }

                    // try to cleanup removing old dsp
                    GC.Collect();
                }
            }
        }
        private DSP _dsp;

        public string NowPlaying { set => labelNowPlaying.Text = $"Now Playing: {value}"; }

        /// <summary>
        /// 
        /// </summary>
        public MxMusicPlayer()
        {
            InitializeComponent();

            _player = new DSPPlayer();

            DoubleBuffered = true;

            /*_player.PlaybackStopped += (sender, args) =>
            {
                if (_dsp != null &&
                _dsp.Channels.Count > 0 &&
                _dsp.Channels[0].LoopStart != 0 &&
                _player.Position == _player.Length)
                {
                    var mill = (int)(_dsp.Channels[0].LoopStart / 2 / (double)_dsp.Frequency * 1.75f * 1000);
                    _player.Position = TimeSpan.FromMilliseconds(mill);
                    PlaySound();
                }
                else
                    PauseSound();
            };*/

            soundProgressBar.ValueChanged += (sender, args) =>
            {
                if (_stopSliderUpdate)
                {
                    double perc = soundProgressBar.Value / (double)soundProgressBar.Maximum;
                    if(!double.IsNaN(_player.Length.TotalMilliseconds * perc))
                        _player.Position = TimeSpan.FromMilliseconds(_player.Length.TotalMilliseconds * perc);
                }
            };

            Disposed += (sender, args) =>
            {
                _player.Dispose();
            };

            SetStyle(ControlStyles.ResizeRedraw, true);
        }


        /// <summary>
        /// 
        /// </summary>
        public void PauseSound()
        {
            _player.Pause();
            buttonPlay.Visible = true;
            buttonPause.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void PlaySound()
        {
            if (_player.Position == _player.Length)
                _player.Position = TimeSpan.Zero;

            _player.Play();
            buttonPlay.Visible = false;
            buttonPause.Visible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void StopSound()
        {
            PauseSound();
            soundProgressBar.Value = 0;
            _player.Stop();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (sender == buttonPlay)
                PlaySound();
            if (sender == buttonPause)
                PauseSound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStop_Click(object sender, EventArgs e)
        {
            StopSound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void soundProgressBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _stopSliderUpdate = true;
                soundProgressBar.Value = soundProgressBar.MouseValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void soundProgressBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _stopSliderUpdate = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void soundProgressBar_MouseMove(object sender, MouseEventArgs e)
        {
            if(_stopSliderUpdate)
                soundProgressBar.Value = soundProgressBar.MouseValue;
        }

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

            label1.Text = String.Format(@"{0:mm\:ss} / {1:mm\:ss}", position, length);

            if (!_stopSliderUpdate &&
                length != TimeSpan.Zero && 
                position != TimeSpan.Zero)
            {
                double perc = position.TotalMilliseconds / length.TotalMilliseconds * soundProgressBar.Maximum;
                soundProgressBar.Value = (int)perc;
            }

            if(!Visible)
                StopSound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MxMusicPlayer_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (var brush = new SolidBrush(RoundBackColor))
                e.Graphics.FillRoundedRectangle(brush, ClientRectangle, Radius);
        }
    }
}
