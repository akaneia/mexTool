using HSDRaw;
using HSDRaw.MEX;

namespace mexTool.Core.Installer
{
    public partial class MEXInstaller
    {
        private static readonly int[] MajorSceneMinorCounts = new int[] { 2, 9, 26, 49, 29, 16, 2, 2, 2, 5, 2, 2, 2, 3, 3, 9, 9, 9, 9, 4, 2, 5, 5, 5, 7, 13, 3, 8, 4, 9, 9, 4, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 9, 3, 9, 0 };
        
        public static void InstallScenes(MEXDOLScrubber dol, MEX_Data data, HSDRawFile resourceFile)
        {
            // Scenes
            data.SceneData = new HSDRaw.MEX.Scenes.MEX_SceneData();
            dol.ExtractDataFromMap(data.SceneData);

            {
                var majordata = data.SceneData.MajorScenes._s;

                var ar = new HSDRaw.MEX.Scenes.MEX_MajorScene[majordata.Length / 0x14];

                for (int i = 0; i < ar.Length; i++)
                {
                    ar[i] = new HSDRaw.MEX.Scenes.MEX_MajorScene()
                    {
                        _s = majordata.GetEmbeddedStruct(0x14 * i, 0x14)
                    };

                    ar[i]._s.Resize(0x18);

                    var ramOffset = (uint)ar[i]._s.GetInt32(0x10);

                    if (ramOffset > 0)
                    {
                        var offset = MEXDOLScrubber.RAMToDOL(ramOffset);
                        ar[i].MinorScene = new HSDArrayAccessor<HSDRaw.MEX.Scenes.MEX_MinorScene>();
                        ar[i].MinorScene._s.SetData(dol.GetSection(offset, 0x18 * MajorSceneMinorCounts[i]));
                    }
                }

                data.SceneData.MajorScenes.Array = ar;

            }

            for (int i = 0; i < data.SceneData.MajorScenes.Length; i++)
            {
                var scene = data.SceneData.MajorScenes[i];

                if (scene._s.GetInt32(0x10) == 0)
                    continue;

                var tableStart = MEXDOLScrubber.RAMToDOL((uint)scene._s.GetInt32(0x10));

                int size = 0;
                while (true)
                {
                    var val = dol.ReadValueAt((uint)(tableStart + 0x18 * size)) >> 24;
                    size++;
                    if (val == 0xFF)
                        break;
                }

                if (size > 0)
                {
                    scene.MinorScene = new HSDArrayAccessor<HSDRaw.MEX.Scenes.MEX_MinorScene>()
                    {
                        _s = new HSDStruct(dol.GetSection(tableStart, size * 0x18))
                    };
                    data.SceneData.MajorScenes[i] = scene;
                }
            }

        }
    }
}
