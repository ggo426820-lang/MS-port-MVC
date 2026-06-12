using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Data.UnitOfWork;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _uow;

        public DashboardController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"]      = "Dashboard";
            ViewData["Breadcrumb"] = "Admin / Dashboard";

            var blogCount       = await _uow.Blogs.CountPublishedAsync();
            var projectCount    = await _uow.Projects.CountActiveAsync();
            var msgCount        = await _uow.ContactMessages.CountAsync();
            var subCount        = await _uow.Newsletter.CountAsync();
            var eventCount      = await _uow.Events.CountAsync();
            var testimonialCount= await _uow.Testimonials.CountAsync();
            var skillCount      = await _uow.Skills.CountAsync();

            ViewData["BlogCount"]        = blogCount;
            ViewData["ProjectCount"]     = projectCount;
            ViewData["MessageCount"]     = msgCount;
            ViewData["SubscriberCount"]  = subCount;
            ViewData["EventCount"]       = eventCount;
            ViewData["TestimonialCount"] = testimonialCount;
            ViewData["SkillCount"]       = skillCount;

            var recentPosts    = await _uow.Blogs.GetRecentAsync(5);
            var recentMessages = (await _uow.ContactMessages.GetAllAsync()).Take(5);

            ViewData["RecentPosts"]    = recentPosts;
            ViewData["RecentMessages"] = recentMessages;

            return View("~/Areas/Admin/Views/AdminDashboard/Index.cshtml");
        }

        public IActionResult QuickActions()
        {
            ViewData["Title"] = "Quick Actions";
            return View("~/Areas/Admin/Views/AdminDashboard/QuickActions.cshtml");
        }

        public async Task<IActionResult> SystemStatus()
        {
            ViewData["Title"] = "System Status";
            ViewData["DbBlogCount"]    = await _uow.Blogs.CountAsync();
            ViewData["DbProjectCount"] = await _uow.Projects.CountAsync();
            ViewData["DbSkillCount"]   = await _uow.Skills.CountAsync();
            return View("~/Areas/Admin/Views/AdminDashboard/SystemStatus.cshtml");
        }
    }
}
