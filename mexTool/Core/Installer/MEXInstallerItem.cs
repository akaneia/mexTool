using HSDRaw;
using HSDRaw.MEX;

namespace mexTool.Core.Installer
{
    public partial class MEXInstaller
    {
        private static readonly byte[] CommonItemStates = new byte[] { 7, 10, 8, 8, 10, 10, 14, 12, 6, 6, 1, 6, 6, 14, 10, 8, 6, 6, 4, 8, 8, 6, 6, 6, 10, 8, 2, 2, 6, 6, 6, 6, 6, 6, 8, 1, 1, 2, 10, 1, 4, 6, 6 };
        private static readonly byte[] FighterItemStates = new byte[] { 12, 18, 10, 12, 2, 1, 8, 1, 1, 1, 3, 2, 2, 3, 3, 8, 8, 4, 4, 10, 10, 6, 6, 1, 1, 3, 1, 3, 3, 3, 3, 12, 12, 10, 10, 1, 6, 1, 3, 3, 2, 2, 1, 3, 3, 1, 2, 1, 2, 1, 4, 10, 4, 10, 6, 2, 6, 1, 2, 4, 3, 2, 1, 4, 1, 2, 1, 1, 1, 18, 4, 4, 1, 1, 2, 2, 2, 1, 2, 2, 2, 2, 1, 2, 1, 2, 2, 1, 8, 1, 4, 2, 1, 2, 2, 12, 12, 6, 6, 10, 10, 18, 3, 1, 2, 1, 2, 1, 10, 6, 1, 1, 2, 1, 3, 3, 6, 20 };
        private static readonly byte[] PokemonItemStates = new byte[] { 6, 3, 3, 4, 3, 4, 3, 3, 3, 3, 4, 3, 2, 2, 2, 6, 8, 6, 6, 3, 6, 8, 3, 3, 3, 8, 2, 3, 4, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 6, 1, 0 };
        private static readonly byte[] StageItemStates = new byte[] { 12, 1, 6, 12, 8, 22, 0, 0, 12, 12, 6, 12, 14, 14, 0, 0, 0, 8, 8, 0, 0, 0, 10, 0, 0, 12, 8, 2, 6 };

        public static void InstallItems(MEXDOLScrubber dol, MEX_Data data, HSDRawFile resourceFile)
        {
            // generate item table
            data.ItemTable = new MEX_ItemTables();
            dol.ExtractDataFromMap(data.ItemTable);
            data.ItemTable._s.SetReferenceStruct(0x14, new HSDStruct(4));

            // Optional Item States
            //if (settings.IncludeItemStates)
            {
                var itemStride = 0x10;
                for (int i = 0; i < data.ItemTable.CommonItems.Length; i++)
                {
                    var off = (uint)data.ItemTable.CommonItems._s.GetInt32(i * 0x3C);
                    //System.Diagnostics.Debug.WriteLine($"{GUI.MEX.DefaultItemNames.CommonItemNames[i],-40} \v0x{off.ToString("X8")} \v0x{DOLScrubber.RAMToDOL(off).ToString("X8")} \v{CommonItemStates[i]}");

                    if (off == 0 || CommonItemStates[i] == 0)
                        continue;
                    off = MEXDOLScrubber.RAMToDOL(off);
                    data.ItemTable.CommonItems._s.SetReferenceStruct(i * 0x3C, new HSDStruct(dol.GetSection(off, CommonItemStates[i] * itemStride)));
                }
                for (int i = 0; i < data.ItemTable.FighterItems.Length; i++)
                {
                    var off = (uint)data.ItemTable.FighterItems._s.GetInt32(i * 0x3C);
                    //System.Diagnostics.Debug.WriteLine($"{GUI.MEX.DefaultItemNames.FighterItemNames[i],-40} \v0x{off.ToString("X8")} \v0x{DOLScrubber.RAMToDOL(off).ToString("X8")} \v{FighterItemStates[i]}");

                    if (off == 0 || FighterItemStates[i] == 0)
                        continue;
                    off = MEXDOLScrubber.RAMToDOL(off);
                    data.ItemTable.FighterItems._s.SetReferenceStruct(i * 0x3C, new HSDStruct(dol.GetSection(off, FighterItemStates[i] * itemStride)));
                }
                for (int i = 0; i < data.ItemTable.Pokemon.Length; i++)
                {
                    var off = (uint)data.ItemTable.Pokemon._s.GetInt32(i * 0x3C);
                    //System.Diagnostics.Debug.WriteLine($"{GUI.MEX.DefaultItemNames.PokemonItemNames[i],-40} \v0x{off.ToString("X8")} \v0x{DOLScrubber.RAMToDOL(off).ToString("X8")} \v{PokemonItemStates[i]}");

                    if (off == 0 || PokemonItemStates[i] == 0)
                        continue;
                    off = MEXDOLScrubber.RAMToDOL(off);
                    data.ItemTable.Pokemon._s.SetReferenceStruct(i * 0x3C, new HSDStruct(dol.GetSection(off, PokemonItemStates[i] * itemStride)));
                }
                for (int i = 0; i < data.ItemTable.StageItems.Length; i++)
                {
                    var off = (uint)data.ItemTable.StageItems._s.GetInt32(i * 0x3C);
                    //System.Diagnostics.Debug.WriteLine($"{GUI.MEX.DefaultItemNames.StageItemNames[i],-40} \v0x{off.ToString("X8")} \v0x{DOLScrubber.RAMToDOL(off).ToString("X8")} \v{StageItemStates[i]}");

                    if (off == 0 || StageItemStates[i] == 0)
                        continue;
                    off = MEXDOLScrubber.RAMToDOL(off);
                    data.ItemTable.StageItems._s.SetReferenceStruct(i * 0x3C, new HSDStruct(dol.GetSection(off, StageItemStates[i] * itemStride)));
                }
            }

        }
    }
}
