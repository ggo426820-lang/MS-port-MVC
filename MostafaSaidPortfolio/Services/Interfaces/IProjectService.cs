using System.Collections.Generic;
using System.Threading.Tasks;
using MostafaSaidPortfolio.Models;

namespace MostafaSaidPortfolio.Services.Interfaces
{
    public interface IProjectService
    {
        // Get all projects
        Task<IEnumerable<Project>> GetAllAsync();

        // Get project by Id
        Task<Project?> GetByIdAsync(int id);

        // Add a new project
        Task<Project> AddAsync(Project entity);

        // Update an existing project
        Task<Project> UpdateAsync(Project entity);

        // Delete project by Id
        Task<bool> DeleteAsync(int id);
    }
}
