using MostafaSaidPortfolio.Domain.Common;
using MostafaSaidPortfolio.Domain.Enums;

namespace MostafaSaidPortfolio.Domain.Entities;

/// <summary>
/// Blog post entity
/// </summary>
public class BlogPost : BaseEntity
{
    /// <summary>
    /// Blog post title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Blog post content
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Short summary/excerpt
    /// </summary>
    public string Summary { get; set; } = string.Empty;

    /// <summary>
    /// URL-friendly slug
    /// </summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// SEO meta title
    /// </summary>
    public string MetaTitle { get; set; } = string.Empty;

    /// <summary>
    /// SEO meta description
    /// </summary>
    public string MetaDescription { get; set; } = string.Empty;

    /// <summary>
    /// Featured image URL
    /// </summary>
    public string FeaturedImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// Blog post status
    /// </summary>
    public BlogPostStatus Status { get; set; } = BlogPostStatus.Draft;

    /// <summary>
    /// Publication date
    /// </summary>
    public DateTime? PublishedAt { get; set; }

    /// <summary>
    /// Scheduled publication date
    /// </summary>
    public DateTime? ScheduledAt { get; set; }

    /// <summary>
    /// View count
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// Reading time in minutes (auto-calculated)
    /// </summary>
    public int ReadingTime { get; set; }

    /// <summary>
    /// Is this a featured post
    /// </summary>
    public bool IsFeatured { get; set; }

    /// <summary>
    /// Category ID
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// Author/User ID
    /// </summary>
    public string? AuthorId { get; set; }

    // Navigation properties
    public Category? Category { get; set; }
    public ApplicationUser? Author { get; set; }
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    /// <summary>
    /// Computed category name (populated by repositories via JOIN queries)
    /// </summary>
    public string? CategoryName { get; set; }

    /// <summary>
    /// Computed author name (populated by repositories via JOIN queries)
    /// </summary>
    public string? AuthorName { get; set; }

    /// <summary>
    /// Estimated reading time (alias for ReadingTime for view compatibility)
    /// </summary>
    public int EstimatedReadingTime => ReadingTime > 0 ? ReadingTime : CalculateReadingTime();

    /// <summary>
    /// Calculate estimated reading time (200 words per minute)
    /// </summary>
    public int CalculateReadingTime()
    {
        var wordCount = Content.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
        return Math.Max(1, wordCount / 200);
    }

    /// <summary>
    /// Is blog post published
    /// </summary>
    public bool IsPublished => Status == BlogPostStatus.Published && PublishedAt.HasValue;

    /// <summary>
    /// Publish the blog post
    /// </summary>
    public void Publish(string? publishedBy = null)
    {
        Status = BlogPostStatus.Published;
        PublishedAt = DateTime.UtcNow;
        ScheduledAt = null;
        UpdatedBy = publishedBy;
        MarkAsUpdated(publishedBy);
    }

    /// <summary>
    /// Unpublish the blog post
    /// </summary>
    public void Unpublish(string? unPublishedBy = null)
    {
        Status = BlogPostStatus.Draft;
        PublishedAt = null;
        UpdatedBy = unPublishedBy;
        MarkAsUpdated(unPublishedBy);
    }

    /// <summary>
    /// Schedule blog post for publishing
    /// </summary>
    public void Schedule(DateTime scheduledDate, string? scheduledBy = null)
    {
        Status = BlogPostStatus.Scheduled;
        ScheduledAt = scheduledDate;
        UpdatedBy = scheduledBy;
        MarkAsUpdated(scheduledBy);
    }
}
