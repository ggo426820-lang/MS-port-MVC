using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Domain.Enums;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService) => _blogService = blogService;

        public async Task<IActionResult> Index()
        {
            var posts = await _blogService.GetAllPublishedAsync();
            return View(posts);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var post = await _blogService.GetByIdAsync(id);
            if (post == null) return NotFound();
            await _blogService.IncrementViewCountAsync(id);
            return View(post);
        }

        public async Task<IActionResult> Category(int categoryId)
        {
            var posts = await _blogService.GetByCategoryAsync(categoryId);
            return View("Index", posts);
        }

        public async Task<IActionResult> Search(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return View("Index", new List<BlogPost>());

            var posts = await _blogService.SearchAsync(q);
            ViewBag.Query = q;
            return View("Index", posts);
        }
    }
}

