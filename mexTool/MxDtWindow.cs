using HSDRawViewer.GUI;
using mexTool.GUI;
using mexTool.GUI.Pages;
using mexTool.Tools;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace mexTool
{
    public partial class MxDtWindow : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// 
        /// </summary>
        public MxDtWindow()
        {
            InitializeComponent();

            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new MenuColorTable());
            foreach (ToolStripMenuItem mi in menuStrip1.Items)
            {
                mi.ForeColor = Color.White;
                foreach(var item in mi.DropDownItems)
                {
                    if (item is ToolStripMenuItem tsmi)
                        tsmi.ForeColor = Color.White;
                }
            }

            buttonFighter.FlatAppearance.MouseOverBackColor = ThemeColors.MainColorList[0];
            buttonFighter.FlatAppearance.MouseDownBackColor = ThemeColors.SecondColorList[0];

            buttonStages.FlatAppearance.MouseOverBackColor = ThemeColors.MainColorList[1];
            buttonStages.FlatAppearance.MouseDownBackColor = ThemeColors.SecondColorList[1];

            buttonMenu.FlatAppearance.MouseOverBackColor = ThemeColors.MainColorList[2];
            buttonMenu.FlatAppearance.MouseDownBackColor = ThemeColors.SecondColorList[2];

            buttonMusic.FlatAppearance.MouseOverBackColor = ThemeColors.MainColorList[3];
            buttonMusic.FlatAppearance.MouseDownBackColor = ThemeColors.SecondColorList[3];

            buttonSound.FlatAppearance.MouseOverBackColor = ThemeColors.MainColorList[4];
            buttonSound.FlatAppearance.MouseDownBackColor = ThemeColors.SecondColorList[4];

            DoubleBuffered = true;

            worker.WorkerReportsProgress = true;
            worker.DoWork += Save;

            labelGameName.Text = "";

            Application.Idle += (sender, args) =>
            {
                if (mexTool.Updater.UpdateReady)
                    buttonUpdate.Visible = true;
            };

            ThreadStart t = new ThreadStart(mexTool.Updater.CheckLatest);
            Thread thread = new Thread(t);
            thread.Start();

            // clear temp files on close
            FormClosing += (sender, args) => { Core.MEX.ImageResource?.ClearTempFiles(); };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void headerPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Maximize()
        {
            if (WindowState == FormWindowState.Maximized)
                WindowState = FormWindowState.Normal;
            else
            if (WindowState == FormWindowState.Normal)
            {
                MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                WindowState = FormWindowState.Maximized;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMinMax_Click(object sender, EventArgs e)
        {
            Maximize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void headerPanel_DoubleClick(object sender, EventArgs e)
        {
            Maximize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStrip1_MouseDoubleClick(object sender, EventArgs e)
        {
            Maximize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openISOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy)
                return;

            fileSystemToolStripMenuItem.Enabled = true;
            iSOToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;

            using (OpenFileDialog d = new OpenFileDialog())
            {
                d.Filter = "Gamecube ISO (*.iso)|*.iso";
                if (d.ShowDialog() == DialogResult.OK)
                {
                    if (openedPath == d.FileName)
                        MessageBox.Show("ISO is already opened", "Open ISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                    {
                        if (Core.MEX.InitFromISO(d.FileName))
                            FileSystemLoaded(d.FileName);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy)
                return;

            fileSystemToolStripMenuItem.Enabled = false;
            iSOToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            closeFileSystemToolStripMenuItem.Enabled = true;

            using (OpenFolderDialog d = new OpenFolderDialog())
            {
                if (d.ShowDialog() == DialogResult.OK)
                {
                    if (openedPath == d.SelectedPath)
                        MessageBox.Show("File System is already opened", "Open ISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                    {
                        if (Core.MEX.InitFromFileSystem(d.SelectedPath))
                            FileSystemLoaded(d.SelectedPath);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void FileSystemLoaded(string path)
        {
            CloseFileSystem(false);

            openedPath = path;

            var banner = Core.MEX.ImageResource.GetBanner();

            if (banner != null)
            {
                labelGameName.Text = banner.MetaData.LongName;
                pictureBoxBanner.Image = ImageTools.RGBAToBitmap(banner.GetBannerImageRGBA8(), 96, 32);
            }

            _fighterPage = new FighterPage();
            _fighterPage.Dock = DockStyle.Fill;

            _musicPage = new MusicPage();
            _musicPage.Dock = DockStyle.Fill;

            _menuPage = new MenuPage();
            _menuPage.Dock = DockStyle.Fill;

            _stagePage = new StagePage();
            _stagePage.Dock = DockStyle.Fill;

            _soundPage = new SoundPage();
            _soundPage.Dock = DockStyle.Fill;

            _fighterPage.Visible = false;
            _musicPage.Visible = false;
            _menuPage.Visible = false;
            _stagePage.Visible = false;
            _soundPage.Visible = false;

            Controls.Add(_fighterPage);
            Controls.Add(_stagePage);
            Controls.Add(_menuPage);
            Controls.Add(_musicPage);
            Controls.Add(_soundPage);

            _fighterPage.BringToFront();
            _stagePage.BringToFront();
            _menuPage.BringToFront();
            _musicPage.BringToFront();
            _soundPage.BringToFront();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CloseFileSystem(bool closeMex)
        {
            if(closeMex)
            {
                labelGameName.Text = "";
                if (pictureBoxBanner.Image != null)
                    pictureBoxBanner.Image.Dispose();
                pictureBoxBanner.Image = null;

                fileSystemToolStripMenuItem.Enabled = true;
                iSOToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
                saveToolStripMenuItem.Enabled = false;
                closeFileSystemToolStripMenuItem.Enabled = false;

                openedPath = null;
            }

            _fighterPage?.Dispose();
            _stagePage?.Dispose();
            _menuPage?.Dispose();
            _musicPage?.Dispose();
            _soundPage?.Dispose();

            _fighterPage = null;
            _stagePage = null;
            _menuPage = null;
            _musicPage = null;
            _soundPage = null;

            if (closeMex)
                Core.MEX.Close();
        }

        private FighterPage _fighterPage;
        private StagePage _stagePage;
        private MenuPage _menuPage;
        private MusicPage _musicPage;
        private SoundPage _soundPage;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageID"></param>
        private void SelectPage(int pageID)
        {
            _musicPage?.StopSound();

            buttonFighter.BackColor = Color.Transparent;
            buttonStages.BackColor = Color.Transparent;
            buttonMusic.BackColor = Color.Transparent;
            buttonSound.BackColor = Color.Transparent;
            buttonMenu.BackColor = Color.Transparent;

            if(_fighterPage != null)
            {
                _fighterPage.Visible = false;
                _musicPage.Visible = false;
                _menuPage.Visible = false;
                _stagePage.Visible = false;
                _soundPage.Visible = false;
            }

            if (pageID == 0)
            {
                if (_fighterPage != null)
                    _fighterPage.Visible = true;
                buttonFighter.BackColor = ThemeColors.SecondColorList[0];
            }
            if (pageID == 1)
            {
                if (_stagePage != null)
                    _stagePage.Visible = true;
                buttonStages.BackColor = ThemeColors.SecondColorList[1];
            }
            if (pageID == 2)
            {
                if (_menuPage != null)
                    _menuPage.Visible = true;
                buttonMenu.BackColor = ThemeColors.SecondColorList[2];
            }
            if (pageID == 3)
            {
                if (_musicPage != null)
                    _musicPage.Visible = true;
                buttonMusic.BackColor = ThemeColors.SecondColorList[3];
            }
            if (pageID == 4)
            {
                if (_soundPage != null)
                    _soundPage.Visible = true;
                buttonSound.BackColor = ThemeColors.SecondColorList[4];
            }
        }

        private void buttonFighter_Click(object sender, EventArgs e)
        {
            SelectPage(0);
        }

        private void buttonStages_Click(object sender, EventArgs e)
        {
            SelectPage(1);
        }

        private void buttonMenu_Click(object sender, EventArgs e)
        {
            SelectPage(2);
        }

        private void buttonMusic_Click(object sender, EventArgs e)
        {
            SelectPage(3);
        }

        private void buttonSound_Click(object sender, EventArgs e)
        {
            SelectPage(4);
        }


        private BackgroundWorker worker = new BackgroundWorker();

        private static ForceMode _forceMode;
        private static string rebuildPath;


        private static string openedPath;

        private enum ForceMode
        {
            Normal,
            ISO,
            FileSystem
        }

        /// <summary>
        /// 
        /// </summary>
        private void BeginSaving(ForceMode mode)
        {
            if (rebuildPath != null && openedPath == rebuildPath)
            {
                MessageBox.Show("Must create a new ISO at a different location", "Save ISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // make sure save path is not in use
            if (rebuildPath != null && !Directory.Exists(rebuildPath))
            {
                var fi = new FileInfo(rebuildPath);
                try
                {
                    using (FileStream stream = fi.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                        stream.Close();
                }
                catch (IOException)
                {
                    MessageBox.Show("File location is already in use!\nYou cannot overrwrite the current ISO and make sure the destination is not open in another program.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            savePanel.Visible = true;
            Enabled = false;
            
            _forceMode = mode;

            worker.RunWorkerAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy || !Core.MEX.Initialized)
                return;

            rebuildPath = null;

            if (Core.MEX.ImageResource.IsoNeedsRebuild)
            {
                MessageBox.Show("ISO need to be rebuilt", "Rebuild ISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                using (var d = new SaveFileDialog())
                {
                    d.Title = "Choose new iso file path";
                    d.Filter = "Gamecube ISO (*.iso)|*.iso";

                    if (d.ShowDialog() == DialogResult.OK)
                        rebuildPath = d.FileName;
                }
            }

            if (Core.MEX.ImageResource.IsoNeedsRebuild && rebuildPath == null)
                return;

            BeginSaving(ForceMode.Normal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iSOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy || !Core.MEX.Initialized)
                return;

            rebuildPath = null;

            using (var d = new SaveFileDialog())
            {
                d.Title = "Choose new image file path";
                d.Filter = "Gamecube ISO (*.iso)|*.iso";

                if (d.ShowDialog() == DialogResult.OK)
                    rebuildPath = d.FileName;
                else
                    return;
            }

            BeginSaving(ForceMode.ISO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy || !Core.MEX.Initialized)
                return;

            rebuildPath = null;

            using (var d = new OpenFolderDialog())
            {
                d.Title = "Choose output folder";

                if (d.ShowDialog() == DialogResult.OK)
                    rebuildPath = d.SelectedPath;
                else
                    return;
            }

            BeginSaving(ForceMode.FileSystem);
        }

        /// <summary>
        /// 
        /// </summary>
        private void Save(object sender, DoWorkEventArgs args)
        {
            Core.MEX.PrepareSave(ReportProgress);

            if (Core.MEX.ImageResource.SourceIsFileSystem && _forceMode == ForceMode.ISO)
            {
                // build new iso
                var dol = Core.MEX.ImageResource.GetDOL();

                using (GCILib.GCISO iso = new GCILib.GCISO(Core.MEX.ImageResource.GetBoot(), Core.MEX.ImageResource.GetBin2(), Core.MEX.ImageResource.GetAppLoader(), Core.MEX.ImageResource.GetDOL()))
                {
                    foreach(var file in Core.MEX.ImageResource.GetAllFiles())
                    {
                        //Console.WriteLine(Core.MEX.ImageResource.GetRealFilePath(file));
                        iso.AddFile(file, Core.MEX.ImageResource.GetRealFilePath(file));
                    }

                    iso.Rebuild(rebuildPath, ReportProgress);
                }
            }
            else if (Core.MEX.ImageResource.SourceIsISO && _forceMode == ForceMode.FileSystem)
            {
                // write system data
                Directory.CreateDirectory(rebuildPath + "/sys/");
                File.WriteAllBytes(Path.Combine(rebuildPath + "/sys/", "main.dol"), Core.MEX.ImageResource.GetDOL());
                File.WriteAllBytes(Path.Combine(rebuildPath + "/sys/", "apploader.img"), Core.MEX.ImageResource.GetAppLoader());
                File.WriteAllBytes(Path.Combine(rebuildPath + "/sys/", "boot.bin"), Core.MEX.ImageResource.GetBoot());
                File.WriteAllBytes(Path.Combine(rebuildPath + "/sys/", "bi2.bin"), Core.MEX.ImageResource.GetBin2());

                // write files
                var files = Core.MEX.ImageResource.GetAllFiles();
                int index = 0;
                foreach (var file in files)
                {
                    var output = rebuildPath + "/files" + file;
                    Directory.CreateDirectory(Path.GetDirectoryName(output));
                    Core.MEX.ImageResource.DumpFileFromISO(file, output);
                    //File.WriteAllBytes(output, Core.MEX.ImageResource.GetFile(file));
                    index++;
                    ReportProgress(null, new ProgressChangedEventArgs((int)((index / (float)files.Length) * 99), null));
                }

                ReportProgress(null, new ProgressChangedEventArgs(100, null));
            }
            else
            {
                Core.MEX.ImageResource.Save(ReportProgress, rebuildPath, false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ReportProgress(object sender, ProgressChangedEventArgs args)
        {
            MethodInvoker m = new MethodInvoker(() => {
                progressBarSaving.Value = args.ProgressPercentage;
                if (progressBarSaving.Value == 100)
                {
                    savePanel.Visible = false;
                    Enabled = true;
                }
                });
            progressBarSaving.Invoke(m);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeFileSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseFileSystem(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (Core.MEX.Initialized)
            {
                MessageBox.Show("Please save changes and/or close filesystem before updating.", "Update Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (Updater.UpdateReady &&
                MessageBox.Show(
                    $"Would you like to download the following update?\n{mexTool.Updater.LatestRelease.Name}\n{Updater.LatestRelease.Body.Substring(Updater.LatestRelease.Body.IndexOf("Message:"))}",
                    "m-ex Tool Updater", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                RunUpdater();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void RunUpdater()
        {
            File.Delete("old_mexUpdater.exe");
            File.Delete("old_mexUpdater.exe.config");

            File.Move("mexUpdater.exe", "old_mexUpdater.exe");
            File.Move("mexUpdater.exe.config", "old_mexUpdater.exe.config");

            Process p = new Process();
            p.StartInfo.FileName = Path.Combine(ApplicationSettings.ExecutablePath, "old_mexUpdater.exe");
            p.StartInfo.Arguments = $"{Updater.DownloadURL} \"{Updater.Version}\" -r";
            p.Start();

            Application.Exit();
        }
    }
}
