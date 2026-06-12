using Dapper;
using Npgsql;
using MostafaSaidPortfolio.Data.Seeders.Interfaces;

namespace MostafaSaidPortfolio.Data.Seeders.Implementations;

/// <summary>
/// Seeder for ContactMessage entities
/// </summary>
public class ContactMessageSeeder : ISeeder
{
    public async Task SeedAsync(NpgsqlConnection connection)
    {
        var existingCount = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM \"ContactMessages\"");
        
        if (existingCount > 0)
            return;

        await connection.ExecuteAsync(@"
            INSERT INTO ""ContactMessages"" 
            (""Id"", ""Name"", ""Email"", ""Subject"", ""Message"", ""IsRead"", ""IsReplied"", ""CreatedAt"", ""CreatedBy"")
            VALUES
                (gen_random_uuid(), 'John Smith', 'john@example.com', 'Project Inquiry', 'I am interested in discussing a potential web development project. Could you provide your rates and availability?', TRUE, TRUE, NOW(), 'system'),
                (gen_random_uuid(), 'Sarah Johnson', 'sarah@example.com', 'Freelance Collaboration', 'Would you be interested in collaborating on an e-commerce platform? We have clients looking for experienced ASP.NET developers.', TRUE, FALSE, NOW(), 'system'),
                (gen_random_uuid(), 'Ahmed Hassan', 'ahmed@example.com', 'Portfolio Review', 'Your portfolio is impressive. I wanted to compliment your work on the blog platform. Great technical execution!', TRUE, FALSE, NOW(), 'system'),
                (gen_random_uuid(), 'Lisa Chen', 'lisa@example.com', 'Job Opportunity', 'We have an open position for a Senior .NET Developer at our company. Are you currently open to new opportunities?', FALSE, FALSE, NOW(), 'system'),
                (gen_random_uuid(), 'Mohamed Ali', 'mohamed@example.com', 'Question about Dapper', 'I noticed you use Dapper in your projects. Do you have any resources or recommendations for learning it efficiently?', TRUE, TRUE, NOW(), 'system')
        ");
    }
}
