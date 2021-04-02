using HSDRaw;
using HSDRaw.Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace mexTool.Core.Installer
{

    /// <summary>
    /// 
    /// </summary>
    public class MEXDOLScrubber : IDisposable
    {
        private BinaryReaderExt r;

        /// <summary>
        /// Offsets in dol to data tables
        /// </summary>
        public static readonly Dictionary<string, Tuple<int, int>> dolMap = new Dictionary<string, Tuple<int, int>>()
        {

        // Menu

            { "CSSIconData", new Tuple<int, int>(0x3EDA48, 0x398) },
            { "SSSIconData", new Tuple<int, int>(0x3ED6D0, 0x348) },
            
	    // Fighter Data

            { "DefineIDs", new Tuple<int, int>(0x3B9DE0, 0x64) },
            { "CostumeIDs", new Tuple<int, int>(0x3D21A0, 0x68) },
            { "AnimCount", new Tuple<int, int>(0x3BDFC8, 0x110) },
            { "EffectIDs", new Tuple<int, int>(0x3BF6FC, 0x21) },
            { "ResultScale", new Tuple<int, int>(0x3D4058, 0x68) },
            { "SSMFileIDs", new Tuple<int, int>(0x3B83C0, 0x210) },
            { "RstRuntime", new Tuple<int, int>(0x3BF5F4, 0x110) },
            { "FighterSongIDs", new Tuple<int, int>(0x3B94A0, 0x40) },
            //{ "ClassicTrophyLookup", new Tuple<int, int>(0x3B49BC, 0x34) },
            //{ "AdventureTrophyLookup", new Tuple<int, int>(0x3B4978, 0x34) },
            //{ "AllStarTrophyLookup", new Tuple<int, int>(0x3B4A00, 0x34) },
           

	    // Fighter Functions

            { "OnLoad", new Tuple<int, int>(0x3BE154, 0x84) },
            { "OnDeath", new Tuple<int, int>(0x3BE1D8, 0x84) },
            { "OnUnknown", new Tuple<int, int>(0x3BE25C, 0x84) },
            { "MoveLogic", new Tuple<int, int>(0x3BE2E0, 0x84) },
            { "SpecialN", new Tuple<int, int>(0x3BE67C, 0x84) },
            { "SpecialNAir", new Tuple<int, int>(0x3BE5F8, 0x84) },
            { "SpecialS", new Tuple<int, int>(0x3BE3E8, 0x84) },
            { "SpecialSAir", new Tuple<int, int>(0x3BE574, 0x84) },
            { "SpecialHi", new Tuple<int, int>(0x3BE784, 0x84) },
            { "SpecialHiAir", new Tuple<int, int>(0x3BE46C, 0x84) },
            { "SpecialLw", new Tuple<int, int>(0x3BE700, 0x84) },
            { "SpecialLwAir", new Tuple<int, int>(0x3BE4F0, 0x84) },
            { "OnAbsorb", new Tuple<int, int>(0x3BE808, 0x84) },
            { "onItemPickup", new Tuple<int, int>(0x3BE88C, 0x84) },
            { "onMakeItemInvisible", new Tuple<int, int>(0x3BE910, 0x84) },
            { "onMakeItemVisible", new Tuple<int, int>(0x3BE994, 0x84) },
            { "onItemDrop", new Tuple<int, int>(0x3BEA18, 0x84) },
            { "onItemCatch", new Tuple<int, int>(0x3BEA9C, 0x84) },
            { "onUnknownItemRelated", new Tuple<int, int>(0x3BEB20, 0x84) },
            { "onApplyHeadItem", new Tuple<int, int>(0x3BEBA4, 0x84) },
            { "onRemoveHeadItem", new Tuple<int, int>(0x3BEC28, 0x84) },
            { "onHit", new Tuple<int, int>(0x3BECAC, 0x84) },
            { "onUnknownEyeTextureRelated", new Tuple<int, int>(0x3BED30, 0x84) },
            { "onFrame", new Tuple<int, int>(0x3BEDB4, 0x84) },
            { "onActionStateChange", new Tuple<int, int>(0x3BEE38, 0x84) },
            { "onRespawn", new Tuple<int, int>(0x3BEEBC, 0x84) },
            { "onModelRender", new Tuple<int, int>(0x3BF0CC, 0x84) },
            { "onShadowRender", new Tuple<int, int>(0x3BF150, 0x84) },
            { "onUnknownMultijump", new Tuple<int, int>(0x3BF1D4, 0x84) },
            { "onActionStateChangeWhileEyeTextureIsChanged", new Tuple<int, int>(0x3BF258, 0x84) },
            { "onTwoEntryTable", new Tuple<int, int>(0x3BF5F4, 0x108) },
            { "onExtRstAnim", new Tuple<int, int>(0x3BF4EC, 0x84) },
            { "onIndexExtResultAnim", new Tuple<int, int>(0x3BF570, 0x84) },
            { "DemoMoveLogic", new Tuple<int, int>(0x3BE364, 0x84) },
			
            // SSM

            { "SSM_BufferSizes", new Tuple<int, int>(0x3B94E4, 0x1C0) },
            { "SSM_LookupTable", new Tuple<int, int>(0x3B85D0, 0xE0) },

            // Item

            { "CommonItems", new Tuple<int, int>(0x3EE4C4, 0xA14) },
            { "FighterItems", new Tuple<int, int>(0x3F0100, 0x1BA8) },
            { "Pokemon", new Tuple<int, int>(0x3EF3CC, 0xB04) },
            { "StageItems", new Tuple<int, int>(0x3F1D20, 0x6CC) },

            // kirby 

	        { "KirbyEffectIDs", new Tuple<int, int>(0x3C846C, 0x21) },

            { "KirbyAbility", new Tuple<int, int>(0x3C6CC8, 0x108) },
            { "KirbySpecialN", new Tuple<int, int>(0x3C6DD0, 0x84) },
            { "KirbySpecialNAir", new Tuple<int, int>(0x3C6E54, 0x84) },
            
	        // Stage Data

	        { "StageIDTable", new Tuple<int, int>(0x3E6960, 0xD68) },
            { "ReverbTable", new Tuple<int, int>(0x3B86B0, 0xD8) }, // is actually larger, but entries are unused
	        { "CollisionTable", new Tuple<int, int>(0x3BC248, 0x238) },
            
            
	        // Scene

	        { "MajorScenes", new Tuple<int, int>(0x3D7CA4, 0x398) },
            { "MinorSceneFunctions", new Tuple<int, int>(0x3D7920, 0x384) },


        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dolFile"></param>
        public MEXDOLScrubber(string dolFile)
        {
            r = new BinaryReaderExt(new FileStream(dolFile, FileMode.Open));
            r.BigEndian = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dolFile"></param>
        public MEXDOLScrubber(byte[] dolFile)
        {
            r = new BinaryReaderExt(new MemoryStream(dolFile));
            r.BigEndian = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dolAddr"></param>
        /// <returns></returns>
        public uint ReadValueAt(uint dolAddr)
        {
            r.Position = dolAddr;
            return r.ReadUInt32();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loc"></param>
        public HSDStruct GetStruct(Tuple<int, int> loc)
        {
            r.BaseStream.Position = loc.Item1;
            HSDStruct s = new HSDStruct(r.ReadBytes(loc.Item2));
            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ramAddr"></param>
        /// <returns></returns>
        public static uint DOLToRAM(uint dolAddr)
        {
            if (dolAddr == 0)
                return 0;

            else if (dolAddr + 0x80003000 <= 0x804D0000)
                return dolAddr + 0x80003000;
            else
                return dolAddr + 0x800a4fe0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ramAddr"></param>
        /// <returns></returns>
        public static uint RAMToDOL(uint ramAddr)
        {
            if (ramAddr == 0)
                return 0;

            else if (ramAddr <= 0x804D0000)
                return ramAddr - 0x80003000;
            else
                return ramAddr - 0x800a4fe0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public byte[] GetSection(uint doloffset, int size)
        {
            return r.GetSection(doloffset, size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public HSD_String[] ReadStringTable(uint position, int length, bool shiftJis = false)
        {
            HSD_String[] s = new HSD_String[length];

            for (int i = 0; i < length; i++)
            {
                r.Position = position + (uint)i * 4;
                s[i] = ReadStringAt(r.ReadUInt32(), shiftJis);
            }

            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ramOffset"></param>
        public HSD_String ReadStringAt(uint ramOffset, bool shiftJis = false)
        {
            if (ramOffset == 0 || ramOffset == 0xFFFFFFFF)
                return null;

            var off = RAMToDOL(ramOffset);

            r.BaseStream.Position = off;

            var str = "";

            if (shiftJis)
            {
                byte[] c = r.ReadBytes(2);
                while (c.Length >= 2 && !(c[0] == 0 && c[1] == 0))
                {
                    str += System.Text.Encoding.GetEncoding(932).GetString(c);
                    c = r.ReadBytes(2);
                }
            }
            else
            {
                byte c = r.ReadByte();
                while (c != 0)
                {
                    str += (char)c;
                    c = r.ReadByte();
                }
            }

            return new HSD_String() { Value = str };
        }


        /// <summary>
        /// 
        /// </summary>
        public void ExtractDataFromMap(HSDAccessor acc)
        {
            foreach (var p in acc.GetType().GetProperties())
            {
                if (dolMap.ContainsKey(p.Name))
                {
                    var i = Activator.CreateInstance(p.PropertyType);
                    ((HSDAccessor)i)._s = GetStruct(dolMap[p.Name]);
                    p.SetValue(acc, i);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            r.Close();
            r.Dispose();
        }
    }
}
