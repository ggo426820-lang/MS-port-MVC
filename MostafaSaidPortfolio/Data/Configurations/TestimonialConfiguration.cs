using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Data.Configurations;

/// <summary>
/// Entity Framework configuration for Testimonial
/// </summary>
public class TestimonialConfiguration : IEntityTypeConfiguration<Testimonial>
{
    public void Configure(EntityTypeBuilder<Testimonial> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.AuthorName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Position)
            .HasMaxLength(200);
        
        builder.Property(x => x.Company)
            .HasMaxLength(200);
        
        builder.Property(x => x.Content)
            .HasColumnType("text");
        
        builder.Property(x => x.ImageUrl)
            .HasMaxLength(500);
    }
}
