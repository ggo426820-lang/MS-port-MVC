using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Data.Configurations;

/// <summary>
/// Entity Framework configuration for ApplicationUser
/// </summary>
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.FullName)
            .HasMaxLength(100);
        
        builder.Property(x => x.Bio)
            .HasColumnType("text");
        
        builder.Property(x => x.ProfileImageUrl)
            .HasMaxLength(500);
    }
}
