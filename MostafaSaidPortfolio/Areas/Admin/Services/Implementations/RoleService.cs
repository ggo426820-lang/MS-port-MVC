using Microsoft.AspNetCore.Identity;
using MostafaSaidPortfolio.Areas.Admin.Models;
using MostafaSaidPortfolio.Areas.Admin.Services.Interfaces;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Areas.Admin.Services.Implementations;

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private static readonly HashSet<string> _systemRoles = new(StringComparer.OrdinalIgnoreCase) { "Admin", "Editor", "Viewer" };

    public RoleService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        var roles = _roleManager.Roles.OrderBy(r => r.Name).ToList();
        var result = new List<Role>();
        foreach (var r in roles)
        {
            var users = await _userManager.GetUsersInRoleAsync(r.Name!);
            result.Add(new Role
            {
                Id           = r.Id,
                Name         = r.Name!,
                IsSystemRole = _systemRoles.Contains(r.Name!),
                UserCount    = users.Count
            });
        }
        return result;
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        var r = await _roleManager.FindByNameAsync(name);
        if (r == null) return null;
        var users = await _userManager.GetUsersInRoleAsync(r.Name!);
        return new Role { Id = r.Id, Name = r.Name!, IsSystemRole = _systemRoles.Contains(r.Name!), UserCount = users.Count };
    }

    public async Task<(bool Success, IEnumerable<string> Errors)> CreateAsync(string name, string? description = null)
    {
        var result = await _roleManager.CreateAsync(new IdentityRole(name));
        return (result.Succeeded, result.Errors.Select(e => e.Description));
    }

    public async Task<(bool Success, IEnumerable<string> Errors)> UpdateAsync(string id, string newName, string? description = null)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null) return (false, new[] { "Role not found." });
        role.Name = newName;
        var result = await _roleManager.UpdateAsync(role);
        return (result.Succeeded, result.Errors.Select(e => e.Description));
    }

    public async Task<(bool Success, string Error)> DeleteAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null) return (false, "Role not found.");
        if (_systemRoles.Contains(role.Name!)) return (false, "System roles cannot be deleted.");
        var result = await _roleManager.DeleteAsync(role);
        return (result.Succeeded, result.Errors.FirstOrDefault()?.Description ?? string.Empty);
    }

    public async Task<IEnumerable<string>> GetUsersInRoleAsync(string roleName)
    {
        var users = await _userManager.GetUsersInRoleAsync(roleName);
        return users.Select(u => u.UserName ?? u.Email ?? u.Id);
    }

    public async Task<(bool Success, string Error)> AssignUserAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return (false, "User not found.");
        var result = await _userManager.AddToRoleAsync(user, roleName);
        return (result.Succeeded, result.Errors.FirstOrDefault()?.Description ?? string.Empty);
    }

    public async Task<(bool Success, string Error)> RemoveUserAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return (false, "User not found.");
        var result = await _userManager.RemoveFromRoleAsync(user, roleName);
        return (result.Succeeded, result.Errors.FirstOrDefault()?.Description ?? string.Empty);
    }
}
