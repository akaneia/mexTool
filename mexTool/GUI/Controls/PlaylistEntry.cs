using mexTool.Core;
using System;
using System.Windows.Forms;

namespace mexTool.GUI.Controls
{
    public partial class PlaylistEntry : UserControl
    {
        public int Value { get => mxProgressBar1.Value; }

        public MEXMusic SelectedMusic { get => comboBox1.SelectedItem as MEXMusic; set => comboBox1.SelectedItem = value; }

        private MEXPlaylistEntry _entry;

        public MEXPlaylistEntry Entry { get => _entry; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        public PlaylistEntry(MEXPlaylistEntry entry)
        {
            InitializeComponent();

            comboBox1.DataSource = Core.MEX.BackgroundMusic;
            comboBox1.DropDownHeight = 400;

            mxProgressBar1.Value = entry.PlayChance;
            SelectedMusic = entry.Music;

            _entry = entry;
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Deleted;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDeleted(object sender, EventArgs e)
        {
            EventHandler handler = Deleted;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Played;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPlayed(object sender, EventArgs e)
        {
            EventHandler handler = Played;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            OnDeleted(this, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_entry != null)
                _entry.Music = SelectedMusic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxProgressBar1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mxProgressBar1.Value = mxProgressBar1.MouseValue;
                _entry.PlayChance = mxProgressBar1.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxProgressBar1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mxProgressBar1.Value = mxProgressBar1.MouseValue;
                _entry.PlayChance = mxProgressBar1.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_Resize(object sender, EventArgs e)
        {
            comboBox1.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playButton_Click(object sender, EventArgs e)
        {
            OnPlayed(this, EventArgs.Empty);
        }
    }
}
