using MostafaSaidPortfolio.Data.Repositories.Interfaces;

namespace MostafaSaidPortfolio.Data.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IBlogRepository Blogs { get; }
        IProjectRepository Projects { get; }
        ITestimonialRepository Testimonials { get; }
        INewsletterRepository Newsletter { get; }
        IEventRepository Events { get; }
        IContactMessageRepository ContactMessages { get; }
        ICategoryRepository Categories { get; }
        ISkillRepository Skills { get; }

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
