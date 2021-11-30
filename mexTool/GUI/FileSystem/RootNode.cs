using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mexTool.GUI.FileSystem
{
    public class RootNode : FolderNode
    {
        public class RootProperties
        {
            public int NumberOfFiles { get => Core.MEX.ImageResource.GetAllFiles().Length; }
        }

        /// <summary>
        /// 
        /// </summary>
        public RootNode()
        {
            Text = "root";
            ImageKey = "root";
            SelectedImageKey = "root";

            // add dummy
            Nodes.Add(new TreeNode());

            Properties = new RootProperties();

            //Core.MEX.ImageResource.RenameFile("codes.gct", "codes_poop.gct");
            //Core.MEX.ImageResource.RenameFile("codes_poop.gct", "codes_poop2.gct");
            //Core.MEX.ImageResource.RenameFile("codes_poop.gct", "DbCo.dat");
            //Core.MEX.ImageResource.DeleteFile("codes_poop2.gct");
            //Core.MEX.ImageResource.AddFile("codes.gct", new byte[4]);
            //Core.MEX.ImageResource.RenameFile("codes.gct", "codes_poop.gct");
        }
    }
}
