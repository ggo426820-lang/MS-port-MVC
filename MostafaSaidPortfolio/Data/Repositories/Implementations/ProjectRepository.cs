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
            p.""Id"", p.""Title"", p.""Description"", p.""LongDescription"", p.""Slug"", p.""TechnologyStack"",
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

        public async Task<Project?> GetBySlugAsync(string slug)
        {
            return await _connection.QueryFirstOrDefaultAsync<Project>(
                $"SELECT {Columns} {Joins} WHERE p.\"Slug\" = @slug AND p.\"IsDeleted\" = FALSE",
                new { slug }, _transaction);
        }

        public async Task<IEnumerable<Project>> GetByCategoryAsync(Guid categoryId)
        {
            return await _connection.QueryAsync<Project>(
                $"SELECT {Columns} {Joins} WHERE p.\"CategoryId\" = @categoryId AND p.\"IsDeleted\" = FALSE ORDER BY p.\"DisplayOrder\", p.\"CreatedAt\" DESC",
                new { categoryId }, _transaction);
        }

        public async Task<IEnumerable<Project>> SearchAsync(string query)
        {
            return await _connection.QueryAsync<Project>(
                $"SELECT {Columns} {Joins} WHERE p.\"IsDeleted\" = FALSE AND (p.\"Title\" ILIKE @q OR p.\"Description\" ILIKE @q OR p.\"TechnologyStack\" ILIKE @q) ORDER BY p.\"DisplayOrder\", p.\"CreatedAt\" DESC",
                new { q = $"%{query}%" }, _transaction);
        }

        public async Task<(IEnumerable<Project> Items, int TotalCount)> GetPagedAsync(
            string? search, Guid? categoryId, string sort, int page, int pageSize)
        {
            var conditions = new List<string> { "p.\"IsDeleted\" = FALSE" };

            if (!string.IsNullOrWhiteSpace(search))
                conditions.Add("(p.\"Title\" ILIKE @search OR p.\"Description\" ILIKE @search OR p.\"TechnologyStack\" ILIKE @search)");

            if (categoryId.HasValue)
                conditions.Add("p.\"CategoryId\" = @categoryId");

            var where = "WHERE " + string.Join(" AND ", conditions);

            var sortClause = sort switch
            {
                "oldest"  => "p.\"CreatedAt\" ASC",
                "popular" => "p.\"ViewCount\" DESC, p.\"DisplayOrder\" ASC",
                "title"   => "p.\"Title\" ASC",
                _         => "p.\"IsFeatured\" DESC, p.\"DisplayOrder\" ASC, p.\"CreatedAt\" DESC"
            };

            var searchParam = string.IsNullOrWhiteSpace(search) ? null : $"%{search}%";
            var parameters = new
            {
                search   = searchParam,
                categoryId,
                pageSize,
                offset   = (page - 1) * pageSize
            };

            var totalCount = await _connection.ExecuteScalarAsync<int>(
                $"SELECT COUNT(*) FROM \"Projects\" p {where}",
                parameters, _transaction);

            var items = await _connection.QueryAsync<Project>(
                $"SELECT {Columns} {Joins} {where} ORDER BY {sortClause} LIMIT @pageSize OFFSET @offset",
                parameters, _transaction);

            return (items, totalCount);
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
