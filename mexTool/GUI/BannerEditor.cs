using mexTool.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mexTool.GUI
{
    public partial class BannerEditor : Form
    {
        public class ProxyMetaData
        {
            public GCILib.GCBanner.MetaFooter meta = new GCILib.GCBanner.MetaFooter();

            public string LongName { get => meta.LongName; set => meta.LongName = value; }

            public string LongMaker { get => meta.LongMaker; set => meta.LongMaker = value; }

            public string ShortName { get => meta.ShortName; set => meta.ShortName = value; }

            public string ShortMaker { get => meta.ShortMaker; set => meta.ShortMaker = value; }

            public string Description { get => meta.Description; set => meta.Description = value; }
        }

        private GCILib.GCBanner _banner;
        private ProxyMetaData _metadata = new ProxyMetaData();
        public DialogResult Result { get; internal set; } = DialogResult.None;

        public BannerEditor()
        {
            InitializeComponent();

            FormClosing += (send, arg) =>
            {
                if (mxPictureBox1.Image != null)
                    mxPictureBox1.Image.Dispose();
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="banner"></param>
        public void SetBanner(GCILib.GCBanner banner)
        {
            _banner = banner;

            if (_banner == null)
                return;

            mxPictureBox1.Image = ImageTools.RGBAToBitmap(banner.GetBannerImageRGBA8(), 96, 32);
            _metadata.meta = banner.MetaData;
            mxPropertyGrid1.SelectedObject = _metadata; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GCILib.GCBanner GetBanner()
        {
            _banner.MetaData = _metadata.meta;
            return _banner;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxButton3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxButton1_Click(object sender, EventArgs e)
        {
            if (_banner != null)
                using (OpenFileDialog d = new OpenFileDialog())
                {
                    d.Filter = "PNG (*png)|*.png";

                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        if (mxPictureBox1.Image != null)
                            mxPictureBox1.Image.Dispose();

                        var bmp = new Bitmap(d.FileName);
                        mxPictureBox1.Image = bmp;
                        _banner.SetBannerImageRGBA8(bmp.GetRGBAData());
                    }
                }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxButton2_Click(object sender, EventArgs e)
        {
            if (mxPictureBox1.Image != null)
            {
                using (SaveFileDialog d = new SaveFileDialog())
                {
                    d.Filter = "PNG (*png)|*.png";

                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        mxPictureBox1.Image.Save(d.FileName);
                    }
                }
            }
        }
    }
}
