using MeleeMedia.Audio;
using System;
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
            // TODO: add sound
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemoveSound_Click(object sender, EventArgs e)
        {
            // TODO: remove sound and adjust ids?
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReplaceSound_Click(object sender, EventArgs e)
        {
            // TODO replace sound
        }
    }
}
