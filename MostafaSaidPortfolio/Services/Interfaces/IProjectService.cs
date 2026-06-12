using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllActiveAsync();
        Task<IEnumerable<Project>> GetFeaturedAsync(int count = 3);
        Task<Project?> GetByIdAsync(Guid id);
        Task<IEnumerable<Project>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<Project>> SearchAsync(string query);

        Task<Project> AddAsync(Project entity);
        Task<Project> UpdateAsync(Project entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
