using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IBlogService _blogService;
        private readonly ITestimonialService _testimonialService;

        public HomeController(
            IProjectService projectService,
            IBlogService blogService,
            ITestimonialService testimonialService)
        {
            _projectService = projectService;
            _blogService = blogService;
            _testimonialService = testimonialService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Projects = await _projectService.GetFeaturedAsync(3);
            ViewBag.Blogs = await _blogService.GetRecentAsync(3);
            ViewBag.Testimonials = await _testimonialService.GetApprovedAsync();
            return View();
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View();
    }
}
