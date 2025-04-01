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
                string gameFolderName = new DirectoryInfo(exeFolder).Name;
                string steamAppsPath = exeFolder;

                // Sobe até encontrar a pasta steamapps
                while (!steamAppsPath.EndsWith("steamapps") && steamAppsPath != Directory.GetDirectoryRoot(steamAppsPath))
                {
                    steamAppsPath = Path.GetDirectoryName(steamAppsPath);
                }

                if (!Directory.Exists(steamAppsPath))
                    return null;

                string[] manifestFiles = Directory.GetFiles(steamAppsPath, "appmanifest_*.acf");

                foreach (string manifest in manifestFiles)
                {
                    string[] lines = File.ReadAllLines(manifest);
                    string appId = null;
                    string installdir = null;

                    foreach (string line in lines)
                    {
                        if (line.Contains("\"appid\"") && appId == null)
                        {
                            appId = ExtractQuotedValue(line);
                        }
                        else if (line.Contains("\"installdir\"") && installdir == null)
                        {
                            installdir = ExtractQuotedValue(line);
                        }

                        if (!string.IsNullOrEmpty(appId) && !string.IsNullOrEmpty(installdir))
                        {
                            if (string.Equals(installdir, gameFolderName, StringComparison.OrdinalIgnoreCase))
                            {
                                return appId;
                            }
                        }
                    }
                }
            }
            catch { }

            return null;
        }

        private static string ExtractQuotedValue(string line)
        {
            var match = Regex.Match(line, "\"[^\"]+\"\\s+\"([^\"]+)\"");
            return match.Success ? match.Groups[1].Value : null;
        }
    }
}
