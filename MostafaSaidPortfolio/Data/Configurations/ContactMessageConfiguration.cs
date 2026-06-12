using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Data.Configurations;

/// <summary>
/// Entity Framework configuration for ContactMessage
/// </summary>
public class ContactMessageConfiguration : IEntityTypeConfiguration<ContactMessage>
{
    public void Configure(EntityTypeBuilder<ContactMessage> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.Email)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.Subject)
            .HasMaxLength(200);
        
        builder.Property(x => x.Message)
            .HasMaxLength(2000);
        
        builder.Property(x => x.ReplyMessage)
            .HasColumnType("text");
        
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
