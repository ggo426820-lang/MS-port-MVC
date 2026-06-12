using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Domain.Enums;

namespace MostafaSaidPortfolio.Services.Interfaces
{
    public interface INewsletterService
    {
        Task<bool> SubscribeAsync(string email);
        Task<bool> UnsubscribeAsync(string email);
        Task<IEnumerable<NewsletterSubscriber>> GetAllAsync();
    }
}

