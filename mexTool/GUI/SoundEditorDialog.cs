using MeleeMedia.Audio;
using System.Windows.Forms;

namespace mexTool.GUI
{
    public partial class SoundEditorDialog : Form
    {
        public SoundEditorDialog()
        {
            InitializeComponent();

            CenterToScreen();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dsp"></param>
        public void SetSound(DSP dsp)
        {
            editor.SetSound(dsp);
        }

        private void mxButton1_Click(object sender, System.EventArgs e)
        {
            editor.ApplyTrimming();
            Close();
        }
    }
}
