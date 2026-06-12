using Dapper;
using MostafaSaidPortfolio.Data.Repositories.Interfaces;
using MostafaSaidPortfolio.Domain.Entities;
using Npgsql;

namespace MostafaSaidPortfolio.Data.Repositories.Implementations
{
    public class TestimonialRepository : BaseRepository<Testimonial>, ITestimonialRepository
    {
        protected override string TableName => "Testimonials";
        protected override string Columns => @"""Id"", ""AuthorName"", ""Position"", ""Content"", ""Company"", ""Rating"", ""ImageUrl"", ""IsFeatured"", ""DisplayOrder"", ""IsApproved"", ""CreatedAt"", ""UpdatedAt""";

        public TestimonialRepository(NpgsqlConnection connection) : base(connection) { }

        public async Task<IEnumerable<Testimonial>> GetApprovedAsync()
        {
            return await _connection.QueryAsync<Testimonial>(
                $@"SELECT {Columns} FROM ""Testimonials"" WHERE ""IsApproved"" = TRUE ORDER BY ""CreatedAt"" DESC",
                transaction: _transaction);
        }

        public async Task<bool> ApproveAsync(Guid id)
        {
            var rows = await _connection.ExecuteAsync(
                @"UPDATE ""Testimonials"" SET ""IsApproved"" = TRUE WHERE ""Id"" = @id",
                new { id }, _transaction);
            return rows > 0;
        }

        public async Task<bool> RejectAsync(Guid id)
        {
            var rows = await _connection.ExecuteAsync(
                @"UPDATE ""Testimonials"" SET ""IsApproved"" = FALSE WHERE ""Id"" = @id",
                new { id }, _transaction);
            return rows > 0;
        }

        public override async Task AddAsync(Testimonial entity)
        {
            await _connection.ExecuteAsync(@"
                INSERT INTO ""Testimonials"" (""Id"", ""AuthorName"", ""Position"", ""Content"", ""Company"", ""Rating"", ""ImageUrl"", ""IsFeatured"", ""DisplayOrder"", ""IsApproved"")
                VALUES (@Id, @AuthorName, @Position, @Content, @Company, @Rating, @ImageUrl, @IsFeatured, @DisplayOrder, @IsApproved)",
                entity, _transaction);
        }

        public override async Task<bool> UpdateAsync(Testimonial entity)
        {
            var rows = await _connection.ExecuteAsync(@"
                UPDATE ""Testimonials"" SET
                    ""AuthorName"" = @AuthorName, ""Position"" = @Position, ""Content"" = @Content, ""Company"" = @Company,
                    ""Rating"" = @Rating, ""ImageUrl"" = @ImageUrl, ""IsApproved"" = @IsApproved
                WHERE ""Id"" = @Id", entity, _transaction);
            return rows > 0;
        }
    }
}
