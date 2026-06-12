using MostafaSaidPortfolio.Areas.Admin.Models;

namespace MostafaSaidPortfolio.Areas.Admin.Services.Interfaces;

public interface ISystemSettingsService
{
    Task<IEnumerable<SystemSetting>> GetAllAsync();
    Task<IEnumerable<SystemSetting>> GetByGroupAsync(string group);
    Task<string?> GetAsync(string key);
    Task<T?> GetAsync<T>(string key);
    Task UpsertAsync(string key, string value, string? displayName = null, string group = "General");
    Task<bool> DeleteAsync(string key);
}
