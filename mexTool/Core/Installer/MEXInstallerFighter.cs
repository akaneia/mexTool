using HSDRaw;
using HSDRaw.Common;
using HSDRaw.MEX;
using HSDRaw.MEX.Characters;
using System;
using System.Linq;

namespace mexTool.Core.Installer
{
    public partial class MEXInstaller
    {
        private static readonly uint CharStringOffset = 0x3BEF40;
        private static readonly uint VIFiles = 0x3FCFA8;

        private static readonly uint CostumePointerOffset = 0x3BDEC0;
        private static readonly uint CostumeStringOffset = 0x3BF360;
        private static readonly uint ftDemoStringOffset = 0x3BF468;
        private static readonly uint CharAnimStringOffset = 0x3BF3E4;

        private static readonly uint EndClassicStringOffset = 0x3D88B8;
        private static readonly uint EndAllStarStringOffset = 0x3D8F10;
        private static readonly uint EndAdventureStringOffset = 0x3D8BF4;
        private static readonly uint EndMovieStringOffset = 0x3D81F4;

        private static readonly byte[] MoveLogicEntries = new byte[] {
                              12,35,23,46,203,23,21,24,36,30,
                              26,26,26,18,28,32,20,20,32,18,
                              21,10,35,26,40,23,32,50,49,0,
                              0,24,1};

