using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Domain.ViewModels.Common;
using MostafaSaidPortfolio.Domain.ViewModels.Projects;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ICategoryService _categoryService;
        private const int PageSize = 9;

        public ProjectsController(IProjectService projectService, ICategoryService categoryService)
        {
            _projectService = projectService;
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

            var (items, totalCount) = await _projectService.GetPagedAsync(q, categoryId, sort, page, PageSize);
            var categories = await _categoryService.GetActiveAsync();

            var vm = new ProjectListViewModel
            {
                Projects = new PagedResult<Domain.Entities.Project>
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
            Domain.Entities.Project? project = await _projectService.GetBySlugAsync(id);
            if (project == null && Guid.TryParse(id, out var guid))
                project = await _projectService.GetByIdAsync(guid);
            if (project == null) return NotFound();
            return View(project);
        }
    }
}
