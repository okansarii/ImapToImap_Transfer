using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImapMigrator.Models;
using ImapMigrator.Services;

namespace ImapMigrator.Forms
{
    public partial class MainForm : Form
    {
        private readonly MigrationStateRepository _repository;
        private readonly ImapMigrationEngine _engine;

        private CancellationTokenSource? _cts;
        private PauseTokenSource? _pts;
        private bool _isMigrating = false;

        public MainForm()
        {
            InitializeComponent();

            _repository = new MigrationStateRepository();
            _engine = new ImapMigrationEngine(_repository);
            _engine.OnLog += Engine_OnLog;

            WireEvents();
        }

        private void WireEvents()
        {
            btnTestSource.Click += async (s, e) => await TestConnectionAsync(GetSourceConfig());
            btnTestTarget.Click += async (s, e) => await TestConnectionAsync(GetTargetConfig());
            btnFetchFolders.Click += async (s, e) => await FetchFoldersAsync();
            btnSelectAllFolders.Click += (s, e) => ToggleAllFolders(true);
            btnUnselectAllFolders.Click += (s, e) => ToggleAllFolders(false);

            btnStart.Click += async (s, e) => await StartMigrationProcessAsync();
            btnPauseResume.Click += (s, e) => TogglePauseResume();
            btnStop.Click += (s, e) => StopMigrationProcess();
            btnClearLogs.Click += (s, e) => rtbLogs.Clear();
        }

        private ImapServerConfig GetSourceConfig()
        {
            return new ImapServerConfig
            {
                Host = txtSourceHost.Text.Trim(),
                Port = (int)numSourcePort.Value,
                UseSsl = chkSourceSsl.Checked,
                Email = txtSourceEmail.Text.Trim(),
                Password = txtSourcePass.Text.Trim()
            };
        }

        private ImapServerConfig GetTargetConfig()
        {
            return new ImapServerConfig
            {
                Host = txtTargetHost.Text.Trim(),
                Port = (int)numTargetPort.Value,
                UseSsl = chkTargetSsl.Checked,
                Email = txtTargetEmail.Text.Trim(),
                Password = txtTargetPass.Text.Trim()
            };
        }

