using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Domain.Enums;

namespace MostafaSaidPortfolio.Services.Interfaces
{
    public interface ITestimonialService
    {
        Task<IEnumerable<Testimonial>> GetApprovedAsync();
        Task<IEnumerable<Testimonial>> GetAllAsync();
        Task<Testimonial?> GetByIdAsync(int id);
        Task<Testimonial> AddAsync(Testimonial entity);
        Task<bool> DeleteAsync(int id);
    }
}

