using MostafaSaidPortfolio.Areas.Admin.Services.Implementations;
using MostafaSaidPortfolio.Areas.Admin.Services.Interfaces;
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
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<INewsletterService, NewsletterService>();
            services.AddScoped<IEventsService, EventsService>();
            services.AddScoped<ITestimonialService, TestimonialService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ILocalizationService, LocalizationService>();
            services.AddScoped<IAccountService, AccountService>();

            // Admin services (singleton — they hold in-memory state within the process)
            services.AddSingleton<IAuditService, AuditService>();
            services.AddSingleton<IBackupService, BackupService>();
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<ISystemSettingsService, SystemSettingsService>();

            // Admin services (scoped — depend on Identity, need per-request lifetime)
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();

            return services;
        }
    }
}
