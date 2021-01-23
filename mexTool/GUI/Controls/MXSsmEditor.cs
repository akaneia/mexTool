using MeleeMedia.Audio;
using mexTool.Core;
using System;
using System.IO;
using System.Windows.Forms;

namespace mexTool.GUI.Controls
{

    public partial class MXSsmEditor : UserControl
    {

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler PlayDsp;

        protected void OnPlayDsp(EventArgs e)
        {
            EventHandler handler = PlayDsp;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DSP SelectedDSP
        {
            get
            {
                if (mxListBox1.SelectedItem is DSP dsp)
                    return dsp;
                return null;
            }
        }

        public event EventHandler RemoveSound;

        protected virtual void OnRemoveSound(SoundRemovedEventArgs e)
        {
            var handler = RemoveSound;
            if (handler != null) handler(this, e);
        }

        public MXSsmEditor()
        {
            InitializeComponent();
        }

        private SSM _ssm;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"></param>
        public void SetSSM(SSM sb)
        {
            _ssm = sb;
            mxListBox1.DataSource = sb.Sounds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxListBox1_DoubleClicked(object sender, EventArgs e)
        {
            if (mxListBox1.SelectedItem is DSP dsp)
            {
                mxPropertyGrid1.SelectedObject = dsp;
                OnPlayDsp(EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (mxListBox1.DataSource != null)
                mxPropertyGrid1.SelectedObject = SelectedDSP;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddSound_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog d = new OpenFileDialog())
            {
                d.Filter = Tools.DSPExtensions.SupportedImportFormatFilter;

                if (d.ShowDialog() == DialogResult.OK)
                {
                    // import sound file
                    var dsp = Tools.DSPExtensions.FromFile(d.FileName);

                    // add dsp
                    var sounds = _ssm.Sounds;
                    Array.Resize(ref sounds, sounds.Length + 1);
                    sounds[sounds.Length - 1] = dsp;

                    // reset bindings
                    _ssm.Sounds = sounds;
                    mxListBox1.DataSource = _ssm.Sounds;

                    // select added dsp
                    mxListBox1.SelectedItem = dsp;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemoveSound_Click(object sender, EventArgs e)
        {
            if (mxListBox1.SelectedIndex == -1)
                return;

            // remove selected dsp
            var sounds = _ssm.Sounds;
            for(int i = mxListBox1.SelectedIndex; i < sounds.Length - 1; i++)
                sounds[i] = sounds[i + 1];
            Array.Resize(ref sounds, sounds.Length - 1);

            // reset bindings
            var selectedIndex = mxListBox1.SelectedIndex;
            _ssm.Sounds = sounds;
            mxListBox1.DataSource = _ssm.Sounds;

            // select added dsp
            mxListBox1.SelectedIndex = selectedIndex;

            OnRemoveSound(new SoundRemovedEventArgs() { SoundIndex = selectedIndex });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReplaceSound_Click(object sender, EventArgs e)
        {
            if (mxListBox1.SelectedItem is DSP dsp)
                using (OpenFileDialog d = new OpenFileDialog())
                {
                    d.Filter = Tools.DSPExtensions.SupportedImportFormatFilter;

                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        // replace sound
                        dsp.FromFile(d.FileName);

                        // select added dsp
                        mxListBox1.SelectedItem = dsp;
                    }
                }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (mxListBox1.SelectedItem is DSP dsp)
                using (SaveFileDialog d = new SaveFileDialog())
                {
                    d.Filter = DSP.SupportedExportFilter;

                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        dsp.ExportFormat(d.FileName);
                    }
                }
        }
    }
    public class SoundRemovedEventArgs : EventArgs
    {
        public int SoundIndex { get; set; }
    }
}
