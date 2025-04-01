using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BepInExInstall
{
    public static class Downloader
    {
        public static async Task DownloadZipWithProgress(string url, string destination, Action<int> progress)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += (s, e) =>
                {
                    progress?.Invoke(e.ProgressPercentage);
                };

                await client.DownloadFileTaskAsync(new Uri(url), destination);
            }
        }
    }
}
