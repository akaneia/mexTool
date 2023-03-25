using HSDRaw.Common;
using HSDRaw.GX;
using HSDRaw.MEX;
using mexTool.GUI;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using YamlDotNet.Serialization;

namespace mexTool.Core
{
    public class MEXCostume: IDrawableListItem
    {
        /// <summary>
        /// 
        /// </summary>
        [YamlIgnore]
        public MEX_CostumeFileSymbol Costume = new MEX_CostumeFileSymbol();

        /// <summary>
        /// 
        /// </summary>
        [YamlIgnore]
        public HSD_TOBJ Icon = new HSD_TOBJ();

        /// <summary>
        /// 
        /// </summary>
        [YamlIgnore]
        public HSD_TOBJ CSP;


        [Category("Costume"), DisplayName("Filename"), Description("Filename of the costume")]
        public string FileName { get => Costume.FileName; set => Costume.FileName = value; }

        [Category("Costume"), DisplayName("Model Symbol"), Description("Joint symbol inside the costume dat. Leave blank if there isn't one.")]
        public string ModelSymbol { get => Costume.JointSymbol; set => Costume.JointSymbol = value; }

        [Category("Costume"), DisplayName("Material Symbol"), Description("Material symbol inside the costume dat. Leave blank if there isn't one.")]
        public string MaterialSymbol { get => Costume.MatAnimSymbol; set => Costume.MatAnimSymbol = value; }

        [Category("Costume"), DisplayName("Visibility Table Index"), Description("Table to use for visibility lookup. Unless specific otherwise, use 0")]
        public int VisibilityIndex { get => Costume.VisibilityLookupIndex; set => Costume.VisibilityLookupIndex = value; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Image ToImage()
        {
            if (Icon == null)
                return null;

            return Icon.ToBitmap();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] ToPackage()
        {
            using (MemoryStream zipToOpen = new MemoryStream())
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                {
                    {
                        var dat = GetCostumeFileData();
                        ZipArchiveEntry file = archive.CreateEntry(FileName);
                        if (dat != null && dat.Length > 0)
                            using (var o = file.Open())
                                o.Write(dat, 0, dat.Length);
                    }

                    if (Icon != null && Icon.ImageData != null)
                    {
                        ZipArchiveEntry stc = archive.CreateEntry("stc.png");
                        using (var img = Icon.ToBitmap())
                        using (var o = stc.Open())
                            img.Save(o, System.Drawing.Imaging.ImageFormat.Png);
                    }

                    if (CSP != null && CSP.ImageData != null)
                    {
                        ZipArchiveEntry csp = archive.CreateEntry("csp.png");
                        using (var img = CSP.ToBitmap())
                        using (var o = csp.Open())
                            img.Save(o, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }

                return zipToOpen.ToArray();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetCostumeFileData()
        {
            return MEX.ImageResource.GetFileData(FileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        public void FromImage(Bitmap bmp)
        {
            Icon = new HSD_TOBJ()
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

            if (bmp.Width > 24 || bmp.Height > 24)
            {
                using (var resize = bmp.Resize(24, 24, System.Drawing.Drawing2D.InterpolationMode.Default))
                    Icon.EncodeImageData(resize.GetBGRAData(), bmp.Width, bmp.Height, GXTexFmt.CI4, GXTlutFmt.RGB5A3);
            }
            else
            {
                Icon.EncodeImageData(bmp.GetBGRAData(), bmp.Width, bmp.Height, GXTexFmt.CI4, GXTlutFmt.RGB5A3);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return FileName == null ? "Icon" : FileName;
        }
    }
}
