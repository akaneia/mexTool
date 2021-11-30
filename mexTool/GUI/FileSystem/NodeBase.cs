using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mexTool.GUI.FileSystem
{
    public class NodeBase : TreeNode
    {

        public object Properties;

        public string FullImagePath { get => FullPath.Length > 5 ? FullPath.Substring(5) : ""; }

        public virtual void ExpandData()
        {

        }
    }
}
