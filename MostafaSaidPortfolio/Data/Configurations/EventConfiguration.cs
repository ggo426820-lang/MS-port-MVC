using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Data.Configurations;

/// <summary>
/// Entity Framework configuration for Event
/// </summary>
public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnType("text");
        
        builder.Property(x => x.Location)
            .HasMaxLength(200);
        
        builder.Property(x => x.EventUrl)
            .HasMaxLength(500);
        
        builder.Property(x => x.Status)
            .HasConversion<int>();
    }
}
