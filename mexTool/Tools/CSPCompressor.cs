using HSDRaw.Common;
using System.ComponentModel;
using System.Drawing;

namespace mexTool.Tools
{
    public class CSPCompressorSettings
    {
        [DisplayName("Max Width")]
        public int MaxWidth { get; set; } = 136;

        [DisplayName("Max Height")]
        public int MaxHeight { get; set; } = 188;

        [DisplayName("Preserve Aspect Ratio")]
        public bool PreserveAspect { get; set; } = true;
    }

    public class CSPCompressor
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="tobj"></param>
        /// <returns></returns>
        public static bool CompressCSP(CSPCompressorSettings settings, HSD_TOBJ tobj)
        {
            if (tobj.ImageData != null &&
                (tobj.ImageData.Width > settings.MaxWidth || tobj.ImageData.Height > settings.MaxHeight))
            {
                using (var bmp = tobj.ToBitmap())
                {
                    using (var resize = CompressCSP(settings, bmp))
                    {
                        var newtobj = resize.ToTOBJ(HSDRaw.GX.GXTexFmt.CI8, HSDRaw.GX.GXTlutFmt.RGB5A3);
                        tobj.ImageData = newtobj.ImageData;
                        tobj.TlutData = newtobj.TlutData;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static Bitmap CompressCSP(CSPCompressorSettings settings, Bitmap bmp)
        {
            if (bmp.Width > settings.MaxWidth || bmp.Height > settings.MaxHeight)
            {
                if (settings.PreserveAspect)
                {
                    if (settings.MaxWidth > settings.MaxHeight)
                    {
                        return bmp.Resize(false, true, settings.MaxWidth, settings.MaxHeight);
                    }
                    else
                    {
                        return bmp.Resize(true, false, settings.MaxWidth, settings.MaxHeight);
                    }
                }
                else
                {
                    return bmp.Resize(settings.MaxWidth, settings.MaxHeight);
                }
            }

            return bmp.Resize(bmp.Width, bmp.Height);
        }


    }
}
