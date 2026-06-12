using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllActiveAsync();
        Task<IEnumerable<Project>> GetFeaturedAsync(int count = 3);
        Task<Project?> GetByIdAsync(Guid id);
        Task<Project?> GetBySlugAsync(string slug);
        Task<IEnumerable<Project>> GetByCategoryAsync(Guid categoryId);
        Task<IEnumerable<Project>> SearchAsync(string query);
        Task<(IEnumerable<Project> Items, int TotalCount)> GetPagedAsync(
            string? search, Guid? categoryId, string sort, int page, int pageSize);

        Task<Project> AddAsync(Project entity);
        Task<Project> UpdateAsync(Project entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
