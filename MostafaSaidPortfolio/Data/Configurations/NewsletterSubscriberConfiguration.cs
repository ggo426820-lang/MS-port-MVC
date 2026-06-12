using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Data.Configurations;

/// <summary>
/// Entity Framework configuration for NewsletterSubscriber
/// </summary>
public class NewsletterSubscriberConfiguration : IEntityTypeConfiguration<NewsletterSubscriber>
{
    public void Configure(EntityTypeBuilder<NewsletterSubscriber> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Email)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.Name)
            .HasMaxLength(100);
        
        builder.Property(x => x.ConfirmationToken)
            .HasMaxLength(500);
        
        builder.Property(x => x.UnsubscribeToken)
            .HasMaxLength(500);
        
        builder.HasIndex(x => x.Email)
            .IsUnique();
    }
}
