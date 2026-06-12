using MostafaSaidPortfolio.Areas.Admin.Models;
using MostafaSaidPortfolio.Areas.Admin.Services.Interfaces;

namespace MostafaSaidPortfolio.Areas.Admin.Services.Implementations;

/// <summary>
/// In-memory settings service — persists for the lifetime of the process.
/// Swap for a DB-backed implementation to survive restarts.
/// </summary>
public class SystemSettingsService : ISystemSettingsService
{
    private static readonly Dictionary<string, SystemSetting> _settings = new();
    private static readonly object _lock = new();

    public Task<IEnumerable<SystemSetting>> GetAllAsync()
    {
        lock (_lock) { return Task.FromResult<IEnumerable<SystemSetting>>(_settings.Values.ToList()); }
    }

    public Task<IEnumerable<SystemSetting>> GetByGroupAsync(string group)
    {
        lock (_lock)
        {
            return Task.FromResult<IEnumerable<SystemSetting>>(
                _settings.Values.Where(s => s.Group == group).ToList());
        }
    }

    public Task<string?> GetAsync(string key)
    {
        lock (_lock) { return Task.FromResult(_settings.TryGetValue(key, out var s) ? s.Value : null); }
    }

    public Task<T?> GetAsync<T>(string key)
    {
        lock (_lock)
        {
            if (!_settings.TryGetValue(key, out var s)) return Task.FromResult<T?>(default);
            try
            {
                return Task.FromResult<T?>((T)Convert.ChangeType(s.Value, typeof(T)));
            }
            catch
            {
                return Task.FromResult<T?>(default);
            }
        }
    }

    public Task UpsertAsync(string key, string value, string? displayName = null, string group = "General")
    {
        lock (_lock)
        {
            if (_settings.TryGetValue(key, out var existing))
            {
                existing.Value = value;
                if (displayName != null) existing.DisplayName = displayName;
            }
            else
            {
                _settings[key] = new SystemSetting { Key = key, Value = value, DisplayName = displayName ?? key, Group = group };
            }
        }
        return Task.CompletedTask;
    }

    public Task<bool> DeleteAsync(string key)
    {
        lock (_lock) { return Task.FromResult(_settings.Remove(key)); }
    }
}
