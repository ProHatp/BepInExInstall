using BepInExInstall;
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

namespace BepInExInstall
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
                return;
            
            BepInExConfigManager.UpdateFromUI(bepInExConfig, this);
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
            try
            {
                UIManager.ShowProgress(ProgressBarDownload, labelStatus, "Downloading files...");

                bool success = await BepInExManager.InstallBepInEx(
                    exePath,
                    progress => UIManager.UpdateProgress(ProgressBarDownload, labelStatus, progress, $"Downloading... {progress}%"),
                    status => labelStatus.Text = status
                );

                if (success)
                {
                    LoadedGame();
                    if (GameInfo.bepinex_version_enum != VersionsBepInEx.v6_0_0)
                        OpenFileAndWaitStart();
                }
            }
            catch (Exception ex)
            {
                UIManager.ShowError($"Erro: {ex.Message}");
            }
            finally
            {
                UIManager.HideProgress(ProgressBarDownload, labelStatus);
            }
        }

        private async void DownloadUnityExplorer()
        {
            try
            {
                UIManager.ShowProgress(ProgressBarDownload, labelStatus, "Downloading files...");

                bool success = await UnityExplorerManager.InstallUnityExplorer(
                    exePath,
                    progress => UIManager.UpdateProgress(ProgressBarDownload, labelStatus, progress, $"Downloading... {progress}%"),
                    status => labelStatus.Text = status
                );

                if (success)
                {
                    LoadedGame();
                }
            }
            catch (Exception ex)
            {
                UIManager.ShowError($"Erro: {ex.Message}");
            }
            finally
            {
                UIManager.HideProgress(ProgressBarDownload, labelStatus);
            }
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

        private void LoadedGame()
        {
            GameInfoManager.Load(exePath);
            GameInfoManager.ApplyToUI(this);
            LoadAvailableVersions();

            if (GameInfo.checkBepInExLoaded && GameInfo.ini_file != null)
            {
                bepInExConfig = GameInfo.bepinex_config_data;
                ini = GameInfo.ini_file;
                BepInExConfigManager.ApplyToUI(bepInExConfig, this);
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
                        BepInExConfigManager.Reset(this);
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
            BepInExManager.UninstallBepInEx(exePath);
            BepInExConfigManager.Reset(this);
            LoadedGame();
            labelStatus.Text = "BepInEx Removed Successfully!";
        }

        private void InstallUnityExplorer_Click(object sender, EventArgs e)
        {
            DownloadUnityExplorer();
        }

        private void UnistallUnityExplorer_Click(object sender, EventArgs e)
        {
            UnityExplorerManager.UninstallUnityExplorer(exePath);
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
                    BepInExConfigManager.Reset(this);
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
            BepInExManager.UninstallBepInEx(exePath);
            BepInExConfigManager.Reset(this);
            LoadedGame();
            labelStatus.Text = "BepInEx Removed Successfully!";
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
            UnityExplorerManager.UninstallUnityExplorer(exePath);
            LoadedGame();
            labelStatus.Text = "Unity Explorer Removed Successfully!";
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
                    BepInExConfigManager.Reset(this);
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
