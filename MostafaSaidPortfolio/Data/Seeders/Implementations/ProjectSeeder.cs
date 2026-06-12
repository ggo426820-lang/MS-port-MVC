using Dapper;
using Npgsql;
using MostafaSaidPortfolio.Data.Seeders.Interfaces;

namespace MostafaSaidPortfolio.Data.Seeders.Implementations;

/// <summary>
/// Seeder for Project entities and their tag associations
/// </summary>
public class ProjectSeeder : ISeeder
{
    public async Task SeedAsync(NpgsqlConnection connection)
    {
        var existingCount = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM \"Projects\"");

        if (existingCount > 0)
            return;

        var devCategoryId = await connection.ExecuteScalarAsync<Guid>(
            "SELECT \"Id\" FROM \"Categories\" WHERE \"Slug\" = 'development' LIMIT 1");
        var designCategoryId = await connection.ExecuteScalarAsync<Guid>(
            "SELECT \"Id\" FROM \"Categories\" WHERE \"Slug\" = 'design' LIMIT 1");
        var productivityCategoryId = await connection.ExecuteScalarAsync<Guid>(
            "SELECT \"Id\" FROM \"Categories\" WHERE \"Slug\" = 'productivity' LIMIT 1");
        var apiCategoryId = await connection.ExecuteScalarAsync<Guid>(
            "SELECT \"Id\" FROM \"Categories\" WHERE \"Slug\" = 'api-integration' LIMIT 1");

        await connection.ExecuteAsync(@"
            INSERT INTO ""Projects""
            (""Id"", ""Title"", ""Description"", ""LongDescription"", ""Slug"", ""TechnologyStack"", ""GitHubUrl"", ""LiveUrl"", ""ImageUrl"", ""ThumbnailUrl"", ""CategoryId"", ""Status"", ""DisplayOrder"", ""IsFeatured"", ""ViewCount"", ""LikeCount"", ""IsDeleted"", ""CreatedAt"", ""UpdatedAt"", ""CreatedBy"")
            VALUES
                (gen_random_uuid(), 'Portfolio Website',
                    'A personal portfolio showcasing projects and blog posts',
                    'Built with ASP.NET Core 9, PostgreSQL, and Dapper ORM. Features a blog, project showcase, testimonials, and newsletter.',
                    'portfolio-website', 'ASP.NET Core, PostgreSQL, Dapper, Tailwind CSS',
                    'https://github.com/mostafasaid/portfolio', 'https://example.com/portfolio', '', '',
                    @devCategoryId, 1, 1, TRUE, 150, 25, FALSE, NOW() - interval '60 days', NOW(), 'system'),

                (gen_random_uuid(), 'E-commerce App',
                    'An online shop with cart, checkout, and product management',
                    'Full-featured e-commerce platform with cart, checkout, user authentication, and product management.',
                    'ecommerce-app', 'ASP.NET Core, PostgreSQL, Stripe, Bootstrap',
                    'https://github.com/mostafasaid/ecommerce', 'https://example.com/shop', '', '',
                    @devCategoryId, 1, 2, TRUE, 320, 58, FALSE, NOW() - interval '50 days', NOW(), 'system'),

                (gen_random_uuid(), 'Blog Platform',
                    'A full-featured blog with authentication and rich content',
                    'Supports markdown content, categories, tags, comments, and newsletter subscriptions.',
                    'blog-platform', 'ASP.NET Core, Identity, PostgreSQL, Dapper',
                    'https://github.com/mostafasaid/blog-platform', 'https://example.com/blog', '', '',
                    @devCategoryId, 1, 3, TRUE, 210, 42, FALSE, NOW() - interval '40 days', NOW(), 'system'),

                (gen_random_uuid(), 'Task Manager App',
                    'A productivity app for managing tasks and project boards',
                    'Includes project boards, task assignments, deadline tracking, and email reminders.',
                    'task-manager-app', 'ASP.NET Core, JavaScript, PostgreSQL',
                    'https://github.com/mostafasaid/task-manager', 'https://example.com/tasks', '', '',
                    @productivityCategoryId, 1, 4, FALSE, 85, 15, FALSE, NOW() - interval '30 days', NOW(), 'system'),

                (gen_random_uuid(), 'Weather Dashboard',
                    'Real-time weather dashboard with 7-day forecast',
                    'Integrates with OpenWeatherMap API. Shows current conditions, 7-day forecast, and historical data.',
                    'weather-dashboard', 'ASP.NET Core, REST APIs, Chart.js, Tailwind CSS',
                    'https://github.com/mostafasaid/weather-dashboard', 'https://example.com/weather', '', '',
                    @apiCategoryId, 1, 5, FALSE, 120, 28, FALSE, NOW() - interval '20 days', NOW(), 'system'),

                (gen_random_uuid(), 'Design System Toolkit',
                    'A reusable component library and design system',
                    'A comprehensive design system with Figma source files, CSS custom properties, and reusable Razor components.',
                    'design-system-toolkit', 'Figma, CSS, JavaScript, Tailwind CSS',
                    'https://github.com/mostafasaid/design-system', 'https://example.com/design-system', '', '',
                    @designCategoryId, 1, 6, FALSE, 95, 20, FALSE, NOW() - interval '10 days', NOW(), 'system')
        ", new { devCategoryId, designCategoryId, productivityCategoryId, apiCategoryId });

        await SeedProjectTagsAsync(connection);
    }

