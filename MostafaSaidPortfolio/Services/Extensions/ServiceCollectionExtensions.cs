using Microsoft.Extensions.DependencyInjection;
using MostafaSaidPortfolio.Services.Interfaces;
using MostafaSaidPortfolio.Services.Implementations;

namespace MostafaSaidPortfolio.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ILocalizationService, LocalizationService>();
            services.AddScoped<INewsletterService, NewsletterService>();
            services.AddScoped<IEventsService, EventsService>();
            services.AddScoped<ITestimonialService, TestimonialService>();
            return services;
        }
    }
}
