using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MostafaSaidPortfolio.Data;
using MostafaSaidPortfolio.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MostafaSaidPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Latest 3 projects
            var projects = await _context.Projects
                .OrderByDescending(p => p.CreatedAt)
                .Take(3)
                .ToListAsync();

            // Latest 3 published blog posts
            var blogs = await _context.BlogPosts
                .Where(b => b.IsPublished)
                .OrderByDescending(b => b.CreatedAt)
                .Take(3)
                .ToListAsync();

            ViewBag.Projects = projects;
            ViewBag.Blogs = blogs;

            return View();
        }
    }
}
