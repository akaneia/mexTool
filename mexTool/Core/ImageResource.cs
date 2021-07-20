using GCILib;
using mexTool.Core.FileSystem;
using mexTool.Core.Installer;
using System;
using System.ComponentModel;
using System.IO;
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

            return TryInstallMex(_fileSystem, folderPath);
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
                if(MessageBox.Show("Vanilla melee image detected.\nInstall m-ex system?", "Install m-ex?", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    return MEXInstaller.InstallMEX(this);
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
            return fs.FileExists("MxDt.dat") && IsMeleeISO(fs);
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
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool FileExists(string filePath)
        {
            if (_fileSystem == null)
                return false;

            if (_tempManager.FileIsRemoved(filePath))
                return false;

            return _fileSystem.FileExists(_tempManager.GetRenamedFile(filePath)) || _tempManager.FileExists(filePath);
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

            return _fileSystem.GetBanner();
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
        public string[] GetAllFiles()
        {
            if (_fileSystem == null)
                return new string[0];

            return _fileSystem.GetFileList();
        }

        /// <summary>
        /// Fasters than load files into ram then dumping
        /// </summary>
        public void DumpFileFromISO(string internalPath, string filePath)
        {
            if (_fileSystem is FS_ISO iso_fs)
            {
                if (iso_fs.FileExists(internalPath))
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
    }
}
