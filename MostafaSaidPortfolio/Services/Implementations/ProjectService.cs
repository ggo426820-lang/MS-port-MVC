using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MostafaSaidPortfolio.Models;
using MostafaSaidPortfolio.Services.Interfaces;
using MostafaSaidPortfolio.Data;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Set<Project>().ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Set<Project>().FindAsync(id);
        }

        public async Task<Project> AddAsync(Project entity)
        {
            _context.Set<Project>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Project> UpdateAsync(Project entity)
        {
            _context.Set<Project>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _context.Set<Project>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
