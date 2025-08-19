using System.Collections.Generic;
using System.Threading.Tasks;
using MostafaSaidPortfolio.Models;

namespace MostafaSaidPortfolio.Services.Interfaces
{
    public interface ITestimonialService
    {
        // Get all testimonials
        Task<IEnumerable<Testimonial>> GetAllAsync();

        // Get testimonial by Id (int is fine here since Testimonial.Id is usually an int)
        Task<Testimonial?> GetByIdAsync(int id);

        // Add a new testimonial
        Task<Testimonial> AddAsync(Testimonial entity);

        // Update an existing testimonial
        Task<Testimonial> UpdateAsync(Testimonial entity);

        // Delete testimonial by Id
        Task<bool> DeleteAsync(int id);
    }
}
