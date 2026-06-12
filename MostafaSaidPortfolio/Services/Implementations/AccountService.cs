using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class AccountService : IAccountService
    {
        public Task<IEnumerable<ApplicationUser>> GetAllAsync() =>
            Task.FromResult<IEnumerable<ApplicationUser>>(Array.Empty<ApplicationUser>());

        public Task<ApplicationUser?> GetByIdAsync(string id) =>
            Task.FromResult<ApplicationUser?>(null);

        public Task<ApplicationUser> AddAsync(ApplicationUser entity) =>
            Task.FromResult(entity);

        public Task<ApplicationUser> UpdateAsync(ApplicationUser entity) =>
            Task.FromResult(entity);

        public Task<bool> DeleteAsync(string id) =>
            Task.FromResult(false);
    }
}
