using GCILib;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace mexTool.Core.FileSystem
{
    public class FS_Extracted : IFS
    {
        private string _folderPath;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool TryOpen(string path)
        {
            _folderPath = path;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            _folderPath = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="filePath"></param>
        /// <param name="saveAs"></param>
        public void Save(ProgressChangedEventHandler progress, string filePath, bool saveAs, TempFileManager manager)
        {
            HashSet<string> newFiles = new HashSet<string>();

            // remove
            foreach (var rem in manager.FileToRemove)
            {
                File.Delete(GetFolderPath(rem));
            }
            progress.Invoke(null, new ProgressChangedEventArgs(25, null));

            // rename
            foreach (var ren in manager.FileToRename)
            {
                File.Move(GetFolderPath(ren.OriginalName), GetFolderPath(ren.NewName));
            }
            progress.Invoke(null, new ProgressChangedEventArgs(50, null));

            // add
            foreach (var add in manager.FileToAdd)
            {
                var realPath = GetFolderPath(add.ImagePath);

                if (add.TempPath == realPath)
                    continue;

                if (!File.Exists(add.TempPath))
                    continue;

                if (File.Exists(realPath))
                    File.Delete(realPath);

                File.Move(add.TempPath, realPath);

                newFiles.Add(add.ImagePath);
            }
            progress.Invoke(null, new ProgressChangedEventArgs(75, null));

            // save
            // nothing to do

            // cleanup
            manager.Clear();
            manager.DeleteTempFiles(); 

            // done
            progress.Invoke(null, new ProgressChangedEventArgs(100, null));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool FileExists(string filePath)
        {
            return File.Exists(Path.Combine(_folderPath + "\\files\\", filePath));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetAppLoader()
        {
            return File.ReadAllBytes(_folderPath + @"\sys\apploader.img");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetBin2()
        {
            return File.ReadAllBytes(_folderPath + @"\sys\bi2.bin");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetBoot()
        {
            return File.ReadAllBytes(_folderPath + @"\sys\boot.bin");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dol"></param>
        public void SetBoot(byte[] boot)
        {
            File.WriteAllBytes(_folderPath + @"\sys\boot.bin", boot);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetDOL()
        {
            return File.ReadAllBytes(_folderPath + @"\sys\main.dol");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dol"></param>
        public void SetDOL(byte[] dol)
        {
            File.WriteAllBytes(_folderPath + @"\sys\main.dol", dol);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetAddressOrder()
        {
            var path = _folderPath + @"\sys\file_order.txt";
            if (File.Exists(path))
                return File.ReadAllLines(path);
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public byte[] GetFileData(string path)
        {
            return File.ReadAllBytes(GetFolderPath(path));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public uint GetFileSize(string path)
        {
            return (uint)new FileInfo(GetFolderPath(path)).Length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetFileList()
        {
            return Directory.GetFiles(_folderPath + @"\files\", "*", SearchOption.AllDirectories).Select(e => e.Replace(_folderPath + @"\files\", "")).ToArray();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public string GetFolderPath(string rootPath)
        {
            return Path.Combine(_folderPath + "\\files", rootPath);
        }
    }
}
