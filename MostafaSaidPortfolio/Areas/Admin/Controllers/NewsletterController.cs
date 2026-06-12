using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Data.UnitOfWork;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NewsletterController : Controller
    {
        private readonly IUnitOfWork _uow;

        public NewsletterController(IUnitOfWork uow) => _uow = uow;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"]      = "Newsletter Subscribers";
            ViewData["Breadcrumb"] = "Admin / Newsletter";
            var subscribers = await _uow.Newsletter.GetAllAsync();
            ViewData["ActiveCount"] = await _uow.Newsletter.CountActiveAsync();
            return View("~/Areas/Admin/Views/Newsletter/Index.cshtml", subscribers);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Unsubscribe(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return BadRequest();
            await _uow.Newsletter.UnsubscribeAsync(email);
            TempData["Success"] = $"'{email}' unsubscribed.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _uow.Newsletter.DeleteAsync(id);
            TempData[ok ? "Success" : "Error"] = ok ? "Subscriber deleted." : "Subscriber not found.";
            return RedirectToAction(nameof(Index));
        }
    }
}
