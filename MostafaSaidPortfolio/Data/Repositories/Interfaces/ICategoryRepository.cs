using MostafaSaidPortfolio.Models;

namespace MostafaSaidPortfolio.Data.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetActiveAsync();
        Task<Category?> GetBySlugAsync(string slug);
    }
}
