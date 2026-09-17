using System;

namespace ImapToImap_Transfer.Models
{
    public class ImapServerConfig
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 993;
        public bool UseSsl { get; set; } = true;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class FolderMappingInfo
    {
        public string SourceFolderName { get; set; } = string.Empty;
        public string TargetFolderName { get; set; } = string.Empty;
        public bool Selected { get; set; } = true;
        public int TotalMessages { get; set; } = 0;

        public override string ToString()
        {
            return $"{SourceFolderName} -> {TargetFolderName} ({TotalMessages} Mailler)";
        }
    }

    public enum LogLevel
    {
        Info,
        Success,
        Warning,
        Error
    }

    public class LogEventArgs : EventArgs
    {
        public DateTime Timestamp { get; } = DateTime.Now;
        public string Message { get; }
        public LogLevel Level { get; }

        public LogEventArgs(string message, LogLevel level = LogLevel.Info)
        {
            Message = message;
            Level = level;
        }
    }

    public class MigrationProgressReport
    {
        public string CurrentFolder { get; set; } = string.Empty;
        public int FolderCurrentIndex { get; set; }
        public int FolderTotalCount { get; set; }

        public int OverallProcessed { get; set; }
        public int OverallTotal { get; set; }

        public int MigratedCount { get; set; }
        public int SkippedCount { get; set; }
        public int FailedCount { get; set; }

        public string CurrentSubject { get; set; } = string.Empty;
        public double SpeedMailsPerSec { get; set; }
    }

    public class MigrationOptions
    {
        public int BatchSize { get; set; } = 50;
        public int DelayBetweenMailsMs { get; set; } = 50;
        public bool SkipExisting { get; set; } = true;
        public bool PreserveFlags { get; set; } = true;
        public bool PreserveInternalDate { get; set; } = true;
    }
}
