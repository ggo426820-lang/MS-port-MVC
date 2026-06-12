namespace MostafaSaidPortfolio.Domain.ViewModels.Common;

/// <summary>
/// Generic paged result wrapper
/// </summary>
public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 9;
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((decimal)TotalCount / PageSize) : 0;
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
    public int FirstItem => TotalCount == 0 ? 0 : (PageNumber - 1) * PageSize + 1;
    public int LastItem => Math.Min(PageNumber * PageSize, TotalCount);
}
