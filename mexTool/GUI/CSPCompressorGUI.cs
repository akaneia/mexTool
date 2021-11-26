using mexTool.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace mexTool.GUI.Controls
{
    public partial class CSPCompressorGUI : Form
    {
        private static CSPCompressorSettings Settings { get; } = new CSPCompressorSettings();

        private Bitmap PreviewImage;

        private BackgroundWorker worker;

        /// <summary>
        /// 
        /// </summary>
        public CSPCompressorGUI()
        {
            InitializeComponent();

            mxPropertyGrid1.SelectedObject = Settings;
            mxPictureBox1.Image = Properties.Resources.csp_preview;

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += Save;

            GeneratePreview();

            CenterToParent();

            FormClosing += (send, arg) =>
            {
                worker.Dispose();
                if (PreviewImage != null)
                {
                    PreviewImage.Dispose();
                    PreviewImage = null;
                    mxPictureBox2.Image = null;
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        private void GeneratePreview()
        {
            if (PreviewImage != null)
            {
                PreviewImage.Dispose();
                PreviewImage = null;
                mxPictureBox2.Image = null;
            }
            //PreviewImage = CSPCompressor.CompressCSP(Settings, Properties.Resources.csp_preview).ToTOBJ(HSDRaw.GX.GXTexFmt.CI8, HSDRaw.GX.GXTlutFmt.RGB5A3).ToBitmap();
            PreviewImage = CSPCompressor.CompressCSP(Settings, Properties.Resources.csp_preview);
            mxPictureBox2.Image = PreviewImage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void mxPropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            GeneratePreview();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure? This cannot be undone", "Resize CSPs", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Enabled = false;
                worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Save(object sender, DoWorkEventArgs args)
        {
            // loop through all costume csps and apply resize settings
            float fighterCount = Core.MEX.Fighters.Count;
            float fighterIndex = 0;
            foreach (var fighter in Core.MEX.Fighters)
            {
                ReportProgress(null, new ProgressChangedEventArgs((int)((fighterIndex / fighterCount) * 100), $"Compressing {fighter.NameText}..."));

                foreach (var costume in fighter.Costumes)
                    ThreadPool.QueueUserWorkItem(CompressCSPThread, costume);

                fighterIndex++;
            }

            ReportProgress(null, new ProgressChangedEventArgs(100, "Done!"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private static void CompressCSPThread(object obj)
        {
            if (obj != null && obj is Core.MEXCostume costume && costume.CSP != null)
                CSPCompressor.CompressCSP(Settings, costume.CSP);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ReportProgress(object sender, ProgressChangedEventArgs args)
        {
            MethodInvoker m = new MethodInvoker(() => {
                saveProgressBar.Value = args.ProgressPercentage;
                saveLabel.Text = args.UserState as string;
                if (saveProgressBar.Value == 100)
                    Enabled = true;
            });
            saveProgressBar.Invoke(m);
        }
    }
}
