using System.Collections.Generic;
using System.Threading.Tasks;
using MostafaSaidPortfolio.Models;

namespace MostafaSaidPortfolio.Services.Interfaces
{
    public interface IBlogService
    {
        // Get all blog posts
        Task<IEnumerable<BlogPost>> GetAllAsync();

        // Get blog post by Id
        Task<BlogPost?> GetByIdAsync(int id);

        // Add a new blog post
        Task<BlogPost> AddAsync(BlogPost entity);

        // Update an existing blog post
        Task<BlogPost> UpdateAsync(BlogPost entity);

        // Delete blog post by Id
        Task<bool> DeleteAsync(int id);
    }
}
