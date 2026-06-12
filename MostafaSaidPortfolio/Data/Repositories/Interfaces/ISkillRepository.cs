using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Data.Repositories.Interfaces
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task<IEnumerable<Skill>> GetActiveAsync();
        Task<IEnumerable<Skill>> GetByCategoryAsync(string category);
        Task<IEnumerable<string>> GetCategoriesAsync();
    }
}
