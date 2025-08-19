using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MostafaSaidPortfolio.Models;
using MostafaSaidPortfolio.Services.Interfaces;
using MostafaSaidPortfolio.Data; // ✅ This is required for AppDbContext

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class TestimonialService : ITestimonialService
    {
        private readonly ApplicationDbContext _context;

        public TestimonialService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all testimonials
        public async Task<IEnumerable<Testimonial>> GetAllAsync()
        {
            return await _context.Set<Testimonial>().ToListAsync();
        }

        // Get testimonial by Id
        public async Task<Testimonial?> GetByIdAsync(int id)
        {
            return await _context.Set<Testimonial>().FindAsync(id);
        }

        // Add new testimonial
        public async Task<Testimonial> AddAsync(Testimonial entity)
        {
            _context.Set<Testimonial>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Update testimonial
        public async Task<Testimonial> UpdateAsync(Testimonial entity)
        {
            _context.Set<Testimonial>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Delete testimonial
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _context.Set<Testimonial>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
