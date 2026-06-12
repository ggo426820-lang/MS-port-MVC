using Dapper;
using Npgsql;
using MostafaSaidPortfolio.Data.Seeders.Interfaces;

namespace MostafaSaidPortfolio.Data.Seeders.Implementations;

/// <summary>
/// Seeder for BlogPost entities and their tag associations
/// </summary>
public class BlogPostSeeder : ISeeder
{
    public async Task SeedAsync(NpgsqlConnection connection)
    {
        var existingCount = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM \"BlogPosts\"");

        if (existingCount > 0)
            return;

        var devCategoryId = await connection.ExecuteScalarAsync<Guid>(
            "SELECT \"Id\" FROM \"Categories\" WHERE \"Slug\" = 'development' LIMIT 1");

        var designCategoryId = await connection.ExecuteScalarAsync<Guid>(
            "SELECT \"Id\" FROM \"Categories\" WHERE \"Slug\" = 'design' LIMIT 1");

        var content1 = "ASP.NET Core is a cross-platform, high-performance framework for building modern cloud-based applications. " +
            "It runs on Windows, Linux, and macOS, making it ideal for deploying to any environment.\n\n" +
            "<h2>What is ASP.NET Core?</h2>\n" +
            "<p>ASP.NET Core redesigned ASP.NET by combining MVC and Web API into a single unified framework. " +
            "It is open-source, lightweight, and highly modular.</p>\n\n" +
            "<h2>Setting Up Your First App</h2>\n" +
            "<p>Install the .NET SDK from the official Microsoft website. Then use the dotnet CLI to scaffold a new MVC application, " +
            "configure your services in Program.cs, and run the project. Your app will be accessible at localhost:5000.</p>\n\n" +
            "<h2>Middleware Pipeline</h2>\n" +
            "<p>ASP.NET Core uses a middleware pipeline to process HTTP requests. Each middleware component can handle the request " +
            "or pass it to the next component. This makes it easy to add authentication, logging, and error handling.</p>";

        var content2 = "Great design is not just about aesthetics — it is about usability and user experience. " +
            "Here are the key principles every developer should understand.\n\n" +
            "<h2>1. Visual Hierarchy</h2>\n" +
            "<p>Guide users' eyes using size, weight, and color to indicate importance. " +
            "The most critical element should be the most visually prominent.</p>\n\n" +
            "<h2>2. Consistency</h2>\n" +
            "<p>Use consistent colors, fonts, and spacing throughout your application. " +
            "This reduces cognitive load and builds trust with your users.</p>\n\n" +
            "<h2>3. Feedback</h2>\n" +
            "<p>Always provide feedback for user actions. Loading states, success messages, and error indicators keep users informed " +
            "and prevent frustration.</p>\n\n" +
            "<h2>4. Accessibility</h2>\n" +
            "<p>Design for everyone. Use sufficient color contrast, provide alt text for images, " +
            "and ensure keyboard navigation works throughout your application.</p>";

        var content3 = "PostgreSQL is one of the most powerful open-source relational databases, and Dapper is a lightweight micro-ORM " +
            "that gives you full control over your SQL.\n\n" +
            "<h2>Why Dapper?</h2>\n" +
            "<p>Unlike Entity Framework Core, Dapper does not generate SQL for you — you write it yourself. " +
            "This means predictable performance, no N+1 query surprises, and full use of PostgreSQL-specific features.</p>\n\n" +
            "<h2>Getting Started</h2>\n" +
            "<p>Add the Dapper and Npgsql NuGet packages to your project. Create a connection factory that reads " +
            "the DATABASE_URL environment variable, then use Dapper's QueryAsync and ExecuteAsync methods to interact with your data.</p>\n\n" +
            "<h2>Best Practices</h2>\n" +
            "<p>Always use parameterized queries to prevent SQL injection. Use quoted PascalCase column names in PostgreSQL " +
            "to match your C# property names for seamless Dapper mapping without extra configuration.</p>";

