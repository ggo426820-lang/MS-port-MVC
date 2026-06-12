using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BackupController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"]      = "Backup & Restore";
            ViewData["Breadcrumb"] = "Admin / Backup";
            ViewData["LastBackup"] = "Not configured";
            return View("~/Areas/Admin/Views/AuditMediaLibrary/Index.cshtml");
        }
    }
}
