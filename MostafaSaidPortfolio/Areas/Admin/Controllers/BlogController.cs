using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Data.UnitOfWork;
using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Domain.Enums;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly IUnitOfWork _uow;

        public BlogController(IUnitOfWork uow) => _uow = uow;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"]      = "Blog Posts";
            ViewData["Breadcrumb"] = "Admin / Blog";
            var posts = await _uow.Blogs.GetAllAsync();
            return View("~/Areas/Admin/Views/Blog/Index.cshtml", posts);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Title"]      = "New Post";
            ViewData["Breadcrumb"] = "Admin / Blog / New";
            ViewData["Categories"] = await _uow.Categories.GetActiveAsync();
            return View("~/Areas/Admin/Views/Blog/Create.cshtml", new BlogPost { Id = Guid.NewGuid() });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPost model)
        {
            if (string.IsNullOrWhiteSpace(model.Slug))
                model.Slug = Slugify(model.Title);

            ModelState.Remove(nameof(model.Slug));
            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = await _uow.Categories.GetActiveAsync();
                return View("~/Areas/Admin/Views/Blog/Create.cshtml", model);
            }

            if (model.Status == BlogPostStatus.Published && model.PublishedAt == null)
                model.PublishedAt = DateTime.UtcNow;

            await _uow.Blogs.AddAsync(model);
            TempData["Success"] = $"Post '{model.Title}' created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var post = await _uow.Blogs.GetByIdAsync(id);
            if (post == null) return NotFound();
            ViewData["Title"]      = "Edit Post";
            ViewData["Breadcrumb"] = "Admin / Blog / Edit";
            ViewData["Categories"] = await _uow.Categories.GetActiveAsync();
            return View("~/Areas/Admin/Views/Blog/Edit.cshtml", post);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BlogPost model)
        {
            if (id != model.Id) return BadRequest();
            if (string.IsNullOrWhiteSpace(model.Slug)) model.Slug = Slugify(model.Title);
            ModelState.Remove(nameof(model.Slug));
            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = await _uow.Categories.GetActiveAsync();
                return View("~/Areas/Admin/Views/Blog/Edit.cshtml", model);
            }
            if (model.Status == BlogPostStatus.Published && model.PublishedAt == null)
                model.PublishedAt = DateTime.UtcNow;
            var ok = await _uow.Blogs.UpdateAsync(model);
            if (!ok) return NotFound();
            TempData["Success"] = $"Post '{model.Title}' updated.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var post = await _uow.Blogs.GetByIdAsync(id);
            if (post == null) return NotFound();
            ViewData["Title"]      = "Delete Post";
            ViewData["Breadcrumb"] = "Admin / Blog / Delete";
            return View("~/Areas/Admin/Views/Blog/Delete.cshtml", post);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ok = await _uow.Blogs.DeleteAsync(id);
            TempData[ok ? "Success" : "Error"] = ok ? "Post deleted." : "Post not found.";
            return RedirectToAction(nameof(Index));
        }

        private static string Slugify(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return Guid.NewGuid().ToString("N")[..8];
            return System.Text.RegularExpressions.Regex.Replace(
                title.ToLowerInvariant().Trim(), @"[^a-z0-9]+", "-").Trim('-');
        }
    }
}
