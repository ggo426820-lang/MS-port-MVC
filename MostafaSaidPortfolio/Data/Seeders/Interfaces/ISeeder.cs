using Npgsql;

namespace MostafaSaidPortfolio.Data.Seeders.Interfaces;

/// <summary>
/// Interface for database seeders
/// </summary>
public interface ISeeder
{
    /// <summary>
    /// Seed data for specific entity
    /// </summary>
    Task SeedAsync(NpgsqlConnection connection);
}
