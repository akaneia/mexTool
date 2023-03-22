using MeleeMedia.Audio;
using System.Windows.Forms;

namespace mexTool.GUI
{
    public partial class SoundEditorDialog : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public SoundEditorDialog()
        {
            InitializeComponent();

            CenterToScreen();

            DialogResult = DialogResult.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dsp"></param>
        public void SetSound(DSP dsp)
        {
            editor.SetSound(dsp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxButton1_Click(object sender, System.EventArgs e)
        {
            editor.ApplyTrimming();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
