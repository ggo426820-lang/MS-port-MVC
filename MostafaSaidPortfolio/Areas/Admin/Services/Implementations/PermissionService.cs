using Microsoft.AspNetCore.Identity;
using MostafaSaidPortfolio.Areas.Admin.Models;
using MostafaSaidPortfolio.Areas.Admin.Services.Interfaces;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Areas.Admin.Services.Implementations;

public class PermissionService : IPermissionService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    // All known permissions in the system
    private static readonly List<Permission> _allPermissions = new()
    {
        new() { Name = Permission.Names.BlogRead,    DisplayName = "Read Blog Posts",    Group = "Blog",     IsSystemPermission = true },
        new() { Name = Permission.Names.BlogWrite,   DisplayName = "Write Blog Posts",   Group = "Blog",     IsSystemPermission = true },
        new() { Name = Permission.Names.BlogDelete,  DisplayName = "Delete Blog Posts",  Group = "Blog",     IsSystemPermission = true },
        new() { Name = Permission.Names.ProjectRead, DisplayName = "Read Projects",      Group = "Projects", IsSystemPermission = true },
        new() { Name = Permission.Names.ProjectWrite,DisplayName = "Write Projects",     Group = "Projects", IsSystemPermission = true },
        new() { Name = Permission.Names.UsersManage, DisplayName = "Manage Users",       Group = "Users",    IsSystemPermission = true },
        new() { Name = Permission.Names.SettingsEdit,DisplayName = "Edit Site Settings", Group = "Settings", IsSystemPermission = true },
    };

    public PermissionService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public Task<IEnumerable<Permission>> GetAllAsync() =>
        Task.FromResult<IEnumerable<Permission>>(_allPermissions);

    public async Task<IEnumerable<Permission>> GetForRoleAsync(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null) return Enumerable.Empty<Permission>();
        var claims = await _roleManager.GetClaimsAsync(role);
        var permissionNames = claims.Where(c => c.Type == "permission").Select(c => c.Value).ToHashSet();
        return _allPermissions.Where(p => permissionNames.Contains(p.Name));
    }

    public async Task<bool> HasPermissionAsync(string userId, string permissionName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;
        var roles = await _userManager.GetRolesAsync(user);
        if (roles.Contains("Admin")) return true;
        var claims = await _userManager.GetClaimsAsync(user);
        return claims.Any(c => c.Type == "permission" && c.Value == permissionName);
    }

    public async Task GrantToRoleAsync(string roleName, string permissionName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null) return;
        var existing = await _roleManager.GetClaimsAsync(role);
        if (!existing.Any(c => c.Type == "permission" && c.Value == permissionName))
            await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim("permission", permissionName));
    }

    public async Task RevokeFromRoleAsync(string roleName, string permissionName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null) return;
        var existing = await _roleManager.GetClaimsAsync(role);
        var claim = existing.FirstOrDefault(c => c.Type == "permission" && c.Value == permissionName);
        if (claim != null)
            await _roleManager.RemoveClaimAsync(role, claim);
    }
}