    private static async Task SeedProjectTagsAsync(NpgsqlConnection connection)
    {
        var existingCount = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM \"ProjectTags\"");
        if (existingCount > 0)
            return;

        var projectIds = (await connection.QueryAsync<(Guid Id, string Slug)>(
            "SELECT \"Id\", \"Slug\" FROM \"Projects\" WHERE \"IsDeleted\" = FALSE"))
            .ToDictionary(p => p.Slug, p => p.Id);

        var tagIds = (await connection.QueryAsync<(Guid Id, string Slug)>(
            "SELECT \"Id\", \"Slug\" FROM \"Tags\" WHERE \"IsDeleted\" = FALSE"))
            .ToDictionary(t => t.Slug, t => t.Id);

        var links = new List<(Guid ProjectId, Guid TagId)>();

        void Link(string projectSlug, string tagSlug)
        {
            if (projectIds.TryGetValue(projectSlug, out var pId) && tagIds.TryGetValue(tagSlug, out var tId))
                links.Add((pId, tId));
        }

        Link("portfolio-website", "aspnet-core");
        Link("portfolio-website", "csharp");
        Link("portfolio-website", "tailwind-css");
        Link("portfolio-website", "postgresql");

        Link("ecommerce-app", "aspnet-core");
        Link("ecommerce-app", "csharp");
        Link("ecommerce-app", "rest-api");
        Link("ecommerce-app", "bootstrap");

        Link("blog-platform", "aspnet-core");
        Link("blog-platform", "csharp");
        Link("blog-platform", "postgresql");
        Link("blog-platform", "dapper");

        Link("task-manager-app", "javascript");
        Link("task-manager-app", "aspnet-core");
        Link("task-manager-app", "web-development");

        Link("weather-dashboard", "rest-api");
        Link("weather-dashboard", "javascript");
        Link("weather-dashboard", "tailwind-css");

        Link("design-system-toolkit", "tailwind-css");
        Link("design-system-toolkit", "javascript");
        Link("design-system-toolkit", "web-development");

        foreach (var (projectId, tagId) in links)
        {
            await connection.ExecuteAsync(
                "INSERT INTO \"ProjectTags\" (\"ProjectsId\", \"TagsId\") VALUES (@projectId, @tagId) ON CONFLICT DO NOTHING",
                new { projectId, tagId });
        }
    }
}
