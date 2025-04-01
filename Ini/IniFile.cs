using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

public class IniFile
{
    public Dictionary<string, Dictionary<string, string>> Data { get; private set; }

    public IniFile(string path)
    {
        Data = new Dictionary<string, Dictionary<string, string>>();
        Load(path);
    }

    public void Load(string path)
    {
        string currentSection = "";
        foreach (var line in File.ReadAllLines(path))
        {
            string trimmed = line.Trim();

            if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#"))
                continue;

            if (trimmed.StartsWith("[") && trimmed.EndsWith("]"))
            {
                currentSection = trimmed.Trim('[', ']');
                if (!Data.ContainsKey(currentSection))
                    Data[currentSection] = new Dictionary<string, string>();
            }
            else if (trimmed.Contains("="))
            {
                var parts = trimmed.Split(new[] { '=' }, 2);
                string key = parts[0].Trim();
                string value = parts[1].Trim();

                Data[currentSection][key] = value;
            }
        }
    }

    public void Save(string path)
    {
        if (!File.Exists(path))
        {
            MessageBox.Show("File already exists!");
            return;
        }

        using (var writer = new StreamWriter(path))
        {
            foreach (var section in Data)
            {
                writer.WriteLine($"[{section.Key}]");
                foreach (var kv in section.Value)
                    writer.WriteLine($"{kv.Key} = {kv.Value}");

                writer.WriteLine();
            }
        }
    }

    public string Get(string section, string key, string defaultValue = "")
    {
        return Data.ContainsKey(section) && Data[section].ContainsKey(key)
            ? Data[section][key]
            : defaultValue;
    }

    public void Set(string section, string key, string value)
    {
        if (!Data.ContainsKey(section))
            Data[section] = new Dictionary<string, string>();

        Data[section][key] = value;
    }

    public void GetAllConfigByFifle(BepInExConfig bepInExConfig)
    {
        if (this == null)
        {
            return;
        }

        /* Caching */
        bool.TryParse(this.Get("Caching", "EnableAssemblyCache"), out bepInExConfig.EnableAssemblyCache);

        /* Chainloader */
        bool.TryParse(this.Get("Chainloader", "HideManagerGameObject"), out bepInExConfig.HideManagerGameObject);

        /* Logging */
        bool.TryParse(this.Get("Logging", "UnityLogListening"), out bepInExConfig.UnityLogListening);
        bool.TryParse(this.Get("Logging", "LogConsoleToUnityLog"), out bepInExConfig.LogConsoleToUnityLog);

        /* Logging.Console */
        bool.TryParse(this.Get("Logging.Console", "Enabled"), out bepInExConfig.ConsoleEnabled);
        bool.TryParse(this.Get("Logging.Console", "PreventClose"), out bepInExConfig.PreventClose);
        bool.TryParse(this.Get("Logging.Console", "ShiftJisEncoding"), out bepInExConfig.ShiftJisEncoding);

        /* Logging.Disk */
        bool.TryParse(this.Get("Logging.Disk", "WriteUnityLog"), out bepInExConfig.WriteUnityLog);
        bool.TryParse(this.Get("Logging.Disk", "AppendLog"), out bepInExConfig.AppendLog);
        bool.TryParse(this.Get("Logging.Disk", "Enabled"), out bepInExConfig.DiskLogEnabled);

        /* Preloader */
        bool.TryParse(this.Get("Preloader", "ApplyRuntimePatches"), out bepInExConfig.ApplyRuntimePatches);
        bool.TryParse(this.Get("Preloader", "DumpAssemblies"), out bepInExConfig.DumpAssemblies);
        bool.TryParse(this.Get("Preloader", "LoadDumpedAssemblies"), out bepInExConfig.LoadDumpedAssemblies);
        bool.TryParse(this.Get("Preloader", "BreakBeforeLoadAssemblies"), out bepInExConfig.BreakBeforeLoadAssemblies);
    }
}
