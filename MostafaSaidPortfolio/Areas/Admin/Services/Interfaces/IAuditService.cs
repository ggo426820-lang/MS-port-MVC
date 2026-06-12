using MostafaSaidPortfolio.Areas.Admin.Models;

namespace MostafaSaidPortfolio.Areas.Admin.Services.Interfaces;

public interface IAuditService
{
    Task LogAsync(string action, string entityType, string? entityId = null,
        string? oldValues = null, string? newValues = null,
        string? userId = null, string? userName = null, string? ipAddress = null,
        bool success = true, string? errorMessage = null);

    Task<IEnumerable<AuditLog>> GetRecentAsync(int count = 50);
    Task<IEnumerable<AuditLog>> SearchAsync(string? action, string? entityType,
        string? userId, DateTime? from, DateTime? to, int page = 1, int pageSize = 50);
    Task<AuditLog?> GetByIdAsync(Guid id);
    Task PurgeOlderThanAsync(DateTime cutoff);
}
