using mexTool.Tools;
using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace mexTool
{
    class Updater
    {
        static Release[] releases;

        public static Release LatestRelease;

        public static string DownloadURL;
        public static string Version;

        public static bool UpdateReady;

        /// <summary>
        /// 
        /// </summary>
        public static bool UpdateCodes()
        {
            // https://github.com/akaneia/m-ex/raw/master/asm/codes.gct
            var codesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"\lib\codes.gct");

            if (!File.Exists(codesPath))
                return false;

            var mxhash = HashGen.ComputeSHA256Hash(File.ReadAllBytes(codesPath));

            using (var client = new WebClient())
                client.DownloadFile(@"https://github.com/akaneia/m-ex/raw/master/asm/codes.gct", codesPath);

            var newhash = HashGen.ComputeSHA256Hash(File.ReadAllBytes(codesPath));

            if (!mxhash.Equals(newhash))
                return true;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void CheckLatest()
        {
            string currentVersion = "";
            if (File.Exists(Path.Combine(ApplicationSettings.ExecutablePath, "version.txt")))
                currentVersion = File.ReadAllText(Path.Combine(ApplicationSettings.ExecutablePath, "version.txt"));

            try
            {
                var client = new GitHubClient(new ProductHeaderValue("mex-updater"));
                GetReleases(client).Wait();

                foreach (Release latest in releases)
                {
                    if (latest.Prerelease && 
                        latest.Assets.Count > 0 && 
                        !latest.Assets[0].UpdatedAt.ToString().Equals(currentVersion))
                    {
                        Console.WriteLine($"Name: {latest.Name}");
                        Console.WriteLine($"URL: {latest.Assets[0].BrowserDownloadUrl}");
                        Console.WriteLine($"Upload Date: {latest.Assets[0].UpdatedAt}");

                        LatestRelease = latest;
                        DownloadURL = latest.Assets[0].BrowserDownloadUrl;
                        Version = latest.Assets[0].UpdatedAt.ToString();
                        UpdateReady = true;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to get latest update\n{e.ToString()}");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        static async Task GetReleases(GitHubClient client)
        {
            List<Release> Releases = new List<Release>();
            foreach (Release r in await client.Repository.Release.GetAll("akaneia", "mexTool"))
                Releases.Add(r);
            releases = Releases.ToArray();
        }
    }
}
