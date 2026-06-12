using MostafaSaidPortfolio.Data.UnitOfWork;
using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _uow;

    public CategoryService(IUnitOfWork uow) => _uow = uow;

    public Task<IEnumerable<Category>> GetActiveAsync() =>
        _uow.Categories.GetActiveAsync();

    public Task<Category?> GetBySlugAsync(string slug) =>
        _uow.Categories.GetBySlugAsync(slug);
}
