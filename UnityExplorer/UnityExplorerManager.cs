using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BepInExInstall
{
    public static class UnityExplorerManager
    {
        public static async Task<bool> InstallUnityExplorer(string exePath, Action<int> onProgressUpdate, Action<string> onStatusUpdate)
        {
            if (GameInfo.unityType == UnityType.Unknown || GameInfo.arcteture == Arcteture.Unknown || GameInfo.bepinex_version_enum == VersionsBepInEx.Unknown)
                return false;

            string zipUrl = DownloadFiles.GetUnityExplorerUrl();
            string tempZipPath = Path.Combine(Path.GetTempPath(), "unityexplorer.zip");
            string targetFolder = Path.Combine(Path.GetDirectoryName(exePath), "BepInEx");

            onStatusUpdate?.Invoke("Downloading files...");
            await Downloader.DownloadZipWithProgress(zipUrl, tempZipPath, onProgressUpdate);

            onStatusUpdate?.Invoke("Extracting files...");
            await Task.Run(() => DownloadFiles.ExtractZipOverwrite(tempZipPath, targetFolder));

            onStatusUpdate?.Invoke("Unity Explorer downloaded and extracted successfully!");
            return true;
        }

        public static void UninstallUnityExplorer(string exePath)
        {
            var config = new UnityExplorerConfig();
            config.RemoverUnityExplorer(exePath);
        }
    }
}
