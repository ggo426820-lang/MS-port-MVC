using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Data.Repositories.Interfaces
{
    public interface IContactMessageRepository : IRepository<ContactMessage>
    {
        Task<IEnumerable<ContactMessage>> GetUnreadAsync();
        Task<bool> MarkAsReadAsync(Guid id);
        Task<bool> MarkAllAsReadAsync();
        Task<int> CountUnreadAsync();
    }
}
