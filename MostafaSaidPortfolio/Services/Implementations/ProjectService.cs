using MostafaSaidPortfolio.Data.UnitOfWork;
using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _uow;

        public ProjectService(IUnitOfWork uow) => _uow = uow;

        public Task<IEnumerable<Project>> GetAllActiveAsync() =>
            _uow.Projects.GetActiveAsync();

        public Task<IEnumerable<Project>> GetFeaturedAsync(int count = 3) =>
            _uow.Projects.GetFeaturedAsync(count);

        public Task<Project?> GetByIdAsync(Guid id) =>
            _uow.Projects.GetByIdAsync(id);

        public Task<Project?> GetBySlugAsync(string slug) =>
            _uow.Projects.GetBySlugAsync(slug);

        public Task<IEnumerable<Project>> GetByCategoryAsync(Guid categoryId) =>
            _uow.Projects.GetByCategoryAsync(categoryId);

        public Task<IEnumerable<Project>> SearchAsync(string query) =>
            _uow.Projects.SearchAsync(query);

        public Task<(IEnumerable<Project> Items, int TotalCount)> GetPagedAsync(
            string? search, Guid? categoryId, string sort, int page, int pageSize) =>
            _uow.Projects.GetPagedAsync(search, categoryId, sort, page, pageSize);

        public async Task<Project> AddAsync(Project entity)
        {
            await _uow.Projects.AddAsync(entity);
            return entity;
        }

        public async Task<Project> UpdateAsync(Project entity)
        {
            await _uow.Projects.UpdateAsync(entity);
            return entity;
        }

        public Task<bool> DeleteAsync(Guid id) =>
            _uow.Projects.DeleteAsync(id);
    }
}
