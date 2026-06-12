using Npgsql;

namespace MostafaSaidPortfolio.Data.Migrations;

/// <summary>
/// Database schema migrations - creates all tables
/// </summary>
public static class SchemaMigrations
{
    /// <summary>
    /// Run all schema migrations
    /// </summary>
    public static async Task RunAllAsync(NpgsqlConnection connection)
    {
        await CreateCategoriesTableAsync(connection);
        await CreateUsersTableAsync(connection);
        await CreateProjectsTableAsync(connection);
        await CreateTagsTableAsync(connection);
        await CreateBlogPostsTableAsync(connection);
        await CreateBlogPostTagsTableAsync(connection);
        await CreateProjectTagsTableAsync(connection);
        await CreateCommentsTableAsync(connection);
        await CreateEventsTableAsync(connection);
        await CreateNewsletterSubscribersTableAsync(connection);
        await CreateTestimonialsTableAsync(connection);
        await CreateContactMessagesTableAsync(connection);
    }

    private static async Task CreateCategoriesTableAsync(NpgsqlConnection conn)
    {
        await conn.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS ""Categories"" (
                ""Id"" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                ""Name"" VARCHAR(100) NOT NULL,
                ""Description"" TEXT NOT NULL DEFAULT '',
                ""Slug"" VARCHAR(100) NOT NULL,
                ""Icon"" VARCHAR(100) NOT NULL DEFAULT '',
                ""Color"" VARCHAR(20) NOT NULL DEFAULT '',
                ""BackgroundColor"" VARCHAR(20) NOT NULL DEFAULT '',
                ""DisplayOrder"" INT NOT NULL DEFAULT 0,
                ""IsActive"" BOOLEAN NOT NULL DEFAULT TRUE,
                ""ParentId"" UUID REFERENCES ""Categories""(""Id"") ON DELETE RESTRICT,
                ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""CreatedBy"" VARCHAR(100),
                ""UpdatedBy"" VARCHAR(100),
                ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""DeletedAt"" TIMESTAMP,
                ""DeletedBy"" VARCHAR(100)
            );
        ");
    }

    private static async Task CreateUsersTableAsync(NpgsqlConnection conn)
    {
        await conn.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS ""ApplicationUsers"" (
                ""Id"" VARCHAR(128) PRIMARY KEY,
                ""FullName"" VARCHAR(100),
                ""Bio"" TEXT,
                ""ProfileImageUrl"" VARCHAR(500),
                ""IsActive"" BOOLEAN NOT NULL DEFAULT TRUE,
                ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT NOW()
            );
        ");
    }

    private static async Task CreateProjectsTableAsync(NpgsqlConnection conn)
    {
        await conn.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS ""Projects"" (
                ""Id"" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                ""Title"" VARCHAR(200) NOT NULL,
                ""Description"" TEXT NOT NULL DEFAULT '',
                ""LongDescription"" TEXT NOT NULL DEFAULT '',
                ""Slug"" VARCHAR(200) NOT NULL,
                ""TechnologyStack"" VARCHAR(1000) NOT NULL DEFAULT '',
                ""GitHubUrl"" VARCHAR(500) NOT NULL DEFAULT '',
                ""LiveUrl"" VARCHAR(500) NOT NULL DEFAULT '',
                ""ImageUrl"" VARCHAR(500) NOT NULL DEFAULT '',
                ""ThumbnailUrl"" VARCHAR(500) NOT NULL DEFAULT '',
                ""CategoryId"" UUID REFERENCES ""Categories""(""Id"") ON DELETE SET NULL,
                ""Status"" INT NOT NULL DEFAULT 0,
                ""DisplayOrder"" INT NOT NULL DEFAULT 0,
                ""IsFeatured"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""ViewCount"" INT NOT NULL DEFAULT 0,
                ""LikeCount"" INT NOT NULL DEFAULT 0,
                ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""CreatedBy"" VARCHAR(100),
                ""UpdatedBy"" VARCHAR(100),
                ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""DeletedAt"" TIMESTAMP,
                ""DeletedBy"" VARCHAR(100)
            );
            
            CREATE UNIQUE INDEX IF NOT EXISTS ""IX_Projects_Slug"" ON ""Projects""(""Slug"") WHERE NOT ""IsDeleted"";
        ");
    }

