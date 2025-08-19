# Base path for Extensions folder
$basePath = "C:\Users\Mostafa\OneDrive\Desktop\Projects\MostafaSaidPortfolio\MostafaSaidPortfolio\Services\Extensions"

# Create the folder if it doesn't exist
if (!(Test-Path $basePath)) { New-Item -ItemType Directory -Path $basePath -Force }

# Extension files with functional templates
$files = @{
    "ServiceCollectionExtensions.cs" = @"
using Microsoft.Extensions.DependencyInjection;
using MostafaSaidPortfolio.Services.Interfaces;
using MostafaSaidPortfolio.Services.Implementations;

namespace MostafaSaidPortfolio.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPortfolioServices(this IServiceCollection services)
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
"@

    "DbContextExtensions.cs" = @"
using MostafaSaidPortfolio.Models;
using System;
using System.Linq;

namespace MostafaSaidPortfolio.Services.Extensions
{
    public static class DbContextExtensions
    {
        public static void SeedInitialData(this AppDbContext context)
        {
            if (!context.Projects.Any())
            {
                context.Projects.Add(new Project { Title = ""Sample Project"", Description = ""This is a sample project."", CreatedAt = DateTime.Now });
            }

            if (!context.BlogPosts.Any())
            {
                context.BlogPosts.Add(new BlogPost { Title = ""Welcome Post"", Content = ""This is your first blog post."", CreatedAt = DateTime.Now });
            }

            context.SaveChanges();
        }
    }
}
"@

    "IdentityExtensions.cs" = @"
using Microsoft.AspNetCore.Identity;
using MostafaSaidPortfolio.Models;
using System.Threading.Tasks;

namespace MostafaSaidPortfolio.Services.Extensions
{
    public static class IdentityExtensions
    {
        public static async Task EnsureRoleExistsAsync(this RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        public static async Task AssignRoleIfNotExistsAsync(this UserManager<ApplicationUser> userManager, ApplicationUser user, string roleName)
        {
            if (!await userManager.IsInRoleAsync(user, roleName))
            {
                await userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}
"@

    "EmailExtensions.cs" = @"
using System.Net.Mail;

namespace MostafaSaidPortfolio.Services.Extensions
{
    public static class EmailExtensions
    {
        public static bool IsValidEmail(this string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static string FormatEmailTemplate(string name, string message)
        {
            return $""Hello {name},\n\n{message}\n\nBest regards,\nMostafa Said"";
        }
    }
}
"@

    "LocalizationExtensions.cs" = @"
using Microsoft.Extensions.Localization;

namespace MostafaSaidPortfolio.Services.Extensions
{
    public static class LocalizationExtensions
    {
        public static string Translate(this IStringLocalizer localizer, string key)
        {
            return localizer[key];
        }
    }
}
"@

    "NewsletterExtensions.cs" = @"
using MostafaSaidPortfolio.Models;

namespace MostafaSaidPortfolio.Services.Extensions
{
    public static class NewsletterExtensions
    {
        public static bool IsSubscribed(this NewsletterSubscriber subscriber)
        {
            return subscriber != null && subscriber.IsActive;
        }
    }
}
"@

    "EventsExtensions.cs" = @"
using MostafaSaidPortfolio.Models;
using System;

namespace MostafaSaidPortfolio.Services.Extensions
{
    public static class EventsExtensions
    {
        public static bool IsUpcoming(this Event ev)
        {
            return ev != null && ev.Date > DateTime.Now;
        }

        public static bool IsPast(this Event ev)
        {
            return ev != null && ev.Date <= DateTime.Now;
        }
    }
}
"@
}

# Create each file
foreach ($file in $files.GetEnumerator()) {
    $filePath = Join-Path $basePath $file.Key
    $file.Value | Out-File -FilePath $filePath -Encoding UTF8
    Write-Host "Created: $filePath"
}

Write-Host "All functional extension files created successfully!"
