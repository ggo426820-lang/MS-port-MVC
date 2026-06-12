namespace MostafaSaidPortfolio.Domain.ViewModels.Common;

/// <summary>
/// Generic pagination view model
/// </summary>
public class PaginationViewModel
{
    /// <summary>
    /// Current page number
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Items per page
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Total item count
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Total page count
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((decimal)TotalCount / PageSize);

    /// <summary>
    /// Has previous page
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// Has next page
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;
}
