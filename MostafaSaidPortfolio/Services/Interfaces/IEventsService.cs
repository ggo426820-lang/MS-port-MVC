using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Services.Interfaces
{
    public interface IEventsService
    {
        Task<IEnumerable<Event>> GetUpcomingAsync();
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(Guid id);
        Task<Event> AddAsync(Event entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
