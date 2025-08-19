using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MostafaSaidPortfolio.Data; // ✅ this fixes AppDbContext reference
using MostafaSaidPortfolio.Models;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly ApplicationDbContext _context;

        public BlogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.Set<BlogPost>().ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(int id)
        {
            return await _context.Set<BlogPost>().FindAsync(id);
        }

        public async Task<BlogPost> AddAsync(BlogPost entity)
        {
            _context.Set<BlogPost>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<BlogPost> UpdateAsync(BlogPost entity)
        {
            _context.Set<BlogPost>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _context.Set<BlogPost>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
