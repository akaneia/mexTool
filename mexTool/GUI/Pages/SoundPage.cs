﻿using HSDRawViewer.GUI;
using MeleeMedia.Audio;
using mexTool.Core;
using mexTool.GUI.Controls;
using System;
using System.Windows.Forms;

namespace mexTool.GUI.Pages
{
    public partial class SoundPage : UserControl
    {
        private MXSsmEditor _ssmEditor;
        private MXSemEditor _semEditor;

        public SoundPage()
        {
            InitializeComponent();

            _ssmEditor = new MXSsmEditor();
            _ssmEditor.Visible = false;
            _ssmEditor.Dock = DockStyle.Fill;
            _ssmEditor.PlayDsp += (sender, args) =>
            {
                mxMusicPlayer1.NowPlaying = "";
                mxMusicPlayer1.DSP = _ssmEditor.SelectedDSP;
                mxMusicPlayer1.PlaySound();
            };
            _ssmEditor.RemoveSound += (sender, args) =>
            {
                _semEditor.RemoveSoundID((args as SoundRemovedEventArgs).SoundIndex);
            };
            panel1.Controls.Add(_ssmEditor);

            _semEditor = new MXSemEditor();
            _semEditor.Visible = false;
            _semEditor.Dock = DockStyle.Fill;
            _semEditor.PlayDsp += (sender, args) =>
            {
                mxMusicPlayer1.NowPlaying = "";
                mxMusicPlayer1.DSP = null;

                if (bankListBox.SelectedItem is MEXSoundBank bank &&
                    _semEditor.SelectedSoundBankIndex >= 0 &&
                    _semEditor.SelectedSoundBankIndex < bank.SoundBank.Sounds.Length)
                {
                    mxMusicPlayer1.DSP = bank.SoundBank.Sounds[_semEditor.SelectedSoundBankIndex];
                    mxMusicPlayer1.NowPlaying = _semEditor.SelectedScript?.Name;
                    mxMusicPlayer1.PlaySound();
                }
            };
            _semEditor.ChangeDsp += (sender, args) =>
            {
                mxMusicPlayer1.NowPlaying = "";
                mxMusicPlayer1.DSP = null;

                if (bankListBox.SelectedItem is MEXSoundBank bank &&
                    _semEditor.SelectedSoundBankIndex >= 0 &&
                    _semEditor.SelectedSoundBankIndex < bank.SoundBank.Sounds.Length)
                {
                    mxMusicPlayer1.DSP = bank.SoundBank.Sounds[_semEditor.SelectedSoundBankIndex];
                    mxMusicPlayer1.NowPlaying = _semEditor.SelectedScript?.Name;
                }
            };
            panel1.Controls.Add(_semEditor);

            bankListBox.DataSource = MEX.SoundBanks;

            mxMusicPlayer1.ProgressBarVisible = false;

            scriptTabButton.BackFillColor = ThemeColors.TabColorSelected;
            LoadTab();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCSSTab_Click(object sender, EventArgs e)
        {
            scriptTabButton.BackFillColor = ThemeColors.TabColor;
            bankTabButton.BackFillColor = ThemeColors.TabColor;

            ((MXButton)sender).BackFillColor = ThemeColors.TabColorSelected;

            LoadTab();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadTab()
        {
            _semEditor.Visible = false;
            _ssmEditor.Visible = false;

            if (bankListBox.SelectedItem is MEXSoundBank bank)
            {
                if (scriptTabButton.BackFillColor == ThemeColors.TabColorSelected)
                {
                    _semEditor.Visible = true;
                    _semEditor.SetScript(bank.ScriptBank, MEX.SoundBanks.IndexOf(bank));
                }
                else
                if (bankTabButton.BackFillColor == ThemeColors.TabColorSelected)
                {
                    _ssmEditor.Visible = true;
                    _ssmEditor.SetSSM(bank.SoundBank);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bankListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bankListBox.SelectedItem is MEXSoundBank bank)
            {
                _semEditor.SetScript(bank.ScriptBank, MEX.SoundBanks.IndexOf(bank));
                _ssmEditor.SetSSM(bank.SoundBank);
                mexbankPropertyGrid.SelectedObject = bank;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (bankListBox.SelectedItem is MEXSoundBank bank)
            {
                if (bank.IsMEXSound &&
                    MessageBox.Show($"Are you sure you want to delete {bank.ToString()}?", "Delete Sound Bank", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    MEX.SoundBanks.Remove(bank);
                    bankListBox.Invalidate();
                }
                else
                {
                    MessageBox.Show("Cannot delete base game sound banks.", "Delete Sound Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            if (bankListBox.SelectedItem is MEXSoundBank bank)
            {
                MEX.SoundBanks.Add(bank.Copy());
                bankListBox.Invalidate();
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
                d.Filter = "MEX Soundbank (*.spk)|*.spk";

                if (d.ShowDialog() == DialogResult.OK)
                {
                    MEX.SoundBanks.Add(new MEXSoundBank(d.FileName));
                    bankListBox.Invalidate();
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
            if (bankListBox.SelectedItem is MEXSoundBank bank)
            {
                using (SaveFileDialog d = new SaveFileDialog())
                {
                    d.Filter = "MEX Soundbank (*.spk)|*.spk";

                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        bank.SaveAsPackage(d.FileName);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cleanAllButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Cleaning scripts removes unused commands and results in smaller filesize", "Clean Scripts?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int removed = 0;

                foreach (var b in MEX.SoundBanks)
                    foreach (var s in b.ScriptBank.Scripts)
                    {
                        removed += s.Codes.RemoveAll(d => d.Code == SEM_CODE.NULL);
                    }

                if (removed != 0)
                    MessageBox.Show($"Removed {removed} unused codes!", "Cleaning Success");
                else
                    MessageBox.Show($"No NULL codes found", "Cleaning Success");
            }
        }
    }
}
