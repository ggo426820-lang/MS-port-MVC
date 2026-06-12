using MostafaSaidPortfolio.Data.UnitOfWork;
using MostafaSaidPortfolio.Models;
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

        public Task<Project?> GetByIdAsync(int id) =>
            _uow.Projects.GetByIdAsync(id);

        public Task<IEnumerable<Project>> GetByCategoryAsync(int categoryId) =>
            _uow.Projects.GetByCategoryAsync(categoryId);

        public Task<IEnumerable<Project>> SearchAsync(string query) =>
            _uow.Projects.SearchAsync(query);

        public async Task<Project> AddAsync(Project entity)
        {
            entity.Id = await _uow.Projects.AddAsync(entity);
            return entity;
        }

        public async Task<Project> UpdateAsync(Project entity)
        {
            await _uow.Projects.UpdateAsync(entity);
            return entity;
        }

        public Task<bool> DeleteAsync(int id) =>
            _uow.Projects.DeleteAsync(id);
    }
}
