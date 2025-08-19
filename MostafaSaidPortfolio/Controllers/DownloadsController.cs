using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Controllers
{
    public class DownloadsController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Category(string category) => View();
    }
}
