using MostafaSaidPortfolio.Domain.Common;
using MostafaSaidPortfolio.Domain.Enums;

namespace MostafaSaidPortfolio.Domain.Entities;

/// <summary>
/// Portfolio project entity
/// </summary>
public class Project : BaseEntity
{
    /// <summary>
    /// Project title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Short project description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Detailed project description
    /// </summary>
    public string LongDescription { get; set; } = string.Empty;

    /// <summary>
    /// URL-friendly slug
    /// </summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// Comma-separated technology stack
    /// </summary>
    public string TechnologyStack { get; set; } = string.Empty;

    /// <summary>
    /// GitHub repository URL
    /// </summary>
    public string GitHubUrl { get; set; } = string.Empty;

    /// <summary>
    /// Live project URL
    /// </summary>
    public string LiveUrl { get; set; } = string.Empty;

    /// <summary>
    /// Main project image URL
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// Thumbnail image URL
    /// </summary>
    public string ThumbnailUrl { get; set; } = string.Empty;

    /// <summary>
    /// Project status
    /// </summary>
    public ProjectStatus Status { get; set; } = ProjectStatus.Pending;

    /// <summary>
    /// Display order in UI
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// Is this a featured project
    /// </summary>
    public bool IsFeatured { get; set; }

    /// <summary>
    /// View count
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// Like/Favorite count
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// Category ID
    /// </summary>
    public Guid? CategoryId { get; set; }

    // Navigation properties
    public Category? Category { get; set; }
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();

    /// <summary>
    /// Computed category name (populated by repositories via JOIN queries)
    /// </summary>
    public string? CategoryName { get; set; }

    /// <summary>
    /// Technology stack as array (computed from TechnologyStack string)
    /// </summary>
    public string[] TechStackList =>
        string.IsNullOrWhiteSpace(TechnologyStack)
            ? Array.Empty<string>()
            : TechnologyStack.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    /// <summary>
    /// Get technology stack as array
    /// </summary>
    public string[] GetTechStackList() => TechStackList;
}
