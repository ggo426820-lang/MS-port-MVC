using MostafaSaidPortfolio.Areas.Admin.Models;

namespace MostafaSaidPortfolio.Areas.Admin.Services.Interfaces;

public interface IBackupService
{
    Task<BackupJob> CreateBackupAsync(string createdBy);
    Task<IEnumerable<BackupJob>> GetAllAsync();
    Task<BackupJob?> GetByIdAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
    Task<Stream?> DownloadAsync(Guid id);
    BackupJob? GetLastCompleted();
}
