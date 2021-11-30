using System.Windows.Forms;

namespace mexTool.GUI.FileSystem
{
    public class FolderNode : NodeBase
    {
        public class FolderProperties
        {

        }

        public FolderNode() : this("New Folder")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public FolderNode(string folder_name)
        {
            Text = folder_name;
            ImageKey = "folder";
            SelectedImageKey = "folder";

            // add dummy
            Nodes.Add(new TreeNode());

            Properties = new FolderProperties();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void ExpandData()
        {
            if (!Core.MEX.Initialized)
                return;

            var folders = Core.MEX.ImageResource.GetFoldersInDirectory(FullImagePath);

            foreach (var f in folders)
                Nodes.Add(new FolderNode(System.IO.Path.GetFileName(f)));

            var files = Core.MEX.ImageResource.GetFilesInDirectory(FullImagePath);

            foreach (var f in files)
            {
                var fn = new FileNode(System.IO.Path.GetFileName(f));
                Nodes.Add(fn);
                if (Core.MEX.ImageResource.IsAddedFile(f))
                    fn.ForeColor = System.Drawing.Color.LightGreen;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ImportFile(string filePath)
        {
            var fileName = System.IO.Path.GetFileName(filePath);
            var fullPath = System.IO.Path.Combine(FullImagePath.Length > 0 ? FullImagePath + "\\" : "", fileName);

            if (Core.MEX.ImageResource.AddFile(fullPath, filePath))
            {
                Collapse();
                Expand();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void Export(string path)
        {
            foreach (var f in Core.MEX.ImageResource.GetFilesInDirectory(FullImagePath, includeSubdirectories: true))
            {
                var fp = System.IO.Path.Combine(path + "/", f);

                var dir = System.IO.Path.GetDirectoryName(fp);
                if (!System.IO.Directory.Exists(dir))
                    System.IO.Directory.CreateDirectory(dir);

                System.IO.File.WriteAllBytes(fp, Core.MEX.ImageResource.GetFileData(f));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Delete()
        {
            // remove all files
            foreach (var f in Core.MEX.ImageResource.GetFilesInDirectory(FullImagePath, includeSubdirectories: true))
                Core.MEX.ImageResource.DeleteFile(f);

            //// remove folder node
            Parent.Nodes.Remove(this);
        }
    }
}
