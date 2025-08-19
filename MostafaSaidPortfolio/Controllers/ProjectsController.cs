using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MostafaSaidPortfolio.Data;
using MostafaSaidPortfolio.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MostafaSaidPortfolio.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                .Include(p => p.Category)
                .Where(p => !p.IsDeleted)
                .OrderBy(p => p.DisplayOrder)
                .ToListAsync();
            return View(projects);
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var project = await _context.Projects
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);

            if (project == null) return NotFound();

            return View(project);
        }

        // GET: Projects/Category/5
        public async Task<IActionResult> Category(int? id)
        {
            if (id == null) return NotFound();

            var projects = await _context.Projects
                .Include(p => p.Category)
                .Where(p => p.CategoryId == id && !p.IsDeleted)
                .OrderBy(p => p.DisplayOrder)
                .ToListAsync();

            return View(projects);
        }

        // GET: Projects/Search?query=example
        public async Task<IActionResult> Search(string query)
        {
            var projects = await _context.Projects
                .Include(p => p.Category)
                .Where(p => !p.IsDeleted &&
                            (p.Title.Contains(query) || p.Description.Contains(query)))
                .OrderBy(p => p.DisplayOrder)
                .ToListAsync();

            ViewBag.Query = query;
            return View(projects);
        }

        // GET: Projects/Archive
        public async Task<IActionResult> Archive()
        {
            var projects = await _context.Projects
                .Include(p => p.Category)
                .Where(p => p.IsDeleted)
                .OrderByDescending(p => p.UpdatedAt)
                .ToListAsync();

            return View(projects);
        }
    }
}
