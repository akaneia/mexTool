using HSDRaw;
using HSDRaw.Common;
using HSDRaw.MEX;
using System.Linq;

namespace mexTool.Core.Installer
{
    public partial class MEXInstaller
    {
        private static readonly uint KirbyCapOffset = 0x3C79D0;
        private static readonly uint KirbyCostumeOffset = 0x3C83E8;

        public static void InstallKirby(MEXDOLScrubber dol, MEX_Data data, HSDRawFile resourceFile)
        {
            // generate kirby data table
            data.KirbyData = new MEX_KirbyTable();
            dol.ExtractDataFromMap(data.KirbyData);
            data.KirbyData.CapFiles = new HSDArrayAccessor<MEX_KirbyCapFiles>()
            {
                _s = new HSDFixedLengthPointerArrayAccessor<HSD_String>()
                {
                    Array = dol.ReadStringTable(KirbyCapOffset, 33 * 2)
                }._s
            };
            data.KirbyData.KirbyCostumes = new HSDFixedLengthPointerArrayAccessor<MEX_KirbyCostume>();
            for (uint i = 0; i < 33; i++)
            {
                var ramaddr = MEXDOLScrubber.RAMToDOL(dol.ReadValueAt(KirbyCostumeOffset + i * 4));

                if(ramaddr != 0)
                {
                    var symbols = dol.ReadStringTable(ramaddr, 6 * 3);

                    var costumes = new MEX_CostumeFileSymbol[6];

                    for (int j = 0; j < costumes.Length; j++)
                    {
                        costumes[j] = new MEX_CostumeFileSymbol()
                        {
                            FileName = symbols[j * 3 + 0].Value,
                            JointSymbol = symbols[j * 3 + 1].Value,
                            MatAnimSymbol = symbols[j * 3 + 2].Value,
                            VisibilityLookupIndex = j
                        };
                    }

                    data.KirbyData.KirbyCostumes.Add(new MEX_KirbyCostume() { Array = costumes });
                }
                else
                    data.KirbyData.KirbyCostumes.Add(null);
            }

            // note: runtimes aren't really needed here
            data.KirbyData.CapFileRuntime = new HSDAccessor() { _s = new HSDStruct(0x100) };
            data.KirbyData.CapFtCmdRuntime = new HSDAccessor() { _s = new HSDStruct(0x100) };
            data.KirbyData.MoveLogicRuntime = new HSDAccessor() { _s = new HSDStruct(0x100) };
            data.KirbyData.CostumeRuntime = new HSDAccessor() { _s = new HSDStruct(0xB8) };
            data.KirbyData.CostumeRuntime._s.SetReferenceStruct(0x0C, new HSDStruct(0x30));
            data.KirbyData.CostumeRuntime._s.SetReferenceStruct(0x3C, new HSDStruct(0x30));
            data.KirbyData.CostumeRuntime._s.SetReferenceStruct(0x40, new HSDStruct(0x30));
            data.KirbyData.CostumeRuntime._s.SetReferenceStruct(0x58, new HSDStruct(0x30));
            data.KirbyData.CostumeRuntime._s.SetReferenceStruct(0x60, new HSDStruct(0x30));


            // generate kirby function table
            data.KirbyFunctions = new MEX_KirbyFunctionTable();
            dol.ExtractDataFromMap(data.KirbyFunctions);
            var abtb = new HSDUIntArray() { _s = dol.GetStruct(MEXDOLScrubber.dolMap["KirbyAbility"]) };
            data.KirbyFunctions.OnAbilityGain = new HSDUIntArray() { Array = abtb.Array.Where((e, i) => i % 2 == 0).ToArray() };
            data.KirbyFunctions.OnAbilityLose = new HSDUIntArray() { Array = abtb.Array.Where((e, i) => i % 2 == 1).ToArray() };
            data.KirbyFunctions.KirbyOnHit = new HSDUIntArray() { Array = new uint[data.MetaData.NumOfInternalIDs] };
            data.KirbyFunctions.KirbyOnItemInit = new HSDUIntArray() { Array = new uint[data.MetaData.NumOfInternalIDs] };
        }
    }
}
