using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AuditController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"]      = "Audit Log";
            ViewData["Breadcrumb"] = "Admin / Audit Log";
            return View("~/Areas/Admin/Views/Audit/Index.cshtml");
        }

        public IActionResult Details(string id)
        {
            ViewData["Title"] = "Audit Entry Details";
            ViewData["EntryId"] = id;
            return View("~/Areas/Admin/Views/Audit/Details.cshtml");
        }

        public IActionResult Search(string q)
        {
            ViewData["Title"] = "Search Audit Log";
            ViewData["Query"] = q;
            return View("~/Areas/Admin/Views/Audit/Search.cshtml");
        }
    }
}
