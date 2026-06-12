using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Controllers
{
    public class CultureController : Controller
    {
        private static readonly HashSet<string> _supported =
            new(StringComparer.OrdinalIgnoreCase) { "en", "ar" };

        [HttpGet]
        public IActionResult Set(string culture, string returnUrl = "/")
        {
            if (!_supported.Contains(culture))
                culture = "en";

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires  = DateTimeOffset.UtcNow.AddYears(1),
                    IsEssential = true,
                    SameSite = SameSiteMode.Lax
                });

            if (!Url.IsLocalUrl(returnUrl))
                returnUrl = "/";

            return LocalRedirect(returnUrl);
        }
    }
}
