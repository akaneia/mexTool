using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;

namespace mexUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
                return;

            var downloadUrl = args[0];
            var version = args[1];

            using (var client = new WebClient())
                client.DownloadFile(downloadUrl, "update.zip");

            using (FileStream stream = new FileStream("update.zip", FileMode.OpenOrCreate))
            using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                foreach (var e in archive.Entries)
                {
                    // TODO: extract
                    Console.WriteLine(e.FullName + " " + e.CompressedLength.ToString("X"));
                    if(e.CompressedLength != 0)
                    {
                        if(!string.IsNullOrEmpty(e.FullName) && !string.IsNullOrEmpty(Path.GetDirectoryName(e.FullName)))
                            Directory.CreateDirectory(Path.GetDirectoryName(e.FullName));

                        using (var output = new FileStream(e.FullName, FileMode.Create))
                        using (var export = e.Open())
                        {
                            export.CopyTo(output);
                        }
                    }
                }
            }

            File.Delete("update.zip");
            File.WriteAllText("version.txt", version);

            if(args.Length >= 2 && args[2] == "-r")
                Process.Start("mexTool.exe");

            Console.WriteLine("Done!");
            Thread.Sleep(1000);
        }
    }
}
