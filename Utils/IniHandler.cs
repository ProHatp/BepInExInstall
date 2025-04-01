using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BepInExInstall
{
    public static class IniHandler
    {
        public static IniFile TryLoadIni(string exePath, out string iniPath)
        {
            iniPath = Path.Combine(Path.GetDirectoryName(exePath), "BepInEx", "config", "BepInEx.cfg");

            if (!File.Exists(iniPath))
            {
                GameInfo.checkBepInExLoaded = false;
                return null;
            }

            GameInfo.checkBepInExLoaded = true;
            return new IniFile(iniPath);
        }
    }
}
