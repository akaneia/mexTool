using HSDRaw;
using HSDRaw.Common;
using HSDRaw.MEX;
using HSDRaw.MEX.Sounds;

namespace mexTool.Core.Installer
{
    public partial class MEXInstaller
    {
        private static readonly uint SSMStringOffset = 0x3B8CFC;

        private static readonly uint MusicStringOffset = 0x3B9314;

        public static void InstallSounds(MEXDOLScrubber dol, MEX_Data data, HSDRawFile resourceFile)
        {
            // generate ssm table
            data.SSMTable = new MEX_SSMTable();
            dol.ExtractDataFromMap(data.SSMTable);

            data.SSMTable.SSM_SSMFiles = new HSDNullPointerArrayAccessor<HSD_String>() { Array = dol.ReadStringTable(SSMStringOffset, data.MetaData.NumOfSSMs + 1) };
            data.SSMTable.SSM_Runtime = new HSDAccessor() { _s = new HSDStruct(0x18) };
            data.SSMTable.SSM_Runtime._s.SetReferenceStruct(0x00, new HSDStruct(0x180));
            data.SSMTable.SSM_Runtime._s.SetReferenceStruct(0x04, new HSDStruct(0xDC));
            data.SSMTable.SSM_Runtime._s.SetReferenceStruct(0x08, new HSDStruct(0xDC));
            data.SSMTable.SSM_Runtime._s.SetReferenceStruct(0x0C, new HSDStruct(0xDC));
            data.SSMTable.SSM_Runtime._s.SetReferenceStruct(0x10, new HSDStruct(0xDC));
            data.SSMTable.SSM_Runtime._s.SetReferenceStruct(0x14, new HSDStruct(0xDC));

            // add null entry
            data.SSMTable.SSM_SSMFiles.Add(new HSD_String("null.ssm"));

            // generate music table
            data.MusicTable = new MEX_BGMStruct();
            ExtractDataFromResource(resourceFile, data.MusicTable);
            data.MusicTable.BGMFileNames = new HSDFixedLengthPointerArrayAccessor<HSD_String>() { Array = dol.ReadStringTable(MusicStringOffset, data.MetaData.NumOfMusic) };
            data.MusicTable.MenuPlaylist = new HSDArrayAccessor<MEX_PlaylistItem>() { Array = new MEX_PlaylistItem[] { new MEX_PlaylistItem() { ChanceToPlay = 100, HPSID = 52 } } };
            data.MusicTable.MenuPlayListCount = 1;
        }
    }
}
