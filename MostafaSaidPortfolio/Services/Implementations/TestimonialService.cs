using MostafaSaidPortfolio.Data.UnitOfWork;
using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Domain.Enums;
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

        public Task<Testimonial?> GetByIdAsync(int id) =>
            _uow.Testimonials.GetByIdAsync(id);

        public async Task<Testimonial> AddAsync(Testimonial entity)
        {
            entity.Id = await _uow.Testimonials.AddAsync(entity);
            return entity;
        }

        public Task<bool> DeleteAsync(int id) =>
            _uow.Testimonials.DeleteAsync(id);
    }
}

