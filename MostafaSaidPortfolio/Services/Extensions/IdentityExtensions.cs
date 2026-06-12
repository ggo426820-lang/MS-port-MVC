using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MostafaSaidPortfolio.Data;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    // Password policy
                    options.Password.RequireDigit           = true;
                    options.Password.RequireUppercase       = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredLength         = 10;
                    options.Password.RequiredUniqueChars    = 4;

                    // Account lockout — 5 failed attempts locks for 15 minutes
                    options.Lockout.DefaultLockoutTimeSpan  = TimeSpan.FromMinutes(15);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers      = true;

                    // Each user must have a unique email
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Secure auth cookie settings
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly     = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite     = SameSiteMode.Strict;
                options.LoginPath           = "/Account/Login";
                options.AccessDeniedPath    = "/Account/AccessDenied";
                options.ExpireTimeSpan      = TimeSpan.FromHours(8);
                options.SlidingExpiration   = true;
            });

            return services;
        }
    }
}
