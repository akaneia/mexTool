using HSDRaw;
using HSDRaw.Common;
using HSDRaw.Common.Animation;
using HSDRaw.Melee;
using HSDRaw.MEX;
using HSDRaw.MEX.Menus;
using HSDRaw.MEX.Misc;
using HSDRaw.MEX.Scenes;
using HSDRaw.MEX.Sounds;
using HSDRaw.MEX.Stages;
using HSDRaw.Tools;
using MeleeMedia.Audio;
using MeleeMedia.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace mexTool.Core
{
    public class BindingListExt<T> : BindingList<T>
    {
        protected override void RemoveItem(int itemIndex)
        {
            T deletedItem = this.Items[itemIndex];

            if (BeforeRemove != null)
                BeforeRemove(deletedItem);

            //remove item from list
            base.RemoveItem(itemIndex);
        }

        public delegate void myIntDelegate(T deletedItem);
        public event myIntDelegate BeforeRemove;
    }

    public class MEX
    {
        private static byte VersionMajor = 1;

        private static byte VersionMinor = 0;

        /// <summary>
        /// 
        /// </summary>
        public static bool Initialized
        {
            get; internal set;
        }

        public static ImageResource ImageResource { get => _imageResource; }
        private static ImageResource _imageResource;

        // static files
        public static HSDRawFile PlCoFile { get; internal set; }

        public static SBM_ftLoadCommonData FighterCommonData
        {
            get
            {
                return PlCoFile.Roots[0].Data as SBM_ftLoadCommonData;
            }
        }

        public static HSDRawFile IfAllFile { get; internal set; }

        public static MEX_Stock StockManager
        {
            get
            {
                return IfAllFile["Stc_icns"]?.Data as MEX_Stock;
            }
        }

        public static HSDRawFile CSSFile { get; internal set; }

        public static HSDRawFile SSSFile { get; internal set; }

        public static HSDRawFile SmSt { get; internal set; }

        // scene related
        public static int StartingScene;
        public static int LastMajorSceneID;
        public static int LastMinorSceneID;

        public static int FighterCount { get => Fighters.Count; }

        /// <summary>
        /// Fighters in order of internal id
        /// </summary>
        public static BindingListExt<MEXFighter> Fighters = new BindingListExt<MEXFighter>();

        /// <summary>
        /// 
        /// </summary>
        public static BindingListExt<MEXStage> Stages = new BindingListExt<MEXStage>();

        private static List<MEX_StageIDTable> StageIDs = new List<MEX_StageIDTable>();

        // Items
        public static MEX_Item[] CommonItems;
        public static MEX_Item[] FighterItems;
        public static MEX_Item[] PokemonItems;
        public static MEX_Item[] StageItems;

        /// <summary>
        /// 
        /// </summary>
        private static int MexItemOffset
        {
            get
            {
                return CommonItems.Length + FighterItems.Length + PokemonItems.Length + StageItems.Length;
            }
        }

        /// <summary>
        /// Background music
        /// </summary>
        public static BindingListExt<MEXMusic> BackgroundMusic = new BindingListExt<MEXMusic>();


        /// <summary>
        /// 
        /// </summary>
        public static BindingListExt<MEXSoundBank> SoundBanks = new BindingListExt<MEXSoundBank>();

        /// <summary>
        /// 
        /// </summary>
        public static List<MEX_EffectFiles> EffectFiles = new List<MEX_EffectFiles>();

        /// <summary>
        /// 
        /// </summary>
        public static MEXPlaylist MainMenuPlaylist;

        public static MEX_MenuParameters MenuParameters;

        public static MEX_SceneData SceneData;

        /// <summary>
        /// 
        /// </summary>
        public static BindingList<HSD_TOBJ> ReservedIcons = new BindingList<HSD_TOBJ>();

        /// <summary>
        /// 
        /// </summary>
        public static BindingList<MEXStageIcon> StageIcons = new BindingList<MEXStageIcon>();

        /// <summary>
        /// 
        /// </summary>
        public static BindingList<MEXFighterIcon> FighterIcons = new BindingList<MEXFighterIcon>();



        // Misc

        private static HSD_TOBJ StageIcon1;
        private static HSD_TOBJ StageIcon2;

        public static BindingList<HSD_TOBJ> Emblems = new BindingList<HSD_TOBJ>();

        public static List<MEX_GawColor> GaWColors = new List<MEX_GawColor>();

        /// <summary>
        /// 
        /// </summary>
        public static void Close()
        {
            Initialized = false;

            EffectFiles.Clear();
            ReservedIcons.Clear();
            Fighters.Clear();
            SoundBanks.Clear();
            BackgroundMusic.Clear();
            Stages.Clear();
            StageIDs.Clear();
            StageIcons.Clear();
            FighterIcons.Clear();
            Emblems.Clear();
            GaWColors.Clear();

            CommonItems = null;
            StageItems = null;
            FighterItems = null;
            PokemonItems = null;

            MainMenuPlaylist = null;
            MenuParameters = null;
            SceneData = null;

            PlCoFile = null;
            IfAllFile = null;
            CSSFile = null;
            SSSFile = null;
            SmSt = null;

            StageIcon1 = null;
            StageIcon2 = null;

            if (_imageResource != null)
            {
                _imageResource.DeleteTempFiles();
                _imageResource.Close();
                _imageResource = null;
            }

            // just manually call this to start freeing resources
            GC.Collect();
        }

        /// <summary>
        /// 
        /// </summary>
        private static void Init()
        {
            // loads data from mxdt file
            if (_imageResource == null || Initialized)
                return;


            // load other files and resources
            PlCoFile = new HSDRawFile(_imageResource.GetFileData("PlCo.dat"));
            IfAllFile = new HSDRawFile(_imageResource.GetFileData("IfAll.usd"));
            CSSFile = new HSDRawFile(_imageResource.GetFileData("MnSlChr.usd"));
            SSSFile = new HSDRawFile(_imageResource.GetFileData("MnSlMap.usd"));
            SmSt = new HSDRawFile(_imageResource.GetFileData("SmSt.dat"));


            // load ui
            StockManager.GetFighterIcons(out List<List<HSD_TOBJ>> fighterIcons, out List<HSD_TOBJ> reservedIcons);
            foreach (var ri in reservedIcons)
                ReservedIcons.Add(ri);

            Emblems.Clear();
            foreach (var tobj in (IfAllFile["Eblm_matanim_joint"].Data as HSD_MatAnimJoint).MaterialAnimation.TextureAnimation.ToTOBJs())
                Emblems.Add(tobj);


            // load mex data
            var _mexData = new HSDRawFile(_imageResource.GetFileData("MxDt.dat")).Roots[0].Data as MEX_Data;


            // meta data
            StartingScene = _mexData.MetaData.EnterScene;


            // scene data
            LastMajorSceneID = _mexData.MetaData.LastMajor;
            LastMinorSceneID = _mexData.MetaData.LastMinor;
            SceneData = _mexData.SceneData;


            // load effect data
            foreach (var eff in _mexData.EffectTable.EffectFiles.Array)
                EffectFiles.Add(eff);


            // load scene data
            MenuParameters = _mexData.MenuTable.Parameters;


            // load sound files
            var smst = SmSt.Roots[0].Data as smSoundTestLoadData;
            var soundNames = smst.SoundNames;
            var soundids = smst.SoundIDs;
            var semBanks = SEM.ReadSEMFile(ImageResource.GetFileData("audio/us/smash2.sem"));
            var ssmFiles = _mexData.SSMTable.SSM_SSMFiles.Array;

            for (int i = 0; i < ssmFiles.Length; i++)
            {
                var ssmFile = ImageResource.GetFileData("audio/us/" + ssmFiles[i].Value);

                if (ssmFile == null)
                    break;

                // load ssm file data
                var ssm = new SSM();
                using (MemoryStream stream = new MemoryStream(ssmFile))
                    ssm.Open(ssmFiles[i].Value, stream);

                var bank = new SEMBank();
                
                if(i < semBanks.Count)
                    bank = semBanks[i];

                // load script meta data
                for(int j = 0; j < bank.Scripts.Length; j++)
                {
                    // load script name
                    var sindex = soundids.IndexOf(i * 10000 + j);
                    if (sindex != -1 && sindex < soundNames.Length)
                        bank.Scripts[j].Name = soundNames[sindex];

                    // adjust sound id to relative
                    if (bank.Scripts[j].SFXID != -1)
                        bank.Scripts[j].SFXID -= ssm.StartIndex;
                }


                // create sem package
                var sp = new MEXSoundBank(bank, ssm);
                sp.GroupFlags = (uint)_mexData.SSMTable.SSM_LookupTable[i].EntireFlag;
                sp.Flags = (uint)_mexData.SSMTable.SSM_BufferSizes[i].Flag;

                // add sem package to bank
                SoundBanks.Add(sp);
            }


            // load bgm music data
            for (int i = 0; i < _mexData.MetaData.NumOfMusic; i++)
            {
                var bgm = new MEXMusic()
                {
                    FileName = _mexData.MusicTable.BGMFileNames[i].Value
                };

                if (i < _mexData.MusicTable.BGMLabels.Length)
                    bgm.Label = _mexData.MusicTable.BGMLabels[i].Value;

                BackgroundMusic.Add(bgm);
            }


            // load main menu playlist
            MainMenuPlaylist = new MEXPlaylist();
            MainMenuPlaylist.Entries.AddRange(_mexData.MusicTable.MenuPlaylist.Array.Select(e => new MEXPlaylistEntry() { Music = BackgroundMusic[e.HPSID], PlayChance = e.ChanceToPlay }));


            // load items
            CommonItems = _mexData.ItemTable.CommonItems.Array;
            FighterItems = _mexData.ItemTable.FighterItems.Array;
            PokemonItems = _mexData.ItemTable.Pokemon.Array;
            StageItems = _mexData.ItemTable.StageItems.Array;
            var mexItem = _mexData.ItemTable.MEXItems;



            // external stage ids
            StageIDs.AddRange(_mexData.StageData.StageIDTable.Array);

            // stage data
            for (int i = 0; i < _mexData.StageFunctions.Length; i++)
            {
                // load basic stage data
                var stage = new MEXStage()
                {
                    Stage = _mexData.StageFunctions[i],
                    Reverb = _mexData.StageData.ReverbTable[i],
                    Collision = _mexData.StageData.CollisionTable[i],
                    StageName = _mexData.StageData.StageNames[i].Value,
                };

                // load playlist
                stage.Playlist.FromPlaylistStruct(_mexData.StageData.StagePlaylists[i]);

                // load sound bank
                var reverbIndex = _mexData.StageData.ReverbTable[i].SSMID;
                if(reverbIndex < SoundBanks.Count)
                    stage.SoundBank = SoundBanks[reverbIndex];

                // load items
                foreach (var lookup in _mexData.StageData.StageItemLookup[i].Entries)
                    stage.Items.Add(mexItem[lookup - MexItemOffset]);

                // add stage
                Stages.Add(stage);
            }



            // load fighters
            var ftData = _mexData.FighterData;
            var ftFunc = _mexData.FighterFunctions;
            var kbData = _mexData.KirbyData;
            var kbFunc = _mexData.KirbyFunctions;

            for (int i = 0; i < _mexData.MetaData.NumOfInternalIDs; i++)
            {
                // get ids
                var internalId = i;
                var externalId = MEXFighterIDConverter.ToExternalID(i, _mexData.MetaData.NumOfExternalIDs);

                var hasDemo = internalId < ftData.FtDemo_SymbolNames.Length && ftData.FtDemo_SymbolNames[internalId] != null;

                // load fighter parameters
                var ft = new MEXFighter();

                ft.NameText = ftData.NameText[externalId].Value;
                ft.FighterDataPath = ftData.CharFiles[internalId].FileName;
                ft.FighterDataSymbol = ftData.CharFiles[internalId].Symbol;
                ft.AnimCount = ftData.AnimCount[internalId].AnimCount;
                ft.AnimFile = ftData.AnimFiles[internalId].Value;
                ft.RstAnimFile = ftData.ResultAnimFiles[externalId].Value;
                ft.RstAnimCount = ftData.RstRuntime[internalId].AnimMax;
                ft.InsigniaID = ftData.InsigniaIDs[externalId];
                ft.CanWallJump = ftData.WallJump[internalId] != 0;
                ft.EffectFile = ftData.EffectIDs[internalId] < EffectFiles.Count ? EffectFiles[ftData.EffectIDs[internalId]].FileName : null;
                ft.EffectSymbol = ftData.EffectIDs[internalId] < EffectFiles.Count ? EffectFiles[ftData.EffectIDs[internalId]].Symbol : null;
                ft.AnnouncerCall = ftData.AnnouncerCalls[externalId];
                ft.SoundBank = ftData.SSMFileIDs[externalId].SSMID < SoundBanks.Count ? SoundBanks[ftData.SSMFileIDs[externalId].SSMID] : null;
                ft.SSMBitfield1 = (uint)ftData.SSMFileIDs[externalId].BitField1;
                ft.SSMBitfield2 = (uint)ftData.SSMFileIDs[externalId].BitField2;
                ft.KirbyCapFileName = kbData.CapFiles[internalId].FileName;
                ft.KirbyCapSymbol = kbData.CapFiles[internalId].Symbol;
                ft.KirbyEffectFile = kbData.KirbyEffectIDs[internalId] < EffectFiles.Count ? EffectFiles[kbData.KirbyEffectIDs[internalId]].FileName : null;
                ft.KirbyEffectSymbol = kbData.KirbyEffectIDs[internalId] < EffectFiles.Count ? EffectFiles[kbData.KirbyEffectIDs[internalId]].Symbol : null;
                ft.VictoryTheme = externalId < ftData.VictoryThemeIDs.Length &&
                    ftData.VictoryThemeIDs[externalId] > 0 &&
                    ftData.VictoryThemeIDs[externalId] < BackgroundMusic.Count ? 
                    BackgroundMusic[ftData.VictoryThemeIDs[externalId]] : 
                    BackgroundMusic[0];
                ft.FighterSongID1 = ftData.FighterSongIDs[externalId].SongID1 >= 0 && ftData.FighterSongIDs[externalId].SongID1 < BackgroundMusic.Count ? BackgroundMusic[ftData.FighterSongIDs[externalId].SongID1] : null;
                ft.FighterSongID2 = ftData.FighterSongIDs[externalId].SongID2 >= 0 && ftData.FighterSongIDs[externalId].SongID2 < BackgroundMusic.Count ? BackgroundMusic[ftData.FighterSongIDs[externalId].SongID2] : null;
                
                // Note: not all fighters have these
                ft.DemoEnding = hasDemo ? ftData.FtDemo_SymbolNames[internalId].Ending : "";
                ft.DemoIntro = hasDemo ? ftData.FtDemo_SymbolNames[internalId].Intro : "";
                ft.DemoResult = hasDemo ? ftData.FtDemo_SymbolNames[internalId].Result : "";
                ft.DemoWait = hasDemo ? ftData.FtDemo_SymbolNames[internalId].ViWait : "";

                ft.DemoFile = externalId < ftData.VIFiles.Length && ftData.VIFiles[externalId] != null ? ftData.VIFiles[externalId].Value : "";
                ft.RedCostumeIndex = externalId < ftData.CostumeIDs.Length ? ftData.CostumeIDs[externalId].RedCostumeIndex : (byte)0;
                ft.BlueCostumeIndex = externalId < ftData.CostumeIDs.Length ? ftData.CostumeIDs[externalId].BlueCostumeIndex : (byte)0;
                ft.GreenCostumeIndex = externalId < ftData.CostumeIDs.Length ? ftData.CostumeIDs[externalId].GreenCostumeIndex : (byte)0;
                ft.ResultScreenScale = externalId < ftData.ResultScale.Length ? ftData.ResultScale[externalId] : 1;
                ft.TargetTestStage = ftData.TargetTestStageLookups[externalId] < StageIDs.Count ? Stages[StageIDs[ftData.TargetTestStageLookups[externalId]].StageID] : Stages[0];
                ft.RacetoTheFinishTime = ftData.RaceToFinishTimeLimits[externalId];
                ft.EndClassicFile = externalId < ftData.EndClassicFiles.Length ? ftData.EndClassicFiles[externalId].Value : "";
                ft.EndAdventureFile = externalId < ftData.EndAdventureFiles.Length ? ftData.EndAdventureFiles[externalId].Value : "";
                ft.EndAllStarFile = externalId < ftData.EndAllStarFiles.Length ? ftData.EndAllStarFiles[externalId].Value : "";
                ft.EndMovieFile = externalId < ftData.EndMovieFiles.Length ? ftData.EndMovieFiles[externalId].Value : "";
                

                ft.Functions = new MEXFighterFunctions()
                {
                    MoveLogic = ftFunc.MoveLogic[internalId] != null ? ftFunc.MoveLogic[internalId].Array : null,
                    OnLoad = ftFunc.OnLoad[internalId],
                    OnDeath = ftFunc.OnDeath[internalId],
                    OnUnk = ftFunc.OnUnknown[internalId],
                    DemoMoveLogicPointer = ftFunc.DemoMoveLogic[internalId],
                    SpecialN = ftFunc.SpecialN[internalId],
                    SpecialNAir = ftFunc.SpecialNAir[internalId],
                    SpecialHi = ftFunc.SpecialHi[internalId],
                    SpecialHiAir = ftFunc.SpecialHiAir[internalId],
                    SpecialLw = ftFunc.SpecialLw[internalId],
                    SpecialLwAir = ftFunc.SpecialLwAir[internalId],
                    SpecialS = ftFunc.SpecialS[internalId],
                    SpecialSAir = ftFunc.SpecialSAir[internalId],
                    OnAbsorb = ftFunc.OnAbsorb[internalId],
                    OnItemPickup = ftFunc.onItemPickup[internalId],
                    OnMakeItemInvisible = ftFunc.onMakeItemInvisible[internalId],
                    OnMakeItemVisible = ftFunc.onMakeItemVisible[internalId],
                    OnItemDrop = ftFunc.onItemDrop[internalId],
                    OnItemCatch = ftFunc.onItemCatch[internalId],
                    OnUnknownItemRelated = ftFunc.onUnknownItemRelated[internalId],
                    OnUnknownCharacterFlags1 = ftFunc.onApplyHeadItem[internalId],
                    OnUnknownCharacterFlags2 = ftFunc.onRemoveHeadItem[internalId],
                    OnHit = ftFunc.onHit[internalId],
                    OnUnknownEyeTextureRelated = ftFunc.onUnknownEyeTextureRelated[internalId],
                    OnFrame = ftFunc.onFrame[internalId],
                    OnActionStateChange = ftFunc.onActionStateChange[internalId],
                    OnRespawn = ftFunc.onRespawn[internalId],
                    OnModelRender = ftFunc.onModelRender[internalId],
                    OnShadowRender = ftFunc.onShadowRender[internalId],
                    OnUnknownMultijump = ftFunc.onUnknownMultijump[internalId],
                    OnActionStateChangeWhileEyeTextureIsChanged = ftFunc.onActionStateChangeWhileEyeTextureIsChanged[internalId],
                    OnTwoEntryTable1 = ftFunc.onTwoEntryTable[internalId * 2],
                    OnTwoEntryTable2 = ftFunc.onTwoEntryTable[internalId * 2 + 1],
                    OnLand = ftFunc.onLand[internalId],
                    OnExtRstAnim = ftFunc.onExtRstAnim[internalId],
                    OnIndexExtRstAnim = ftFunc.onIndexExtResultAnim[internalId],

                    SmashDown = ftFunc.onSmashDown[internalId],
                    SmashUp = ftFunc.onSmashUp[internalId],
                    SmashSide = ftFunc.onSmashForward[internalId],
                    EnterFloat = ftFunc.enterFloat[internalId],
                    EnterDoubleJump = ftFunc.enterSpecialDoubleJump[internalId],
                    EnterTether = ftFunc.enterTether[internalId],
                    onThrowBk = ftFunc.onThrowBk[internalId],
                    onThrowFw = ftFunc.onThrowFw[internalId],
                    onThrowHi = ftFunc.onThrowHi[internalId],
                    onThrowLw = ftFunc.onThrowLw[internalId],
                    getSwordTrail = ftFunc.getTrailData[internalId],

                    KirbyOnHit = kbFunc.KirbyOnHit[internalId],
                    KirbyOnItemInit = kbFunc.KirbyOnItemInit[internalId],
                    KirbyOnLoseAbility = kbFunc.OnAbilityLose[internalId],
                    KirbyOnSwallow = kbFunc.OnAbilityGain[internalId],
                    KirbySpecialN = kbFunc.KirbySpecialN[internalId],
                    KirbySpecialNAir = kbFunc.KirbySpecialNAir[internalId],
                };


                // load costumes and stock icons
                for(int k = 0; k < ftData.CostumeFileSymbols[internalId].CostumeSymbols.Length; k++)
                {
                    //
                    var costume = new MEXCostume();
                    
                    // load costume data
                    costume.Costume = ftData.CostumeFileSymbols[internalId].CostumeSymbols[k];

                    // load stock icon if it exists
                    if (i < fighterIcons.Count && k < fighterIcons[i].Count)
                        costume.Icon = fighterIcons[i][k];

                    //
                    ft.Costumes.Add(costume);
                }

                // load kirby costumes if they exist
                if (kbData.KirbyCostumes[internalId] != null)
                    for (int k = 0; k < kbData.KirbyCostumes[internalId].Length; k++)
                    {
                        ft.KirbyCostumes.Add(new MEXCostume() { Costume = kbData.KirbyCostumes[internalId][k] });
                    }


                // PlCo bone table
                ft.BoneTable = FighterCommonData.BoneTables[i];
                ft.UnkTable = FighterCommonData.FighterTable[i];


                // load items
                foreach (var lookup in ftData.FighterItemLookup[internalId].Entries)
                    ft.Items.Add(mexItem[lookup - MexItemOffset]);


                // add fighter
                Fighters.Add(ft);
            }


            // SubCharacterInternalID and Behavior
            for (int i = 0; i < _mexData.MetaData.NumOfInternalIDs; i++)
            {
                var externalId = MEXFighterIDConverter.ToExternalID(i, _mexData.MetaData.NumOfExternalIDs);
                var def = ftData.DefineIDs[externalId];
                if (def != null)
                {
                    if((sbyte)def.SubCharacterInternalID >= 0)
                        Fighters[i].SubCharacter = Fighters[def.SubCharacterInternalID];
                    Fighters[i].SubCharacterBehavior = def.SubCharacterBehavior;
                }
            }


            // load misc files
            LoadMexMenuFileData(_mexData);

            GaWColors.AddRange(_mexData.MiscData.GawColors.Array);

            #region remove events

            if(!Initialized)
            {
                BackgroundMusic.BeforeRemove += (removed) =>
                {
                    // remove fighter music entries
                    foreach (var f in MEX.Fighters)
                    {
                        if (f.VictoryTheme == removed)
                            f.VictoryTheme = null;

                        if (f.FighterSongID1 == removed)
                            f.FighterSongID1 = null;

                        if (f.FighterSongID2 == removed)
                            f.FighterSongID2 = null;
                    }

                    // remove music from stages
                    foreach (var s in MEX.Stages)
                        s.Playlist.RemoveMusic(removed);

                    // delete music from menu playlist
                    MEX.MainMenuPlaylist.RemoveMusic(removed);
                };

                Fighters.BeforeRemove += (removed) =>
                {
                    // remove from select icons
                    foreach (var v in FighterIcons)
                        if (v.Fighter == removed)
                            v.Fighter = Fighters[0];

                    // TODO: optional delete fighter files

                    // if soundbank is not in use, and is a mex sound bank, delete it

                    // delete stage files not in use

                    // delete music not in use
                };

                SoundBanks.BeforeRemove += (removed) =>
                {
                    // remove from fighter
                    foreach (var v in Fighters)
                        if (v.SoundBank == removed)
                            v.SoundBank = null;

                    // remove from stage
                    foreach (var v in Stages)
                        if (v.SoundBank == removed)
                            v.SoundBank = null;
                };

                Stages.BeforeRemove += (removed) =>
                {
                    // remove from select icons
                    foreach (var v in StageIcons)
                        if (v.Stage == removed)
                            v.Stage = MEX.Stages[0];

                    // remove from fighter
                    foreach (var v in Fighters)
                        if (v.TargetTestStage == removed)
                            v.TargetTestStage = null;

                    // TODO: optional delete stage files
                    // if soundbank is not in use, and is a mex sound bank, delete it
                };

                Emblems.ListChanged += (sender, args) =>
                {
                    if(args.ListChangedType == ListChangedType.ItemDeleted)
                    {
                        foreach (var f in Fighters)
                            if (f.InsigniaID == args.NewIndex)
                                f.InsigniaID = 0;
                    }    
                };
            }

            #endregion

            // Done
            Initialized = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void PrepareSave(ProgressChangedEventHandler progress)
        {
            progress(null, new ProgressChangedEventArgs(0, null));

            var mxdt = GenerateMxDt();

            progress(null, new ProgressChangedEventArgs(5, null));

            SaveSoundBanks(mxdt, "temp//");

            progress(null, new ProgressChangedEventArgs(8, null));

            // save files to temp directory
            PlCoFile.Save("temp//PlCo.dat", trim: true);
            ImageResource.AddFile("PlCo.dat", "temp//PlCo.dat");

            IfAllFile.Save("temp//IfAll.usd", trim: true);
            ImageResource.AddFile("IfAll.usd", "temp//IfAll.usd");

            CSSFile.Save("temp//MnSlChr.usd", trim: true);
            ImageResource.AddFile("MnSlChr.usd", "temp//MnSlChr.usd");

            SSSFile.Save("temp//MnSlMap.usd", trim: true);
            ImageResource.AddFile("MnSlMap.usd", "temp//MnSlMap.usd");

            SmSt.Save("temp//SmSt.dat");
            ImageResource.AddFile("SmSt.dat", "temp//SmSt.dat");

            // generate mxdt
            HSDRawFile f = new HSDRawFile();
            f.Roots.Add(new HSDRootNode()
            {
                Name = "mexData",
                Data = mxdt
            });
            f.Save("MxDt.dat");
            ImageResource.AddFile("MxDt.dat", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MxDt.dat"));

            progress(null, new ProgressChangedEventArgs(10, null));
        }

        /// <summary>
        /// Generates new mexData structure
        /// </summary>
        private static MEX_Data GenerateMxDt()
        {
            // begin dumping data to mxdt
            var mxdt = new MEX_Data();

            // track all mexItems
            List<MEX_Item> mexItems = new List<MEX_Item>();

            // clear effect banks
            ClearEffects();

            // set metaData
            mxdt.MetaData = new MEX_Meta()
            {
                NumOfCSSIcons = FighterIcons.Count,
                NumOfSSSIcons = StageIcons.Count,
                NumOfEffects = EffectFiles.Count,
                NumOfSSMs = SoundBanks.Count,
                NumOfMusic = BackgroundMusic.Count,
                NumOfInternalIDs = Fighters.Count,
                NumOfExternalIDs = Fighters.Count,
                NumOfInternalStage = Stages.Count,
                NumOfExternalStage = StageIDs.Count,
                EnterScene = StartingScene,
                LastMajor = LastMajorSceneID,
                LastMinor = LastMinorSceneID
            };
            mxdt.MetaData._s.SetByte(0, VersionMajor);
            mxdt.MetaData._s.SetByte(1, VersionMinor);


            // menu data
            mxdt.MenuTable = new MEX_MenuTable();


            // update external fighter ids
            // z order icons
            var sortedIcons = FighterIcons;//.OrderBy(e => e.Z);
            foreach (var v in sortedIcons)
                v.Icon.ExternalCharID = (byte)MEXFighterIDConverter.ToExternalID(Fighters.IndexOf(v.Fighter), Fighters.Count);
            mxdt.MenuTable.CSSIconData = new MEX_IconData() { Icons = sortedIcons.Select(e => e.Icon).ToArray() };



            // clear non vanilla stage ids
            StageIDs.RemoveRange(0x11E, StageIDs.Count - 0x11E);

            Dictionary<MEXStage, int> stageToId = new Dictionary<MEXStage, int>();
            foreach(var v in Stages)
            {
                var index = Stages.IndexOf(v);

                // check if this is a new stage
                if (StageIDs.Find(e => e.StageID == index) == null)
                {
                    // assign new external id for this stage
                    stageToId.Add(v, StageIDs.Count);
                    StageIDs.Add(new MEX_StageIDTable() { StageID = index });
                }
                else
                {
                    stageToId.Add(v, StageIDs.FindIndex(e => e.StageID == index));
                }
            }

            // generate an id for each new non vanilla stage
            foreach (var sicon in StageIcons)
            {
                if(stageToId.ContainsKey(sicon.Stage))
                    sicon._icon.ExternalID = stageToId[sicon.Stage];
            }

            // update external stage ids
            mxdt.MenuTable.SSSIconData = new HSDArrayAccessor<MEX_StageIconData>() { Array = StageIcons.Select(e => e._icon).ToArray() };
            mxdt.MenuTable.Parameters = MenuParameters;
            
            // random stage select bitfield
            mxdt.MenuTable.SSSBitField = new SSSBitfield() { _s = new HSDStruct(StageIcons.Count / 8 + 1) };

            // store random enabled
            for (int i = 0; i < StageIcons.Count; i++)
                mxdt.MenuTable.SSSBitField.SetField(i, StageIcons[i].RandomEnabled);


            // scene data
            mxdt.SceneData = SceneData;


            // save music table
            mxdt.MusicTable = new MEX_BGMStruct();
            mxdt.MusicTable.BGMFileNames = new HSDFixedLengthPointerArrayAccessor<HSD_String>() { Array = BackgroundMusic.Select(e => new HSD_String() { Value = e.FileName }).ToArray() };
            mxdt.MusicTable.BGMLabels = new HSDFixedLengthPointerArrayAccessor<HSD_ShiftJIS_String>() { Array = BackgroundMusic.Select(e => new HSD_ShiftJIS_String() { Value = e.Label }).ToArray() };
            mxdt.MusicTable.MenuPlaylist = new HSDArrayAccessor<HSDRaw.MEX.Sounds.MEX_PlaylistItem>() { Array = MainMenuPlaylist.Entries.Select(e => new MEX_PlaylistItem() { HPSID = (ushort)BackgroundMusic.IndexOf(e.Music), ChanceToPlay = (short)e.PlayChance }).ToArray() };
            mxdt.MusicTable.MenuPlayListCount = MainMenuPlaylist.Entries.Count;



            // save fighter data

            mxdt.FighterData = new MEX_FighterData();
            mxdt.FighterFunctions = new MEX_FighterFunctionTable();
            mxdt.KirbyData = new MEX_KirbyTable();
            mxdt.KirbyFunctions = new MEX_KirbyFunctionTable();

            // runtime fighter pointer struct
            mxdt.FighterData._s.GetCreateReference<HSDAccessor>(0x40)._s.Resize(Fighters.Count * 8);
            mxdt.FighterData.RuntimeIntroParamLookup._s.Resize(Fighters.Count * 4);


            // kirby runtimes
            mxdt.KirbyData.CapFileRuntime = new HSDAccessor() { _s = new HSDStruct(4 * Fighters.Count) };
            mxdt.KirbyData.CapFtCmdRuntime = new HSDAccessor() { _s = new HSDStruct(4 * Fighters.Count) };
            mxdt.KirbyData.CostumeRuntime = new HSDAccessor() { _s = new HSDStruct(4 * Fighters.Count) };
            mxdt.KirbyFunctions.MoveLogicRuntime = new HSDAccessor() { _s = new HSDStruct(4 * Fighters.Count) };

            int internalId = 0;
            foreach (var f in Fighters)
            {
                var externalId = MEXFighterIDConverter.ToExternalID(internalId, Fighters.Count);

                // set parameters
                var fd = mxdt.FighterData;
                var kb = mxdt.KirbyData;

                fd.NameText.Set(externalId, new HSD_String(f.NameText));
                fd.CharFiles.Set(internalId, new MEX_CharFileStrings() { FileName = f.FighterDataPath, Symbol = f.FighterDataSymbol });
                fd.AnimCount.Set(internalId, new MEX_AnimCount() { AnimCount = f.AnimCount });
                fd.AnimFiles.Set(internalId, new HSD_String(f.AnimFile));
                fd.ResultAnimFiles.Set(externalId, new HSD_String(f.RstAnimFile));
                fd.RstRuntime.Set(internalId, new HSDRaw.MEX.Characters.MEX_RstRuntime() { AnimMax = f.RstAnimCount });
                fd.InsigniaIDs[externalId] = f.InsigniaID;
                fd.WallJump[internalId] = f.CanWallJump ? (byte)1 : (byte)0;
                fd.EffectIDs[internalId] = (byte)GetEffectID(f.EffectFile, f.EffectSymbol);
                fd.AnnouncerCalls[externalId] = f.AnnouncerCall;

                // save costumes
                fd.CostumeFileSymbols.Set(internalId, new MEX_CostumeFileSymbolTable() { CostumeSymbols = new HSDArrayAccessor<MEX_CostumeFileSymbol>() { Array = f.Costumes.Select(e => e.Costume).ToArray() } });

                fd.CostumePointers.Set(internalId, new MEX_CostumeRuntimePointers()
                {
                    CostumeCount = (byte)f.Costumes.Count,
                    Pointer = new HSDRaw.HSDAccessor() { _s = new HSDRaw.HSDStruct(0x18 * f.Costumes.Count) }
                });

                fd.CostumeIDs.Set(externalId, new MEX_CostumeIDs()
                {
                    CostumeCount = (byte)f.Costumes.Count,
                    RedCostumeIndex = f.RedCostumeIndex,
                    BlueCostumeIndex = f.BlueCostumeIndex,
                    GreenCostumeIndex = f.GreenCostumeIndex
                });

                fd.DefineIDs.Set(externalId, new MEX_CharDefineIDs()
                {
                    InternalID = (byte)(internalId + (internalId == 11 ? -1 : 0)),
                    SubCharacterBehavior = f.SubCharacterBehavior,
                    SubCharacterInternalID = (byte)Fighters.IndexOf(f.SubCharacter)
                });

                fd.SSMFileIDs.Set(externalId, new MEX_CharSSMFileID()
                {
                    SSMID = (byte)SoundBanks.IndexOf(f.SoundBank),
                    BitField1 = (int)f.SSMBitfield1,
                    BitField2 = (int)f.SSMBitfield2
                });

                fd.VictoryThemeIDs[externalId] = BackgroundMusic.IndexOf(f.VictoryTheme);
                fd.FighterSongIDs.Set(externalId, new HSDRaw.MEX.Characters.MEX_FighterSongID()
                {
                    SongID1 = (short)BackgroundMusic.IndexOf(f.FighterSongID1),
                    SongID2 = (short)BackgroundMusic.IndexOf(f.FighterSongID1)
                });

                fd.FtDemo_SymbolNames.Set(internalId, new MEX_FtDemoSymbolNames()
                {
                    Intro = f.DemoIntro,
                    Ending = f.DemoEnding,
                    Result = f.DemoResult,
                    ViWait = f.DemoWait
                });

                fd.VIFiles.Set(externalId, new HSD_String(f.DemoFile));

                fd.ResultScale[externalId] = f.ResultScreenScale;
                if (f.TargetTestStage != null && stageToId.ContainsKey(f.TargetTestStage))
                    fd.TargetTestStageLookups[externalId] = (ushort)stageToId[f.TargetTestStage];
                else
                    fd.TargetTestStageLookups[externalId] = 0;
                fd.RaceToFinishTimeLimits[externalId] = f.RacetoTheFinishTime;
                fd.EndClassicFiles.Set(externalId, new HSD_String(f.EndClassicFile));
                fd.EndAdventureFiles.Set(externalId, new HSD_String(f.EndAdventureFile));
                fd.EndAllStarFiles.Set(externalId, new HSD_String(f.EndAllStarFile));
                fd.EndMovieFiles.Set(externalId, new HSD_String(f.EndMovieFile));

                // Kirby
                kb.CapFiles.Set(internalId, new MEX_KirbyCapFiles() { FileName = f.KirbyCapFileName, Symbol = f.KirbyCapSymbol });
                kb.KirbyEffectIDs[internalId] = (byte)GetEffectID(f.KirbyEffectFile, f.KirbyEffectSymbol);
                if (f.KirbyCostumes != null && f.KirbyCostumes.Count > 0)
                {
                    kb.KirbyCostumes.Set(internalId, new MEX_KirbyCostume() { Array = f.KirbyCostumes.Select(e => e.Costume).ToArray() });
                    kb.CostumeRuntime._s.SetReference(internalId * 4, new HSDAccessor() { _s = new HSDStruct(f.KirbyCostumes.Count * 8) });
                }
                else
                {
                    kb.KirbyCostumes.Set(internalId, null);
                    kb.CostumeRuntime._s.SetReference(internalId * 4, null);
                }


                // Functions
                var ff = mxdt.FighterFunctions;
                var func = f.Functions;

                if (func.MoveLogic != null)
                    ff.MoveLogic.Set(internalId, new HSDArrayAccessor<MEX_MoveLogic>() { Array = func.MoveLogic });
                ff.OnLoad[internalId] = func.OnLoad;
                ff.OnDeath[internalId] = func.OnDeath;
                ff.OnUnknown[internalId] = func.OnUnk;
                ff.DemoMoveLogic[internalId] = func.DemoMoveLogicPointer;
                ff.SpecialN[internalId] = func.SpecialN;
                ff.SpecialNAir[internalId] = func.SpecialNAir;
                ff.SpecialHi[internalId] = func.SpecialHi;
                ff.SpecialHiAir[internalId] = func.SpecialHiAir;
                ff.SpecialS[internalId] = func.SpecialS;
                ff.SpecialSAir[internalId] = func.SpecialSAir;
                ff.SpecialLw[internalId] = func.SpecialLw;
                ff.SpecialLwAir[internalId] = func.SpecialLwAir;
                ff.OnAbsorb[internalId] = func.OnAbsorb;
                ff.onItemCatch[internalId] = func.OnItemPickup;
                ff.onMakeItemInvisible[internalId] = func.OnMakeItemInvisible;
                ff.onMakeItemVisible[internalId] = func.OnMakeItemVisible;
                ff.onItemPickup[internalId] = func.OnItemPickup;
                ff.onItemDrop[internalId] = func.OnItemDrop;
                ff.onItemCatch[internalId] = func.OnItemCatch;
                ff.onUnknownItemRelated[internalId] = func.OnUnknownItemRelated;
                ff.onApplyHeadItem[internalId] = func.OnUnknownCharacterFlags1;
                ff.onRemoveHeadItem[internalId] = func.OnUnknownCharacterFlags2;
                ff.onHit[internalId] = func.OnHit;
                ff.onUnknownEyeTextureRelated[internalId] = func.OnUnknownEyeTextureRelated;
                ff.onFrame[internalId] = func.OnFrame;
                ff.onActionStateChange[internalId] = func.OnActionStateChange;
                ff.onRespawn[internalId] = func.OnRespawn;
                ff.onModelRender[internalId] = func.OnModelRender;
                ff.onShadowRender[internalId] = func.OnShadowRender;
                ff.onUnknownMultijump[internalId] = func.OnUnknownMultijump;
                ff.onActionStateChangeWhileEyeTextureIsChanged[internalId] = func.OnActionStateChangeWhileEyeTextureIsChanged;
                ff.onTwoEntryTable[internalId * 2] = func.OnTwoEntryTable1;
                ff.onTwoEntryTable[internalId * 2 + 1] = func.OnTwoEntryTable2;
                ff.onLand[internalId] = func.OnLand;
                ff.onExtRstAnim[internalId] = func.OnExtRstAnim;
                ff.onIndexExtResultAnim[internalId] = func.OnIndexExtRstAnim;

                ff.onSmashDown[internalId] = func.SmashDown;
                ff.onSmashUp[internalId] = func.SmashUp;
                ff.onSmashForward[internalId] = func.SmashSide;
                ff.enterFloat[internalId] = func.EnterFloat;
                ff.enterSpecialDoubleJump[internalId] = func.EnterDoubleJump;
                ff.enterTether[internalId] = func.EnterTether;
                ff.onThrowBk[internalId] = func.onThrowBk;
                ff.onThrowFw[internalId] = func.onThrowFw;
                ff.onThrowHi[internalId] = func.onThrowHi;
                ff.onThrowLw[internalId] = func.onThrowLw;
                ff.getTrailData[internalId] = func.getSwordTrail;

                var kff = mxdt.KirbyFunctions;
                kff.KirbyOnHit[internalId] = func.KirbyOnHit;
                kff.KirbyOnItemInit[internalId] = func.KirbyOnItemInit;
                kff.OnAbilityLose[internalId] = func.KirbyOnLoseAbility;
                kff.OnAbilityGain[internalId] = func.KirbyOnSwallow;
                kff.KirbySpecialN[internalId] = func.KirbySpecialN;
                kff.KirbySpecialNAir[internalId] = func.KirbySpecialNAir;



                // save items
                var itemEntries = new ushort[f.Items.Count];
                for (int i = 0; i < itemEntries.Length; i++)
                {
                    itemEntries[i] = (ushort)(MexItemOffset + mexItems.Count);
                    mexItems.Add(f.Items[i]);
                }
                mxdt.FighterData.FighterItemLookup.Set(internalId, new MEX_ItemLookup() { Entries = itemEntries });


                // PlCo bone table
                FighterCommonData.BoneTables.Set(internalId, f.BoneTable);
                FighterCommonData.FighterTable.Set(internalId, f.UnkTable);


                internalId++;
            }

            // 
            var tb1 = new byte[54]; 
            var tb2 = new byte[54];
            tb2[53] = 255;
            for(byte i = 0; i < 53; i++)
            {
                tb1[i] = i;
                tb2[i] = i;
            }
            var commonBoneTable = new SBM_BoneLookupTable()
            {
                BoneCount = 53,
            };
            commonBoneTable._s.SetReferenceStruct(0x00, new HSDStruct(tb1));
            commonBoneTable._s.SetReferenceStruct(0x04, new HSDStruct(tb2));
            FighterCommonData.BoneTables.Set(internalId, commonBoneTable);
            FighterCommonData.FighterTable.Set(internalId, null);


            // save stage data

            mxdt.StageData = new MEX_StageData();
            mxdt.StageData.StageIDTable = new HSDArrayAccessor<MEX_StageIDTable>() { Array = StageIDs.ToArray() };

            for (int i = 0; i < Stages.Count; i++)
            {
                // adjust internal id
                Stages[i].InternalID = i;

                // set stage structs
                mxdt.StageData.StageNames.Set(i, new HSD_String(Stages[i].StageName));
                mxdt.StageData.CollisionTable.Set(i, Stages[i].Collision);

                // save sound bank indices
                Stages[i].Reverb.SSMID = (byte)SoundBanks.IndexOf(Stages[i].SoundBank);
                mxdt.StageData.ReverbTable.Set(i, Stages[i].Reverb);

                // mex stages need at least one song to work
                if (Stages[i].IsMEXStage && Stages[i].Playlist.Entries.Count == 0)
                    Stages[i].Playlist.Entries.Add(new MEXPlaylistEntry() { Music = BackgroundMusic[0], PlayChance = 50 });

                // save playlist 
                mxdt.StageData.StagePlaylists.Set(i, Stages[i].Playlist.ToPlaylistStruct());

                // save items
                var itemEntries = new ushort[Stages[i].Items.Count];
                for (int j = 0; j < itemEntries.Length; j++)
                {
                    itemEntries[j] = (ushort)(MexItemOffset + mexItems.Count);
                    mexItems.Add(Stages[i].Items[j]);
                }
                mxdt.StageData.StageItemLookup.Set(i, new MEX_ItemLookup() { Entries = itemEntries });
            }
            mxdt.StageFunctions = new HSDFixedLengthPointerArrayAccessor<MEX_Stage>() { Array = Stages.Select(e => e.Stage).ToArray() };



            // save effects
            mxdt.EffectTable = new MEX_EffectData();
            mxdt.EffectTable.EffectFiles = new HSDArrayAccessor<MEX_EffectFiles>() { Array = EffectFiles.ToArray() };
            var effectFileCount = EffectFiles.Count;
            mxdt.EffectTable.RuntimeUnk1 = new HSDAccessor() { _s = new HSDStruct(0x60) };
            mxdt.EffectTable.RuntimeUnk3 = new HSDAccessor() { _s = new HSDStruct(4 * effectFileCount) };
            mxdt.EffectTable.RuntimeTexGrNum = new HSDAccessor() { _s = new HSDStruct(4 * effectFileCount) };
            mxdt.EffectTable.RuntimeTexGrData = new HSDAccessor() { _s = new HSDStruct(4 * effectFileCount) };
            mxdt.EffectTable.RuntimeUnk4 = new HSDAccessor() { _s = new HSDStruct(4 * effectFileCount) };
            mxdt.EffectTable.RuntimePtclLast = new HSDAccessor() { _s = new HSDStruct(4 * effectFileCount) };
            mxdt.EffectTable.RuntimePtclData = new HSDAccessor() { _s = new HSDStruct(4 * effectFileCount) };
            mxdt.EffectTable.RuntimeLookup = new HSDAccessor() { _s = new HSDStruct(4 * effectFileCount) };


            // save item table
            mxdt.ItemTable = new MEX_ItemTables();
            mxdt.ItemTable.CommonItems = new HSDArrayAccessor<MEX_Item>() { Array = CommonItems };
            mxdt.ItemTable.FighterItems = new HSDArrayAccessor<MEX_Item>() { Array = FighterItems };
            mxdt.ItemTable.Pokemon = new HSDArrayAccessor<MEX_Item>() { Array = PokemonItems };
            mxdt.ItemTable.StageItems = new HSDArrayAccessor<MEX_Item>() { Array = StageItems };
            mxdt.ItemTable.MEXItems = new HSDArrayAccessor<MEX_Item>() { Array = mexItems.ToArray() };
            mxdt.ItemTable._s.GetCreateReference<HSDAccessor>(0x14)._s.Resize(Math.Max(4, mexItems.Count * 4));


            // Save misc data
            mxdt.MiscData = new MEX_Misc();
            mxdt.MiscData.GawColors = new HSDArrayAccessor<MEX_GawColor>()
            {
                Array = GaWColors.ToArray()
            };


            // save stock icon node
            StockManager.GenerateStockData(Fighters.Select(e => e.Costumes.Select(r => r.Icon).ToList()).ToList(), ReservedIcons.ToList());
            StockManager.CustomStockLength = StockManager.CustomStockEntries.Length;
            // save emblems
            (IfAllFile["Eblm_matanim_joint"].Data as HSD_MatAnimJoint).MaterialAnimation.TextureAnimation.FromTOBJs(Emblems, true);


            // generate fighter select
            var mexSelectChr = new MEX_mexSelectChr();

            mexSelectChr.IconModel = new HSD_JOBJ()
            {
                Flags = JOBJ_FLAG.CLASSICAL_SCALING | JOBJ_FLAG.ROOT_XLU | JOBJ_FLAG.ROOT_TEXEDGE,
                SX = 1,
                SY = 1,
                SZ = 1
            };
            mexSelectChr.IconAnimJoint = new HSD_AnimJoint();

            mexSelectChr.IconMatAnimJoint = new HSD_MatAnimJoint();

            foreach (var v in sortedIcons)
            {
                // remove next
                v._joint.Next = null;
                v.MaterialAnimation.Next = null;

                // update image
                v._joint.Dobj.Next.Mobj.Textures = v.Image;

                // remove no zupdate flag
                v._joint.Flags |= JOBJ_FLAG.TEXEDGE;

                //v._joint.Dobj.Mobj.RenderFlags &= ~RENDER_MODE.NO_ZUPDATE;
                //v._joint.Dobj.Next.Mobj.RenderFlags &= ~RENDER_MODE.NO_ZUPDATE;
                
                v._joint.Dobj.Mobj.RenderFlags |= RENDER_MODE.NO_ZUPDATE;
                v._joint.Dobj.Next.Mobj.RenderFlags |= RENDER_MODE.NO_ZUPDATE;

                // add child
                mexSelectChr.IconModel.AddChild(v._joint);
                mexSelectChr.IconAnimJoint.AddChild(v.ToAnimJoint());
                mexSelectChr.IconMatAnimJoint.AddChild(v.MaterialAnimation);
            }

            // generate csp node
            var stride = Fighters.Count;
            List<FOBJKey> keys = new List<FOBJKey>();
            List<HSD_TOBJ> tobjs = new List<HSD_TOBJ>();
            foreach (var v in Fighters)
            {
                for (int i = 0; i < v.Costumes.Count; i++)
                {
                    if (v.Costumes[i].CSP != null)
                    {
                        var id = MEXFighterIDConverter.ToExternalID(Fighters.IndexOf(v), Fighters.Count);
                        keys.Add(new FOBJKey() { Frame = stride * i + id, Value = tobjs.Count, InterpolationType = GXInterpolationType.HSD_A_OP_CON });
                        tobjs.Add(v.Costumes[i].CSP);
                    }
                }
            }
            keys = keys.OrderBy(e => e.Frame).ToList();

            HSD_MatAnim anim = new HSD_MatAnim();
            anim.TextureAnimation = new HSD_TexAnim();
            anim.TextureAnimation.FromTOBJs(tobjs, false);
            anim.TextureAnimation.AnimationObject = new HSD_AOBJ();
            anim.TextureAnimation.AnimationObject.EndFrame = 1200;
            anim.TextureAnimation.AnimationObject.FObjDesc = new HSD_FOBJDesc();
            anim.TextureAnimation.AnimationObject.FObjDesc.SetKeys(keys, (byte)TexTrackType.HSD_A_T_TIMG);
            anim.TextureAnimation.AnimationObject.FObjDesc.Next = new HSD_FOBJDesc();
            anim.TextureAnimation.AnimationObject.FObjDesc.Next.SetKeys(keys, (byte)TexTrackType.HSD_A_T_TCLT);

            mexSelectChr.CSPMatAnim = anim;
            mexSelectChr.CSPStride = stride;

            CSSFile["mexSelectChr"].Data = mexSelectChr;




            // generate stage select data
            var mexMapData = SSSFile["mexMapData"].Data as MEX_mexMapData;

            // note: can't generate icon model itself

            mexMapData.IconAnimJoint = new HSD_AnimJoint();
            mexMapData.IconAnimJoint.Child = new HSD_AnimJoint();

            mexMapData.PositionModel = new HSD_JOBJ()
            {
                Flags = JOBJ_FLAG.CLASSICAL_SCALING | JOBJ_FLAG.ROOT_XLU,
                SX = 1,
                SY = 1,
                SZ = 1
            };
            mexMapData.PositionAnimJoint = new HSD_AnimJoint();

            foreach (var v in StageIcons)
            {
                v._joint.Next = null;
                mexMapData.PositionModel.AddChild(v._joint);
                mexMapData.PositionAnimJoint.AddChild(v.ToAnimJoint());
            }

            var iconTOBJs = StageIcons.Select(e => e.Image).ToList();
            iconTOBJs.Insert(0, StageIcon2);
            iconTOBJs.Insert(0, StageIcon1);
            mexMapData.IconMatAnimJoint = new HSD_MatAnimJoint();
            mexMapData.IconMatAnimJoint.Child = new HSD_MatAnimJoint();
            mexMapData.IconMatAnimJoint.Child.MaterialAnimation = new HSD_MatAnim();
            mexMapData.IconMatAnimJoint.Child.MaterialAnimation.Next = new HSD_MatAnim();
            mexMapData.IconMatAnimJoint.Child.MaterialAnimation.Next.TextureAnimation = new HSD_TexAnim();
            mexMapData.IconMatAnimJoint.Child.MaterialAnimation.Next.TextureAnimation.FromTOBJs(iconTOBJs, true);

            mexMapData.StageNameMaterialAnimation = new HSD_MatAnimJoint();
            mexMapData.StageNameMaterialAnimation.Child = new HSD_MatAnimJoint();
            mexMapData.StageNameMaterialAnimation.Child.Child = new HSD_MatAnimJoint();
            mexMapData.StageNameMaterialAnimation.Child.Child.MaterialAnimation = new HSD_MatAnim();
            mexMapData.StageNameMaterialAnimation.Child.Child.MaterialAnimation.TextureAnimation = new HSD_TexAnim();
            mexMapData.StageNameMaterialAnimation.Child.Child.MaterialAnimation.TextureAnimation.FromTOBJs(StageIcons.Select(e => e._previewText), true);

            // done
            return mxdt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mxdt"></param>
        /// <param name="path"></param>
        private static void SaveSoundBanks(MEX_Data mxdt, string path)
        {
            // save sem table
            mxdt.SSMTable = new MEX_SSMTable();

            mxdt.SSMTable.SSM_SSMFiles = new HSDNullPointerArrayAccessor<HSD_String>();
            mxdt.SSMTable.SSM_BufferSizes = new HSDArrayAccessor<MEX_SSMSizeAndFlags>();
            mxdt.SSMTable.SSM_LookupTable = new HSDArrayAccessor<MEX_SSMLookup>();

            mxdt.SSMTable.SSM_BufferSizes.Set(SoundBanks.Count, new MEX_SSMSizeAndFlags());// blank entry at end
            mxdt.SSMTable.SSM_LookupTable.Set(SoundBanks.Count, new MEX_SSMLookup());// blank entry at beginning

            // runtime structs
            HSDStruct rtTable = new HSDStruct(6 * 4);
            rtTable.SetReferenceStruct(0x00, new HSDStruct(GeneratePaddedBuffer(0x180, 0x01)));
            rtTable.SetReferenceStruct(0x04, new HSDStruct(GeneratePaddedBuffer(SoundBanks.Count * 4, 0x02)));
            rtTable.SetReferenceStruct(0x08, new HSDStruct(GeneratePaddedBuffer(SoundBanks.Count * 4, 0x03)));
            rtTable.SetReferenceStruct(0x0C, new HSDStruct(GeneratePaddedBuffer(SoundBanks.Count * 4, 0x04)));
            rtTable.SetReferenceStruct(0x10, new HSDStruct(GeneratePaddedBuffer(SoundBanks.Count * 4, 0x05)));
            rtTable.SetReferenceStruct(0x14, new HSDStruct(GeneratePaddedBuffer(SoundBanks.Count * 4, 0x06)));
            mxdt.SSMTable._s.SetReferenceStruct(0x0C, rtTable);


            List<int> soundIDs = new List<int>();
            List<string> soundNames = new List<string>();
            var soundCount = 0;
            for (int i = 0; i < SoundBanks.Count; i++)
            {
                // update starting index
                SoundBanks[i].SoundBank.StartIndex = soundCount;

                // adjust sound id to relative
                for (int j = 0; j < SoundBanks[i].ScriptBank.Scripts.Length; j++)
                {
                    if (SoundBanks[i].ScriptBank.Scripts[j].SFXID != -1)
                        SoundBanks[i].ScriptBank.Scripts[j].SFXID += soundCount;

                    soundIDs.Add(i * 10000 + j);
                    soundNames.Add(string.IsNullOrEmpty(SoundBanks[i].ScriptBank.Scripts[j].Name) ? "SFXUntitled" : SoundBanks[i].ScriptBank.Scripts[j].Name);
                }

                // save sound bank
                var tempPath = path + "audio/" + SoundBanks[i].SoundBank.Name;
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(tempPath));
                SoundBanks[i].SoundBank.Save(tempPath, out int bufferSize);
                ImageResource.AddFile(@"audio\us\" + SoundBanks[i].SoundBank.Name, tempPath);

                // update mxdt info
                mxdt.SSMTable.SSM_SSMFiles.Set(i, new HSD_String() { Value = SoundBanks[i].SoundBank.Name });
                mxdt.SSMTable.SSM_BufferSizes.Set(i, new MEX_SSMSizeAndFlags() { SSMFileSize = bufferSize, Flag = (int)SoundBanks[i].Flags });
                mxdt.SSMTable.SSM_LookupTable.Set(i, new MEX_SSMLookup() { EntireFlag = (int)SoundBanks[i].GroupFlags });

                soundCount += SoundBanks[i].SoundBank.Sounds.Length;
            }

            // save smst
            var smst = SmSt.Roots[0].Data as smSoundTestLoadData;

            smst.SoundNames = soundNames.ToArray();
            smst.SoundIDs = soundIDs.ToArray();
            smst.MusicBanks = BackgroundMusic.Select(e => e.FileName).ToArray();
            smst.SoundBankCount = SoundBanks.Select(e => e.ScriptBank.Scripts.Length).ToArray();
            smst.SoundBankNames = SoundBanks.Select(e => e.SoundBank.Name).ToArray();

            // save sem
            SEM.SaveSEMFile(path + "audio/smash2.sem", SoundBanks.Select(e => e.ScriptBank).ToList());
            ImageResource.AddFile("audio/us/smash2.sem", path + "audio/smash2.sem");


            // adjust sound id to relative again
            for (int i = 0; i < SoundBanks.Count; i++)
            {
                for (int j = 0; j < SoundBanks[i].ScriptBank.Scripts.Length; j++)
                {
                    if (SoundBanks[i].ScriptBank.Scripts[j].SFXID != -1)
                        SoundBanks[i].ScriptBank.Scripts[j].SFXID -= SoundBanks[i].SoundBank.StartIndex;
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static bool InitFromISO(string isoPath)
        {
            Close();
            _imageResource = new ImageResource();

            var success = _imageResource.OpenISO(isoPath);
            if (success)
                Init();

            return success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static bool InitFromFileSystem(string folderPath)
        {
            Close();
            _imageResource = new ImageResource();

            var success = _imageResource.OpenFolder(folderPath);
            if (success)
                Init();

            return success;
        }

        /// <summary>
        /// 
        /// </summary>
        private static void LoadMexMenuFileData(MEX_Data _mexData)
        {
            // Fighters -----------------------------------------------------------------------------------
            var mexSlcChar = CSSFile["mexSelectChr"].Data as MEX_mexSelectChr;
            var CSSIcons = _mexData.MenuTable.CSSIconData.Icons;

            var cssiconModel = GetModelBounds(mexSlcChar.IconModel.Child.Dobj.Next);
            var cssjoints = mexSlcChar.IconModel.Children;
            var cssanimJoints = mexSlcChar.IconAnimJoint.Children;
            var cssmatAnimJoint = mexSlcChar.IconMatAnimJoint.Children;
            var csps = mexSlcChar.CSPMatAnim.TextureAnimation.ToTOBJs();
            var keys = mexSlcChar.CSPMatAnim.TextureAnimation.AnimationObject.FObjDesc.GetDecodedKeys();
            var cspStride = mexSlcChar.CSPStride;

            for (int i = 0; i < CSSIcons.Length; i++)
            {
                var icos = new MEXFighterIcon();
                icos.Icon = CSSIcons[i];
                var fighterId = MEXFighterIDConverter.ToInternalID(icos.Icon.ExternalCharID, Fighters.Count);
                icos.Fighter = fighterId == -1 ? null : Fighters[fighterId];
                icos._joint = cssjoints[i];
                icos.FromAnimJoint(cssanimJoints[i]);
                icos.Image = cssjoints[i].Dobj.Next.Mobj.Textures;
                icos.IconModel = cssiconModel;
                icos.MaterialAnimation = cssmatAnimJoint[i];

                // load csps
                int cspIndex = 0;
                while (true)
                {
                    var key = icos.Icon.ExternalCharID + (cspIndex * cspStride);

                    var k = keys.Find(e => e.Frame == key);

                    if (k == null)
                        break;

                    var fighter = Fighters[MEXFighterIDConverter.ToInternalID(icos.Icon.ExternalCharID, Fighters.Count)];
                    
                    if(cspIndex < fighter.Costumes.Count)
                        fighter.Costumes[cspIndex].CSP = csps[(int)k.Value];

                    cspIndex++;
                }

                FighterIcons.Add(icos);
            }



            // Stage----------------------------------------------------------------------------------

            var mexMapData = SSSFile["mexMapData"].Data as MEX_mexMapData;
            var SSSIcons = _mexData.MenuTable.SSSIconData;

            var iconModel = GetModelBounds(mexMapData.IconModel.Child.Dobj.Next);

            var joints = mexMapData.PositionModel.Children;
            var animJoints = mexMapData.PositionAnimJoint.Children;
            var images = mexMapData.IconMatAnimJoint.Child.MaterialAnimation.Next.TextureAnimation.ToTOBJs();
            var previews = mexMapData.StageNameMaterialAnimation.Child.Child.MaterialAnimation.TextureAnimation.ToTOBJs();

            StageIcon1 = images[0];
            StageIcon2 = images[1];

            for (int i = 0; i < SSSIcons.Length; i++)
            {
                var icos = new MEXStageIcon();
                icos._icon = SSSIcons[i];

                if (i < joints.Length)
                    icos._joint = joints[i];
                else
                    icos._joint = HSDAccessor.DeepClone<HSD_JOBJ>(joints[0]);

                if (i < animJoints.Length)
                    icos.FromAnimJoint(animJoints[i]);

                if (i + 2 < images.Length)
                    icos.Image = images[2 + i];
                else
                    icos.Image = images[2];

                icos.IconModel = iconModel;
                var stageID = StageIDs[SSSIcons[i].ExternalID].StageID;
                icos.Stage = stageID < Stages.Count ? Stages[stageID] : null;

                if (i < previews.Length)
                    icos._previewText = previews[i];

                if (_mexData.MenuTable._s.Length > 0 && _mexData.MenuTable.SSSBitField != null)
                    icos.RandomEnabled = _mexData.MenuTable.SSSBitField.GetField(i);

                StageIcons.Add(icos);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static RectangleF GetModelBounds(HSD_DOBJ dobj)
        {
            var positionAttribute = dobj.Pobj.ToGXAttributes().First(e => e.AttributeName == HSDRaw.GX.GXAttribName.GX_VA_POS);

            float maxX = float.MinValue;
            float maxY = float.MinValue;
            float minX = float.MaxValue;
            float minY = float.MaxValue;
            foreach (var vertex in positionAttribute.DecodedData)
            {
                maxX = Math.Max(maxX, vertex[0]);
                minX = Math.Min(minX, vertex[0]);
                maxY = Math.Max(maxY, vertex[1]);
                minY = Math.Min(minY, vertex[1]);
            }

            return new RectangleF(minX, minY, maxX - minX, maxY - minY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static int GetExternalStageID(int stageid)
        {
            var ids = StageIDs.Select((e, index) => new Tuple<int, int>(e.StageID, index)).Where(e => e.Item1 == stageid).Select(e => e.Item2).ToArray();

            // new stage id
            if (ids.Length == 0)
            {
                StageIDs.Add(new MEX_StageIDTable() { StageID = stageid });
                return StageIDs.Count - 1;
            }

            return ids[ids.Length - 1];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static int GetEffectID(string filename, string symbol)
        {
            if (string.IsNullOrEmpty(filename) || string.IsNullOrEmpty(symbol))
                return -1;

            // find existing effect slot and update the symbol if needed
            var effect = EffectFiles.FindIndex(e => e.FileName == filename);

            if (effect != -1)
            {
                EffectFiles[effect].Symbol = symbol;
                return effect;
            }

            // find slot for new effect
            var empty = EffectFiles.FindIndex(e =>
                e != EffectFiles[30] && 
                string.IsNullOrEmpty(e.FileName));

            if(empty != -1)
            {
                EffectFiles[empty].FileName = filename;
                EffectFiles[empty].Symbol = symbol;
                return empty;
            }

            // add new effect slot
            if(EffectFiles.Count < 64 && empty != 64)
            {
                EffectFiles.Add(new MEX_EffectFiles() { FileName = filename, Symbol = symbol });
                return EffectFiles.Count - 1;
            }

            // no room for new effects
            System.Windows.Forms.MessageBox.Show($"There is no more room for additional effect banks!\nCannot add {filename}", "Add Effect Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        private static void ClearEffects()
        {
            EffectFiles[35].FileName = null;
            EffectFiles[35].Symbol = null;
            EffectFiles[40].FileName = null;
            EffectFiles[40].Symbol = null;
            for (int i = 22; i <= 30; i++)
            {
                EffectFiles[i].FileName = null;
                EffectFiles[i].Symbol = null;
            }
            for (int i = 42; i <= 45; i++)
            {
                EffectFiles[i].FileName = null;
                EffectFiles[i].Symbol = null;
            }
            for (int i = 50; i < EffectFiles.Count; i++)
            {
                EffectFiles[i].FileName = null;
                EffectFiles[i].Symbol = null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static byte[] GeneratePaddedBuffer(int size, byte value)
        {
            byte[] b = new byte[size];
            for (int i = 0; i < b.Length; i++)
                b[i] = value;
            return b;
        }
    }
}
