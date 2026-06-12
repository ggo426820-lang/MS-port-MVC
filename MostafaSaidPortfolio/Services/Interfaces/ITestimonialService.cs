using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Services.Interfaces
{
    public interface ITestimonialService
    {
        Task<IEnumerable<Testimonial>> GetApprovedAsync();
        Task<IEnumerable<Testimonial>> GetAllAsync();
        Task<Testimonial?> GetByIdAsync(Guid id);
        Task<Testimonial> AddAsync(Testimonial entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
