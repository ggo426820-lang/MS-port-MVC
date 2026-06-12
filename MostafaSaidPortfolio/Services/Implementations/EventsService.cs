using MostafaSaidPortfolio.Data.UnitOfWork;
using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class EventsService : IEventsService
    {
        private readonly IUnitOfWork _uow;

        public EventsService(IUnitOfWork uow) => _uow = uow;

        public Task<IEnumerable<Event>> GetUpcomingAsync() =>
            _uow.Events.GetUpcomingAsync();

        public Task<IEnumerable<Event>> GetAllAsync() =>
            _uow.Events.GetAllAsync();

        public Task<Event?> GetByIdAsync(Guid id) =>
            _uow.Events.GetByIdAsync(id);

        public async Task<Event> AddAsync(Event entity)
        {
            await _uow.Events.AddAsync(entity);
            return entity;
        }

        public Task<bool> DeleteAsync(Guid id) =>
            _uow.Events.DeleteAsync(id);
    }
}