    private static async Task CreateTagsTableAsync(NpgsqlConnection conn)
    {
        await conn.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS ""Tags"" (
                ""Id"" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                ""Name"" VARCHAR(100) NOT NULL,
                ""Slug"" VARCHAR(100) NOT NULL,
                ""Color"" VARCHAR(20) NOT NULL DEFAULT '',
                ""UsageCount"" INT NOT NULL DEFAULT 0,
                ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""CreatedBy"" VARCHAR(100),
                ""UpdatedBy"" VARCHAR(100),
                ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""DeletedAt"" TIMESTAMP,
                ""DeletedBy"" VARCHAR(100)
            );
        ");
    }

    private static async Task CreateBlogPostsTableAsync(NpgsqlConnection conn)
    {
        await conn.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS ""BlogPosts"" (
                ""Id"" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                ""Title"" VARCHAR(300) NOT NULL,
                ""Content"" TEXT NOT NULL DEFAULT '',
                ""Summary"" VARCHAR(1000) NOT NULL DEFAULT '',
                ""Slug"" VARCHAR(200) NOT NULL,
                ""MetaTitle"" VARCHAR(200) NOT NULL DEFAULT '',
                ""MetaDescription"" VARCHAR(500) NOT NULL DEFAULT '',
                ""FeaturedImageUrl"" VARCHAR(500) NOT NULL DEFAULT '',
                ""Status"" INT NOT NULL DEFAULT 0,
                ""PublishedAt"" TIMESTAMP,
                ""ScheduledAt"" TIMESTAMP,
                ""ViewCount"" INT NOT NULL DEFAULT 0,
                ""ReadingTime"" INT NOT NULL DEFAULT 0,
                ""IsFeatured"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""CategoryId"" UUID REFERENCES ""Categories""(""Id"") ON DELETE SET NULL,
                ""AuthorId"" VARCHAR(128) REFERENCES ""ApplicationUsers""(""Id"") ON DELETE SET NULL,
                ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""CreatedBy"" VARCHAR(100),
                ""UpdatedBy"" VARCHAR(100),
                ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""DeletedAt"" TIMESTAMP,
                ""DeletedBy"" VARCHAR(100)
            );
            
            CREATE UNIQUE INDEX IF NOT EXISTS ""IX_BlogPosts_Slug"" ON ""BlogPosts""(""Slug"") WHERE NOT ""IsDeleted"";
        ");
    }

    private static async Task CreateBlogPostTagsTableAsync(NpgsqlConnection conn)
    {
        await conn.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS ""BlogPostTags"" (
                ""BlogPostsId"" UUID NOT NULL REFERENCES ""BlogPosts""(""Id"") ON DELETE CASCADE,
                ""TagsId"" UUID NOT NULL REFERENCES ""Tags""(""Id"") ON DELETE CASCADE,
                PRIMARY KEY (""BlogPostsId"", ""TagsId"")
            );
        ");
    }

    private static async Task CreateProjectTagsTableAsync(NpgsqlConnection conn)
    {
        await conn.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS ""ProjectTags"" (
                ""ProjectsId"" UUID NOT NULL REFERENCES ""Projects""(""Id"") ON DELETE CASCADE,
                ""TagsId"" UUID NOT NULL REFERENCES ""Tags""(""Id"") ON DELETE CASCADE,
                PRIMARY KEY (""ProjectsId"", ""TagsId"")
            );
        ");
    }

    private static async Task CreateCommentsTableAsync(NpgsqlConnection conn)
    {
        await conn.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS ""Comments"" (
                ""Id"" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                ""Content"" TEXT NOT NULL DEFAULT '',
                ""AuthorName"" VARCHAR(100),
                ""AuthorEmail"" VARCHAR(100),
                ""AuthorWebsite"" VARCHAR(500),
                ""IsApproved"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""BlogPostId"" UUID NOT NULL REFERENCES ""BlogPosts""(""Id"") ON DELETE CASCADE,
                ""UserId"" VARCHAR(128) REFERENCES ""ApplicationUsers""(""Id"") ON DELETE SET NULL,
                ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""CreatedBy"" VARCHAR(100),
                ""UpdatedBy"" VARCHAR(100),
                ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""DeletedAt"" TIMESTAMP,
                ""DeletedBy"" VARCHAR(100)
            );
        ");
    }

