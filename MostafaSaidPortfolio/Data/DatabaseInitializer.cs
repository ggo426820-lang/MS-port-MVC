using Dapper;
using Npgsql;
using MostafaSaidPortfolio.Data.Migrations;
using MostafaSaidPortfolio.Data.Seeders.Implementations;
using MostafaSaidPortfolio.Data.Seeders.Interfaces;

namespace MostafaSaidPortfolio.Data
{
    /// <summary>
    /// Initializes database schema and seeds data
    /// </summary>
    public static class DatabaseInitializer
    {
        /// <summary>
        /// Initialize database tables and seed data
        /// </summary>
        public static async Task InitializeAsync(DbConnectionFactory factory)
        {
            using var conn = factory.CreateConnection();
            await conn.OpenAsync();

            // Create all schema tables
            await SchemaMigrations.RunAllAsync(conn);

            // Seed data in order (dependencies first)
            await SeedDataAsync(conn);
        }

        private static async Task SeedDataAsync(NpgsqlConnection connection)
        {
            var seeders = new List<ISeeder>
            {
                new CategorySeeder(),
                new TagSeeder(),
                new ProjectSeeder(),
                new BlogPostSeeder(),
                new CommentSeeder(),
                new TestimonialSeeder(),
                new NewsletterSubscriberSeeder(),
                new ContactMessageSeeder(),
                new EventSeeder(),
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(connection);
            }
        }
    }
}
