using System.Collections.Generic;
using System.Threading.Tasks;
using MostafaSaidPortfolio.Models;

namespace MostafaSaidPortfolio.Services.Interfaces
{
    public interface IEventsService
    {
        // Get all events
        Task<IEnumerable<Event>> GetAllAsync();

        // Get event by Id
        Task<Event?> GetByIdAsync(int id);

        // Add a new event
        Task<Event> AddAsync(Event entity);

        // Update an existing event
        Task<Event> UpdateAsync(Event entity);

        // Delete event by Id
        Task<bool> DeleteAsync(int id);
    }
}
