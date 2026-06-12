using MostafaSaidPortfolio.Data.UnitOfWork;
using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class TestimonialService : ITestimonialService
    {
        private readonly IUnitOfWork _uow;

        public TestimonialService(IUnitOfWork uow) => _uow = uow;

        public Task<IEnumerable<Testimonial>> GetApprovedAsync() =>
            _uow.Testimonials.GetApprovedAsync();

        public Task<IEnumerable<Testimonial>> GetAllAsync() =>
            _uow.Testimonials.GetAllAsync();

        public Task<Testimonial?> GetByIdAsync(Guid id) =>
            _uow.Testimonials.GetByIdAsync(id);

        public async Task<Testimonial> AddAsync(Testimonial entity)
        {
            await _uow.Testimonials.AddAsync(entity);
            return entity;
        }

        public Task<bool> DeleteAsync(Guid id) =>
            _uow.Testimonials.DeleteAsync(id);
    }
}
