using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NotificationsController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"]      = "Notifications";
            ViewData["Breadcrumb"] = "Admin / Notifications";
            return View("~/Areas/Admin/Views/Notifications/Index.cshtml");
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Create Notification";
            return View("~/Areas/Admin/Views/Notifications/Create.cshtml");
        }

        public IActionResult History()
        {
            ViewData["Title"] = "Notification History";
            return View("~/Areas/Admin/Views/Notifications/History.cshtml");
        }

        public IActionResult Templates()
        {
            ViewData["Title"] = "Notification Templates";
            return View("~/Areas/Admin/Views/Notifications/Templates.cshtml");
        }
    }
}
