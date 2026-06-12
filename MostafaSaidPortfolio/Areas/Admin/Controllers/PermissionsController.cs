using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PermissionsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole>    _roleManager;

        public PermissionsController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            ViewData["Title"]      = "Permissions";
            ViewData["Breadcrumb"] = "Admin / Permissions";
            var users = _userManager.Users.OrderBy(u => u.UserName).ToList();
            var roles = _roleManager.Roles.OrderBy(r => r.Name).ToList();
            ViewData["Users"] = users;
            ViewData["Roles"] = roles;
            return View("~/Areas/Admin/Views/Permissions/Index.cshtml");
        }

        public async Task<IActionResult> Assign(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            ViewData["Title"]    = $"Assign Roles — {user.UserName}";
            ViewData["User"]     = user;
            ViewData["Roles"]    = _roleManager.Roles.ToList();
            ViewData["UserRoles"]= await _userManager.GetRolesAsync(user);
            return View("~/Areas/Admin/Views/Permissions/Assign.cshtml");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            var current = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, current);
            if (roles?.Any() == true)
                await _userManager.AddToRolesAsync(user, roles);
            TempData["Success"] = $"Roles updated for {user.UserName}.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(string roleId)
        {
            ViewData["Title"]  = "Edit Permission";
            ViewData["RoleId"] = roleId;
            return View("~/Areas/Admin/Views/Permissions/Edit.cshtml");
        }
    }
}
