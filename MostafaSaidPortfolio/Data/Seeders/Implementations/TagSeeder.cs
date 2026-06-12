using Dapper;
using Npgsql;
using MostafaSaidPortfolio.Data.Seeders.Interfaces;

namespace MostafaSaidPortfolio.Data.Seeders.Implementations;

/// <summary>
/// Seeder for Tag entities
/// </summary>
public class TagSeeder : ISeeder
{
    public async Task SeedAsync(NpgsqlConnection connection)
    {
        var existingCount = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM \"Tags\"");
        
        if (existingCount > 0)
            return;

        await connection.ExecuteAsync(@"
            INSERT INTO ""Tags"" 
            (""Id"", ""Name"", ""Slug"", ""Color"", ""UsageCount"", ""IsDeleted"", ""CreatedAt"", ""UpdatedAt"", ""CreatedBy"")
            VALUES
                (gen_random_uuid(), 'C#', 'csharp', '#239120', 5, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'ASP.NET Core', 'aspnet-core', '#512BD4', 8, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'PostgreSQL', 'postgresql', '#336791', 6, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'Dapper', 'dapper', '#FF6B35', 4, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'Entity Framework', 'entity-framework', '#512BD4', 3, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'REST API', 'rest-api', '#00D084', 7, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'JavaScript', 'javascript', '#F7DF1E', 4, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'Tailwind CSS', 'tailwind-css', '#06B6D4', 5, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'Bootstrap', 'bootstrap', '#7952B3', 3, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'Web Development', 'web-development', '#3B82F6', 10, FALSE, NOW(), NOW(), 'system')
        ");
    }
}
