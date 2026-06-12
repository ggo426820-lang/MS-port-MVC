using Dapper;
using MostafaSaidPortfolio.Data.Repositories.Interfaces;
using MostafaSaidPortfolio.Domain.Entities;
using Npgsql;

namespace MostafaSaidPortfolio.Data.Repositories.Implementations
{
    public class NewsletterRepository : BaseRepository<NewsletterSubscriber>, INewsletterRepository
    {
        protected override string TableName => "NewsletterSubscribers";
        protected override string Columns => @"""Id"", ""Email"", ""Name"", ""IsActive"", ""IsConfirmed"", ""CreatedAt""";

        public NewsletterRepository(NpgsqlConnection connection) : base(connection) { }

        public async Task<bool> SubscribeAsync(string email)
        {
            try
            {
                await _connection.ExecuteAsync(@"
                    INSERT INTO ""NewsletterSubscribers"" (""Id"", ""Email"", ""IsActive"")
                    VALUES (gen_random_uuid(), @email, TRUE)
                    ON CONFLICT (""Email"") DO UPDATE SET ""IsActive"" = TRUE",
                    new { email }, _transaction);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UnsubscribeAsync(string email)
        {
            var rows = await _connection.ExecuteAsync(
                @"UPDATE ""NewsletterSubscribers"" SET ""IsActive"" = FALSE WHERE ""Email"" = @email",
                new { email }, _transaction);
            return rows > 0;
        }

        public async Task<NewsletterSubscriber?> GetByEmailAsync(string email)
        {
            return await _connection.QueryFirstOrDefaultAsync<NewsletterSubscriber>(
                $@"SELECT {Columns} FROM ""NewsletterSubscribers"" WHERE ""Email"" = @email",
                new { email }, _transaction);
        }

        public async Task<IEnumerable<NewsletterSubscriber>> GetActiveAsync()
        {
            return await _connection.QueryAsync<NewsletterSubscriber>(
                $@"SELECT {Columns} FROM ""NewsletterSubscribers"" WHERE ""IsActive"" = TRUE ORDER BY ""CreatedAt"" DESC",
                transaction: _transaction);
        }

        public async Task<int> CountActiveAsync()
        {
            return await _connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM ""NewsletterSubscribers"" WHERE ""IsActive"" = TRUE",
                transaction: _transaction);
        }

        public override async Task AddAsync(NewsletterSubscriber entity)
        {
            await _connection.ExecuteAsync(@"
                INSERT INTO ""NewsletterSubscribers"" (""Id"", ""Email"", ""Name"", ""IsActive"")
                VALUES (@Id, @Email, @Name, @IsActive)
                ON CONFLICT (""Email"") DO UPDATE SET ""IsActive"" = TRUE",
                entity, _transaction);
        }

        public override async Task<bool> UpdateAsync(NewsletterSubscriber entity)
        {
            var rows = await _connection.ExecuteAsync(@"
                UPDATE ""NewsletterSubscribers"" SET ""IsActive"" = @IsActive WHERE ""Id"" = @Id",
                entity, _transaction);
            return rows > 0;
        }
    }
}
