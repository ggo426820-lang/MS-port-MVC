using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace MostafaSaidPortfolio.Extensions
{
    public static class LocalizationExtensions
    {
        public static IServiceCollection AddCustomLocalization(this IServiceCollection services)
        {
            // Empty string ResourcesPath means root namespace lookup
            services.AddLocalization(opts => opts.ResourcesPath = "");

            var supported = new[] { new CultureInfo("en"), new CultureInfo("ar") };

            services.Configure<RequestLocalizationOptions>(opts =>
            {
                opts.DefaultRequestCulture             = new RequestCulture("en");
                opts.SupportedCultures                 = supported;
                opts.SupportedUICultures               = supported;
                opts.FallBackToParentCultures          = true;
                opts.FallBackToParentUICultures        = true;

                // Cookie provider first — persists the user's language choice across requests
                opts.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
            });

            return services;
        }
    }
}
