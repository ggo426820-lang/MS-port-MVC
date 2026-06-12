namespace MostafaSaidPortfolio.Areas.Admin.Models;

public enum BackupStatus { Pending, Running, Completed, Failed }

public class BackupJob
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public BackupStatus Status { get; set; } = BackupStatus.Pending;
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public long? FileSizeBytes { get; set; }
    public string? FilePath { get; set; }
    public string? ErrorMessage { get; set; }
    public string CreatedBy { get; set; } = string.Empty;

    public string FileSizeFormatted => FileSizeBytes.HasValue
        ? FileSizeBytes.Value switch
        {
            < 1024 => $"{FileSizeBytes.Value} B",
            < 1048576 => $"{FileSizeBytes.Value / 1024.0:F1} KB",
            _ => $"{FileSizeBytes.Value / 1048576.0:F1} MB"
        }
        : "—";
}
