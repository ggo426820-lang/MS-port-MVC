using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Success() => View();
        public IActionResult Error() => View();
    }
}
