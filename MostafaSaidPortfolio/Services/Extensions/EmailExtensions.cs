using Microsoft.Extensions.DependencyInjection;

namespace MostafaSaidPortfolio.Extensions
{
    public static class EmailExtensions
    {
        public static IServiceCollection AddCustomEmail(this IServiceCollection services)
        {
            // TODO: Configure your email services here
            // Example:
            // services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
