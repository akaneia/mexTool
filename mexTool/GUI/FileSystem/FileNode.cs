using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mexTool.GUI.FileSystem
{
    public class FileNode : NodeBase
    {
        /// <summary>
        /// 
        /// </summary>
        public class FileProperties
        {
            public FileNode _node;

            public string FileName { get => _node.Text; }

            public string FileSize { get => $"{Core.MEX.ImageResource.GetFileSize(_node.FullImagePath) / 1000f} KB"; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file_name"></param>
        public FileNode(string file_name)
        {
            Text = file_name;
            ImageKey = "file";
            SelectedImageKey = "file";

            Properties = new FileProperties() { _node = this };
        }
    }
}
