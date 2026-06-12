using Dapper;
using Npgsql;

namespace MostafaSaidPortfolio.Data
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeAsync(DbConnectionFactory factory)
        {
            using var conn = factory.CreateConnection();
            await conn.OpenAsync();

            await conn.ExecuteAsync(@"
                CREATE TABLE IF NOT EXISTS ""Categories"" (
                    ""Id"" SERIAL PRIMARY KEY,
                    ""Name"" VARCHAR(100) NOT NULL DEFAULT '',
                    ""Description"" TEXT NOT NULL DEFAULT '',
                    ""Slug"" VARCHAR(100) NOT NULL DEFAULT '',
                    ""Icon"" VARCHAR(100) NOT NULL DEFAULT '',
                    ""Color"" VARCHAR(20) NOT NULL DEFAULT '',
                    ""BackgroundColor"" VARCHAR(20) NOT NULL DEFAULT '',
                    ""DisplayOrder"" INT NOT NULL DEFAULT 0,
                    ""IsActive"" BOOLEAN NOT NULL DEFAULT TRUE,
                    ""ParentId"" INT REFERENCES ""Categories""(""Id"") ON DELETE RESTRICT,
                    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW()
                );

                CREATE TABLE IF NOT EXISTS ""Users"" (
                    ""Id"" SERIAL PRIMARY KEY,
                    ""Name"" VARCHAR(100) NOT NULL DEFAULT ''
                );

                CREATE TABLE IF NOT EXISTS ""Projects"" (
                    ""Id"" SERIAL PRIMARY KEY,
                    ""Title"" VARCHAR(200) NOT NULL DEFAULT '',
                    ""Description"" TEXT NOT NULL DEFAULT '',
                    ""LongDescription"" TEXT NOT NULL DEFAULT '',
                    ""TechnologyStack"" VARCHAR(1000) NOT NULL DEFAULT '',
                    ""GitHubUrl"" VARCHAR(500) NOT NULL DEFAULT '',
                    ""LiveUrl"" VARCHAR(500) NOT NULL DEFAULT '',
                    ""ImageUrl"" VARCHAR(500) NOT NULL DEFAULT '',
                    ""ThumbnailUrl"" VARCHAR(500) NOT NULL DEFAULT '',
                    ""CategoryId"" INT REFERENCES ""Categories""(""Id"") ON DELETE SET NULL,
                    ""Status"" INT NOT NULL DEFAULT 1,
                    ""DisplayOrder"" INT NOT NULL DEFAULT 0,
                    ""IsFeatured"" BOOLEAN NOT NULL DEFAULT FALSE,
                    ""ViewCount"" INT NOT NULL DEFAULT 0,
                    ""LikeCount"" INT NOT NULL DEFAULT 0,
                    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                    ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                    ""CreatedBy"" VARCHAR(100) NOT NULL DEFAULT '',
                    ""UpdatedBy"" VARCHAR(100) NOT NULL DEFAULT '',
                    ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE
                );

                CREATE UNIQUE INDEX IF NOT EXISTS ""IX_Projects_Title"" ON ""Projects""(""Title"");

                CREATE TABLE IF NOT EXISTS ""Tags"" (
                    ""Id"" SERIAL PRIMARY KEY,
                    ""Name"" VARCHAR(100) NOT NULL DEFAULT ''
                );

                CREATE TABLE IF NOT EXISTS ""BlogPosts"" (
                    ""Id"" SERIAL PRIMARY KEY,
                    ""Name"" VARCHAR(300) NOT NULL DEFAULT '',
                    ""Title"" VARCHAR(300) NOT NULL DEFAULT '',
                    ""Content"" TEXT NOT NULL DEFAULT '',
                    ""Summary"" VARCHAR(1000) NOT NULL DEFAULT '',
                    ""Slug"" VARCHAR(200) NOT NULL DEFAULT '',
                    ""MetaTitle"" VARCHAR(200) NOT NULL DEFAULT '',
                    ""MetaDescription"" VARCHAR(500) NOT NULL DEFAULT '',
                    ""FeaturedImageUrl"" VARCHAR(500) NOT NULL DEFAULT '',
                    ""CategoryId"" INT REFERENCES ""Categories""(""Id"") ON DELETE SET NULL,
                    ""AuthorId"" INT REFERENCES ""Users""(""Id"") ON DELETE SET NULL,
                    ""Status"" INT NOT NULL DEFAULT 0,
                    ""PublishedAt"" TIMESTAMP,
                    ""ScheduledAt"" TIMESTAMP,
                    ""ViewCount"" INT NOT NULL DEFAULT 0,
                    ""CommentCount"" INT NOT NULL DEFAULT 0,
                    ""ReadingTime"" INT,
                    ""IsFeatured"" BOOLEAN NOT NULL DEFAULT FALSE,
                    ""IsPublished"" BOOLEAN NOT NULL DEFAULT FALSE,
                    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                    ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                    ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE
                );

                CREATE TABLE IF NOT EXISTS ""BlogPostTags"" (
                    ""BlogPostsId"" INT NOT NULL REFERENCES ""BlogPosts""(""Id"") ON DELETE CASCADE,
                    ""TagsId"" INT NOT NULL REFERENCES ""Tags""(""Id"") ON DELETE CASCADE,
                    PRIMARY KEY (""BlogPostsId"", ""TagsId"")
                );

                CREATE TABLE IF NOT EXISTS ""ProjectTags"" (
                    ""ProjectsId"" INT NOT NULL REFERENCES ""Projects""(""Id"") ON DELETE CASCADE,
                    ""TagsId"" INT NOT NULL REFERENCES ""Tags""(""Id"") ON DELETE CASCADE,
                    PRIMARY KEY (""ProjectsId"", ""TagsId"")
                );

                CREATE TABLE IF NOT EXISTS ""Comments"" (
                    ""Id"" SERIAL PRIMARY KEY,
                    ""Content"" TEXT NOT NULL DEFAULT '',
                    ""BlogPostId"" INT REFERENCES ""BlogPosts""(""Id"") ON DELETE SET NULL,
                    ""AuthorId"" INT REFERENCES ""Users""(""Id"") ON DELETE SET NULL,
                    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                    ""IsApproved"" BOOLEAN NOT NULL DEFAULT FALSE
                );

                CREATE TABLE IF NOT EXISTS ""Events"" (
                    ""Id"" SERIAL PRIMARY KEY,
                    ""Title"" VARCHAR(200) NOT NULL DEFAULT '',
                    ""Description"" TEXT NOT NULL DEFAULT '',
                    ""Date"" TIMESTAMP NOT NULL,
                    ""EndDate"" TIMESTAMP NOT NULL,
                    ""Location"" VARCHAR(200) NOT NULL DEFAULT '',
                    ""IsOnline"" BOOLEAN NOT NULL DEFAULT FALSE,
                    ""MaxAttendees"" INT NOT NULL DEFAULT 0,
                    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW()
                );

                CREATE TABLE IF NOT EXISTS ""NewsletterSubscribers"" (
                    ""Id"" SERIAL PRIMARY KEY,
                    ""Email"" VARCHAR(200) NOT NULL,
                    ""SubscribedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                    ""IsActive"" BOOLEAN NOT NULL DEFAULT TRUE
                );

                CREATE UNIQUE INDEX IF NOT EXISTS ""IX_NewsletterSubscribers_Email"" ON ""NewsletterSubscribers""(""Email"");

                CREATE TABLE IF NOT EXISTS ""Testimonials"" (
                    ""Id"" SERIAL PRIMARY KEY,
                    ""Author"" VARCHAR(100) NOT NULL DEFAULT '',
                    ""Content"" TEXT NOT NULL DEFAULT '',
                    ""Company"" VARCHAR(200) NOT NULL DEFAULT '',
                    ""Rating"" INT NOT NULL DEFAULT 5,
                    ""ImageUrl"" VARCHAR(500) NOT NULL DEFAULT '',
                    ""IsApproved"" BOOLEAN NOT NULL DEFAULT FALSE,
                    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW()
                );

                CREATE TABLE IF NOT EXISTS ""ContactMessages"" (
                    ""Id"" SERIAL PRIMARY KEY,
                    ""Name"" VARCHAR(200) NOT NULL DEFAULT '',
                    ""Email"" VARCHAR(200) NOT NULL,
                    ""Subject"" VARCHAR(200) NOT NULL DEFAULT '',
                    ""Message"" VARCHAR(2000) NOT NULL DEFAULT '',
                    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                    ""IsRead"" BOOLEAN NOT NULL DEFAULT FALSE
                );
            ");

            await SeedAsync(conn);
        }

        private static async Task SeedAsync(NpgsqlConnection conn)
        {
            var catCount = await conn.ExecuteScalarAsync<int>(@"SELECT COUNT(*) FROM ""Categories""");
            if (catCount > 0) return;

            await conn.ExecuteAsync(@"
                INSERT INTO ""Categories"" (""Id"", ""Name"", ""Slug"") VALUES
                    (1, 'Development', 'development'),
                    (2, 'Design', 'design'),
                    (3, 'Productivity', 'productivity'),
                    (4, 'API Integration', 'api-integration');

                SELECT setval('""Categories_Id_seq""', 10);

                INSERT INTO ""Users"" (""Id"", ""Name"") VALUES (1, 'Mostafa Said');
                SELECT setval('""Users_Id_seq""', 10);

                INSERT INTO ""Projects""
                    (""Title"", ""Description"", ""LongDescription"", ""TechnologyStack"", ""LiveUrl"", ""GitHubUrl"", ""ImageUrl"", ""ThumbnailUrl"", ""CategoryId"", ""DisplayOrder"", ""IsFeatured"")
                VALUES
                    ('Portfolio Website', 'A personal portfolio showcasing projects, blog posts, and contact info.', 'Built with ASP.NET Core 9, PostgreSQL, and Dapper ORM. Features a blog, project showcase, testimonials, and newsletter.', 'ASP.NET Core, PostgreSQL, Dapper, Tailwind CSS', 'https://example.com/portfolio', 'https://github.com/mostafasaid/portfolio', '', '', 1, 1, TRUE),
                    ('E-commerce App', 'An online shop built with ASP.NET Core and PostgreSQL.', 'Full-featured e-commerce platform with cart, checkout, user authentication, and product management.', 'ASP.NET Core, PostgreSQL, Stripe, Bootstrap', 'https://example.com/shop', 'https://github.com/mostafasaid/ecommerce', '', '', 1, 2, TRUE),
                    ('Blog Platform', 'A full-featured blog platform with authentication and CRUD operations.', 'Supports markdown content, categories, tags, comments, and newsletter subscriptions.', 'ASP.NET Core, Identity, PostgreSQL, Dapper', 'https://example.com/blog', 'https://github.com/mostafasaid/blog-platform', '', '', 1, 3, TRUE),
                    ('Task Manager App', 'A productivity app for managing tasks with deadlines and priorities.', 'Includes project boards, task assignments, deadline tracking, and email reminders.', 'ASP.NET Core, JavaScript, PostgreSQL', 'https://example.com/tasks', 'https://github.com/mostafasaid/task-manager', '', '', 3, 4, FALSE),
                    ('Weather Dashboard', 'A real-time weather dashboard using public APIs and responsive UI.', 'Integrates with OpenWeatherMap API. Shows current conditions, 7-day forecast, and historical data.', 'ASP.NET Core, REST APIs, Chart.js, Tailwind CSS', 'https://example.com/weather', 'https://github.com/mostafasaid/weather-dashboard', '', '', 4, 5, FALSE);
            ");

            await SeedBlogPostsAsync(conn);
            await SeedTestimonialsAsync(conn);
        }

        private static async Task SeedBlogPostsAsync(NpgsqlConnection conn)
        {
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

            await conn.ExecuteAsync(@"
                INSERT INTO ""BlogPosts""
                    (""Name"", ""Title"", ""Content"", ""Summary"", ""Slug"", ""MetaTitle"", ""MetaDescription"",
                     ""IsPublished"", ""IsFeatured"", ""CategoryId"", ""AuthorId"", ""ReadingTime"", ""FeaturedImageUrl"", ""PublishedAt"")
                VALUES
                    (@name1, @title1, @content1, @summary1, @slug1, @metaTitle1, @metaDesc1, TRUE, TRUE, 1, 1, 5, '', NOW()),
                    (@name2, @title2, @content2, @summary2, @slug2, @metaTitle2, @metaDesc2, TRUE, FALSE, 2, 1, 4, '', NOW()),
                    (@name3, @title3, @content3, @summary3, @slug3, @metaTitle3, @metaDesc3, TRUE, FALSE, 1, 1, 8, '', NOW())",
                new
                {
                    name1 = "getting-started-aspnet-core", title1 = "Getting Started with ASP.NET Core",
                    content1, summary1 = "An introduction to building web apps with ASP.NET Core — setup, structure, and your first endpoint.",
                    slug1 = "getting-started-with-aspnet-core", metaTitle1 = "Getting Started with ASP.NET Core",
                    metaDesc1 = "Learn the basics of ASP.NET Core, a cross-platform framework for building modern web applications.",

                    name2 = "ui-ux-design-principles", title2 = "UI/UX Design Principles Every Developer Should Know",
                    content2, summary2 = "Core principles of UI/UX design that help developers create better user experiences.",
                    slug2 = "ui-ux-design-principles", metaTitle2 = "UI/UX Design Principles",
                    metaDesc2 = "Explore the fundamental principles of UI/UX design for better, more usable interfaces.",

                    name3 = "postgresql-dapper-combination", title3 = "PostgreSQL + Dapper: A Powerful Combination",
                    content3, summary3 = "Why PostgreSQL and Dapper make an excellent pairing for .NET applications that need performance and control.",
                    slug3 = "postgresql-dapper-powerful-combination", metaTitle3 = "PostgreSQL + Dapper: A Powerful Combination",
                    metaDesc3 = "Explore how to use PostgreSQL and Dapper together for high-performance .NET data access."
                });
        }

        private static async Task SeedTestimonialsAsync(NpgsqlConnection conn)
        {
            await conn.ExecuteAsync(@"
                INSERT INTO ""Testimonials"" (""Author"", ""Content"", ""Company"", ""Rating"", ""IsApproved"")
                VALUES
                    ('Ahmed Hassan', 'Mostafa delivered our project on time and exceeded expectations. The code quality was excellent.', 'TechCorp Egypt', 5, TRUE),
                    ('Sara Ali', 'Working with Mostafa was a great experience. He has strong technical skills and communicates clearly.', 'Freelance Client', 5, TRUE),
                    ('Mohamed Kamal', 'The portfolio website he built for us looks amazing and performs flawlessly.', 'Digital Agency', 5, TRUE);
            ");
        }
    }
}
