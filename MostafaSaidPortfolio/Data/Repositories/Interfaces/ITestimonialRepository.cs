using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Data.Repositories.Interfaces
{
    public interface ITestimonialRepository : IRepository<Testimonial>
    {
        Task<IEnumerable<Testimonial>> GetApprovedAsync();
        Task<bool> ApproveAsync(Guid id);
        Task<bool> RejectAsync(Guid id);
    }
}
