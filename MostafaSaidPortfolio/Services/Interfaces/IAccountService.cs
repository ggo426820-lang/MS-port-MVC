using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser?> GetByIdAsync(string id);
        Task<ApplicationUser> AddAsync(ApplicationUser entity);
        Task<ApplicationUser> UpdateAsync(ApplicationUser entity);
        Task<bool> DeleteAsync(string id);
    }
}
