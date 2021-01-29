using GCILib;
using mexTool.Core.Installer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace mexTool.Core
{
    public class ImageResource
    {
        public bool Initialized { get; internal set; } = false;

        public bool SourceIsISO { get => _iso != null; }

        public bool SourceIsFileSystem { get => _folderPath != null; }

        private GCISO _iso;

        private string _folderPath;

        private Dictionary<string, string> filesToRename = new Dictionary<string, string>();
        private Dictionary<string, string> filestoAdd = new Dictionary<string, string>();
        private List<string> filestoRemove = new List<string>();

        public bool IsoNeedsRebuild { get => _iso != null && _iso.NeedsRebuild; }

        /// <summary>
        /// 
        /// </summary>
        public bool OpenISO(string isoPath)
        {
            Close();
            _iso = new GCISO(isoPath);
            return TryInit();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool OpenFolder(string filePath)
        {
            Close();
            _folderPath = filePath;
            return TryInit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool TryInit()
        {
            System.Diagnostics.Debug.WriteLine(IsMexISO() + " " + IsMeleeISO());
            if (IsMexISO())
            {
                return true;
            }
            else
            if (IsMeleeISO())
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
        public void ClearTempFiles()
        {
            foreach (var v in tempPaths)
                File.Delete(v);
            tempPaths.Clear();
        }


        /// <summary>
        /// 
        /// </summary>
        public void Save(ProgressChangedEventHandler progress, string filePath, bool saveAs)
        {
            if (_iso != null)
            {
                if (filePath != null)
                    _iso.Rebuild(filePath, progress);
                else
                    _iso.Save(progress);

                ClearTempFiles();
            }

            if(_folderPath != null)
            {
                Dictionary<string, string> tempToNew = new Dictionary<string, string>();

                // add files
                foreach (var f in filestoAdd)
                {
                    var realPath = GetFolderPath(f.Key);

                    if (f.Value == realPath)
                        continue;

                    if (!File.Exists(f.Value))
                        continue;

                    if (File.Exists(realPath))
                        File.Delete(realPath);

                    File.Move(f.Value, realPath);
                }
                progress.Invoke(null, new ProgressChangedEventArgs(25, null));

                // rename files
                foreach (var f in filesToRename)
                {
                    if(File.Exists(f.Key))
                    {
                        var temp = GenerateTempFile(f.Key);
                        tempToNew.Add(temp, GetFolderPath(f.Value));
                        File.Move(GetFolderPath(f.Key), temp);
                    }
                }
                foreach (var f in tempToNew)
                {
                    if(!File.Exists(f.Value))
                        File.Move(f.Key, f.Value);
                }
                progress.Invoke(null, new ProgressChangedEventArgs(50, null));

                // delete files
                foreach (var v in filestoRemove)
                    File.Delete(GetFolderPath(v));

                progress.Invoke(null, new ProgressChangedEventArgs(75, null));

                // reset
                filesToRename.Clear();
                filestoAdd.Clear();
                filestoRemove.Clear();

                ClearTempFiles();

                progress.Invoke(null, new ProgressChangedEventArgs(100, null));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        private string GetFolderPath(string rootPath)
        {
            return Path.Combine(_folderPath + "\\files", rootPath);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            _folderPath = null;
            if (_iso != null)
                _iso.Dispose();
            _iso = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsMexISO()
        {
            // MxDt exists
            return FileExists("MxDt.dat") && IsMeleeISO();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsMeleeISO()
        {
            return FileExists("IfAll.usd") &&
                FileExists("PlCo.dat") &&
                FileExists("SmSt.dat") &&
                FileExists("audio/smash2.sem") && 
                FileExists("audio/us/smash2.sem") &&
                FileExists("MnSlChr.usd") &&
                FileExists("MnSlMap.usd");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool FileExists(string filePath)
        {
            if (_iso != null)
                return _iso.FileExists(filePath);

            if (_folderPath != null && filePath != null)
            {
                if (filestoRemove.Contains(filePath))
                    return true;
                else
                if (filestoAdd.ContainsKey(filePath))
                    return true;
                else
                if(!filesToRename.ContainsKey(filePath))
                    return File.Exists(Path.Combine(_folderPath + "\\files", filePath));
                else
                    return filesToRename.Values.Contains(filePath);

            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public byte[] GetFile(string path)
        {
            if (_iso != null)
                return _iso.GetFileData(path);

            if (_folderPath != null && FileExists(path))
            {
                if (filestoAdd.ContainsKey(path))
                    return File.ReadAllBytes(filestoAdd[path]);
                else
                if (!filesToRename.ContainsKey(path))
                    return File.ReadAllBytes(Path.Combine(_folderPath + "\\files", path));
                else
                    return File.ReadAllBytes(Path.Combine(_folderPath + "\\files", filesToRename[path]));
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetRealFilePath(string path)
        {
            if (_folderPath != null)
            {
                if (filestoAdd.ContainsKey(path))
                    return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filestoAdd[path]);
                else
                if (filesToRename.ContainsKey(path))
                    return filesToRename[path];
                else
                    return Path.Combine(_folderPath + "\\files\\", path);
            }

            return "";
        }

        private List<string> tempPaths = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GenerateTempFile(string sourceName)
        {
            var tempdir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp\\");

            var fname = Path.GetFileName(sourceName);
            int index = 0;

            while (File.Exists(Path.Combine(tempdir, fname + index)))
                index++;

            fname = Path.Combine(tempdir, fname + index);

            tempdir = Path.GetDirectoryName(fname);

            if (!Directory.Exists(tempdir))
                Directory.CreateDirectory(tempdir);

            tempPaths.Add(fname);

            return fname;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool AddFile(string filePath, byte[] data)
        {
            var temp = GenerateTempFile(filePath);

            File.WriteAllBytes(temp, data);

            return AddFileTempPath(filePath, temp);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool AddFile(string filePath, string diskFilePath)
        {
            // copy to temp folder
            var temp = GenerateTempFile(diskFilePath);

            File.Copy(diskFilePath, temp);

            return AddFileTempPath(filePath, temp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="diskFilePath"></param>
        /// <returns></returns>
        private bool AddFileTempPath(string filePath, string diskFilePath)
        {
            if (_iso != null)
            {
                return _iso.AddFile(filePath, diskFilePath);
            }

            if (_folderPath != null)
            {
                if (filestoAdd.ContainsKey(filePath))
                    filestoAdd[filePath] = diskFilePath;
                else
                    filestoAdd.Add(filePath, diskFilePath);

                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public void DeleteFile(string filePath)
        {
            if (_iso != null)
                _iso.DeleteFileOrFolder(filePath);

            if (_folderPath != null)
            {
                filestoAdd.Remove(filePath);

                if(filesToRename.Values.Contains(filePath))
                {
                    var item = filesToRename.First(kvp => kvp.Value == filePath);
                    filesToRename.Remove(item.Key);
                    //filePath = item.Key;
                }

                if (FileExists(filePath) && !filestoRemove.Contains(filePath))
                    filestoRemove.Add(filePath);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GCBanner GetBanner()
        {
            if (_iso != null)
                return _iso.FindBanner();

            if (_folderPath != null)
                return new GCBanner(File.ReadAllBytes(Path.Combine(_folderPath + "\\files", "opening.bnr")));

            return null;
        }

        /// <summary>
        /// Marks file for renaming
        /// Renaming does not occur until image is saved
        /// </summary>
        public bool RenameFile(string src, string dest)
        {
            if (_iso != null)
                return _iso.RenameFile(src, dest);

            if (_folderPath != null)
            {
                var keys = filesToRename.Where(kvp => kvp.Value == src).ToList();

                foreach (var item in keys)
                {
                    if(item.Key == dest)
                    {
                        filesToRename.Remove(item.Key);
                    }else
                    {
                        filesToRename[item.Key] = dest;
                    }
                }

                if (keys.Count == 0)
                    filesToRename.Add(src, dest);

                filestoRemove.Remove(dest);

                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetDOL()
        {
            if (SourceIsISO)
                return _iso.DOLData;

            if (SourceIsFileSystem)
                return File.ReadAllBytes(_folderPath + @"\sys\main.dol");

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void SetDOL(byte[] dol)
        {
            if (SourceIsISO)
                _iso.DOLData = dol;

            if (SourceIsFileSystem)
                File.WriteAllBytes(_folderPath + @"\sys\main.dol", dol);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetAppLoader()
        {
            if (SourceIsISO)
                return _iso.AppLoader;

            if (SourceIsFileSystem)
                return File.ReadAllBytes(_folderPath + @"\sys\apploader.img");

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetBin2()
        {
            if (SourceIsISO)
                return _iso.Boot2;

            if (SourceIsFileSystem)
                return File.ReadAllBytes(_folderPath + @"\sys\bi2.bin");

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetBoot()
        {
            if (SourceIsISO)
                return _iso.Boot;

            if (SourceIsFileSystem)
                return File.ReadAllBytes(_folderPath + @"\sys\boot.bin");

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetAllFiles()
        {
            if (SourceIsISO)
                return _iso.GetAllFilePaths().ToArray();

            if (SourceIsFileSystem)
                return Directory.GetFiles(_folderPath + @"\files\", "*", SearchOption.AllDirectories).Select(e=>e.Replace(_folderPath + @"\files\", "")).ToArray();

            return new string[0];
        }

        /// <summary>
        /// 
        /// </summary>
        public void DumpFileFromISO(string internalPath, string filePath)
        {
            if(_iso != null)
            {
                if (_iso.SeekFileStream(internalPath, out Stream stream, out uint size))
                {
                    int read = 0;
                    byte[] buffer = new byte[2048];
                    using (var fstream = new FileStream(filePath, FileMode.Create))
                    {
                        while(read < size)
                        {
                            var toRead = Math.Min((int)size - read, buffer.Length);
                            read += stream.Read(buffer, 0, toRead);
                            fstream.Write(buffer, 0, toRead);
                        }
                    }
                }
                else
                {
                    File.WriteAllBytes(filePath, GetFile(internalPath));
                }
            }
        }
    }
}
