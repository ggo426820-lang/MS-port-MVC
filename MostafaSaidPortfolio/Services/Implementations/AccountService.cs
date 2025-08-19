using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MostafaSaidPortfolio.Models;
using MostafaSaidPortfolio.Services.Interfaces;
using MostafaSaidPortfolio.Data;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Set<Account>().ToListAsync();
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _context.Set<Account>().FindAsync(id);
        }

        public async Task<Account> AddAsync(Account entity)
        {
            _context.Set<Account>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Account> UpdateAsync(Account entity)
        {
            _context.Set<Account>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _context.Set<Account>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
