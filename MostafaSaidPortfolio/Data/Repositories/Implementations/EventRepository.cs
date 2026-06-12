using Dapper;
using MostafaSaidPortfolio.Data.Repositories.Interfaces;
using MostafaSaidPortfolio.Models;
using Npgsql;

namespace MostafaSaidPortfolio.Data.Repositories.Implementations
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        protected override string TableName => "Events";
        protected override string Columns => @"""Id"", ""Title"", ""Description"", ""Date"", ""EndDate"", ""Location"", ""IsOnline"", ""MaxAttendees"", ""CreatedAt""";

        public EventRepository(NpgsqlConnection connection) : base(connection) { }

        public async Task<IEnumerable<Event>> GetUpcomingAsync()
        {
            return await _connection.QueryAsync<Event>(
                $@"SELECT {Columns} FROM ""Events"" WHERE ""Date"" >= NOW() ORDER BY ""Date"" ASC",
                transaction: _transaction);
        }

        public async Task<IEnumerable<Event>> GetPastAsync()
        {
            return await _connection.QueryAsync<Event>(
                $@"SELECT {Columns} FROM ""Events"" WHERE ""Date"" < NOW() ORDER BY ""Date"" DESC",
                transaction: _transaction);
        }

        public async Task<int> CountUpcomingAsync()
        {
            return await _connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM ""Events"" WHERE ""Date"" >= NOW()",
                transaction: _transaction);
        }

        public override async Task<int> AddAsync(Event entity)
        {
            return await _connection.ExecuteScalarAsync<int>(@"
                INSERT INTO ""Events"" (""Title"", ""Description"", ""Date"", ""EndDate"", ""Location"", ""IsOnline"", ""MaxAttendees"")
                VALUES (@Title, @Description, @Date, @EndDate, @Location, @IsOnline, @MaxAttendees)
                RETURNING ""Id""", entity, _transaction);
        }

        public override async Task<bool> UpdateAsync(Event entity)
        {
            var rows = await _connection.ExecuteAsync(@"
                UPDATE ""Events"" SET
                    ""Title"" = @Title, ""Description"" = @Description, ""Date"" = @Date,
                    ""EndDate"" = @EndDate, ""Location"" = @Location,
                    ""IsOnline"" = @IsOnline, ""MaxAttendees"" = @MaxAttendees
                WHERE ""Id"" = @Id", entity, _transaction);
            return rows > 0;
        }
    }
}
