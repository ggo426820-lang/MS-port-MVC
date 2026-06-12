using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Data.Repositories.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetActiveAsync();
        Task<IEnumerable<Project>> GetFeaturedAsync(int count = 3);
        Task<Project?> GetBySlugAsync(string slug);
        Task<IEnumerable<Project>> GetByCategoryAsync(Guid categoryId);
        Task<IEnumerable<Project>> SearchAsync(string query);
        Task<int> CountActiveAsync();
        Task<(IEnumerable<Project> Items, int TotalCount)> GetPagedAsync(
            string? search, Guid? categoryId, string sort, int page, int pageSize);
    }
}
