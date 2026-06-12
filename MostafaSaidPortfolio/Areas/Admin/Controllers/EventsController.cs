using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Data.UnitOfWork;
using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Domain.Enums;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EventsController : Controller
    {
        private readonly IUnitOfWork _uow;

        public EventsController(IUnitOfWork uow) => _uow = uow;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"]      = "Events";
            ViewData["Breadcrumb"] = "Admin / Events";
            var events = await _uow.Events.GetAllAsync();
            return View("~/Areas/Admin/Views/Events/Index.cshtml", events);
        }

        public IActionResult Create()
        {
            ViewData["Title"]      = "New Event";
            ViewData["Breadcrumb"] = "Admin / Events / New";
            return View("~/Areas/Admin/Views/Events/Create.cshtml",
                new Event { Id = Guid.NewGuid(), EventDate = DateTime.UtcNow.AddDays(7), Status = EventStatus.Upcoming });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event model)
        {
            if (!ModelState.IsValid)
                return View("~/Areas/Admin/Views/Events/Create.cshtml", model);
            await _uow.Events.AddAsync(model);
            TempData["Success"] = $"Event '{model.Title}' created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var evt = await _uow.Events.GetByIdAsync(id);
            if (evt == null) return NotFound();
            ViewData["Title"]      = "Edit Event";
            ViewData["Breadcrumb"] = "Admin / Events / Edit";
            return View("~/Areas/Admin/Views/Events/Edit.cshtml", evt);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Event model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid)
                return View("~/Areas/Admin/Views/Events/Edit.cshtml", model);
            var ok = await _uow.Events.UpdateAsync(model);
            if (!ok) return NotFound();
            TempData["Success"] = $"Event '{model.Title}' updated.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var evt = await _uow.Events.GetByIdAsync(id);
            if (evt == null) return NotFound();
            ViewData["Title"]      = "Delete Event";
            ViewData["Breadcrumb"] = "Admin / Events / Delete";
            return View("~/Areas/Admin/Views/Events/Delete.cshtml", evt);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ok = await _uow.Events.DeleteAsync(id);
            TempData[ok ? "Success" : "Error"] = ok ? "Event deleted." : "Event not found.";
            return RedirectToAction(nameof(Index));
        }
    }
}
