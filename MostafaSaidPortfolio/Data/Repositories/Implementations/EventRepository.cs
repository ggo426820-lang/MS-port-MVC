using Dapper;
using MostafaSaidPortfolio.Data.Repositories.Interfaces;
using MostafaSaidPortfolio.Domain.Entities;
using Npgsql;

namespace MostafaSaidPortfolio.Data.Repositories.Implementations
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        protected override string TableName => "Events";
        protected override string Columns => @"""Id"", ""Title"", ""Description"", ""EventDate"", ""EventEndDate"", ""Location"", ""EventUrl"", ""Status"", ""MaxAttendees"", ""RegisteredCount"", ""IsFeatured"", ""DisplayOrder"", ""CreatedAt""";

        public EventRepository(NpgsqlConnection connection) : base(connection) { }

        public async Task<IEnumerable<Event>> GetUpcomingAsync()
        {
            return await _connection.QueryAsync<Event>(
                $@"SELECT {Columns} FROM ""Events"" WHERE ""EventDate"" >= NOW() AND ""IsDeleted"" = FALSE ORDER BY ""EventDate"" ASC",
                transaction: _transaction);
        }

        public async Task<IEnumerable<Event>> GetPastAsync()
        {
            return await _connection.QueryAsync<Event>(
                $@"SELECT {Columns} FROM ""Events"" WHERE ""EventDate"" < NOW() AND ""IsDeleted"" = FALSE ORDER BY ""EventDate"" DESC",
                transaction: _transaction);
        }

        public async Task<int> CountUpcomingAsync()
        {
            return await _connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM ""Events"" WHERE ""EventDate"" >= NOW() AND ""IsDeleted"" = FALSE",
                transaction: _transaction);
        }

        public override async Task AddAsync(Event entity)
        {
            await _connection.ExecuteAsync(@"
                INSERT INTO ""Events"" (""Id"", ""Title"", ""Description"", ""EventDate"", ""EventEndDate"", ""Location"", ""EventUrl"", ""Status"", ""MaxAttendees"", ""IsFeatured"", ""DisplayOrder"")
                VALUES (@Id, @Title, @Description, @EventDate, @EventEndDate, @Location, @EventUrl, @Status, @MaxAttendees, @IsFeatured, @DisplayOrder)",
                entity, _transaction);
        }

        public override async Task<bool> UpdateAsync(Event entity)
        {
            var rows = await _connection.ExecuteAsync(@"
                UPDATE ""Events"" SET
                    ""Title"" = @Title, ""Description"" = @Description, ""EventDate"" = @EventDate,
                    ""EventEndDate"" = @EventEndDate, ""Location"" = @Location,
                    ""EventUrl"" = @EventUrl, ""Status"" = @Status, ""MaxAttendees"" = @MaxAttendees
                WHERE ""Id"" = @Id", entity, _transaction);
            return rows > 0;
        }
    }
}
