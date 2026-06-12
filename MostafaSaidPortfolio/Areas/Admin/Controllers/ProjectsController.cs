using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Data.UnitOfWork;
using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Domain.Enums;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProjectsController : Controller
    {
        private readonly IUnitOfWork _uow;

        public ProjectsController(IUnitOfWork uow) => _uow = uow;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"]      = "Projects";
            ViewData["Breadcrumb"] = "Admin / Projects";
            var projects = await _uow.Projects.GetAllAsync();
            return View("~/Areas/Admin/Views/Projects/Index.cshtml", projects);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Title"]      = "New Project";
            ViewData["Breadcrumb"] = "Admin / Projects / New";
            ViewData["Categories"] = await _uow.Categories.GetActiveAsync();
            return View("~/Areas/Admin/Views/Projects/Create.cshtml",
                new Domain.Entities.Project { Id = Guid.NewGuid(), Status = ProjectStatus.Active });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Domain.Entities.Project model)
        {
            if (string.IsNullOrWhiteSpace(model.Slug))
                model.Slug = Slugify(model.Title);
            ModelState.Remove(nameof(model.Slug));
            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = await _uow.Categories.GetActiveAsync();
                return View("~/Areas/Admin/Views/Projects/Create.cshtml", model);
            }
            await _uow.Projects.AddAsync(model);
            TempData["Success"] = $"Project '{model.Title}' created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var project = await _uow.Projects.GetByIdAsync(id);
            if (project == null) return NotFound();
            ViewData["Title"]      = "Edit Project";
            ViewData["Breadcrumb"] = "Admin / Projects / Edit";
            ViewData["Categories"] = await _uow.Categories.GetActiveAsync();
            return View("~/Areas/Admin/Views/Projects/Edit.cshtml", project);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Domain.Entities.Project model)
        {
            if (id != model.Id) return BadRequest();
            if (string.IsNullOrWhiteSpace(model.Slug)) model.Slug = Slugify(model.Title);
            ModelState.Remove(nameof(model.Slug));
            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = await _uow.Categories.GetActiveAsync();
                return View("~/Areas/Admin/Views/Projects/Edit.cshtml", model);
            }
            var ok = await _uow.Projects.UpdateAsync(model);
            if (!ok) return NotFound();
            TempData["Success"] = $"Project '{model.Title}' updated.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var project = await _uow.Projects.GetByIdAsync(id);
            if (project == null) return NotFound();
            ViewData["Title"]      = "Delete Project";
            ViewData["Breadcrumb"] = "Admin / Projects / Delete";
            return View("~/Areas/Admin/Views/Projects/Delete.cshtml", project);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ok = await _uow.Projects.DeleteAsync(id);
            TempData[ok ? "Success" : "Error"] = ok ? "Project deleted." : "Project not found.";
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
