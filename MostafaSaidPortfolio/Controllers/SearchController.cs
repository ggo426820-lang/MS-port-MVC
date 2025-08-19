using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Results(string q) => View();
    }
}
