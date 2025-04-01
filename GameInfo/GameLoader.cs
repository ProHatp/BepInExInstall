using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
    }
}
