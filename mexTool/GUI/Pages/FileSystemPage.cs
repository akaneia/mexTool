using mexTool.GUI.FileSystem;
using System;
using System.Windows.Forms;

namespace mexTool.GUI.Pages
{
    public partial class FileSystemPage : UserControl
    {
        private ContextMenu _fileContextMenu;
        private ContextMenu _folderContextMenu;

        public FileSystemPage()
        {
            InitializeComponent();

            ImageList myImageList = new ImageList();
            myImageList.ImageSize = new System.Drawing.Size(24, 24);
            myImageList.Images.Add("root", Properties.Resources.ico_disc);
            myImageList.Images.Add("folder", Properties.Resources.ico_folder);
            myImageList.Images.Add("file", Properties.Resources.ico_file);

            treeView1.ImageList = myImageList;

            treeView1.Nodes.Add(new RootNode());


            treeView1.BeforeExpand += (sender, args) =>
            {

            };

            treeView1.AfterExpand += (sender, args) =>
            {
                args.Node.Nodes.Clear();
                treeView1.BeginUpdate();
                if (args.Node is NodeBase node)
                {
                    node.ExpandData();
                }
                treeView1.EndUpdate();
            };

            treeView1.AfterCollapse += (sender, args) =>
            {
                treeView1.BeginUpdate();
                args.Node.Nodes.Clear();
                args.Node.Nodes.Add(new TreeNode());
                treeView1.EndUpdate();
            };

            treeView1.NodeMouseClick += (sender, args) =>
            {
                mxPropertyGrid1.SelectedObject = null;

                if (args.Node is NodeBase node)
                    mxPropertyGrid1.SelectedObject = node.Properties;

                treeView1.SelectedNode = treeView1.GetNodeAt(args.Location);

                if (args.Button == MouseButtons.Right && args.Node != null)
                {
                    if (args.Node is FileNode file)
                        _fileContextMenu.Show(this, args.Location);

                    if (args.Node is FolderNode folder)
                    {
                        _folderContextMenu.MenuItems[2].Visible = !(args.Node is RootNode);
                        _folderContextMenu.Show(this, args.Location);
                    }
                }
            };

            CreateFileContextMenu();
            CreateFolderContextMenu();

            Disposed += (s, a) =>
            {
                _fileContextMenu?.Dispose();
                _folderContextMenu?.Dispose();
            };
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateFileContextMenu()
        {
            if (_fileContextMenu != null)
                return;

            _fileContextMenu = new ContextMenu();

            var fileImport = new MenuItem("Import");
            fileImport.Click += (s, a) =>
            {
                if (treeView1.SelectedNode is FileNode node)
                    using (OpenFileDialog d = new OpenFileDialog())
                    {
                        d.FileName = node.Text;
                        if (d.ShowDialog() == DialogResult.OK)
                            Core.MEX.ImageResource.AddFile(node.FullImagePath, d.FileName);
                    }
            };
            _fileContextMenu.MenuItems.Add(fileImport);

            var fileExport = new MenuItem("Export");
            fileExport.Click += (s, a) =>
            {
                if (treeView1.SelectedNode is FileNode node)
                    using (SaveFileDialog d = new SaveFileDialog())
                    {
                        d.FileName = node.Text;
                        if (d.ShowDialog() == DialogResult.OK)
                            System.IO.File.WriteAllBytes(d.FileName, Core.MEX.ImageResource.GetFileData(node.FullImagePath));
                    }
            };
            _fileContextMenu.MenuItems.Add(fileExport);

            var fileDelete = new MenuItem("Delete");
            fileDelete.Click += (s, a) =>
            {
                if (treeView1.SelectedNode is FileNode node &&
                    MessageBox.Show($"Are you sure you want to delete {node.Text}?", "Delete File", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Core.MEX.ImageResource.DeleteFile(node.FullImagePath);
                    node.Parent.Nodes.Remove(node);
                    mxPropertyGrid1.SelectedObject = null;
                }
            };
            _fileContextMenu.MenuItems.Add(fileDelete);
        }



        /// <summary>
        /// 
        /// </summary>
        private void CreateFolderContextMenu()
        {
            if (_folderContextMenu != null)
                return;

            _folderContextMenu = new ContextMenu();

            var fileImport = new MenuItem("Import File");
            fileImport.Click += (s, a) =>
            {
                if (treeView1.SelectedNode is FolderNode node)
                    using (OpenFileDialog d = new OpenFileDialog())
                    {
                        if (d.ShowDialog() == DialogResult.OK)
                            node.ImportFile(d.FileName);
                    }
            };
            _folderContextMenu.MenuItems.Add(fileImport);

            var fileExport = new MenuItem("Export Folder");
            fileExport.Click += (s, a) =>
            {
                if (treeView1.SelectedNode is FolderNode node)
                    using (OpenFolderDialog d = new OpenFolderDialog())
                    {
                        if (d.ShowDialog() == DialogResult.OK)
                        {
                            node.Export(d.SelectedPath);
                        }
                    }
            };
            _folderContextMenu.MenuItems.Add(fileExport);

            var fileDelete = new MenuItem("Delete Folder");
            fileDelete.Click += (s, a) =>
            {
                if (treeView1.SelectedNode is FolderNode node &&
                    MessageBox.Show($"Are you sure you want to delete {node.Text}?", "Delete Folder", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // delete folder
                    node.Delete();

                    // clear property grid
                    mxPropertyGrid1.SelectedObject = null;
                }
            };
            _folderContextMenu.MenuItems.Add(fileDelete);
        }

        private void FileSystemPage_Load(object sender, EventArgs e)
        {

        }
    }
}
