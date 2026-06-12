using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SystemSettingsController : Controller
    {
        private readonly IConfiguration _config;

        public SystemSettingsController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            ViewData["Title"]      = "System Settings";
            ViewData["Breadcrumb"] = "Admin / System Settings";
            ViewData["SmtpServer"] = _config["EmailSettings:SmtpServer"] ?? "—";
            ViewData["SmtpPort"]   = _config["EmailSettings:Port"] ?? "—";
            ViewData["Sender"]     = _config["EmailSettings:SenderEmail"] ?? "—";
            ViewData["Env"]        = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            return View("~/Areas/Admin/Views/SystemSettings/Index.cshtml");
        }

        public IActionResult Email()
        {
            ViewData["Title"]      = "Email Settings";
            ViewData["Breadcrumb"] = "Admin / System Settings / Email";
            ViewData["SmtpServer"] = _config["EmailSettings:SmtpServer"] ?? "";
            ViewData["SmtpPort"]   = _config["EmailSettings:Port"] ?? "587";
            ViewData["SenderName"] = _config["EmailSettings:SenderName"] ?? "";
            ViewData["SenderEmail"]= _config["EmailSettings:SenderEmail"] ?? "";
            return View("~/Areas/Admin/Views/SystemSettings/Email.cshtml");
        }

        public IActionResult Cache()
        {
            ViewData["Title"]      = "Cache Settings";
            ViewData["Breadcrumb"] = "Admin / System Settings / Cache";
            return View("~/Areas/Admin/Views/SystemSettings/Cache.cshtml");
        }

        public IActionResult Performance()
        {
            ViewData["Title"]      = "Performance";
            ViewData["Breadcrumb"] = "Admin / System Settings / Performance";
            ViewData["GcGen0"] = GC.CollectionCount(0);
            ViewData["GcGen1"] = GC.CollectionCount(1);
            ViewData["GcGen2"] = GC.CollectionCount(2);
            ViewData["Memory"] = GC.GetTotalMemory(false) / 1024 / 1024;
            return View("~/Areas/Admin/Views/SystemSettings/Performance.cshtml");
        }
    }
}
