using HSDRawViewer.GUI;
using mexTool.Core;
using mexTool.GUI.Controls;
using System;
using System.Windows.Forms;

namespace mexTool.GUI.Pages
{
    public partial class StagePage : UserControl
    {
        private MXPropertyGrid _propertyGrid;
        private ItemEditor _itemEditor;
        private PlaylistEditor _playListEditor;

        public StagePage()
        {
            InitializeComponent();

            _propertyGrid = new MXPropertyGrid();
            _propertyGrid.Visible = false;
            _propertyGrid.Dock = DockStyle.Fill;
            _propertyGrid.PropertySort = PropertySort.Categorized;
            contentPanel.Controls.Add(_propertyGrid);

            buttonGobjCopy.SendToBack();

            _itemEditor = new ItemEditor();
            _itemEditor.Visible = false;
            _itemEditor.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(_itemEditor);

            _playListEditor = new PlaylistEditor();
            _playListEditor.Visible = false;
            _playListEditor.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(_playListEditor);

            listBoxStage.DataSource = Core.MEX.Stages;

            SelectTab(buttonGeneralTab, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SelectTab(object sender, EventArgs args)
        {
            buttonGeneralTab.BackFillColor = ThemeColors.TabColor;
            buttonItemTab.BackFillColor = ThemeColors.TabColor;
            buttonPlaylistTab.BackFillColor = ThemeColors.TabColor;

            ((MXButton)sender).BackFillColor = ThemeColors.TabColorSelected;

            ShowEditor();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ShowEditor()
        {
            _propertyGrid.Visible = false;
            _itemEditor.Visible = false;
            _playListEditor.Visible = false;
            buttonGobjCopy.Visible = false;

            if (listBoxStage.SelectedItem is MEXStage stage)
            {
                if (buttonGeneralTab.BackFillColor == ThemeColors.TabColorSelected)
                {
                    buttonGobjCopy.Visible = true;
                    _propertyGrid.Visible = true;
                    _propertyGrid.SelectedObject = stage;
                }

                if (buttonItemTab.BackFillColor == ThemeColors.TabColorSelected)
                {
                    _itemEditor.Visible = true;
                    _itemEditor.DataSource = stage.Items;
                }

                if (buttonPlaylistTab.BackFillColor == ThemeColors.TabColorSelected)
                {
                    _playListEditor.Visible = true;
                    _playListEditor.SetPlaylist(stage.Playlist);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxStage_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowEditor();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxStage.SelectedItem is MEXStage stage)
            {
                if (stage.IsMEXStage &&
                    MessageBox.Show($"Are you sure you want to delete {stage.ToString()}?", "Delete Stage", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                    MEX.Stages.Remove(stage);
                else
                {
                    MessageBox.Show("Cannot delete base game stage.", "Delete Stage", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClone_Click(object sender, EventArgs e)
        {
            if (listBoxStage.SelectedItem is MEXStage stage)
            {
                var clone = stage.Copy();

                clone.StageName += "Clone";
                clone.SoundBank = stage.SoundBank;

                MEX.Stages.Add(clone);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog d = new OpenFileDialog())
            {
                d.Multiselect = true;
                d.Filter = "MEX Stage (*.zip)|*.zip";

                if (d.ShowDialog() == DialogResult.OK)
                {
                    foreach (var f in d.FileNames)
                        MEXStage.InstallStageFromFile(f);
                    listBoxStage.Invalidate();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (listBoxStage.SelectedItem is MEXStage stage)
            {
                using (SaveFileDialog d = new SaveFileDialog())
                {
                    d.FileName = stage.StageName.Replace(" ", "_") + ".zip";
                    d.Filter = "MEX Stage (*.zip)|*.zip";

                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        stage.SaveStageToPackage(d.FileName);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGobjCopy_Click(object sender, EventArgs e)
        {
            if (listBoxStage.SelectedItem is MEXStage stage)
            {
                if (stage.MapGOBJs == null || stage.MapGOBJs.Length == 0)
                {
                    MessageBox.Show("This MxDt file does not contains map gobj functions", "Nothing to copy", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Clipboard.SetText(stage.GetMoveLogicStruct());

                MessageBox.Show("Map GOBJ Functions Copied to Clipboard");
            }
        }
    }
}
