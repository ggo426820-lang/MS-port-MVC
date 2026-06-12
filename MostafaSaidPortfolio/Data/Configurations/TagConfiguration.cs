using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Data.Configurations;

/// <summary>
/// Entity Framework configuration for Tag
/// </summary>
public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Slug)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Color)
            .HasMaxLength(20);
        
        builder.HasMany(x => x.BlogPosts)
            .WithMany(x => x.Tags)
            .UsingEntity("BlogPostTags");
        
        builder.HasMany(x => x.Projects)
            .WithMany(x => x.Tags)
            .UsingEntity("ProjectTags");
    }
}
