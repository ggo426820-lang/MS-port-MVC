using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly INewsletterService _newsletterService;

        public NewsletterController(INewsletterService newsletterService) =>
            _newsletterService = newsletterService;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
                await _newsletterService.SubscribeAsync(email.Trim());

            TempData["SubscribeSuccess"] = "Thank you for subscribing!";
            string referer = Request.Headers["Referer"].ToString();
            return Redirect(string.IsNullOrEmpty(referer) ? "/" : referer);
        }
    }
}
