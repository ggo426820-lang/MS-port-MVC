namespace MostafaSaidPortfolio.Extensions
{
    public static class SecurityHeadersExtensions
    {
        public static IApplicationBuilder UseSecurityHeaders(
            this IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                var h = context.Response.Headers;

                h.Append("X-Content-Type-Options", "nosniff");
                h.Append("X-Frame-Options",         "SAMEORIGIN");
                h.Append("X-XSS-Protection",        "1; mode=block");
                h.Append("Referrer-Policy",          "strict-origin-when-cross-origin");
                h.Append("Permissions-Policy",       "camera=(), microphone=(), geolocation=()");

                if (!env.IsDevelopment())
                {
                    h.Append("Content-Security-Policy",
                        "default-src 'self'; " +
                        "script-src 'self' 'unsafe-inline' https://cdn.tailwindcss.com https://cdnjs.cloudflare.com; " +
                        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com https://cdn.tailwindcss.com; " +
                        "font-src 'self' https://fonts.gstatic.com; " +
                        "img-src 'self' data: https:; " +
                        "connect-src 'self'");
                }

                await next();
            });

            return app;
        }
    }
}
