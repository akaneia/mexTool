using HSDRaw;
using HSDRaw.Melee.Pl;
using HSDRawViewer.GUI;
using mexTool.Core;
using mexTool.GUI.Controls;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace mexTool.GUI.Pages
{
    public partial class FighterPage : UserControl
    {
        private MXPropertyGrid _propertyGrid;
        private ItemEditor _itemEditor;
        private MXButton _copyMoveLogic;
        private MXCostumeEditor _costumeEditor;

        /// <summary>
        /// 
        /// </summary>
        public FighterPage()
        {
            InitializeComponent();

            _propertyGrid = new MXPropertyGrid();
            _propertyGrid.Visible = false;
            _propertyGrid.Dock = DockStyle.Fill;
            _propertyGrid.PropertySort = PropertySort.Categorized;
            panel1.Controls.Add(_propertyGrid);

            _itemEditor = new ItemEditor();
            _itemEditor.Visible = false;
            _itemEditor.Dock = DockStyle.Fill;
            panel1.Controls.Add(_itemEditor);

            _costumeEditor = new MXCostumeEditor();
            _costumeEditor.Visible = false;
            _costumeEditor.Dock = DockStyle.Fill;
            panel1.Controls.Add(_costumeEditor);

            _copyMoveLogic = new MXButton();
            _copyMoveLogic.Text = "Copy Move Logic Struct to Clipboard";
            _copyMoveLogic.ForeColor = Color.White;
            _copyMoveLogic.Height = 24;
            _copyMoveLogic.Visible = false;
            _copyMoveLogic.Dock = DockStyle.Top;
            _copyMoveLogic.Click += CopyMoveLogic;
            _copyMoveLogic.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
            panel1.Controls.Add(_copyMoveLogic);

            fighterListBox.DataSource = MEX.Fighters;
            buttonGeneralTab.PerformClick();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CopyMoveLogic(object sender, EventArgs args)
        {
            if (fighterListBox.SelectedItem is MEXFighter fighter)
            {
                var moveLogic = fighter.Functions.MoveLogic;

                if (moveLogic == null)
                {
                    MessageBox.Show("This MxDt file does not contains move logic", "Nothing to copy", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                var ftDataFile = MEX.ImageResource.GetFile(fighter.FighterDataPath);

                SBM_FighterData fighterData = null;

                if (ftDataFile != null)
                    fighterData = new HSDRawFile(ftDataFile).Roots[0].Data as SBM_FighterData;

                StringBuilder table = new StringBuilder();

                int index = 341;
                foreach (var m in moveLogic)
                {
                    table.AppendLine($"\t// State: {index} - " + (fighterData != null && m.AnimationID != -1 && fighterData.FighterCommandTable.Commands[m.AnimationID].Name != null ? System.Text.RegularExpressions.Regex.Replace(fighterData.FighterCommandTable.Commands[m.AnimationID].Name.Replace("_figatree", ""), @"Ply.*_Share_ACTION_", "") : "Animation: " + m.AnimationID.ToString("X")));
                    index++;
                    table.AppendLine(string.Format(
                        "\t{{" +
                        "\n\t\t{0, -12}// AnimationID" +
                        "\n\t\t0x{1, -10}// StateFlags" +
                        "\n\t\t0x{2, -10}// AttackID" +
                        "\n\t\t0x{3, -10}// BitFlags" +
                        "\n\t\t0x{4, -10}// AnimationCallback" +
                        "\n\t\t0x{5, -10}// IASACallback" +
                        "\n\t\t0x{6, -10}// PhysicsCallback" +
                        "\n\t\t0x{7, -10}// CollisionCallback" +
                        "\n\t\t0x{8, -10}// CameraCallback" +
                        "\n\t}},",
                m.AnimationID + ",",
                m.StateFlags.ToString("X") + ",",
                m.AttackID.ToString("X") + ",",
                m.BitFlags.ToString("X") + ",",
                m.AnimationCallBack.ToString("X") + ",",
                m.IASACallBack.ToString("X") + ",",
                m.PhysicsCallback.ToString("X") + ",",
                m.CollisionCallback.ToString("X") + ",",
                m.CameraCallback.ToString("X") + ","
                ));
                }

                Clipboard.SetText(
                    @"__attribute__((used))
static struct MoveLogic move_logic[] = {
" + table.ToString() + @"}; ");

                MessageBox.Show("Move Logic Struct Copied to Clipboard", "Move Logic Copy");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>

        private void SelectTab(object sender, EventArgs args)
        {
            buttonGeneralTab.BackFillColor = ThemeColors.TabColor;
            buttonCostumeTab.BackFillColor = ThemeColors.TabColor;
            buttonItemsTab.BackFillColor = ThemeColors.TabColor;
            buttonFunctionTab.BackFillColor = ThemeColors.TabColor;
            buttonBoneTab.BackFillColor = ThemeColors.TabColor;

            ((MXButton)sender).BackFillColor = ThemeColors.TabColorSelected;

            ShowEditor();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ShowEditor()
        {
            _itemEditor.Visible = false;
            _propertyGrid.Visible = false;
            _copyMoveLogic.Visible = false;
            _costumeEditor.Visible = false;

            if (fighterListBox.SelectedItem is MEXFighter fighter)
            {
                if (buttonGeneralTab.BackFillColor == ThemeColors.TabColorSelected)
                {
                    _propertyGrid.Visible = true;
                    _propertyGrid.SelectedObject = fighter;
                }
                if (buttonBoneTab.BackFillColor == ThemeColors.TabColorSelected)
                {
                    _propertyGrid.Visible = true;
                    _propertyGrid.SelectedObject = fighter.BoneTable;
                }
                if (buttonFunctionTab.BackFillColor == ThemeColors.TabColorSelected)
                {
                    _copyMoveLogic.Visible = true;
                    _propertyGrid.Visible = true;
                    _propertyGrid.SelectedObject = fighter.Functions;
                }
                if (buttonCostumeTab.BackFillColor == ThemeColors.TabColorSelected)
                {
                    _costumeEditor.Visible = true;
                    _costumeEditor.SetFighter(fighter);
                }
                if (buttonItemsTab.BackFillColor == ThemeColors.TabColorSelected)
                {
                    _itemEditor.Visible = true;
                    _itemEditor.DataSource = fighter.Items;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fighterListBox_SelectedObjectChanged(object sender, EventArgs e)
        {
            ShowEditor();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxButton2_Click(object sender, EventArgs e)
        {
            if (fighterListBox.SelectedItem is MEXFighter fighter)
            {
                var clone = fighter.Copy();

                clone.NameText += "Clone";
                clone.TargetTestStage = fighter.TargetTestStage;
                clone.SoundBank = fighter.SoundBank;
                clone.VictoryTheme = fighter.VictoryTheme;
                clone.FighterSongID1 = fighter.FighterSongID1;
                clone.FighterSongID2 = fighter.FighterSongID2;
                clone.EffectFile = fighter.EffectFile;
                clone.KirbyEffectFile = fighter.KirbyEffectFile;

                MEX.Fighters.Insert(MEX.Fighters.Count - 6, clone);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (fighterListBox.SelectedItem is MEXFighter fighter)
            {
                if(fighter.IsMEXFighter && 
                    MessageBox.Show($"Are you sure you want to delete {fighter.NameText}?", "Delete Fighter", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                    MEX.Fighters.Remove(fighter);
                else
                {
                    MessageBox.Show("Cannot delete base game fighters.", "Delete Fighter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importButton_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog d = new OpenFileDialog())
            {
                d.Filter = "MEX Fighter (*.zip)|*.zip";

                if (d.ShowDialog() == DialogResult.OK)
                {
                    MEXFighter.InstallFighterFromFile(d.FileName);
                    fighterListBox.Invalidate();
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
            if (fighterListBox.SelectedItem is MEXFighter fighter)
            {
                using (SaveFileDialog d = new SaveFileDialog())
                {
                    d.Filter = "MEX Fighter (*.zip)|*.zip";

                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        fighter.SaveFighterToPackage(d.FileName);
                    }
                }
            }
        }
    }
}
