using Dapper;
using Npgsql;
using MostafaSaidPortfolio.Data.Seeders.Interfaces;

namespace MostafaSaidPortfolio.Data.Seeders.Implementations;

/// <summary>
/// Seeder for NewsletterSubscriber entities
/// </summary>
public class NewsletterSubscriberSeeder : ISeeder
{
    public async Task SeedAsync(NpgsqlConnection connection)
    {
        var existingCount = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM \"NewsletterSubscribers\"");
        
        if (existingCount > 0)
            return;

        await connection.ExecuteAsync(@"
            INSERT INTO ""NewsletterSubscribers"" 
            (""Id"", ""Email"", ""Name"", ""IsActive"", ""IsConfirmed"", ""ConfirmedAt"", ""IsDeleted"", ""CreatedAt"", ""UpdatedAt"", ""CreatedBy"")
            VALUES
                (gen_random_uuid(), 'subscriber1@example.com', 'John Developer', TRUE, TRUE, NOW(), FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'subscriber2@example.com', 'Jane Coder', TRUE, TRUE, NOW(), FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'subscriber3@example.com', 'Ahmed Tech', TRUE, TRUE, NOW(), FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'subscriber4@example.com', 'Sara Builder', TRUE, TRUE, NOW(), FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'subscriber5@example.com', 'Mohamed Engineer', TRUE, FALSE, NULL, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'subscriber6@example.com', 'Noor Designer', TRUE, TRUE, NOW(), FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'subscriber7@example.com', 'Ali Architect', TRUE, TRUE, NOW(), FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'subscriber8@example.com', 'Fatima Developer', TRUE, TRUE, NOW(), FALSE, NOW(), NOW(), 'system')
        ");
    }
}
