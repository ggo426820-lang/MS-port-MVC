using Dapper;
using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Data;
using MostafaSaidPortfolio.ViewModels;

namespace MostafaSaidPortfolio.Controllers
{
    public class ContactController : Controller
    {
        private readonly DbConnectionFactory _factory;

        public ContactController(DbConnectionFactory factory) => _factory = factory;

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ContactViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            using var conn = _factory.CreateConnection();
            await conn.ExecuteAsync(@"
                INSERT INTO ""ContactMessages"" (""Name"", ""Email"", ""Subject"", ""Message"")
                VALUES (@Name, @Email, @Subject, @Message)", model);

            return RedirectToAction("Success");
        }

        public IActionResult Success() => View();
    }
}
