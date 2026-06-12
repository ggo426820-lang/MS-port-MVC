using MostafaSaidPortfolio.Data.UnitOfWork;
using MostafaSaidPortfolio.Services.Implementations;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            // Unit of Work — one per HTTP request (owns the DB connection + optional transaction)
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Application services — delegate to UoW / repositories
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<INewsletterService, NewsletterService>();
            services.AddScoped<IEventsService, EventsService>();
            services.AddScoped<ITestimonialService, TestimonialService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ILocalizationService, LocalizationService>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
