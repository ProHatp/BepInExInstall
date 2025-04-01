using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BepInExInstall
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0)
            {
                string arg = args[0].ToLower();
                string path = args.Length > 1 ? args[1] : string.Empty;

                if (arg == "--install" && !string.IsNullOrEmpty(path))
                {
                    RunCliInstall(path).Wait();
                    return;
                }
                else if (arg == "--uninstall" && !string.IsNullOrEmpty(path))
                {
                    BepInExManager.UninstallBepInEx(path);
                    Console.WriteLine("BepInEx successfully uninstalled.");
                    return;
                }
                else
                {
                    Console.WriteLine("Usage:");
                    Console.WriteLine("  --install <path_to_exe>");
                    Console.WriteLine("  --uninstall <path_to_exe>");
                    return;
                }
            }

            Application.Run(new Main());
        }

        static async Task RunCliInstall(string exePath)
        {
            Console.WriteLine("Installing BepInEx...");
            GameInfo.gameProcess = new System.Diagnostics.Process();
            GameInfo.unityType = UnityType.Mono; // default assumption
            GameInfo.arcteture = Arcteture.x64;  // default assumption
            GameInfo.selected_version = VersionsBepInEx.v5_4_15; // DEFAULT version

            try
            {
                bool result = await BepInExManager.InstallBepInEx(
                    exePath,
                    progress => Console.Write($"\rProgress: {progress}%"),
                    status => Console.WriteLine($"\n{status}")
                );

                if (result)
                    Console.WriteLine("\nInstallation completed successfully!");
                else
                    Console.WriteLine("\nInstallation failed or was canceled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
