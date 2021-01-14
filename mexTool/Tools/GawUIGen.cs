using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace mexTool.Tools
{
    public class GawUIGen
    {

        public static Bitmap GenerateStock(Color fill, Color outline)
        {
            var output = new Bitmap(24, 24);

            // get masks
            using (var outlineBmp = new Bitmap(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib/gaw_stc_outline_mask.png")))
            using (var fillBmp = new Bitmap(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib/gaw_stc_fill_mask.png")))
            using (var outlineLayer = GenerateColorMap(outlineBmp, outline, fill))
            using (var fillLayer = GenerateColorMap(fillBmp, fill, fill))
            using (var g = Graphics.FromImage(output))
            {
                g.DrawImage(fillLayer, 0, 0);
                g.DrawImage(outlineLayer, 0, 0);
            }

            return output;
        }

        public static Bitmap GenerateCSP(Color fill, Color outline)
        {
            var output = new Bitmap(136, 188);

            // get masks
            using (var outlineBmp = new Bitmap(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib/gaw_csp_outline_mask.png")))
            using (var fillBmp = new Bitmap(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib/gaw_csp_fill_mask.png")))
            using (var outlineLayer = GenerateColorMap(outlineBmp, outline, fill))
            using (var fillLayer = GenerateColorMap(fillBmp, fill, fill))
            using (var shadowLayer = new Bitmap(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib/gaw_csp_shadow.png")))
            using (var g = Graphics.FromImage(output))
            {
                g.DrawImage(shadowLayer, 0, 0);
                g.DrawImage(fillLayer, 0, 0);
                g.DrawImage(outlineLayer, 0, 0);
            }

            return output;
        }

        private static Bitmap GenerateColorMap(Bitmap source, Color c, Color mixColor)
        {
            var mask = source.GetBGRAData();
            byte[] output = new byte[source.Width * source.Height * 4];

            for (int i = 0; i < output.Length; i += 4)
            {
                output[i + 3] = mask[i + 1];
                output[i + 2] = (byte)((mixColor.R * (255 - c.A) + c.R * c.A) / 255);
                output[i + 1] = (byte)((mixColor.G * (255 - c.A) + c.G * c.A) / 255);
                output[i] = (byte)((mixColor.B * (255 - c.A) + c.B * c.A) / 255);
            }

            var bmp = new Bitmap(source.Width, source.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            try
            {
                BitmapData bmpData = bmp.LockBits(
                                     new Rectangle(0, 0, bmp.Width, bmp.Height),
                                     ImageLockMode.WriteOnly, bmp.PixelFormat);
                Marshal.Copy(output, 0, bmpData.Scan0, output.Length);
                bmp.UnlockBits(bmpData);
            }
            catch { bmp.Dispose(); throw; }

            return bmp;
        }


    }
}
