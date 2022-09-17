using GCILib;
using HSDRaw;
using mexTool.Core.FileSystem;
using mexTool.Core.Installer;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace mexTool.Core
{
    public class ImageResource
    {
        /// <summary>
        /// File System interface
        /// </summary>
        private IFS _fileSystem;

        /// <summary>
        /// temp file manager
        /// </summary>
        private TempFileManager _tempManager = new TempFileManager();

        /// <summary>
        /// 
        /// </summary>
        public Type SourceType { get => _fileSystem == null ? null : _fileSystem.GetType(); }

        /// <summary>
        /// 
        /// </summary>
        public bool OpenISO(string isoPath)
        {
            Close();

            _fileSystem = new FS_ISO();

            return TryInstallMex(_fileSystem, isoPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public bool OpenFolder(string folderPath)
        {
            Close();

            _fileSystem = new FS_Extracted();

            if (TryInstallMex(_fileSystem, folderPath))
            {
                if (!IsMexISO(_fileSystem))
                    return false;

                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool TryInstallMex(IFS fs, string filepath)
        {
            if (!fs.TryOpen(filepath))
                return false;

            if (IsMexISO(fs))
                return true;

            if (IsMeleeISO(fs))
            {
                if (IsMeleeVersion120(fs, out string version))
                {
                    if (MessageBox.Show("Vanilla melee image detected.\nInstall m-ex system?", "Install m-ex?", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                    {
                        return MEXInstaller.InstallMEX(this);
                    }
                }
                else
                {
                    MessageBox.Show($"Error: Only version 1.02 of Melee is currently supported\nFound: {version}\nExpected: 2001/11/14", "Version Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            Close();
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void DeleteTempFiles()
        {
            _tempManager.DeleteTempFiles();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Save(ProgressChangedEventHandler progress, string filePath, bool saveAs)
        {
            if (_fileSystem == null)
                return;

            _fileSystem.Save(progress, filePath, saveAs, _tempManager);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            _fileSystem?.Close();
        }

        /// <summary>
        /// Returns true if this iso appears to contain a 
        /// melee filesystem with m-ex installed
        /// </summary>
        /// <returns></returns>
        private bool IsMexISO(IFS fs)
        {
            // MxDt exists
            return fs.FileExists("MxDt.dat") && fs.FileExists("codes.gct") && IsMeleeISO(fs);
        }

        /// <summary>
        /// Returns true if the file system appears to be from melee
        /// Determines by checking the existance of key files
        /// </summary>
        /// <returns></returns>
        private bool IsMeleeISO(IFS fs)
        {
            return fs.FileExists("IfAll.usd") &&
                fs.FileExists("PlCo.dat") &&
                fs.FileExists("SmSt.dat") &&
                fs.FileExists("audio/smash2.sem") &&
                fs.FileExists("audio/us/smash2.sem") &&
                fs.FileExists("MnSlChr.usd") &&
                fs.FileExists("MnSlMap.usd");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        private bool IsMeleeVersion120(IFS fs, out string version)
        {
            using (MemoryStream stream = new MemoryStream(fs.GetAppLoader()))
            using (BinaryReaderExt f = new BinaryReaderExt(stream))
            {
                version = f.ReadString(0, -1);

                return version.Equals("2001/11/14");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool FileExists(string filePath)
        {
            if (_fileSystem == null)
                return false;

            //if (_tempManager.FileIsRemoved(filePath))
            //    return false;
            //_fileSystem.FileExists(_tempManager.GetRenamedFile(filePath)) || 

            return _tempManager.FileExists(_fileSystem.GetFileList(), filePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public byte[] GetFileData(string path)
        {
            var tempData = _tempManager.GetFileData(path);

            if (tempData != null)
                return tempData;

            if (_fileSystem == null)
                return null;

            return _fileSystem.GetFileData(_tempManager.GetRenamedFile(path));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public uint GetFileSize(string path)
        {
            if (_fileSystem == null)
                return 0;

            if (!FileExists(path))
                return 0;

            var tempData = _tempManager.GetFileSize(path);

            if (tempData != 0)
                return tempData;

            return _fileSystem.GetFileSize(_tempManager.GetRenamedFile(path));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool AddFile(string filePath, byte[] data)
        {
            return _tempManager.AddFile(filePath, data);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool AddFile(string filePath, string diskFilePath)
        {
            return _tempManager.AddFile(filePath, diskFilePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public void DeleteFile(string filePath)
        {
            _tempManager.DeleteFile(filePath);
        }

        /// <summary>
        /// Marks file for renaming
        /// Renaming does not occur until image is saved
        /// </summary>
        public bool RenameFile(string src, string dest)
        {
            if (FileExists(dest))
                return false;

            if (!FileExists(src))
                return false;

            return _tempManager.RenameFile(src, dest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GCBanner GetBanner()
        {
            if (_fileSystem == null)
                return null;

            if (FileExists("opening.bnr"))
                return new GCBanner(GetFileData("opening.bnr"));
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetDOL()
        {
            if (_fileSystem == null)
                return null;

            return _fileSystem.GetDOL();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void SetDOL(byte[] dol)
        {
            if (_fileSystem == null)
                return;

            _fileSystem.SetDOL(dol);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetAppLoader()
        {
            if (_fileSystem == null)
                return null;

            return _fileSystem.GetAppLoader();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetBin2()
        {
            if (_fileSystem == null)
                return null;

            return _fileSystem.GetBin2();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetBoot()
        {
            if (_fileSystem == null)
                return null;

            return _fileSystem.GetBoot();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void SetBoot(byte[] boot)
        {
            if (boot == null)
                return;

            _fileSystem.SetBoot(boot);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetAllFiles()
        {
            if (_fileSystem == null)
                return new string[0];

            return _tempManager.GetFileList(_fileSystem.GetFileList()).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public string[] GetFilesInDirectory(string directory, bool sorted = true, bool includeSubdirectories = false)
        {
            // check temp files
            var fs =
                includeSubdirectories ? 
                _tempManager.GetFileList(_fileSystem.GetFileList()).Where(e=>System.IO.Path.GetDirectoryName(e).StartsWith(directory))
                :
                _tempManager.GetFileList(_fileSystem.GetFileList()).Where(e => System.IO.Path.GetDirectoryName(e).Equals(directory))
                ;

            if (sorted)
                fs = fs.OrderBy(s => s);

            return fs.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public string[] GetFoldersInDirectory(string directory, bool sorted = true)
        {
            // this is super ugly
            var fs = _tempManager.GetFileList(_fileSystem.GetFileList())
                .GroupBy(x => Path.GetDirectoryName(x))
                .Select(g => Path.GetDirectoryName(g.First()))
                .Where(e => !string.IsNullOrEmpty(e) && Path.GetDirectoryName(e) != null && Path.GetDirectoryName(e).Equals(directory));

            if (sorted)
                fs = fs.OrderBy(s => s);

            return fs.ToArray();
        }

        /// <summary>
        /// Fasters than load files into ram then dumping
        /// </summary>
        public void DumpFileFromISO(string internalPath, string filePath)
        {
            if (_fileSystem is FS_ISO iso_fs)
            {
                if (iso_fs.FileExists(internalPath) && !IsAddedFile(internalPath))
                {
                    iso_fs.DumpFileFromISO(internalPath, filePath);
                }
                else
                {
                    File.WriteAllBytes(filePath, GetFileData(internalPath));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetRealFilePath(string path)
        {
            if (SourceType == typeof(FS_Extracted))
                return _tempManager.GetRealFilePath(path, (FS_Extracted)_fileSystem);

            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsAddedFile(string path)
        {
            return _tempManager.IsNewFile(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetAddressOrder()
        {
            if (_fileSystem == null)
                return null;

            if (_fileSystem is FS_Extracted ext)
                return ext.GetAddressOrder();

            return null;
        }
    }
}
