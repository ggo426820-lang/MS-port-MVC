using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Data.UnitOfWork;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SkillsController : Controller
    {
        private readonly IUnitOfWork _uow;

        public SkillsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"]      = "Skills";
            ViewData["Breadcrumb"] = "Admin / Skills";
            var skills = await _uow.Skills.GetAllAsync();
            return View("~/Areas/Admin/Views/Skills/Index.cshtml", skills);
        }

        public IActionResult Create()
        {
            ViewData["Title"]      = "Add Skill";
            ViewData["Breadcrumb"] = "Admin / Skills / Add";
            return View("~/Areas/Admin/Views/Skills/Create.cshtml", new Skill());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Skill model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Add Skill";
                return View("~/Areas/Admin/Views/Skills/Create.cshtml", model);
            }
            await _uow.Skills.AddAsync(model);
            TempData["Success"] = $"Skill '{model.Name}' added successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var skill = await _uow.Skills.GetByIdAsync(id);
            if (skill == null) return NotFound();
            ViewData["Title"]      = "Edit Skill";
            ViewData["Breadcrumb"] = "Admin / Skills / Edit";
            return View("~/Areas/Admin/Views/Skills/Edit.cshtml", skill);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Skill model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Edit Skill";
                return View("~/Areas/Admin/Views/Skills/Edit.cshtml", model);
            }
            var ok = await _uow.Skills.UpdateAsync(model);
            if (!ok) return NotFound();
            TempData["Success"] = $"Skill '{model.Name}' updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var skill = await _uow.Skills.GetByIdAsync(id);
            if (skill == null) return NotFound();
            ViewData["Title"]      = "Delete Skill";
            ViewData["Breadcrumb"] = "Admin / Skills / Delete";
            return View("~/Areas/Admin/Views/Skills/Delete.cshtml", skill);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ok = await _uow.Skills.DeleteAsync(id);
            TempData[ok ? "Success" : "Error"] = ok ? "Skill deleted." : "Skill not found.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Categories()
        {
            ViewData["Title"]      = "Skill Categories";
            ViewData["Breadcrumb"] = "Admin / Skills / Categories";
            var cats = await _uow.Skills.GetCategoriesAsync();
            return View("~/Areas/Admin/Views/Skills/Categories.cshtml", cats);
        }
    }
}
