using HSDRawViewer.GUI;
using mexTool.GUI.Controls;
using System;
using System.Windows.Forms;

namespace mexTool.GUI.Pages
{
    public partial class MenuPage : UserControl
    {
        private PlaylistEditor _playlistEditor;
        private MXStageSelectEditor _sssEditor;
        private MXFighterSelectEditor _cssEditor;
        private MXEmblemEditor _emblems;

        public MenuPage()
        {
            InitializeComponent();

            _playlistEditor = new PlaylistEditor();
            _playlistEditor.Visible = false;
            _playlistEditor.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(_playlistEditor);

            _sssEditor = new MXStageSelectEditor();
            _sssEditor.Visible = false;
            _sssEditor.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(_sssEditor);

            _cssEditor = new MXFighterSelectEditor();
            _cssEditor.Visible = false;
            _cssEditor.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(_cssEditor);

            _emblems = new MXEmblemEditor();
            _emblems.Visible = false;
            _emblems.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(_emblems);
        }

        private void SelectTab(object sender, EventArgs args)
        {
            buttonPlaylistTab.BackFillColor = ThemeColors.TabColor;
            buttonCSSTab.BackFillColor = ThemeColors.TabColor;
            buttonSSSTab.BackFillColor = ThemeColors.TabColor;
            buttonEmblemTab.BackFillColor = ThemeColors.TabColor;

            ((MXButton)sender).BackFillColor = ThemeColors.TabColorSelected;

            ShowEditor();
        }

        private void ShowEditor()
        {
            _playlistEditor.Visible = false;
            _sssEditor.Visible = false;
            _cssEditor.Visible = false;
            _emblems.Visible = false;

            if (buttonPlaylistTab.BackFillColor == ThemeColors.TabColorSelected)
            {
                _playlistEditor.Visible = true;
                _playlistEditor.SetPlaylist(Core.MEX.MainMenuPlaylist);
            }

            if (buttonSSSTab.BackFillColor == ThemeColors.TabColorSelected)
                _sssEditor.Visible = true;

            if (buttonCSSTab.BackFillColor == ThemeColors.TabColorSelected)
                _cssEditor.Visible = true;

            if (buttonEmblemTab.BackFillColor == ThemeColors.TabColorSelected)
                _emblems.Visible = true;
        }


    }
}
