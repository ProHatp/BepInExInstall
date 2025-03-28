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
    public partial class Main: Form
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
        }

        private void LoadAvailableVersions()
        {
            comboBepInExVersions.Items.Clear();

            UnityType unityType = GameInfo.unityType;

            var allVersions = Enum.GetValues(typeof(VersionsBepInEx)).Cast<VersionsBepInEx>();

            foreach (var version in allVersions)
            {
                if (version == VersionsBepInEx.Unknown)
                    continue;

                var key = (version, unityType, GameInfo.arcteture);
                if (DownloadFiles.VersionedBepInExLinks.ContainsKey(key))
                {
                    comboBepInExVersions.Items.Add(version);
                }
            }

            if (comboBepInExVersions.Items.Count > 0)
            {
                comboBepInExVersions.SelectedIndex = 0;
                GameInfo.selected_version = (VersionsBepInEx)comboBepInExVersions.SelectedItem;
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

        private void StartGame()
        {
            GameInfo.gameProcess.StartInfo.FileName             = exePath;
            GameInfo.gameProcess.Start();
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
                MessageBox.Show($"Erro ao abrir o arquivo: {ex.Message}");
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
                SelectGame.Enabled = false;
                SelectGame.Text = "Baixando...";
                labelStatus.Text = "Baixando arquivos...";
                progressBar1.Value = 0;
                progressBar1.Visible = true;
                labelStatus.Visible = true;

                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += (s, e) =>
                    {
                        progressBar1.Value = e.ProgressPercentage;
                        labelStatus.Text = $"Baixando... {e.ProgressPercentage}%";
                    };

                    await client.DownloadFileTaskAsync(new Uri(zipUrl), tempZipPath);
                }

                labelStatus.Text = "Extraindo arquivos...";
                SelectGame.Text = "Extraindo...";

                await Task.Run(() => DownloadFiles.ExtractZipOverwrite(tempZipPath, targetFolder));

                MessageBox.Show("BepInEx baixado e extraído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OpenFileAndWaitStart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SelectGame.Enabled = true;
                SelectGame.Text = "Selecionar Pasta";
                progressBar1.Visible = false;
                labelStatus.Visible = false;
            }
        }

        private async void DownloadUnityExplorer()
        {
            if (GameInfo.unityType == UnityType.Unknown || GameInfo.arcteture == Arcteture.Unknown || GameInfo.selected_version == VersionsBepInEx.Unknown)
                return;

            string targetFolder         = Path.GetDirectoryName(exePath);
            string zipUrl               = DownloadFiles.GetUnityExplorerUrl();
            string tempZipPath          = Path.Combine(Path.GetTempPath(), "unityexplorer.zip");

            try
            {
                SelectGame.Enabled = false;
                SelectGame.Text = "Baixando...";
                labelStatus.Text = "Baixando arquivos...";
                progressBar1.Value = 0;
                progressBar1.Visible = true;
                labelStatus.Visible = true;

                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += (s, e) =>
                    {
                        progressBar1.Value = e.ProgressPercentage;
                        labelStatus.Text = $"Baixando... {e.ProgressPercentage}%";
                    };

                    await client.DownloadFileTaskAsync(new Uri(zipUrl), tempZipPath);
                }

                labelStatus.Text = "Extraindo arquivos...";
                SelectGame.Text = "Extraindo...";

                await Task.Run(() => DownloadFiles.ExtractZipOverwrite(tempZipPath, targetFolder));

                MessageBox.Show("UnityExplorer baixado e extraído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OpenFileAndWaitStart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SelectGame.Enabled = true;
                SelectGame.Text = "Selecionar Pasta";
                progressBar1.Visible = false;
                labelStatus.Visible = false;
            }
        }

        private void ResetConfig()
        {
            EnableAssemblyCache.Checked = false;
            HideManagerGameObject.Checked = false;
            UnityLogListening.Checked = false;
            LogConsoleToUnityLog.Checked = false;
            ConsoleEnabled.Checked = false;
            PreventClose.Checked = false;
            ShiftJisEncoding.Checked = false;
            WriteUnityLog.Checked = false;
            AppendLog.Checked = false;
            DiskLogEnabled.Checked = false;
            ApplyRuntimePatches.Checked = false;
            DumpAssemblies.Checked = false;
            LoadDumpedAssemblies.Checked = false;
            BreakBeforeLoadAssemblies.Checked = false;
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
            ResetConfig();
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

        private void SelectGame_Click(object sender, EventArgs e)
        {
            OpenFileDialog folder   = new OpenFileDialog();
            folder.Filter           = "Executable Files|*.exe";

            if (folder.ShowDialog() == DialogResult.OK)
            {
                exePath = folder.FileName;
                LoadedGame();

                if(!GameInfo.checkBepInExLoaded && GameInfo.checkBepInEx)
                {
                    var resultado = MessageBox.Show("DO YOU WANT TO CHARGE BEPINEX?", "INFORMATION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        OpenFileAndWaitStart();
                    }
                }
            }
        }

        private void DownLoadBepIn_Click(object sender, EventArgs e)
        {
            DownloadBepInEx();
        }

        private void OpenGame_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void CloseGame_Click(object sender, EventArgs e)
        {
            GameInfo.gameProcess.Kill();
        }

        private void UnistallBepInEx_Click(object sender, EventArgs e)
        {
            bepInExConfig.RemoveBepInEx(exePath);
            LoadedGame();
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
                        var resultado = MessageBox.Show("DO YOU WANT TO CHARGE BEPINEX?", "INFORMATION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (resultado == DialogResult.Yes)
                        {
                            OpenFileAndWaitStart();
                        }
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

        private void comboBepInExVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBepInExVersions.SelectedItem is VersionsBepInEx selected)
            {
                GameInfo.selected_version = selected;
            }
        }

        private void InstallUnityExplorer_Click(object sender, EventArgs e)
        {
            DownloadUnityExplorer();
        }

        private void UnistallUnityExplorer_Click(object sender, EventArgs e)
        {
            unityExplorerConfig.RemoverUnityExplorer(exePath);
        }
    }
}
