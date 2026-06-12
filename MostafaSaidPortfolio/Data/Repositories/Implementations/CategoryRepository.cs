using Dapper;
using MostafaSaidPortfolio.Data.Repositories.Interfaces;
using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Domain.Enums;
using Npgsql;

namespace MostafaSaidPortfolio.Data.Repositories.Implementations
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        protected override string TableName => "Categories";
        protected override string Columns => @"""Id"", ""Name"", ""Description"", ""Slug"", ""Icon"", ""Color"", ""BackgroundColor"", ""DisplayOrder"", ""IsActive"", ""ParentId"", ""CreatedAt""";

        public CategoryRepository(NpgsqlConnection connection) : base(connection) { }

        public async Task<IEnumerable<Category>> GetActiveAsync()
        {
            return await _connection.QueryAsync<Category>(
                $@"SELECT {Columns} FROM ""Categories"" WHERE ""IsActive"" = TRUE ORDER BY ""DisplayOrder"", ""Name""",
                transaction: _transaction);
        }

        public async Task<Category?> GetBySlugAsync(string slug)
        {
            return await _connection.QueryFirstOrDefaultAsync<Category>(
                $@"SELECT {Columns} FROM ""Categories"" WHERE ""Slug"" = @slug",
                new { slug }, _transaction);
        }

        public override async Task<int> AddAsync(Category entity)
        {
            return await _connection.ExecuteScalarAsync<int>(@"
                INSERT INTO ""Categories"" (""Name"", ""Description"", ""Slug"", ""Icon"", ""Color"", ""BackgroundColor"", ""DisplayOrder"", ""IsActive"", ""ParentId"")
                VALUES (@Name, @Description, @Slug, @Icon, @Color, @BackgroundColor, @DisplayOrder, @IsActive, @ParentId)
                RETURNING ""Id""", entity, _transaction);
        }

        public override async Task<bool> UpdateAsync(Category entity)
        {
            var rows = await _connection.ExecuteAsync(@"
                UPDATE ""Categories"" SET
                    ""Name"" = @Name, ""Description"" = @Description, ""Slug"" = @Slug,
                    ""Icon"" = @Icon, ""Color"" = @Color, ""BackgroundColor"" = @BackgroundColor,
                    ""DisplayOrder"" = @DisplayOrder, ""IsActive"" = @IsActive, ""ParentId"" = @ParentId
                WHERE ""Id"" = @Id", entity, _transaction);
            return rows > 0;
        }
    }
}

