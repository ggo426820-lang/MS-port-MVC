using MostafaSaidPortfolio.Data.UnitOfWork;
using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Domain.Enums;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class NewsletterService : INewsletterService
    {
        private readonly IUnitOfWork _uow;

        public NewsletterService(IUnitOfWork uow) => _uow = uow;

        public Task<bool> SubscribeAsync(string email) =>
            _uow.Newsletter.SubscribeAsync(email.Trim().ToLowerInvariant());

        public Task<bool> UnsubscribeAsync(string email) =>
            _uow.Newsletter.UnsubscribeAsync(email.Trim().ToLowerInvariant());

        public Task<IEnumerable<NewsletterSubscriber>> GetAllAsync() =>
            _uow.Newsletter.GetAllAsync();
    }
}

