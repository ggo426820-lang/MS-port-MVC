using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Details(int id) => View();
        public IActionResult Register(int id) => View();
    }
}
