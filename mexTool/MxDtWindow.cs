using HSDRawViewer.GUI;
using mexTool.Core.FileSystem;
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

            Maximize();

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

            buttonCodes.FlatAppearance.MouseOverBackColor = ThemeColors.MainColorList[5];
            buttonCodes.FlatAppearance.MouseDownBackColor = ThemeColors.SecondColorList[5];

            buttonFileSystem.FlatAppearance.MouseOverBackColor = ThemeColors.MainColorList[6];
            buttonFileSystem.FlatAppearance.MouseDownBackColor = ThemeColors.SecondColorList[6];

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
            FormClosing += (sender, args) => { Core.MEX.ImageResource?.DeleteTempFiles(); };
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
                        {
                            FileSystemLoaded(d.FileName);
                        }
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

            using (OpenFolderDialog d = new OpenFolderDialog())
            {
                if (d.ShowDialog() == DialogResult.OK)
                {
                    if (openedPath == d.SelectedPath)
                        MessageBox.Show("File System is already opened", "Open ISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                    {
                        if (Core.MEX.InitFromFileSystem(d.SelectedPath))
                        {
                            FileSystemLoaded(d.SelectedPath);
                        }
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

            fileSystemToolStripMenuItem.Enabled = true;
            iSOToolStripMenuItem.Enabled = true;
            closeFileSystemToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;

            openedPath = path;

            RefreshBanner();

            _fileSystemPage = new FileSystemPage();
            _fileSystemPage.Dock = DockStyle.Fill;

            _codesPage = new CodesPage();
            _codesPage.Dock = DockStyle.Fill;

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

            _fileSystemPage.Visible = false;
            _codesPage.Visible = false;
            _fighterPage.Visible = false;
            _musicPage.Visible = false;
            _menuPage.Visible = false;
            _stagePage.Visible = false;
            _soundPage.Visible = false;

            Controls.Add(_fileSystemPage);
            Controls.Add(_codesPage);
            Controls.Add(_fighterPage);
            Controls.Add(_stagePage);
            Controls.Add(_menuPage);
            Controls.Add(_musicPage);
            Controls.Add(_soundPage);

            _fileSystemPage.BringToFront();
            _codesPage.BringToFront();
            _fighterPage.BringToFront();
            _stagePage.BringToFront();
            _menuPage.BringToFront();
            _musicPage.BringToFront();
            _soundPage.BringToFront();

            //CheckCodeUpdate();
        }

        /// <summary>
        /// 
        /// </summary>
        private void RefreshBanner()
        {
            var banner = Core.MEX.ImageResource.GetBanner();

            if (banner != null)
            {
                labelGameName.Text = banner.MetaData.LongName;

                if (pictureBoxBanner.Image != null)
                    pictureBoxBanner.Image.Dispose();

                pictureBoxBanner.Image = ImageTools.RGBAToBitmap(banner.GetBannerImageRGBA8(), 96, 32);
            }
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

            _fileSystemPage?.Dispose();
            _codesPage?.Dispose();
            _fighterPage?.Dispose();
            _stagePage?.Dispose();
            _menuPage?.Dispose();
            _musicPage?.Dispose();
            _soundPage?.Dispose();

            _fileSystemPage = null;
            _codesPage = null;
            _fighterPage = null;
            _stagePage = null;
            _menuPage = null;
            _musicPage = null;
            _soundPage = null;

            if (closeMex)
                Core.MEX.Close();
        }

        private FileSystemPage _fileSystemPage;
        private CodesPage _codesPage;
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

            buttonFileSystem.BackColor = Color.Transparent;
            buttonCodes.BackColor = Color.Transparent;
            buttonFighter.BackColor = Color.Transparent;
            buttonStages.BackColor = Color.Transparent;
            buttonMusic.BackColor = Color.Transparent;
            buttonSound.BackColor = Color.Transparent;
            buttonMenu.BackColor = Color.Transparent;

            if(_fighterPage != null)
            {
                _fileSystemPage.Visible = false;
                _codesPage.Visible = false;
                _fighterPage.Visible = false;
                _musicPage.Visible = false;
                _menuPage.Visible = false;
                _stagePage.Visible = false;
                _soundPage.Visible = false;
            }

            if (pageID == 0)
            {
                if (_fileSystemPage != null)
                    _fileSystemPage.Visible = true;
                buttonFileSystem.BackColor = ThemeColors.SecondColorList[6];
            }
            if (pageID == 1)
            {
                if (_fighterPage != null)
                    _fighterPage.Visible = true;
                buttonFighter.BackColor = ThemeColors.SecondColorList[0];
            }
            if (pageID == 2)
            {
                if (_stagePage != null)
                    _stagePage.Visible = true;
                buttonStages.BackColor = ThemeColors.SecondColorList[1];
            }
            if (pageID == 3)
            {
                if (_menuPage != null)
                    _menuPage.Visible = true;
                buttonMenu.BackColor = ThemeColors.SecondColorList[2];
            }
            if (pageID == 4)
            {
                if (_musicPage != null)
                    _musicPage.Visible = true;
                buttonMusic.BackColor = ThemeColors.SecondColorList[3];
            }
            if (pageID == 5)
            {
                if (_soundPage != null)
                    _soundPage.Visible = true;
                buttonSound.BackColor = ThemeColors.SecondColorList[4];
            }
            if (pageID == 6)
            {
                if (_codesPage != null)
                    _codesPage.Visible = true;
                buttonCodes.BackColor = ThemeColors.SecondColorList[5];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFileSystem_Click(object sender, EventArgs e)
        {
            SelectPage(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFighter_Click(object sender, EventArgs e)
        {
            SelectPage(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStages_Click(object sender, EventArgs e)
        {
            SelectPage(2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMenu_Click(object sender, EventArgs e)
        {
            SelectPage(3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMusic_Click(object sender, EventArgs e)
        {
            SelectPage(4);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSound_Click(object sender, EventArgs e)
        {
            SelectPage(5);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCodes_Click(object sender, EventArgs e)
        {
            SelectPage(6);
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
                    MessageBox.Show("File location is already in use!\nYou cannot overwrite the current ISO and make sure the destination is not open in another program.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                Core.MEX.PrepareSave(ReportProgress);

                string[] failed = null;
                _codesPage?.SaveCodes(out failed);
                if (failed != null && failed.Length > 0)
                {
                    MessageBox.Show(string.Join("\n", failed), "Failed to add following code(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (Core.MEX.ImageResource.SourceType == typeof(FS_Extracted) && _forceMode == ForceMode.ISO)
                {
                    // build new iso
                    var dol = Core.MEX.ImageResource.GetDOL();

                    using (GCILib.GCISO iso = new GCILib.GCISO(Core.MEX.ImageResource.GetBoot(), Core.MEX.ImageResource.GetBin2(), Core.MEX.ImageResource.GetAppLoader(), Core.MEX.ImageResource.GetDOL()))
                    {
                        // put files in specific order
                        var file_order = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib\\file_order.txt");
                        if (File.Exists(file_order))
                        {
                            var filelist = File.ReadAllLines(file_order);
                            iso.SetAddressTable(filelist);
                        }
                        
                        // add files to iso
                        foreach (var file in Core.MEX.ImageResource.GetAllFiles())
                        {
                            //Console.WriteLine(Core.MEX.ImageResource.GetRealFilePath(file));
                            iso.AddFile(file, Core.MEX.ImageResource.GetRealFilePath(file));
                        }

                        // rebuild iso
                        iso.Rebuild(rebuildPath, ReportProgress);
                    }
                }
                else if (Core.MEX.ImageResource.SourceType == typeof(FS_ISO) && _forceMode == ForceMode.FileSystem)
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
                        var output = rebuildPath + "/files/" + file;
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
            catch (Exception e)
            {
                ReportProgress(null, new ProgressChangedEventArgs(100, e.Message));
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

                    if(args.UserState is string str && !string.IsNullOrEmpty(str))
                    {
                        MessageBox.Show(str, "Error Saving Filesystem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Updater.UpdateCodes())
            {
                MessageBox.Show("Codes updated!", "Update Codes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (_codesPage != null)
                    _codesPage.InitMEXCodes();

                if (Core.MEX.Initialized)
                    CheckCodeUpdate();
            }
            else
            {
                MessageBox.Show("Codes are up to date!", "Update Codes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private bool CheckCodeUpdate()
        {
            var dolPatchPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib/dol.patch");
            Console.WriteLine(Core.Installer.MEXDolPatcher.CheckPatchApplied(Core.MEX.ImageResource.GetDOL(), dolPatchPath));
            if (!Core.Installer.MEXDolPatcher.CheckPatchApplied(Core.MEX.ImageResource.GetDOL(), dolPatchPath))
            {
                // update dol
                if (MessageBox.Show("Update DOL file?\nRecommended", "DOL update detected", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Core.MEX.ImageResource.SetDOL(Core.Installer.MEXDolPatcher.ApplyPatch(Core.MEX.ImageResource.GetDOL(), dolPatchPath));
                }
            }

            var codesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"lib\codes.gct");

            if (!File.Exists(codesPath))
                return false;


            var mx_codes = File.ReadAllBytes(codesPath);
            var fs_codes = Core.MEX.ImageResource.GetFileData("codes.gct");
            if (fs_codes != null)
            {
                var mxhash = HashGen.ComputeSHA256Hash(mx_codes);
                var hash = HashGen.ComputeSHA256Hash(fs_codes);

                if (!mxhash.Equals(hash))
                {
                    // update codes.gct
                    if (MessageBox.Show("Update codes file?", "Codes update detected", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Core.MEX.ImageResource.AddFile("codes.gct", mx_codes);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var abount = new About())
                abount.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cSPCompressorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Core.MEX.Initialized)
                return;

            using (GUI.Controls.CSPCompressorGUI comp = new GUI.Controls.CSPCompressorGUI())
            {
                comp.ShowDialog();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editBannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenEditBanner();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxBanner_Click(object sender, EventArgs e)
        {
            OpenEditBanner();
        }

        /// <summary>
        /// 
        /// </summary>
        private void OpenEditBanner()
        {
            if (!Core.MEX.Initialized)
                return;

            var banner = Core.MEX.ImageResource.GetBanner();

            if (banner != null)
                using (BannerEditor edit = new BannerEditor())
                {
                    edit.SetBanner(banner);
                    edit.SetBoot(Core.MEX.ImageResource.GetBoot());
                    if (edit.ShowDialog() == DialogResult.OK)
                    {
                        Core.MEX.ImageResource.AddFile("opening.bnr", edit.GetBanner().GetData());
                        Core.MEX.ImageResource.SetBoot(edit.GetBoot());
                        RefreshBanner();
                    }
                }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void verifyISOContentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Core.MEX.Initialized)
                return;

            var file_order = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib\\file_order.txt");

            if (File.Exists(file_order))
            {
                var filelist = File.ReadAllLines(file_order);
                var files = Core.MEX.ImageResource.GetAllFiles();
                var hash = new System.Collections.Generic.HashSet<string>();
                foreach (var v in files)
                    hash.Add(v);

                var missing = new System.Collections.Generic.List<string>();
                foreach (var v in filelist)
                {
                    if (!hash.Contains(v.Substring(1).Replace("/", "\\")))
                    {
                        missing.Add(v);
                    }
                }

                if (missing.Count > 0)
                    MessageBox.Show("Missing File(s):\n" + String.Join("\n", missing), "Verify ISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("No missing files detected!", "Verify ISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importCSPsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Core.MEX.Initialized)
                return;

            using (var d = new OpenFolderDialog())
            {
                if (d.ShowDialog() == DialogResult.OK)
                {
                    var files = Directory.GetFiles(d.SelectedPath);

                    int toProcess = files.Length;
                    using (ManualResetEvent resetEvent = new ManualResetEvent(false))
                    {
                        foreach (var f in Directory.GetFiles(d.SelectedPath))
                        {
                            if (Path.GetExtension(f) == ".png")
                            {
                                var costume = Path.GetFileNameWithoutExtension(f).Replace("csp_", "");

                                foreach (var fi in Core.MEX.Fighters)
                                {
                                    foreach (var c in fi.Costumes)
                                    {
                                        if (c.FileName.StartsWith(costume))
                                        {
                                            ThreadPool.QueueUserWorkItem(
                                               new WaitCallback(x =>
                                               {
                                                   using (var bmp = new Bitmap(f))
                                                       c.CSP = bmp.ToTOBJ(HSDRaw.GX.GXTexFmt.CI8, HSDRaw.GX.GXTlutFmt.RGB5A3);

                                                   if (Interlocked.Decrement(ref toProcess) == 0)
                                                       resetEvent.Set();

                                               }));
                                        }
                                    }
                                }
                            }
                        }

                        resetEvent.WaitOne();
                    }


                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportCSPsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Core.MEX.Initialized)
                return;

            using (var d = new OpenFolderDialog())
            {
                if (d.ShowDialog() == DialogResult.OK)
                {
                    var path = d.SelectedPath;

                    foreach (var f in Core.MEX.Fighters)
                    {
                        foreach (var c in f.Costumes)
                        {
                            // skip game and watch
                            if (c.FileName.Contains("PlGwNr"))
                                continue;

                            var export_name = path + "\\" + Path.GetFileNameWithoutExtension(c.FileName) + ".png";

                            using (var b = c.CSP.ToBitmap())
                            {
                                if (b != null)
                                    b.Save(export_name);
                            }
                        }
                    }
                }
            }
        }
    }
}
