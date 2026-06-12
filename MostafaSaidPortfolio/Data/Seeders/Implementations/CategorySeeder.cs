using Dapper;
using Npgsql;
using MostafaSaidPortfolio.Data.Seeders.Interfaces;

namespace MostafaSaidPortfolio.Data.Seeders.Implementations;

/// <summary>
/// Seeder for Category entities
/// </summary>
public class CategorySeeder : ISeeder
{
    public async Task SeedAsync(NpgsqlConnection connection)
    {
        var existingCount = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM \"Categories\"");
        
        if (existingCount > 0)
            return;

        await connection.ExecuteAsync(@"
            INSERT INTO ""Categories"" 
            (""Id"", ""Name"", ""Description"", ""Slug"", ""Icon"", ""Color"", ""BackgroundColor"", ""DisplayOrder"", ""IsActive"", ""CreatedAt"", ""CreatedBy"")
            VALUES
                (gen_random_uuid(), 'Development', 'Web and software development projects', 'development', 'code', '#3B82F6', '#DBEAFE', 1, TRUE, NOW(), 'system'),
                (gen_random_uuid(), 'Design', 'UI/UX and graphic design projects', 'design', 'palette', '#10B981', '#DCFCE7', 2, TRUE, NOW(), 'system'),
                (gen_random_uuid(), 'Productivity', 'Tools and productivity applications', 'productivity', 'zap', '#F59E0B', '#FEF3C7', 3, TRUE, NOW(), 'system'),
                (gen_random_uuid(), 'API Integration', 'Third-party API integration projects', 'api-integration', 'plug', '#8B5CF6', '#EDE9FE', 4, TRUE, NOW(), 'system')
        ");
    }
}
