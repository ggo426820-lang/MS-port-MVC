using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Controllers
{
    public class NewsletterController : Controller
    {
        public IActionResult Subscribe() => View();
        public IActionResult Unsubscribe() => View();
        public IActionResult Preferences() => View();
    }
}
