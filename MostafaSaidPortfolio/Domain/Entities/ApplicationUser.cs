using Microsoft.AspNetCore.Identity;

namespace MostafaSaidPortfolio.Domain.Entities;

/// <summary>
/// Application user entity extending ASP.NET Identity
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// User full name
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// Display name (alias for FullName for view compatibility)
    /// </summary>
    public string? Name => FullName ?? UserName;

    /// <summary>
    /// User bio/about section
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// Profile image URL
    /// </summary>
    public string? ProfileImageUrl { get; set; }

    /// <summary>
    /// Account creation date
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Last update date
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Is account active
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<ContactMessage> ContactMessages { get; set; } = new List<ContactMessage>();
}
