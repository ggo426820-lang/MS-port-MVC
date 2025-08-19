using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/404")]
        public IActionResult NotFoundPage() => View("404");

        [Route("Error/500")]
        public IActionResult InternalServerError() => View("500");

        [Route("Error/Maintenance")]
        public IActionResult Maintenance() => View("Maintenance");
    }
}
