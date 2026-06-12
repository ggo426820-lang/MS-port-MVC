using MostafaSaidPortfolio.Data.Repositories.Implementations;
using MostafaSaidPortfolio.Data.Repositories.Interfaces;
using Npgsql;

namespace MostafaSaidPortfolio.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NpgsqlConnection _connection;
        private NpgsqlTransaction? _transaction;

        private readonly BlogRepository _blogs;
        private readonly ProjectRepository _projects;
        private readonly TestimonialRepository _testimonials;
        private readonly NewsletterRepository _newsletter;
        private readonly EventRepository _events;
        private readonly ContactMessageRepository _contactMessages;
        private readonly CategoryRepository _categories;
        private readonly SkillRepository _skills;

        public IBlogRepository Blogs => _blogs;
        public IProjectRepository Projects => _projects;
        public ITestimonialRepository Testimonials => _testimonials;
        public INewsletterRepository Newsletter => _newsletter;
        public IEventRepository Events => _events;
        public IContactMessageRepository ContactMessages => _contactMessages;
        public ICategoryRepository Categories => _categories;
        public ISkillRepository Skills => _skills;

        private IReadOnlyList<dynamic> AllRepositories => new dynamic[]
        {
            _blogs, _projects, _testimonials, _newsletter,
            _events, _contactMessages, _categories, _skills
        };

        public UnitOfWork(DbConnectionFactory factory)
        {
            _connection = factory.CreateConnection();
            _connection.Open();

            _blogs = new BlogRepository(_connection);
            _projects = new ProjectRepository(_connection);
            _testimonials = new TestimonialRepository(_connection);
            _newsletter = new NewsletterRepository(_connection);
            _events = new EventRepository(_connection);
            _contactMessages = new ContactMessageRepository(_connection);
            _categories = new CategoryRepository(_connection);
            _skills = new SkillRepository(_connection);
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _connection.BeginTransactionAsync();
            _blogs.SetTransaction(_transaction);
            _projects.SetTransaction(_transaction);
            _testimonials.SetTransaction(_transaction);
            _newsletter.SetTransaction(_transaction);
            _events.SetTransaction(_transaction);
            _contactMessages.SetTransaction(_transaction);
            _categories.SetTransaction(_transaction);
            _skills.SetTransaction(_transaction);
        }

        public async Task CommitAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No active transaction to commit.");
            await _transaction.CommitAsync();
            await ClearTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await ClearTransactionAsync();
            }
        }

        private async Task ClearTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
            _blogs.SetTransaction(null);
            _projects.SetTransaction(null);
            _testimonials.SetTransaction(null);
            _newsletter.SetTransaction(null);
            _events.SetTransaction(null);
            _contactMessages.SetTransaction(null);
            _categories.SetTransaction(null);
            _skills.SetTransaction(null);
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
                await _transaction.DisposeAsync();
            await _connection.DisposeAsync();
        }
    }
}
