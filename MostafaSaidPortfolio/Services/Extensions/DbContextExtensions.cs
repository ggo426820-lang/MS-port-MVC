using Microsoft.EntityFrameworkCore;
using MostafaSaidPortfolio.Data;

namespace MostafaSaidPortfolio.Extensions
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
                ?? configuration.GetConnectionString("DefaultConnection")
                ?? string.Empty;
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            return services;
        }
    }
}
