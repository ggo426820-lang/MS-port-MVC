using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Data.UnitOfWork;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _uow;

        public ContactController(IUnitOfWork uow) => _uow = uow;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"]      = "Contact Messages";
            ViewData["Breadcrumb"] = "Admin / Messages";
            var messages = await _uow.ContactMessages.GetAllAsync();
            return View("~/Areas/Admin/Views/Contact/Index.cshtml", messages);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var msg = await _uow.ContactMessages.GetByIdAsync(id);
            if (msg == null) return NotFound();
            if (!msg.IsRead)
                await _uow.ContactMessages.MarkAsReadAsync(id);
            ViewData["Title"]      = "Message Details";
            ViewData["Breadcrumb"] = "Admin / Messages / Details";
            return View("~/Areas/Admin/Views/Contact/Details.cshtml", msg);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkRead(Guid id)
        {
            await _uow.ContactMessages.MarkAsReadAsync(id);
            TempData["Success"] = "Message marked as read.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAllRead()
        {
            await _uow.ContactMessages.MarkAllAsReadAsync();
            TempData["Success"] = "All messages marked as read.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var msg = await _uow.ContactMessages.GetByIdAsync(id);
            if (msg == null) return NotFound();
            ViewData["Title"]      = "Delete Message";
            ViewData["Breadcrumb"] = "Admin / Messages / Delete";
            return View("~/Areas/Admin/Views/Contact/Delete.cshtml", msg);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ok = await _uow.ContactMessages.DeleteAsync(id);
            TempData[ok ? "Success" : "Error"] = ok ? "Message deleted." : "Message not found.";
            return RedirectToAction(nameof(Index));
        }
    }
}
