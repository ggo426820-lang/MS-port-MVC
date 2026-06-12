using Dapper;
using Npgsql;
using MostafaSaidPortfolio.Data.Seeders.Interfaces;

namespace MostafaSaidPortfolio.Data.Seeders.Implementations;

/// <summary>
/// Seeder for Event entities
/// </summary>
public class EventSeeder : ISeeder
{
    public async Task SeedAsync(NpgsqlConnection connection)
    {
        var existingCount = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM \"Events\"");
        
        if (existingCount > 0)
            return;

        var upcomingDate = DateTime.UtcNow.AddMonths(1);
        var pastDate = DateTime.UtcNow.AddMonths(-1);

        await connection.ExecuteAsync(@"
            INSERT INTO ""Events"" 
            (""Id"", ""Title"", ""Description"", ""EventDate"", ""EventEndDate"", ""Location"", ""EventUrl"", ""Status"", ""MaxAttendees"", ""RegisteredCount"", ""IsFeatured"", ""DisplayOrder"", ""CreatedAt"", ""CreatedBy"")
            VALUES
                (gen_random_uuid(), 'Web Development Workshop', 'Learn modern web development practices with ASP.NET Core and PostgreSQL', @upcomingDate, @upcomingDate::date + interval '3 hours', 'Tech Hub Cairo', 'https://example.com/events/web-workshop', 0, 50, 28, TRUE, 1, NOW(), 'system'),
                (gen_random_uuid(), 'API Design Masterclass', 'Deep dive into REST API design principles and best practices', @upcomingDate, @upcomingDate::date + interval '2 hours', 'Online', 'https://example.com/events/api-masterclass', 0, 100, 67, TRUE, 2, NOW(), 'system'),
                (gen_random_uuid(), 'C# Conference 2026', 'Annual conference for C# and .NET developers from across the region', @upcomingDate, @upcomingDate::date + interval '1 day', 'Convention Center', 'https://example.com/events/csharp-conf', 0, 500, 345, TRUE, 3, NOW(), 'system'),
                (gen_random_uuid(), 'Dapper & PostgreSQL Optimization', 'Techniques to optimize database performance with Dapper and PostgreSQL', @upcomingDate, @upcomingDate::date + interval '3 hours', 'Tech Hub Cairo', 'https://example.com/events/dapper-opt', 0, 40, 32, FALSE, 4, NOW(), 'system'),
                (gen_random_uuid(), 'Past Development Summit', 'Recap of the summer development summit with key takeaways', @pastDate, @pastDate::date + interval '8 hours', 'Convention Center', 'https://example.com/events/past-summit', 2, 300, 287, FALSE, 5, NOW(), 'system')
        ", new { upcomingDate });
    }
}
