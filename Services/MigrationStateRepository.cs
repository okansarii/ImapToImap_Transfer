using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace ImapToImap_Transfer.Services
{
    public class MigrationStateRepository
    {
        private readonly string _connectionString;

        public MigrationStateRepository(string dbFileName = "migration_state.db")
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbFileName);
            _connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = dbPath,
                Mode = SqliteOpenMode.ReadWriteCreate
            }.ConnectionString;

            InitDatabase();
        }

        private void InitDatabase()
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            string createTableCmd = @"
                CREATE TABLE IF NOT EXISTS MigratedEmails (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    SourceEmail TEXT NOT NULL,
                    TargetEmail TEXT NOT NULL,
                    FolderName TEXT NOT NULL,
                    MessageId TEXT,
                    SourceUid INTEGER NOT NULL,
                    Status TEXT NOT NULL,
                    MigratedAt TEXT NOT NULL,
                    ErrorMessage TEXT
                );
                CREATE INDEX IF NOT EXISTS IX_MigratedEmails_Lookup 
                ON MigratedEmails (SourceEmail, FolderName, SourceUid);
                
                CREATE INDEX IF NOT EXISTS IX_MigratedEmails_MsgId 
                ON MigratedEmails (SourceEmail, FolderName, MessageId);
            ";

            using var cmd = new SqliteCommand(createTableCmd, conn);
            cmd.ExecuteNonQuery();
        }

        public async Task<bool> IsAlreadyMigratedAsync(string sourceEmail, string folderName, string messageId, uint sourceUid)
        {
            using var conn = new SqliteConnection(_connectionString);
            await conn.OpenAsync().ConfigureAwait(false);

            string query = @"
                SELECT COUNT(1) FROM MigratedEmails 
                WHERE SourceEmail = @SourceEmail 
                  AND FolderName = @FolderName 
                  AND Status = 'COMPLETED'
                  AND (SourceUid = @SourceUid OR (MessageId IS NOT NULL AND MessageId != '' AND MessageId = @MessageId));
            ";

            using var cmd = new SqliteCommand(query, conn);
            cmd.Parameters.AddWithValue("@SourceEmail", sourceEmail ?? string.Empty);
            cmd.Parameters.AddWithValue("@FolderName", folderName ?? string.Empty);
            cmd.Parameters.AddWithValue("@SourceUid", Convert.ToInt64(sourceUid));
            cmd.Parameters.AddWithValue("@MessageId", messageId ?? (object)DBNull.Value);

            object? result = await cmd.ExecuteScalarAsync().ConfigureAwait(false);
            return result != null && result != DBNull.Value && Convert.ToInt64(result) > 0;
        }

        public async Task RecordMigrationAsync(
            string sourceEmail, 
            string targetEmail, 
            string folderName, 
            string? messageId, 
            uint sourceUid, 
            string status, 
            string? errorMessage = null)
        {
            using var conn = new SqliteConnection(_connectionString);
            await conn.OpenAsync().ConfigureAwait(false);

            string insertCmd = @"
                INSERT INTO MigratedEmails (SourceEmail, TargetEmail, FolderName, MessageId, SourceUid, Status, MigratedAt, ErrorMessage)
                VALUES (@SourceEmail, @TargetEmail, @FolderName, @MessageId, @SourceUid, @Status, @MigratedAt, @ErrorMessage);
            ";

            using var cmd = new SqliteCommand(insertCmd, conn);
            cmd.Parameters.AddWithValue("@SourceEmail", sourceEmail ?? string.Empty);
            cmd.Parameters.AddWithValue("@TargetEmail", targetEmail ?? string.Empty);
            cmd.Parameters.AddWithValue("@FolderName", folderName ?? string.Empty);
            cmd.Parameters.AddWithValue("@MessageId", messageId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@SourceUid", Convert.ToInt64(sourceUid));
            cmd.Parameters.AddWithValue("@Status", status ?? "COMPLETED");
            cmd.Parameters.AddWithValue("@MigratedAt", DateTime.UtcNow.ToString("o"));
            cmd.Parameters.AddWithValue("@ErrorMessage", (object?)errorMessage ?? DBNull.Value);

            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task ClearStateAsync()
        {
            using var conn = new SqliteConnection(_connectionString);
            await conn.OpenAsync().ConfigureAwait(false);

            string clearCmd = "DELETE FROM MigratedEmails;";
            using var cmd = new SqliteCommand(clearCmd, conn);
            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
    }
}
