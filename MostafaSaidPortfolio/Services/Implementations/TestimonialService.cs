using Dapper;
using MostafaSaidPortfolio.Data;
using MostafaSaidPortfolio.Models;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class TestimonialService : ITestimonialService
    {
        private readonly DbConnectionFactory _factory;

        public TestimonialService(DbConnectionFactory factory) => _factory = factory;

        public async Task<IEnumerable<Testimonial>> GetApprovedAsync()
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryAsync<Testimonial>(@"
                SELECT * FROM ""Testimonials""
                WHERE ""IsApproved"" = TRUE
                ORDER BY ""CreatedAt"" DESC");
        }

        public async Task<IEnumerable<Testimonial>> GetAllAsync()
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryAsync<Testimonial>(@"
                SELECT * FROM ""Testimonials"" ORDER BY ""CreatedAt"" DESC");
        }

        public async Task<Testimonial?> GetByIdAsync(int id)
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<Testimonial>(
                @"SELECT * FROM ""Testimonials"" WHERE ""Id"" = @id", new { id });
        }

        public async Task<Testimonial> AddAsync(Testimonial entity)
        {
            using var conn = _factory.CreateConnection();
            entity.Id = await conn.ExecuteScalarAsync<int>(@"
                INSERT INTO ""Testimonials"" (""Author"", ""Content"", ""Company"", ""Rating"", ""ImageUrl"", ""IsApproved"")
                VALUES (@Author, @Content, @Company, @Rating, @ImageUrl, FALSE)
                RETURNING ""Id""", entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = _factory.CreateConnection();
            var rows = await conn.ExecuteAsync(
                @"DELETE FROM ""Testimonials"" WHERE ""Id"" = @id", new { id });
            return rows > 0;
        }
    }
}
