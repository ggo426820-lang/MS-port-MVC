using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class IntegrationsController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"]      = "Integrations";
            ViewData["Breadcrumb"] = "Admin / Integrations";
            return View("~/Areas/Admin/Views/Integrations/Index.cshtml");
        }

        public IActionResult Analytics()
        {
            ViewData["Title"] = "Analytics Integration";
            return View("~/Areas/Admin/Views/Integrations/Analytics.cshtml");
        }

        public IActionResult Social()
        {
            ViewData["Title"] = "Social Media";
            return View("~/Areas/Admin/Views/Integrations/Social.cshtml");
        }

        public IActionResult Webhooks()
        {
            ViewData["Title"] = "Webhooks";
            return View("~/Areas/Admin/Views/Integrations/Webhooks.cshtml");
        }
    }
}
