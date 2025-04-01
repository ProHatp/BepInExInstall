using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BepInExInstall
{
    public static class GameLoader
    {
        public static string ExePath { get; private set; }

        public static void SetExePath(string path)
        {
            ExePath = path;
        }

        public static async Task<bool> StartGameAsync()
        {
            if (string.IsNullOrEmpty(ExePath)) return false;

            GameInfo.gameProcess.StartInfo.FileName = ExePath;
            GameInfo.gameProcess.Start();

            await Task.Delay(3000);
            return true;
        }

        public static void ExitGame()
        {
            try
            {
                if (GameInfo.gameProcess != null && !GameInfo.gameProcess.HasExited)
                {
                    GameInfo.gameProcess.Kill();
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro ao tentar finalizar processo: " + ex.Message);
            }

            if (!string.IsNullOrEmpty(ExePath))
            {
                string name = Path.GetFileNameWithoutExtension(ExePath);
                foreach (var proc in Process.GetProcessesByName(name))
                {
                    try { proc.Kill(); } catch (Exception ex) { Debug.WriteLine(ex.Message); }
                }
            }
        }
        public static string TryGetAppIdFromSteamTxt(string exePath)
        {
            try
            {
                string dir = Path.GetDirectoryName(exePath);
                string steamTxtPath = Path.Combine(dir, "steam_appid.txt");

                if (File.Exists(steamTxtPath))
                {
                    return File.ReadAllText(steamTxtPath).Trim();
                }

                // Tenta uma pasta acima
                steamTxtPath = Path.Combine(Directory.GetParent(dir).FullName, "steam_appid.txt");
                if (File.Exists(steamTxtPath))
                {
                    return File.ReadAllText(steamTxtPath).Trim();
                }
            }
            catch { }

            return null;
        }

        public static string TryGetAppIdFromExePath(string exePath)
        {
            try
            {
                string exeFolder = Path.GetDirectoryName(exePath);
                string steamAppsPath = exeFolder;

                while (!steamAppsPath.EndsWith("steamapps") && steamAppsPath != Directory.GetDirectoryRoot(steamAppsPath))
                {
                    steamAppsPath = Path.GetDirectoryName(steamAppsPath);
                }

                if (!Directory.Exists(steamAppsPath))
                    return null;

                string[] manifestFiles = Directory.GetFiles(steamAppsPath, "appmanifest_*.acf");

                foreach (string manifest in manifestFiles)
                {
                    string content = File.ReadAllText(manifest);
                    if (content.Contains(Path.GetFileNameWithoutExtension(exePath)))
                    {
                        var match = Regex.Match(content, @"""appid""\s+""(\d+)""");
                        if (match.Success)
                        {
                            return match.Groups[1].Value;
                        }
                    }
                }
            }
            catch { }

            return null;
        }

    }
}
