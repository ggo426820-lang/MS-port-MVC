namespace MostafaSaidPortfolio.Data
{
    public static class ConnectionHelper
    {
        public static string ToNpgsqlConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Database connection string is empty.");

            if (!connectionString.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) &&
                !connectionString.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
                return connectionString;

            var uri = new Uri(connectionString);
            var host = uri.Host;
            var port = uri.Port > 0 ? uri.Port : 5432;
            var database = uri.AbsolutePath.TrimStart('/');

            string username = string.Empty;
            string password = string.Empty;
            if (!string.IsNullOrEmpty(uri.UserInfo))
            {
                var parts = uri.UserInfo.Split(':', 2);
                username = Uri.UnescapeDataString(parts[0]);
                if (parts.Length > 1)
                    password = Uri.UnescapeDataString(parts[1]);
            }

            var conn = $"Host={host};Port={port};Database={database};Username={username};Password={password}";

            if (!string.IsNullOrEmpty(uri.Query))
            {
                var query = uri.Query.TrimStart('?');
                foreach (var part in query.Split('&', StringSplitOptions.RemoveEmptyEntries))
                {
                    var kv = part.Split('=', 2);
                    if (kv.Length == 2)
                    {
                        var key = kv[0].ToLowerInvariant();
                        var value = Uri.UnescapeDataString(kv[1]);
                        if (key == "sslmode")
                            conn += $";SSL Mode={value}";
                    }
                }
            }

            return conn;
        }
    }
}
