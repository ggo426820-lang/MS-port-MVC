using Dapper;
using MostafaSaidPortfolio.Data.Repositories.Interfaces;
using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Domain.Enums;
using Npgsql;

namespace MostafaSaidPortfolio.Data.Repositories.Implementations
{
    public class BlogRepository : BaseRepository<BlogPost>, IBlogRepository
    {
        protected override string TableName => "BlogPosts";

        protected override string Columns => @"
            b.""Id"", b.""Title"", b.""Content"", b.""Summary"", b.""Slug"",
            b.""MetaTitle"", b.""MetaDescription"", b.""FeaturedImageUrl"",
            b.""CategoryId"", b.""AuthorId"", b.""Status"", b.""PublishedAt"",
            b.""ViewCount"", b.""ReadingTime"", b.""IsFeatured"",
            b.""CreatedAt"", b.""UpdatedAt"", b.""IsDeleted"",
            c.""Name"" AS ""CategoryName"",
            u.""FullName"" AS ""AuthorName""";

        private string Joins => @"
            FROM ""BlogPosts"" b
            LEFT JOIN ""Categories"" c ON b.""CategoryId"" = c.""Id""
            LEFT JOIN ""AspNetUsers"" u ON b.""AuthorId"" = u.""Id""";

        public BlogRepository(NpgsqlConnection connection) : base(connection) { }

        public override async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await _connection.QueryFirstOrDefaultAsync<BlogPost>(
                $"SELECT {Columns} {Joins} WHERE b.\"Id\" = @id AND b.\"IsDeleted\" = FALSE",
                new { id }, _transaction);
        }

        public override async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _connection.QueryAsync<BlogPost>(
                $"SELECT {Columns} {Joins} WHERE b.\"IsDeleted\" = FALSE ORDER BY b.\"CreatedAt\" DESC",
                transaction: _transaction);
        }

        public override async Task<int> CountAsync()
        {
            return await _connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM ""BlogPosts"" WHERE ""IsDeleted"" = FALSE",
                transaction: _transaction);
        }

        public async Task<IEnumerable<BlogPost>> GetPublishedAsync()
        {
            return await _connection.QueryAsync<BlogPost>(
                $"SELECT {Columns} {Joins} WHERE b.\"Status\" = 1 AND b.\"IsDeleted\" = FALSE ORDER BY b.\"CreatedAt\" DESC",
                transaction: _transaction);
        }

        public async Task<IEnumerable<BlogPost>> GetFeaturedAsync(int count = 3)
        {
            return await _connection.QueryAsync<BlogPost>(
                $"SELECT {Columns} {Joins} WHERE b.\"IsFeatured\" = TRUE AND b.\"Status\" = 1 AND b.\"IsDeleted\" = FALSE ORDER BY b.\"CreatedAt\" DESC LIMIT @count",
                new { count }, _transaction);
        }

        public async Task<IEnumerable<BlogPost>> GetRecentAsync(int count = 5)
        {
            return await _connection.QueryAsync<BlogPost>(
                $"SELECT {Columns} {Joins} WHERE b.\"Status\" = 1 AND b.\"IsDeleted\" = FALSE ORDER BY b.\"CreatedAt\" DESC LIMIT @count",
                new { count }, _transaction);
        }

        public async Task<BlogPost?> GetBySlugAsync(string slug)
        {
            return await _connection.QueryFirstOrDefaultAsync<BlogPost>(
                $"SELECT {Columns} {Joins} WHERE b.\"Slug\" = @slug AND b.\"IsDeleted\" = FALSE",
                new { slug }, _transaction);
        }

        public async Task<IEnumerable<BlogPost>> GetByCategoryAsync(int categoryId)
        {
            return await _connection.QueryAsync<BlogPost>(
                $"SELECT {Columns} {Joins} WHERE b.\"CategoryId\" = @categoryId AND b.\"Status\" = 1 AND b.\"IsDeleted\" = FALSE ORDER BY b.\"CreatedAt\" DESC",
                new { categoryId }, _transaction);
        }

        public async Task<IEnumerable<BlogPost>> SearchAsync(string query, int limit = 50)
        {
            return await _connection.QueryAsync<BlogPost>(
                $"SELECT {Columns} {Joins} WHERE b.\"Status\" = 1 AND b.\"IsDeleted\" = FALSE AND (b.\"Title\" ILIKE @q OR b.\"Summary\" ILIKE @q OR b.\"Content\" ILIKE @q) ORDER BY b.\"CreatedAt\" DESC LIMIT @limit",
                new { q = $"%{query}%", limit }, _transaction);
        }

        public async Task<bool> IncrementViewCountAsync(Guid id)
        {
            var rows = await _connection.ExecuteAsync(
                @"UPDATE ""BlogPosts"" SET ""ViewCount"" = ""ViewCount"" + 1 WHERE ""Id"" = @id",
                new { id }, _transaction);
            return rows > 0;
        }

        public async Task<int> CountPublishedAsync()
        {
            return await _connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM ""BlogPosts"" WHERE ""Status"" = 1 AND ""IsDeleted"" = FALSE",
                transaction: _transaction);
        }

        public override async Task AddAsync(BlogPost entity)
        {
            await _connection.ExecuteAsync(@"
                INSERT INTO ""BlogPosts""
                    (""Id"", ""Title"", ""Content"", ""Summary"", ""Slug"", ""MetaTitle"", ""MetaDescription"",
                     ""FeaturedImageUrl"", ""CategoryId"", ""AuthorId"", ""Status"", ""ReadingTime"",
                     ""IsFeatured"", ""PublishedAt"", ""CreatedAt"", ""UpdatedAt"")
                VALUES
                    (@Id, @Title, @Content, @Summary, @Slug, @MetaTitle, @MetaDescription,
                     @FeaturedImageUrl, @CategoryId, @AuthorId, @Status, @ReadingTime,
                     @IsFeatured, @PublishedAt, NOW(), NOW())",
                entity, _transaction);
        }

        public override async Task<bool> UpdateAsync(BlogPost entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            var rows = await _connection.ExecuteAsync(@"
                UPDATE ""BlogPosts"" SET
                    ""Title"" = @Title, ""Content"" = @Content,
                    ""Summary"" = @Summary, ""Slug"" = @Slug, ""MetaTitle"" = @MetaTitle,
                    ""MetaDescription"" = @MetaDescription, ""FeaturedImageUrl"" = @FeaturedImageUrl,
                    ""CategoryId"" = @CategoryId, ""Status"" = @Status, ""ReadingTime"" = @ReadingTime,
                    ""IsFeatured"" = @IsFeatured, ""UpdatedAt"" = @UpdatedAt
                WHERE ""Id"" = @Id", entity, _transaction);
            return rows > 0;
        }

        public override async Task<bool> DeleteAsync(Guid id)
        {
            var rows = await _connection.ExecuteAsync(
                @"UPDATE ""BlogPosts"" SET ""IsDeleted"" = TRUE WHERE ""Id"" = @id",
                new { id }, _transaction);
            return rows > 0;
        }
    }
}
