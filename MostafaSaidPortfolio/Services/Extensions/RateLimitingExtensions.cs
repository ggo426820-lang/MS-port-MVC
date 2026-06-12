using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace MostafaSaidPortfolio.Extensions
{
    public static class RateLimitingExtensions
    {
        public static IServiceCollection AddCustomRateLimiting(this IServiceCollection services)
        {
            services.AddRateLimiter(opts =>
            {
                opts.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                // Contact form — 5 submissions per hour per IP
                opts.AddFixedWindowLimiter("contact", policy =>
                {
                    policy.Window                  = TimeSpan.FromHours(1);
                    policy.PermitLimit             = 5;
                    policy.QueueLimit              = 0;
                    policy.QueueProcessingOrder    = QueueProcessingOrder.OldestFirst;
                });

                // Newsletter subscribe — 3 per hour per IP
                opts.AddFixedWindowLimiter("newsletter", policy =>
                {
                    policy.Window                  = TimeSpan.FromHours(1);
                    policy.PermitLimit             = 3;
                    policy.QueueLimit              = 0;
                    policy.QueueProcessingOrder    = QueueProcessingOrder.OldestFirst;
                });

                // Login — 10 attempts per 15 minutes per IP
                opts.AddFixedWindowLimiter("login", policy =>
                {
                    policy.Window                  = TimeSpan.FromMinutes(15);
                    policy.PermitLimit             = 10;
                    policy.QueueLimit              = 0;
                    policy.QueueProcessingOrder    = QueueProcessingOrder.OldestFirst;
                });
            });

            return services;
        }
    }
}
