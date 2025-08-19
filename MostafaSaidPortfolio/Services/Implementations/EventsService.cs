using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MostafaSaidPortfolio.Models;
using MostafaSaidPortfolio.Services.Interfaces;
using MostafaSaidPortfolio.Data;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class EventsService : IEventsService
    {
        private readonly ApplicationDbContext _context;

        public EventsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Set<Event>().ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Set<Event>().FindAsync(id);
        }

        public async Task<Event> AddAsync(Event entity)
        {
            _context.Set<Event>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Event> UpdateAsync(Event entity)
        {
            _context.Set<Event>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _context.Set<Event>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
