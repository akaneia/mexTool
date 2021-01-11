using MeleeMedia.Audio;
using mexTool.Core;
using mexTool.Tools;
using System;
using System.Windows.Forms;

namespace mexTool.GUI.Controls
{
    public partial class PlaylistEditor : UserControl
    {
        private MEXPlaylist _playList;
        private DSPPlayer _player;

        /// <summary>
        /// 
        /// </summary>
        public PlaylistEditor()
        {
            InitializeComponent();

            _player = new DSPPlayer();
            var timer = new Timer();

            timer.Start();

            timer.Tick += (sender, args) =>
            {
                if (!Visible)
                    _player.Stop();
            };

            Disposed += (sender, args) => { _player.Dispose(); timer.Stop(); timer.Dispose(); };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playList"></param>
        public void SetPlaylist(MEXPlaylist playList)
        {
            _playList = playList;
            GenerateEditor();
        }

        /// <summary>
        /// 
        /// </summary>
        public void GenerateEditor()
        {
            foreach (Control c in panel1.Controls)
                c.Dispose();

            panel1.Controls.Clear();

            foreach(var entry in _playList.Entries)
                AddEditor(entry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        private void AddEditor(MEXPlaylistEntry entry)
        {
            var panel = new PlaylistEntry(entry);
            panel.Dock = DockStyle.Top;
            // self deletion
            panel.Deleted += (sender, args) =>
            {
                if(sender is PlaylistEntry e)
                {
                    panel1.Controls.Remove(e);
                    e.Dispose();
                    _playList.Entries.Remove(e.Entry);
                }
            };
            panel.Played += (sender, args) =>
            {
                if(_player.IsPlaying)
                {
                    _player.Stop();
                }
                else
                {
                    var music = ((PlaylistEntry)sender).SelectedMusic;

                    var data = MEX.ImageResource.GetFile("audio\\" + music.FileName);

                    if (data != null)
                    {
                        _player.Stop();
                        _player.LoadDSP(HPS.ToDSP(data), ApplicationSettings.DefaultDevice);
                        _player.Position = TimeSpan.Zero;
                        _player.Play();
                    }
                }
            };
            panel1.Controls.Add(panel);
            panel.BringToFront();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var newEntry = new MEXPlaylistEntry() { Music = MEX.BackgroundMusic[0], PlayChance = 50 };

            _playList.Entries.Add(newEntry);

            AddEditor(newEntry);
        }
    }
}
