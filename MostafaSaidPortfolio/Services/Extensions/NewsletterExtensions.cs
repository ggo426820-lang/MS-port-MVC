namespace MostafaSaidPortfolio.Extensions
{
    public static class NewsletterExtensions
    {
        public static IServiceCollection AddCustomNewsletter(this IServiceCollection services)
        {
            // Configure newsletter background jobs, queue providers, or MailChimp / SendGrid
            // integration settings here.
            // Example:
            //   services.AddHostedService<NewsletterDispatchService>();
            return services;
        }
    }
}
