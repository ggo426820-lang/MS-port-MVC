namespace MostafaSaidPortfolio.Extensions
{
    public static class EmailExtensions
    {
        public static IServiceCollection AddCustomEmail(this IServiceCollection services)
        {
            // Configure SMTP / SendGrid / SES settings here when email sending is needed.
            // Example:
            //   services.Configure<SmtpSettings>(config.GetSection("Smtp"));
            //   services.AddTransient<IEmailSender, SmtpEmailSender>();
            return services;
        }
    }
}