        private async Task TestConnectionAsync(ImapServerConfig config)
        {
            if (string.IsNullOrEmpty(config.Host) || string.IsNullOrEmpty(config.Email) || string.IsNullOrEmpty(config.Password))
            {
                MessageBox.Show("Lütfen Host, E-Posta ve Şifre alanlarını eksiksiz doldurun.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnTestSource.Enabled = false;
            btnTestTarget.Enabled = false;

            try
            {
                bool success = await _engine.TestConnectionAsync(config);
                if (success)
                {
                    MessageBox.Show($"[{config.Host}] Bağlantı ve Kimlik Doğrulama Başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"[{config.Host}] Bağlantı Başarısız! Lütfen bilgileri kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                btnTestSource.Enabled = true;
                btnTestTarget.Enabled = true;
            }
        }

        private async Task FetchFoldersAsync()
        {
            var sourceConfig = GetSourceConfig();
            if (string.IsNullOrEmpty(sourceConfig.Email) || string.IsNullOrEmpty(sourceConfig.Password))
            {
                MessageBox.Show("Klasörleri çekebilmek için Kaynak (Yandex) E-Posta ve Uygulama Şifresini girin.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnFetchFolders.Enabled = false;
            dgvFolders.Rows.Clear();

            try
            {
                var folders = await _engine.GetFoldersAsync(sourceConfig);
                foreach (var f in folders)
                {
                    int rowIndex = dgvFolders.Rows.Add(f.Selected, f.SourceFolderName, f.TargetFolderName, f.TotalMessages);
                    dgvFolders.Rows[rowIndex].Tag = f;
                }

                topTabControl.SelectedTab = tabFolders;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Klasörler çekilirken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnFetchFolders.Enabled = true;
            }
        }

        private void ToggleAllFolders(bool select)
        {
            foreach (DataGridViewRow row in dgvFolders.Rows)
            {
                row.Cells[0].Value = select;
            }
        }

        private List<FolderMappingInfo> GetSelectedFolderMappings()
        {
            var list = new List<FolderMappingInfo>();
            foreach (DataGridViewRow row in dgvFolders.Rows)
            {
                bool selected = Convert.ToBoolean(row.Cells[0].Value);
                string source = Convert.ToString(row.Cells[1].Value) ?? string.Empty;
                string target = Convert.ToString(row.Cells[2].Value) ?? string.Empty;
                int count = Convert.ToInt32(row.Cells[3].Value);

                list.Add(new FolderMappingInfo
                {
                    Selected = selected,
                    SourceFolderName = source,
                    TargetFolderName = string.IsNullOrWhiteSpace(target) ? source : target.Trim(),
                    TotalMessages = count
                });
            }
            return list;
        }

        private async Task StartMigrationProcessAsync()
        {
            var sourceConfig = GetSourceConfig();
            var targetConfig = GetTargetConfig();

            if (string.IsNullOrEmpty(sourceConfig.Email) || string.IsNullOrEmpty(sourceConfig.Password) ||
                string.IsNullOrEmpty(targetConfig.Host) || string.IsNullOrEmpty(targetConfig.Email) || string.IsNullOrEmpty(targetConfig.Password))
            {
                MessageBox.Show("Lütfen hem Kaynak hem de Hedef sunucu bilgilerini eksiksiz girin.", "Eksik Ayarlar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedFolders = GetSelectedFolderMappings().Where(f => f.Selected).ToList();
            if (selectedFolders.Count == 0)
            {
                MessageBox.Show("Lütfen taşınacak en az 1 klasör seçin.", "Klasör Seçilmedi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                topTabControl.SelectedTab = tabFolders;
                return;
            }

            _cts = new CancellationTokenSource();
            _pts = new PauseTokenSource();
            _isMigrating = true;

            SetUiStateForMigration(true);

            var options = new MigrationOptions
            {
                DelayBetweenMailsMs = (int)numDelayMs.Value,
                SkipExisting = chkSkipExisting.Checked,
                PreserveFlags = chkPreserveFlags.Checked,
                PreserveInternalDate = chkPreserveDates.Checked
            };

            var progress = new Progress<MigrationProgressReport>(ReportProgressToUi);

            try
            {
                await _engine.StartMigrationAsync(
                    sourceConfig,
                    targetConfig,
                    selectedFolders,
                    options,
                    progress,
                    _pts.Token,
                    _cts.Token);

                MessageBox.Show("Taşıma işlemi başarıyla tamamlandı!", "Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                AppendLog("İşlem kullanıcı tarafından durduruldu.", LogLevel.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Taşıma sırasında kritik hata: {ex.Message}", "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isMigrating = false;
                SetUiStateForMigration(false);
            }
        }

        private void TogglePauseResume()
        {
            if (!_isMigrating || _pts == null) return;

            if (_pts.IsPaused)
            {
                _pts.Resume();
                btnPauseResume.Text = "⏸ Duraklat";
                btnPauseResume.BackColor = Color.DarkGoldenrod;
                AppendLog("İşlem DEVAM ETTİRİLDİ.", LogLevel.Info);
            }
            else
            {
                _pts.Pause();
                btnPauseResume.Text = "▶ Devam Et";
                btnPauseResume.BackColor = Color.DodgerBlue;
                AppendLog("İşlem DURAKLATILDI. Bekleniyor...", LogLevel.Warning);
            }
        }

        private void StopMigrationProcess()
        {
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                if (MessageBox.Show("Taşıma işlemini iptal etmek istediğinize emin misiniz?", "İptal Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _pts?.Resume();
                    _cts.Cancel();
                    btnStop.Enabled = false;
                }
            }
        }

        private void SetUiStateForMigration(bool migrating)
        {
            btnStart.Enabled = !migrating;
            btnPauseResume.Enabled = migrating;
            btnStop.Enabled = migrating;

            grpSource.Enabled = !migrating;
            grpTarget.Enabled = !migrating;
            grpOptions.Enabled = !migrating;
            btnFetchFolders.Enabled = !migrating;

            if (!migrating)
            {
                btnPauseResume.Text = "⏸ Duraklat";
                btnPauseResume.BackColor = Color.DarkGoldenrod;
            }
        }

        private void ReportProgressToUi(MigrationProgressReport report)
        {
            if (report.OverallTotal > 0)
            {
                int overallPct = Math.Min(100, (int)((double)report.OverallProcessed / report.OverallTotal * 100));
                pbOverall.Value = overallPct;
                lblOverallProgress.Text = $"Genel İlerleme: {report.OverallProcessed} / {report.OverallTotal} (%{overallPct})";
            }

            if (report.FolderTotalCount > 0)
            {
                int folderPct = Math.Min(100, (int)((double)report.FolderCurrentIndex / report.FolderTotalCount * 100));
                pbFolder.Value = folderPct;
                lblFolderProgress.Text = $"Klasör [{report.CurrentFolder}] İlerlemesi: {report.FolderCurrentIndex} / {report.FolderTotalCount} (%{folderPct})";
            }

            lblStatMigrated.Text = $"Aktarılan: {report.MigratedCount}";
            lblStatSkipped.Text = $"Atlanan (Resume): {report.SkippedCount}";
            lblStatFailed.Text = $"Hatalı: {report.FailedCount}";
            lblStatSpeed.Text = $"Transfer Hızı: {report.SpeedMailsPerSec} mail/s";

            lblStatusInfo.Text = $"İşleniyor [{report.CurrentFolder}]: {report.CurrentSubject}";
        }

        private void Engine_OnLog(object? sender, LogEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => AppendLog(e.Message, e.Level)));
            }
            else
            {
                AppendLog(e.Message, e.Level);
            }
        }

        private void AppendLog(string message, LogLevel level)
        {
            string timeStr = DateTime.Now.ToString("HH:mm:ss");
            string line = $"[{timeStr}] {message}\n";

            Color logColor = level switch
            {
                LogLevel.Success => Color.LimeGreen,
                LogLevel.Warning => Color.Orange,
                LogLevel.Error => Color.Crimson,
                _ => Color.Cyan
            };

            rtbLogs.SelectionStart = rtbLogs.TextLength;
            rtbLogs.SelectionLength = 0;
            rtbLogs.SelectionColor = logColor;
            rtbLogs.AppendText(line);
            rtbLogs.SelectionColor = rtbLogs.ForeColor;

            rtbLogs.ScrollToCaret();
        }
    }
}
