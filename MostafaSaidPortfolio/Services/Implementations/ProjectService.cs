using Dapper;
using MostafaSaidPortfolio.Data;
using MostafaSaidPortfolio.Models;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DbConnectionFactory _factory;

        public ProjectService(DbConnectionFactory factory) => _factory = factory;

        private const string Cols = @"
            p.""Id"", p.""Title"", p.""Description"", p.""LongDescription"", p.""TechnologyStack"",
            p.""GitHubUrl"", p.""LiveUrl"", p.""ImageUrl"", p.""ThumbnailUrl"", p.""CategoryId"",
            p.""Status"", p.""DisplayOrder"", p.""IsFeatured"", p.""ViewCount"", p.""LikeCount"",
            p.""CreatedAt"", p.""UpdatedAt"", p.""CreatedBy"", p.""UpdatedBy"", p.""IsDeleted"",
            c.""Name"" AS ""CategoryName""";

        private const string Joins = @"
            FROM ""Projects"" p
            LEFT JOIN ""Categories"" c ON p.""CategoryId"" = c.""Id""";

        public async Task<IEnumerable<Project>> GetAllActiveAsync()
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryAsync<Project>(
                $@"SELECT {Cols} {Joins}
                   WHERE p.""IsDeleted"" = FALSE
                   ORDER BY p.""DisplayOrder"", p.""CreatedAt"" DESC");
        }

        public async Task<IEnumerable<Project>> GetFeaturedAsync(int count = 3)
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryAsync<Project>(
                $@"SELECT {Cols} {Joins}
                   WHERE p.""IsDeleted"" = FALSE
                   ORDER BY p.""IsFeatured"" DESC, p.""DisplayOrder"", p.""CreatedAt"" DESC
                   LIMIT @count", new { count });
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<Project>(
                $@"SELECT {Cols} {Joins}
                   WHERE p.""Id"" = @id AND p.""IsDeleted"" = FALSE", new { id });
        }

        public async Task<IEnumerable<Project>> GetByCategoryAsync(int categoryId)
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryAsync<Project>(
                $@"SELECT {Cols} {Joins}
                   WHERE p.""CategoryId"" = @categoryId AND p.""IsDeleted"" = FALSE
                   ORDER BY p.""DisplayOrder""", new { categoryId });
        }

        public async Task<IEnumerable<Project>> SearchAsync(string query)
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryAsync<Project>(
                $@"SELECT {Cols} {Joins}
                   WHERE p.""IsDeleted"" = FALSE
                     AND (p.""Title"" ILIKE @q OR p.""Description"" ILIKE @q OR p.""TechnologyStack"" ILIKE @q)
                   ORDER BY p.""DisplayOrder""", new { q = $"%{query}%" });
        }

        public async Task<Project> AddAsync(Project entity)
        {
            using var conn = _factory.CreateConnection();
            entity.Id = await conn.ExecuteScalarAsync<int>(@"
                INSERT INTO ""Projects""
                    (""Title"", ""Description"", ""LongDescription"", ""TechnologyStack"",
                     ""GitHubUrl"", ""LiveUrl"", ""ImageUrl"", ""ThumbnailUrl"",
                     ""CategoryId"", ""Status"", ""DisplayOrder"", ""IsFeatured"", ""CreatedAt"", ""UpdatedAt"")
                VALUES
                    (@Title, @Description, @LongDescription, @TechnologyStack,
                     @GitHubUrl, @LiveUrl, @ImageUrl, @ThumbnailUrl,
                     @CategoryId, @Status, @DisplayOrder, @IsFeatured, NOW(), NOW())
                RETURNING ""Id""", entity);
            return entity;
        }

        public async Task<Project> UpdateAsync(Project entity)
        {
            using var conn = _factory.CreateConnection();
            entity.UpdatedAt = DateTime.UtcNow;
            await conn.ExecuteAsync(@"
                UPDATE ""Projects""
                SET ""Title"" = @Title, ""Description"" = @Description, ""LongDescription"" = @LongDescription,
                    ""TechnologyStack"" = @TechnologyStack, ""GitHubUrl"" = @GitHubUrl, ""LiveUrl"" = @LiveUrl,
                    ""ImageUrl"" = @ImageUrl, ""ThumbnailUrl"" = @ThumbnailUrl, ""CategoryId"" = @CategoryId,
                    ""IsFeatured"" = @IsFeatured, ""UpdatedAt"" = @UpdatedAt
                WHERE ""Id"" = @Id", entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = _factory.CreateConnection();
            var rows = await conn.ExecuteAsync(
                @"UPDATE ""Projects"" SET ""IsDeleted"" = TRUE WHERE ""Id"" = @id", new { id });
            return rows > 0;
        }
    }
}
