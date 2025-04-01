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
    }
}
