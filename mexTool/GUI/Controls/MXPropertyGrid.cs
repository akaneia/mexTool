using System.Drawing;
using System.Windows.Forms;

namespace mexTool.GUI.Controls
{
    public class MXPropertyGrid : PropertyGrid
    {
        public MXPropertyGrid()
        {
            CategoryForeColor = Color.White;
            CommandsForeColor = Color.White;
            HelpForeColor = Color.White;
            ViewForeColor = Color.White;

            CategorySplitterColor = Color.Transparent;

            ToolbarVisible = false;

            ViewBorderColor = Color.Transparent;
            CommandsBorderColor = Color.Transparent;
            HelpBorderColor = Color.Transparent;

            BackColor = Color.FromArgb(40, 40, 40);
            CommandsBackColor = Color.FromArgb(40, 40, 40);
            HelpBackColor = Color.FromArgb(40, 40, 40);
            LineColor = Color.FromArgb(40, 40, 40);
            ViewBackColor = Color.Black;
        }
    }
}
