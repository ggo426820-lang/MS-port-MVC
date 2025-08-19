using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MostafaSaidPortfolio.Models;

namespace MostafaSaidPortfolio.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Project> Projects { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<NewsletterSubscriber> NewsletterSubscribers { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Project configuration
            builder.Entity<Project>().HasIndex(p => p.Title).IsUnique();

            // BlogPost configuration
            builder.Entity<BlogPost>()
                   .HasOne(b => b.Category)
                   .WithMany(c => c.BlogPosts)
                   .HasForeignKey(b => b.CategoryId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<BlogPost>()
                   .HasMany(b => b.Tags)
                   .WithMany(t => t.BlogPosts);

            // Event configuration
            builder.Entity<Event>().Property(e => e.Date).IsRequired();
            builder.Entity<Event>().Property(e => e.EndDate).IsRequired();

            // Newsletter configuration
            builder.Entity<NewsletterSubscriber>().HasIndex(n => n.Email).IsUnique();

            // Testimonial configuration
            builder.Entity<Testimonial>()
                   .Property(t => t.Author)
                   .HasMaxLength(100)
                   .IsRequired();

            // ContactMessage configuration
            builder.Entity<ContactMessage>().Property(c => c.Email).IsRequired();
            builder.Entity<ContactMessage>().Property(c => c.Message).IsRequired().HasMaxLength(1000);

            // Seed initial data
            DataSeeder.Seed(builder);
        }
    }
}
