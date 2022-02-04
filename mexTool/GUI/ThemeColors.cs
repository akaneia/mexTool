using System.Collections.Generic;
using System.Drawing;

namespace HSDRawViewer.GUI
{
    public static class ThemeColors
    {
        public static Color TabColor = Color.FromArgb(80, 80, 100);
        public static Color TabColorSelected = Color.FromArgb(40, 40, 60);

        public static List<Color> MainColorList = new List<Color>() {
            Color.FromArgb(0, 106, 255), // Purple
            Color.FromArgb(0x00, 0x96, 0x88), // Green
            Color.FromArgb(0xE0, 0x50, 0x20), // Orange
            Color.FromArgb(0xd8, 0x00, 0x73), // Pink
            Color.FromArgb(106, 0, 255), // Indigo
            Color.FromArgb(110, 110, 0), // Yellow ish
            Color.FromArgb(0xb0, 0x2e, 0x0c), // Blue
        };

        public static List<Color> SecondColorList
        {
            get
            {
                if(_secondColorList == null)
                {
                    _secondColorList = new List<Color>();
                    foreach (var c in MainColorList)
                        _secondColorList.Add(ChangeColorBrightness(c, -0.25));
                }
                return _secondColorList;
            }
        }
        public static List<Color> _secondColorList;

        private static Color ChangeColorBrightness(Color color, double correctionFactor)
        {
            double red = color.R;
            double green = color.G;
            double blue = color.B;
            //If correction factor is less than 0, darken color.
            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            //If correction factor is greater than zero, lighten color.
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }
            return Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);
        }
    }
}
