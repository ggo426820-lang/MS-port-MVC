using MostafaSaidPortfolio.Data.UnitOfWork;
using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Domain.Enums;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _uow;

        public BlogService(IUnitOfWork uow) => _uow = uow;

        public Task<IEnumerable<BlogPost>> GetAllPublishedAsync() =>
            _uow.Blogs.GetPublishedAsync();

        public Task<IEnumerable<BlogPost>> GetFeaturedAsync(int count = 3) =>
            _uow.Blogs.GetFeaturedAsync(count);

        public Task<IEnumerable<BlogPost>> GetRecentAsync(int count = 5) =>
            _uow.Blogs.GetRecentAsync(count);

        public Task<BlogPost?> GetByIdAsync(int id) =>
            _uow.Blogs.GetByIdAsync(id);

        public Task<BlogPost?> GetBySlugAsync(string slug) =>
            _uow.Blogs.GetBySlugAsync(slug);

        public Task<IEnumerable<BlogPost>> GetByCategoryAsync(int categoryId) =>
            _uow.Blogs.GetByCategoryAsync(categoryId);

        public Task<IEnumerable<BlogPost>> SearchAsync(string query) =>
            _uow.Blogs.SearchAsync(query);

        public Task IncrementViewCountAsync(int id) =>
            _uow.Blogs.IncrementViewCountAsync(id);

        public async Task<BlogPost> AddAsync(BlogPost entity)
        {
            entity.Id = await _uow.Blogs.AddAsync(entity);
            return entity;
        }

        public async Task<BlogPost> UpdateAsync(BlogPost entity)
        {
            await _uow.Blogs.UpdateAsync(entity);
            return entity;
        }

        public Task<bool> DeleteAsync(int id) =>
            _uow.Blogs.DeleteAsync(id);
    }
}

