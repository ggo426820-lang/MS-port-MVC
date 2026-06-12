using MostafaSaidPortfolio.Areas.Admin.Models;
using MostafaSaidPortfolio.Areas.Admin.Services.Interfaces;

namespace MostafaSaidPortfolio.Areas.Admin.Services.Implementations;

/// <summary>
/// In-memory audit service — swap for a DB-backed implementation when an AuditLogs table is added.
/// </summary>
public class AuditService : IAuditService
{
    private static readonly List<AuditLog> _logs = new();
    private static readonly object _lock = new();

    public Task LogAsync(string action, string entityType, string? entityId = null,
        string? oldValues = null, string? newValues = null,
        string? userId = null, string? userName = null, string? ipAddress = null,
        bool success = true, string? errorMessage = null)
    {
        var entry = new AuditLog
        {
            Action       = action,
            EntityType   = entityType,
            EntityId     = entityId,
            OldValues    = oldValues,
            NewValues    = newValues,
            UserId       = userId,
            UserName     = userName,
            IpAddress    = ipAddress,
            Success      = success,
            ErrorMessage = errorMessage,
            Timestamp    = DateTime.UtcNow
        };
        lock (_lock) { _logs.Insert(0, entry); if (_logs.Count > 5000) _logs.RemoveAt(_logs.Count - 1); }
        return Task.CompletedTask;
    }

    public Task<IEnumerable<AuditLog>> GetRecentAsync(int count = 50)
    {
        lock (_lock) { return Task.FromResult<IEnumerable<AuditLog>>(_logs.Take(count).ToList()); }
    }

    public Task<IEnumerable<AuditLog>> SearchAsync(string? action, string? entityType,
        string? userId, DateTime? from, DateTime? to, int page = 1, int pageSize = 50)
    {
        lock (_lock)
        {
            var q = _logs.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(action))     q = q.Where(l => l.Action.Contains(action, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(entityType)) q = q.Where(l => l.EntityType.Contains(entityType, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(userId))     q = q.Where(l => l.UserId == userId);
            if (from.HasValue) q = q.Where(l => l.Timestamp >= from.Value);
            if (to.HasValue)   q = q.Where(l => l.Timestamp <= to.Value);
            return Task.FromResult<IEnumerable<AuditLog>>(q.Skip((page - 1) * pageSize).Take(pageSize).ToList());
        }
    }

    public Task<AuditLog?> GetByIdAsync(Guid id)
    {
        lock (_lock) { return Task.FromResult(_logs.FirstOrDefault(l => l.Id == id)); }
    }

    public Task PurgeOlderThanAsync(DateTime cutoff)
    {
        lock (_lock) { _logs.RemoveAll(l => l.Timestamp < cutoff); }
        return Task.CompletedTask;
    }
}
