using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Domain.Enums;

namespace MostafaSaidPortfolio.Data.Repositories.Interfaces
{
    public interface INewsletterRepository : IRepository<NewsletterSubscriber>
    {
        Task<bool> SubscribeAsync(string email);
        Task<bool> UnsubscribeAsync(string email);
        Task<NewsletterSubscriber?> GetByEmailAsync(string email);
        Task<IEnumerable<NewsletterSubscriber>> GetActiveAsync();
        Task<int> CountActiveAsync();
    }
}

