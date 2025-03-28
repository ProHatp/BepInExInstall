namespace ProjectToolsBepInEx
{
    partial class Main
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.SelectGame = new System.Windows.Forms.Button();
            this.EnableAssemblyCache = new System.Windows.Forms.CheckBox();
            this.HideManagerGameObject = new System.Windows.Forms.CheckBox();
            this.UnityLogListening = new System.Windows.Forms.CheckBox();
            this.LogConsoleToUnityLog = new System.Windows.Forms.CheckBox();
            this.PreventClose = new System.Windows.Forms.CheckBox();
            this.ShiftJisEncoding = new System.Windows.Forms.CheckBox();
            this.WriteUnityLog = new System.Windows.Forms.CheckBox();
            this.AppendLog = new System.Windows.Forms.CheckBox();
            this.ApplyRuntimePatches = new System.Windows.Forms.CheckBox();
            this.DumpAssemblies = new System.Windows.Forms.CheckBox();
            this.LoadDumpedAssemblies = new System.Windows.Forms.CheckBox();
            this.BreakBeforeLoadAssemblies = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ConsoleEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.DiskLogEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.game_architecture = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.unity_type = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.game_name = new System.Windows.Forms.Label();
            this.unity_version = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bepinex_status = new System.Windows.Forms.Label();
            this.bepinex_version = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.bepinex_loaded = new System.Windows.Forms.Label();
            this.bepinex_config = new System.Windows.Forms.Label();
            this.DownLoadBepIn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelStatus = new System.Windows.Forms.Label();
            this.OpenGame = new System.Windows.Forms.Button();
            this.CloseGame = new System.Windows.Forms.Button();
            this.UnistallBepInEx = new System.Windows.Forms.Button();
            this.comboBepInExVersions = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.unityexplorer_loaded = new System.Windows.Forms.Label();
            this.InstallUnityExplorer = new System.Windows.Forms.Button();
            this.UnistallUnityExplorer = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SelectGame
            // 
            this.SelectGame.Location = new System.Drawing.Point(6, 455);
            this.SelectGame.Name = "SelectGame";
            this.SelectGame.Size = new System.Drawing.Size(191, 29);
            this.SelectGame.TabIndex = 0;
            this.SelectGame.Text = "SELECIONE O JOGO";
            this.SelectGame.UseVisualStyleBackColor = true;
            this.SelectGame.Click += new System.EventHandler(this.SelectGame_Click);
            // 
            // EnableAssemblyCache
            // 
            this.EnableAssemblyCache.AutoSize = true;
            this.EnableAssemblyCache.Location = new System.Drawing.Point(6, 19);
            this.EnableAssemblyCache.Name = "EnableAssemblyCache";
            this.EnableAssemblyCache.Size = new System.Drawing.Size(140, 17);
            this.EnableAssemblyCache.TabIndex = 1;
            this.EnableAssemblyCache.Text = "Enable Assembly Cache";
            this.EnableAssemblyCache.UseVisualStyleBackColor = true;
            // 
            // HideManagerGameObject
            // 
            this.HideManagerGameObject.AutoSize = true;
            this.HideManagerGameObject.Location = new System.Drawing.Point(6, 23);
            this.HideManagerGameObject.Name = "HideManagerGameObject";
            this.HideManagerGameObject.Size = new System.Drawing.Size(158, 17);
            this.HideManagerGameObject.TabIndex = 2;
            this.HideManagerGameObject.Text = "Hide Manager Game Object";
            this.HideManagerGameObject.UseVisualStyleBackColor = true;
            // 
            // UnityLogListening
            // 
            this.UnityLogListening.AutoSize = true;
            this.UnityLogListening.Location = new System.Drawing.Point(6, 19);
            this.UnityLogListening.Name = "UnityLogListening";
            this.UnityLogListening.Size = new System.Drawing.Size(116, 17);
            this.UnityLogListening.TabIndex = 4;
            this.UnityLogListening.Text = "Unity Log Listening";
            this.UnityLogListening.UseVisualStyleBackColor = true;
            // 
            // LogConsoleToUnityLog
            // 
            this.LogConsoleToUnityLog.AutoSize = true;
            this.LogConsoleToUnityLog.Location = new System.Drawing.Point(6, 42);
            this.LogConsoleToUnityLog.Name = "LogConsoleToUnityLog";
            this.LogConsoleToUnityLog.Size = new System.Drawing.Size(149, 17);
            this.LogConsoleToUnityLog.TabIndex = 5;
            this.LogConsoleToUnityLog.Text = "Log Console To Unity Log";
            this.LogConsoleToUnityLog.UseVisualStyleBackColor = true;
            // 
            // PreventClose
            // 
            this.PreventClose.AutoSize = true;
            this.PreventClose.Location = new System.Drawing.Point(6, 42);
            this.PreventClose.Name = "PreventClose";
            this.PreventClose.Size = new System.Drawing.Size(92, 17);
            this.PreventClose.TabIndex = 6;
            this.PreventClose.Text = "Prevent Close";
            this.PreventClose.UseVisualStyleBackColor = true;
            // 
            // ShiftJisEncoding
            // 
            this.ShiftJisEncoding.AutoSize = true;
            this.ShiftJisEncoding.Location = new System.Drawing.Point(6, 65);
            this.ShiftJisEncoding.Name = "ShiftJisEncoding";
            this.ShiftJisEncoding.Size = new System.Drawing.Size(110, 17);
            this.ShiftJisEncoding.TabIndex = 7;
            this.ShiftJisEncoding.Text = "Shift Jis Encoding";
            this.ShiftJisEncoding.UseVisualStyleBackColor = true;
            // 
            // WriteUnityLog
            // 
            this.WriteUnityLog.AutoSize = true;
            this.WriteUnityLog.Location = new System.Drawing.Point(6, 46);
            this.WriteUnityLog.Name = "WriteUnityLog";
            this.WriteUnityLog.Size = new System.Drawing.Size(99, 17);
            this.WriteUnityLog.TabIndex = 10;
            this.WriteUnityLog.Text = "Write Unity Log";
            this.WriteUnityLog.UseVisualStyleBackColor = true;
            // 
            // AppendLog
            // 
            this.AppendLog.AutoSize = true;
            this.AppendLog.Location = new System.Drawing.Point(6, 69);
            this.AppendLog.Name = "AppendLog";
            this.AppendLog.Size = new System.Drawing.Size(84, 17);
            this.AppendLog.TabIndex = 11;
            this.AppendLog.Text = "Append Log";
            this.AppendLog.UseVisualStyleBackColor = true;
            // 
            // ApplyRuntimePatches
            // 
            this.ApplyRuntimePatches.AutoSize = true;
            this.ApplyRuntimePatches.Location = new System.Drawing.Point(6, 19);
            this.ApplyRuntimePatches.Name = "ApplyRuntimePatches";
            this.ApplyRuntimePatches.Size = new System.Drawing.Size(136, 17);
            this.ApplyRuntimePatches.TabIndex = 12;
            this.ApplyRuntimePatches.Text = "Apply Runtime Patches";
            this.ApplyRuntimePatches.UseVisualStyleBackColor = true;
            // 
            // DumpAssemblies
            // 
            this.DumpAssemblies.AutoSize = true;
            this.DumpAssemblies.Location = new System.Drawing.Point(6, 42);
            this.DumpAssemblies.Name = "DumpAssemblies";
            this.DumpAssemblies.Size = new System.Drawing.Size(109, 17);
            this.DumpAssemblies.TabIndex = 13;
            this.DumpAssemblies.Text = "Dump Assemblies";
            this.DumpAssemblies.UseVisualStyleBackColor = true;
            // 
            // LoadDumpedAssemblies
            // 
            this.LoadDumpedAssemblies.AutoSize = true;
            this.LoadDumpedAssemblies.Location = new System.Drawing.Point(6, 65);
            this.LoadDumpedAssemblies.Name = "LoadDumpedAssemblies";
            this.LoadDumpedAssemblies.Size = new System.Drawing.Size(148, 17);
            this.LoadDumpedAssemblies.TabIndex = 14;
            this.LoadDumpedAssemblies.Text = "Load Dumped Assemblies";
            this.LoadDumpedAssemblies.UseVisualStyleBackColor = true;
            // 
            // BreakBeforeLoadAssemblies
            // 
            this.BreakBeforeLoadAssemblies.AutoSize = true;
            this.BreakBeforeLoadAssemblies.Location = new System.Drawing.Point(6, 88);
            this.BreakBeforeLoadAssemblies.Name = "BreakBeforeLoadAssemblies";
            this.BreakBeforeLoadAssemblies.Size = new System.Drawing.Size(170, 17);
            this.BreakBeforeLoadAssemblies.TabIndex = 15;
            this.BreakBeforeLoadAssemblies.Text = "Break Before Load Assemblies";
            this.BreakBeforeLoadAssemblies.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.EnableAssemblyCache);
            this.groupBox1.Location = new System.Drawing.Point(523, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 120);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Caching";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.HideManagerGameObject);
            this.groupBox2.Location = new System.Drawing.Point(523, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(273, 120);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chainloader";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(523, 264);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(273, 120);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Harmony Logger";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.UnityLogListening);
            this.groupBox4.Controls.Add(this.LogConsoleToUnityLog);
            this.groupBox4.Location = new System.Drawing.Point(523, 390);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(273, 120);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Logging";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ConsoleEnabled);
            this.groupBox5.Controls.Add(this.PreventClose);
            this.groupBox5.Controls.Add(this.ShiftJisEncoding);
            this.groupBox5.Location = new System.Drawing.Point(802, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(273, 120);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Logging Console";
            // 
            // ConsoleEnabled
            // 
            this.ConsoleEnabled.AutoSize = true;
            this.ConsoleEnabled.Location = new System.Drawing.Point(6, 19);
            this.ConsoleEnabled.Name = "ConsoleEnabled";
            this.ConsoleEnabled.Size = new System.Drawing.Size(59, 17);
            this.ConsoleEnabled.TabIndex = 18;
            this.ConsoleEnabled.Text = "Enable";
            this.ConsoleEnabled.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.WriteUnityLog);
            this.groupBox6.Controls.Add(this.AppendLog);
            this.groupBox6.Controls.Add(this.DiskLogEnabled);
            this.groupBox6.Location = new System.Drawing.Point(802, 138);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(273, 120);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Logging Disk";
            // 
            // DiskLogEnabled
            // 
            this.DiskLogEnabled.AutoSize = true;
            this.DiskLogEnabled.Location = new System.Drawing.Point(6, 23);
            this.DiskLogEnabled.Name = "DiskLogEnabled";
            this.DiskLogEnabled.Size = new System.Drawing.Size(59, 17);
            this.DiskLogEnabled.TabIndex = 19;
            this.DiskLogEnabled.Text = "Enable";
            this.DiskLogEnabled.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.ApplyRuntimePatches);
            this.groupBox7.Controls.Add(this.DumpAssemblies);
            this.groupBox7.Controls.Add(this.LoadDumpedAssemblies);
            this.groupBox7.Controls.Add(this.BreakBeforeLoadAssemblies);
            this.groupBox7.Location = new System.Drawing.Point(802, 264);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(273, 120);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Preloader";
            // 
            // groupBox8
            // 
            this.groupBox8.Location = new System.Drawing.Point(802, 390);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(273, 120);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Preloader Entrypoint";
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxIcon.Location = new System.Drawing.Point(3, 12);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(103, 89);
            this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxIcon.TabIndex = 18;
            this.pictureBoxIcon.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 25);
            this.label1.TabIndex = 19;
            this.label1.Text = "Game:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 20;
            this.label3.Text = "BepInEx";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 23);
            this.label2.TabIndex = 21;
            this.label2.Text = "Unity Version:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetPartial;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 897F));
            this.tableLayoutPanel1.Controls.Add(this.game_architecture, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.unity_type, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.game_name, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.unity_version, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.bepinex_status, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.bepinex_version, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.bepinex_loaded, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.bepinex_config, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.unityexplorer_loaded, 1, 9);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(112, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 11;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(396, 273);
            this.tableLayoutPanel1.TabIndex = 22;
            // 
            // game_architecture
            // 
            this.game_architecture.Location = new System.Drawing.Point(129, 87);
            this.game_architecture.Name = "game_architecture";
            this.game_architecture.Size = new System.Drawing.Size(279, 20);
            this.game_architecture.TabIndex = 34;
            this.game_architecture.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 20);
            this.label8.TabIndex = 33;
            this.label8.Text = "Architecture:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // unity_type
            // 
            this.unity_type.Location = new System.Drawing.Point(129, 59);
            this.unity_type.Name = "unity_type";
            this.unity_type.Size = new System.Drawing.Size(279, 23);
            this.unity_type.TabIndex = 28;
            this.unity_type.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 23);
            this.label5.TabIndex = 27;
            this.label5.Text = "Unity Type:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // game_name
            // 
            this.game_name.Location = new System.Drawing.Point(129, 3);
            this.game_name.Name = "game_name";
            this.game_name.Size = new System.Drawing.Size(279, 25);
            this.game_name.TabIndex = 22;
            this.game_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // unity_version
            // 
            this.unity_version.Location = new System.Drawing.Point(129, 31);
            this.unity_version.Name = "unity_version";
            this.unity_version.Size = new System.Drawing.Size(279, 23);
            this.unity_version.TabIndex = 24;
            this.unity_version.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 20);
            this.label4.TabIndex = 25;
            this.label4.Text = "BepInEx Version";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bepinex_status
            // 
            this.bepinex_status.Location = new System.Drawing.Point(129, 138);
            this.bepinex_status.Name = "bepinex_status";
            this.bepinex_status.Size = new System.Drawing.Size(279, 20);
            this.bepinex_status.TabIndex = 23;
            this.bepinex_status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bepinex_version
            // 
            this.bepinex_version.Location = new System.Drawing.Point(129, 161);
            this.bepinex_version.Name = "bepinex_version";
            this.bepinex_version.Size = new System.Drawing.Size(279, 20);
            this.bepinex_version.TabIndex = 26;
            this.bepinex_version.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 20);
            this.label6.TabIndex = 29;
            this.label6.Text = "BepInEx Loaded";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 20);
            this.label7.TabIndex = 30;
            this.label7.Text = "BepInEx Config";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bepinex_loaded
            // 
            this.bepinex_loaded.Location = new System.Drawing.Point(129, 184);
            this.bepinex_loaded.Name = "bepinex_loaded";
            this.bepinex_loaded.Size = new System.Drawing.Size(279, 20);
            this.bepinex_loaded.TabIndex = 31;
            this.bepinex_loaded.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bepinex_config
            // 
            this.bepinex_config.Location = new System.Drawing.Point(129, 207);
            this.bepinex_config.Name = "bepinex_config";
            this.bepinex_config.Size = new System.Drawing.Size(279, 20);
            this.bepinex_config.TabIndex = 32;
            this.bepinex_config.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DownLoadBepIn
            // 
            this.DownLoadBepIn.Location = new System.Drawing.Point(317, 299);
            this.DownLoadBepIn.Name = "DownLoadBepIn";
            this.DownLoadBepIn.Size = new System.Drawing.Size(191, 29);
            this.DownLoadBepIn.TabIndex = 23;
            this.DownLoadBepIn.Text = "INSTALL BEPINEX";
            this.DownLoadBepIn.UseVisualStyleBackColor = true;
            this.DownLoadBepIn.Click += new System.EventHandler(this.DownLoadBepIn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 487);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(511, 23);
            this.progressBar1.TabIndex = 25;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(203, 471);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 13);
            this.labelStatus.TabIndex = 26;
            // 
            // OpenGame
            // 
            this.OpenGame.Location = new System.Drawing.Point(112, 420);
            this.OpenGame.Name = "OpenGame";
            this.OpenGame.Size = new System.Drawing.Size(191, 29);
            this.OpenGame.TabIndex = 27;
            this.OpenGame.Text = "OPEN GAME";
            this.OpenGame.UseVisualStyleBackColor = true;
            this.OpenGame.Click += new System.EventHandler(this.OpenGame_Click);
            // 
            // CloseGame
            // 
            this.CloseGame.Location = new System.Drawing.Point(317, 420);
            this.CloseGame.Name = "CloseGame";
            this.CloseGame.Size = new System.Drawing.Size(191, 29);
            this.CloseGame.TabIndex = 28;
            this.CloseGame.Text = "CLOSE GAME";
            this.CloseGame.UseVisualStyleBackColor = true;
            this.CloseGame.Click += new System.EventHandler(this.CloseGame_Click);
            // 
            // UnistallBepInEx
            // 
            this.UnistallBepInEx.Location = new System.Drawing.Point(317, 334);
            this.UnistallBepInEx.Name = "UnistallBepInEx";
            this.UnistallBepInEx.Size = new System.Drawing.Size(191, 29);
            this.UnistallBepInEx.TabIndex = 29;
            this.UnistallBepInEx.Text = "UNISTALL BEPINEX";
            this.UnistallBepInEx.UseVisualStyleBackColor = true;
            this.UnistallBepInEx.Click += new System.EventHandler(this.UnistallBepInEx_Click);
            // 
            // comboBepInExVersions
            // 
            this.comboBepInExVersions.FormattingEnabled = true;
            this.comboBepInExVersions.Location = new System.Drawing.Point(112, 302);
            this.comboBepInExVersions.Name = "comboBepInExVersions";
            this.comboBepInExVersions.Size = new System.Drawing.Size(199, 21);
            this.comboBepInExVersions.TabIndex = 30;
            this.comboBepInExVersions.SelectedIndexChanged += new System.EventHandler(this.comboBepInExVersions_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(6, 230);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 20);
            this.label9.TabIndex = 35;
            this.label9.Text = "Unity Explorer Loaded";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // unityexplorer_loaded
            // 
            this.unityexplorer_loaded.Location = new System.Drawing.Point(129, 230);
            this.unityexplorer_loaded.Name = "unityexplorer_loaded";
            this.unityexplorer_loaded.Size = new System.Drawing.Size(279, 20);
            this.unityexplorer_loaded.TabIndex = 36;
            this.unityexplorer_loaded.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // InstallUnityExplorer
            // 
            this.InstallUnityExplorer.Location = new System.Drawing.Point(112, 385);
            this.InstallUnityExplorer.Name = "InstallUnityExplorer";
            this.InstallUnityExplorer.Size = new System.Drawing.Size(191, 29);
            this.InstallUnityExplorer.TabIndex = 31;
            this.InstallUnityExplorer.Text = "INSTALL UNITY EXPLORER";
            this.InstallUnityExplorer.UseVisualStyleBackColor = true;
            this.InstallUnityExplorer.Click += new System.EventHandler(this.InstallUnityExplorer_Click);
            // 
            // UnistallUnityExplorer
            // 
            this.UnistallUnityExplorer.Location = new System.Drawing.Point(317, 385);
            this.UnistallUnityExplorer.Name = "UnistallUnityExplorer";
            this.UnistallUnityExplorer.Size = new System.Drawing.Size(191, 29);
            this.UnistallUnityExplorer.TabIndex = 32;
            this.UnistallUnityExplorer.Text = "UNISTALL UNITY EXPLORER";
            this.UnistallUnityExplorer.UseVisualStyleBackColor = true;
            this.UnistallUnityExplorer.Click += new System.EventHandler(this.UnistallUnityExplorer_Click);
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1082, 516);
            this.Controls.Add(this.UnistallUnityExplorer);
            this.Controls.Add(this.InstallUnityExplorer);
            this.Controls.Add(this.comboBepInExVersions);
            this.Controls.Add(this.UnistallBepInEx);
            this.Controls.Add(this.CloseGame);
            this.Controls.Add(this.OpenGame);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.DownLoadBepIn);
            this.Controls.Add(this.pictureBoxIcon);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SelectGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Main";
            this.Text = "Project Tools BepInEx";
            this.Load += new System.EventHandler(this.Main_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Main_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Main_DragEnter);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SelectGame;
        private System.Windows.Forms.CheckBox EnableAssemblyCache;
        private System.Windows.Forms.CheckBox HideManagerGameObject;
        private System.Windows.Forms.CheckBox UnityLogListening;
        private System.Windows.Forms.CheckBox LogConsoleToUnityLog;
        private System.Windows.Forms.CheckBox PreventClose;
        private System.Windows.Forms.CheckBox ShiftJisEncoding;
        private System.Windows.Forms.CheckBox WriteUnityLog;
        private System.Windows.Forms.CheckBox AppendLog;
        private System.Windows.Forms.CheckBox ApplyRuntimePatches;
        private System.Windows.Forms.CheckBox DumpAssemblies;
        private System.Windows.Forms.CheckBox LoadDumpedAssemblies;
        private System.Windows.Forms.CheckBox BreakBeforeLoadAssemblies;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox ConsoleEnabled;
        private System.Windows.Forms.CheckBox DiskLogEnabled;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label unity_version;
        private System.Windows.Forms.Label bepinex_status;
        private System.Windows.Forms.Label game_name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label unity_type;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label bepinex_version;
        private System.Windows.Forms.Button DownLoadBepIn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label bepinex_loaded;
        private System.Windows.Forms.Label bepinex_config;
        private System.Windows.Forms.Button OpenGame;
        private System.Windows.Forms.Button CloseGame;
        private System.Windows.Forms.Button UnistallBepInEx;
        private System.Windows.Forms.Label game_architecture;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBepInExVersions;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label unityexplorer_loaded;
        private System.Windows.Forms.Button InstallUnityExplorer;
        private System.Windows.Forms.Button UnistallUnityExplorer;
    }
}

