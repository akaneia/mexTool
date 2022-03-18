using HSDRaw;
using MeleeMedia.Audio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;

namespace mexTool.Core
{
    public enum SoundBankGroupType
    {
        Null,
        Constant,
        NarratorName,
        Menu,
        Fighter,
        Stage,
        Minigame
    }

    public class MEXSoundBank
    {
        public SoundBankGroupType Group 
        {
            get => (SoundBankGroupType)((GroupFlags >> 24) & 0xFF);
            set => GroupFlags = (GroupFlags & ~0xFF000000) | (((uint)value & 0xFF) << 24);
        }

        public byte GroupFlag1
        {
            get => (byte)((GroupFlags >> 16) & 0xFF);
            set => GroupFlags = (uint)((GroupFlags & ~0x00FF0000) | (((uint)value & 0xFF) << 16));
        }

        public byte GroupFlag2
        {
            get => (byte)((GroupFlags >> 8) & 0xFF);
            set => GroupFlags = (uint)((GroupFlags & ~0x0000FF00) | (((uint)value & 0xFF) << 8));
        }

        public byte GroupFlag3
        {
            get => (byte)(GroupFlags & 0xFF);
            set => GroupFlags = (uint)((GroupFlags & ~0x000000FF) | ((uint)value & 0xFF));
        }

        /// <summary>
        /// 
        /// </summary>
        public uint Flags { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public uint GroupFlags { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public SEMBank ScriptBank { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public SSM SoundBank { get; internal set; }


        [Browsable(false), YamlIgnore]
        public bool IsMEXSound
        {
            get
            {
                return MEX.SoundBanks.IndexOf(this) > 55;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptBank"></param>
        /// <param name="soundBank"></param>
        public MEXSoundBank(Stream stream)
        {
            LoadFromStream(stream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptBank"></param>
        /// <param name="soundBank"></param>
        public MEXSoundBank(string filePath)
        {
            LoadFromPackage(filePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptBank"></param>
        /// <param name="soundBank"></param>
        public MEXSoundBank(SEMBank scriptBank, SSM soundBank)
        {
            ScriptBank = scriptBank;
            SoundBank = soundBank;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return SoundBank.Name;
        }

        /// <summary>
        /// Removes unused sounds from sound bank
        /// </summary>
        public void RemoveUnusedSoundsFromBank()
        {
            if (ScriptBank == null || SoundBank == null)
                return;

            var usedSounds = ScriptBank.Scripts.Select(e => e.SFXID);

            List<DSP> newList = new List<DSP>();

            for (int i = 0; i < SoundBank.Sounds.Length; i++)
            {
                if (usedSounds.Contains(i))
                    newList.Add(SoundBank.Sounds[i]);
            }

            SoundBank.Sounds = newList.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadFromPackage(string fileName)
        {
            using (FileStream s = new FileStream(fileName, FileMode.Open))
                LoadFromStream(s);
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadFromStream(Stream s)
        {
            using (BinaryReaderExt r = new BinaryReaderExt(s))
            {
                if (s.Length < 0x14)
                    return;

                if (new string(r.ReadChars(3)) != "SPK")
                    return;

                int version = 1;
                if (r.ReadChar() == '2')
                    version = 2;

                GroupFlags = r.ReadUInt32();
                Flags = r.ReadUInt32();

                var ssmSize = r.ReadInt32();
                ScriptBank = new SEMBank();
                ScriptBank.Scripts = new SEMBankScript[r.ReadInt32()];

                for (int i = 0; i < ScriptBank.Scripts.Length; i++)
                {
                    ScriptBank.Scripts[i] = new SEMBankScript();
                    ScriptBank.Scripts[i].Decompile(r.GetSection(r.ReadUInt32(), r.ReadInt32()));

                    if (version > 1)
                    {
                        var temp = r.Position + 4;
                        r.Position = r.ReadUInt32();
                        ScriptBank.Scripts[i].Name = r.ReadString(r.ReadChar());
                        r.Position = temp;
                    }
                }

                var name = r.ReadString(r.ReadChar());

                if (ssmSize == 0)
                {
                    SoundBank = null;
                }
                else
                {
                    SoundBank = new SSM();
                    using (MemoryStream ssmStream = new MemoryStream(r.ReadBytes(ssmSize)))
                        SoundBank.Open(name, ssmStream);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveAsPackage(string fileName)
        {
            using (FileStream s = new FileStream(fileName, FileMode.Create))
                WriteToStream(s);
        }

        /// <summary>
        /// 
        /// </summary>
        public void WriteToStream(Stream s)
        {
            using (BinaryWriter w = new BinaryWriter(s))
            {
                w.Write(new char[] { 'S', 'P', 'K', '2' });

                int version = 2;

                w.Write(GroupFlags);
                w.Write(Flags);
                w.Write(0);
                w.Write(ScriptBank.Scripts.Length);

                w.Write(new byte[ScriptBank.Scripts.Length * 0xC]);

                w.Write(SoundBank != null ? SoundBank.Name : "");

                if (SoundBank != null)
                    using (MemoryStream ssmFile = new MemoryStream())
                    {
                        SoundBank.WriteToStream(ssmFile, out int bs);
                        var ssm = ssmFile.ToArray();
                        w.Write(ssm);
                        var temp = s.Position;
                        s.Position = 0x0C;
                        w.Write(ssm.Length);
                        s.Position = temp;
                    }

                for (int i = 0; i < ScriptBank.Scripts.Length; i++)
                {
                    var commandData = ScriptBank.Scripts[i].Compile();

                    var temp = s.Position;
                    s.Position = 0x14 + 0x0C * i;

                    // write header info
                    w.Write((int)temp);
                    w.Write(commandData.Length);
                    if (version > 1)
                        w.Write((uint)(temp + commandData.Length));

                    // write data to stream
                    s.Position = temp;
                    w.Write(commandData);
                    if (version > 1)
                        w.Write(ScriptBank.Scripts[i].Name);
                }
            }
        }
    }
}
