using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Domain.ViewModels.Blog;
using MostafaSaidPortfolio.Domain.ViewModels.Common;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private const int PageSize = 9;

        public BlogController(IBlogService blogService, ICategoryService categoryService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string? q, string? categorySlug, string sort = "newest", int page = 1)
        {
            page = Math.Max(1, page);

            Guid? categoryId = null;
            if (!string.IsNullOrWhiteSpace(categorySlug))
            {
                var cat = await _categoryService.GetBySlugAsync(categorySlug);
                categoryId = cat?.Id;
            }

            var (items, totalCount) = await _blogService.GetPagedAsync(q, categoryId, sort, page, PageSize);
            var categories = await _categoryService.GetActiveAsync();

            var vm = new BlogListViewModel
            {
                Posts = new PagedResult<Domain.Entities.BlogPost>
                {
                    Items      = items,
                    TotalCount = totalCount,
                    PageNumber = page,
                    PageSize   = PageSize
                },
                Categories   = categories,
                Search       = q,
                CategorySlug = categorySlug,
                CategoryId   = categoryId,
                Sort         = sort,
                Page         = page
            };

            return View(vm);
        }

        public async Task<IActionResult> Details(string id)
        {
            Domain.Entities.BlogPost? post = await _blogService.GetBySlugAsync(id);
            if (post == null && Guid.TryParse(id, out var guid))
                post = await _blogService.GetByIdAsync(guid);
            if (post == null) return NotFound();
            await _blogService.IncrementViewCountAsync(post.Id);
            return View(post);
        }
    }
}