    private static async Task CreateEventsTableAsync(NpgsqlConnection conn)
    {
        await conn.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS ""Events"" (
                ""Id"" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                ""Title"" VARCHAR(200) NOT NULL,
                ""Description"" TEXT NOT NULL DEFAULT '',
                ""EventDate"" TIMESTAMP NOT NULL,
                ""EventEndDate"" TIMESTAMP,
                ""Location"" VARCHAR(200) NOT NULL DEFAULT '',
                ""EventUrl"" VARCHAR(500),
                ""Status"" INT NOT NULL DEFAULT 0,
                ""MaxAttendees"" INT,
                ""RegisteredCount"" INT NOT NULL DEFAULT 0,
                ""IsFeatured"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""DisplayOrder"" INT NOT NULL DEFAULT 0,
                ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""CreatedBy"" VARCHAR(100),
                ""UpdatedBy"" VARCHAR(100),
                ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""DeletedAt"" TIMESTAMP,
                ""DeletedBy"" VARCHAR(100)
            );
        ");
    }

    private static async Task CreateNewsletterSubscribersTableAsync(NpgsqlConnection conn)
    {
        await conn.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS ""NewsletterSubscribers"" (
                ""Id"" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                ""Email"" VARCHAR(200) NOT NULL,
                ""Name"" VARCHAR(100),
                ""IsActive"" BOOLEAN NOT NULL DEFAULT TRUE,
                ""IsConfirmed"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""ConfirmationToken"" VARCHAR(500),
                ""ConfirmedAt"" TIMESTAMP,
                ""UnsubscribedAt"" TIMESTAMP,
                ""UnsubscribeToken"" VARCHAR(500),
                ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""CreatedBy"" VARCHAR(100),
                ""UpdatedBy"" VARCHAR(100),
                ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""DeletedAt"" TIMESTAMP,
                ""DeletedBy"" VARCHAR(100)
            );
            
            CREATE UNIQUE INDEX IF NOT EXISTS ""IX_NewsletterSubscribers_Email"" ON ""NewsletterSubscribers""(""Email"");
        ");
    }

    private static async Task CreateTestimonialsTableAsync(NpgsqlConnection conn)
    {
        await conn.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS ""Testimonials"" (
                ""Id"" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                ""AuthorName"" VARCHAR(100) NOT NULL,
                ""Position"" VARCHAR(200) NOT NULL DEFAULT '',
                ""Company"" VARCHAR(200),
                ""Content"" TEXT NOT NULL DEFAULT '',
                ""Rating"" INT NOT NULL DEFAULT 5,
                ""ImageUrl"" VARCHAR(500),
                ""IsFeatured"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""DisplayOrder"" INT NOT NULL DEFAULT 0,
                ""IsApproved"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""CreatedBy"" VARCHAR(100),
                ""UpdatedBy"" VARCHAR(100),
                ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""DeletedAt"" TIMESTAMP,
                ""DeletedBy"" VARCHAR(100)
            );
        ");
    }

    private static async Task CreateContactMessagesTableAsync(NpgsqlConnection conn)
    {
        await conn.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS ""ContactMessages"" (
                ""Id"" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                ""Name"" VARCHAR(200) NOT NULL,
                ""Email"" VARCHAR(200) NOT NULL,
                ""Subject"" VARCHAR(200) NOT NULL DEFAULT '',
                ""Message"" VARCHAR(2000) NOT NULL DEFAULT '',
                ""IsRead"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""IsReplied"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""ReplyMessage"" TEXT,
                ""RepliedAt"" TIMESTAMP,
                ""UserId"" VARCHAR(128) REFERENCES ""ApplicationUsers""(""Id"") ON DELETE SET NULL,
                ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
                ""CreatedBy"" VARCHAR(100),
                ""UpdatedBy"" VARCHAR(100),
                ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE,
                ""DeletedAt"" TIMESTAMP,
                ""DeletedBy"" VARCHAR(100)
            );
        ");
    }
}
