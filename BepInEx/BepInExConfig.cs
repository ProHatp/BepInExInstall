using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


public class BepInExConfig
{
    /* Caching */
    public bool EnableAssemblyCache;

    /* Chainloader */
    public bool HideManagerGameObject;

    /* Harmony.Logger */
    public string LogChannels;

    /* Logging */
    public bool UnityLogListening;
    public bool LogConsoleToUnityLog;

    /* Logging.Console */
    public bool ConsoleEnabled;
    public bool PreventClose;
    public bool ShiftJisEncoding;
    public string StandardOutType;
    public string ConsoleLogLevels;

    /* Logging.Disk */
    public bool WriteUnityLog;
    public bool AppendLog;
    public bool DiskLogEnabled;
    public string DiskLogLevels;

    /* Preloader */
    public bool ApplyRuntimePatches;
    public string HarmonyBackend;
    public bool DumpAssemblies;
    public bool LoadDumpedAssemblies;
    public bool BreakBeforeLoadAssemblies;

    /* Preloader.Entrypoint */
    public string EntryAssembly;
    public string EntryType;
    public string EntryMethod;

    public void OpenRepository()
    {
        var psi = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "https://github.com/BepInEx/BepInEx",
            UseShellExecute = true
        };
        System.Diagnostics.Process.Start(psi);
    }

    public void RemoveCommentsFromCfg(string filePath)
    {
        var cleanedLines = File.ReadAllLines(filePath)
            .Where(line => !line.TrimStart().StartsWith("#"))
            .ToList();

        File.WriteAllLines(filePath, cleanedLines);
    }

    public void DeletePreloaderFiles(string exePath)
    {
        string targetFolder = Path.GetDirectoryName(exePath);
        try
        {
            if (!Directory.Exists(targetFolder))
            {
                return;
            }

            string[] files = Directory.GetFiles(targetFolder, "preloader_*");

            foreach (string file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception)
                {

                }
            }
        }
        catch (Exception)
        {

        }
    }

    public void RemoveBepInEx(string exePath)
    {
        try
        {
            string targetFolder     = Path.GetDirectoryName(exePath);
            string bepinexPath      = Path.Combine(targetFolder, "BepInEx");
            string dotnet_path      = Path.Combine(targetFolder, "dotnet");

            if (Directory.Exists(bepinexPath))
            {
                Directory.Delete(bepinexPath, true);
            }

            if (Directory.Exists(dotnet_path))
            {
                Directory.Delete(dotnet_path, true);
            }

            string[] arquivosParaExcluir = new[]
            {
                ".doorstop_version",
                "changelog.txt",
                "doorstop_config.ini",
                "winhttp.dll"
            };

            foreach (string nomeArquivo in arquivosParaExcluir)
            {
                string caminho = Path.Combine(targetFolder, nomeArquivo);
                if (File.Exists(caminho))
                {
                    File.Delete(caminho);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error removing files: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public bool CreateBkpConfigFile(string iniPath)
    {
        string backupPath = Path.Combine(Path.GetDirectoryName(iniPath), "BepInEx_backup.cfg");
        if (File.Exists(backupPath))
            return true;

        try
        {
            File.Copy(iniPath, backupPath, true);
            RemoveCommentsFromCfg(iniPath);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
