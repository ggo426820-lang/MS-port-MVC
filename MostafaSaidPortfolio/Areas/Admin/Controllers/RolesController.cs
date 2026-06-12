using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<MostafaSaidPortfolio.Domain.Entities.ApplicationUser> _userManager;

        public RolesController(
            RoleManager<IdentityRole> roleManager,
            UserManager<MostafaSaidPortfolio.Domain.Entities.ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewData["Title"]      = "Roles";
            ViewData["Breadcrumb"] = "Admin / Roles";
            var roles = _roleManager.Roles.OrderBy(r => r.Name).ToList();
            return View("~/Areas/Admin/Views/Roles/Index.cshtml", roles);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Create Role";
            return View("~/Areas/Admin/Views/Roles/Create.cshtml");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                ModelState.AddModelError("", "Role name is required.");
                return View("~/Areas/Admin/Views/Roles/Create.cshtml");
            }
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                TempData["Error"] = $"Role '{roleName}' already exists.";
                return RedirectToAction(nameof(Index));
            }
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            TempData[result.Succeeded ? "Success" : "Error"] = result.Succeeded
                ? $"Role '{roleName}' created."
                : string.Join("; ", result.Errors.Select(e => e.Description));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            ViewData["Title"] = "Edit Role";
            return View("~/Areas/Admin/Views/Roles/Edit.cshtml", role);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string newName)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            role.Name = newName;
            var result = await _roleManager.UpdateAsync(role);
            TempData[result.Succeeded ? "Success" : "Error"] = result.Succeeded
                ? $"Role renamed to '{newName}'."
                : string.Join("; ", result.Errors.Select(e => e.Description));
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            if (role.Name == "Admin")
            {
                TempData["Error"] = "Cannot delete the built-in Admin role.";
                return RedirectToAction(nameof(Index));
            }
            var result = await _roleManager.DeleteAsync(role);
            TempData[result.Succeeded ? "Success" : "Error"] = result.Succeeded
                ? "Role deleted."
                : string.Join("; ", result.Errors.Select(e => e.Description));
            return RedirectToAction(nameof(Index));
        }
    }
}
