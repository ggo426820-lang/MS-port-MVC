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
            // Only redirect to local paths — never follow external Referer URLs
            string referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer) && Uri.TryCreate(referer, UriKind.Absolute, out var refUri)
                && string.Equals(refUri.Authority, Request.Host.Value, StringComparison.OrdinalIgnoreCase))
            {
                return Redirect(refUri.PathAndQuery);
            }
            return Redirect("/");
        }
    }
}
