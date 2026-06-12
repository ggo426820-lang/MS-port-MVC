using Dapper;
using Npgsql;
using MostafaSaidPortfolio.Data.Seeders.Interfaces;

namespace MostafaSaidPortfolio.Data.Seeders.Implementations;

/// <summary>
/// Seeder for Skill entities
/// </summary>
public class SkillSeeder : ISeeder
{
    public async Task SeedAsync(NpgsqlConnection connection)
    {
        var existingCount = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM \"Skills\"");

        if (existingCount > 0)
            return;

        await connection.ExecuteAsync(@"
            INSERT INTO ""Skills""
            (""Id"", ""Name"", ""Category"", ""Proficiency"", ""IconName"", ""Description"", ""DisplayOrder"", ""IsActive"", ""IsDeleted"", ""CreatedAt"", ""UpdatedAt"")
            VALUES
                -- Frontend
                (gen_random_uuid(), 'HTML5',           'Frontend', 95, 'html5',         'Semantic markup, accessibility, and modern HTML features.',              1,  TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'CSS3',            'Frontend', 90, 'css3',          'Responsive layouts, animations, custom properties, and Flexbox/Grid.',  2,  TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'JavaScript',      'Frontend', 85, 'javascript',    'ES6+, async/await, DOM manipulation, and browser APIs.',                3,  TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'TypeScript',      'Frontend', 75, 'typescript',    'Typed JavaScript for large-scale, maintainable applications.',          4,  TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'Tailwind CSS',    'Frontend', 90, 'tailwindcss',   'Utility-first CSS framework for rapid, consistent UI development.',     5,  TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'Bootstrap',       'Frontend', 80, 'bootstrap',     'Component library for fast, responsive web interfaces.',               6,  TRUE, FALSE, NOW(), NOW()),

                -- Backend
                (gen_random_uuid(), 'C#',              'Backend',  95, 'csharp',        'Primary language: LINQ, async patterns, generics, and OOP.',           10, TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'ASP.NET Core',    'Backend',  92, 'dotnet',        'MVC, Web API, middleware pipeline, and dependency injection.',         11, TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'Dapper',          'Backend',  90, 'dapper',        'Lightweight micro-ORM for high-performance SQL data access.',           12, TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'Entity Framework Core', 'Backend', 80, 'entityframework', 'Code-first ORM with migrations and LINQ-to-SQL queries.', 13, TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'REST APIs',       'Backend',  88, 'api',           'Designing and consuming RESTful services with proper HTTP semantics.',  14, TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'PostgreSQL',      'Backend',  88, 'postgresql',    'Relational database design, indexing, and advanced query optimization.', 15, TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'Redis',           'Backend',  65, 'redis',         'In-memory caching and pub/sub messaging.',                             16, TRUE, FALSE, NOW(), NOW()),

                -- DevOps
                (gen_random_uuid(), 'Git',             'DevOps',   90, 'git',           'Version control, branching strategies, and collaborative workflows.',  20, TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'Docker',          'DevOps',   70, 'docker',        'Containerization, Dockerfiles, and Docker Compose.',                   21, TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'Linux',           'DevOps',   75, 'linux',         'Shell scripting, server administration, and process management.',      22, TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'CI/CD',           'DevOps',   68, 'github',        'Automated build, test, and deployment pipelines.',                     23, TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'Azure',           'DevOps',   65, 'azure',         'Cloud hosting, App Service, Blob Storage, and Azure DevOps.',          24, TRUE, FALSE, NOW(), NOW()),

                -- Design
                (gen_random_uuid(), 'Figma',           'Design',   72, 'figma',         'UI wireframing, prototyping, and design collaboration.',               30, TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'UI/UX Design',    'Design',   70, 'design',        'User-centred design principles, accessibility, and usability testing.', 31, TRUE, FALSE, NOW(), NOW()),

                -- Tools
                (gen_random_uuid(), 'Visual Studio',   'Tools',    92, 'visualstudio',  'Full IDE for .NET development with advanced debugging.',               40, TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'VS Code',         'Tools',    90, 'vscode',        'Lightweight editor with rich extension ecosystem.',                    41, TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'Postman',         'Tools',    85, 'postman',       'API testing, collections, and automated test suites.',                 42, TRUE, FALSE, NOW(), NOW()),
                (gen_random_uuid(), 'GitHub',          'Tools',    88, 'github',        'Repository hosting, pull requests, and project management.',           43, TRUE, FALSE, NOW(), NOW())
        ");
    }
}
