using Microsoft.EntityFrameworkCore;
using MostafaSaidPortfolio.Data;

namespace MostafaSaidPortfolio.Extensions
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddCustomDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var raw = Environment.GetEnvironmentVariable("DATABASE_URL")
                ?? configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DATABASE_URL environment variable not set.");

            var connectionString = ConnectionHelper.ToNpgsqlConnectionString(raw);

            // EF Core — used by Identity only
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Dapper connection factory — used by all repositories
            services.AddSingleton<DbConnectionFactory>();

            return services;
        }
    }
}
