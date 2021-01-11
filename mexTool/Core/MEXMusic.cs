using HSDRaw.Common;
using System.ComponentModel;
using YamlDotNet.Serialization;

namespace mexTool.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class MEXMusic
    {
        [Category("Property"), DisplayName("File Name"), Description("The filename to look for in the audio folder.")]
        public string FileName
        {
            get => _fileName;
            set
            {
                // only allow if file does not already exist
                if (_fileName != null && MEX.ImageResource.FileExists("audio\\" + value))
                    return;

                // rename file on image
                if (_fileName != null)
                    MEX.ImageResource.RenameFile("audio\\" + _fileName, "audio\\" + value);

                // accept new filename
                _fileName = value;
            }
        }
        private string _fileName;

        [Category("Property"), DisplayName("Label"), Description("The label used in-game to refer to the track.")]
        public string Label
        {
            get => _label; 
            set
            {
                _label = new HSD_ShiftJIS_String(value).Value;
            }
        }
        private string _label;


        [Browsable(false), YamlIgnore]
        public bool IsMexMusic
        {
            get
            {
                return MEX.BackgroundMusic.IndexOf(this) > 97;
            }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Label) ? FileName : Label;
        }
    }
}
