using System.Collections.Generic;
using System.Threading.Tasks;
using MostafaSaidPortfolio.Models;

namespace MostafaSaidPortfolio.Services.Interfaces
{
    public interface INewsletterService
    {
        // Get all subscribers
        Task<IEnumerable<NewsletterSubscriber>> GetAllAsync();

        // Get subscriber by Id
        Task<NewsletterSubscriber?> GetByIdAsync(int id);

        // Add a new subscriber
        Task<NewsletterSubscriber> AddAsync(NewsletterSubscriber entity);

        // Update an existing subscriber
        Task<NewsletterSubscriber> UpdateAsync(NewsletterSubscriber entity);

        // Delete subscriber by Id
        Task<bool> DeleteAsync(int id);
    }
}
