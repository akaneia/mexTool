using mexTool.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace mexTool.GUI.Controls
{
    public partial class MXFighterSelectEditor : UserControl
    {
        private bool UpdatingSelected = false;

        /// <summary>
        /// 
        /// </summary>
        public MXFighterSelectEditor()
        {
            InitializeComponent();

            fighterListBox.DataSource = MEX.FighterIcons;
            fighterListBox.SelectionMode = SelectionMode.MultiExtended;

            renderer.DataSource = MEX.FighterIcons;
            renderer.ItemMoved += (sender, args) => UpdatePropertyGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stageListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (UpdatingSelected)
                return;

            UpdatingSelected = true;

            renderer.SelectItems(UpdatePropertyGrid());

            UpdatingSelected = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renderer_SelectedIconChanged(object sender, EventArgs e)
        {
            if (UpdatingSelected)
                return;

            UpdatingSelected = true;

            fighterListBox.SelectedItem = null;
            fighterListBox.ClearSelected();

            foreach (var icon in renderer.SelectedIcons)
                fighterListBox.SelectedItems.Add(icon);

            UpdatePropertyGrid();

            UpdatingSelected = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private MEXFighterIcon[] UpdatePropertyGrid()
        {
            var items = new MEXFighterIcon[fighterListBox.SelectedItems.Count];

            for (int i = 0; i < items.Length; i++)
                items[i] = (MEXFighterIcon)fighterListBox.SelectedItems[i];

            mxPropertyGrid1.SelectedObjects = items;

            buttonExportIcon.Visible = false;
            buttonImportIcon.Visible = false;
            mxPictureBox1.Image = null;

            // edit icon info only when 1 is selected
            if (items.Length == 1)
            {
                buttonExportIcon.Visible = true;
                buttonImportIcon.Visible = true;
                mxPictureBox1.Image = items[0].GetImage();
            }

            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void mxPropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //renderer.Do();
            renderer.RefreshDraw();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addStage_Click(object sender, EventArgs e)
        {
            // make clone of the first fighter
            MEXFighterIcon icon = MEX.FighterIcons[0];

            // unless one is already selected
            if (fighterListBox.SelectedItem is MEXFighterIcon ico)
                icon = ico;

            var iconImage = icon.Image;
            icon.Image = null;

            var clone = icon.Copy();
            clone.Fighter = icon.Fighter;

            clone.X = 0;
            clone.Y = 0;
            clone.Image = iconImage.Copy();

            icon.Image = iconImage;

            MEX.FighterIcons.Add(clone);

            fighterListBox.ClearSelected();
            fighterListBox.SelectedItem = clone;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeStage_Click(object sender, EventArgs e)
        {
            // must keep at least 1 fighter
            if (MEX.FighterIcons.Count < 1 || 
                MEX.FighterIcons.Count - fighterListBox.SelectedItems.Count < 1)
                return;

            if (MessageBox.Show("Are you sure you want to delete these icons?", "Delete CSS Icon(s)", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;

            List<MEXFighterIcon> toRemove = new List<MEXFighterIcon>();

            foreach (MEXFighterIcon icon in fighterListBox.SelectedItems)
                toRemove.Add(icon);

            foreach(var icon in toRemove)
                MEX.FighterIcons.Remove(icon);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonImportIcon_Click(object sender, EventArgs e)
        {
            if (fighterListBox.SelectedItems.Count == 1 && fighterListBox.SelectedItem is MEXFighterIcon icon)
            {
                using (OpenFileDialog d = new OpenFileDialog())
                {
                    d.Filter = "PNG (*.png)|*.png";

                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        using (var bmp = new Bitmap(d.FileName))
                        {
                            icon.Image = bmp.ToTOBJ(HSDRaw.GX.GXTexFmt.CI8, HSDRaw.GX.GXTlutFmt.RGB5A3);
                            mxPictureBox1.Image = icon.GetImage();
                            renderer.RefreshDraw();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExportIcon_Click(object sender, EventArgs e)
        {
            if (fighterListBox.SelectedItems.Count == 1 && fighterListBox.SelectedItem is MEXFighterIcon icon)
            {
                using (SaveFileDialog d = new SaveFileDialog())
                {
                    d.Filter = "PNG (*.png)|*.png";
                    d.FileName = icon.ToString() + "_icon.png";

                    if (d.ShowDialog() == DialogResult.OK)
                        icon.GetImage().Save(d.FileName);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renderer_Resize(object sender, EventArgs e)
        {
            renderer.RefreshDraw();
        }
    }
}
