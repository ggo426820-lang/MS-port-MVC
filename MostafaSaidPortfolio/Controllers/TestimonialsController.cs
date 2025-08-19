using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Controllers
{
    public class TestimonialsController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Submit() => View();
    }
}
