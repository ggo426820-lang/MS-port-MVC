using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MostafaSaidPortfolio.Data.Configurations;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Data
{
    /// <summary>
    /// Entity Framework Core database context
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet declarations
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<NewsletterSubscriber> NewsletterSubscribers { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply all entity configurations
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new TagConfiguration());
            builder.ApplyConfiguration(new BlogPostConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new ProjectConfiguration());
            builder.ApplyConfiguration(new TestimonialConfiguration());
            builder.ApplyConfiguration(new EventConfiguration());
            builder.ApplyConfiguration(new NewsletterSubscriberConfiguration());
            builder.ApplyConfiguration(new ContactMessageConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
        }
    }
}

