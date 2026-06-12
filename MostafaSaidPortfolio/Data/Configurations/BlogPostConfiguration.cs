using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Data.Configurations;

/// <summary>
/// Entity Framework configuration for BlogPost
/// </summary>
public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .HasMaxLength(300)
            .IsRequired();
        
        builder.Property(x => x.Content)
            .HasColumnType("text");
        
        builder.Property(x => x.Summary)
            .HasMaxLength(1000);
        
        builder.Property(x => x.Slug)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.MetaTitle)
            .HasMaxLength(200);
        
        builder.Property(x => x.MetaDescription)
            .HasMaxLength(500);
        
        builder.Property(x => x.FeaturedImageUrl)
            .HasMaxLength(500);
        
        builder.Property(x => x.Status)
            .HasConversion<int>();
        
        builder.HasOne(x => x.Category)
            .WithMany(x => x.BlogPosts)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(x => x.Author)
            .WithMany()
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(x => x.Comments)
            .WithOne(x => x.BlogPost)
            .HasForeignKey(x => x.BlogPostId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
