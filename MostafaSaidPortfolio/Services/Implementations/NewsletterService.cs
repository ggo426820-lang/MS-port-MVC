using Dapper;
using MostafaSaidPortfolio.Data;
using MostafaSaidPortfolio.Models;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class NewsletterService : INewsletterService
    {
        private readonly DbConnectionFactory _factory;

        public NewsletterService(DbConnectionFactory factory) => _factory = factory;

        public async Task<bool> SubscribeAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            using var conn = _factory.CreateConnection();
            try
            {
                await conn.ExecuteAsync(@"
                    INSERT INTO ""NewsletterSubscribers"" (""Email"", ""IsActive"")
                    VALUES (@email, TRUE)
                    ON CONFLICT (""Email"") DO UPDATE SET ""IsActive"" = TRUE", new { email });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UnsubscribeAsync(string email)
        {
            using var conn = _factory.CreateConnection();
            var rows = await conn.ExecuteAsync(@"
                UPDATE ""NewsletterSubscribers"" SET ""IsActive"" = FALSE WHERE ""Email"" = @email", new { email });
            return rows > 0;
        }

        public async Task<IEnumerable<NewsletterSubscriber>> GetAllAsync()
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryAsync<NewsletterSubscriber>(
                @"SELECT * FROM ""NewsletterSubscribers"" ORDER BY ""SubscribedAt"" DESC");
        }
    }
}
