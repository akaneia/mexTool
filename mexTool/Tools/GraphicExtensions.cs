using HSDRaw.Common;
using HSDRaw.GX;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System.Drawing
{
    public static class GraphicExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ContrastColor(Color color)
        {
            int d = 0;

            // Counting the perceptive luminance - human eye favors green color... 
            double luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;

            if (luminance > 0.5)
                d = 0; // bright colors - black font
            else
                d = 255; // dark colors - white font

            return Color.FromArgb(d, d, d);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="rect"></param>
        public static void DrawRectangle(this Graphics g, Pen pen, RectangleF rect)
        {
            g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bm"></param>
        /// <param name="set_width"></param>
        /// <param name="set_height"></param>
        /// <param name="new_width"></param>
        /// <param name="new_height"></param>
        /// <returns></returns>
        public static Bitmap Resize(this Bitmap bm, bool set_width, bool set_height, int new_width, int new_height, InterpolationMode interpolationMode)
        {
            // Calculate the new width and height.
            if (!set_width) new_width = bm.Width * new_height / bm.Height;
            if (!set_height) new_height = bm.Height * new_width / bm.Width;

            // Resize and return the image.
            return bm.Resize(new_width, new_height, interpolationMode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bm"></param>
        /// <param name="new_width"></param>
        /// <param name="new_height"></param>
        /// <returns></returns>
        public static Bitmap Resize(this Bitmap bm, int new_width, int new_height, InterpolationMode interpolationMode)
        {
            // Make rectangles representing the original and new dimensions.
            Rectangle src_rect = new Rectangle(0, 0, bm.Width, bm.Height);
            Rectangle dest_rect = new Rectangle(0, 0, new_width, new_height);

            // Make the new bitmap.
            Bitmap bm2 = new Bitmap(new_width, new_height);
            using (Graphics gr = Graphics.FromImage(bm2))
            {
                gr.InterpolationMode = interpolationMode;
                gr.DrawImage(bm, dest_rect, src_rect, GraphicsUnit.Pixel);
            }

            return bm2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static HSD_TOBJ ToTOBJ(this Bitmap bmp, GXTexFmt texFmt, GXTlutFmt palFmt)
        {
            var tobj = new HSD_TOBJ()
            {
                MagFilter = GXTexFilter.GX_LINEAR,
                Flags = TOBJ_FLAGS.COORD_UV | TOBJ_FLAGS.LIGHTMAP_DIFFUSE | TOBJ_FLAGS.COLORMAP_MODULATE | TOBJ_FLAGS.ALPHAMAP_MODULATE,
                HScale = 1,
                WScale = 1,
                WrapS = GXWrapMode.CLAMP,
                WrapT = GXWrapMode.CLAMP,
                SX = 1,
                SY = 1,
                SZ = 1,
                GXTexGenSrc = GXTexGenSrc.GX_TG_TEX0,
                Blending = 1
            };

            tobj.EncodeImageData(bmp.GetBGRAData(), bmp.Width, bmp.Height, texFmt, palFmt);

            return tobj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap ToBitmap(this HSD_TOBJ tobj)
        {
            if (tobj == null || tobj.ImageData == null)
                return null;

            var data = tobj.GetDecodedImageData();
            var width = tobj.ImageData.Width;
            var height = tobj.ImageData.Height;

            if (width == 0) width = 1;
            if (height == 0) height = 1;

            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                BitmapData bmpData = bmp.LockBits(
                                     new Rectangle(0, 0, bmp.Width, bmp.Height),
                                     ImageLockMode.WriteOnly, bmp.PixelFormat);

                Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
                bmp.UnlockBits(bmpData);
            }
            catch { bmp.Dispose(); throw; }

            return bmp;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool MoveUp<T>(this BindingList<T> list, int index)
        {
            if (index > 0 && index < list.Count)
            {
                T item = list[index];
                list.RemoveAt(index);
                list.Insert(index - 1, item);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool MoveDown<T>(this BindingList<T> list, int index)
        {
            if (index >= 0 && index + 1 < list.Count)
            {
                T item = list[index];
                list.RemoveAt(index);
                list.Insert(index + 1, item);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static byte[] GetBGRAData(this Bitmap bmp)
        {
            var bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var length = bitmapData.Stride * bitmapData.Height;

            byte[] bytes = new byte[length];

            Marshal.Copy(bitmapData.Scan0, bytes, 0, length);
            bmp.UnlockBits(bitmapData);

            return bytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static byte[] GetRGBAData(this Bitmap bmp)
        {
            var bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var length = bitmapData.Stride * bitmapData.Height;

            byte[] bytes = new byte[length];

            Marshal.Copy(bitmapData.Scan0, bytes, 0, length);
            bmp.UnlockBits(bitmapData);

            for (int i = 0; i < bytes.Length; i+=4)
            {
                var temp = bytes[i];
                bytes[i] = bytes[i + 2];
                bytes[i + 2] = temp;
            }

            return bytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="bound"></param>
        /// <param name="fill"></param>
        /// <param name="border"></param>
        /// <param name="rot"></param>
        public static void DrawFilledRectangle(this Graphics graphics, RectangleF bound, Color fill, Color border)
        {
            using (var colBrush = new SolidBrush(fill))
                graphics.FillRectangle(colBrush, bound);

            using (var colBrush = new Pen(border))
                graphics.DrawRectangle(colBrush, bound);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="pen"></param>
        /// <param name="bounds"></param>
        /// <param name="cornerRadius"></param>
        public static void DrawImage(this Graphics graphics, Image image, RectangleF bound, float rot)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            // store state
            var transform = graphics.Transform;

            // reset 
            graphics.ResetTransform();

            //move rotation point to center of image
            graphics.TranslateTransform(bound.X + bound.Width / 2, bound.Y + bound.Height / 2);

            //rotate
            graphics.RotateTransform((float)(rot * 180 / Math.PI));

            //move image back
            graphics.TranslateTransform(-bound.X - bound.Width / 2, -bound.Y - bound.Height / 2);

            //draw passed in image onto graphics object
            graphics.MultiplyTransform(transform);

            // draw image
            graphics.DrawImage(image, bound);

            // restore state
            graphics.Transform = transform;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="pen"></param>
        /// <param name="bounds"></param>
        /// <param name="cornerRadius"></param>
        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (pen == null)
                throw new ArgumentNullException("pen");

            using (GraphicsPath path = RoundedRect(bounds, cornerRadius))
            {
                graphics.DrawPath(pen, path);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="brush"></param>
        /// <param name="bounds"></param>
        /// <param name="cornerRadius"></param>
        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (brush == null)
                throw new ArgumentNullException("brush");

            using (GraphicsPath path = RoundedRect(bounds, cornerRadius))
            {
                graphics.FillPath(brush, path);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="font"></param>
        /// <param name="Rect"></param>
        /// <param name="BorderRadius"></param>
        /// <param name="BorderWidth"></param>
        /// <param name="TextAlign"></param>
        /// <param name="Padding"></param>
        public static void DrawText(this Graphics g, string text, Color color, Font font, RectangleF Rect, int BorderRadius, int BorderWidth, ContentAlignment TextAlign, Padding Padding)
        {
            float r2 = BorderRadius / 4f;
            float w2 = BorderWidth / 2f;
            Point point = new Point();
            StringFormat format = new StringFormat();

            switch (TextAlign)
            {
                case ContentAlignment.TopLeft:
                    point.X = (int)(Rect.X + r2 / 2 + w2 + Padding.Left);
                    point.Y = (int)(Rect.Y + r2 / 2 + w2 + Padding.Top);
                    format.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopCenter:
                    point.X = (int)(Rect.X + Rect.Width / 2f);
                    point.Y = (int)(Rect.Y + r2 / 2 + w2 + Padding.Top);
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopRight:
                    point.X = (int)(Rect.X + Rect.Width - r2 / 2 - w2 - Padding.Right);
                    point.Y = (int)(Rect.Y + r2 / 2 + w2 + Padding.Top);
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.MiddleLeft:
                    point.X = (int)(Rect.X + r2 / 2 + w2 + Padding.Left);
                    point.Y = (int)(Rect.Y + Rect.Height / 2);
                    format.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleCenter:
                    point.X = (int)(Rect.X + Rect.Width / 2);
                    point.Y = (int)(Rect.Y + Rect.Height / 2);
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    point.X = (int)(Rect.X + Rect.Width - r2 / 2 - w2 - Padding.Right);
                    point.Y = (int)(Rect.Y + Rect.Height / 2);
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomLeft:
                    point.X = (int)(Rect.X + r2 / 2 + w2 + Padding.Left);
                    point.Y = (int)(Rect.Y + Rect.Height - r2 / 2 - w2 - Padding.Bottom);
                    format.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomCenter:
                    point.X = (int)(Rect.X + Rect.Width / 2);
                    point.Y = (int)(Rect.Y + Rect.Height - r2 / 2 - w2 - Padding.Bottom);
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomRight:
                    point.X = (int)(Rect.X + Rect.Width - r2 / 2 - w2 - Padding.Right);
                    point.Y = (int)(Rect.Y + Rect.Height - r2 / 2 - w2 - Padding.Bottom);
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Far;
                    break;
                default:
                    break;
            }

            using (Brush brush = new SolidBrush(color))
                g.DrawString(text, font, brush, point, format);
        }
    }
}
