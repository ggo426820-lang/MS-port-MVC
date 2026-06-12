using MostafaSaidPortfolio.Models;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class AccountService : IAccountService
    {
        public Task<IEnumerable<Account>> GetAllAsync() =>
            Task.FromResult<IEnumerable<Account>>(Array.Empty<Account>());

        public Task<Account?> GetByIdAsync(int id) =>
            Task.FromResult<Account?>(null);

        public Task<Account> AddAsync(Account entity) =>
            Task.FromResult(entity);

        public Task<Account> UpdateAsync(Account entity) =>
            Task.FromResult(entity);

        public Task<bool> DeleteAsync(int id) =>
            Task.FromResult(false);
    }
}
