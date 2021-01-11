using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mexTool.GUI.Controls
{
    public class MXToolStrip : ToolStrip
    {
        public MXToolStrip() : base()
        {
            Renderer = new ToolStripProfessionalRenderer(new MenuColorTable());

            foreach (ToolStripMenuItem mi in Items)
            {
                mi.ForeColor = Color.White;
                foreach (var item in mi.DropDownItems)
                {
                    if (item is ToolStripMenuItem tsmi)
                        tsmi.ForeColor = Color.White;
                }
            }
        }

    }
}
