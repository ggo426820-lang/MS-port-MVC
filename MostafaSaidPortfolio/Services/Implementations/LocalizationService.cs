using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class LocalizationService : ILocalizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocalizationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<IEnumerable<string>> GetSupportedCulturesAsync()
        {
            var cultures = new List<string> { "en-US", "ar-EG" }; // Example
            return Task.FromResult<IEnumerable<string>>(cultures);
        }

        public Task SetCultureAsync(string culture)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                context.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture))
                );
            }
            return Task.CompletedTask;
        }

        public Task<string> GetCurrentCultureAsync()
        {
            var culture = CultureInfo.CurrentCulture.Name;
            return Task.FromResult(culture);
        }
    }
}
