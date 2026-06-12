using Dapper;
using Npgsql;
using MostafaSaidPortfolio.Data.Seeders.Interfaces;

namespace MostafaSaidPortfolio.Data.Seeders.Implementations;

/// <summary>
/// Seeder for Project entities
/// </summary>
public class ProjectSeeder : ISeeder
{
    public async Task SeedAsync(NpgsqlConnection connection)
    {
        var existingCount = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM \"Projects\"");
        
        if (existingCount > 0)
            return;

        // Get the Development category ID (first one)
        var devCategoryId = await connection.ExecuteScalarAsync<Guid>(
            "SELECT \"Id\" FROM \"Categories\" WHERE \"Slug\" = 'development' LIMIT 1");

        await connection.ExecuteAsync(@"
            INSERT INTO ""Projects"" 
            (""Id"", ""Title"", ""Description"", ""LongDescription"", ""Slug"", ""TechnologyStack"", ""GitHubUrl"", ""LiveUrl"", ""ImageUrl"", ""ThumbnailUrl"", ""CategoryId"", ""Status"", ""DisplayOrder"", ""IsFeatured"", ""ViewCount"", ""LikeCount"", ""CreatedAt"", ""CreatedBy"")
            VALUES
                (gen_random_uuid(), 'Portfolio Website', 'A personal portfolio showcasing projects and blog posts', 'Built with ASP.NET Core 9, PostgreSQL, and Dapper ORM. Features a blog, project showcase, testimonials, and newsletter.', 'portfolio-website', 'ASP.NET Core, PostgreSQL, Dapper, Tailwind CSS', 'https://github.com/mostafasaid/portfolio', 'https://example.com/portfolio', '', '', @categoryId, 1, 1, TRUE, 150, 25, NOW(), 'system'),
                (gen_random_uuid(), 'E-commerce App', 'An online shop with cart and checkout', 'Full-featured e-commerce platform with cart, checkout, user authentication, and product management.', 'ecommerce-app', 'ASP.NET Core, PostgreSQL, Stripe, Bootstrap', 'https://github.com/mostafasaid/ecommerce', 'https://example.com/shop', '', '', @categoryId, 1, 2, TRUE, 320, 58, NOW(), 'system'),
                (gen_random_uuid(), 'Blog Platform', 'A full-featured blog with authentication', 'Supports markdown content, categories, tags, comments, and newsletter subscriptions.', 'blog-platform', 'ASP.NET Core, Identity, PostgreSQL, Dapper', 'https://github.com/mostafasaid/blog-platform', 'https://example.com/blog', '', '', @categoryId, 1, 3, TRUE, 210, 42, NOW(), 'system'),
                (gen_random_uuid(), 'Task Manager App', 'A productivity app for managing tasks', 'Includes project boards, task assignments, deadline tracking, and email reminders.', 'task-manager-app', 'ASP.NET Core, JavaScript, PostgreSQL', 'https://github.com/mostafasaid/task-manager', 'https://example.com/tasks', '', '', @categoryId, 0, 4, FALSE, 85, 15, NOW(), 'system'),
                (gen_random_uuid(), 'Weather Dashboard', 'Real-time weather dashboard', 'Integrates with OpenWeatherMap API. Shows current conditions, 7-day forecast, and historical data.', 'weather-dashboard', 'ASP.NET Core, REST APIs, Chart.js, Tailwind CSS', 'https://github.com/mostafasaid/weather-dashboard', 'https://example.com/weather', '', '', @categoryId, 1, 5, FALSE, 120, 28, NOW(), 'system')
        ", new { categoryId = devCategoryId });
    }
}
