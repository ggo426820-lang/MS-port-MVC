using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Data.Configurations;

/// <summary>
/// Entity Framework configuration for Comment
/// </summary>
public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Content)
            .HasColumnType("text");
        
        builder.Property(x => x.AuthorName)
            .HasMaxLength(100);
        
        builder.Property(x => x.AuthorEmail)
            .HasMaxLength(100);
        
        builder.Property(x => x.AuthorWebsite)
            .HasMaxLength(500);
        
        builder.HasOne(x => x.BlogPost)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.BlogPostId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
