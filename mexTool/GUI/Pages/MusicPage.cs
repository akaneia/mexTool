using MeleeMedia.Audio;
using mexTool.Core;
using System.IO;
using System.Windows.Forms;

namespace mexTool.GUI.Pages
{
    public partial class MusicPage : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public MusicPage()
        {
            InitializeComponent();

            mxListBox1.DataSource = MEX.BackgroundMusic;

            Disposed += (sender, args) =>
            {
                mxListBox1.DataSource = null;
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public void StopSound()
        {
            soundPlayer.StopSound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxListBox1_SelectedObjectChanged(object sender, System.EventArgs e)
        {
            if (mxListBox1.SelectedItem is MEXMusic music)
            {
                var data = MEX.ImageResource.GetFile("audio\\" + music.FileName);
                if (data != null)
                {
                    soundPlayer.NowPlaying = music.ToString();
                    soundPlayer.DSP = HPS.ToDSP(data);
                }
                else
                {
                    soundPlayer.NowPlaying = "";
                    soundPlayer.DSP = null;
                }

                if (mxPropertyGrid1 != null)
                    mxPropertyGrid1.SelectedObject = music;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importButton_Click(object sender, System.EventArgs e)
        {
            using (OpenFileDialog d = new OpenFileDialog())
            {
                d.Filter = Tools.DSPExtensions.SupportedImportFormatFilter;

                if (d.ShowDialog() == DialogResult.OK)
                {
                    var newFileName = Path.GetFileNameWithoutExtension(d.FileName) + ".hps";
                    var newFilePath = "audio\\" + newFileName;

                    if(MEX.ImageResource.FileExists(newFilePath))
                    {
                        MessageBox.Show("A file with that name already exists");
                    }
                    else
                    {
                        var temp = Path.GetTempFileName();

                        var dsp = Tools.DSPExtensions.FromFile(d.FileName);
                        HPS.SaveDSPAsHPS(dsp, temp);

                        MEX.ImageResource.AddFile(newFilePath, temp);
                        MEX.BackgroundMusic.Add(new MEXMusic() { FileName = newFileName, Label = Path.GetFileNameWithoutExtension(d.FileName) });
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, System.EventArgs e)
        {
            if (mxListBox1.SelectedItem is MEXMusic msc && !msc.IsMexMusic)
            {
                MessageBox.Show("Cannot delete base game music!", "Delete Music", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // adjust playlists
            if (mxListBox1.SelectedItem is MEXMusic music && MessageBox.Show($"Are you sure you want to delete {music.ToString()}?", "Delete Music", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                // 
                MEX.BackgroundMusic.Remove(music);
                MEX.ImageResource.DeleteFile("audio\\" + music.FileName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExport_Click(object sender, System.EventArgs e)
        {
            if (mxListBox1.SelectedItem is MEXMusic music)
                using (SaveFileDialog d = new SaveFileDialog())
                {
                    d.Filter = DSP.SupportedExportFilter;

                    if(d.ShowDialog() == DialogResult.OK)
                    {
                        var data = MEX.ImageResource.GetFile("audio\\" + music.FileName);
                        HPS.ToDSP(data).ExportFormat(d.FileName);
                    }
                }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void replaceButton_Click(object sender, System.EventArgs e)
        {
            if (mxListBox1.SelectedItem is MEXMusic music)
                using (OpenFileDialog d = new OpenFileDialog())
                {
                    d.Filter = DSP.SupportedImportFilter;

                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        var temp = Path.GetTempFileName();

                        var dsp = new DSP(d.FileName);
                        HPS.SaveDSPAsHPS(dsp, temp);

                        MEX.ImageResource.AddFile("audio\\" + music.FileName, temp);

                    }
                }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxListBox1_DoubleClicked(object sender, System.EventArgs e)
        {
            soundPlayer.PlaySound();
        }
    }
}
