using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using ImapMigrator.Models;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using MimeKit;

namespace ImapMigrator.Services
{
    public class ImapMigrationEngine
    {
        private readonly MigrationStateRepository _repository;

        public event EventHandler<LogEventArgs>? OnLog;

        public ImapMigrationEngine(MigrationStateRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        private void EmitLog(string message, LogLevel level = LogLevel.Info)
        {
            OnLog?.Invoke(this, new LogEventArgs(message, level));
        }

        public async Task<bool> TestConnectionAsync(ImapServerConfig config, CancellationToken cancellationToken = default)
        {
            using var client = new ImapClient();
            try
            {
                EmitLog($"[{config.Host}] Sunucusuna bağlanılıyor...", LogLevel.Info);
                SecureSocketOptions sslOptions = config.UseSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.Auto;

                await client.ConnectAsync(config.Host, config.Port, sslOptions, cancellationToken).ConfigureAwait(false);
                EmitLog($"[{config.Host}] Bağlantı başarılı. Kimlik doğrulanıyor ({config.Email})...", LogLevel.Info);

                await client.AuthenticateAsync(config.Email, config.Password, cancellationToken).ConfigureAwait(false);
                EmitLog($"[{config.Host}] Kimlik doğrulama BAŞARILI.", LogLevel.Success);

                await client.DisconnectAsync(true, cancellationToken).ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                EmitLog($"[{config.Host}] Bağlantı Hatanız: {ex.Message}", LogLevel.Error);
                return false;
            }
        }

        public async Task<List<FolderMappingInfo>> GetFoldersAsync(ImapServerConfig config, CancellationToken cancellationToken = default)
        {
            var folderList = new List<FolderMappingInfo>();
            using var client = new ImapClient();

            try
            {
                EmitLog($"[{config.Host}] Klasör listesi alınıyor...", LogLevel.Info);
                SecureSocketOptions sslOptions = config.UseSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.Auto;

                await client.ConnectAsync(config.Host, config.Port, sslOptions, cancellationToken).ConfigureAwait(false);
                await client.AuthenticateAsync(config.Email, config.Password, cancellationToken).ConfigureAwait(false);

                var personalFolder = client.GetFolder(client.PersonalNamespaces[0]);
                var subFolders = await personalFolder.GetSubfoldersAsync(false, cancellationToken).ConfigureAwait(false);

                foreach (var folder in subFolders)
                {
                    if ((folder.Attributes & FolderAttributes.NoSelect) != 0)
                        continue;

                    await folder.OpenAsync(FolderAccess.ReadOnly, cancellationToken).ConfigureAwait(false);
                    int messageCount = folder.Count;
                    await folder.CloseAsync(false, cancellationToken).ConfigureAwait(false);

                    folderList.Add(new FolderMappingInfo
                    {
                        SourceFolderName = folder.FullName,
                        TargetFolderName = MapTargetFolderName(folder.FullName),
                        Selected = true,
                        TotalMessages = messageCount
                    });
                }

                await client.DisconnectAsync(true, cancellationToken).ConfigureAwait(false);
                EmitLog($"Toplam {folderList.Count} adet klasör başarıyla listelendi.", LogLevel.Success);
            }
            catch (Exception ex)
            {
                EmitLog($"Klasörler alınırken hata oluştu: {ex.Message}", LogLevel.Error);
                throw;
            }

            return folderList;
        }

        private string MapTargetFolderName(string sourceFolderName)
        {
            if (string.Equals(sourceFolderName, "INBOX", StringComparison.OrdinalIgnoreCase))
                return "INBOX";

            return sourceFolderName;
        }

        private async Task<IMailFolder> EnsureFolderOpenAsync(
            ImapClient client,
            ImapServerConfig config,
            string folderName,
            FolderAccess access,
            string clientName,
            CancellationToken cancellationToken)
        {
            if (!client.IsConnected)
            {
                EmitLog($"[{clientName}] IMAP Bağlantısı kopmuş, yeniden bağlanılıyor ({config.Host})...", LogLevel.Warning);
                SecureSocketOptions sslOptions = config.UseSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.Auto;
                await client.ConnectAsync(config.Host, config.Port, sslOptions, cancellationToken).ConfigureAwait(false);
            }

            if (!client.IsAuthenticated)
            {
                await client.AuthenticateAsync(config.Email, config.Password, cancellationToken).ConfigureAwait(false);
            }

            IMailFolder folder;
            try
            {
                folder = await client.GetFolderAsync(folderName, cancellationToken).ConfigureAwait(false);
            }
            catch
            {
                if (access == FolderAccess.ReadWrite)
                {
                    var topFolder = client.GetFolder(client.PersonalNamespaces[0]);
                    EmitLog($"Hedef sunucuda [{folderName}] klasörü oluşturuluyor...", LogLevel.Info);
                    folder = await topFolder.CreateAsync(folderName, true, cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    throw;
                }
            }

            if (!folder.IsOpen || folder.Access != access)
            {
                await folder.OpenAsync(access, cancellationToken).ConfigureAwait(false);
            }

            return folder;
        }

        public async Task StartMigrationAsync(
            ImapServerConfig sourceConfig,
            ImapServerConfig targetConfig,
            List<FolderMappingInfo> selectedFolders,
            MigrationOptions options,
            IProgress<MigrationProgressReport> progress,
            PauseToken pauseToken,
            CancellationToken cancellationToken)
        {
            int totalSelectedMessages = selectedFolders.Where(f => f.Selected).Sum(f => f.TotalMessages);
            int overallProcessed = 0;
            int totalMigrated = 0;
            int totalSkipped = 0;
            int totalFailed = 0;

            var stopwatch = Stopwatch.StartNew();

            EmitLog($"=== TAŞIMA İŞLEMİ BAŞLATILIYOR ===", LogLevel.Info);
            EmitLog($"Kaynak: {sourceConfig.Email} ({sourceConfig.Host})", LogLevel.Info);
            EmitLog($"Hedef : {targetConfig.Email} ({targetConfig.Host})", LogLevel.Info);
            EmitLog($"Toplam Taşınacak Klasör Sayısı: {selectedFolders.Count(f => f.Selected)}, Tahmini Mail: {totalSelectedMessages}", LogLevel.Info);

            using var sourceClient = new ImapClient();
            using var targetClient = new ImapClient();

            try
            {
                foreach (var folderMapping in selectedFolders.Where(f => f.Selected))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await pauseToken.WaitWhilePausedAsync(cancellationToken).ConfigureAwait(false);

                    EmitLog($"--> Klasör İşleniyor: [{folderMapping.SourceFolderName}] -> [{folderMapping.TargetFolderName}]", LogLevel.Info);

                    IMailFolder sourceFolder = await EnsureFolderOpenAsync(sourceClient, sourceConfig, folderMapping.SourceFolderName, FolderAccess.ReadOnly, "Kaynak Yandex", cancellationToken).ConfigureAwait(false);
                    int folderTotalCount = sourceFolder.Count;

                    if (folderTotalCount == 0)
                    {
                        EmitLog($"Klasör boş, atlanıyor: [{folderMapping.SourceFolderName}]", LogLevel.Info);
                        continue;
                    }

                    // Fetch summaries in chunks of 500 to avoid large payload timeout disconnects
                    int chunkSize = 500;
                    var summaries = new List<IMessageSummary>();

                    for (int i = 0; i < folderTotalCount; i += chunkSize)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        await pauseToken.WaitWhilePausedAsync(cancellationToken).ConfigureAwait(false);

                        int startIndex = i;
                        int endIndex = Math.Min(i + chunkSize - 1, folderTotalCount - 1);

                        bool fetchOk = false;
                        int fetchAttempts = 0;

                        while (!fetchOk && fetchAttempts < 5)
                        {
                            fetchAttempts++;
                            try
                            {
                                sourceFolder = await EnsureFolderOpenAsync(sourceClient, sourceConfig, folderMapping.SourceFolderName, FolderAccess.ReadOnly, "Kaynak Yandex", cancellationToken).ConfigureAwait(false);
                                var chunk = await sourceFolder.FetchAsync(
                                    startIndex, endIndex,
                                    MessageSummaryItems.UniqueId | MessageSummaryItems.Flags | MessageSummaryItems.Envelope | MessageSummaryItems.InternalDate,
                                    cancellationToken).ConfigureAwait(false);

                                summaries.AddRange(chunk);
                                fetchOk = true;
                            }
                            catch (Exception ex) when (ex is ImapProtocolException || ex is IOException || ex is SocketException || ex is ServiceNotConnectedException)
                            {
                                EmitLog($"Klasör özetleri çekilirken bağlantı koptu ({fetchAttempts}/5). Yeniden deneniyor... Hata: {ex.Message}", LogLevel.Warning);
                                try { if (sourceClient.IsConnected) await sourceClient.DisconnectAsync(false).ConfigureAwait(false); } catch { }
                                await Task.Delay(2000 * fetchAttempts, cancellationToken).ConfigureAwait(false);
                            }
                        }
                    }

                    int folderCurrentIndex = 0;

                    foreach (var summary in summaries)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        await pauseToken.WaitWhilePausedAsync(cancellationToken).ConfigureAwait(false);

                        folderCurrentIndex++;
                        overallProcessed++;

                        string msgId = summary.Envelope?.MessageId ?? string.Empty;
                        string subject = summary.Envelope?.Subject ?? "(Konu Yok)";
                        uint uid = summary.UniqueId.Id;

                        bool alreadyMigrated = false;
                        if (options.SkipExisting)
                        {
                            alreadyMigrated = await _repository.IsAlreadyMigratedAsync(
                                sourceConfig.Email,
                                folderMapping.SourceFolderName,
                                msgId,
                                uid).ConfigureAwait(false);
                        }

                        if (alreadyMigrated)
                        {
                            totalSkipped++;
                            ReportProgress(progress, folderMapping.SourceFolderName, folderCurrentIndex, folderTotalCount,
                                overallProcessed, totalSelectedMessages, totalMigrated, totalSkipped, totalFailed,
                                subject, stopwatch.Elapsed.TotalSeconds);
                            continue;
                        }

                        // Resilient Mail Transfer Loop
                        bool success = false;
                        int attempt = 0;
                        int maxAttempts = 5;

                        while (!success && attempt < maxAttempts)
                        {
                            attempt++;
                            try
                            {
                                sourceFolder = await EnsureFolderOpenAsync(sourceClient, sourceConfig, folderMapping.SourceFolderName, FolderAccess.ReadOnly, "Kaynak Yandex", cancellationToken).ConfigureAwait(false);
                                IMailFolder targetFolder = await EnsureFolderOpenAsync(targetClient, targetConfig, folderMapping.TargetFolderName, FolderAccess.ReadWrite, "Hedef Sunucu", cancellationToken).ConfigureAwait(false);

                                MimeMessage message = await sourceFolder.GetMessageAsync(summary.UniqueId, cancellationToken).ConfigureAwait(false);

                                MessageFlags flags = options.PreserveFlags && summary.Flags.HasValue ? summary.Flags.Value : MessageFlags.None;
                                DateTimeOffset internalDate = options.PreserveInternalDate && summary.InternalDate.HasValue
                                    ? summary.InternalDate.Value
                                    : DateTimeOffset.UtcNow;

                                await targetFolder.AppendAsync(message, flags, internalDate, cancellationToken).ConfigureAwait(false);

                                await _repository.RecordMigrationAsync(
                                    sourceConfig.Email,
                                    targetConfig.Email,
                                    folderMapping.SourceFolderName,
                                    msgId,
                                    uid,
                                    "COMPLETED").ConfigureAwait(false);

                                totalMigrated++;
                                success = true;
                            }
                            catch (OperationCanceledException)
                            {
                                throw;
                            }
                            catch (Exception ex)
                            {
                                EmitLog($"Hata (Deneme {attempt}/{maxAttempts}) Mail UID {uid} [{subject}]: {ex.Message}", LogLevel.Warning);
                                
                                // Force connection reset on connection/session drops so next attempt gets fresh open folders
                                try { if (sourceClient.IsConnected) await sourceClient.DisconnectAsync(false).ConfigureAwait(false); } catch { }
                                try { if (targetClient.IsConnected) await targetClient.DisconnectAsync(false).ConfigureAwait(false); } catch { }

                                if (attempt >= maxAttempts)
                                {
                                    totalFailed++;
                                    await _repository.RecordMigrationAsync(
                                        sourceConfig.Email,
                                        targetConfig.Email,
                                        folderMapping.SourceFolderName,
                                        msgId,
                                        uid,
                                        "FAILED",
                                        ex.Message).ConfigureAwait(false);
                                }
                                else
                                {
                                    await Task.Delay(2000 * attempt, cancellationToken).ConfigureAwait(false);
                                }
                            }
                        }

                        ReportProgress(progress, folderMapping.SourceFolderName, folderCurrentIndex, folderTotalCount,
                            overallProcessed, totalSelectedMessages, totalMigrated, totalSkipped, totalFailed,
                            subject, stopwatch.Elapsed.TotalSeconds);

                        if (options.DelayBetweenMailsMs > 0)
                        {
                            await Task.Delay(options.DelayBetweenMailsMs, cancellationToken).ConfigureAwait(false);
                        }
                    }
                }

                EmitLog($"=== TAŞIMA İŞLEMİ TAMAMLANDI ===", LogLevel.Success);
                EmitLog($"Toplam Süre: {stopwatch.Elapsed:hh\\:mm\\:ss}", LogLevel.Success);
                EmitLog($"Başarılı: {totalMigrated} | Atlanan (Zaten Var): {totalSkipped} | Hatalı: {totalFailed}", LogLevel.Success);
            }
            catch (OperationCanceledException)
            {
                EmitLog("İşlem kullanıcı tarafından İPTAL edildi.", LogLevel.Warning);
            }
            catch (Exception ex)
            {
                EmitLog($"Kritik Transfer Hatası: {ex.Message}", LogLevel.Error);
                throw;
            }
            finally
            {
                try { if (sourceClient.IsConnected) await sourceClient.DisconnectAsync(true).ConfigureAwait(false); } catch { }
                try { if (targetClient.IsConnected) await targetClient.DisconnectAsync(true).ConfigureAwait(false); } catch { }
            }
        }

        private void ReportProgress(
            IProgress<MigrationProgressReport> progress,
            string currentFolder,
            int folderCurrentIndex,
            int folderTotalCount,
            int overallProcessed,
            int overallTotal,
            int migratedCount,
            int skippedCount,
            int failedCount,
            string currentSubject,
            double elapsedSeconds)
        {
            if (progress == null) return;

            double speed = elapsedSeconds > 0 ? (migratedCount + skippedCount) / elapsedSeconds : 0;

            progress.Report(new MigrationProgressReport
            {
                CurrentFolder = currentFolder,
                FolderCurrentIndex = folderCurrentIndex,
                FolderTotalCount = folderTotalCount,
                OverallProcessed = overallProcessed,
                OverallTotal = overallTotal,
                MigratedCount = migratedCount,
                SkippedCount = skippedCount,
                FailedCount = failedCount,
                CurrentSubject = currentSubject,
                SpeedMailsPerSec = Math.Round(speed, 1)
            });
        }
    }
}
