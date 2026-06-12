namespace MostafaSaidPortfolio.Domain.ViewModels.Blog;

/// <summary>
/// Blog post view model
/// </summary>
public class BlogPostViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string FeaturedImageUrl { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public int ViewCount { get; set; }
    public int ReadingTime { get; set; }
    public bool IsFeatured { get; set; }
    public DateTime PublishedAt { get; set; }
    public List<string> Tags { get; set; } = new();
    public int CommentCount { get; set; }
}
