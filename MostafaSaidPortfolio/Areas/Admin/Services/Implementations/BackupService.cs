using MostafaSaidPortfolio.Areas.Admin.Models;
using MostafaSaidPortfolio.Areas.Admin.Services.Interfaces;

namespace MostafaSaidPortfolio.Areas.Admin.Services.Implementations;

/// <summary>
/// Placeholder backup service — no real DB dump is performed until infrastructure is configured.
/// </summary>
public class BackupService : IBackupService
{
    private static readonly List<BackupJob> _jobs = new();
    private static readonly object _lock = new();

    public async Task<BackupJob> CreateBackupAsync(string createdBy)
    {
        var job = new BackupJob
        {
            Name      = $"backup_{DateTime.UtcNow:yyyyMMdd_HHmmss}.sql",
            Status    = BackupStatus.Running,
            StartedAt = DateTime.UtcNow,
            CreatedBy = createdBy
        };
        lock (_lock) { _jobs.Insert(0, job); }

        // Simulate brief work
        await Task.Delay(200);
        job.Status      = BackupStatus.Completed;
        job.CompletedAt = DateTime.UtcNow;
        job.FileSizeBytes = 0; // would be actual file size
        return job;
    }

    public Task<IEnumerable<BackupJob>> GetAllAsync()
    {
        lock (_lock) { return Task.FromResult<IEnumerable<BackupJob>>(_jobs.ToList()); }
    }

    public Task<BackupJob?> GetByIdAsync(Guid id)
    {
        lock (_lock) { return Task.FromResult(_jobs.FirstOrDefault(j => j.Id == id)); }
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        lock (_lock)
        {
            var job = _jobs.FirstOrDefault(j => j.Id == id);
            if (job == null) return Task.FromResult(false);
            _jobs.Remove(job);
            return Task.FromResult(true);
        }
    }

    public Task<Stream?> DownloadAsync(Guid id)
    {
        // Would return a FileStream for the actual backup file
        return Task.FromResult<Stream?>(null);
    }

    public BackupJob? GetLastCompleted()
    {
        lock (_lock) { return _jobs.FirstOrDefault(j => j.Status == BackupStatus.Completed); }
    }
}
