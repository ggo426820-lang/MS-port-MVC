using MostafaSaidPortfolio.Models;

namespace MostafaSaidPortfolio.Services.Interfaces
{
    public interface IEventsService
    {
        Task<IEnumerable<Event>> GetUpcomingAsync();
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(int id);
        Task<Event> AddAsync(Event entity);
        Task<bool> DeleteAsync(int id);
    }
}
