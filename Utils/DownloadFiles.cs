using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;

static class DownloadFiles
{
    public static Dictionary<(VersionsBepInEx, UnityType, Arcteture), string> VersionedBepInExLinks = new Dictionary<(VersionsBepInEx, UnityType, Arcteture), string>
    {
        { (VersionsBepInEx.v6_0_0, UnityType.Mono, Arcteture.x86),          "https://github.com/BepInEx/BepInEx/releases/download/v6.0.0-pre.2/BepInEx-Unity.Mono-win-x86-6.0.0-pre.2.zip" },
        { (VersionsBepInEx.v6_0_0, UnityType.Mono, Arcteture.x64),          "https://github.com/BepInEx/BepInEx/releases/download/v6.0.0-pre.2/BepInEx-Unity.Mono-win-x64-6.0.0-pre.2.zip" },

        { (VersionsBepInEx.v6_0_0, UnityType.IL2CPP, Arcteture.x86),        "https://github.com/BepInEx/BepInEx/releases/download/v6.0.0-pre.2/BepInEx-Unity.IL2CPP-win-x86-6.0.0-pre.2.zip" },
        { (VersionsBepInEx.v6_0_0, UnityType.IL2CPP, Arcteture.x64),        "https://github.com/BepInEx/BepInEx/releases/download/v6.0.0-pre.2/BepInEx-Unity.IL2CPP-win-x64-6.0.0-pre.2.zip" },

        { (VersionsBepInEx.v5_4_15, UnityType.Mono, Arcteture.x86),         "https://github.com/BepInEx/BepInEx/releases/download/v5.4.15/BepInEx_x86_5.4.15.0.zip" },
        { (VersionsBepInEx.v5_4_15, UnityType.Mono, Arcteture.x64),         "https://github.com/BepInEx/BepInEx/releases/download/v5.4.15/BepInEx_x64_5.4.15.0.zip" },
    };

    public static Dictionary<(VersionsBepInEx, UnityType, Arcteture), string> VersionedUnityExplorerLinks = new Dictionary<(VersionsBepInEx, UnityType, Arcteture), string>
    {
        { (VersionsBepInEx.v6_0_0, UnityType.Mono, Arcteture.x86),          "https://github.com/yukieiji/UnityExplorer/releases/download/v4.12.3/UnityExplorer.BepInEx6.Unity.Mono.zip" },
        { (VersionsBepInEx.v6_0_0, UnityType.Mono, Arcteture.x64),          "https://github.com/yukieiji/UnityExplorer/releases/download/v4.12.3/UnityExplorer.BepInEx6.Unity.Mono.zip" },

        { (VersionsBepInEx.v6_0_0, UnityType.IL2CPP, Arcteture.x86),        "https://github.com/yukieiji/UnityExplorer/releases/download/v4.12.3/UnityExplorer.BepInEx.Unity.IL2CPP.CoreCLR.zip" },
        { (VersionsBepInEx.v6_0_0, UnityType.IL2CPP, Arcteture.x64),        "https://github.com/yukieiji/UnityExplorer/releases/download/v4.12.3/UnityExplorer.BepInEx.Unity.IL2CPP.CoreCLR.zip" },

        { (VersionsBepInEx.v5_4_15, UnityType.Mono, Arcteture.x86),         "https://github.com/yukieiji/UnityExplorer/releases/download/v4.12.3/UnityExplorer.BepInEx5.Mono.zip" },
        { (VersionsBepInEx.v5_4_15, UnityType.Mono, Arcteture.x64),         "https://github.com/yukieiji/UnityExplorer/releases/download/v4.12.3/UnityExplorer.BepInEx5.Mono.zip" },
    };

    public static void ExtractZipOverwrite(string zipPath, string extractPath)
    {
        using (ZipArchive archive = ZipFile.OpenRead(zipPath))
        {
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                string destinationPath = Path.Combine(extractPath, entry.FullName);
                Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                if (string.IsNullOrEmpty(entry.Name))
                    continue;
                entry.ExtractToFile(destinationPath, true);
            }
        }
    }

    public static string GetUnityExplorerUrl()
    {
        var key = (GameInfo.selected_version, GameInfo.unityType, GameInfo.arcteture);

        if (VersionedUnityExplorerLinks.TryGetValue(key, out string url))
            return url;

        return null;
    }

    public static string GetBepInExDownloadUrl()
    {
        var key = (GameInfo.selected_version, GameInfo.unityType, GameInfo.arcteture);

        if (VersionedBepInExLinks.TryGetValue(key, out string url))
            return url;

        return null;
    }
}
