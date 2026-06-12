using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Domain.ViewModels.Common;

namespace MostafaSaidPortfolio.Domain.ViewModels.Projects;

/// <summary>
/// View model for the projects index page with search, filter, sort, and pagination
/// </summary>
public class ProjectListViewModel
{
    public PagedResult<MostafaSaidPortfolio.Domain.Entities.Project> Projects { get; set; } = new();
    public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();

    public string? Search { get; set; }
    public string? CategorySlug { get; set; }
    public Guid? CategoryId { get; set; }
    public string Sort { get; set; } = "newest";
    public int Page { get; set; } = 1;

    public bool HasFilters => !string.IsNullOrWhiteSpace(Search) || CategoryId.HasValue;
}
