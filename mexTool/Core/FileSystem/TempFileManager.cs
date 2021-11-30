using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace mexTool.Core.FileSystem
{
    public class TempFileManager
    {
        public class FileRename
        {
            public string OriginalName;

            public string NewName;

            public FileRename(string originalName, string newName)
            {
                OriginalName = originalName;
                NewName = newName;
            }
        }

        public class FileAdd
        {
            public string TempPath;

            public string ImagePath;

            public FileAdd(string tempPath, string imagePath)
            {
                TempPath = tempPath;
                ImagePath = imagePath;
            }
        }

        private List<string> tempPaths = new List<string>();
        private List<FileRename> toRename = new List<FileRename>();
        private List<FileAdd> toAdd = new List<FileAdd>();
        private List<string> toRemove = new List<string>();

        public FileRename[] FileToRename { get => toRename.ToArray(); }
        public FileAdd[] FileToAdd { get => toAdd.ToArray(); }
        public string[] FileToRemove { get => toRemove.ToArray(); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileList"></param>
        /// <returns></returns>
        public List<string> GetFileList(string[] fileList)
        {
            List<string> files = new List<string>();

            // add original files
            files.AddRange(fileList);

            // remove deleted files
            foreach (var f in FileToRemove)
                files.Remove(f);

            // rename files
            foreach (var f in FileToRename)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    if (files[i].Equals(f.OriginalName))
                        files[i] = f.NewName;
                }
            }

            //
            HashSet<string> hashes = new HashSet<string>();
            foreach (var v in files)
                hashes.Add(v);

            // add new files
            foreach (var f in FileToAdd)
                if (!hashes.Contains(f.ImagePath))
                    files.Add(f.ImagePath);

            return files;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool FileExists(string[] fileList, string path)
        {
            return GetFileList(fileList).Contains(path);
            //return (
            //    toRename.Find(r => r.NewName.Equals(path)) != null || 
            //    toAdd.Find(e => e.ImagePath.Equals(path)) != null) && 
            //    !toRemove.Contains(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void DeleteFile(string path)
        {
            // removing it from adding queue
            toAdd.RemoveAll(e => e.ImagePath.Equals(path));

            // remove it from rename
            var rename = toRename.Find(e => e.NewName.Equals(path));
            if (rename != null)
            {
                toRemove.Add(rename.OriginalName);
                toRename.Remove(rename);
            }

            // add it to remove queue
            if (!toRemove.Contains(path))
                toRemove.Add(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool FileIsRemoved(string path)
        {
            // check if in remove queue
            // check if being renamed and nothing else being renamed to it
            return toRemove.Contains(path) ||
                (toRename.Exists(e=>e.OriginalName.Equals(path)) &&
                !toRename.Exists(e => e.NewName.Equals(path)));
        }

        /// <summary>
        /// Marks file for renaming
        /// Renaming does not occur until image is saved
        /// </summary>
        public bool RenameFile(string src, string dest)
        {
            var addedFile = toAdd.Find(e => e.ImagePath.Equals(src));
            if (addedFile != null)
            {
                addedFile.ImagePath = dest;
                //toRemove.Remove(dest);
                return true;
            }

            var targetName = toRename.Find(e => e.OriginalName.Equals(src));
            if (targetName != null)
            {
                targetName.NewName = dest;
                //toRemove.Remove(dest);
                return true;
            }

            var alreadyRenamed = toRename.Find(e => e.NewName.Equals(src));
            if (alreadyRenamed != null)
            {
                alreadyRenamed.NewName = dest;
                //toRename.Remove(alreadyRenamed);
                return true;
            }

            toRename.Add(new FileRename(src, dest));
            //toRemove.Remove(dest);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetRenamedFile(string path)
        {
            var rename = toRename.Find(e => e.NewName == path);

            if (rename != null)
                return rename.OriginalName;

            return path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetFileData(string path)
        {
            var dataAdd = toAdd.Find(e=>e.ImagePath.Equals(path));

            if (dataAdd != null)
                return File.ReadAllBytes(dataAdd.TempPath);

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public uint GetFileSize(string path)
        {
            var dataAdd = toAdd.Find(e => e.ImagePath.Equals(path));

            if (dataAdd != null)
                return (uint)new FileInfo(dataAdd.TempPath).Length;

            return 0;
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
            var add = toAdd.Find(e => e.ImagePath.ToLower().Equals(filePath.ToLower()));

            if (add != null)
                add.TempPath = diskFilePath;

            //toRemove.Remove(filePath);
            toAdd.Add(new FileAdd(diskFilePath, filePath));

            return true;
        }

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

            //tempdir = Path.GetDirectoryName(fname);

            //if (!Directory.Exists(tempdir))
            //    Directory.CreateDirectory(tempdir);

            tempPaths.Add(fname);

            return fname;
        }

        /// <summary>
        /// 
        /// </summary>
        public void DeleteTempFiles()
        {
            foreach (var v in tempPaths)
                File.Delete(v);

            tempPaths.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            toAdd.Clear();
            toRemove.Clear();
            toRename.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetRealFilePath(string path, FS_Extracted fs)
        {
            if (toAdd.Exists(e=>e.ImagePath.Equals(path)))
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, toAdd.Find(e=>e.ImagePath.Equals(path)).TempPath);

            return fs.GetFolderPath(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsNewFile(string path)
        {
            return toAdd.Exists(e => e.ImagePath == path);
        }
    }
}