        public static void InstallFighters(MEXDOLScrubber dol, MEX_Data data, HSDRawFile resourceFile)
        {
            // generate fighter table
            data.FighterData = new MEX_FighterData();
            dol.ExtractDataFromMap(data.FighterData);
            ExtractDataFromResource(resourceFile, data.FighterData);

            data.FighterData.DefineIDs.Add(new MEX_CharDefineIDs());
            data.FighterData.SSMFileIDs.Add(new MEX_CharSSMFileID());


            // convert fighter songs from bytes to shorts
            {
                var d = dol.GetSection((uint)MEXDOLScrubber.dolMap["FighterSongIDs"].Item1, MEXDOLScrubber.dolMap["FighterSongIDs"].Item2);
                data.FighterData.FighterSongIDs.Array = Enumerable.Range(0, d.Length / 2).Select(i => new MEX_FighterSongID()
                {
                    SongID1 = d[i * 2],
                    SongID2 = d[i * 2 + 1]
                }).ToArray();
            }


            // costume strings and runtime setup
            data.FighterData.CostumePointers = new HSDArrayAccessor<MEX_CostumeRuntimePointers>() { _s = dol.GetStruct(new Tuple<int, int>((int)CostumePointerOffset, 0x108)) };
            data.FighterData.CostumeFileSymbols = new HSDArrayAccessor<MEX_CostumeFileSymbolTable>();
            for (int i = 0; i < data.FighterData.CostumePointers.Length; i++)
            {
                data.FighterData.CostumePointers._s.SetReferenceStruct(i * 8, new HSDStruct(0x18 * data.FighterData.CostumePointers[i].CostumeCount));

                if (data.FighterData.CostumePointers[i].CostumeCount > 0)
                {
                    var addr = MEXDOLScrubber.RAMToDOL(dol.ReadValueAt(CostumeStringOffset + (uint)i * 4));

                    MEX_CostumeFileSymbolTable costume = new MEX_CostumeFileSymbolTable();

                    var strings = dol.ReadStringTable(addr, data.FighterData.CostumePointers[i].CostumeCount * 3);

                    for (int j = 0; j < data.FighterData.CostumePointers[i].CostumeCount; j++)
                    {
                        costume.CostumeSymbols.Add(new MEX_CostumeFileSymbol()
                        {
                            FileName = strings[j * 3]?.Value,
                            JointSymbol = strings[j * 3 + 1]?.Value,
                            MatAnimSymbol = strings[j * 3 + 2]?.Value,
                            VisibilityLookupIndex = j
                        });
                    }

                    data.FighterData.CostumeFileSymbols.Set(i, costume);
                }
            }
            data.FighterData.CostumeFileSymbols.Add(new MEX_CostumeFileSymbolTable());

            // anim file strings
            data.FighterData.AnimFiles = new HSDFixedLengthPointerArrayAccessor<HSD_String>() { Array = dol.ReadStringTable(CharAnimStringOffset, data.MetaData.NumOfInternalIDs) };

            // vi files
            data.FighterData.VIFiles = new HSDFixedLengthPointerArrayAccessor<HSD_String>() { Array = dol.ReadStringTable(VIFiles, 27) };

            // image files
            data.FighterData.EndClassicFiles = new HSDFixedLengthPointerArrayAccessor<HSD_String>() { Array = dol.ReadStringTable(EndClassicStringOffset, 26) };
            data.FighterData.EndAdventureFiles = new HSDFixedLengthPointerArrayAccessor<HSD_String>() { Array = dol.ReadStringTable(EndAdventureStringOffset, 26) };
            data.FighterData.EndAllStarFiles = new HSDFixedLengthPointerArrayAccessor<HSD_String>() { Array = dol.ReadStringTable(EndAllStarStringOffset, 26) };
            data.FighterData.EndMovieFiles = new HSDFixedLengthPointerArrayAccessor<HSD_String>() { Array = dol.ReadStringTable(EndMovieStringOffset, 26) };

            // ftDemo strings
            data.FighterData.FtDemo_SymbolNames = new HSDFixedLengthPointerArrayAccessor<MEX_FtDemoSymbolNames>();
            for (uint i = 0; i < 27; i++)
            {
                var addr = MEXDOLScrubber.RAMToDOL(dol.ReadValueAt(ftDemoStringOffset + i * 4));

                data.FighterData.FtDemo_SymbolNames.Add(
                    new MEX_FtDemoSymbolNames()
                    {
                        _s = new HSDFixedLengthPointerArrayAccessor<HSD_String>()
                        {
                            Array = dol.ReadStringTable(addr, 4)
                        }._s
                    }
                    );
            }


            // character file strings
            data.FighterData._s.SetReferenceStruct(0x40, new HSDStruct(0x108));
            data.FighterData.CharFiles = new HSDArrayAccessor<MEX_CharFileStrings>()
            {
                _s = new HSDFixedLengthPointerArrayAccessor<HSD_String>()
                {
                    Array = dol.ReadStringTable(CharStringOffset, data.MetaData.NumOfInternalIDs * 2)
                }._s
            };

            // blank mex data
            data.FighterData.FighterItemLookup = new HSDArrayAccessor<MEX_ItemLookup>() { Array = new MEX_ItemLookup[data.MetaData.NumOfInternalIDs] };


            // generate figther functions
            data.FighterFunctions = new MEX_FighterFunctionTable();
            dol.ExtractDataFromMap(data.FighterFunctions);

            data.FighterFunctions.enterFloat.Array = new uint[33];
            data.FighterFunctions.enterSpecialDoubleJump.Array = new uint[33];
            data.FighterFunctions.enterTether.Array = new uint[33];
            data.FighterFunctions.onLand.Array = new uint[33];
            data.FighterFunctions.onSmashDown.Array = new uint[33];
            data.FighterFunctions.onSmashForward.Array = new uint[33];
            data.FighterFunctions.onSmashUp.Array = new uint[33];
            data.FighterFunctions.onIntroL.Array = new uint[33];
            data.FighterFunctions.onIntroR.Array = new uint[33];
            data.FighterFunctions.onCatch.Array = new uint[33];
            data.FighterFunctions.onAppeal.Array = new uint[33];
            data.FighterFunctions.getTrailData.Array = new uint[33];

            // special double jump code
            for (int i = 0; i < data.FighterFunctions.enterSpecialDoubleJump.Length; i++)
            {
                switch (i)
                {
                    case 0x08: // Ness
                        data.FighterFunctions.enterSpecialDoubleJump[i] = 0x800cbd18;
                        break;
                    case 0x0E: // Yoshi
                        data.FighterFunctions.enterSpecialDoubleJump[i] = 0x800cbe98;
                        break;
                    case 0x09: // Peach
                        data.FighterFunctions.enterSpecialDoubleJump[i] = 0x800cc0e8;
                        break;
                    case 0x10: // Mewtwo
                        data.FighterFunctions.enterSpecialDoubleJump[i] = 0x800cc238;
                        break;
                    default:
                        data.FighterFunctions.enterSpecialDoubleJump[i] = 0x800cbbc0;
                        break;
                }

            }

            // Optional Move Logic
            //if (settings.IncludeMoveLogic)
            {
                var moveLogicStruct = data.FighterFunctions._s.GetReference<HSDAccessor>(0x0C);
                var movelogicStride = 0x20;
                for (int i = 0; i < data.MetaData.NumOfInternalIDs; i++)
                {
                    // get move logic pointer
                    var off = (uint)moveLogicStruct._s.GetInt32(i * 4);

                    // null pointer skips
                    if (off == 0 || MoveLogicEntries[i] == 0)
                        continue;

                    // convert ram offset to dol
                    off = MEXDOLScrubber.RAMToDOL(off);

                    // set the pointer to data
                    moveLogicStruct._s.SetReferenceStruct(i * 4, new HSDStruct(dol.GetSection(off, MoveLogicEntries[i] * movelogicStride)));
                }
            }
        }
    }
}
