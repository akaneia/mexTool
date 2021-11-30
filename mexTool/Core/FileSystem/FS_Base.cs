using GCILib;
using System.ComponentModel;

namespace mexTool.Core.FileSystem
{
    public interface IFS
    {
        bool TryOpen(string path);

        void Close();

        void Save(ProgressChangedEventHandler progress, string filePath, bool saveAs, TempFileManager manager);

        bool FileExists(string filePath);

        byte[] GetFileData(string path);

        uint GetFileSize(string path);

        byte[] GetDOL();

        void SetDOL(byte[] dol);

        byte[] GetAppLoader();

        byte[] GetBin2();

        byte[] GetBoot();

        string[] GetFileList();
    }
}
