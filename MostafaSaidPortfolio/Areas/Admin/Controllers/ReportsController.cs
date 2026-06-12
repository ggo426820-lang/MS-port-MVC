using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Data.UnitOfWork;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        private readonly IUnitOfWork _uow;

        public ReportsController(IUnitOfWork uow) { _uow = uow; }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"]      = "Reports";
            ViewData["Breadcrumb"] = "Admin / Reports";
            ViewData["BlogCount"]    = await _uow.Blogs.CountPublishedAsync();
            ViewData["ProjectCount"] = await _uow.Projects.CountActiveAsync();
            ViewData["MsgCount"]     = await _uow.ContactMessages.CountAsync();
            ViewData["SubCount"]     = await _uow.Newsletter.CountAsync();
            return View("~/Areas/Admin/Views/Reports/Index.cshtml");
        }

        public IActionResult Custom()
        {
            ViewData["Title"] = "Custom Report";
            return View("~/Areas/Admin/Views/Reports/Custom.cshtml");
        }

        public IActionResult Export()
        {
            ViewData["Title"] = "Export Reports";
            return View("~/Areas/Admin/Views/Reports/Export.cshtml");
        }

        public IActionResult Scheduled()
        {
            ViewData["Title"] = "Scheduled Reports";
            return View("~/Areas/Admin/Views/Reports/Scheduled.cshtml");
        }
    }
}
