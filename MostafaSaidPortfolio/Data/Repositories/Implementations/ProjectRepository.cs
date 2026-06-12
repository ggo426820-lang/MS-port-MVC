using Dapper;
using MostafaSaidPortfolio.Data.Repositories.Interfaces;
using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Domain.Enums;
using Npgsql;

namespace MostafaSaidPortfolio.Data.Repositories.Implementations
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        protected override string TableName => "Projects";

        protected override string Columns => @"
            p.""Id"", p.""Title"", p.""Description"", p.""LongDescription"", p.""TechnologyStack"",
            p.""GitHubUrl"", p.""LiveUrl"", p.""ImageUrl"", p.""ThumbnailUrl"", p.""CategoryId"",
            p.""Status"", p.""DisplayOrder"", p.""IsFeatured"", p.""ViewCount"", p.""LikeCount"",
            p.""CreatedAt"", p.""UpdatedAt"", p.""CreatedBy"", p.""UpdatedBy"", p.""IsDeleted"",
            c.""Name"" AS ""CategoryName""";

        private string Joins => @"
            FROM ""Projects"" p
            LEFT JOIN ""Categories"" c ON p.""CategoryId"" = c.""Id""";

        public ProjectRepository(NpgsqlConnection connection) : base(connection) { }

        public override async Task<Project?> GetByIdAsync(Guid id)
        {
            return await _connection.QueryFirstOrDefaultAsync<Project>(
                $"SELECT {Columns} {Joins} WHERE p.\"Id\" = @id AND p.\"IsDeleted\" = FALSE",
                new { id }, _transaction);
        }

        public override async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _connection.QueryAsync<Project>(
                $"SELECT {Columns} {Joins} WHERE p.\"IsDeleted\" = FALSE ORDER BY p.\"DisplayOrder\", p.\"CreatedAt\" DESC",
                transaction: _transaction);
        }

        public override async Task<int> CountAsync()
        {
            return await _connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM ""Projects"" WHERE ""IsDeleted"" = FALSE",
                transaction: _transaction);
        }

        public async Task<IEnumerable<Project>> GetActiveAsync()
        {
            return await _connection.QueryAsync<Project>(
                $"SELECT {Columns} {Joins} WHERE p.\"IsDeleted\" = FALSE ORDER BY p.\"DisplayOrder\", p.\"CreatedAt\" DESC",
                transaction: _transaction);
        }

        public async Task<IEnumerable<Project>> GetFeaturedAsync(int count = 3)
        {
            return await _connection.QueryAsync<Project>(
                $"SELECT {Columns} {Joins} WHERE p.\"IsDeleted\" = FALSE ORDER BY p.\"IsFeatured\" DESC, p.\"DisplayOrder\", p.\"CreatedAt\" DESC LIMIT @count",
                new { count }, _transaction);
        }

        public async Task<IEnumerable<Project>> GetByCategoryAsync(int categoryId)
        {
            return await _connection.QueryAsync<Project>(
                $"SELECT {Columns} {Joins} WHERE p.\"CategoryId\" = @categoryId AND p.\"IsDeleted\" = FALSE ORDER BY p.\"DisplayOrder\", p.\"CreatedAt\" DESC",
                new { categoryId }, _transaction);
        }

        public async Task<IEnumerable<Project>> SearchAsync(string query, int limit = 50)
        {
            return await _connection.QueryAsync<Project>(
                $"SELECT {Columns} {Joins} WHERE p.\"IsDeleted\" = FALSE AND (p.\"Title\" ILIKE @q OR p.\"Description\" ILIKE @q OR p.\"TechnologyStack\" ILIKE @q) ORDER BY p.\"DisplayOrder\", p.\"CreatedAt\" DESC LIMIT @limit",
                new { q = $"%{query}%", limit }, _transaction);
        }

        public async Task<int> CountActiveAsync()
        {
            return await _connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM ""Projects"" WHERE ""IsDeleted"" = FALSE",
                transaction: _transaction);
        }

        public override async Task AddAsync(Project entity)
        {
            await _connection.ExecuteAsync(@"
                INSERT INTO ""Projects""
                    (""Id"", ""Title"", ""Description"", ""LongDescription"", ""Slug"", ""TechnologyStack"",
                     ""GitHubUrl"", ""LiveUrl"", ""ImageUrl"", ""ThumbnailUrl"",
                     ""CategoryId"", ""Status"", ""DisplayOrder"", ""IsFeatured"", ""CreatedAt"", ""UpdatedAt"")
                VALUES
                    (@Id, @Title, @Description, @LongDescription, @Slug, @TechnologyStack,
                     @GitHubUrl, @LiveUrl, @ImageUrl, @ThumbnailUrl,
                     @CategoryId, @Status, @DisplayOrder, @IsFeatured, NOW(), NOW())",
                entity, _transaction);
        }

        public override async Task<bool> UpdateAsync(Project entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            var rows = await _connection.ExecuteAsync(@"
                UPDATE ""Projects"" SET
                    ""Title"" = @Title, ""Description"" = @Description, ""LongDescription"" = @LongDescription,
                    ""TechnologyStack"" = @TechnologyStack, ""GitHubUrl"" = @GitHubUrl, ""LiveUrl"" = @LiveUrl,
                    ""ImageUrl"" = @ImageUrl, ""ThumbnailUrl"" = @ThumbnailUrl, ""CategoryId"" = @CategoryId,
                    ""Status"" = @Status, ""DisplayOrder"" = @DisplayOrder, ""IsFeatured"" = @IsFeatured,
                    ""UpdatedAt"" = @UpdatedAt
                WHERE ""Id"" = @Id", entity, _transaction);
            return rows > 0;
        }

        public override async Task<bool> DeleteAsync(Guid id)
        {
            var rows = await _connection.ExecuteAsync(
                @"UPDATE ""Projects"" SET ""IsDeleted"" = TRUE WHERE ""Id"" = @id",
                new { id }, _transaction);
            return rows > 0;
        }
    }
}
