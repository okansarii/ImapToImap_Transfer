namespace ImapMigrator.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.topTabControl = new System.Windows.Forms.TabControl();
            
            // Tab 1: Credentials & Settings
            this.tabConnSettings = new System.Windows.Forms.TabPage();
            this.grpSource = new System.Windows.Forms.GroupBox();
            this.lblSourceHost = new System.Windows.Forms.Label();
            this.txtSourceHost = new System.Windows.Forms.TextBox();
            this.lblSourcePort = new System.Windows.Forms.Label();
            this.numSourcePort = new System.Windows.Forms.NumericUpDown();
            this.chkSourceSsl = new System.Windows.Forms.CheckBox();
            this.lblSourceEmail = new System.Windows.Forms.Label();
            this.txtSourceEmail = new System.Windows.Forms.TextBox();
            this.lblSourcePass = new System.Windows.Forms.Label();
            this.txtSourcePass = new System.Windows.Forms.TextBox();
            this.btnTestSource = new System.Windows.Forms.Button();

            this.grpTarget = new System.Windows.Forms.GroupBox();
            this.lblTargetHost = new System.Windows.Forms.Label();
            this.txtTargetHost = new System.Windows.Forms.TextBox();
            this.lblTargetPort = new System.Windows.Forms.Label();
            this.numTargetPort = new System.Windows.Forms.NumericUpDown();
            this.chkTargetSsl = new System.Windows.Forms.CheckBox();
            this.lblTargetEmail = new System.Windows.Forms.Label();
            this.txtTargetEmail = new System.Windows.Forms.TextBox();
            this.lblTargetPass = new System.Windows.Forms.Label();
            this.txtTargetPass = new System.Windows.Forms.TextBox();
            this.btnTestTarget = new System.Windows.Forms.Button();

            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.lblDelay = new System.Windows.Forms.Label();
            this.numDelayMs = new System.Windows.Forms.NumericUpDown();
            this.chkSkipExisting = new System.Windows.Forms.CheckBox();
            this.chkPreserveFlags = new System.Windows.Forms.CheckBox();
            this.chkPreserveDates = new System.Windows.Forms.CheckBox();

            // Tab 2: Folders
            this.tabFolders = new System.Windows.Forms.TabPage();
            this.btnFetchFolders = new System.Windows.Forms.Button();
            this.btnSelectAllFolders = new System.Windows.Forms.Button();
            this.btnUnselectAllFolders = new System.Windows.Forms.Button();
            this.dgvFolders = new System.Windows.Forms.DataGridView();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSourceFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTargetFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMsgCount = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // Bottom Panel: Control & Monitoring
            this.grpDashboard = new System.Windows.Forms.GroupBox();
            this.lblStatusInfo = new System.Windows.Forms.Label();
            this.lblOverallProgress = new System.Windows.Forms.Label();
            this.pbOverall = new System.Windows.Forms.ProgressBar();
            this.lblFolderProgress = new System.Windows.Forms.Label();
            this.pbFolder = new System.Windows.Forms.ProgressBar();

            this.pnlStats = new System.Windows.Forms.Panel();
            this.lblStatMigrated = new System.Windows.Forms.Label();
            this.lblStatSkipped = new System.Windows.Forms.Label();
            this.lblStatFailed = new System.Windows.Forms.Label();
            this.lblStatSpeed = new System.Windows.Forms.Label();

            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnPauseResume = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnClearLogs = new System.Windows.Forms.Button();

            this.rtbLogs = new System.Windows.Forms.RichTextBox();

            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.topTabControl.SuspendLayout();
            this.tabConnSettings.SuspendLayout();
            this.grpSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSourcePort)).BeginInit();
            this.grpTarget.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetPort)).BeginInit();
            this.grpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayMs)).BeginInit();
            this.tabFolders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFolders)).BeginInit();
            this.grpDashboard.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();

            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            this.mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            
            // Panel 1 (Top Tabs)
            this.mainSplitContainer.Panel1.Controls.Add(this.topTabControl);
            this.mainSplitContainer.Panel1MinSize = 320;
            
            // Panel 2 (Bottom Logs & Controls)
            this.mainSplitContainer.Panel2.Controls.Add(this.rtbLogs);
            this.mainSplitContainer.Panel2.Controls.Add(this.grpDashboard);
            this.mainSplitContainer.Size = new System.Drawing.Size(984, 761);
            this.mainSplitContainer.SplitterDistance = 350;
            this.mainSplitContainer.TabIndex = 0;

            // 
            // topTabControl
            // 
            this.topTabControl.Controls.Add(this.tabConnSettings);
            this.topTabControl.Controls.Add(this.tabFolders);
            this.topTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topTabControl.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.topTabControl.Location = new System.Drawing.Point(0, 0);
            this.topTabControl.Name = "topTabControl";
            this.topTabControl.SelectedIndex = 0;
            this.topTabControl.Size = new System.Drawing.Size(984, 350);
            this.topTabControl.TabIndex = 0;

            // 
            // tabConnSettings
            // 
            this.tabConnSettings.Controls.Add(this.grpSource);
            this.tabConnSettings.Controls.Add(this.grpTarget);
            this.tabConnSettings.Controls.Add(this.grpOptions);
            this.tabConnSettings.Location = new System.Drawing.Point(4, 30);
            this.tabConnSettings.Name = "tabConnSettings";
            this.tabConnSettings.Padding = new System.Windows.Forms.Padding(10);
            this.tabConnSettings.Size = new System.Drawing.Size(976, 316);
            this.tabConnSettings.TabIndex = 0;
            this.tabConnSettings.Text = "Sunucu & Bağlantı Ayarları";
            this.tabConnSettings.UseVisualStyleBackColor = true;

            // 
            // grpSource
            // 
            this.grpSource.Controls.Add(this.lblSourceHost);
            this.grpSource.Controls.Add(this.txtSourceHost);
            this.grpSource.Controls.Add(this.lblSourcePort);
            this.grpSource.Controls.Add(this.numSourcePort);
            this.grpSource.Controls.Add(this.chkSourceSsl);
            this.grpSource.Controls.Add(this.lblSourceEmail);
            this.grpSource.Controls.Add(this.txtSourceEmail);
            this.grpSource.Controls.Add(this.lblSourcePass);
            this.grpSource.Controls.Add(this.txtSourcePass);
            this.grpSource.Controls.Add(this.btnTestSource);
            this.grpSource.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpSource.ForeColor = System.Drawing.Color.Navy;
            this.grpSource.Location = new System.Drawing.Point(13, 10);
            this.grpSource.Name = "grpSource";
            this.grpSource.Size = new System.Drawing.Size(450, 200);
            this.grpSource.TabIndex = 0;
            this.grpSource.TabStop = false;
            this.grpSource.Text = "Kaynak Sunucu (Yandex IMAP)";

            // Source Host & Port & SSL
            this.lblSourceHost.AutoSize = true;
            this.lblSourceHost.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSourceHost.ForeColor = System.Drawing.Color.Black;
            this.lblSourceHost.Location = new System.Drawing.Point(15, 30);
            this.lblSourceHost.Text = "IMAP Host:";

            this.txtSourceHost.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSourceHost.Location = new System.Drawing.Point(100, 27);
            this.txtSourceHost.Size = new System.Drawing.Size(180, 23);
            this.txtSourceHost.Text = "imap.yandex.com";

            this.lblSourcePort.AutoSize = true;
            this.lblSourcePort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSourcePort.ForeColor = System.Drawing.Color.Black;
            this.lblSourcePort.Location = new System.Drawing.Point(290, 30);
            this.lblSourcePort.Text = "Port:";

            this.numSourcePort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numSourcePort.Location = new System.Drawing.Point(330, 27);
            this.numSourcePort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            this.numSourcePort.Value = new decimal(new int[] { 993, 0, 0, 0 });
            this.numSourcePort.Size = new System.Drawing.Size(55, 23);

            this.chkSourceSsl.AutoSize = true;
            this.chkSourceSsl.Checked = true;
            this.chkSourceSsl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkSourceSsl.ForeColor = System.Drawing.Color.Black;
            this.chkSourceSsl.Location = new System.Drawing.Point(392, 29);
            this.chkSourceSsl.Text = "SSL";

            // Source Email & Password
            this.lblSourceEmail.AutoSize = true;
            this.lblSourceEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSourceEmail.ForeColor = System.Drawing.Color.Black;
            this.lblSourceEmail.Location = new System.Drawing.Point(15, 65);
            this.lblSourceEmail.Text = "E-Posta:";

            this.txtSourceEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSourceEmail.Location = new System.Drawing.Point(100, 62);
            this.txtSourceEmail.Size = new System.Drawing.Size(330, 23);

            this.lblSourcePass.AutoSize = true;
            this.lblSourcePass.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSourcePass.ForeColor = System.Drawing.Color.Black;
            this.lblSourcePass.Location = new System.Drawing.Point(15, 100);
            this.lblSourcePass.Text = "Uyg. Şifresi:";

            this.txtSourcePass.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSourcePass.Location = new System.Drawing.Point(100, 97);
            this.txtSourcePass.PasswordChar = '*';
            this.txtSourcePass.Size = new System.Drawing.Size(330, 23);

            this.btnTestSource.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnTestSource.ForeColor = System.Drawing.Color.Black;
            this.btnTestSource.Location = new System.Drawing.Point(100, 140);
            this.btnTestSource.Size = new System.Drawing.Size(180, 32);
            this.btnTestSource.Text = "Kaynak Bağlantıyı Test Et";

            // 
            // grpTarget
            // 
            this.grpTarget.Controls.Add(this.lblTargetHost);
            this.grpTarget.Controls.Add(this.txtTargetHost);
            this.grpTarget.Controls.Add(this.lblTargetPort);
            this.grpTarget.Controls.Add(this.numTargetPort);
            this.grpTarget.Controls.Add(this.chkTargetSsl);
            this.grpTarget.Controls.Add(this.lblTargetEmail);
            this.grpTarget.Controls.Add(this.txtTargetEmail);
            this.grpTarget.Controls.Add(this.lblTargetPass);
            this.grpTarget.Controls.Add(this.txtTargetPass);
            this.grpTarget.Controls.Add(this.btnTestTarget);
            this.grpTarget.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpTarget.ForeColor = System.Drawing.Color.DarkGreen;
            this.grpTarget.Location = new System.Drawing.Point(480, 10);
            this.grpTarget.Name = "grpTarget";
            this.grpTarget.Size = new System.Drawing.Size(470, 200);
            this.grpTarget.TabIndex = 1;
            this.grpTarget.TabStop = false;
            this.grpTarget.Text = "Hedef Sunucu (Target IMAP)";

            // Target Controls
            this.lblTargetHost.AutoSize = true;
            this.lblTargetHost.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTargetHost.ForeColor = System.Drawing.Color.Black;
            this.lblTargetHost.Location = new System.Drawing.Point(15, 30);
            this.lblTargetHost.Text = "IMAP Host:";

            this.txtTargetHost.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtTargetHost.Location = new System.Drawing.Point(100, 27);
            this.txtTargetHost.Size = new System.Drawing.Size(190, 23);

            this.lblTargetPort.AutoSize = true;
            this.lblTargetPort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTargetPort.ForeColor = System.Drawing.Color.Black;
            this.lblTargetPort.Location = new System.Drawing.Point(300, 30);
            this.lblTargetPort.Text = "Port:";

            this.numTargetPort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numTargetPort.Location = new System.Drawing.Point(340, 27);
            this.numTargetPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            this.numTargetPort.Value = new decimal(new int[] { 993, 0, 0, 0 });
            this.numTargetPort.Size = new System.Drawing.Size(55, 23);

            this.chkTargetSsl.AutoSize = true;
            this.chkTargetSsl.Checked = true;
            this.chkTargetSsl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkTargetSsl.ForeColor = System.Drawing.Color.Black;
            this.chkTargetSsl.Location = new System.Drawing.Point(402, 29);
            this.chkTargetSsl.Text = "SSL";

            this.lblTargetEmail.AutoSize = true;
            this.lblTargetEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTargetEmail.ForeColor = System.Drawing.Color.Black;
            this.lblTargetEmail.Location = new System.Drawing.Point(15, 65);
            this.lblTargetEmail.Text = "E-Posta:";

            this.txtTargetEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtTargetEmail.Location = new System.Drawing.Point(100, 62);
            this.txtTargetEmail.Size = new System.Drawing.Size(350, 23);

            this.lblTargetPass.AutoSize = true;
            this.lblTargetPass.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTargetPass.ForeColor = System.Drawing.Color.Black;
            this.lblTargetPass.Location = new System.Drawing.Point(15, 100);
            this.lblTargetPass.Text = "Şifre:";

            this.txtTargetPass.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtTargetPass.Location = new System.Drawing.Point(100, 97);
            this.txtTargetPass.PasswordChar = '*';
            this.txtTargetPass.Size = new System.Drawing.Size(350, 23);

            this.btnTestTarget.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnTestTarget.ForeColor = System.Drawing.Color.Black;
            this.btnTestTarget.Location = new System.Drawing.Point(100, 140);
            this.btnTestTarget.Size = new System.Drawing.Size(180, 32);
            this.btnTestTarget.Text = "Hedef Bağlantıyı Test Et";

            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.lblDelay);
            this.grpOptions.Controls.Add(this.numDelayMs);
            this.grpOptions.Controls.Add(this.chkSkipExisting);
            this.grpOptions.Controls.Add(this.chkPreserveFlags);
            this.grpOptions.Controls.Add(this.chkPreserveDates);
            this.grpOptions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpOptions.Location = new System.Drawing.Point(13, 220);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(937, 80);
            this.grpOptions.TabIndex = 2;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Gelişmiş Transfer ve Performans Ayarları";

            this.lblDelay.AutoSize = true;
            this.lblDelay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDelay.Location = new System.Drawing.Point(15, 33);
            this.lblDelay.Text = "Mailler Arası Bekleme (ms):";

            this.numDelayMs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numDelayMs.Location = new System.Drawing.Point(170, 30);
            this.numDelayMs.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            this.numDelayMs.Value = new decimal(new int[] { 50, 0, 0, 0 });
            this.numDelayMs.Size = new System.Drawing.Size(65, 23);

            this.chkSkipExisting.AutoSize = true;
            this.chkSkipExisting.Checked = true;
            this.chkSkipExisting.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkSkipExisting.Location = new System.Drawing.Point(260, 32);
            this.chkSkipExisting.Text = "Daha Önce Aktarılanları Atla (SQLite Resume)";

            this.chkPreserveFlags.AutoSize = true;
            this.chkPreserveFlags.Checked = true;
            this.chkPreserveFlags.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkPreserveFlags.Location = new System.Drawing.Point(550, 32);
            this.chkPreserveFlags.Text = "Okundu / Bayrakları Koru";

            this.chkPreserveDates.AutoSize = true;
            this.chkPreserveDates.Checked = true;
            this.chkPreserveDates.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkPreserveDates.Location = new System.Drawing.Point(740, 32);
            this.chkPreserveDates.Text = "Orijinal Tarihleri Koru";

            // 
            // tabFolders
            // 
            this.tabFolders.Controls.Add(this.btnFetchFolders);
            this.tabFolders.Controls.Add(this.btnSelectAllFolders);
            this.tabFolders.Controls.Add(this.btnUnselectAllFolders);
            this.tabFolders.Controls.Add(this.dgvFolders);
            this.tabFolders.Location = new System.Drawing.Point(4, 30);
            this.tabFolders.Name = "tabFolders";
            this.tabFolders.Padding = new System.Windows.Forms.Padding(10);
            this.tabFolders.Size = new System.Drawing.Size(976, 316);
            this.tabFolders.TabIndex = 1;
            this.tabFolders.Text = "Klasör Seçimi ve Eşleme";
            this.tabFolders.UseVisualStyleBackColor = true;

            this.btnFetchFolders.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnFetchFolders.Location = new System.Drawing.Point(10, 10);
            this.btnFetchFolders.Size = new System.Drawing.Size(200, 35);
            this.btnFetchFolders.Text = "Klasörleri Kaynaktan Getir";

            this.btnSelectAllFolders.Location = new System.Drawing.Point(220, 10);
            this.btnSelectAllFolders.Size = new System.Drawing.Size(120, 35);
            this.btnSelectAllFolders.Text = "Tümünü Seç";

            this.btnUnselectAllFolders.Location = new System.Drawing.Point(350, 10);
            this.btnUnselectAllFolders.Size = new System.Drawing.Size(120, 35);
            this.btnUnselectAllFolders.Text = "Seçimleri Temizle";

            this.dgvFolders.AllowUserToAddRows = false;
            this.dgvFolders.AllowUserToDeleteRows = false;
            this.dgvFolders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFolders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFolders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colSelect,
                this.colSourceFolder,
                this.colTargetFolder,
                this.colMsgCount
            });
            this.dgvFolders.Location = new System.Drawing.Point(10, 55);
            this.dgvFolders.Name = "dgvFolders";
            this.dgvFolders.RowTemplate.Height = 25;
            this.dgvFolders.Size = new System.Drawing.Size(955, 245);
            this.dgvFolders.TabIndex = 3;

            this.colSelect.HeaderText = "Seç";
            this.colSelect.FillWeight = 15F;

            this.colSourceFolder.HeaderText = "Kaynak Klasör (Yandex)";
            this.colSourceFolder.ReadOnly = true;
            this.colSourceFolder.FillWeight = 40F;

            this.colTargetFolder.HeaderText = "Hedef Klasör Adı";
            this.colTargetFolder.FillWeight = 40F;

            this.colMsgCount.HeaderText = "Mail Sayısı";
            this.colMsgCount.ReadOnly = true;
            this.colMsgCount.FillWeight = 20F;

            // 
            // grpDashboard
            // 
            this.grpDashboard.Controls.Add(this.lblStatusInfo);
            this.grpDashboard.Controls.Add(this.lblOverallProgress);
            this.grpDashboard.Controls.Add(this.pbOverall);
            this.grpDashboard.Controls.Add(this.lblFolderProgress);
            this.grpDashboard.Controls.Add(this.pbFolder);
            this.grpDashboard.Controls.Add(this.pnlStats);
            this.grpDashboard.Controls.Add(this.pnlButtons);
            this.grpDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpDashboard.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpDashboard.Location = new System.Drawing.Point(0, 0);
            this.grpDashboard.Name = "grpDashboard";
            this.grpDashboard.Size = new System.Drawing.Size(984, 185);
            this.grpDashboard.TabIndex = 0;
            this.grpDashboard.TabStop = false;
            this.grpDashboard.Text = "Transfer Kontrol ve Durum Paneli";

            // Progress Controls
            this.lblOverallProgress.AutoSize = true;
            this.lblOverallProgress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOverallProgress.Location = new System.Drawing.Point(15, 25);
            this.lblOverallProgress.Text = "Genel İlerleme: 0 / 0 (%0)";

            this.pbOverall.Location = new System.Drawing.Point(15, 45);
            this.pbOverall.Size = new System.Drawing.Size(500, 20);

            this.lblFolderProgress.AutoSize = true;
            this.lblFolderProgress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFolderProgress.Location = new System.Drawing.Point(15, 70);
            this.lblFolderProgress.Text = "Aktif Klasör İlerlemesi: 0 / 0 (%0)";

            this.pbFolder.Location = new System.Drawing.Point(15, 90);
            this.pbFolder.Size = new System.Drawing.Size(500, 18);

            this.lblStatusInfo.AutoSize = true;
            this.lblStatusInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblStatusInfo.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblStatusInfo.Location = new System.Drawing.Point(15, 113);
            this.lblStatusInfo.Size = new System.Drawing.Size(500, 18);
            this.lblStatusInfo.Text = "Hazır.";

            // Buttons Panel
            this.pnlButtons.Controls.Add(this.btnStart);
            this.pnlButtons.Controls.Add(this.btnPauseResume);
            this.pnlButtons.Controls.Add(this.btnStop);
            this.pnlButtons.Controls.Add(this.btnClearLogs);
            this.pnlButtons.Location = new System.Drawing.Point(15, 135);
            this.pnlButtons.Size = new System.Drawing.Size(950, 42);

            this.btnStart.BackColor = System.Drawing.Color.ForestGreen;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(0, 3);
            this.btnStart.Size = new System.Drawing.Size(160, 36);
            this.btnStart.Text = "▶ Transferi Başlat";

            this.btnPauseResume.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnPauseResume.Enabled = false;
            this.btnPauseResume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPauseResume.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPauseResume.ForeColor = System.Drawing.Color.White;
            this.btnPauseResume.Location = new System.Drawing.Point(170, 3);
            this.btnPauseResume.Size = new System.Drawing.Size(140, 36);
            this.btnPauseResume.Text = "⏸ Duraklat";

            this.btnStop.BackColor = System.Drawing.Color.Crimson;
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(320, 3);
            this.btnStop.Size = new System.Drawing.Size(130, 36);
            this.btnStop.Text = "⏹ İptal Et";

            this.btnClearLogs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnClearLogs.Location = new System.Drawing.Point(820, 3);
            this.btnClearLogs.Size = new System.Drawing.Size(130, 36);
            this.btnClearLogs.Text = "Logları Temizle";

            // Stats Panel
            this.pnlStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStats.Controls.Add(this.lblStatMigrated);
            this.pnlStats.Controls.Add(this.lblStatSkipped);
            this.pnlStats.Controls.Add(this.lblStatFailed);
            this.pnlStats.Controls.Add(this.lblStatSpeed);
            this.pnlStats.Location = new System.Drawing.Point(530, 25);
            this.pnlStats.Size = new System.Drawing.Size(435, 100);

            this.lblStatMigrated.AutoSize = true;
            this.lblStatMigrated.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatMigrated.ForeColor = System.Drawing.Color.Green;
            this.lblStatMigrated.Location = new System.Drawing.Point(15, 12);
            this.lblStatMigrated.Text = "Aktarılan: 0";

            this.lblStatSkipped.AutoSize = true;
            this.lblStatSkipped.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatSkipped.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblStatSkipped.Location = new System.Drawing.Point(210, 12);
            this.lblStatSkipped.Text = "Atlanan (Resume): 0";

            this.lblStatFailed.AutoSize = true;
            this.lblStatFailed.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatFailed.ForeColor = System.Drawing.Color.Red;
            this.lblStatFailed.Location = new System.Drawing.Point(15, 55);
            this.lblStatFailed.Text = "Hatalı: 0";

            this.lblStatSpeed.AutoSize = true;
            this.lblStatSpeed.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatSpeed.ForeColor = System.Drawing.Color.DarkViolet;
            this.lblStatSpeed.Location = new System.Drawing.Point(210, 55);
            this.lblStatSpeed.Text = "Transfer Hızı: 0 mail/s";

            // 
            // rtbLogs
            // 
            this.rtbLogs.BackColor = System.Drawing.Color.Black;
            this.rtbLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLogs.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbLogs.ForeColor = System.Drawing.Color.LimeGreen;
            this.rtbLogs.Location = new System.Drawing.Point(0, 185);
            this.rtbLogs.Name = "rtbLogs";
            this.rtbLogs.ReadOnly = true;
            this.rtbLogs.Size = new System.Drawing.Size(984, 226);
            this.rtbLogs.TabIndex = 1;
            this.rtbLogs.Text = "";

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.mainSplitContainer);
            this.MinimumSize = new System.Drawing.Size(1000, 800);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImapMigrator - Yandex & High-Volume IMAP Mail Migration Engine";

            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.topTabControl.ResumeLayout(false);
            this.tabConnSettings.ResumeLayout(false);
            this.grpSource.ResumeLayout(false);
            this.grpSource.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSourcePort)).EndInit();
            this.grpTarget.ResumeLayout(false);
            this.grpTarget.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetPort)).EndInit();
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayMs)).EndInit();
            this.tabFolders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFolders)).EndInit();
            this.grpDashboard.ResumeLayout(false);
            this.grpDashboard.PerformLayout();
            this.pnlStats.ResumeLayout(false);
            this.pnlStats.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.TabControl topTabControl;
        private System.Windows.Forms.TabPage tabConnSettings;
        private System.Windows.Forms.TabPage tabFolders;

        private System.Windows.Forms.GroupBox grpSource;
        private System.Windows.Forms.Label lblSourceHost;
        private System.Windows.Forms.TextBox txtSourceHost;
        private System.Windows.Forms.Label lblSourcePort;
        private System.Windows.Forms.NumericUpDown numSourcePort;
        private System.Windows.Forms.CheckBox chkSourceSsl;
        private System.Windows.Forms.Label lblSourceEmail;
        private System.Windows.Forms.TextBox txtSourceEmail;
        private System.Windows.Forms.Label lblSourcePass;
        private System.Windows.Forms.TextBox txtSourcePass;
        private System.Windows.Forms.Button btnTestSource;

        private System.Windows.Forms.GroupBox grpTarget;
        private System.Windows.Forms.Label lblTargetHost;
        private System.Windows.Forms.TextBox txtTargetHost;
        private System.Windows.Forms.Label lblTargetPort;
        private System.Windows.Forms.NumericUpDown numTargetPort;
        private System.Windows.Forms.CheckBox chkTargetSsl;
        private System.Windows.Forms.Label lblTargetEmail;
        private System.Windows.Forms.TextBox txtTargetEmail;
        private System.Windows.Forms.Label lblTargetPass;
        private System.Windows.Forms.TextBox txtTargetPass;
        private System.Windows.Forms.Button btnTestTarget;

        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.NumericUpDown numDelayMs;
        private System.Windows.Forms.CheckBox chkSkipExisting;
        private System.Windows.Forms.CheckBox chkPreserveFlags;
        private System.Windows.Forms.CheckBox chkPreserveDates;

        private System.Windows.Forms.Button btnFetchFolders;
        private System.Windows.Forms.Button btnSelectAllFolders;
        private System.Windows.Forms.Button btnUnselectAllFolders;
        private System.Windows.Forms.DataGridView dgvFolders;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSourceFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTargetFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMsgCount;

        private System.Windows.Forms.GroupBox grpDashboard;
        private System.Windows.Forms.Label lblOverallProgress;
        private System.Windows.Forms.ProgressBar pbOverall;
        private System.Windows.Forms.Label lblFolderProgress;
        private System.Windows.Forms.ProgressBar pbFolder;
        private System.Windows.Forms.Label lblStatusInfo;

        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.Label lblStatMigrated;
        private System.Windows.Forms.Label lblStatSkipped;
        private System.Windows.Forms.Label lblStatFailed;
        private System.Windows.Forms.Label lblStatSpeed;

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnPauseResume;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnClearLogs;

        private System.Windows.Forms.RichTextBox rtbLogs;
    }
}