        await connection.ExecuteAsync(@"
            INSERT INTO ""BlogPosts""
            (""Id"", ""Title"", ""Content"", ""Summary"", ""Slug"", ""MetaTitle"", ""MetaDescription"", ""FeaturedImageUrl"", ""Status"", ""PublishedAt"", ""ViewCount"", ""ReadingTime"", ""IsFeatured"", ""CategoryId"", ""IsDeleted"", ""CreatedAt"", ""UpdatedAt"", ""CreatedBy"")
            VALUES
                (gen_random_uuid(), 'Getting Started with ASP.NET Core', @content1, 'An introduction to building web apps with ASP.NET Core — setup, structure, and your first endpoint.', 'getting-started-with-aspnet-core', 'Getting Started with ASP.NET Core', 'Learn the basics of ASP.NET Core, a cross-platform framework for building modern web applications.', '', 1, NOW() - interval '30 days', 245, 5, TRUE,  @devCategoryId,    FALSE, NOW() - interval '30 days', NOW() - interval '30 days', 'system'),
                (gen_random_uuid(), 'UI/UX Design Principles Every Developer Should Know', @content2, 'Core principles of UI/UX design that help developers create better user experiences.',                  'ui-ux-design-principles',              'UI/UX Design Principles',              'Explore the fundamental principles of UI/UX design for better, more usable interfaces.',                     '', 1, NOW() - interval '20 days', 186, 4, FALSE, @designCategoryId, FALSE, NOW() - interval '20 days', NOW() - interval '20 days', 'system'),
                (gen_random_uuid(), 'PostgreSQL + Dapper: A Powerful Combination',         @content3, 'Why PostgreSQL and Dapper make an excellent pairing for .NET applications that need performance and control.', 'postgresql-dapper-powerful-combination', 'PostgreSQL + Dapper: A Powerful Combination', 'Explore how to use PostgreSQL and Dapper together for high-performance .NET data access.',                '', 1, NOW() - interval '10 days', 312, 8, FALSE, @devCategoryId,    FALSE, NOW() - interval '10 days', NOW() - interval '10 days', 'system')
        ", new { content1, content2, content3, devCategoryId, designCategoryId });

        await SeedBlogPostTagsAsync(connection);
    }

    private static async Task SeedBlogPostTagsAsync(NpgsqlConnection connection)
    {
        var existingCount = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM \"BlogPostTags\"");
        if (existingCount > 0)
            return;

        var post1Id = await connection.ExecuteScalarAsync<Guid>(
            "SELECT \"Id\" FROM \"BlogPosts\" WHERE \"Slug\" = 'getting-started-with-aspnet-core' LIMIT 1");
        var post2Id = await connection.ExecuteScalarAsync<Guid>(
            "SELECT \"Id\" FROM \"BlogPosts\" WHERE \"Slug\" = 'ui-ux-design-principles' LIMIT 1");
        var post3Id = await connection.ExecuteScalarAsync<Guid>(
            "SELECT \"Id\" FROM \"BlogPosts\" WHERE \"Slug\" = 'postgresql-dapper-powerful-combination' LIMIT 1");

        if (post1Id == Guid.Empty || post2Id == Guid.Empty || post3Id == Guid.Empty)
            return;

        var tagIds = (await connection.QueryAsync<(Guid Id, string Slug)>(
            "SELECT \"Id\", \"Slug\" FROM \"Tags\" WHERE \"IsDeleted\" = FALSE"))
            .ToDictionary(t => t.Slug, t => t.Id);

        var links = new List<(Guid PostId, Guid TagId)>();

        void Link(Guid postId, string tagSlug)
        {
            if (tagIds.TryGetValue(tagSlug, out var tagId))
                links.Add((postId, tagId));
        }

        Link(post1Id, "aspnet-core");
        Link(post1Id, "csharp");
        Link(post1Id, "web-development");

        Link(post2Id, "tailwind-css");
        Link(post2Id, "javascript");
        Link(post2Id, "web-development");

        Link(post3Id, "postgresql");
        Link(post3Id, "dapper");
        Link(post3Id, "csharp");

        foreach (var (postId, tagId) in links)
        {
            await connection.ExecuteAsync(
                "INSERT INTO \"BlogPostTags\" (\"BlogPostsId\", \"TagsId\") VALUES (@postId, @tagId) ON CONFLICT DO NOTHING",
                new { postId, tagId });
        }
    }
}
