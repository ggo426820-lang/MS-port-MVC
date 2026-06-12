using MostafaSaidPortfolio.Models;

namespace MostafaSaidPortfolio.Data.Repositories.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetActiveAsync();
        Task<IEnumerable<Project>> GetFeaturedAsync(int count = 3);
        Task<IEnumerable<Project>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<Project>> SearchAsync(string query);
        Task<int> CountActiveAsync();
    }
}
