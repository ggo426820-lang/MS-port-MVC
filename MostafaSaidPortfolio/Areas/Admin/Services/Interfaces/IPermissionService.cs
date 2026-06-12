using MostafaSaidPortfolio.Areas.Admin.Models;

namespace MostafaSaidPortfolio.Areas.Admin.Services.Interfaces;

public interface IPermissionService
{
    Task<IEnumerable<Permission>> GetAllAsync();
    Task<IEnumerable<Permission>> GetForRoleAsync(string roleName);
    Task<bool> HasPermissionAsync(string userId, string permissionName);
    Task GrantToRoleAsync(string roleName, string permissionName);
    Task RevokeFromRoleAsync(string roleName, string permissionName);
}
