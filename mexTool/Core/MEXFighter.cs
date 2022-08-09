using HSDRaw.Melee;
using HSDRaw.MEX;
using System.ComponentModel;
using mexTool.GUI;
using System.Drawing;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.IO;
using System.IO.Compression;
using mexTool.Tools;
using System.Windows.Forms;
using System.Linq;
using MeleeMedia.Audio;
using System;
using HSDRaw.GX;
using System.Collections.Generic;

namespace mexTool.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class MEXFighter : IDrawableListItem
    {
        [Category("0 - General"), DisplayName("Name"), Description("Name used for CSS screen")]
        public string NameText { get; set; }

        [Category("0 - General"), DisplayName("Fighter Data File"), Description("File containing fighter's data")]
        public string FighterDataPath { get; set; }

        [Category("0 - General"), DisplayName("Fighter Data Symbol"), Description("Symbol used inside of Fighter Data file")]
        public string FighterDataSymbol { get; set; }

        [Category("0 - General"), DisplayName("Emblem ID"), Description("Index of emblem associated with the fighter")]
        public byte InsigniaID { get; set; }



        [Category("1 - Animation"), DisplayName("Animation File"), Description("File containing the fighter animations")]
        public string AnimFile { get; set; }

        [Category("1 - Animation"), DisplayName("Animation Count"), Description("Number of animations the fighter has")]
        public int AnimCount { get; set; }

        [Category("1 - Animation"), DisplayName("Result Animation File"), Description("File Containing the Result Fighter Animations")]
        public string RstAnimFile { get; set; }

        [Category("1 - Animation"), DisplayName("Result Animation Count"), Description("Number of Result Animations")]
        public int RstAnimCount { get; set; }




        [Category("2 - Demo"), DisplayName("VI Wait File"), Description("")]
        public string DemoFile { get; set; }

        [Category("2 - Demo"), DisplayName("Vi Wait"), Description("")]
        public string DemoWait { get; set; }

        [Category("2 - Demo"), DisplayName("Result"), Description("")]
        public string DemoResult { get; set; }

        [Category("2 - Demo"), DisplayName("Intro"), Description("")]
        public string DemoIntro { get; set; }

        [Category("2 - Demo"), DisplayName("Ending"), Description("")]
        public string DemoEnding { get; set; }

        [Category("2 - Demo"), DisplayName("Classic Ending Image File"),  Description("")]
        public string EndClassicFile { get; set; }

        [Category("2 - Demo"), DisplayName("Adventure Ending Image File"), Description("")]
        public string EndAdventureFile { get; set; }

        [Category("2 - Demo"), DisplayName("All Star Ending Image File"), Description("")]
        public string EndAllStarFile { get; set; }

        [Category("2 - Demo"), DisplayName("Ending Movie File"), Description("")]
        public string EndMovieFile { get; set; }


        [Category("2 - Demo"), DisplayName(""), Description("")]
        public short ClassicTrophyId { get; set; }

        [Category("2 - Demo"), DisplayName(""), Description("")]
        public short AdventureTrophyId { get; set; }

        [Category("2 - Demo"), DisplayName(""), Description("")]
        public short AllStarTrophyId { get; set; }



        [Category("3 - Sounds"), DisplayName("SoundBank"), Description("Soundbank to load for fighter")]
        [TypeConverter(typeof(SoundFileConverter))]
        [YamlIgnore]
        public MEXSoundBank SoundBank { get; set; }

        [Category("3 - Sounds"), DisplayName("Narrator Sound Clip"), Description("Sound effect index of narrator sound clip")]
        [YamlIgnore]
        public int AnnouncerCall { get; set; }

        [Category("3 - Sounds"), DisplayName("Victory Theme"), Description("Music to play on victory screen")]
        [TypeConverter(typeof(MusicFileConverter))]
        public MEXMusic VictoryTheme { get; set; }

        [Category("3 - Sounds"), DisplayName("Fighter Music 1"), Description("Possible music to play for fighter credits")]
        [TypeConverter(typeof(MusicFileConverter))]
        public MEXMusic FighterSongID1 { get; set; }

        [Category("3 - Sounds"), DisplayName("Fighter Music 2"), Description("Possible music to play for fighter credits")]
        [TypeConverter(typeof(MusicFileConverter))]
        public MEXMusic FighterSongID2 { get; set; }



        [Category("4 - Effects"), DisplayName("Effect FileName"), Description("Effect file to load for fighter")]
        public string EffectFile { get; set; }

        [Category("4 - Effects"), DisplayName("Effect Symbol"), Description("Symbol in effect file to load")]
        public string EffectSymbol { get; set; }


        [Category("4 - Effects"), DisplayName("Kirby Effect File"), Description("Effect file to load for Kirby")]
        public string KirbyEffectFile { get; set; }

        [Category("4 - Effects"), DisplayName("Kirby Effect Symbol"), Description("Symbol in Kirby effect file to load")]
        public string KirbyEffectSymbol { get; set; }



        [Category("5 - Kirby"), DisplayName("Kirby Cap FileName"), Description("Kirby cap file associated with this fighter")]
        public string KirbyCapFileName { get; set; }

        [Category("5 - Kirby"), DisplayName("Kirby Cap Symbol"), Description("Symbol name in cap file")]
        public string KirbyCapSymbol { get; set; }



        [Category("6 - Misc"), DisplayName("Target Test Stage"), Description("The stage id of the target test stage for this fighter")]
        [TypeConverter(typeof(StageConverter))]
        [YamlIgnore]
        public MEXStage TargetTestStage { get; set; }

        [Category("6 - Misc"), DisplayName("Race to the Finish Time"), Description("Seconds the fighter has to complete \"Race to the Finish\"")]
        public int RacetoTheFinishTime { get; set; }

        [Category("6 - Misc"), DisplayName("Result Screen Scale"), Description("Amount to scale model on result screen")]
        public float ResultScreenScale { get; set; }

        [Category("6 - Misc"), DisplayName("Can Wall Jump"), Description("Determines if fighter can wall jump")]
        public bool CanWallJump { get; set; }

        [Category("6 - Misc"), DisplayName("Sub-Fighter"), Description("The fighter associated with this fighter (Sheik/Zelda and Ice Climbers)")]
        [TypeConverter(typeof(FighterConverter))]
        [YamlIgnore]
        public MEXFighter SubCharacter { get; set; }

        [Category("6 - Misc"), DisplayName("Sub-Fighter Behavior"), Description("The association between this fighter and the sub-fighter")]
        public SubCharacterBehavior SubCharacterBehavior { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MEXFighterFunctions Functions = new MEXFighterFunctions();

        /// <summary>
        /// 
        /// </summary>
        public SBM_PlCoUnknownFighterTable UnkTable;

        /// <summary>
        /// 
        /// </summary>
        public SBM_BoneLookupTable BoneTable;

        /// <summary>
        /// 
        /// </summary>
        public BindingList<MEX_Item> Items = new BindingList<MEX_Item>();

        /// <summary>
        /// 
        /// </summary>
        public BindingList<MEXCostume> Costumes = new BindingList<MEXCostume>();

        public BindingList<MEXCostume> KirbyCostumes = new BindingList<MEXCostume>();

        [Category("6 - Misc"), DisplayName(""), Description("")]
        public byte RedCostumeIndex { get; set; }

        [Category("6 - Misc"), DisplayName(""), Description("")]
        public byte BlueCostumeIndex { get; set; }

        [Category("6 - Misc"), DisplayName(""), Description("")]
        public byte GreenCostumeIndex { get; set; }

        // SSM info
        // no need to expose these to the user
        public uint SSMBitfield1;
        public uint SSMBitfield2;


        [Browsable(false), YamlIgnore]
        public bool IsMEXFighter
        {
            get
            {
                var index = MEX.Fighters.IndexOf(this);
                return (index >= 0x21 - 6 && index < Core.MEX.Fighters.Count - 6);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveFighterToPackage(string filePath)
        {
            using (FileStream zipToOpen = new FileStream(filePath, FileMode.OpenOrCreate))
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
            {
                // set export version
                Functions.Version = 1;

                // serialize fighter data
                var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .WithTypeInspector(inspector => new MEXTypeInspector(inspector))
                .Build();
                ZipArchiveEntry fighter = archive.CreateEntry("fighter.yml");
                using (StreamWriter writer = new StreamWriter(fighter.Open()))
                    writer.Write(serializer.Serialize(this));


                // effect file (as dat files)
                archive.AddFile(EffectFile, MEX.ImageResource.GetFileData(EffectFile));
                if (!string.IsNullOrEmpty(KirbyEffectFile))
                    archive.AddFile(KirbyEffectFile, MEX.ImageResource.GetFileData(KirbyEffectFile));


                // pl + aj + result anim
                archive.AddFile(FighterDataPath, MEX.ImageResource.GetFileData(FighterDataPath));
                archive.AddFile(AnimFile, MEX.ImageResource.GetFileData(AnimFile));
                archive.AddFile(RstAnimFile, MEX.ImageResource.GetFileData(RstAnimFile));

                // ViWaitAJ
                archive.AddFile(DemoFile, MEX.ImageResource.GetFileData(DemoFile));

                // costumes
                foreach (var c in Costumes)
                {
                    var cpack = c.ToPackage();
                    File.WriteAllBytes(Path.GetFileNameWithoutExtension(c.FileName) + ".zip", cpack);
                    using (var o = archive.CreateEntry($"{Path.GetFileNameWithoutExtension(c.FileName)}.zip").Open())
                        o.Write(cpack, 0, cpack.Length);
                }

                // kirby hat
                archive.AddFile(KirbyCapFileName, MEX.ImageResource.GetFileData(KirbyCapFileName));

                // movie
                archive.AddFile(EndMovieFile, MEX.ImageResource.GetFileData(EndMovieFile));

                // images
                archive.AddFile(EndAdventureFile, MEX.ImageResource.GetFileData(EndAdventureFile));
                archive.AddFile(EndAllStarFile, MEX.ImageResource.GetFileData(EndAllStarFile));
                archive.AddFile(EndClassicFile, MEX.ImageResource.GetFileData(EndClassicFile));

                // TODO: demo symbols (these are embedded in certain files)


                // announcer call (have to extract this dsp)
                {
                    var announcerBank = AnnouncerCall / 10000;
                    var scriptIndex = AnnouncerCall % 10000;
                    var announcerSoundId = MEX.SoundBanks[announcerBank].ScriptBank.Scripts[scriptIndex].SFXID;
                    var announcerDSP = MEX.SoundBanks[announcerBank].SoundBank.Sounds[announcerSoundId];
                    var announcerWAV = announcerDSP.ToWAVE();

                    using (var o = archive.CreateEntry("namecall.wav").Open())
                        o.Write(announcerWAV, 0, announcerWAV.Length);
                }


                // sound
                if (SoundBank != null && SoundBank.SoundBank.Name != "null.ssm")
                {
                    using (MemoryStream soundStream = new MemoryStream())
                    {
                        SoundBank.WriteToStream(soundStream);
                        archive.AddFile("sound.spk", soundStream.ToArray());
                    }
                }


                // music (as hps)
                if (VictoryTheme != null && VictoryTheme.IsMexMusic)
                    archive.AddFile(VictoryTheme.FileName, MEX.ImageResource.GetFileData("audio\\" + VictoryTheme.FileName));

                if (FighterSongID1 != null && FighterSongID1.IsMexMusic)
                    archive.AddFile(FighterSongID1.FileName, MEX.ImageResource.GetFileData("audio\\" + FighterSongID1.FileName));

                if (FighterSongID2 != null && FighterSongID2.IsMexMusic && FighterSongID1 != FighterSongID2)
                    archive.AddFile(FighterSongID2.FileName, MEX.ImageResource.GetFileData("audio\\" + FighterSongID2.FileName));


                // target stage (as stage package)
                if (TargetTestStage != null && TargetTestStage.FileName != null)
                    using (var o = archive.CreateEntry(TargetTestStage.FileName).Open())
                        TargetTestStage.SaveStagePackageToStream(o);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public static void InstallFighterFromFile(string filePath)
        {
            using (FileStream zipToOpen = new FileStream(filePath, FileMode.OpenOrCreate))
                InstallFighterFromStream(zipToOpen);
        }


        private static Dictionary<string, string> YamlCompatbilityV0 = new Dictionary<string, string>()
        {
            { "onRespawn:", "resetAttribute:"  },
            { "onDeath:", "onRespawn:"  },
            { "onUnk:", "onDestroy:"    },
            { "smashUp:", "onSmashHi:"    },
            { "smashDown:", "onSmashLw:"    },
            { "smashSide:", "onSmashF:"    },
            { "onItemDrop:", "onItemRelease:"    },
            { "onUnknownCharacterFlags1:", "onApplyHeadItem:"    },
            { "onUnknownCharacterFlags2:", "onRemoveHeadItem:"    },
            { "onHit:", "eyeTextureDamaged:"    },
            { "onUnknownEyeTextureRelated:", "eyeTextureNormal:"    },
            { "onActionStateChangeWhileEyeTextureIsChanged:", "onActionStateChangeWhileEyeTextureIsChanged1:"    },
            { "onLand:", "onLanding:"    },
        };

        private static string UpdateYamlCompatibility(string yml)
        {
            if (yml.Contains("version: 0") || !yml.Contains("version:"))
            {
                foreach (var v in YamlCompatbilityV0)
                {
                    yml = yml.Replace(v.Key, v.Value);
                }
            }
            return yml;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public static void InstallFighterFromStream(Stream stream)
        {
            // check filename conflicts
            using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                var stageEntry = archive.GetEntry("fighter.yml");

                var serializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .WithTypeInspector(inspector => new MEXTypeInspector(inspector))
                .IgnoreUnmatchedProperties()
                .Build();

                MEXFighter fighter = null;

                try
                {
                    using (StreamReader r = new StreamReader(stageEntry.Open()))
                        fighter = serializer.Deserialize<MEXFighter>(UpdateYamlCompatibility(r.ReadToEnd()));
                } catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

                if (fighter == null)
                {
                    MessageBox.Show("Fighter failed to install.\nIt may have been exported with an earlier build of mexTool.", "Fighter Install Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // effect file (as dat files)
                InstallFile(archive, fighter.EffectFile);
                InstallFile(archive, fighter.KirbyEffectFile);


                // pl + aj + result anim
                InstallFile(archive, fighter.FighterDataPath);
                InstallFile(archive, fighter.AnimFile);
                InstallFile(archive, fighter.RstAnimFile);


                // ViWaitAJ
                InstallFile(archive, fighter.DemoFile);

                // costumes
                foreach(var c in fighter.Costumes)
                {
                    var costumeFile = archive.GetEntry(Path.GetFileNameWithoutExtension(c.FileName) + ".zip");

                    if(costumeFile != null)
                    {
                        using (var costumeStream = costumeFile.Open())
                        using (var cs = new MemoryStream())
                        {
                            costumeStream.CopyTo(cs);
                            using (ZipArchive costumeArchive = new ZipArchive(cs, ZipArchiveMode.Read))
                            {
                                if (costumeArchive.GetEntry("csp.png") != null)
                                    using (var en = costumeArchive.GetEntry("csp.png").Open())
                                    using (var bmp = new Bitmap(en))
                                        c.CSP = bmp.ToTOBJ(GXTexFmt.CI8, GXTlutFmt.RGB5A3);

                                if (costumeArchive.GetEntry("stc.png") != null)
                                    using (var en = costumeArchive.GetEntry("stc.png").Open())
                                    using (var bmp = new Bitmap(en))
                                        c.Icon = bmp.ToTOBJ(GXTexFmt.CI4, GXTlutFmt.RGB5A3);

                                if (costumeArchive.GetEntry(c.FileName) != null)
                                    InstallFile(costumeArchive, c.FileName);
                            }
                        }
                    }
                }

                // kirby hat
                InstallFile(archive, fighter.KirbyCapFileName);

                // movie
                InstallFile(archive, fighter.EndMovieFile);

                // images
                InstallFile(archive, fighter.EndAdventureFile);
                InstallFile(archive, fighter.EndAllStarFile);
                InstallFile(archive, fighter.EndClassicFile);


                // TODO: demo symbols (these are embedded in certain files)


                // announcer call (have to extract this dsp)
                var entry = archive.GetFile("namecall.wav");

                if (entry != null)
                {
                    var dsp = new DSP();
                    dsp.FromFormat(entry, "wav");
                    var announcerIndex = MEX.SoundBanks[51].SoundBank.Sounds.Length;
                    var script_index = MEX.SoundBanks[51].ScriptBank.Scripts.Length;
                    MEX.SoundBanks[51].SoundBank.AddSound(dsp);
                    var scripts = MEX.SoundBanks[51].ScriptBank.Scripts;
                    Array.Resize(ref scripts, script_index + 1);
                    var script = new SEMBankScript();
                    script.Codes.Add(new SEMCode(SEM_CODE.SET_SFXID) { Value = announcerIndex });
                    script.Codes.Add(new SEMCode(SEM_CODE.SET_REVERB1) { Value = 48 });
                    script.Codes.Add(new SEMCode(SEM_CODE.SET_PRIORITY) { Value = 15 });
                    script.Codes.Add(new SEMCode(SEM_CODE.PLAY) { Value = 229 });
                    script.Codes.Add(new SEMCode(SEM_CODE.END_PLAYBACK));
                    scripts[script_index] = script;
                    MEX.SoundBanks[51].ScriptBank.Scripts = scripts;
                    fighter.AnnouncerCall = 510000 + script_index;
                }


                // install sound bank
                fighter.SoundBank = MEX.SoundBanks[55];
                var soundFile = archive.GetFile("sound.spk");
                if (soundFile != null)
                    using (MemoryStream soundStream = new MemoryStream(soundFile))
                    {
                        var sf = new MEXSoundBank(soundStream);
                        var soundBank = Core.MEX.SoundBanks.FirstOrDefault(e => e.SoundBank.Name == sf.SoundBank.Name);

                        // don't add vanilla soundbanks
                        if (soundBank != null)
                        {
                            MessageBox.Show($"Warning: Soundbank with name \"{soundBank.SoundBank.Name}\" already exists.\nUsing existing soundbank instead.", "Soundbank Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            fighter.SoundBank = soundBank;
                        }
                        else
                        {
                            fighter.SoundBank = sf;
                            MEX.SoundBanks.Add(sf);
                        }
                    }



                // link and add missing music
                fighter.VictoryTheme = InstallFighterMusic(fighter, archive, fighter.VictoryTheme);
                fighter.FighterSongID1 = InstallFighterMusic(fighter, archive, fighter.FighterSongID1);
                fighter.FighterSongID2 = InstallFighterMusic(fighter, archive, fighter.FighterSongID2);


                // TODO: install target test stage


                // add stage file
                MEX.Fighters.Insert(MEX.FighterCount - 6, fighter);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static bool InstallFile(ZipArchive archive, string file)
        {
            var entry = archive.GetFile(file);

            if(entry != null)
            {
                MEX.ImageResource.AddFile(file, entry);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fighter"></param>
        /// <param name="archive"></param>
        /// <param name="bgm"></param>
        private static MEXMusic InstallFighterMusic(MEXFighter fighter, ZipArchive archive, MEXMusic bgm)
        {
            var music = MEX.BackgroundMusic.FirstOrDefault(e => e.FileName == bgm.FileName);

            if (music != null)
                return music;
            else
            {
                // find music in zip
                var musicFile = archive.GetFile(Path.GetFileName(bgm.FileName));

                if (musicFile != null)
                {
                    // add music
                    MEX.ImageResource.AddFile("audio\\" + Path.GetFileName(bgm.FileName), musicFile);
                    MEX.BackgroundMusic.Add(bgm);
                }

                return bgm;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return NameText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Image ToImage()
        {
            if (Costumes.Count == 0)
                return null;

            return Costumes[0].ToImage();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MEXFighterFunctions
    {
        [Browsable(false)]
        public int Version { get; set; } = 0;

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnLoad { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnRespawn { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnDestroy { get; set; }

        [Category("Fighter")]
        public MEX_MoveLogic[] MoveLogic { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint MoveLogicPointer { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint DemoMoveLogicPointer { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint SpecialN { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint SpecialNAir { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint SpecialHi { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint SpecialHiAir { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint SpecialLw { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint SpecialLwAir { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint SpecialS { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint SpecialSAir { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnSmashHi { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnSmashLw { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnSmashF { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnAbsorb { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnItemPickup { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnMakeItemInvisible { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnMakeItemVisible { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnItemRelease { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnItemCatch { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnUnknownItemRelated { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnApplyHeadItem { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnRemoveHeadItem { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint EyeTextureDamaged { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint EyeTextureNormal { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnFrame { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnActionStateChange { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint ResetAttribute { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnModelRender { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnShadowRender { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnUnknownMultijump { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnActionStateChangeWhileEyeTextureIsChanged1 { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnActionStateChangeWhileEyeTextureIsChanged2 { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnTwoEntryTable1 { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnTwoEntryTable2 { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnLanding { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnExtRstAnim { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint OnIndexExtRstAnim { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint EnterFloat { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint EnterDoubleJump { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter")]
        public uint EnterTether { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter"), Description("")]
        public uint OnIntroL { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter"), Description("")]
        public uint OnIntroR { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter"), Description("")]
        public uint OnCatch { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter"), Description("")]
        public uint OnAppeal { get; set; }

        [TypeConverter(typeof(HexType)), Category("Fighter"), Description("")]
        public uint GetSwordTrail { get; set; }



        [TypeConverter(typeof(HexType)), DisplayName("N Special"), Category("Kirby"), Description("")]
        public uint KirbySpecialN { get; set; }

        [TypeConverter(typeof(HexType)), DisplayName("N Air Special"), Category("Kirby"), Description("")]
        public uint KirbySpecialNAir { get; set; }

        [TypeConverter(typeof(HexType)), DisplayName("OnSwallow"), Category("Kirby"), Description("")]
        public uint KirbyOnSwallow { get; set; }

        [TypeConverter(typeof(HexType)), DisplayName("OnLoseAbility"), Category("Kirby"), Description("")]
        public uint KirbyOnLoseAbility { get; set; }

        [TypeConverter(typeof(HexType)), DisplayName("OnHit"), Category("Kirby"), Description("")]
        public uint KirbyOnHit { get; set; }

        [TypeConverter(typeof(HexType)), DisplayName("OnDeath"), Category("Kirby"), Description("")]
        public uint KirbyOnDeath { get; set; }

        [TypeConverter(typeof(HexType)), DisplayName("OnItemInit"), Category("Kirby"), Description("")]
        public uint KirbyOnItemInit { get; set; }

        [TypeConverter(typeof(HexType)), DisplayName("OnFrame"), Category("Kirby"), Description("")]
        public uint KirbyOnFrame { get; set; }
    }
}
