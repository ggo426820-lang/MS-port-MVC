using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MostafaSaidPortfolio.Data;
using MostafaSaidPortfolio.Models;
using System.Linq;

namespace MostafaSaidPortfolio.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Blog
        public async Task<IActionResult> Index()
        {
            var posts = await _context.BlogPosts
                                      .Where(p => p.IsPublished)
                                      .OrderByDescending(p => p.CreatedAt)
                                      .ToListAsync();
            return View(posts);
        }

        // GET: /Blog/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var post = await _context.BlogPosts
                                     .Include(p => p.Category)
                                     .FirstOrDefaultAsync(p => p.Id == id && p.IsPublished);

            if (post == null) return NotFound();
            return View(post);
        }

        // GET: /Blog/Category/development
        public async Task<IActionResult> Category(string category)
        {
            if (string.IsNullOrEmpty(category)) return NotFound();

            var posts = await _context.BlogPosts
                                      .Include(p => p.Category)
                                      .Where(p => p.Category.Name.ToLower() == category.ToLower() && p.IsPublished)
                                      .OrderByDescending(p => p.CreatedAt)
                                      .ToListAsync();

            ViewBag.CategoryName = category;
            return View(posts);
        }

        // GET: /Blog/Archive
        public async Task<IActionResult> Archive()
        {
            var posts = await _context.BlogPosts
                                      .Where(p => p.IsPublished)
                                      .OrderByDescending(p => p.CreatedAt)
                                      .ToListAsync();
            return View(posts);
        }

        // GET: /Blog/Search?q=aspnet
        public async Task<IActionResult> Search(string q)
        {
            if (string.IsNullOrEmpty(q)) return View(new List<BlogPost>());

            var posts = await _context.BlogPosts
                                      .Where(p => p.IsPublished && (p.Name.Contains(q) || p.Content.Contains(q)))
                                      .OrderByDescending(p => p.CreatedAt)
                                      .ToListAsync();

            ViewBag.Query = q;
            return View(posts);
        }

        // GET: /Blog/Tags/tagname
        public async Task<IActionResult> Tags(string tag)
        {
            if (string.IsNullOrEmpty(tag)) return NotFound();

            var posts = await _context.BlogPosts
                .Include(p => p.Tags)
                .Where(p => p.IsPublished && p.Tags.Any(t => t.Name.ToLower() == tag.ToLower()))
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            ViewBag.Tag = tag;
            return View(posts);
        }
    }
}
