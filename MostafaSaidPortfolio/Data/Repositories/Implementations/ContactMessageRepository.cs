using Dapper;
using MostafaSaidPortfolio.Data.Repositories.Interfaces;
using MostafaSaidPortfolio.Domain.Entities;
using Npgsql;

namespace MostafaSaidPortfolio.Data.Repositories.Implementations
{
    public class ContactMessageRepository : BaseRepository<ContactMessage>, IContactMessageRepository
    {
        protected override string TableName => "ContactMessages";
        protected override string Columns => @"""Id"", ""Name"", ""Email"", ""Subject"", ""Message"", ""CreatedAt"", ""IsRead""";

        public ContactMessageRepository(NpgsqlConnection connection) : base(connection) { }

        public override async Task<IEnumerable<ContactMessage>> GetAllAsync()
        {
            return await _connection.QueryAsync<ContactMessage>(
                $@"SELECT {Columns} FROM ""ContactMessages"" ORDER BY ""CreatedAt"" DESC",
                transaction: _transaction);
        }

        public async Task<IEnumerable<ContactMessage>> GetUnreadAsync()
        {
            return await _connection.QueryAsync<ContactMessage>(
                $@"SELECT {Columns} FROM ""ContactMessages"" WHERE ""IsRead"" = FALSE ORDER BY ""CreatedAt"" DESC",
                transaction: _transaction);
        }

        public async Task<bool> MarkAsReadAsync(Guid id)
        {
            var rows = await _connection.ExecuteAsync(
                @"UPDATE ""ContactMessages"" SET ""IsRead"" = TRUE WHERE ""Id"" = @id",
                new { id }, _transaction);
            return rows > 0;
        }

        public async Task<bool> MarkAllAsReadAsync()
        {
            var rows = await _connection.ExecuteAsync(
                @"UPDATE ""ContactMessages"" SET ""IsRead"" = TRUE WHERE ""IsRead"" = FALSE",
                transaction: _transaction);
            return rows > 0;
        }

        public async Task<IEnumerable<ContactMessage>> GetRecentAsync(int count)
        {
            return await _connection.QueryAsync<ContactMessage>(
                $@"SELECT {Columns} FROM ""ContactMessages"" ORDER BY ""CreatedAt"" DESC LIMIT @count",
                new { count }, _transaction);
        }

        public async Task<int> CountUnreadAsync()
        {
            return await _connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM ""ContactMessages"" WHERE ""IsRead"" = FALSE",
                transaction: _transaction);
        }

        public new async Task<bool> DeleteAsync(Guid id)
        {
            var rows = await _connection.ExecuteAsync(
                @"DELETE FROM ""ContactMessages"" WHERE ""Id"" = @id",
                new { id }, _transaction);
            return rows > 0;
        }

        public override async Task AddAsync(ContactMessage entity)
        {
            await _connection.ExecuteAsync(@"
                INSERT INTO ""ContactMessages"" (""Id"", ""Name"", ""Email"", ""Subject"", ""Message"")
                VALUES (@Id, @Name, @Email, @Subject, @Message)",
                entity, _transaction);
        }

        public override async Task<bool> UpdateAsync(ContactMessage entity)
        {
            var rows = await _connection.ExecuteAsync(@"
                UPDATE ""ContactMessages"" SET ""IsRead"" = @IsRead WHERE ""Id"" = @Id",
                entity, _transaction);
            return rows > 0;
        }
    }
}
