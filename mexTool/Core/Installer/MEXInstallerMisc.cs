using HSDRaw;
using HSDRaw.MEX;
using System.Drawing;

namespace mexTool.Core.Installer
{
    public partial class MEXInstaller
    {
        public static void InstallMisc(MEXDOLScrubber dol, MEX_Data data, HSDRawFile resourceFile)
        {
            data.MiscData = new HSDRaw.MEX.Misc.MEX_Misc();

            data.MiscData.GawColors = new HSDArrayAccessor<HSDRaw.MEX.Misc.MEX_GawColor>()
            {
                Array = new HSDRaw.MEX.Misc.MEX_GawColor[]
                {
                    new HSDRaw.MEX.Misc.MEX_GawColor() { FillColor = Color.FromArgb(0xFF, 0x00, 0x00, 0x00), OutlineColor = Color.FromArgb(0x80, 0xFF, 0xFF, 0xFF) },
                    new HSDRaw.MEX.Misc.MEX_GawColor() { FillColor = Color.FromArgb(0xFF, 0x6E, 0x00, 0x00), OutlineColor = Color.FromArgb(0x80, 0xFF, 0xFF, 0xFF) },
                    new HSDRaw.MEX.Misc.MEX_GawColor() { FillColor = Color.FromArgb(0xFF, 0x00, 0x00, 0x6E), OutlineColor = Color.FromArgb(0x80, 0xFF, 0xFF, 0xFF) },
                    new HSDRaw.MEX.Misc.MEX_GawColor() { FillColor = Color.FromArgb(0xFF, 0x00, 0x6E, 0x00), OutlineColor = Color.FromArgb(0x80, 0xFF, 0xFF, 0xFF) },
                }
            };
        }
    }
}
