using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Data.UnitOfWork;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TestimonialsController : Controller
    {
        private readonly IUnitOfWork _uow;

        public TestimonialsController(IUnitOfWork uow) => _uow = uow;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"]      = "Testimonials";
            ViewData["Breadcrumb"] = "Admin / Testimonials";
            var testimonials = await _uow.Testimonials.GetAllAsync();
            return View("~/Areas/Admin/Views/Testimonials/Index.cshtml", testimonials);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(Guid id)
        {
            var ok = await _uow.Testimonials.ApproveAsync(id);
            TempData[ok ? "Success" : "Error"] = ok ? "Testimonial approved." : "Not found.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(Guid id)
        {
            var ok = await _uow.Testimonials.RejectAsync(id);
            TempData[ok ? "Success" : "Error"] = ok ? "Testimonial rejected." : "Not found.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var t = await _uow.Testimonials.GetByIdAsync(id);
            if (t == null) return NotFound();
            ViewData["Title"]      = "Delete Testimonial";
            ViewData["Breadcrumb"] = "Admin / Testimonials / Delete";
            return View("~/Areas/Admin/Views/Testimonials/Delete.cshtml", t);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ok = await _uow.Testimonials.DeleteAsync(id);
            TempData[ok ? "Success" : "Error"] = ok ? "Testimonial deleted." : "Not found.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var t = await _uow.Testimonials.GetByIdAsync(id);
            if (t == null) return NotFound();
            ViewData["Title"]      = "Edit Testimonial";
            ViewData["Breadcrumb"] = "Admin / Testimonials / Edit";
            return View("~/Areas/Admin/Views/Testimonials/Edit.cshtml", t);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Testimonial model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid)
                return View("~/Areas/Admin/Views/Testimonials/Edit.cshtml", model);
            var ok = await _uow.Testimonials.UpdateAsync(model);
            if (!ok) return NotFound();
            TempData["Success"] = "Testimonial updated.";
            return RedirectToAction(nameof(Index));
        }
    }
}
