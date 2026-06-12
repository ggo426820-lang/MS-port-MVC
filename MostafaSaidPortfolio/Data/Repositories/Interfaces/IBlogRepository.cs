using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Data.Repositories.Interfaces
{
    public interface IBlogRepository : IRepository<BlogPost>
    {
        Task<IEnumerable<BlogPost>> GetPublishedAsync();
        Task<IEnumerable<BlogPost>> GetFeaturedAsync(int count = 3);
        Task<IEnumerable<BlogPost>> GetRecentAsync(int count = 5);
        Task<BlogPost?> GetBySlugAsync(string slug);
        Task<IEnumerable<BlogPost>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<BlogPost>> SearchAsync(string query);
        Task<bool> IncrementViewCountAsync(Guid id);
        Task<int> CountPublishedAsync();
    }
}
