using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


enum UnityType
{
    Mono, IL2CPP, Unknown
}

enum Arcteture
{
    x86, x64, Unknown
}

enum VersionsBepInEx
{
    v6_0_0, v5_4_15, Unknown
}

static class GameInfo
{
    public static Process gameProcess                   = new Process();
    public static Icon icon                             = null;
    public static string game_name                      = "";
    public static string unity_version                  = "";
    public static string unity_type                     = "";
    public static string game_architecture              = "";

    public static Arcteture arcteture                   = Arcteture.Unknown;
    public static UnityType unityType                   = UnityType.Unknown;
    public static VersionsBepInEx selected_version      = VersionsBepInEx.Unknown;

    public static string bepinex_status                 = "";
    public static string bepinex_version                = "";
    public static VersionsBepInEx bepinex_version_enum = VersionsBepInEx.Unknown;

    public static string bepinex_loaded                 = "";
    public static string bepinex_config                 = "";
    public static string unityexplorer_loaded           = "";

    public static bool checkBepInEx                     = false;
    public static bool checkUnity                       = false;
    public static bool checkBepInExLoaded               = false;
}

static class GetInfoFiles
{
    public static void DetectArchitecture(string exePath)
    {
        try
        {
            using (FileStream fs = new FileStream(exePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                fs.Seek(0x3C, SeekOrigin.Begin);
                int peHeaderOffset = reader.ReadInt32();

                fs.Seek(peHeaderOffset, SeekOrigin.Begin);
                uint peSignature = reader.ReadUInt32();

                if (peSignature != 0x00004550)
                {
                    GameInfo.game_architecture  = "Unknown";
                    GameInfo.arcteture          = Arcteture.Unknown;
                    return;
                }

                ushort machine = reader.ReadUInt16();

                switch (machine)
                {
                    case 0x014C:
                        GameInfo.game_architecture = "x86";
                        GameInfo.arcteture = Arcteture.x86;
                        return;
                    case 0x8664:
                        GameInfo.game_architecture = "x64";
                        GameInfo.arcteture = Arcteture.x64;
                        return;
                    default:
                        GameInfo.game_architecture = "Unknown";
                        GameInfo.arcteture = Arcteture.Unknown;
                        return;
                }
            }
        }
        catch (Exception ex)
        {
            GameInfo.game_architecture = $"Erro: {ex.Message}";
            GameInfo.arcteture = Arcteture.Unknown;
            return;
        }
    }

    public static VersionsBepInEx GetVersionBepInEx(string ProductVersion = "")
    {
        if(ProductVersion.Contains("6.0.0"))
        {
            return VersionsBepInEx.v6_0_0;
        }
        else if (ProductVersion.Contains("5.4.15"))
        {
            return VersionsBepInEx.v5_4_15;
        }
        else
        {
            return VersionsBepInEx.Unknown;
        }
    }

    public static void CheckBepInExStatus(string exePath)
    {
        string baseDir = Path.GetDirectoryName(exePath);
        string[] possiblePreloaders = {
            Path.Combine(baseDir, "BepInEx", "core", "BepInEx.Preloader.dll"),
            Path.Combine(baseDir, "BepInEx", "core", "BepInEx.Preloader.Core.dll")
        };

        foreach (var path in possiblePreloaders)
        {
            if (File.Exists(path))
            {
                var info = FileVersionInfo.GetVersionInfo(path);

                GameInfo.bepinex_status         = "INSTALLED";
                GameInfo.bepinex_version        =  info.ProductVersion ?? "Unknown";
                GameInfo.bepinex_loaded         = "LOADED";
                GameInfo.bepinex_config         = "LOADED";
                GameInfo.checkBepInEx           = true;
                GameInfo.bepinex_version_enum   = GetVersionBepInEx(info.ProductVersion);
                return;
            }
        }

        GameInfo.bepinex_status         = "N/A";
        GameInfo.bepinex_version        = "N/A";
        GameInfo.bepinex_loaded         = "NOT LOADED";
        GameInfo.bepinex_config         = "NOT LOADED";
        GameInfo.bepinex_version_enum   = GetVersionBepInEx();
        GameInfo.checkBepInEx           = false;
    }

    public static void LoadExeInfo(string exePath)
    {
        try
        {
            var info = FileVersionInfo.GetVersionInfo(exePath);

            GameInfo.game_name          = $"{Path.GetFileName(exePath)}";
            GameInfo.unity_version      = (GameInfo.checkUnity) ? (!string.IsNullOrWhiteSpace(info.ProductVersion)) ? $"{(info.ProductVersion)}" : "N/A" : "Unknown";
            GameInfo.icon               = Icon.ExtractAssociatedIcon(exePath);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading information: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static void DetectTypeUnity(string exePath)
    {
        string baseDir      = Path.GetDirectoryName(exePath);
        string dataFolder   = Path.Combine(baseDir, Path.GetFileNameWithoutExtension(exePath) + "_Data");
        string monoPath     = Path.Combine(dataFolder, "Managed", "Assembly-CSharp.dll");
        string il2cppDll    = Path.Combine(baseDir, "GameAssembly.dll");
        string il2cppMeta   = Path.Combine(dataFolder, "il2cpp_data", "Metadata", "global-metadata.dat");

        if (File.Exists(monoPath))
        {
            GameInfo.checkUnity     = true;
            GameInfo.unityType      = UnityType.Mono;
            GameInfo.unity_type     = "Mono";
            return;
        }

        if (File.Exists(il2cppDll) || File.Exists(il2cppMeta))
        {
            GameInfo.checkUnity = true;
            GameInfo.unityType = UnityType.IL2CPP;
            GameInfo.unity_type = "IL2CPP";
            return;
        }

        GameInfo.checkUnity = false;
        GameInfo.unityType = UnityType.Unknown;
        GameInfo.unity_type = "Unknown";
        return;
    }

    public static void CheckUnityExplorer(string exePath)
    {
        string baseDir = Path.GetDirectoryName(exePath);
        string[] possiblePreloaders = {
            Path.Combine(baseDir, "BepInEx", "plugins", "sinai-dev-UnityExplorer", "UnityExplorer.BIE6.Unity.Mono.dll"),
            Path.Combine(baseDir, "BepInEx", "plugins", "sinai-dev-UnityExplorer", "UniverseLib.Mono.dll"),

            Path.Combine(baseDir, "BepInEx", "plugins", "sinai-dev-UnityExplorer", "UnityExplorer.BIE.Unity.IL2CPP.CoreCLR.dll"),
            Path.Combine(baseDir, "BepInEx", "plugins", "sinai-dev-UnityExplorer", "UniverseLib.BIE.IL2CPP.Interop.dll"),
        };

        foreach (var path in possiblePreloaders)
        {
            if (File.Exists(path))
            {
                GameInfo.unityexplorer_loaded = "LOADED";
                return;
            }
        }
        GameInfo.unityexplorer_loaded = "NOT LOADED";
    }

    public static void GetInfo(string exePath)
    {
        DetectTypeUnity(exePath);
        CheckBepInExStatus(exePath);
        CheckUnityExplorer(exePath);
        DetectArchitecture(exePath);
        LoadExeInfo(exePath);
    }
}
