using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Data.Configurations;

/// <summary>
/// Entity Framework configuration for Category
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Slug)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnType("text");
        
        builder.Property(x => x.Icon)
            .HasMaxLength(100);
        
        builder.Property(x => x.Color)
            .HasMaxLength(20);
        
        builder.Property(x => x.BackgroundColor)
            .HasMaxLength(20);
        
        builder.HasMany(x => x.BlogPosts)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(x => x.Projects)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(x => x.SubCategories)
            .WithOne(x => x.ParentCategory)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
