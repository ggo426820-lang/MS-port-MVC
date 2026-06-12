using Dapper;
using MostafaSaidPortfolio.Data;
using MostafaSaidPortfolio.Models;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class EventsService : IEventsService
    {
        private readonly DbConnectionFactory _factory;

        public EventsService(DbConnectionFactory factory) => _factory = factory;

        public async Task<IEnumerable<Event>> GetUpcomingAsync()
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryAsync<Event>(@"
                SELECT * FROM ""Events""
                WHERE ""Date"" >= NOW()
                ORDER BY ""Date"" ASC");
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryAsync<Event>(@"
                SELECT * FROM ""Events"" ORDER BY ""Date"" DESC");
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<Event>(
                @"SELECT * FROM ""Events"" WHERE ""Id"" = @id", new { id });
        }

        public async Task<Event> AddAsync(Event entity)
        {
            using var conn = _factory.CreateConnection();
            entity.Id = await conn.ExecuteScalarAsync<int>(@"
                INSERT INTO ""Events"" (""Title"", ""Description"", ""Date"", ""EndDate"", ""Location"", ""IsOnline"", ""MaxAttendees"")
                VALUES (@Title, @Description, @Date, @EndDate, @Location, @IsOnline, @MaxAttendees)
                RETURNING ""Id""", entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = _factory.CreateConnection();
            var rows = await conn.ExecuteAsync(
                @"DELETE FROM ""Events"" WHERE ""Id"" = @id", new { id });
            return rows > 0;
        }
    }
}
