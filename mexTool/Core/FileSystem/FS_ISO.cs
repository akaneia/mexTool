using GCILib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mexTool.Core.FileSystem
{
    public class FS_ISO : IFS
    {
        private string _isoPath;

        private byte[] dol;
        private byte[] appLoader;
        private byte[] boot;
        private byte[] bin2;
        private GCBanner banner;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool TryOpen(string path)
        {
            //_iso = new GCISO(path);
            _isoPath = path;

            using (var _iso = new GCISO(_isoPath))
            {
                dol = _iso.DOLData;
                appLoader = _iso.AppLoader;
                boot = _iso.Boot;
                bin2 = _iso.Boot2;
                banner = _iso.FindBanner();
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            //if (_iso != null)
            //    _iso.Dispose();
            //_iso = null;
            _isoPath = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="filePath"></param>
        /// <param name="saveAs"></param>
        public void Save(ProgressChangedEventHandler progress, string filePath, bool saveAs, TempFileManager manager)
        {
            using (var _iso = new GCISO(_isoPath))
            {
                // add
                foreach (var add in manager.FileToAdd)
                {
                    _iso.AddFile(add.ImagePath, add.TempPath);
                }

                // rename
                foreach (var ren in manager.FileToRename)
                {
                    _iso.RenameFile(ren.OriginalName, ren.NewName);
                }

                // remove
                foreach (var rem in manager.FileToRemove)
                {
                    _iso.DeleteFileOrFolder(rem);
                }

                // save
                if (_iso.NeedsRebuild)
                {
                    MessageBox.Show("ISO need to be rebuilt", "Rebuild ISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    using (var d = new SaveFileDialog())
                    {
                        d.Title = "Choose new iso file path";
                        d.Filter = "Gamecube ISO (*.iso)|*.iso";

                        if (d.ShowDialog() == DialogResult.OK)
                        {
                            _iso.Rebuild(d.FileName, progress);
                            _isoPath = d.FileName;
                        }
                    }
                }
                else
                {
                    _iso.Save(progress);
                }

                // cleanup
                manager.Clear();
                manager.DeleteTempFiles();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool FileExists(string filePath)
        {
            using (var _iso = new GCISO(_isoPath))
                return _iso.FileExists(filePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetAppLoader()
        {
            return appLoader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GCBanner GetBanner()
        {
            return banner;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetBin2()
        {
            return bin2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetBoot()
        {
            return boot;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetDOL()
        {
            return dol;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dol"></param>
        public void SetDOL(byte[] dol)
        {
            this.dol = dol;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public byte[] GetFileData(string path)
        {
            using (var _iso = new GCISO(_isoPath))
                return _iso.GetFileData(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetFileList()
        {
            using (var _iso = new GCISO(_isoPath))
                return _iso.GetAllFilePaths().ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        public void DumpFileFromISO(string internalPath, string filePath)
        {
            using (var _iso = new GCISO(_isoPath))
            {
                if (_iso.SeekFileStream(internalPath, out Stream stream, out uint size))
                {
                    int read = 0;
                    byte[] buffer = new byte[2048];
                    using (var fstream = new FileStream(filePath, FileMode.Create))
                    {
                        while (read < size)
                        {
                            var toRead = Math.Min((int)size - read, buffer.Length);
                            read += stream.Read(buffer, 0, toRead);
                            fstream.Write(buffer, 0, toRead);
                        }
                    }
                }
            }
        }
    }
}