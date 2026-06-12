using MostafaSaidPortfolio.Areas.Admin.Models;

namespace MostafaSaidPortfolio.Areas.Admin.Services.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<Role>> GetAllAsync();
    Task<Role?> GetByNameAsync(string name);
    Task<(bool Success, IEnumerable<string> Errors)> CreateAsync(string name, string? description = null);
    Task<(bool Success, IEnumerable<string> Errors)> UpdateAsync(string id, string newName, string? description = null);
    Task<(bool Success, string Error)> DeleteAsync(string id);
    Task<IEnumerable<string>> GetUsersInRoleAsync(string roleName);
    Task<(bool Success, string Error)> AssignUserAsync(string userId, string roleName);
    Task<(bool Success, string Error)> RemoveUserAsync(string userId, string roleName);
}
