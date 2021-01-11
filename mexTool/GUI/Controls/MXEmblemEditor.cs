using HSDRaw.Common;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace mexTool.GUI.Controls
{
    public partial class MXEmblemEditor : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public MXEmblemEditor()
        {
            InitializeComponent();

            mxListBox1.DataSource = Core.MEX.Emblems;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (mxListBox1.DataSource is BindingList<HSD_TOBJ> list)
            {
                using (OpenFileDialog d = new OpenFileDialog())
                {
                    d.Filter = "PNG (*.png)|*.png";

                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        mxListBox1.Enabled = false;
                        using (var bmp = new Bitmap(d.FileName))
                            list.Add(bmp.ToTOBJ(HSDRaw.GX.GXTexFmt.I4, HSDRaw.GX.GXTlutFmt.IA8));
                        mxListBox1.Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (mxListBox1.DataSource is BindingList<HSD_TOBJ> list &&
                MessageBox.Show("Are you sure you want to delete this Emblem?", "Delete Emblem", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                list.Remove(mxListBox1.SelectedItem as HSD_TOBJ);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (mxListBox1.SelectedItem is HSD_TOBJ tobj)
            {
                using (SaveFileDialog d = new SaveFileDialog())
                {
                    d.Filter = "PNG (*.png)|*.png";
                    d.FileName = "emblem.png";

                    if (d.ShowDialog() == DialogResult.OK)
                        using (var bmp = GraphicExtensions.TOBJToBitmap(tobj))
                            bmp.Save(d.FileName);
                }
            }
        }
    }
}
