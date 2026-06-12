using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Domain.Enums;

namespace MostafaSaidPortfolio.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogPost>> GetAllPublishedAsync();
        Task<IEnumerable<BlogPost>> GetFeaturedAsync(int count = 3);
        Task<IEnumerable<BlogPost>> GetRecentAsync(int count = 5);
        Task<BlogPost?> GetByIdAsync(int id);
        Task<BlogPost?> GetBySlugAsync(string slug);
        Task<IEnumerable<BlogPost>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<BlogPost>> SearchAsync(string query);
        Task IncrementViewCountAsync(int id);

        Task<BlogPost> AddAsync(BlogPost entity);
        Task<BlogPost> UpdateAsync(BlogPost entity);
        Task<bool> DeleteAsync(int id);
    }
}

