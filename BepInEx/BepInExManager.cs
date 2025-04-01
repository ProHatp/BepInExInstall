using BepInExInstall;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BepInExInstall
{
    public static class BepInExManager
    {
        public static async Task<bool> InstallBepInEx(string exePath, Action<int> onProgressUpdate, Action<string> onStatusUpdate)
        {
            if (GameInfo.unityType == UnityType.Unknown || GameInfo.arcteture == Arcteture.Unknown || GameInfo.selected_version == VersionsBepInEx.Unknown)
                return false;

            string zipUrl = DownloadFiles.GetBepInExDownloadUrl();
            string tempZipPath = Path.Combine(Path.GetTempPath(), "bepinex.zip");
            string targetFolder = Path.GetDirectoryName(exePath);

            onStatusUpdate?.Invoke("Downloading files...");

            await Downloader.DownloadZipWithProgress(zipUrl, tempZipPath, onProgressUpdate);

            onStatusUpdate?.Invoke("Extracting files...");
            await Task.Run(() => DownloadFiles.ExtractZipOverwrite(tempZipPath, targetFolder));

            onStatusUpdate?.Invoke("BepInEx downloaded and extracted successfully!");
            return true;
        }

        public static void UninstallBepInEx(string exePath)
        {
            var config = new BepInExConfig();
            config.RemoveBepInEx(exePath);
            config.DeletePreloaderFiles(exePath);
        }
    }
}
