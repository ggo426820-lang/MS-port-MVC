using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TemplatesController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"]      = "Templates";
            ViewData["Breadcrumb"] = "Admin / Templates";
            return View("~/Areas/Admin/Views/Templates/Index.cshtml");
        }

        public IActionResult Email()
        {
            ViewData["Title"] = "Email Templates";
            return View("~/Areas/Admin/Views/Templates/Email.cshtml");
        }

        public IActionResult Notifications()
        {
            ViewData["Title"] = "Notification Templates";
            return View("~/Areas/Admin/Views/Templates/Notifications.cshtml");
        }

        public IActionResult Pdf()
        {
            ViewData["Title"] = "PDF Templates";
            return View("~/Areas/Admin/Views/Templates/Pdf.cshtml");
        }
    }
}
