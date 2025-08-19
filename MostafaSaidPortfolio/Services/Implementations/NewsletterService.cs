using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MostafaSaidPortfolio.Models;
using MostafaSaidPortfolio.Services.Interfaces;
using MostafaSaidPortfolio.Data;


namespace MostafaSaidPortfolio.Services.Implementations
{
    public class NewsletterService : INewsletterService
    {
        private readonly ApplicationDbContext _context;

        public NewsletterService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NewsletterSubscriber>> GetAllAsync()
        {
            return await _context.Set<NewsletterSubscriber>().ToListAsync();
        }

        public async Task<NewsletterSubscriber?> GetByIdAsync(int id)
        {
            return await _context.Set<NewsletterSubscriber>().FindAsync(id);
        }

        public async Task<NewsletterSubscriber> AddAsync(NewsletterSubscriber entity)
        {
            _context.Set<NewsletterSubscriber>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<NewsletterSubscriber> UpdateAsync(NewsletterSubscriber entity)
        {
            _context.Set<NewsletterSubscriber>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _context.Set<NewsletterSubscriber>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
