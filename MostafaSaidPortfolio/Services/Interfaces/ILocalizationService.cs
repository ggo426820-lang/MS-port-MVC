using System.Collections.Generic;
using System.Threading.Tasks;

namespace MostafaSaidPortfolio.Services.Interfaces
{
    public interface ILocalizationService
    {
        // Get all supported cultures (e.g., "en", "fr", "ar")
        Task<IEnumerable<string>> GetSupportedCulturesAsync();

        // Set the current culture
        Task SetCultureAsync(string culture);

        // Get the current culture
        Task<string> GetCurrentCultureAsync();
    }
}
