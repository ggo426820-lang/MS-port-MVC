using Dapper;
using MostafaSaidPortfolio.Data;
using MostafaSaidPortfolio.Models;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly DbConnectionFactory _factory;

        public BlogService(DbConnectionFactory factory) => _factory = factory;

        private const string Cols = @"
            b.""Id"", b.""Name"", b.""Title"", b.""Content"", b.""Summary"", b.""Slug"",
            b.""MetaTitle"", b.""MetaDescription"", b.""FeaturedImageUrl"",
            b.""CategoryId"", b.""AuthorId"", b.""Status"", b.""PublishedAt"",
            b.""ViewCount"", b.""CommentCount"", b.""ReadingTime"", b.""IsFeatured"",
            b.""IsPublished"", b.""CreatedAt"", b.""UpdatedAt"", b.""IsDeleted"",
            c.""Name"" AS ""CategoryName"",
            u.""Name"" AS ""AuthorName""";

        private const string Joins = @"
            FROM ""BlogPosts"" b
            LEFT JOIN ""Categories"" c ON b.""CategoryId"" = c.""Id""
            LEFT JOIN ""Users"" u ON b.""AuthorId"" = u.""Id""";

        public async Task<IEnumerable<BlogPost>> GetAllPublishedAsync()
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryAsync<BlogPost>(
                $@"SELECT {Cols} {Joins}
                   WHERE b.""IsPublished"" = TRUE AND b.""IsDeleted"" = FALSE
                   ORDER BY b.""CreatedAt"" DESC");
        }

        public async Task<IEnumerable<BlogPost>> GetFeaturedAsync(int count = 3)
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryAsync<BlogPost>(
                $@"SELECT {Cols} {Joins}
                   WHERE b.""IsPublished"" = TRUE AND b.""IsFeatured"" = TRUE AND b.""IsDeleted"" = FALSE
                   ORDER BY b.""CreatedAt"" DESC LIMIT @count", new { count });
        }

        public async Task<IEnumerable<BlogPost>> GetRecentAsync(int count = 5)
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryAsync<BlogPost>(
                $@"SELECT {Cols} {Joins}
                   WHERE b.""IsPublished"" = TRUE AND b.""IsDeleted"" = FALSE
                   ORDER BY b.""CreatedAt"" DESC LIMIT @count", new { count });
        }

        public async Task<BlogPost?> GetByIdAsync(int id)
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<BlogPost>(
                $@"SELECT {Cols} {Joins}
                   WHERE b.""Id"" = @id AND b.""IsDeleted"" = FALSE", new { id });
        }

        public async Task<BlogPost?> GetBySlugAsync(string slug)
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<BlogPost>(
                $@"SELECT {Cols} {Joins}
                   WHERE b.""Slug"" = @slug AND b.""IsDeleted"" = FALSE", new { slug });
        }

        public async Task<IEnumerable<BlogPost>> GetByCategoryAsync(int categoryId)
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryAsync<BlogPost>(
                $@"SELECT {Cols} {Joins}
                   WHERE b.""CategoryId"" = @categoryId AND b.""IsPublished"" = TRUE AND b.""IsDeleted"" = FALSE
                   ORDER BY b.""CreatedAt"" DESC", new { categoryId });
        }

        public async Task<IEnumerable<BlogPost>> SearchAsync(string query)
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryAsync<BlogPost>(
                $@"SELECT {Cols} {Joins}
                   WHERE b.""IsPublished"" = TRUE AND b.""IsDeleted"" = FALSE
                     AND (b.""Title"" ILIKE @q OR b.""Summary"" ILIKE @q OR b.""Content"" ILIKE @q)
                   ORDER BY b.""CreatedAt"" DESC", new { q = $"%{query}%" });
        }

        public async Task IncrementViewCountAsync(int id)
        {
            using var conn = _factory.CreateConnection();
            await conn.ExecuteAsync(
                @"UPDATE ""BlogPosts"" SET ""ViewCount"" = ""ViewCount"" + 1 WHERE ""Id"" = @id", new { id });
        }

        public async Task<BlogPost> AddAsync(BlogPost entity)
        {
            using var conn = _factory.CreateConnection();
            entity.Id = await conn.ExecuteScalarAsync<int>(@"
                INSERT INTO ""BlogPosts""
                    (""Name"", ""Title"", ""Content"", ""Summary"", ""Slug"", ""MetaTitle"", ""MetaDescription"",
                     ""FeaturedImageUrl"", ""CategoryId"", ""AuthorId"", ""Status"", ""ReadingTime"",
                     ""IsFeatured"", ""IsPublished"", ""CreatedAt"", ""UpdatedAt"")
                VALUES
                    (@Name, @Title, @Content, @Summary, @Slug, @MetaTitle, @MetaDescription,
                     @FeaturedImageUrl, @CategoryId, @AuthorId, @Status, @ReadingTime,
                     @IsFeatured, @IsPublished, NOW(), NOW())
                RETURNING ""Id""", entity);
            return entity;
        }

        public async Task<BlogPost> UpdateAsync(BlogPost entity)
        {
            using var conn = _factory.CreateConnection();
            entity.UpdatedAt = DateTime.UtcNow;
            await conn.ExecuteAsync(@"
                UPDATE ""BlogPosts""
                SET ""Title"" = @Title, ""Content"" = @Content, ""Summary"" = @Summary,
                    ""Slug"" = @Slug, ""IsPublished"" = @IsPublished, ""IsFeatured"" = @IsFeatured,
                    ""UpdatedAt"" = @UpdatedAt
                WHERE ""Id"" = @Id", entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = _factory.CreateConnection();
            var rows = await conn.ExecuteAsync(
                @"UPDATE ""BlogPosts"" SET ""IsDeleted"" = TRUE WHERE ""Id"" = @id", new { id });
            return rows > 0;
        }
    }
}
