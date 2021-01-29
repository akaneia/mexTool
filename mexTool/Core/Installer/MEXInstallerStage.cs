using HSDRaw;
using HSDRaw.MEX;
using HSDRaw.MEX.Sounds;
using HSDRaw.MEX.Stages;

namespace mexTool.Core.Installer
{
    public partial class MEXInstaller
    {
        private static readonly uint StageFunctionOffset = 0x3DCEDC;

        private static readonly byte[] MapGOBJEntries = new byte[] {
            0, 4, 21, 7, 12, 7, 11, 3, 12, 5,
            4, 2, 11, 7, 21, 18, 10, 28, 39, 41,
            6, 7, 11, 0, 4, 16, 0, 9, 6, 6,
            4, 4, 7, 4, 38, 4, 7, 10, 2, 3,
            4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
            4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
            4, 4, 4, 4, 4, 4, 5, 11, 3, 3,
            3 };

        public static void InstallStages(MEXDOLScrubber dol, MEX_Data data, HSDRawFile resourceFile)
        {
            // generate stage data
            data.StageData = new MEX_StageData();
            dol.ExtractDataFromMap(data.StageData);

            ExtractDataFromResource(resourceFile, data.StageData);

            var stageCount = data.StageData.CollisionTable.Length;
            data.StageData.StageItemLookup = new HSDArrayAccessor<MEX_ItemLookup>() { Array = new MEX_ItemLookup[stageCount] };
            data.StageData.StagePlaylists = new HSDArrayAccessor<MEX_Playlist>() { Array = new MEX_Playlist[stageCount] };

            // generate stage functions
            data.StageFunctions = new HSDFixedLengthPointerArrayAccessor<MEX_Stage>();

            for (int i = 0; i < 71; i++)
            {
                if (dol.ReadValueAt(StageFunctionOffset + (uint)i * 4) == 0)
                {
                    data.StageFunctions.Set(i, new MEX_Stage());
                    continue;
                }

                var off = MEXDOLScrubber.RAMToDOL(dol.ReadValueAt(StageFunctionOffset + (uint)i * 4));

                var stage = new MEX_Stage() { _s = new HSDStruct(dol.GetSection(off, 0x34)) };

                // gobj functions at 0x04 stride 0x14 unknown number of entries

                if (stage._s.GetInt32(0x08) != 0)
                    stage.StageFileName = dol.ReadStringAt((uint)stage._s.GetInt32(0x08)).Value;

                if (stage.MovingCollisionPointCount > 0 && stage._s.GetInt32(0x2C) != 0)
                {
                    var coloff = MEXDOLScrubber.RAMToDOL((uint)stage._s.GetInt32(0x2C));

                    stage._s.SetReferenceStruct(0x2C, new HSDStruct(dol.GetSection(coloff, 6 * stage.MovingCollisionPointCount)));
                }

                data.StageFunctions.Set(i, stage);
            }


            // Optional Map GOBJs
            //if (settings.IncludeMapGOBJs)
            {
                var mapGOBJStride = 20;
                var stages = data.StageFunctions.Array;
                for (int i = 0; i < stages.Length; i++)
                {
                    var off = (uint)stages[i]._s.GetInt32(0x04);

                    if (off == 0)
                        continue;

                    off = MEXDOLScrubber.RAMToDOL(off);

                    stages[i]._s.SetReferenceStruct(0x04, new HSDStruct(dol.GetSection(off, MapGOBJEntries[i] * mapGOBJStride)));
                }
                data.StageFunctions.Array = stages;
            }
        }
    }
}
