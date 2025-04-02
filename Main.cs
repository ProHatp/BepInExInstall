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
            GameLoader.CheckForUpdates();
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

        private void CheckBox_Changed(object sender, EventArgs e)
        {
            if (!GameInfo.isLoadingConfig) 
                return;

            if (ini == null)
                return;

            BepInExConfigManager.UpdateFromUI(bepInExConfig, this);
            BepInExConfigManager.UpdatConfigByFile(ini, this);
            ini.Save(GameInfo.iniPath);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            RegisterCheckBoxEvents();
        }

        private async void StartGame()
        {
            if (!File.Exists(exePath)) return;

            string exeName  = Path.GetFileName(exePath);
            string appId    = GameLoader.TryGetAppIdFromSteamTxt(exePath) ?? GameLoader.TryGetAppIdFromExePath(exePath);

            if (!string.IsNullOrEmpty(appId))
            {
                try
                {
                    labelStatus.Text = $"Launching: {exeName}";
                    Process.Start($"steam://run/{appId}");
                    await Task.Delay(5000);
                    LoadedGame();
                    labelStatus.Text = "";
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to start via Steam AppID: {ex.Message}");
                }
            }

            try
            {
                labelStatus.Text = $"Launching: {exeName}";
                GameInfo.gameProcess.StartInfo.FileName = exePath;
                GameInfo.gameProcess.Start();
                await Task.Delay(5000);
                labelStatus.Text = "";
                LoadedGame();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start game from EXE: {ex.Message}");
            }
        }

        private void ExitGame()
        {
            if (!File.Exists(exePath)) return;

            string exeName = Path.GetFileName(exePath);

            try
            {
                if (GameInfo.gameProcess != null && !GameInfo.gameProcess.HasExited)
                {
                    GameInfo.gameProcess.Kill();
                    labelStatus.Text = $"Game closed: {exeName}";
                    LoadedGame();
                    return;
                }
            }
            catch
            {

            }

            if (GameInfo.gameProcess != null)
            {
                string name     = Path.GetFileNameWithoutExtension(exePath);
                var processes   = Process.GetProcessesByName(name);

                foreach (var proc in processes)
                {
                    try
                    {
                        proc.Kill();
                        labelStatus.Text = $"Game closed: {exeName}";
                        LoadedGame();
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void UnistallBepInExFunc()
        {
            if (!File.Exists(exePath)) return;

            if(GameInfo.checkUnity)
            {
                BepInExManager.UninstallBepInEx(exePath);
                BepInExConfigManager.Reset(this);
                LoadedGame();
                labelStatus.Text = "BepInEx Removed Successfully!";
            } else
            {
                labelStatus.Text = "This game is not Unity!";
            }

        }

        private void UnistallUnityExplorerFunc()
        {
            if (!File.Exists(exePath)) return;

            if (GameInfo.checkUnity)
            {
                UnityExplorerManager.UninstallUnityExplorer(exePath);
                LoadedGame();
                labelStatus.Text = "Unity Explorer Removed Successfully!";
            } else
            {
                labelStatus.Text = "This game is not Unity!";
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
            if (!File.Exists(exePath)) return;
            if (!GameInfo.checkUnity)
            {
                labelStatus.Text = "This game is not Unity!";
                return;
            }

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
            if (!File.Exists(exePath)) return;
            if (!GameInfo.checkUnity)
            {
                labelStatus.Text = "This game is not Unity!";
                return;
            }

            if (!GameInfo.checkBepInEx)
            {
                labelStatus.Text = "This game does not have BepInEx!";
                return;
            }

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

        private void LoadedGame()
        {
            if (!File.Exists(exePath)) return;
            GameInfo.isLoadingConfig = false;

            GameInfoManager.Load(exePath);
            GameInfoManager.ApplyToUI(this);
            LoadAvailableVersions();

            if (GameInfo.checkBepInExLoaded && GameInfo.ini_file != null)
            {
                bepInExConfig = GameInfo.bepinex_config_data;
                ini = GameInfo.ini_file;
                BepInExConfigManager.ApplyToUI(bepInExConfig, this);
            }

            GameInfo.isLoadingConfig = true;
        }

        private void LoadGameInit()
        {
            LoadedGame();
            if (!GameInfo.checkBepInExLoaded && GameInfo.checkBepInEx)
            {
                BepInExConfigManager.Reset(this);
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
                    LoadGameInit();
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
            UnistallBepInExFunc();
        }

        private void InstallUnityExplorer_Click(object sender, EventArgs e)
        {
            DownloadUnityExplorer();
        }

        private void UnistallUnityExplorer_Click(object sender, EventArgs e)
        {
            UnistallUnityExplorerFunc();
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
                LoadGameInit();
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
            UnistallBepInExFunc();
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
            UnistallUnityExplorerFunc();
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
                LoadGameInit();
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

        private void DiscordStripMenuItem_Click(object sender, EventArgs e)
        {
            GameLoader.OpenDiscordGroup();
        }

        private void GithubRepositoryStripMenuItem_Click(object sender, EventArgs e)
        {
            GameLoader.OpenGitHubRepository();
        }
    }
}
