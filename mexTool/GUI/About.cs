using System.Diagnostics;
using System.Windows.Forms;

namespace mexTool.GUI
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            CenterToParent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://twitter.com/TeamAkaneia");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/akaneia");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://discord.gg/XxdFN9JnMX");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UCtzhRKrCzHo_O9eFOVd8H2w");
        }
    }
}
