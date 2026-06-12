using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Data.Configurations;

/// <summary>
/// Entity Framework configuration for Project
/// </summary>
public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasMaxLength(1000);
        
        builder.Property(x => x.LongDescription)
            .HasColumnType("text");
        
        builder.Property(x => x.Slug)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.TechnologyStack)
            .HasMaxLength(1000);
        
        builder.Property(x => x.GitHubUrl)
            .HasMaxLength(500);
        
        builder.Property(x => x.LiveUrl)
            .HasMaxLength(500);
        
        builder.Property(x => x.ImageUrl)
            .HasMaxLength(500);
        
        builder.Property(x => x.ThumbnailUrl)
            .HasMaxLength(500);
        
        builder.Property(x => x.Status)
            .HasConversion<int>();
        
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(x => x.Tags)
            .WithMany(x => x.Projects)
            .UsingEntity("ProjectTags");
    }
}
