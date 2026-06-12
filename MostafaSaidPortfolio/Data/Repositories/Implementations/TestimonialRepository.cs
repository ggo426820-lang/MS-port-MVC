using Dapper;
using MostafaSaidPortfolio.Data.Repositories.Interfaces;
using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Domain.Enums;
using Npgsql;

namespace MostafaSaidPortfolio.Data.Repositories.Implementations
{
    public class TestimonialRepository : BaseRepository<Testimonial>, ITestimonialRepository
    {
        protected override string TableName => "Testimonials";
        protected override string Columns => @"""Id"", ""Author"", ""Content"", ""Company"", ""Rating"", ""ImageUrl"", ""IsApproved"", ""CreatedAt""";

        public TestimonialRepository(NpgsqlConnection connection) : base(connection) { }

        public async Task<IEnumerable<Testimonial>> GetApprovedAsync()
        {
            return await _connection.QueryAsync<Testimonial>(
                $@"SELECT {Columns} FROM ""Testimonials"" WHERE ""IsApproved"" = TRUE ORDER BY ""CreatedAt"" DESC",
                transaction: _transaction);
        }

        public async Task<bool> ApproveAsync(int id)
        {
            var rows = await _connection.ExecuteAsync(
                @"UPDATE ""Testimonials"" SET ""IsApproved"" = TRUE WHERE ""Id"" = @id",
                new { id }, _transaction);
            return rows > 0;
        }

        public async Task<bool> RejectAsync(int id)
        {
            var rows = await _connection.ExecuteAsync(
                @"UPDATE ""Testimonials"" SET ""IsApproved"" = FALSE WHERE ""Id"" = @id",
                new { id }, _transaction);
            return rows > 0;
        }

        public override async Task<int> AddAsync(Testimonial entity)
        {
            return await _connection.ExecuteScalarAsync<int>(@"
                INSERT INTO ""Testimonials"" (""Author"", ""Content"", ""Company"", ""Rating"", ""ImageUrl"", ""IsApproved"")
                VALUES (@Author, @Content, @Company, @Rating, @ImageUrl, @IsApproved)
                RETURNING ""Id""", entity, _transaction);
        }

        public override async Task<bool> UpdateAsync(Testimonial entity)
        {
            var rows = await _connection.ExecuteAsync(@"
                UPDATE ""Testimonials"" SET
                    ""Author"" = @Author, ""Content"" = @Content, ""Company"" = @Company,
                    ""Rating"" = @Rating, ""ImageUrl"" = @ImageUrl, ""IsApproved"" = @IsApproved
                WHERE ""Id"" = @Id", entity, _transaction);
            return rows > 0;
        }
    }
}

