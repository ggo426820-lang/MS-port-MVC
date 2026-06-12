using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Data.UnitOfWork;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AnalyticsController : Controller
    {
        private readonly IUnitOfWork _uow;

        public AnalyticsController(IUnitOfWork uow) { _uow = uow; }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"]      = "Analytics";
            ViewData["Breadcrumb"] = "Admin / Analytics";
            ViewData["BlogCount"]    = await _uow.Blogs.CountPublishedAsync();
            ViewData["ProjectCount"] = await _uow.Projects.CountActiveAsync();
            ViewData["SubCount"]     = await _uow.Newsletter.CountAsync();
            return View("~/Areas/Admin/Views/Analytics/Index.cshtml");
        }

        public IActionResult RealTime()
        {
            ViewData["Title"] = "Real-Time Analytics";
            return View("~/Areas/Admin/Views/Analytics/RealTime.cshtml");
        }

        public IActionResult Reports()
        {
            ViewData["Title"] = "Analytics Reports";
            return View("~/Areas/Admin/Views/Analytics/Reports.cshtml");
        }

        public IActionResult Export()
        {
            ViewData["Title"] = "Export Analytics";
            return View("~/Areas/Admin/Views/Analytics/Export.cshtml");
        }
    }
}
