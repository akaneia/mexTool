using System.IO;
using System.IO.Compression;

namespace mexTool.Tools
{
    public static class ZipExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="zip"></param>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        public static void AddFile(this ZipArchive zip, string filePath, byte[] data)
        {
            if (data != null && data.Length > 0)
                using (var o = zip.CreateEntry(filePath).Open())
                    o.Write(data, 0, data.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zip"></param>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        public static byte[] GetFile(this ZipArchive zip, string filePath)
        {
            if (filePath == null)
                return null;

            var entry = zip.GetEntry(filePath);

            if (entry == null)
                return null;

            using (var s = entry.Open())
            using (var deom = new MemoryStream())
            {
                s.CopyTo(deom);
                return deom.ToArray();
            }
        }
    }
}
