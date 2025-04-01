using BepInExInstall;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BepInExInstall
{
    public static class GameInfoManager
    {
        public static void Load(string exePath)
        {
            GetInfoFiles.GetInfo(exePath);

            if (GameInfo.checkBepInEx)
            {
                string basePath = Path.GetDirectoryName(exePath);
                string iniPath = Path.Combine(basePath, "BepInEx", "config", "BepInEx.cfg");

                if (File.Exists(iniPath))
                {
                    GameInfo.checkBepInExLoaded = true;
                    var ini = new IniFile(iniPath);
                    var bepInExConfig = new BepInExConfig();
                    bepInExConfig.CreateBkpConfigFile(iniPath);
                    ini.GetAllConfigByFifle(bepInExConfig);
                    GameInfo.bepinex_config_data = bepInExConfig;
                    GameInfo.ini_file = ini;
                }
                else
                {
                    GameInfo.checkBepInExLoaded = false;
                }
            }
        }

        public static void ApplyToUI(Main form)
        {
            form.pictureBoxIcon.Image = GameInfo.icon?.ToBitmap();
            form.game_name.Text = GameInfo.game_name;
            form.game_architecture.Text = GameInfo.game_architecture;

            form.unity_version.Text = GameInfo.unity_version;
            form.unity_type.Text = GameInfo.unity_type;
            form.bepinex_status.Text = GameInfo.bepinex_status;
            form.bepinex_version.Text = GameInfo.bepinex_version;
            form.bepinex_loaded.Text = GameInfo.bepinex_loaded;
            form.bepinex_config.Text = GameInfo.bepinex_config;
            form.unityexplorer_loaded.Text = GameInfo.unityexplorer_loaded;
        }
    }
}
