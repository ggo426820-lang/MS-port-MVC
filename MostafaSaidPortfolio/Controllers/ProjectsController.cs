using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService) => _projectService = projectService;

        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetAllActiveAsync();
            return View(projects);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null) return NotFound();
            return View(project);
        }

        public async Task<IActionResult> Category(int id)
        {
            var projects = await _projectService.GetByCategoryAsync(id);
            return View("Index", projects);
        }
    }
}
