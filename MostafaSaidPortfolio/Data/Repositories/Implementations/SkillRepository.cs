using Dapper;
using MostafaSaidPortfolio.Data.Repositories.Interfaces;
using MostafaSaidPortfolio.Domain.Entities;
using Npgsql;

namespace MostafaSaidPortfolio.Data.Repositories.Implementations
{
    public class SkillRepository : BaseRepository<Skill>, ISkillRepository
    {
        protected override string TableName => "Skills";
        protected override string Columns => @"
            ""Id"", ""Name"", ""Category"", ""Proficiency"", ""IconName"",
            ""Description"", ""DisplayOrder"", ""IsActive"", ""IsDeleted"",
            ""CreatedAt"", ""UpdatedAt""";

        public SkillRepository(NpgsqlConnection connection) : base(connection) { }

        public override async Task<IEnumerable<Skill>> GetAllAsync()
        {
            return await _connection.QueryAsync<Skill>(
                $@"SELECT {Columns} FROM ""Skills"" WHERE ""IsDeleted"" = FALSE ORDER BY ""DisplayOrder"", ""Name""",
                transaction: _transaction);
        }

        public async Task<IEnumerable<Skill>> GetActiveAsync()
        {
            return await _connection.QueryAsync<Skill>(
                $@"SELECT {Columns} FROM ""Skills"" WHERE ""IsActive"" = TRUE AND ""IsDeleted"" = FALSE ORDER BY ""DisplayOrder"", ""Name""",
                transaction: _transaction);
        }

        public async Task<IEnumerable<Skill>> GetByCategoryAsync(string category)
        {
            return await _connection.QueryAsync<Skill>(
                $@"SELECT {Columns} FROM ""Skills"" WHERE ""Category"" = @category AND ""IsActive"" = TRUE AND ""IsDeleted"" = FALSE ORDER BY ""DisplayOrder"", ""Name""",
                new { category }, _transaction);
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
            return await _connection.QueryAsync<string>(
                @"SELECT DISTINCT ""Category"" FROM ""Skills"" WHERE ""IsDeleted"" = FALSE ORDER BY ""Category""",
                transaction: _transaction);
        }

        public override async Task AddAsync(Skill entity)
        {
            entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            await _connection.ExecuteAsync(@"
                INSERT INTO ""Skills"" (""Id"", ""Name"", ""Category"", ""Proficiency"", ""IconName"",
                    ""Description"", ""DisplayOrder"", ""IsActive"", ""IsDeleted"", ""CreatedAt"", ""UpdatedAt"")
                VALUES (@Id, @Name, @Category, @Proficiency, @IconName,
                    @Description, @DisplayOrder, @IsActive, FALSE, @CreatedAt, @UpdatedAt)",
                entity, _transaction);
        }

        public override async Task<bool> UpdateAsync(Skill entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            var rows = await _connection.ExecuteAsync(@"
                UPDATE ""Skills"" SET
                    ""Name"" = @Name, ""Category"" = @Category, ""Proficiency"" = @Proficiency,
                    ""IconName"" = @IconName, ""Description"" = @Description,
                    ""DisplayOrder"" = @DisplayOrder, ""IsActive"" = @IsActive,
                    ""UpdatedAt"" = @UpdatedAt
                WHERE ""Id"" = @Id AND ""IsDeleted"" = FALSE",
                entity, _transaction);
            return rows > 0;
        }

        public override async Task<bool> DeleteAsync(Guid id)
        {
            var rows = await _connection.ExecuteAsync(
                @"UPDATE ""Skills"" SET ""IsDeleted"" = TRUE, ""UpdatedAt"" = NOW() WHERE ""Id"" = @id",
                new { id }, _transaction);
            return rows > 0;
        }

        public override async Task<int> CountAsync()
        {
            return await _connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM ""Skills"" WHERE ""IsDeleted"" = FALSE",
                transaction: _transaction);
        }
    }
}
