using MostafaSaidPortfolio.Models;

namespace MostafaSaidPortfolio.Data.Repositories.Interfaces
{
    public interface ITestimonialRepository : IRepository<Testimonial>
    {
        Task<IEnumerable<Testimonial>> GetApprovedAsync();
        Task<bool> ApproveAsync(int id);
        Task<bool> RejectAsync(int id);
    }
}
