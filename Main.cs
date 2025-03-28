using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectToolsBepInEx
{
    public partial class Main: MetroFramework.Forms.MetroForm
    {
        private string iniPath;
        private string exePath;
        private IniFile ini                                 = null;
        public BepInExConfig bepInExConfig                  = new BepInExConfig();
        public UnityExplorerConfig unityExplorerConfig      = new UnityExplorerConfig();

        public Main()
        {
            InitializeComponent();
            LoadAvailableVersions();

            this.Resizable = false;
            this.MaximizeBox = false;

            //this.Style = MetroColorStyle.Green;
            //this.Theme = MetroThemeStyle.Dark;
        }

        private void LoadAvailableVersions()
        {
            ComboBepInExVersions.Items.Clear();

            UnityType unityType = GameInfo.unityType;

            var allVersions = Enum.GetValues(typeof(VersionsBepInEx)).Cast<VersionsBepInEx>();

            foreach (var version in allVersions)
            {
                if (version == VersionsBepInEx.Unknown)
                    continue;

                var key = (version, unityType, GameInfo.arcteture);
                if (DownloadFiles.VersionedBepInExLinks.ContainsKey(key))
                {
                    ComboBepInExVersions.Items.Add(version);
                }
            }

            if (ComboBepInExVersions.Items.Count > 0)
            {
                ComboBepInExVersions.SelectedIndex = 0;
                GameInfo.selected_version = (VersionsBepInEx)ComboBepInExVersions.SelectedItem;
            }
        }

        private void RegisterCheckBoxEvents()
        {
            foreach (Control group in this.Controls)
            {
                if (group is GroupBox)
                {
                    foreach (Control ctrl in group.Controls)
                    {
                        if (ctrl is CheckBox chk)
                        {
                            chk.CheckedChanged += CheckBox_Changed;
                        }
                    }
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            RegisterCheckBoxEvents();
        }

        private void CheckBox_Changed(object sender, EventArgs e)
        {
            if (ini == null)
            {
                return;
            }

            UpdatConfigByFile();
            ini.Save(iniPath);
        }

        private async void StartGame()
        {
            GameInfo.gameProcess.StartInfo.FileName             = exePath;
            GameInfo.gameProcess.Start();
            await Task.Delay(3000);
            LoadedGame();
        }

        private void ExitGame()
        {
            try
            {
                if (GameInfo.gameProcess != null && !GameInfo.gameProcess.HasExited)
                {
                    GameInfo.gameProcess.Kill();
                    return;
                }
            }
            catch
            {

            }

            if (GameInfo.gameProcess != null)
            {
                string name = Path.GetFileNameWithoutExtension(exePath);
                var processes = Process.GetProcessesByName(name);

                foreach (var proc in processes)
                {
                    try
                    {
                        proc.Kill();
                    }
                    catch
                    {

                    }
                }
            }
        }

        private async void OpenFileAndWaitStart()
        {
            try
            {
                GameInfo.gameProcess.StartInfo.FileName = exePath;
                GameInfo.gameProcess.Start();

                await Task.Run(() =>
                {
                    while (GameInfo.gameProcess.MainWindowHandle == IntPtr.Zero)
                    {
                        GameInfo.gameProcess.Refresh();
                        Thread.Sleep(2000);
                    }

                    if (!GameInfo.gameProcess.HasExited)
                    {
                        GameInfo.gameProcess.Kill();  
                    }
                        
                });

                LoadedGame();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening file: {ex.Message}");
            }
        }

        private async void DownloadBepInEx()
        {
            if (GameInfo.unityType == UnityType.Unknown || GameInfo.arcteture == Arcteture.Unknown || GameInfo.selected_version == VersionsBepInEx.Unknown)
                return;

            string targetFolder = Path.GetDirectoryName(exePath);
            string zipUrl       = DownloadFiles.GetBepInExDownloadUrl();
            string tempZipPath  = Path.Combine(Path.GetTempPath(), "bepinex.zip");

            try
            {
                labelStatus.Text        = "Downloading files...";
                ProgressBarDownload.Value      = 0;
                ProgressBarDownload.Visible    = true;
                labelStatus.Visible     = true;

                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += (s, e) =>
                    {
                        ProgressBarDownload.Value = e.ProgressPercentage;
                        labelStatus.Text = $"Downloading... {e.ProgressPercentage}%";
                    };

                    await client.DownloadFileTaskAsync(new Uri(zipUrl), tempZipPath);
                }

                labelStatus.Text = "Extracting files...";

                await Task.Run(() => DownloadFiles.ExtractZipOverwrite(tempZipPath, targetFolder));

                labelStatus.Text = "BepInEx downloaded and extracted successfully!";

                LoadedGame();

                if (GameInfo.bepinex_version_enum != VersionsBepInEx.v6_0_0)
                {
                    OpenFileAndWaitStart();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressBarDownload.Value = 0;
            }
        }

        private async void DownloadUnityExplorer()
        {
            if (GameInfo.unityType == UnityType.Unknown || GameInfo.arcteture == Arcteture.Unknown || GameInfo.bepinex_version_enum == VersionsBepInEx.Unknown)
                return;

            string targetFolder         = Path.Combine(Path.GetDirectoryName(exePath), "BepInEx");
            string zipUrl               = DownloadFiles.GetUnityExplorerUrl();

            string tempZipPath = Path.Combine(Path.GetTempPath(), "unityexplorer.zip");

            try
            {
                labelStatus.Text        = "Downloading files...";
                ProgressBarDownload.Value      = 0;
                ProgressBarDownload.Visible    = true;
                labelStatus.Visible     = true;

                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += (s, e) =>
                    {
                        ProgressBarDownload.Value = e.ProgressPercentage;
                        labelStatus.Text = $"Downloading... {e.ProgressPercentage}%";
                    };

                    await client.DownloadFileTaskAsync(new Uri(zipUrl), tempZipPath);
                }

                labelStatus.Text = "Extracting files...";

                await Task.Run(() => DownloadFiles.ExtractZipOverwrite(tempZipPath, targetFolder));

                labelStatus.Text = "Unity Explorer downloaded and extracted successfully!";

                LoadedGame();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressBarDownload.Value = 0;
            }
        }

        private void ResetConfig()
        {
            EnableAssemblyCache.Checked             = false;
            HideManagerGameObject.Checked           = false;
            UnityLogListening.Checked               = false;
            LogConsoleToUnityLog.Checked            = false;
            ConsoleEnabled.Checked                  = false;
            PreventClose.Checked                    = false;
            ShiftJisEncoding.Checked                = false;
            WriteUnityLog.Checked                   = false;
            AppendLog.Checked                       = false;
            DiskLogEnabled.Checked                  = false;
            ApplyRuntimePatches.Checked             = false;
            DumpAssemblies.Checked                  = false;
            LoadDumpedAssemblies.Checked            = false;
            BreakBeforeLoadAssemblies.Checked       = false;
        }

        private void SetInfoGame()
        {
            pictureBoxIcon.Image        = GameInfo.icon?.ToBitmap();
            game_name.Text              = GameInfo.game_name;
            game_architecture.Text      = GameInfo.game_architecture;

            unity_version.Text          = GameInfo.unity_version;
            unity_type.Text             = GameInfo.unity_type;
            bepinex_status.Text         = GameInfo.bepinex_status;
            bepinex_version.Text        = GameInfo.bepinex_version;

            bepinex_loaded.Text         = GameInfo.bepinex_loaded;
            bepinex_config.Text         = GameInfo.bepinex_config;
            unityexplorer_loaded.Text   = GameInfo.unityexplorer_loaded;
        }

        private void UpdatConfigByFile()
        {
            if (ini == null)
            {
                return;
            }

            /* Caching */
            ini.Set("Caching", "EnableAssemblyCache", EnableAssemblyCache.Checked.ToString().ToLower());

            /* Chainloader */
            ini.Set("Chainloader", "HideManagerGameObject", HideManagerGameObject.Checked.ToString().ToLower());

            /* Logging */
            ini.Set("Logging", "UnityLogListening", UnityLogListening.Checked.ToString().ToLower());
            ini.Set("Logging", "LogConsoleToUnityLog", LogConsoleToUnityLog.Checked.ToString().ToLower());

            /* Logging.Console */
            ini.Set("Logging.Console", "Enabled", ConsoleEnabled.Checked.ToString().ToLower());
            ini.Set("Logging.Console", "PreventClose", PreventClose.Checked.ToString().ToLower());
            ini.Set("Logging.Console", "ShiftJisEncoding", ShiftJisEncoding.Checked.ToString().ToLower());

            /* Logging.Disk */
            ini.Set("Logging.Disk", "WriteUnityLog", WriteUnityLog.Checked.ToString().ToLower());
            ini.Set("Logging.Disk", "AppendLog", AppendLog.Checked.ToString().ToLower());
            ini.Set("Logging.Disk", "Enabled", DiskLogEnabled.Checked.ToString().ToLower());

            /* Preloader */
            ini.Set("Preloader", "ApplyRuntimePatches", ApplyRuntimePatches.Checked.ToString().ToLower());
            ini.Set("Preloader", "DumpAssemblies", DumpAssemblies.Checked.ToString().ToLower());
            ini.Set("Preloader", "LoadDumpedAssemblies", LoadDumpedAssemblies.Checked.ToString().ToLower());
            ini.Set("Preloader", "BreakBeforeLoadAssemblies", BreakBeforeLoadAssemblies.Checked.ToString().ToLower());
        }

        private void SetConfigBepInEx()
        {
            /* Caching */
            EnableAssemblyCache.Checked = bepInExConfig.EnableAssemblyCache;

            /* Chainloader */
            HideManagerGameObject.Checked = bepInExConfig.HideManagerGameObject;

            /* Logging */
            UnityLogListening.Checked = bepInExConfig.UnityLogListening;
            LogConsoleToUnityLog.Checked = bepInExConfig.LogConsoleToUnityLog;

            /* Logging.Console */
            ConsoleEnabled.Checked = bepInExConfig.ConsoleEnabled;
            PreventClose.Checked = bepInExConfig.PreventClose;
            ShiftJisEncoding.Checked = bepInExConfig.ShiftJisEncoding;

            /* Logging.Disk */
            WriteUnityLog.Checked = bepInExConfig.WriteUnityLog;
            AppendLog.Checked = bepInExConfig.AppendLog;
            DiskLogEnabled.Checked = bepInExConfig.DiskLogEnabled;

            /* Preloader */
            ApplyRuntimePatches.Checked = bepInExConfig.ApplyRuntimePatches;
            DumpAssemblies.Checked = bepInExConfig.DumpAssemblies;
            LoadDumpedAssemblies.Checked = bepInExConfig.LoadDumpedAssemblies;
            BreakBeforeLoadAssemblies.Checked = bepInExConfig.BreakBeforeLoadAssemblies;
        }

        private void LoadedGame()
        {
            GetInfoFiles.GetInfo(exePath);
            SetInfoGame();
            LoadAvailableVersions();

            if (GameInfo.checkBepInEx)
            {
                string basePath = Path.GetDirectoryName(exePath);
                iniPath = Path.Combine(basePath, "BepInEx", "config", "BepInEx.cfg");

                if (!File.Exists(iniPath))
                {
                    GameInfo.checkBepInExLoaded = false;
                    return;
                }
                else
                {
                    GameInfo.checkBepInExLoaded = true;
                }

                bepInExConfig.CreateBkpConfigFile(iniPath);

                ini = new IniFile(iniPath);
                ini.GetAllConfigByFifle(bepInExConfig);
                SetConfigBepInEx();
            }
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0 && Path.GetExtension(files[0]).ToLower() == ".exe")
                {
                    exePath = files[0];
                    LoadedGame();

                    if (!GameInfo.checkBepInExLoaded && GameInfo.checkBepInEx)
                    {
                        ResetConfig();
                    }
                }
            }
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0 && Path.GetExtension(files[0]).ToLower() == ".exe")
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        private void InstallBepInEx_Click(object sender, EventArgs e)
        {
            DownloadBepInEx();
        }

        private void UnistallBepInEx_Click(object sender, EventArgs e)
        {
            bepInExConfig.RemoveBepInEx(exePath);
            bepInExConfig.DeletePreloaderFiles(exePath);
            ResetConfig();
            LoadedGame();
            labelStatus.Text = "BepInEx Removed Successfully!";
        }

        private void InstallUnityExplorer_Click(object sender, EventArgs e)
        {
            DownloadUnityExplorer();
        }

        private void UnistallUnityExplorer_Click(object sender, EventArgs e)
        {
            unityExplorerConfig.RemoverUnityExplorer(exePath);
            LoadedGame();
            labelStatus.Text = "Unity Explorer Removed Successfully!";
        }

        private void OpenGame_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void CloseGame_Click(object sender, EventArgs e)
        {
            ExitGame();
        }

        private void SelectGame_Click(object sender, EventArgs e)
        {
            OpenFileDialog folder = new OpenFileDialog
            {
                Filter = "Executable Files|*.exe"
            };

            if (folder.ShowDialog() == DialogResult.OK)
            {
                exePath = folder.FileName;
                LoadedGame();

                if (!GameInfo.checkBepInExLoaded && GameInfo.checkBepInEx)
                {
                    ResetConfig();
                }
            }
        }

        private void ComboBepInExVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBepInExVersions.SelectedItem is VersionsBepInEx selected)
            {
                GameInfo.selected_version = selected;
            }
        }

        private void RestartGame_Click(object sender, EventArgs e)
        {
            ExitGame();
            StartGame();
        }

        private void InstallBepInExToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadBepInEx();
        }

        private void UnistallBepInExToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bepInExConfig.RemoveBepInEx(exePath);
            bepInExConfig.DeletePreloaderFiles(exePath);
            ResetConfig();
            LoadedGame();
        }

        private void OpenRepositoriyBepInExStripMenuItem_Click(object sender, EventArgs e)
        {
            bepInExConfig.OpenRepository();
        }

        private void InstallUnityExplorerStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadUnityExplorer();
        }

        private void UnistallUnityExplorerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            unityExplorerConfig.RemoverUnityExplorer(exePath);
            LoadedGame();
        }

        private void OpenRepositoryUnityExplorerStripMenuItem1_Click(object sender, EventArgs e)
        {
            unityExplorerConfig.OpenRepository();
        }

        private void SelectGameStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog folder = new OpenFileDialog
            {
                Filter = "Executable Files|*.exe"
            };

            if (folder.ShowDialog() == DialogResult.OK)
            {
                exePath = folder.FileName;
                LoadedGame();

                if (!GameInfo.checkBepInExLoaded && GameInfo.checkBepInEx)
                {
                    ResetConfig();
                }
            }
        }

        private void OpenGameStripMenuItem_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void CloseGameStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitGame();
        }

        private void RestartGameStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitGame();
            StartGame();
        }

        private void ReloadBepInExConfigStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadedGame();
        }

        private void OpenGameFolderStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(exePath))
            {
                string folderPath = Path.GetDirectoryName(exePath);
                Process.Start("explorer.exe", folderPath);
            }
            else
            {
                MessageBox.Show("File not found!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
