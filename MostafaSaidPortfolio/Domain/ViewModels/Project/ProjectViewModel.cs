namespace MostafaSaidPortfolio.Domain.ViewModels.Project;

/// <summary>
/// Project view model
/// </summary>
public class ProjectViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public string LiveUrl { get; set; } = string.Empty;
    public string GitHubUrl { get; set; } = string.Empty;
    public string[] Technologies { get; set; } = Array.Empty<string>();
    public string CategoryName { get; set; } = string.Empty;
    public bool IsFeatured { get; set; }
    public int ViewCount { get; set; }
    public int LikeCount { get; set; }
    public List<string> Tags { get; set; } = new();
}
