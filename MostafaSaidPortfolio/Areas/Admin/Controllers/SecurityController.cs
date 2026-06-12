using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SecurityController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public SecurityController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewData["Title"]      = "Security";
            ViewData["Breadcrumb"] = "Admin / Security";
            return View("~/Areas/Admin/Views/Security/Index.cshtml");
        }

        public async Task<IActionResult> LoginAttempts()
        {
            ViewData["Title"]      = "Login Attempts";
            ViewData["Breadcrumb"] = "Admin / Security / Login Attempts";
            var lockedUsers = _userManager.Users
                .Where(u => u.LockoutEnd != null && u.LockoutEnd > DateTimeOffset.UtcNow)
                .OrderByDescending(u => u.LockoutEnd)
                .ToList();
            var allUsers = _userManager.Users
                .OrderByDescending(u => u.AccessFailedCount)
                .Take(20)
                .ToList();
            ViewData["LockedUsers"] = lockedUsers;
            ViewData["AllUsers"]    = allUsers;
            return View("~/Areas/Admin/Views/Security/LoginAttempts.cshtml");
        }

        public IActionResult IpWhitelist()
        {
            ViewData["Title"]      = "IP Whitelist";
            ViewData["Breadcrumb"] = "Admin / Security / IP Whitelist";
            return View("~/Areas/Admin/Views/Security/IpWhitelist.cshtml");
        }

        public IActionResult TwoFactor()
        {
            ViewData["Title"]      = "Two-Factor Authentication";
            ViewData["Breadcrumb"] = "Admin / Security / 2FA";
            return View("~/Areas/Admin/Views/Security/TwoFactor.cshtml");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UnlockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            await _userManager.SetLockoutEndDateAsync(user, null);
            await _userManager.ResetAccessFailedCountAsync(user);
            TempData["Success"] = $"User '{user.UserName}' unlocked.";
            return RedirectToAction(nameof(LoginAttempts));
        }
    }
}
