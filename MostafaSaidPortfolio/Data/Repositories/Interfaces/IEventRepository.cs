using MostafaSaidPortfolio.Models;

namespace MostafaSaidPortfolio.Data.Repositories.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<IEnumerable<Event>> GetUpcomingAsync();
        Task<IEnumerable<Event>> GetPastAsync();
        Task<int> CountUpcomingAsync();
    }
}
