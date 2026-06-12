using MostafaSaidPortfolio.Domain.Entities;

namespace MostafaSaidPortfolio.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetActiveAsync();
    Task<Category?> GetBySlugAsync(string slug);
}
