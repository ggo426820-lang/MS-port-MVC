using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Data.Repositories.Interfaces
{
    public interface IContactMessageRepository : IRepository<ContactMessage>
    {
        Task<IEnumerable<ContactMessage>> GetUnreadAsync();
        Task<IEnumerable<ContactMessage>> GetRecentAsync(int count);
        Task<bool> MarkAsReadAsync(Guid id);
        Task<bool> MarkAllAsReadAsync();
        Task<int> CountUnreadAsync();
        new Task<bool> DeleteAsync(Guid id);
    }
}
