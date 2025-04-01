using BepInExInstall;
using System.Windows.Forms;

namespace BepInExInstall
{
    public static class BepInExConfigManager
    {
        public static void ApplyToUI(BepInExConfig config, Main form)
        {
            form.EnableAssemblyCache.Checked = config.EnableAssemblyCache;
            form.HideManagerGameObject.Checked = config.HideManagerGameObject;
            form.UnityLogListening.Checked = config.UnityLogListening;
            form.LogConsoleToUnityLog.Checked = config.LogConsoleToUnityLog;

            form.ConsoleEnabled.Checked = config.ConsoleEnabled;
            form.PreventClose.Checked = config.PreventClose;
            form.ShiftJisEncoding.Checked = config.ShiftJisEncoding;

            form.WriteUnityLog.Checked = config.WriteUnityLog;
            form.AppendLog.Checked = config.AppendLog;
            form.DiskLogEnabled.Checked = config.DiskLogEnabled;

            form.ApplyRuntimePatches.Checked = config.ApplyRuntimePatches;
            form.DumpAssemblies.Checked = config.DumpAssemblies;
            form.LoadDumpedAssemblies.Checked = config.LoadDumpedAssemblies;
            form.BreakBeforeLoadAssemblies.Checked = config.BreakBeforeLoadAssemblies;
        }

        public static void UpdateFromUI(BepInExConfig config, Main form)
        {
            config.EnableAssemblyCache = form.EnableAssemblyCache.Checked;
            config.HideManagerGameObject = form.HideManagerGameObject.Checked;
            config.UnityLogListening = form.UnityLogListening.Checked;
            config.LogConsoleToUnityLog = form.LogConsoleToUnityLog.Checked;

            config.ConsoleEnabled = form.ConsoleEnabled.Checked;
            config.PreventClose = form.PreventClose.Checked;
            config.ShiftJisEncoding = form.ShiftJisEncoding.Checked;

            config.WriteUnityLog = form.WriteUnityLog.Checked;
            config.AppendLog = form.AppendLog.Checked;
            config.DiskLogEnabled = form.DiskLogEnabled.Checked;

            config.ApplyRuntimePatches = form.ApplyRuntimePatches.Checked;
            config.DumpAssemblies = form.DumpAssemblies.Checked;
            config.LoadDumpedAssemblies = form.LoadDumpedAssemblies.Checked;
            config.BreakBeforeLoadAssemblies = form.BreakBeforeLoadAssemblies.Checked;
        }

        public static void Reset(Main form)
        {
            form.EnableAssemblyCache.Checked = false;
            form.HideManagerGameObject.Checked = false;
            form.UnityLogListening.Checked = false;
            form.LogConsoleToUnityLog.Checked = false;

            form.ConsoleEnabled.Checked = false;
            form.PreventClose.Checked = false;
            form.ShiftJisEncoding.Checked = false;

            form.WriteUnityLog.Checked = false;
            form.AppendLog.Checked = false;
            form.DiskLogEnabled.Checked = false;

            form.ApplyRuntimePatches.Checked = false;
            form.DumpAssemblies.Checked = false;
            form.LoadDumpedAssemblies.Checked = false;
            form.BreakBeforeLoadAssemblies.Checked = false;
        }

        public static void UpdatConfigByFile(IniFile ini, Main form)
        {
            if (ini == null)
            {
                return;
            }

            /* Caching */
            ini.Set("Caching", "EnableAssemblyCache", form.EnableAssemblyCache.Checked.ToString().ToLower());

            /* Chainloader */
            ini.Set("Chainloader", "HideManagerGameObject", form.HideManagerGameObject.Checked.ToString().ToLower());

            /* Logging */
            ini.Set("Logging", "UnityLogListening", form.UnityLogListening.Checked.ToString().ToLower());
            ini.Set("Logging", "LogConsoleToUnityLog", form.LogConsoleToUnityLog.Checked.ToString().ToLower());

            /* Logging.Console */
            ini.Set("Logging.Console", "Enabled", form.ConsoleEnabled.Checked.ToString().ToLower());
            ini.Set("Logging.Console", "PreventClose", form.PreventClose.Checked.ToString().ToLower());
            ini.Set("Logging.Console", "ShiftJisEncoding", form.ShiftJisEncoding.Checked.ToString().ToLower());

            /* Logging.Disk */
            ini.Set("Logging.Disk", "WriteUnityLog", form.WriteUnityLog.Checked.ToString().ToLower());
            ini.Set("Logging.Disk", "AppendLog", form.AppendLog.Checked.ToString().ToLower());
            ini.Set("Logging.Disk", "Enabled", form.DiskLogEnabled.Checked.ToString().ToLower());

            /* Preloader */
            ini.Set("Preloader", "ApplyRuntimePatches", form.ApplyRuntimePatches.Checked.ToString().ToLower());
            ini.Set("Preloader", "DumpAssemblies", form.DumpAssemblies.Checked.ToString().ToLower());
            ini.Set("Preloader", "LoadDumpedAssemblies", form.LoadDumpedAssemblies.Checked.ToString().ToLower());
            ini.Set("Preloader", "BreakBeforeLoadAssemblies", form.BreakBeforeLoadAssemblies.Checked.ToString().ToLower());
        }
    }
}
