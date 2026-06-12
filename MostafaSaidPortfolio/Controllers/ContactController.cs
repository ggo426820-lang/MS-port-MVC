using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Data.UnitOfWork;
using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Domain.Enums;
using MostafaSaidPortfolio.Domain.ViewModels.Contact;

namespace MostafaSaidPortfolio.Controllers
{
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _uow;

        public ContactController(IUnitOfWork uow) => _uow = uow;

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ContactViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            var message = new ContactMessage
            {
                Name  = model.Name,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message
            };

            await _uow.ContactMessages.AddAsync(message);
            return RedirectToAction("Success");
        }

        public IActionResult Success() => View();
    }
}

