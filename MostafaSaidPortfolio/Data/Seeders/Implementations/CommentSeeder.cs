using Dapper;
using Npgsql;
using MostafaSaidPortfolio.Data.Seeders.Interfaces;

namespace MostafaSaidPortfolio.Data.Seeders.Implementations;

/// <summary>
/// Seeder for Comment entities
/// </summary>
public class CommentSeeder : ISeeder
{
    public async Task SeedAsync(NpgsqlConnection connection)
    {
        var existingCount = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM \"Comments\"");
        
        if (existingCount > 0)
            return;

        // Get the first blog post
        var blogPostId = await connection.ExecuteScalarAsync<Guid>(
            "SELECT \"Id\" FROM \"BlogPosts\" LIMIT 1");

        if (blogPostId == Guid.Empty)
            return;

        await connection.ExecuteAsync(@"
            INSERT INTO ""Comments"" 
            (""Id"", ""Content"", ""AuthorName"", ""AuthorEmail"", ""AuthorWebsite"", ""IsApproved"", ""BlogPostId"", ""IsDeleted"", ""CreatedAt"", ""UpdatedAt"", ""CreatedBy"")
            VALUES
                (gen_random_uuid(), 'Great article! I learned a lot about middleware pipeline. Thanks for the clear explanation.', 'Alex Rodriguez', 'alex@example.com', 'https://alex-dev.com', TRUE, @blogPostId, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'This is exactly what I needed. I was confused about how dependency injection works in ASP.NET Core.', 'Jordan Lee', 'jordan@example.com', NULL, TRUE, @blogPostId, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'Do you have any recommendations for advanced ASP.NET Core topics? I would like to learn about gRPC services.', 'Casey Williams', 'casey@example.com', 'https://casey-codes.dev', TRUE, @blogPostId, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'Excellent post! The code examples are helpful and easy to follow. Keep up the good work!', 'Morgan Taylor', 'morgan@example.com', NULL, TRUE, @blogPostId, FALSE, NOW(), NOW(), 'system'),
                (gen_random_uuid(), 'I found a typo in the second code example. Line 12 should use async Task instead of void.', 'Quinn Adams', 'quinn@example.com', 'https://quinn.dev', FALSE, @blogPostId, FALSE, NOW(), NOW(), 'system')
        ", new { blogPostId });
    }
}
