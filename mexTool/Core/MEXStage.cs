using HSDRaw.MEX;
using HSDRaw.MEX.Stages;
using MeleeMedia.Audio;
using mexTool.Tools;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.Windows.Forms;
using System.Text;

namespace mexTool.Core
{
    public class MEXStage
    {
        public MEX_Stage Stage = new MEX_Stage();
        public MEX_StageReverb Reverb = new MEX_StageReverb();
        public MEX_StageCollision Collision = new MEX_StageCollision();

        /// <summary>
        /// 
        /// </summary>
        public MEXPlaylist Playlist = new MEXPlaylist();

        /// <summary>
        /// 
        /// </summary>
        public BindingList<MEX_Item> Items = new BindingList<MEX_Item>();

        [Browsable(false)]
        public int InternalID { get => Stage.StageInternalID; set { Stage.StageInternalID = value; Collision.InternalID = value; } }


        [Category("0 - General"), DisplayName("Stage Name"), Description("")]
        public string StageName { get; set; } = "";

        [Category("0 - General"), DisplayName("File Path"), Description("")]
        public string FileName { get => Stage.StageFileName; set => Stage.StageFileName = value; }

        [Category("0 - General"), DisplayName("Unknown Stage Value"), Description("")]
        public int UnknownValue { get => Stage.UnknownValue; set => Stage.UnknownValue = value; }



        [Category("1 - Sound"), DisplayName("Sound Bank"), Description("")]
        [TypeConverter(typeof(SoundFileConverter))]
        [YamlIgnore]
        public MEXSoundBank SoundBank { get; set; }

        [Category("1 - Sound"), DisplayName("Reverb"), Description("")]
        public int ReverbValue { get => Reverb.Reverb; set => Reverb.Reverb = (byte)value; }

        [Category("1 - Sound"), DisplayName("Unknown Sound Data"), Description("")]
        public int Unknown { get => Reverb.Unknown; set => Reverb.Unknown = (byte)value; }





        [Category("2 - Extra"), DisplayName("Moving Collision Points"), Description("")]
        public MEX_MovingCollisionPoint[] MovingCollisions
        {
            get => Stage.MovingCollisionPoint?.Array;
            set
            {
                if (value == null)
                    return;

                Stage.MovingCollisionPointCount = value.Length;
                if (value == null || value.Length == 0)
                {
                    Stage.MovingCollisionPoint = null;
                }
                else
                {
                    Stage.MovingCollisionPoint = new HSDRaw.HSDArrayAccessor<MEX_MovingCollisionPoint>();
                    Stage.MovingCollisionPoint.Array = value;
                }
            }
        }

        [Category("2 - Extra"), DisplayName("Map GOBJ Functions"), Description("")]
        public MEX_MapGOBJFunctions[] MapGOBJs
        {
            get => Stage.GOBJFunctions?.Array;
            set
            {
                if (value == null || value.Length == 0)
                {
                    Stage.GOBJFunctions = null;
                }
                else
                {
                    Stage.GOBJFunctions = new HSDRaw.HSDArrayAccessor<MEX_MapGOBJFunctions>();
                    Stage.GOBJFunctions.Array = value;
                }
            }
        }



        [Category("3 - Functions"), DisplayName("OnStageInit"), Description(""), TypeConverter(typeof(HexType))]
        public uint OnStageInit { get => Stage.OnStageInit; set => Stage.OnStageInit = value; }

        [Category("3 - Functions"), DisplayName("OnStageLoad"), Description(""), TypeConverter(typeof(HexType))]
        public uint OnStageLoad { get => Stage.OnStageLoad; set => Stage.OnStageLoad = value; }

        [Category("3 - Functions"), DisplayName("OnStageGo"), Description(""), TypeConverter(typeof(HexType))]
        public uint OnStageGo { get => Stage.OnStageGo; set => Stage.OnStageGo = value; }

        [Category("3 - Functions"), DisplayName("OnUnknown1"), Description(""), TypeConverter(typeof(HexType))]
        public uint OnUnknown1 { get => Stage.OnUnknown1; set => Stage.OnUnknown1 = value; }

        [Category("3 - Functions"), DisplayName("OnUnknown2"), Description(""), TypeConverter(typeof(HexType))]
        public uint OnUnknown2 { get => Stage.OnUnknown2; set => Stage.OnUnknown2 = value; }

        [Category("3 - Functions"), DisplayName("OnUnknown3"), Description(""), TypeConverter(typeof(HexType))]
        public uint OnUnknown3 { get => Stage.OnUnknown3; set => Stage.OnUnknown3 = value; }

        [Category("3 - Functions"), DisplayName("OnUnknown4"), Description(""), TypeConverter(typeof(HexType))]
        public uint OnUnknown4 { get => Stage.OnUnknown4; set => Stage.OnUnknown4 = value; }


