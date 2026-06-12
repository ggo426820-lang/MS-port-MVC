using Npgsql;

namespace MostafaSaidPortfolio.Data
{
    public class DbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            var raw = Environment.GetEnvironmentVariable("DATABASE_URL")
                ?? configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DATABASE_URL environment variable not set.");
            _connectionString = ConnectionHelper.ToNpgsqlConnectionString(raw);
        }

        public NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
