using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace mexTool.Tools
{
    public class StageNameGenerator
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Bitmap GenerateStageName(string name, string location)
        {
            Font font = new Font(ApplicationSettings.GetFontFamilyByName("A-OTF Folk Pro H"), 30, FontStyle.Italic);
            Font font2 = new Font(ApplicationSettings.GetFontFamilyByName("Palatino Linotype"), 13, FontStyle.Bold);

            if (font == null || font2 == null)
                return null;

            Bitmap bmp = new Bitmap(224, 56);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.Bilinear;

                g.FillRectangle(new SolidBrush(Color.Black), new RectangleF(0, 0, bmp.Width, bmp.Height));


                using (var brush = new SolidBrush(Color.White))
                {
                    {
                        SizeF stringSize = TextRenderer.MeasureText(location, font2);

                        var stringWidth = stringSize.Width;
                        var scale = 0.95f;
                        var newwidth = stringWidth * scale;

                        g.ResetTransform();
                        g.ScaleTransform(scale, 1);
                        g.DrawString(location, font2, new SolidBrush(Color.White), (224 / 2) / scale, -4, stringFormat);
                    }

                    var textRect = new Rectangle(0, 1, bmp.Width, bmp.Height);
                    DrawStringInside(g, textRect, font, brush, name);
                }
            }

            font.Dispose();
            font2.Dispose();
            stringFormat.Dispose();

            return bmp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="rect"></param>
        /// <param name="font"></param>
        /// <param name="brush"></param>
        /// <param name="text"></param>
        /// <param name="tilt"></param>
        private static void DrawStringInside(Graphics graphics, Rectangle rect, Font font, Brush brush, string text)
        {
            var textSize = graphics.MeasureString(text, font);
            
            var state = graphics.Save();
            graphics.ResetTransform();

            var scale = 0.75f;

            if(textSize.Width * scale > rect.Width)
                scale = rect.Width / textSize.Width;

            var sheer = new Matrix();
            sheer.Translate(textSize.Width * scale / 2, textSize.Height / 2);
            sheer.Shear(-0.2f, 0);
            sheer.Translate(-textSize.Width * scale / 2, -textSize.Height / 2);
            graphics.MultiplyTransform(sheer);

            graphics.TranslateTransform(rect.Left + rect.Width / 2 - textSize.Width * scale / 2, rect.Top);
            graphics.ScaleTransform(scale, (textSize.Height + 1) / textSize.Height);
            graphics.DrawString(text, font, brush, PointF.Empty);
            graphics.Restore(state);
        }
    }
}