        [Browsable(false), YamlIgnore]
        public bool IsMEXStage
        {
            get
            {
                return MEX.Stages.IndexOf(this) > 70;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveStageToPackage(string filePath)
        {
            using (FileStream zipToOpen = new FileStream(filePath, FileMode.OpenOrCreate))
                SaveStagePackageToStream(zipToOpen);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void SaveStagePackageToStream(Stream stream)
        {
            using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Create))
            {
                // serialize fighter data
                var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .WithTypeInspector(inspector => new MEXTypeInspector(inspector))
                .Build();
                ZipArchiveEntry fighter = archive.CreateEntry("stage.yml");
                using (StreamWriter writer = new StreamWriter(fighter.Open()))
                    writer.Write(serializer.Serialize(this));

                // add stage file
                archive.AddFile(Path.GetFileName(FileName), MEX.ImageResource.GetFileData(Path.GetFileName(FileName)));

                // sound
                if (SoundBank != null && SoundBank.SoundBank.Name != "null.ssm")
                {
                    using (MemoryStream soundStream = new MemoryStream())
                    {
                        SoundBank.WriteToStream(soundStream);
                        archive.AddFile("sound.spk", soundStream.ToArray());
                    }
                }

                // music
                foreach (var p in Playlist.Entries)
                {
                    if (p.Music.IsMexMusic)
                        archive.AddFile(p.MusicFileName, MEX.ImageResource.GetFileData("audio\\" + p.Music.FileName));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static void InstallStageFromFile(string filePath)
        {
            using (FileStream zipToOpen = new FileStream(filePath, FileMode.OpenOrCreate))
                InstallStageFromStream(zipToOpen);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static void InstallStageFromStream(Stream stream)
        {
            int index = -1;
            using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                var stageEntry = archive.GetEntry("stage.yml");

                var serializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .WithTypeInspector(inspector => new MEXTypeInspector(inspector))
                .Build();

                MEXStage stage = null;

                using (StreamReader r = new StreamReader(stageEntry.Open()))
                    stage = serializer.Deserialize<MEXStage>(r.ReadToEnd());

                if (stage == null)
                    return;

                // get all dat files in archive
                List<string> datFiles = archive.Entries.Where(e => Path.GetExtension(e.Name).ToLower() == ".dat").Select(e=>e.Name).ToList();

                // check if stage already exists
                foreach (var mstage in MEX.Stages)
                {
                    if (datFiles.Contains(Path.GetFileName(mstage.FileName)))
                    {
                        if (index != -1)
                        {
                            MessageBox.Show("There was an issue installing the stages files.\nInstallation was canceled.", "Stage Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        if (MessageBox.Show($"A stage with the filename {mstage.FileName} already exists.\nWould you like to overwrite?", "Stage Import", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            return;
                        }
                        else
                        {
                            index = MEX.Stages.IndexOf(mstage);
                        }    
                    }
                }
    
                // add stage file to file system
                foreach (var stageFile in datFiles)
                {
                    var data = archive.GetFile(Path.GetFileName(stageFile));
                    if (data != null)
                        MEX.ImageResource.AddFile(Path.GetFileName(stageFile), data);
                }

                // don't add vanilla soundbanks
                stage.SoundBank = MEX.SoundBanks[55];
                var soundFile = archive.GetFile("sound.spk");
                if (soundFile != null)
                    using (MemoryStream soundStream = new MemoryStream(soundFile))
                    {
                        var sf = new MEXSoundBank(soundStream);
                        var soundBank = Core.MEX.SoundBanks.FirstOrDefault(e => e.SoundBank.Name == sf.SoundBank.Name);

                        if (soundBank != null)
                        {
                            MessageBox.Show($"Warning: Soundbank with name \"{soundBank.SoundBank.Name}\" already exists.\nUsing existing soundbank instead.", "Soundbank Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            stage.SoundBank = soundBank;
                        }
                        else
                        {
                            stage.SoundBank = sf;
                            MEX.SoundBanks.Add(sf);
                        }
                    }

                // link and add missing music
                foreach(var v in stage.Playlist.Entries)
                {
                    var music = MEX.BackgroundMusic.FirstOrDefault(e => e.FileName == v.MusicFileName);

                    if (music != null)
                        v.Music = music;
                    else
                    {
                        // find music in zip
                        var musicFile = archive.GetFile(Path.GetFileName(v.MusicFileName));

                        if(musicFile != null)
                        {
                            MEX.ImageResource.AddFile("audio\\" + Path.GetFileName(v.MusicFileName), musicFile);
                            // add music
                            MEX.BackgroundMusic.Add(v.Music);
                        }
                    }
                }

                // add stage
                if (index == -1)
                    MEX.Stages.Add(stage);
                else
                    MEX.Stages[index] = stage;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetMoveLogicStruct()
        {
            StringBuilder table = new StringBuilder();
            int index = 0;
            foreach (var m in MapGOBJs)
            {
                table.AppendLine("\t// map gobj " + index++);

                table.AppendLine(string.Format(
                    "\t{{" +
                    "\n\t\t.onCreation = 0x{0, -8}," +
                    "\n\t\t.onDeletion = 0x{1, -8}," +
                    "\n\t\t.onFrame = 0x{2, -8}," +
                    "\n\t\t.onUnk = 0x{3, -8}," +
                    "\n\t\t.is_lobj = {4}," +
                    "\n\t\t.is_fog = {5}," +
                    "\n\t\t.is_cobj = {6}," +
                    "\n\t}},",
            m.OnCreation.ToString("X"),
            m.OnDeletion.ToString("X"),
            m.OnFrame.ToString("X"),
            m.OnUnk.ToString("X"),
            (m.Bitflags & 0x80000000) == 0 ? 0 : 1,
            (m.Bitflags & 0x40000000) == 0 ? 0 : 1,
            (m.Bitflags & 0x20000000) == 0 ? 0 : 1
            ));
            }

            return @"__attribute__((used))
static struct MapDesc map_gobjs[] = {
" + table.ToString() + @"}; ";
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(StageName) ? (string.IsNullOrEmpty(FileName) ? "Untitled Stage" : FileName) : StageName;
        }
    }
}
