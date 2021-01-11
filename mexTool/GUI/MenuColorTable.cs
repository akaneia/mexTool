using System.Drawing;
using System.Windows.Forms;

namespace mexTool.GUI
{
    public class MenuColorTable : ProfessionalColorTable
    {
        private Color Back = Color.FromArgb(64, 64, 64);
        private Color Selected = Color.FromArgb(80, 80, 80);
        private Color Pressed = Color.FromArgb(40, 40, 40);

        public override Color MenuStripGradientBegin => Back;
        public override Color MenuStripGradientEnd => Back;

        public override Color ButtonSelectedGradientBegin => Selected;

        public override Color ButtonSelectedGradientMiddle => Selected;

        public override Color ButtonSelectedGradientEnd => Selected;

        public override Color ButtonSelectedBorder => Selected;
        public override Color ButtonSelectedHighlightBorder => Selected;

        public override Color ButtonPressedGradientBegin => Pressed;
        public override Color ButtonPressedGradientMiddle => Pressed;
        public override Color ButtonPressedGradientEnd => Pressed;


        public override Color ButtonSelectedHighlight => Selected;
        public override Color MenuBorder => Back;

        public override Color MenuItemPressedGradientBegin => Back;
        public override Color MenuItemPressedGradientEnd => Back;
        public override Color MenuItemSelectedGradientBegin => Back;
        public override Color MenuItemSelectedGradientEnd => Back;

        public override Color MenuItemBorder => Back;
        public override Color MenuItemSelected => Selected;

        public override Color ToolStripBorder => Back;
        public override Color ToolStripDropDownBackground => Back;
        public override Color ToolStripGradientBegin => Back;
        public override Color ToolStripGradientEnd => Back;
        public override Color ToolStripGradientMiddle => Back;


        public override Color ImageMarginGradientBegin => Back;
        public override Color ImageMarginGradientMiddle => Back;
        public override Color ImageMarginGradientEnd => Back;


    }
}
