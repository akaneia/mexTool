using mexTool.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace mexTool.GUI.Controls
{
    public partial class MXStageSelectEditor : UserControl
    {
        private bool UpdatingSelected = false;

        /// <summary>
        /// 
        /// </summary>
        public MXStageSelectEditor()
        {
            InitializeComponent();

            stageListBox.DataSource = Core.MEX.StageIcons;
            stageListBox.SelectionMode = SelectionMode.MultiExtended;


            renderer.DataSource = Core.MEX.StageIcons;
            renderer.ItemMoved += (sender, args) => UpdatePropertyGrid();


            stageNameBox.GotFocus += (RemoveText);
            stageNameBox.LostFocus += (AddText);

            locationBox.GotFocus += (RemoveText);
            locationBox.LostFocus += (AddText);

            AddText(stageNameBox, null);
            AddText(locationBox, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RemoveText(object sender, EventArgs e)
        {
            if (sender is TextBox box)
            {
                var text = "";

                if (box == locationBox)
                    text = "Stage Location...";

                if (box == stageNameBox)
                    text = "Stage Name...";

                if (box.Text == text)
                    box.Text = "";

                box.ForeColor = Color.Black;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddText(object sender, EventArgs e)
        {
            if(sender is TextBox box && string.IsNullOrWhiteSpace(box.Text))
            {
                var text = "";

                if (box == locationBox)
                    text = "Stage Location...";

                if (box == stageNameBox)
                    text = "Stage Name...";

                box.Text = text;
                box.ForeColor = Color.Gray;
            }
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

            renderer.SelectItems(UpdatePropertyGrid(), false);

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

            stageListBox.SelectedItem = null;
            stageListBox.ClearSelected();

            foreach (var icon in renderer.SelectedIcons)
                stageListBox.SelectedItems.Add(icon);

            UpdatePropertyGrid();

            UpdatingSelected = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private MEXStageIcon[] UpdatePropertyGrid()
        {
            RefreshPanel();

            var items = new MEXStageIcon[stageListBox.SelectedItems.Count];

            for (int i = 0; i < items.Length; i++)
                items[i] = (MEXStageIcon)stageListBox.SelectedItems[i];

            if(mxPropertyGrid1 != null)
                mxPropertyGrid1.SelectedObjects = items;

            if(items.Length == 1)
            {
                locationBox.Text = "Stage Location...";
                stageNameBox.Text = items[0].ToString();
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
        private void RefreshPanel()
        {

            // enable edit panel if one icon is selected
            imageEditPanel.Visible = false;

            if (stageListBox.SelectedItems.Count == 1 && stageListBox.SelectedItem is MEXStageIcon icon)
            {
                imageEditPanel.Visible = true;

                if (nameTagBox.Image != null)
                    nameTagBox.Image.Dispose();

                nameTagBox.Image = GraphicExtensions.TOBJToBitmap(icon._previewText);

                iconBox.Image = icon.GetImage();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonImportIcon_Click(object sender, EventArgs e)
        {
            if (stageListBox.SelectedItems.Count == 1 && stageListBox.SelectedItem is MEXStageIcon icon)
            {
                using (OpenFileDialog d = new OpenFileDialog())
                {
                    d.Filter = "PNG (*.png)|*.png";

                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        using (var bmp = new Bitmap(d.FileName))
                            icon.Image = bmp.ToTOBJ(HSDRaw.GX.GXTexFmt.CI8, HSDRaw.GX.GXTlutFmt.RGB565);

                        RefreshPanel();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageNameTagClick(object sender, EventArgs e)
        {
            if (stageListBox.SelectedItems.Count == 1 && stageListBox.SelectedItem is MEXStageIcon icon)
            {
                using (OpenFileDialog d = new OpenFileDialog())
                {
                    d.Filter = "PNG (*.png)|*.png";

                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        using (var bmp = new Bitmap(d.FileName))
                            icon._previewText = bmp.ToTOBJ(HSDRaw.GX.GXTexFmt.I4, HSDRaw.GX.GXTlutFmt.RGB565);

                        RefreshPanel();
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
            if (stageListBox.SelectedItems.Count == 1 && stageListBox.SelectedItem is MEXStageIcon icon)
            {
                using (SaveFileDialog d = new SaveFileDialog())
                {
                    d.Filter = "PNG (*.png)|*.png";

                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        icon.GetImage().Save(d.FileName);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGenerateTag_Click(object sender, EventArgs e)
        {
            if (stageListBox.SelectedItems.Count == 1 && stageListBox.SelectedItem is MEXStageIcon icon)
            {
                using (var img = Tools.StageNameGenerator.GenerateStageName(stageNameBox.Text, locationBox.Text))
                    icon._previewText = img.ToTOBJ(HSDRaw.GX.GXTexFmt.I4, HSDRaw.GX.GXTlutFmt.IA8);

                RefreshPanel();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addStage_Click(object sender, EventArgs e)
        {
            // make clone of the first stage
            MEXStageIcon icon = MEX.StageIcons[0];

            // unless one is already selected
            if (stageListBox.SelectedItem is MEXStageIcon ico)
                icon = ico;

            var iconImage = icon.Image;
            icon.Image = null;

            var clone = icon.Copy();
            clone.Stage = MEX.StageIcons[0].Stage;
            clone.X = -clone.Width / 2;
            clone.Y = -clone.Height / 2;
            clone.Image = iconImage.Copy();

            icon.Image = iconImage;

            MEX.StageIcons.Insert(MEX.StageIcons.Count - 1, clone);

            stageListBox.ClearSelected();
            stageListBox.SelectedItem = clone;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeStage_Click(object sender, EventArgs e)
        {
            // must keep at least 1 stage + random
            // can't remove random
            if (MEX.StageIcons.Count <= 2 || 
                stageListBox.SelectedItems.Contains(MEX.StageIcons[MEX.StageIcons.Count - 1]) || 
                MEX.StageIcons.Count - stageListBox.SelectedItems.Count < 2)
                return;

            List<MEXStageIcon> toRemove = new List<MEXStageIcon>();

            foreach (MEXStageIcon icon in stageListBox.SelectedItems)
                toRemove.Add(icon);

            foreach(var icon in toRemove)
                MEX.StageIcons.Remove(icon);
        }
    }
}
