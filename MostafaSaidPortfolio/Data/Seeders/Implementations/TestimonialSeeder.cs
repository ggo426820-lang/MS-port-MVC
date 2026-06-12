using Dapper;
using Npgsql;
using MostafaSaidPortfolio.Data.Seeders.Interfaces;

namespace MostafaSaidPortfolio.Data.Seeders.Implementations;

/// <summary>
/// Seeder for Testimonial entities
/// </summary>
public class TestimonialSeeder : ISeeder
{
    public async Task SeedAsync(NpgsqlConnection connection)
    {
        var existingCount = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM \"Testimonials\"");
        
        if (existingCount > 0)
            return;

        await connection.ExecuteAsync(@"
            INSERT INTO ""Testimonials"" 
            (""Id"", ""AuthorName"", ""Position"", ""Company"", ""Content"", ""Rating"", ""IsFeatured"", ""DisplayOrder"", ""IsApproved"", ""IsDeleted"", ""CreatedAt"", ""UpdatedAt"", ""CreatedBy"")
            VALUES
                (gen_random_uuid(), 'Ahmed Hassan', 'CTO', 'TechCorp Egypt', 'Mostafa delivered our project on time and exceeded expectations. The code quality was excellent and the architecture was well-thought-out.', 5, TRUE, 1, TRUE, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'Sara Ali', 'Project Manager', 'Freelance Client', 'Working with Mostafa was a great experience. He has strong technical skills and communicates clearly about progress and challenges.', 5, TRUE, 2, TRUE, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'Mohamed Kamal', 'Founder', 'Digital Agency', 'The portfolio website he built for us looks amazing and performs flawlessly. Highly recommended for any web project.', 5, TRUE, 3, TRUE, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'Layla Ibrahim', 'Product Lead', 'SaaS Startup', 'Mostafa''s expertise in ASP.NET Core and PostgreSQL was invaluable. He optimized our database queries and reduced latency by 40%.', 5, FALSE, 4, TRUE, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'Khalid Omar', 'Tech Lead', 'Enterprise Corp', 'Professional, reliable, and always delivers quality work. Mostafa has been a great addition to our development team.', 4, FALSE, 5, TRUE, FALSE, NOW(), NOW(), 'system')
        ");
    }
}
